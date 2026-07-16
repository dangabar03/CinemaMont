const loginRoute = "http://localhost:5129/login";
var btn = document.getElementById("login");

btn.addEventListener("click", async (e) => {
    e.preventDefault();
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;


    if (!email || !password) {
        console.log("Morate uneti oba parametra!");
        return;
    }
    //poziv bekendu
    const response = await fetch(loginRoute, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({Username: email, Password: password})
    });

    var status = response.status;
    if(status == 400 || status == 401) {
        alert("Failed to log in.");
        return;
    }

    alert("ULOGOVAN SI:" + email);

}); 