const title = document.querySelector("#title")
const meter = document.querySelector("#meter")
const user = JSON.parse(localStorage.getItem('currentUser'))
title.textContent = `hello ${user.userName}`
let flag = false;

const GetDataFromForm = () => {
    const userName = document.querySelector("#updateUserNameInput").value
    const password = document.querySelector("#updatePasswordInput").value
    const firstName = document.querySelector("#updateFirstNameInput").value
    const lastName = document.querySelector("#updateLastNameInput").value
    const userId = user.userId
    return ({ userName, password, firstName, lastName, userId });
}

const ShowDetails = () => {
    const container = document.querySelector("#details");
    container.style.visibility = "visible";
}
const UpdateUser = async () => {
    if (!flag) {
        alert("Enter password again")
    }
    else {
        const UpdatedUser = GetDataFromForm();
        try {
            const ResponsePut = await fetch(`api/User/${user.userId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(UpdatedUser)
            });
            if (ResponsePut.ok) {
                alert("Updated successfully");

            }
            else {
                if (ResponsePost.status == 409) {
                   alert("User name already exists") 
                }
                else {
                    alert("One or more details is wrong");
                }
            }
        }
        catch (error) {
            console.log(error)
        }
    }
}

const getPassword = () => {
    const password = document.querySelector("#updatePasswordInput").value
    return (password)
}

const checkPassword = async () => {
    const password = getPassword()
    try {
        const ResponsePost = await fetch(`api/User/Password`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)
        });
        const dataPost = await ResponsePost.json();
        if (!ResponsePost.ok) {
            alert("enter password")
        }
        else {
            if (dataPost < 3) {
                flag = false
                alert("your password is too weak")
            }
            else {
                flag = true
            }
            meter.value = (dataPost / 10) * 2 + 0.2
            return dataPost
        }
    }
    catch (error) {
        console.log(error)
    }
}
const goToProducts = () => {
    window.location.href=("Products.html")
}