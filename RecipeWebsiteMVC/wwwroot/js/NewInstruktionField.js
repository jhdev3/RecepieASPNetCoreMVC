let NewDirectionButton = document.getElementById("NewDirection");
let indexDirection = document.getElementById("DirectionCount").value;

let wrapperOut = document.getElementById("NewFieldOutPut");
console.log(indexDirection);

NewDirectionButton.addEventListener("click", function () {
    let newFields = document.createElement("div");
    //
    newFields.setAttribute("class", "form-group d-flex align-items-end mt-1");
    newFields.appendChild(CreateInputDirectionText(indexDirection));
    newFields.appendChild(CreateDeleteButtonDir());  
    wrapperOut.appendChild(newFields);
    ++indexDirection;
    console.log(indexDirection);

});
function RemoveFieldsDir(element) {
    element.parentElement.remove();
    --indexDirection;
    console.log(indexDirection);

}
function CreateInputDirectionText(i) {
    let inputField = document.createElement("TEXTAREA");
    inputField.setAttribute("rows", "3");
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
//Skulle kunna göras I controller också eller i Custom Add Recipe eller liknande
