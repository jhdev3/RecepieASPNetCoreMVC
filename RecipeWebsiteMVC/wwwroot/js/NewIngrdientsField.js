let NewIngridientButton = document.getElementById("NewIngridient");
let index = document.getElementById("IngridientCount").value;
let wrapper = document.getElementById("input_fields_ingridients");

//console.log(inputValue);

NewIngridientButton.addEventListener("click", function () {
  let newFields = document.createElement("div");
    newFields.setAttribute("class", "form-group d-flex align-items-end mt-1")

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
    inputField.setAttribute("class", "form-control");


  return inputField;
}

function CreateInputUnitType(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Ingredients[${i}].UnitType`);
    inputField.setAttribute("placeholder", "Enhet");
    inputField.setAttribute("class", "form-control");


  return inputField;
}
function CreateInputName(i) {
  let inputField = document.createElement("input");
  inputField.setAttribute("type", "text");
  inputField.setAttribute("name", `Ingredients[${i}].Name`);
    inputField.setAttribute("placeholder", "Namn");
    inputField.setAttribute("class", "form-control");

  return inputField;
}

function CreateDeleteButton() {
  let deleteButton = document.createElement("input");
  deleteButton.setAttribute("type", "button");
  deleteButton.value = "Ta bort";
  deleteButton.setAttribute("class", "btn btn-outline-danger mt-1");
  deleteButton.setAttribute("onclick", "RemoveFields(this)");
  return deleteButton;
}
