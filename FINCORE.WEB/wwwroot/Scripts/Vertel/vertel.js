jQuery(document).ready(function () {
    vertel_load.init();
    $('#dtTanggalKonfirmasi, #dtTglTerimaPinjaman, #dtTglTerimaSebenarnya,#dtJatuhTempo, #tanggalaktaterakhir, #dtTglTerimaTagihan').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    });

    //Ladda.bind(document.getElementById("btnLoadModal"), {
    //    callback: function (instance) {
    //        var progress = 0;
    //        var interval = setInterval(function () {
    //            progress = Math.min(progress + Math.random() * 0.035, 1);
    //            instance.setProgress(progress);

    //            if (progress === 1) {
    //                instance.stop();
    //                clearInterval(interval);
    //            }
    //        }, 200);
    //    }
    //});
});

var vertel_load = {
    init: function () {
        this.listdata();
        this.LoadModal();
        this.InitialConvertMoney();

        //this.save();
        //this.update();
    },
    listdata: function () {
        $('#tbl').dataTable({
            responsive: true,
            processing: true,
            language: {
                emptyTable: "Vertel data not available"
            },
            dom: 'lBfrtip',
            scrollCollapse: true,
            lengthMenu: [[5, 10, 25, 50], [5, 10, 25, 50]],
            order: [],
            scrollX: true,
            scrollY: true,
            ajax: {
                url: "VERTEL/GetData",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                dataSrc: ''
            },
            columns: [
                { data: "verifikasiNo" },
                { data: "cmNo" },
                { data: "agreementNumber" },
                { data: "approveDate" },
                { data: "customerName" },
                { data: "tipeKendaraan" },
                { data: "statusVerifikasi" },
                {
                    data: null,
                    render: function (data, type, full) {
                        var ids = "'" + full.verifikasiNo + "'";
                        console.log(ids);
                        var htmls = "";
                        htmls += '<button type="button" title="Edit" class="btn btn-dark btn-sm" src="~/assets/ico/edit.png" onclick="vertel_load.edit(' + ids + ');">  </button>';
                        return htmls;
                    }
                },
                {
                    data: null,
                    render: function (data, type, full) {
                        var ids = "'" + full.verifikasiNo + "'";
                        var htmls = "";
                        htmls += '<button type="button" title="Print" class="btn btn-dark btn-sm" <img src="~/assets/ico/print.png">  onclick="vertel_load.print(' + ids + ');"><i class="fa fa-pen"></i></button>';
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
                { "targets": [4], "width": "10%", "className": "dt-center" },
                { "targets": [5], "width": "10%", "className": "dt-center" },
                { "targets": [6], "width": "10%", "className": "dt-center" },
                { "targets": [7], "width": "3%", "className": "dt-center" },
                { "targets": [8], "width": "3%", "className": "dt-center" }
            ],
            createdRow: function (row, data, index) {
            }
        });
    },
    edit: function (ids) {
        alert("Vertel selected:" + ids + "Edited");
        //TO DO: View Data from DataTable List
    },
    print: function (ids) {
        alert("Printed Vertel:" + ids);
    },
    LoadModal: function () {
        $("#btnLoadModal").click(function () {
            $.ajax({
                url: "VertelLookUp",
                data: $(this).serialize(),
                type: "POST",
                success: function (data) {
                    $('#exampleModalLong').find('.modal-body').html(data);
                    $('#exampleModalLong').modal({
                        backdrop: false
                    });
                },
                error: function (err) {
                }
            })
        })
    },
    InitialConvertMoney: function () {
        $("#InputAngsuranSebenarnya").val(vertel_load.FormatNumberMoney($("#InputAngsuranSebenarnya").val()));
        $("#InputAngsuranSebenarnya").keyup(function () {
            $("#InputAngsuranSebenarnya").val(vertel_load.FormatNumberMoney($("#InputAngsuranSebenarnya").val().replaceAll(".", "")));
        });

        $("#InputDPSebenarnya").val(vertel_load.FormatNumberMoney($("#InputDPSebenarnya").val()));
        $("#InputDPSebenarnya").keyup(function () {
            $("#InputDPSebenarnya").val(vertel_load.FormatNumberMoney($("#InputDPSebenarnya").val().replaceAll(".", "")));
        });

        $("#InputBiayaAdmin").val(vertel_load.FormatNumberMoney($("#InputBiayaAdmin").val()));
        $("#InputBiayaAdmin").keyup(function () {
            $("#InputBiayaAdmin").val(vertel_load.FormatNumberMoney($("#InputBiayaAdmin").val().replaceAll(".", "")));
        });
    },
    FormatNumberMoney: function (value) {
        if (value != undefined) {
            let nf = new Intl.NumberFormat('id-ID');

            if (nf.format(value) == "NaN") {
                return ""
            }
            else {
                return nf.format(value);
            }
        }
    }
}