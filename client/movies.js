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

printData();


