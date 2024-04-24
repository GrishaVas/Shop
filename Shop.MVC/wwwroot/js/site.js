// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function SetCheckboxes(checkbox) {
    var checkboxes = $("input[name='numbers']");
    var checkboxValue = checkbox.checked;

    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = checkboxValue;
    }
}

function SelectLink(number, name, createdBy) {
    $("input[name='Number']")[0].value = number;
    $("input[name='Name']")[0].value = name;
    $("input[name='CreatedBy']")[0].value = createdBy;
}