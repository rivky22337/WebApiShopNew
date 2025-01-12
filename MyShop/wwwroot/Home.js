
const meter = document.querySelector("#a")
const register = () => {
    const container = document.querySelector("#container");
    container.style.visibility = "visible";
}

const GetDataFromRegister = () => {
    const userName = document.querySelector("#registerUserNameInput").value
    const password = document.querySelector("#registerPasswordInput").value
    const firstName = document.querySelector("#registerFirstNameInput").value
    const lastName = document.querySelector("#registerLastNameInput").value

    return ({ userName, password, firstName, lastName })
}
const CreateNewUser = async () => {
    const newUser = GetDataFromRegister();
    try {
        const ResponsePost = await fetch('api/User', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        });
        if (ResponsePost.status == 400) {
            alert("One or more details is wrong");
        }
        if (ResponsePost.ok) {
            alert("Created successfully");
        } else {
            alert("Bad request");
        }
    }
    catch (Error) {
        alert(Error)
    }
}
const GetDataFromLogin = () => {
    const userName = document.querySelector("#loginUserNameInput").value
    const password = document.querySelector("#loginPasswordInput").value
    return ({ userName, password })
}




const Login = async () => {
    const details = GetDataFromLogin();
    try {
        const ResponsePost = await fetch(`api/User/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(details)
        });
        const dataPost = await ResponsePost.json();
        if (dataPost.status == 400)
            alert("wrong details, please try again!")
        else {
            localStorage.setItem("currentUser", JSON.stringify(dataPost))
            window.location.href = "UserDetails.html";
        }
    }
    catch (Error) {
        alert(Error)
    }
}

const getPassword = () => {
    const password = document.querySelector("#registerPasswordInput").value
    return ( password )
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
            alert("try again")
        }
        else {
            meter.value = (dataPost/10)*2+0.2
            return dataPost
        }
    }
    catch (Error) {
        alert(`error ${Error}`)
    }
}