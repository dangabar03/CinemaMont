let btnRegister = document.getElementById("register");
let registerRoute = "http://localhost:5129/register";

btnRegister.addEventListener("click", async (e) => {
    e.preventDefault();
    let email = document.getElementById("emailreg").value;
    let password = document.getElementById("passwordreg").value;

    if (!email || !password) {
        console.log("Morate uneti oba parametra!");
        return;
    }

    try {
        const response = await fetch(registerRoute, {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Username: email, Password: password })
        });

        const user = await response.json();
        console.log("Registrovan: " + user);


    } catch (error) {
        console.log(error);
    }
});


