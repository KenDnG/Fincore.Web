$(document).ready(function () {
    var Flag = $('#txhiddenFlag').val();
    var CreditId = $('#txcredit_id').val();

    if (Flag != "Add") {
        var NIKCMO = $('#txhiddenNIKCMO').val();
        var NamaCMO = $('#txhiddenNamaCMO').val();
        var CMO_DataNasabah1 = $('#txhiddenCMO_DataNasabah1').val();
        var CMO_NPPNasabah1 = $('#txhiddenCMO_NPPNasabah1').val();
        var CMO_ODNasabah1 = $('#txhiddenCMO_ODNasabah1').val();
        var CMO_DataNasabah2 = $('#txhiddenCMO_DataNasabah2').val();
        var CMO_NPPNasabah2 = $('#txhiddenCMO_NPPNasabah2').val();
        var CMO_ODNasabah2 = $('#txhiddenCMO_ODNasabah2').val();
        var CMO_DataNasabah3 = $('#txhiddenCMO_DataNasabah3').val();
        var CMO_NPPNasabah3 = $('#txhiddenCMO_NPPNasabah3').val();
        var CMO_ODNasabah3 = $('#txhiddenCMO_ODNasabah3').val();
        var KodeDealer = $('#txhiddenKodeDealer').val();
        var NamaDealer = $('#txhiddenNamaDealer').val();
        var Dealer_DataNasabah1 = $('#txhiddenDealer_DataNasabah1').val();
        var Dealer_NPPNasabah1 = $('#txhiddenDealer_NPPNasabah1').val();
        var Dealer_ODNasabah1 = $('#txhiddenDealer_ODNasabah1').val();
        var Dealer_DataNasabah2 = $('#txhiddenDealer_DataNasabah2').val();
        var Dealer_NPPNasabah2 = $('#txhiddenDealer_NPPNasabah2').val();
        var Dealer_ODNasabah2 = $('#txhiddenDealer_ODNasabah2').val();
        var Dealer_DataNasabah3 = $('#txhiddenDealer_DataNasabah3').val();
        var Dealer_NPPNasabah3 = $('#txhiddenDealer_NPPNasabah3').val();
        var Dealer_ODNasabah3 = $('#txhiddenDealer_ODNasabah3').val();

        var Capacity = $('#txhiddenCapacity').val();
        var Collateral = $('#txhiddenCollateral').val();
        var Capital = $('#txhiddenCapital').val();
        var Character = $('#txhiddenCharacter').val();
        var Condition = $('#txhiddenCondition').val();
        var advantage_notes = $('#txhiddenadvantage_notes').val();
        var deficiency_notes = $('#txhiddendeficiency_notes').val();

        $('#NIKCMO').val(NIKCMO);
        $('#NamaCMO').val(NamaCMO);
        $('#CMO_DataNasabah1').val(CMO_DataNasabah1);
        $('#CMO_NPPNasabah1').val(CMO_NPPNasabah1);
        $('#CMO_ODNasabah1').val(CMO_ODNasabah1);
        $('#CMO_DataNasabah2').val(CMO_DataNasabah2);
        $('#CMO_NPPNasabah2').val(CMO_NPPNasabah2);
        $('#CMO_ODNasabah2').val(CMO_ODNasabah2);
        $('#CMO_DataNasabah3').val(CMO_DataNasabah3);
        $('#CMO_NPPNasabah3').val(CMO_NPPNasabah3);
        $('#CMO_ODNasabah3').val(CMO_ODNasabah3);
        $('#KodeDealer').val(KodeDealer);
        $('#NamaDealer').val(NamaDealer);
        $('#Dealer_DataNasabah1').val(Dealer_DataNasabah1);
        $('#Dealer_NPPNasabah1').val(Dealer_NPPNasabah1);
        $('#Dealer_ODNasabah1').val(Dealer_ODNasabah1);
        $('#Dealer_DataNasabah2').val(Dealer_DataNasabah2);
        $('#Dealer_NPPNasabah2').val(Dealer_NPPNasabah2);
        $('#Dealer_ODNasabah2').val(Dealer_ODNasabah2);
        $('#Dealer_DataNasabah3').val(Dealer_DataNasabah3);
        $('#Dealer_NPPNasabah3').val(Dealer_NPPNasabah3);
        $('#Dealer_ODNasabah3').val(Dealer_ODNasabah3);

        $('#Capacity').val(Capacity);
        $('#Collateral').val(Collateral);
        $('#Capital').val(Capital);
        $('#Condition').val(Condition);
        $('#Character').val(Character);
        $('#advantage_notes').val(advantage_notes);
        $('#deficiency_notes').val(deficiency_notes);
    }

    if (Flag == "Approval" || Flag == "View") {
        $('#NIKCMO').attr("readonly", true);
        $('#NamaCMO').attr("readonly", true);
        $('#CMO_DataNasabah1').attr("readonly", true);
        $('#CMO_NPPNasabah1').attr("readonly", true);
        $('#CMO_ODNasabah1').attr("readonly", true);
        $('#CMO_DataNasabah2').attr("readonly", true);
        $('#CMO_NPPNasabah2').attr("readonly", true);
        $('#CMO_ODNasabah2').attr("readonly", true);
        $('#CMO_DataNasabah3').attr("readonly", true);
        $('#CMO_NPPNasabah3').attr("readonly", true);
        $('#CMO_ODNasabah3').attr("readonly", true);
        $('#KodeDealer').attr("readonly", true);
        $('#NamaDealer').attr("readonly", true);
        $('#Dealer_DataNasabah1').attr("readonly", true);
        $('#Dealer_NPPNasabah1').attr("readonly", true);
        $('#Dealer_ODNasabah1').attr("readonly", true);
        $('#Dealer_DataNasabah2').attr("readonly", true);
        $('#Dealer_NPPNasabah2').attr("readonly", true);
        $('#Dealer_ODNasabah2').attr("readonly", true);
        $('#Dealer_DataNasabah3').attr("readonly", true);
        $('#Dealer_NPPNasabah3').attr("readonly", true);
        $('#Dealer_ODNasabah3').attr("readonly", true);

        $('#Capacity').attr("readonly", true);
        $('#Collateral').attr("readonly", true);
        $('#Capital').attr("readonly", true);
        $('#Condition').attr("readonly", true);
        $('#Character').attr("readonly", true);
        $('#advantage_notes').attr("readonly", true);
        $('#deficiency_notes').attr("readonly", true);

        if (Flag == "Approval") {
            var StatusApproval = $('#txhiddenStatusApproval').val();
            var IsApprover = $('#txhiddenIsApprover').val();
            var IsLastApprover = $('#txhiddenIsLastApprover').val();

            $('#btnApprove').hide(); //status jadi verified
            $('#btnReview').hide(); //status jadi correction
            $('#btnReject').hide(); //status jadi reject
            $('#btnVerify').hide(); //status jadi verified

            //RFA
            if (StatusApproval == "0" && IsApprover == "1" && IsLastApprover == "0") {
                $('#btnVerify').show(); //status jadi verified
                $('#btnReview').show(); //status jadi correction
                $('#btnReject').show(); //status jadi reject
            }

            //RFA & LastApprover
            if (StatusApproval == "0" && IsApprover == "1" && IsLastApprover == "1") {
                $('#btnApprove').show(); //status jadi verified
                $('#btnReview').show(); //status jadi correction
                $('#btnReject').show(); //status jadi reject
            }

            //Verified
            if (StatusApproval == "V" && IsApprover == "1" && IsLastApprover == "0") {
                $('#btnVerify').show(); //status jadi verified
                $('#btnReview').show(); //status jadi correction
                $('#btnReject').show(); //status jadi reject
            }

            //Verified & LastApprover
            if (StatusApproval == "V" && IsApprover == "1" && IsLastApprover == "1") {
                $('#btnApprove').show(); //status jadi verified
                $('#btnReview').show(); //status jadi correction
                $('#btnReject').show(); //status jadi reject
            }
        }
    }

    var params = {
        credit_id: CreditId
        , photo_type_id: '-'
        , photo_id: '-'
    }

    $.ajax({
        type: 'GET',
        url: '../CACar/Get_Tr_CM_photo_detail',
        data: params
    }).then(function (data) {
        var i = 0;
        let rowcontent = "";
        var IsShowHeader = "";
        var IsLast = "";

        var count = $("#txCountFile").val();
        var maxrowcol = (parseInt(count) / 2);

        $.each(data, function (key, value) {
            const myArray = value.credit_id.split("#");
            IsShowHeader = myArray[1];
            IsLast = myArray[2];

            if (Flag != "Approval" && Flag != "View") {
                if (i == 0 || i == maxrowcol) {
                    rowcontent += "<div class='col-lg-6 col-md-6 col-sm-12 col-xs-12' style='border-right: 2px solid #e8e8e8'>"
                }

                if (IsShowHeader == "1") {
                    rowcontent += "<h5><div style='margin-top:2%' class='bgc-small'>" + value.photo_type_desc + "</div></h5>"
                }

                rowcontent += "<div class='row'>"
                rowcontent += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>"
                rowcontent += "<p>" + value.photo_desc + "</p>"
                rowcontent += "<input type='hidden' id='photo_type_id_" + i + "' value='" + value.photo_type_id + "' />"
                rowcontent += "<input type='hidden' id='photo_id_" + i + "' value='" + value.photo_id + "' />"
                rowcontent += "<input type='hidden' id='is_new_zoom_" + i + "' value='" + value.is_new_zoom + "' />"
                rowcontent += "</div>"
                rowcontent += "</div>"
                rowcontent += "<div class='row'>"
                rowcontent += "<div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>"

                if (Flag == "Add") {
                    rowcontent += "<input class='form-control' type='file' id='fu_" + i + "' required>"
                }
                else {
                    if (value.filename == "") {
                        rowcontent += "<input class='form-control' type='file' id='fu_" + i + "' required>"
                    }
                    else {
                        rowcontent += "<input class='form-control' type='file' id='fu_" + i + "'>"
                    }
                }

                rowcontent += "</div>"
                rowcontent += "<div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>"
                if (Flag != "Add") {
                    if (value.filename != "") {
                        rowcontent += "<a class='btn-input ladda-button' target='_blank' href='DownloadFile?photo_type_id=" + value.photo_type_id + "&photo_id=" + value.photo_id + "&is_new_zoom=" + value.is_new_zoom + "'>Download</a>"
                    }
                }
                rowcontent += "</div>"
                rowcontent += "</div>"

                if (i == maxrowcol - 1 || i == count - 1) {
                    rowcontent += "</div>"
                }
            }
            else {
                if (i == 0 || i == maxrowcol) {
                    rowcontent += "<div class='col-lg-6 col-md-6 col-sm-12 col-xs-12' style='border-right: 2px solid #e8e8e8'>"
                }

                if (IsShowHeader == "1") {
                    rowcontent += "<h5><div style='margin-top:2%' class='bgc-small'>" + value.photo_type_desc + "</div></h5>"
                }

                rowcontent += "<div class='row'>"
                rowcontent += "<input type='hidden' id='photo_type_id_" + i + "' value='" + value.photo_type_id + "' />"
                rowcontent += "<input type='hidden' id='photo_id_" + i + "' value='" + value.photo_id + "' />"
                rowcontent += "<input type='hidden' id='is_new_zoom_" + i + "' value='" + value.is_new_zoom + "' />"
                rowcontent += "<div style='margin-top:3%' class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>"
                rowcontent += "<p>" + value.photo_desc + "</p>"
                rowcontent += "</div>"
                rowcontent += "<div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>"
                if (value.filename != "") {
                    rowcontent += "<a class='btn-input ladda-button' target='_blank' href='DownloadFile?photo_type_id=" + value.photo_type_id + "&photo_id=" + value.photo_id + "&is_new_zoom=" + value.is_new_zoom + "'>Download</a>"
                }
                rowcontent += "</div>"
                rowcontent += "</div>"

                if (i == maxrowcol - 1 || i == count - 1) {
                    rowcontent += "</div>"
                }
            }

            i = i + 1;
        });

        $("#divData").append(rowcontent);
    });
});

jQuery(document).ready(function () {
    cmphotodetail_load.init();

    //Setup responsiove UI on Mobile
    const pnlFotoID = document.getElementById("FotoID_more");
    pnlFotoID?.addEventListener('click', () => {
        cmphotodetail_load.changeClass_FotoID();
    });

    const pnlCMO = document.getElementById("CMO_more");
    pnlCMO?.addEventListener('click', () => {
        cmphotodetail_load.changeClass_CMO();
    });

    const pnlKeputusan = document.getElementById("Keputusan_more");
    pnlKeputusan?.addEventListener('click', () => {
        cmphotodetail_load.changeClass_Keputusan();
    });

    /* $("#btnViewScore").click(function () {
         myWindow = window.open("http://localhost:1000/CACar/ViewScore", "myWindow", "channelmode=yes,width=1200,height=1200,scrollbars=yes");
         //viewScore();
     });*/

    $("#btnSave").click(function () {
        Submit('D');
    });

    $("#btnRFA").click(function () {
        Submit('0');
    });

    $("#Keputusan").change(function () {
        $('#btnApprove').hide(); //status jadi verified
        $('#btnReview').hide(); //status jadi correction
        $('#btnReject').hide(); //status jadi reject
        $('#btnVerify').hide(); //status jadi verified

        var StatusApproval = $('#txhiddenStatusApproval').val();
        var IsApprover = $('#txhiddenIsApprover').val();
        var IsLastApprover = $('#txhiddenIsLastApprover').val();
        var reason_id = $('#ddlKeputusan').val();

        var params = {
            reason_id: reason_id
        }

        $.ajax({
            type: 'GET',
            url: '../CM/GetReasonType',
            data: params
        }).then(function (data) {
            $.each(data, function (key, value) {
                if (value.reason_type == "1") {
                    //RFA
                    if (StatusApproval == "0" && IsApprover == "1" && IsLastApprover == "0") {
                        $('#btnVerify').show(); //status jadi verified
                    }

                    //RFA & LastApprover
                    if (StatusApproval == "0" && IsApprover == "1" && IsLastApprover == "1") {
                        $('#btnApprove').show(); //status jadi verified
                    }

                    //Verified
                    if (StatusApproval == "V" && IsApprover == "1" && IsLastApprover == "0") {
                        $('#btnVerify').show(); //status jadi verified
                    }

                    //Verified & LastApprover
                    if (StatusApproval == "V" && IsApprover == "1" && IsLastApprover == "1") {
                        $('#btnApprove').show(); //status jadi verified
                    }
                }
                else if (value.reason_type == "2" || value.reason_type == "9") {
                    if (IsApprover == "1") {
                        $('#btnReject').show(); //status jadi reject
                    }
                }
                else if (value.reason_type == "3") {
                    if (IsApprover == "1") {
                        $('#btnReview').show(); //status jadi correction
                    }
                }
            });
        });
    });
});

//function viewScore() {
//    var popup = window.open("", "", "channelmode=yes,width=1200,height=1200,scrollbars=yes");
//    var html = '<%=htmlresponse%>';
//    popup.document.write(html);
//}

var cmphotodetail_load = {
    init: function () {
        this.TogglePanel();
    },
    changeClass_FotoID: function () {
        var content = document.getElementById("FotoID");

        var FotoID = document.getElementById("FotoID_more");
        content.classList.toggle('show');

        if (content.classList.contains("FotoIDShow")) {
            FotoID.innerHTML = "Foto ID";
        } else {
            FotoID.innerHTML = "Foto ID";
        }
    },
    changeClass_CMO: function () {
        var content = document.getElementById("CMO");

        var CMO = document.getElementById("CMO_more");
        content.classList.toggle('show');

        if (content.classList.contains("CMOShow")) {
            CMO.innerHTML = "Data CMO & Dealer";
        } else {
            CMO.innerHTML = "Data CMO & Dealer";
        }
    },
    changeClass_Keputusan: function () {
        var content = document.getElementById("Keputusan");

        var Keputusan = document.getElementById("Keputusan_more");
        content.classList.toggle('show');

        if (content.classList.contains("KeputusanShow")) {
            Keputusan.innerHTML = "Keputusan";
        } else {
            Keputusan.innerHTML = "Keputusan";
        }
    },
    TogglePanel: function () {
        $("btnFotoID").click(function () {
            $("FotoID").toggle();
        });

        $("btnCMO").click(function () {
            $("CMO").toggle();
        });

        $("btnKeputusan").click(function () {
            $("Keputusan").toggle();
        });
    }
}

function SavePhotoDetail(photo_type_id, photo_id, Seq, flagAction) {
    var dataX = new FormData();

    //if (Seq != "") {
    var files = document.getElementById('fu_' + Seq).files;
    var filename = "";

    if (files.length > 0) {
        for (var x = 0; x < files.length; x++) {
            dataX.append("photo_type_id", photo_type_id);
            dataX.append("fileUpload", files[x]);
            dataX.append("photo_id", photo_id);
            dataX.append("flagAction", flagAction);

            $.ajax({
                type: "POST",
                url: '../CACar/SaveCMMotor_PhotoDetail',
                contentType: false,
                processData: false,
                data: dataX,
                success: function (response) {
                    if (response.success) {
                        console.log(response.responseText);
                    }
                }
            });
        }
    }
    //}
    //else {
    //    dataX.append("photo_type_id", "");
    //    dataX.append("fileUpload", "");
    //    dataX.append("photo_id", "");
    //    dataX.append("flagAction", flagAction);

    //    $.ajax({
    //        type: "POST",
    //        url: '../CACar/SaveCMMotor_PhotoDetail',
    //        contentType: false,
    //        processData: false,
    //        data: dataX,
    //        success: function (response) {
    //            if (response.success) {
    //                console.log(response.responseText);
    //            }
    //        }
    //    });
    //}
}

function Submit(flagAction) {
    // save cmo analisa

    var Capacity = $("#Capacity").val();
    var Capital = $("#Capital").val();
    var Character = $("#Character").val();
    var Condition = $("#Condition").val();
    var Collateral = $("#Collateral").val();
    var advantage_notes = $("#advantage_notes").val();
    var deficiency_notes = $("#deficiency_notes").val();

    var dataX = new FormData();
    dataX.append("Capacity", Capacity);
    dataX.append("Capital", Capital);
    dataX.append("Character", Character);
    dataX.append("Condition", Condition);
    dataX.append("Collateral", Collateral);
    dataX.append("advantage_notes", advantage_notes);
    dataX.append("deficiency_notes", deficiency_notes);
    dataX.append("flagAction", flagAction);

    $.ajax({
        type: "POST",
        url: '../CACar/SaveCMOAnalisa',
        contentType: false,
        processData: false,
        data: dataX,
        success: function (response) {
            if (response.success) {
                console.log(response.responseText);
            }
        }
    });

    //save dokumen foto
    var IsLast = "";

    var CountFile = $("#txCountFile").val();
    var i = 0;

    while (i < CountFile) {
        var photo_type_id = $("#photo_type_id_" + i).val();
        var photo_id = $("#photo_id_" + i).val();
        var filename = $("#fu_" + i).val();

        if (i == CountFile - 1) {
            IsLast = "Last";
        }

        if ($("#txhiddenFlag").val() == "Edit" || $("#txhiddenFlag").val() == "Correction") {
            if (filename != "") {
                SavePhotoDetail(photo_type_id, photo_id, i, IsLast);
            }
            //else {
            //    SavePhotoDetail("", "", "", IsLast);
            //}
        }
        else {
            SavePhotoDetail(photo_type_id, photo_id, i, IsLast);
        }

        i = i + 1;
    }
}