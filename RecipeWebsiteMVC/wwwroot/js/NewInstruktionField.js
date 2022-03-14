let NewDirectionButton = document.getElementById("NewDirection");
let indexDirection = document.getElementById("DirectionCount").value;

let wrapperOut = document.getElementById("NewFieldOutPut");

NewDirectionButton.addEventListener("click", function () {
    let newFields = document.createElement("div");
    //
    newFields.setAttribute("class", "form-group d-flex align-items-end mt-1");
    newFields.appendChild(CreateInputDirectionText(indexDirection));
    newFields.appendChild(CreateDeleteButtonDir());
    wrapperOut.appendChild(newFields);
    ++indexDirection;
});
function RemoveFieldsDir(element) {
    element.parentElement.remove();
    --indexDirection;
}
function CreateInputDirectionText(i) {
    let inputField = document.createElement("TEXTAREA");
    inputField.setAttribute("rows", "2");
    inputField.setAttribute("name", `Directions[${i}].Text`);
    inputField.setAttribute("placeholder", "Instruktion");
    inputField.setAttribute("class", "form-control");

    
    return inputField;
}
function CreateDeleteButtonDir() {
    let deleteButton = document.createElement("input");
    deleteButton.setAttribute("type", "button");
    deleteButton.value = "Ta bort";
    deleteButton.setAttribute("class", "btn btn-outline-danger float-right");
    deleteButton.setAttribute("onclick", "RemoveFieldsDir(this)");
    return deleteButton;
}
