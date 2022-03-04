// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let NewIngridientButton = document.getElementById("NewIngridient");
let index = 1;
let wrapper = document.getElementById("input_fields_ingridients");

console.log(wrapper);
NewIngridientButton.addEventListener("click", function () {
  let newFields = document.createElement("div");
  newFields.appendChild(CreateInputUnit(index));
  newFields.appendChild(CreateInputUnitType(index));
  newFields.appendChild(CreateInputName(index));
  newFields.appendChild(CreateDeleteButton(index));

  wrapper.appendChild(newFields);
  ++index;
});

function RemoveFields(element) {
  element.parentElement.remove();
}

function CreateInputUnit(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "number");
  inputField.setAttribute("name", `Ingredients[${i}].Unit`);
  inputField.setAttribute("class", "form-control");
  inputField.setAttribute("placeholder", "Antal");

  return inputField;
}

function CreateInputUnitType(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Ingredients[${i}].UnitType`);
  inputField.setAttribute("class", "form-control");
  inputField.setAttribute("placeholder", "Enhet");

  return inputField;
}
function CreateInputName(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Ingredients[${i}].Name`);
  inputField.setAttribute("class", "form-control");
  inputField.setAttribute("placeholder", "Namn");
  return inputField;
}

function CreateDeleteButton() {
  let deleteButton = document.createElement("button");
  deleteButton.value = "Ta bort";
  deleteButton.setAttribute("class", "Remove_Fields");
  deleteButton.setAttribute("onclick", "RemoveFields(this)");
  return deleteButton;
}
