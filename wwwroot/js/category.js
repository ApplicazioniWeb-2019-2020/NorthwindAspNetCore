$(document).ready(function () {
    $("#jsGrid").jsGrid({
        width: "100%",

        inserting: true,
        editing: true,
        sorting: true,
        paging: true,
        filtering: true,

        autoload: true,
        pageLoading: true,
        pageSize: 10,

        controller: {
            loadData: function (filter) {
                return $.ajax({
                    type: "GET",
                    url: "/api/categories",
                    data: filter,
                    contentType: "application/json"
                });
            },
            insertItem: function (item) {
                return $.ajax({
                    type: "POST",
                    url: "/api/categories",
                    data: JSON.stringify(item),
                    contentType: "application/json",
                    dataType: "json"
                });
            },
            updateItem: function (item) {
                return $.ajax({
                    type: "PUT",
                    url: "/api/categories/" + item.id,
                    data: JSON.stringify(item),
                    contentType: "application/json",
                    dataType: "json"
                });
            },
            deleteItem: function (item) {
                return $.ajax({
                    type: "DELETE",
                    url: "/api/categories/" + item.id,
                    contentType: "application/json",
                    dataType: "json"
                });
            }
        },

        fields: [
            { name: "id", type: "number", width: 50, title: "Id" },
            { name: "name", type: "text", width: 150, validate: "required", title: "Name" },
            { name: "description", type: "text", width: 150, validate: "required", title: "Description" },
            { type: "control" }
        ]
    });
})