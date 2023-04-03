jQuery(document).ready(function () {
    crud_load.init();
});
var crud_load = {
    init: function () {
        this.listdata();

        this.save();
        this.update();
    },
    save: function () {
        $("#btnsave_crud").click(function () {
            var items = {
                KodeCabang: $("#txtkodecabang").val(),
                PoNo: $("#txtpono").val(),
                TipeKonsumen: $("#txttipekonsumen").val(),
                ExistingCustomerRO: $("#txtexistingcustomer").val(),
                NamaNasabah: $("#txtnamanasabah").val()
            }
            var ldBtn = Ladda.create(document.querySelector("#btnsave_crud"));
            $.ajax({
                url: "/CRUD/Insert",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(items),
                cache: false,
                beforeSend: function (xhr) {
                    ldBtn.start();
                }
            }).done(function (data, textStatus, jqXHR) {
                XResponse.Success(data);
                ldBtn.stop();
            }).fail(function (jqXHR, textStatus, errorThrown) {
                var err = textStatus;
                XResponse.Success(err);
                ldBtn.stop();
            });
        });
    },
    update: function () {
        $("#btnupdate_crud").click(function () {
            var items = {
                KodeCabang: $("#txtkodecabang").val(),
                PoNo: $("#txtpono").val(),
                TipeKonsumen: $("#txttipekonsumen").val(),
                ExistingCustomerRO: $("#txtexistingcustomer").val(),
                NamaNasabah: $("#txtnamanasabah").val()
            }
            var ldBtn = Ladda.create(document.querySelector("#btnsave_crud"));
            $.ajax({
                url: "/CRUD/Update",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(items),
                cache: false,
                beforeSend: function (xhr) {
                    ldBtn.start();
                }
            }).done(function (data, textStatus, jqXHR) {
                XResponse.Success(data);
                ldBtn.stop();
            }).fail(function (jqXHR, textStatus, errorThrown) {
                var err = textStatus;
                ldBtn.stop();
            });
        });
    },
    listdata: function () {
        $('#tbl').dataTable({
            responsive: true,
            processing: true,
            language: {
                emptyTable: "PO data not available"
            },
            dom: 'lBfrtip',
            scrollCollapse: true,
            lengthMenu: [[5, 10, 25, 50], [5, 10, 25, 50]],
            order: [],
            scrollX: true,
            scrollY: true,
            ajax: {
                url: "CRUD/GetData",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                dataSrc: ''
            },
            columns: [
                { data: "poNo" },
                { data: "kodeCabang" },
                { data: "namaNasabah" },
                { data: "tipeKonsumen" },
                {
                    data: null,
                    render: function (data, type, full) {
                        var ids = "'" + full.poNo + "'";
                        var htmls = "";
                        htmls += '<button type="button" title="Edit" class="btn btn-dark btn-sm" onclick="crud_load.view(' + ids + ');"><i class="fa fa-pen"></i></button>';
                        return htmls;
                    }
                }
            ],
            columnDefs: [
                //set width column
                { "targets": [0], "width": "10%", "className": "dt-center" },
                { "targets": [1], "width": "10%", "className": "dt-center" },
                { "targets": [2], "width": "10%", "className": "dt-center" },
                { "targets": [3], "width": "10%", "className": "dt-center" },
                { "targets": [4], "width": "3%", "className": "dt-center" }
            ],
            createdRow: function (row, data, index) {
            }
        });
    },
    view: function (ids) {
        alert("PO selected:" + ids);
        //TO DO: View Data from DataTable List
    }
}