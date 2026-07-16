let btnLogin = document.getElementsByClassName("login-page");
let btnRegister = document.getElementsByClassName("register-page");

btnLogin[0].addEventListener("click", (e) => {
    window.location.href = "login.html";
});

btnRegister[0].addEventListener("click", (e) => {
    window.location.href = "register.html";
});