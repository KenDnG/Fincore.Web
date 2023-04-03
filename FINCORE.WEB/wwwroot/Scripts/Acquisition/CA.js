jQuery(document).ready(function () {
    ca_load.init();

    //Setup responsiove UI on Mobile
    const pnlCMO = document.getElementById("CMO_more");
    pnlCMO?.addEventListener('click', () => {
        ca_load.changeClass_CMO();
    });
    const pnlDokumen = document.getElementById("Dokumen_more");
    pnlDokumen?.addEventListener('click', () => {
        ca_load.changeClass_Dokumen();
    });

    //    $("#contactform").validate({
    //    errorClass: "text-danger",
    //    rules: {
    //        Capacity: {
    //            required: true
    //        },
    //        Capital: {
    //            required: true
    //        },
    //        Character: {
    //            required: true
    //        },
    //        Condition: {
    //            required: true
    //        },
    //        Collateral: {
    //            required: true
    //        },
    //        Weakness: {
    //            required: true
    //        },
    //        Strenght: {
    //            required: true
    //        },
    //    },
    //    messages: {
    //        Capacity: "Masukkan Capacity",
    //        Capital: "Masukkan Capital",
    //        Character: "Masukkan Character",
    //        Condition: "Masukkan Condition",
    //        Collateral: "Masukkan Collateral",
    //        Weakness: "Masukkan Weakness",
    //        Strenght: "Masukkan Strenght"
    //    }
    //});
    $("#btn-tes").click(function () {
        if ($('#Capacity').val() == '') {
            alert('Capacity tidak boleh kosong');
            $('#Capacity').focus();
            return false;
        }

        if ($('#Capital').val() == '') {
            alert('Capital tidak boleh kosong');
            $('#Capital').focus();
            return false;
        }
        if ($('#Capital').val() == '') {
            alert('Capital tidak boleh kosong');
            $('#Capital').focus();
            return false;
        }
        if ($('#Character').val() == '') {
            alert('Character tidak boleh kosong');
            $('#Character').focus();
            return false;
        }
        if ($('#Condition').val() == '') {
            alert('Condition tidak boleh kosong');
            $('#Condition').focus();
            return false;
        }
        if ($('#Collateral').val() == '') {
            alert('Collateral tidak boleh kosong');
            $('#Collateral').focus();
            return false;
        }
        if ($('#Weakness').val() == '') {
            alert('Weakness tidak boleh kosong');
            $('#Weakness').focus();
            return false;
        }
        if ($('#Strenght').val() == '') {
            alert('Strenght tidak boleh kosong');
            $('#Strenght').focus();
            return false;
        }

        for (let i = 0; i < $('#rowmandatory').val(); i++) {
            if ($('#cekfotomandatory_' + i).val() == '') {
                alert($('#messagemandatory_' + i).val() + ' tidak boleh kosong');
                $('#cekfotomandatory_' + i).focus();
                return false;
            }
        }
        for (let i = 0; i < $('#rowrumah').val(); i++) {
            if ($('#cekfotorumah_' + i).val() == '') {
                alert($('#messagerumah_' + i).val() + ' tidak boleh kosong');
                $('#cekfotorumah_' + i).focus();
                return false;
            }
        }
        for (let i = 0; i < $('#rownasabah').val(); i++) {
            if ($('#cekfotonasabah_' + i).val() == '') {
                alert($('#messagenasabah_' + i).val() + ' tidak boleh kosong');
                $('#cekfotonasabah_' + i).focus();
                return false;
            }
        }
        for (let i = 0; i < $('#rowanakeuangan').val(); i++) {
            if ($('#cekfotoanakeuangan_' + i).val() == '') {
                alert($('#messageanakeuangan_' + i).val() + ' tidak boleh kosong');
                $('#cekfotoanakeuangan_' + i).focus();
                return false;
            }
        }
        for (let i = 0; i < $('#rowdokkendaraan').val(); i++) {
            if ($('#cekfotodokkendaraan_' + i).val() == '') {
                alert($('#messagedokkendaraan_' + i).val() + ' tidak boleh kosong');
                $('#cekfotodokkendaraan_' + i).focus();
                return false;
            }
        }
        for (let i = 0; i < $('#rowkendaraan').val(); i++) {
            if ($('#cekfotokendaraan_' + i).val() == '') {
                alert($('#messagekendaraan_' + i).val() + ' tidak boleh kosong');
                $('#cekfotokendaraan_' + i).focus();
                return false;
            }
        }
        //var cekmandatory = [];
        //for (let i = 0; i < $('#rowmandatory').val(); i++) {
        //    if ($('#formmandatory_' + i).val() != '') {
        //        cekmandatory.push(
        //            {
        //                filemandatory: $('#formmandatory_' + i)[0].files[0],
        //                namemandatory: $('#namemandatory_' + i).val(),
        //                idmandatory: $('#photoid_' + i).val(),
        //                typeidmandatory: $('#phototypeid_' + i).val(),
        //            });
        //    }
        //}
        //var data = {
        //    CASId: $('#CASId').val(),
        //    Capacity: $('#Capacity').val(),
        //    Capital: $('#Capital').val(),
        //    Character: $('#Character').val(),
        //    Condition: $('#Condition').val(),
        //    Collateral: $('#Collateral').val(),
        //    Weakness: $('#Weakness').val(),
        //    Strenght: $('#Strenght').val(),
        //    MandatoryEdit: cekmandatory,

        //}
        //$.ajax({
        //    type: "POST",
        //    url: "/CA/SaveCA",
        //    data: data,
        //    dataType: "json",
        //    enctype: "multipart/form-data",
        //    success: function (respon) {
        //        if (respon.status == true) {
        //            alert("Data berhasil disave")
        //        }
        //        else {
        //            alert(respon.data)
        //        }
        //    }
        //});
    });
    //    //var CASId = $(this).data("id");
    //    //$.ajax({
    //    //    type: "POST",
    //    //    url: "Aquisition/CA/SaveCA",
    //    //    data: { CASId: CASId },
    //    //    datatype: "json",
    //    //    success: function (respon) {
    //    //        if (respon.status == true) {
    //    //            alert("Data berhasil disave")
    //    //        }
    //    //        else {
    //    //            alert(respon.data)
    //    //        }
    //    //    }
    //    //})
    //});

    //contactform
    //$(".btn-edit").click(function () {
    //})
});
var ca_load = {
    init: function () {
        this.listdata();
        this.TogglePanel();
        //this.save();
        //this.update();
    },
    changeClass_CMO: function () {
        var content = document.getElementById("CMO");

        var identitas = document.getElementById("CMO_more");
        content.classList.toggle('show');

        if (content.classList.contains("CMOShow")) {
            identitas.innerHTML = "Data CMO & Dealer";
        } else {
            identitas.innerHTML = "Data CMO & Dealer";
        }
    },
    changeClass_Dokumen: function () {
        var content = document.getElementById("Dokumen");

        var identitas = document.getElementById("Dokumen_more");
        content.classList.toggle('show');

        if (content.classList.contains("DokumenShow")) {
            identitas.innerHTML = "Dokumen Nasabah";
        } else {
            identitas.innerHTML = "Dokumen Nasabah";
        }
    },
    listdata: function () {
        $('#tbl').dataTable({
            responsive: true,
            processing: true,
            language: {
                emptyTable: "CA data not available"
            },
            dom: 'lBfrtip',
            scrollCollapse: true,
            lengthMenu: [[10, 10, 8, 50], [10, 10, 8, 50]],
            order: [],
            scrollX: true,
            scrollY: true,
            ajax: {
                url: "CA/GetData",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                dataSrc: ''
            },
            columns: [
                { data: "CASId" },
                { data: "DealerName" },
                { data: "EmployeeName" },
                { data: "StatusCas" },
                {
                    data: null,
                    render: function (data, type, full) {
                        var ids = "'" + full.CASId + "'";
                        var htmls = "";
                        htmls += '<button type="button" title="Edit" class="btn btn-dark btn-sm" onclick="ca_load.view(' + ids + ');"><i class="fa fa-pen"></i></button>';
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
        alert("CA selected:" + ids);
        //TO DO: View Data from DataTable List
    },
    TogglePanel: function () {
        $("btnCMO").click(function () {
            $("CMO").toggle();
        });

        $("btnDokumen").click(function () {
            $("Dokumen").toggle();
        });
    },

    //tes: function () {
    //    let divs = $('#show tes');

    //},
}

//    < script >
//    $(document).ready(function () {
//        $("#contactform").validate({
//            errorClass: "text-danger",
//            rules: {
//                Capacity: {
//                    required: true
//                },
//                Capital: {
//                    required: true
//                },
//                Character: {
//                    required: true
//                },
//                Condition: {
//                    required: true
//                },
//                Collateral: {
//                    required: true
//                },
//                Weakness: {
//                    required: true
//                },
//                Strenght: {
//                    required: true
//                },
//            },
//            messages: {
//                Capacity: "Masukkan Capacity",
//                Capital: "Masukkan Capital",
//                Character: "Masukkan Character",
//                Condition: "Masukkan Condition",
//                Collateral: "Masukkan Collateral",
//                Weakness: "Masukkan Weakness",
//                Strenght: "Masukkan Strenght"
//            }
//        })
//    });
//</script >