

// Jquery Client Side DataTable 
//$(document).ready(function () {
//    GetEmployeeRecords();
//});
//var GetEmployeeRecords = function () {
//    $.ajax({
//        type: "POST",
//        url: "/Image/GetTableDataForEmployees",
//        success: function (response) {
//            debugger;
//            $("#table_id").DataTable({
//                "aaData": response,
//                "aoColumns": [
//                    { "mData": "EmpName"},                   
//                    { "mData": "DepartmentId"},
//                    { "mData": "DepartName"},
//                    { "mData": "Address" },
//                    {
//                        "mData": "EmployeeId",
//                        "render": function (EmployeeId, type, full, meta) {
//                            debugger;
//                            return '<a href="#" onclick="AddEditEmployees(' + EmployeeId + ')" ><i class="glyphicon glyphicon-pencil"></i></a>'
//                        }
//                    }

//                ]


//            });
//        }
//    })
//}




// Server Side DataTable 
$(document).ready(function () {
    BindTable();
});


var BindTable = function () {
    $("#table_id").DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Image/GetTableDataForEmployees",
        "fnServerData": function (sSource, aoData, fnCallBack) {

            $.ajax({
                type: "POST",
                url: sSource,
                data: aoData,
                success: fnCallBack
            })
        },
        "aoColumns": [
            { "mData": "EmpName" },
            { "mData": "DepartmentId" },
            { "mData": "DepartName" },
            { "mData": "Address" },
            {
                "mData": "EmployeeId",
                "render": function (EmployeeId, type, full, meta) {
                    debugger;
                    return '<a href="#" onclick="AddEditEmployees(' + EmployeeId + ')" ><i class="glyphicon glyphicon-pencil"></i></a>'
                }
            }

        ]


    });
}