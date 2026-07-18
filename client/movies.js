const btnAddMovie = document.getElementById("add-movie");

//PROMENI OVO, POSTOJI BOLJI NACIN DA SE OVO URADI
function checkAdmin() {
    const admin = localStorage.getItem("token");

    const arr = admin.split(".");
    const payload = arr[1];
    const decoded = atob(payload);
    const obj = JSON.parse(decoded);

    const role = obj["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if(role !== "ADMIN") {
        btnAddMovie.hidden = true;
        return;
    }
}

async function getData() {
    const allowedHost = "http://localhost:5129/movies";
    try {
        var response = await fetch(allowedHost);
        if(!response.ok) {
            throw new Error("MISTAKE");
        }

        const result = await response.json();
        return result;
    } catch (error) {
        console.log(error);
        return [];
    }
}

async function printData() {
    const movies = await getData();
    
    const list = document.getElementById("lista");
    console.log(movies);

    for(const mv of movies) {
        var li = document.createElement("li");
        li.textContent = mv.title;
        list.appendChild(li);
    }
}

checkAdmin();
printData();


