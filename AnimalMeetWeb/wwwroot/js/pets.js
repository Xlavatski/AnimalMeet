var dataTable;

var SexEnum = { "Male": 0, "Female": 1};

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/User/Pets/GetAllPets",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "age", "width": "10%" },
            { "data": "sex", "width": "10%" },
            { "data": "user.name", "width": "10%" },
            { "data": "animalSubtype.name", "width": "10%" },
            { "data": "animalSubtype.animalType.name", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/User/pets/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/User/pets/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "15%"
            }
        ]
    });
}