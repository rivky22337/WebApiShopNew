const title = document.querySelector("#title")
const meter = document.querySelector("#meter")
const user = JSON.parse(localStorage.getItem('currentUser'))
title.textContent = `hello ${user.userName}`

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
    const UpdatedUser = GetDataFromForm();
    try {
        const ResponsePut = await fetch(`api/User/${user.userId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(UpdatedUser)
        });
        if (ResponsePut.status == 400) {
            alert("One or more details is wrong");
        }
        if (ResponsePut.ok) {
            alert("Updated successfully");
        } else {
            alert("Bad request");
        }
    }
    catch (Error) {
        console.log(Error)
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
            alert("enter password again")
        }
        else {
            meter.value = (dataPost / 10) * 2 + 0.2
            return dataPost
        }
    }
    catch (Error) {
        alert(`error ${Error}`)
    }
}