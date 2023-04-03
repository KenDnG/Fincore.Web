$(document).ready(function () {
    var Flag = $("#txhiddenFlag").val();
    var CreditId = $("#txhiddenCreditId").val();
    var IsCreditAnalyst = $("#txhiddenIsCreditAnalyst").val();
    var CountSkalaResiko = $("#txCountSkalaResiko").val();

    document.getElementById("divAO").style.display = "none";
    document.getElementById("divSkalaResiko").style.display = "none";

    $("#txtanalisa_bike").removeAttr("required");
    $("#Kesimpulan").removeAttr("required");

    if (Flag == "Approval") {
        if (IsCreditAnalyst == "1") {
            document.getElementById("divAO").style.display = "block";
            $("#txtanalisa_bike").attr("required", true);
            $("#Kesimpulan").attr("required", true);
        }

        if (parseFloat(CountSkalaResiko) > 0) {
            document.getElementById("divSkalaResiko").style.display = "block";
        }

        $("#btnApprove").hide(); //status jadi verified
        $("#btnReview").hide(); //status jadi correction
        $("#btnReject").hide(); //status jadi reject
        $("#btnVerify").hide(); //status jadi verified

        var StatusApproval = $("#txhiddenStatusApproval").val();
        var IsApprover = $("#txhiddenIsApprover").val();
        var IsLastApprover = $("#txhiddenIsLastApprover").val();

        //RFA
        if (StatusApproval == "0" && IsApprover == "1" && IsLastApprover == "0") {
            $("#btnVerify").show(); //status jadi verified
            $("#btnReview").show(); //status jadi correction
            $("#btnReject").show(); //status jadi reject
        }

        //RFA & LastApprover
        if (StatusApproval == "0" && IsApprover == "1" && IsLastApprover == "1") {
            $("#btnApprove").show(); //status jadi verified
            $("#btnReview").show(); //status jadi correction
            $("#btnReject").show(); //status jadi reject
        }

        //Verified
        if (StatusApproval == "V" && IsApprover == "1" && IsLastApprover == "0") {
            $("#btnVerify").show(); //status jadi verified
            $("#btnReview").show(); //status jadi correction
            $("#btnReject").show(); //status jadi reject
        }

        //Verified & LastApprover
        if (StatusApproval == "V" && IsApprover == "1" && IsLastApprover == "1") {
            $("#btnApprove").show(); //status jadi verified
            $("#btnReview").show(); //status jadi correction
            $("#btnReject").show(); //status jadi reject
        }
    }

    var params = {
        credit_id: CreditId,
        photo_type_id: "-",
        photo_id: "-",
        is_new_zoom: "-",
    };

    $.ajax({
        type: "GET",
        url: "../CM/Get_Tr_CM_photo_detail",
        data: params,
    }).then(function (data) {
        var i = 0;
        let rowcontent = "";
        var IsShowHeader = "";
        var IsLast = "";

        var count = $("#txCountFile").val();
        var maxrowcol = parseInt(count) / 2;

        $.each(data, function (key, value) {
            const myArray = value.credit_id.split("#");
            IsShowHeader = myArray[1];
            IsLast = myArray[2];

            if (Flag != "Approval" && Flag != "View") {
                if (i == 0 || i == maxrowcol) {
                    rowcontent +=
                        "<div class='col-lg-6 col-md-6 col-sm-12 col-xs-12' style='border-right: 2px solid #e8e8e8'>";
                }

                if (IsShowHeader == "1") {
                    rowcontent +=
                        "<h5><div style='margin-top:2%' class='bgc-small'>" +
                        value.photo_type_desc +
                        "</div></h5>";
                }

                rowcontent += "<div class='row'>";
                rowcontent += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>";
                rowcontent += "<p>" + value.photo_desc + "</p>";
                rowcontent +=
                    "<input type='hidden' id='photo_type_id_" +
                    i +
                    "' value='" +
                    value.photo_type_id +
                    "' />";
                rowcontent +=
                    "<input type='hidden' id='photo_id_" +
                    i +
                    "' value='" +
                    value.photo_id +
                    "' />";
                rowcontent +=
                    "<input type='hidden' id='is_new_zoom_" +
                    i +
                    "' value='" +
                    value.is_new_zoom +
                    "' />";
                rowcontent += "</div>";
                rowcontent += "</div>";
                rowcontent += "<div class='row'>";
                rowcontent += "<div class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>";

                if (Flag == "Add") {
                    rowcontent +=
                        "<input class='form-control' type='file' id='fu_" +
                        i +
                        "' required>";
                } else {
                    if (value.filename == "") {
                        rowcontent +=
                            "<input class='form-control' type='file' id='fu_" +
                            i +
                            "' required>";
                    } else {
                        rowcontent +=
                            "<input class='form-control' type='file' id='fu_" + i + "'>";
                    }
                }

                rowcontent += "</div>";
                rowcontent += "<div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>";
                if (Flag != "Add") {
                    if (value.filename != "") {
                        rowcontent +=
                            "<a class='btn-input ladda-button' target='_blank' href='DownloadFile?photo_type_id=" +
                            value.photo_type_id +
                            "&photo_id=" +
                            value.photo_id +
                            "&is_new_zoom=" +
                            value.is_new_zoom +
                            "'>Download</a>";
                    }
                }
                rowcontent += "</div>";
                rowcontent += "</div>";

                if (i == maxrowcol - 1 || i == count - 1) {
                    rowcontent += "</div>";
                }
            } else {
                if (i == 0 || i == maxrowcol) {
                    rowcontent +=
                        "<div class='col-lg-6 col-md-6 col-sm-12 col-xs-12' style='border-right: 2px solid #e8e8e8'>";
                }

                if (IsShowHeader == "1") {
                    rowcontent +=
                        "<h5><div style='margin-top:2%' class='bgc-small'>" +
                        value.photo_type_desc +
                        "</div></h5>";
                }

                rowcontent += "<div class='row'>";
                rowcontent +=
                    "<input type='hidden' id='photo_type_id_" +
                    i +
                    "' value='" +
                    value.photo_type_id +
                    "' />";
                rowcontent +=
                    "<input type='hidden' id='photo_id_" +
                    i +
                    "' value='" +
                    value.photo_id +
                    "' />";
                rowcontent +=
                    "<input type='hidden' id='is_new_zoom_" +
                    i +
                    "' value='" +
                    value.is_new_zoom +
                    "' />";
                rowcontent +=
                    "<div style='margin-top:3%' class='col-lg-8 col-md-8 col-sm-8 col-xs-8'>";
                rowcontent += "<p>" + value.photo_desc + "</p>";
                rowcontent += "</div>";
                rowcontent += "<div class='col-lg-4 col-md-4 col-sm-4 col-xs-4'>";
                if (value.filename != "") {
                    rowcontent +=
                        "<a class='btn-input ladda-button' target='_blank' href='DownloadFile?photo_type_id=" +
                        value.photo_type_id +
                        "&photo_id=" +
                        value.photo_id +
                        "&is_new_zoom=" +
                        value.is_new_zoom +
                        "'>Download</a>";
                }
                rowcontent += "</div>";
                rowcontent += "</div>";

                if (i == maxrowcol - 1 || i == count - 1) {
                    rowcontent += "</div>";
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
    pnlFotoID?.addEventListener("click", () => {
        cmphotodetail_load.changeClass_FotoID();
    });

    const pnlKeputusan = document.getElementById("Keputusan_more");
    pnlKeputusan?.addEventListener("click", () => {
        cmphotodetail_load.changeClass_Keputusan();
    });

    const pnlAnalisa = document.getElementById("Analisa_more");
    pnlAnalisa?.addEventListener("click", () => {
        cmphotodetail_load.changeClass_Analisa();
    });

    const pnlSkalaResiko = document.getElementById("SkalaResiko_more");
    pnlSkalaResiko?.addEventListener("click", () => {
        cmphotodetail_load.changeClass_SkalaResiko();
    });

    $("#btnApprove").click(function () {
        //if ($("#ddlKeputusan").val() == "") {
        //    $("#txhiddenResult").val("Failed");
        //    $("#txhiddenResultMessage").val("Keputusan harus dipilih");
        //    Common.Alert.Error("Keputusan harus dipilih");
        //}
        //else {
        //    if ($("#Alasan").val() == "") {
        //        $("#txhiddenResult").val("Failed");
        //        $("#txhiddenResultMessage").val("Alasan harus diisi");
        //        Common.Alert.Error("Alasan harus diisi");
        //    }
        //    else {
        //        ProcessApproval("A");
        //    }
        //}

        ProcessApproval("A");
    });

    $("#btnVerify").click(function () {
        ProcessApproval("V");
    });

    $("#btnReview").click(function () {
        ProcessApproval("C");
    });

    $("#btnSave").click(function () {
        var Jumlah_Pembiayaan = $("#txhiddenJumlah_Pembiayaan")
            .val()
            .replaceAll(".", "");
        var NPWP = $("#txhiddenNPWP").val();

        if (parseFloat(Jumlah_Pembiayaan) >= 50000000 && NPWP == "") {
            $("#txhiddenResult").val("Failed");
            $("#txhiddenResultMessage").val("No NPWP harus diisi");
        } else {
            //if (NPWP.length != 15) {
            //  $("#txhiddenResult").val("Failed");
            //  $("#txhiddenResultMessage").val("No NPWP harus 15 digit");
            //} else {
            Submit("D");
            //}
        }
    });

    $("#btnRFA").click(function () {
        var Jumlah_Pembiayaan = $("#txhiddenJumlah_Pembiayaan")
            .val()
            .replaceAll(".", "");
        var NPWP = $("#txhiddenNPWP").val();

        if (parseFloat(Jumlah_Pembiayaan) >= 50000000 && NPWP == "") {
            $("#txhiddenResult").val("Failed");
            $("#txhiddenResultMessage").val("No NPWP harus diisi");
        } else {
            //if (NPWP.length != 15) {
            //  $("#txhiddenResult").val("Failed");
            //  $("#txhiddenResultMessage").val("No NPWP harus 15 digit");
            //} else {
            Submit("0");
            //}
        }
    });

    $("#Keputusan").change(function () {
        $("#btnApprove").hide(); //status jadi verified
        $("#btnReview").hide(); //status jadi correction
        $("#btnReject").hide(); //status jadi reject
        $("#btnVerify").hide(); //status jadi verified

        var StatusApproval = $("#txhiddenStatusApproval").val();
        var IsApprover = $("#txhiddenIsApprover").val();
        var IsLastApprover = $("#txhiddenIsLastApprover").val();
        var reason_id = $("#ddlKeputusan").val();

        var params = {
            reason_id: reason_id,
        };

        $.ajax({
            type: "GET",
            url: "../CM/GetReasonType",
            data: params,
        }).then(function (data) {
            $.each(data, function (key, value) {
                if (value.reason_type == "1" || value.reason_type == "9") {
                    //RFA
                    if (
                        StatusApproval == "0" &&
                        IsApprover == "1" &&
                        IsLastApprover == "0"
                    ) {
                        $("#btnVerify").show(); //status jadi verified
                    }

                    //RFA & LastApprover
                    if (
                        StatusApproval == "0" &&
                        IsApprover == "1" &&
                        IsLastApprover == "1"
                    ) {
                        $("#btnApprove").show(); //status jadi verified
                    }

                    //Verified
                    if (
                        StatusApproval == "V" &&
                        IsApprover == "1" &&
                        IsLastApprover == "0"
                    ) {
                        $("#btnVerify").show(); //status jadi verified
                    }

                    //Verified & LastApprover
                    if (
                        StatusApproval == "V" &&
                        IsApprover == "1" &&
                        IsLastApprover == "1"
                    ) {
                        $("#btnApprove").show(); //status jadi verified
                    }
                } else if (value.reason_type == "2") {
                    if (IsApprover == "1") {
                        $("#btnReject").show(); //status jadi reject
                    }
                } else if (value.reason_type == "3") {
                    if (IsApprover == "1") {
                        $("#btnReview").show(); //status jadi correction
                    }
                }
            });
        });
    });
});

var cmphotodetail_load = {
    init: function () {
        this.TogglePanel();
    },
    changeClass_FotoID: function () {
        var content = document.getElementById("FotoID");

        var FotoID = document.getElementById("FotoID_more");
        content.classList.toggle("show");

        if (content.classList.contains("FotoIDShow")) {
            FotoID.innerHTML = "Foto ID";
        } else {
            FotoID.innerHTML = "Foto ID";
        }
    },
    changeClass_Keputusan: function () {
        var content = document.getElementById("Keputusan");

        var Keputusan = document.getElementById("Keputusan_more");
        content.classList.toggle("show");

        if (content.classList.contains("KeputusanShow")) {
            Keputusan.innerHTML = "Keputusan";
        } else {
            Keputusan.innerHTML = "Keputusan";
        }
    },
    changeClass_Analisa: function () {
        var content = document.getElementById("Analisa");

        var analisa = document.getElementById("Analisa_more");
        content.classList.toggle("show");

        if (content.classList.contains("Show")) {
            analisa.innerHTML = "Analisa & Kesimpulan (AO)";
        } else {
            analisa.innerHTML = "Analisa & Kesimpulan (AO)";
        }
    },
    changeClass_SkalaResiko: function () {
        var content = document.getElementById("SkalaResiko");

        var SkalaResiko = document.getElementById("SkalaResiko_more");
        content.classList.toggle("show");

        if (content.classList.contains("Show")) {
            SkalaResiko.innerHTML = "Rekap Skala Kemungkinan Resiko";
        } else {
            SkalaResiko.innerHTML = "Rekap Skala Kemungkinan Resiko";
        }
    },
    TogglePanel: function () {
        $("btnFotoID").click(function () {
            $("FotoID").toggle();
        });

        $("btnKeputusan").click(function () {
            $("Keputusan").toggle();
        });

        $("btnAnalisa").click(function () {
            $("Analisa").toggle();
        });

        $("btnSkalaResiko").click(function () {
            $("SkalaResiko").toggle();
        });
    },
};

function SavePhotoDetail(photo_type_id, photo_id, Seq, flagAction) {
    var files = document.getElementById("fu_" + Seq).files;
    var filename = "";

    if (files.length > 0) {
        var dataX = new FormData();

        for (var x = 0; x < files.length; x++) {
            dataX.append("photo_type_id", photo_type_id);
            dataX.append("fileUpload", files[x]);
            dataX.append("photo_id", photo_id);
            dataX.append("flagAction", flagAction);
        }

        $.ajax({
            type: "POST",
            url: "../CM/SaveCMMotor_PhotoDetail",
            contentType: false,
            processData: false,
            data: dataX,
            success: function (response) {
                if (response.success) {
                    console.log(response.responseText);
                }
            },
        });
    }
}

function Submit(flagAction) {
    var CountFile = $("#txCountFile").val();
    var i = 0;

    while (i < CountFile) {
        var photo_type_id = $("#photo_type_id_" + i).val();
        var photo_id = $("#photo_id_" + i).val();
        var filename = $("#fu_" + i).val();

        if (
            $("#txhiddenFlag").val() == "Edit" ||
            $("#txhiddenFlag").val() == "Correction"
        ) {
            if (filename != "") {
                SavePhotoDetail(photo_type_id, photo_id, i, flagAction);
            }
        } else {
            SavePhotoDetail(photo_type_id, photo_id, i, flagAction);
        }

        i = i + 1;
    }

    $("#txhiddenResult").val("Success");
    $("#txhiddenResultMessage").val("");
}

function ProcessApproval(StatusApproval) {
    var CountSkalaResiko = $("#txCountSkalaResiko").val();
    var i = 1;

    let RiskScaleFields = [];

    while (i <= CountSkalaResiko) {
        var SkalaResikoId = $("#hdnSkalaResikoId" + i).val();
        var SkalaResikoIdValue = $("#ddlSkalaResikoValueId" + i).val();

        var reason_id = $("#ddlKeputusan").val();
        var reason = $("#Alasan").val();
        var analysis = $("#txtanalisa_bike").val();
        var conclusion = $("#Kesimpulan").val();

        let dataX = {
            status_approval: StatusApproval,
            risk_scale_id: SkalaResikoId,
            risk_scale_value: SkalaResikoIdValue,
            reason_id: reason_id,
            reason: reason,
            analysis: analysis,
            conclusion: conclusion,
        };

        RiskScaleFields.push(dataX);

        i = i + 1;
    }

    $.ajax({
        url: "../CM/ProcessApproval",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(RiskScaleFields),
        success: function (data) {
            if (data.success) {
                window.location.href = data.action;
            } else {
                Common.Alert.Error(response.messages);
            }
        },
        error: function (errMsg) {
            Common.Alert.Warning(errMsg);
        },
    });
}