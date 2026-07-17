let btnLogin = document.getElementsByClassName("login-page");
let btnRegister = document.getElementsByClassName("register-page");
let btnLogout = document.getElementsByClassName("logout");

function checkLog() {
    let tok = localStorage.getItem("token");

    if (tok == null) {
        if (btnLogout[0]) btnLogout[0].hidden = true;
    } else {
        if (btnLogin[0]) btnLogin[0].hidden = true;
        if (btnRegister[0]) btnRegister[0].hidden = true;
    }
}

checkLog();

btnLogin[0].addEventListener("click", (e) => {
    window.location.href = "login.html";
});

btnRegister[0].addEventListener("click", (e) => {
    window.location.href = "register.html";
});



btnLogout[0].addEventListener("click", (e) => {
    localStorage.removeItem("token");
    window.location.href = "index.html";
});