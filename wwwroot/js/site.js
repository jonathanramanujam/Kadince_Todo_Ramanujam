// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function EditTodoItem(id) {
    let editElement = document.getElementById("TodoEdit" + id);
    let displayElement = document.getElementById("TodoDisplay" + id);
    if (editElement.style.display === "none") {
        displayElement.style.display = "none";
        editElement.style.display = "block";
    }
    else {
        // Save Todo Item
        editElement.style.display = "none";
        displayElement.style.display = "block";
    }
}

// This should be called on get to set the current filters.
// Will need to bind filter options to the page
// Maybe this is just controlling the buttons being solid or outlined?
//function FilterTodoItems() {
//    let todoElements = document.getElementsByClassName("card-todo");
//    let all = document.getElementById("btn-filter-all");
//    let complete = document.getElementById("btn-filter-complete");
//    let incomplete = document.getElementById("btn-filter-incomplete");

//    if (all.value) {
//        for (let i = 0; i < todoElements.length; i++) {

//        }
//        all.value = true;
//        complete.value = true;
//        incomplete.value = true;
//    }

//    var todoElements = document.getElementsByClassName("fComplete-True");
//    for (let i = 0; i < todoElements.length; i++) {
//        todoElements[i].style.display = "none";
//    }
//}