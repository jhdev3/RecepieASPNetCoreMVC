let NewIngridientButton = document.getElementById("NewIngridient");
let index = 1;
let wrapper = document.getElementById("input_fields_ingridients");

console.log(wrapper);
NewIngridientButton.addEventListener("click", function () {
  let newFields = document.createElement("div");
  newFields.setAttribute("class", "form-control");

  newFields.appendChild(CreateInputUnit(index));
  newFields.appendChild(CreateInputUnitType(index));
  newFields.appendChild(CreateInputName(index));
  newFields.appendChild(CreateDeleteButton());

  wrapper.appendChild(newFields);
  ++index;
});

function RemoveFields(element) {
  element.parentElement.remove();
  --index;
}

function CreateInputUnit(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "number");
  inputField.setAttribute("name", `Ingredients[${i}].Unit`);
  inputField.setAttribute("placeholder", "Antal");

  return inputField;
}

function CreateInputUnitType(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Ingredients[${i}].UnitType`);
  inputField.setAttribute("placeholder", "Enhet");

  return inputField;
}
function CreateInputName(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Ingredients[${i}].Name`);
  inputField.setAttribute("placeholder", "Namn");
  return inputField;
}

function CreateDeleteButton() {
  let deleteButton = document.createElement("input");
  deleteButton.setAttribute("type", "button");
  deleteButton.value = "Ta bort";
  deleteButton.setAttribute("class", "btn btn-outline-danger");
  deleteButton.setAttribute("onclick", "RemoveFields(this)");
  return deleteButton;
}
