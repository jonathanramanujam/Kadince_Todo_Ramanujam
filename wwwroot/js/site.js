// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function EditTodoItem(id) {
    var editElement = document.getElementById("TodoEdit" + id);
    var infoElement = document.getElementById("TodoInfo" + id);
    if (editElement.style.display === "none") {
        infoElement.style.display = "none";
        editElement.style.display = "block";
    }
    else {
        // Save Todo Item
        editElement.style.display = "none";
        infoElement.style.display = "block";
    }
}