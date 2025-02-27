const user = JSON.parse(localStorage.getItem("currentUser"))
let items = JSON.parse(localStorage.getItem(`${user.userId}_CartItems`)) || []
const temp_row = document.querySelector('#temp-row'); 
const itemsList = document.querySelector('#items');
const itemCount = document.querySelector("#itemCount")
const totalAmount = document.querySelector("#totalAmount")

document.addEventListener('DOMContentLoaded', () => {
    itemCount.textContent = items.length
    if (items.length > 0) {
        totalAmount.textContent = items.reduce((sum, item) => sum + item.price, 0)
        items.forEach(item => {
            addItem(item)
        })
    }
    else {
        totalAmount.textContent = 0;
    }
})
const printItems = () => {
    itemsList.innerHTML = ""
    items.forEach(item => {
        addItem(item)
    })
}
const addItem = (item) => {
    const clone = temp_row.content.cloneNode(true);
    clone.querySelector('.itemName').textContent = item.productName;
    clone.querySelector('.price').textContent = `₪${item.price}`;
    clone.querySelector('.itemNumber').textContent = item.productId;
    const imageDiv = clone.querySelector('.image');
    imageDiv.style.backgroundImage = `url('/pictures/${item.imageUrl}.jpg')`;
    itemsList.appendChild(clone);
}
const removeItem = (item) =>{
    const parent = item.parentElement.parentElement.parentElement
    const itemId = parent.querySelector('.itemNumber').textContent
    const item2delete = items.find(i => i.productId == itemId)
    items.splice(items.indexOf(item2delete), 1)
    localStorage.setItem(`${user.userId}_CartItems`, JSON.stringify(items))
    totalAmount.textContent -= item2delete.price
    itemCount.textContent = items.length
    printItems()
}
const placeOrder = async () => {


    try {
        const body = {
            orderItems : items.map(i => ({ productId: i.productId })),
            orderDate: new Date().toISOString().slice(0, 10),
            userId: user.userId,
            orderSum:Number(totalAmount.textContent)
        }
        const ResponsePost = await fetch('api/Order', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });
        if (ResponsePost.status == 400) {
            alert("One or more details is wrong");
        }
        if (ResponsePost.ok) {
            alert("Created successfully");
            localStorage.setItem(`${user.userId}_CartItems`, JSON.stringify([]) )
            window.location.reload()
        } else {
            alert("Bad request");
        }
    }
    catch (Error) {
        alert(Error)
    }

}




