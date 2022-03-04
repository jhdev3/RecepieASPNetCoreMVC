let NewDirectionButton = document.getElementById("NewDirection");
let indexDirection = 1;
let wrapperOut = document.getElementById("NewFieldOutPut");

console.log(wrapper);
NewDirectionButton.addEventListener("click", function () {
  let newFields = document.createElement("div");
  newFields.setAttribute("class", "form-control");
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
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Directions[${i}].Text`);
  inputField.setAttribute("placeholder", "Instruktion");

  return inputField;
}
function CreateDeleteButtonDir() {
  let deleteButton = document.createElement("input");
  deleteButton.setAttribute("type", "button");
  deleteButton.value = "Ta bort";
  deleteButton.setAttribute("class", "btn btn-outline-danger");
  deleteButton.setAttribute("onclick", "RemoveFieldsDir(this)");
  return deleteButton;
}
