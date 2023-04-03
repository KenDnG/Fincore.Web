//SHOW HIDE WEB VIEW
$(document).ready(function () {
  $("btnNPPADD").click(function () {
    $("NPPADD").toggle();
  });

  $("btnAddData").click(function () {
    $("AddData").toggle();
  });

  $("btnDataPersonal").click(function () {
    $("DataPersonal").toggle();
  });

  $("btnDataPersonalRate").click(function () {
    $("DataPersonalRate").toggle();
  });

  $("btnDataItem").click(function () {
    $("DataItem").toggle();
  });

  $("btnDataPenagihan").click(function () {
    $("DataPenagihan").toggle();
  });

  $("btnDataPencairan").click(function () {
    $("DataPencairan").toggle();
  });

  $("btnCMPO").click(function () {
    $("CMPO").toggle();
  });

  $("btnMetodePembayaran").click(function () {
    $("MetodePembayaran").toggle();
  });

  $("btnMetodePembayaranInsentif").click(function () {
    $("MetodePembayaranInsentif").toggle();
  });

  //$("#btnProcessCreditId").click(function () {
  //    cmProcess.handle_btn('btnProcessCreditId');
  //});

  $("#btnCheckRapindo").click(function () {
    Rapindo.handle_btn("btnCheckRapindo");
  });

  $("#chasisNo").blur(function () {
    fnValidationChasisNo(this);
  });

  $("#billReceivedDate").change(function () {
    fnValidationBillReceivedDate(this);
  });

  $("#installmentDate").change(function () {
    fnValidationInstallmentDate(this);
  });

  //$("#btnApprove").click(function () {
  //    var tacWrapper = $("#WrapperOrderSource_TAC").children().length;

  //    console.log("" + tacWrapper)
  //});

  //$("#btnRFA_Add").click(function () {
  //    console.log("Test");
  //});

  $("#btnSaveDraft").click(function () {});
});

var NPP_CRUD = {
  RFA: function () {
    var flag = $("#hdnFlag").val();
    console.log("sdf");
    switch (flag) {
      case "addMotorcycle":
        npp.addDataMotorCyle("btnRFA_Add");
        break;
      case "addCar":
        var tacMax = $("#OS_TAC_Amount").val();
        var tpMax = $("#OS_TP_Amount").val();

        var totTacBeforeTax = $("#TotalTACAmountBeforeTax").val();
        var totTacAfterTax = $("#TotalTACAmountAfterTax").val();

        var totTPBeforeTax = $("#TotalTPAmountBeforeTax").val();
        var totTPAfterTax = $("#TotalTPAmountAfterTax").val();

        if (parseFloat(totTacBeforeTax) > parseFloat(tacMax)) {
          Common.Alert.Warning(
            "Total Sumber Order TAC Tidak Boleh Lebih Dari Maksimal Nominal Pencairan TAC\n" +
              "Total Sumber Order Pihak Ke-3 Harus Sama Dengan Maskimal Nominal Pencairan Pihak Ke-3"
          );
        } else if (parseFloat(totTPBeforeTax) != parseFloat(tpMax)) {
          Common.Alert.Warning(
            "Total Sumber Order TAC Tidak Boleh Lebih Dari Maksimal Nominal Pencairan TAC\n" +
              "Total Sumber Order Pihak Ke-3 Harus Sama Dengan Maskimal Nominal Pencairan Pihak Ke-3"
          );
        } else if (
          parseFloat(totTacBeforeTax) > parseFloat(tacMax) &&
          parseFloat(totTPBeforeTax) != parseFloat(tpMax)
        ) {
          Common.Alert.Warning(
            "Total Sumber Order TAC Tidak Boleh Lebih Dari Maksimal Nominal Pencairan TAC\n" +
              "Total Sumber Order Pihak Ke-3 Harus Sama Dengan Maskimal Nominal Pencairan Pihak Ke-3"
          );
        } else {
          npp.addDataCar("btnRFA_Add");
        }

        break;
      case "editMotorcycle":
        npp.editDataMotorCyle("btnRFA_Edit");
        break;
      case "editCar":
        var tacMax = $("#OS_TAC_Amount").val();
        var tpMax = $("#OS_TP_Amount").val();

        var totTacBeforeTax = $("#TotalTACAmountBeforeTax").val();
        var totTacAfterTax = $("#TotalTACAmountAfterTax").val();

        var totTPBeforeTax = $("#TotalTPAmountBeforeTax").val();
        var totTPAfterTax = $("#TotalTPAmountAfterTax").val();

        if (parseFloat(totTacBeforeTax) > parseFloat(tacMax)) {
          Common.Alert.Warning(
            "Total Sumber Order TAC Tidak Boleh Lebih Dari Maksimal Nominal Pencairan TAC\n" +
              "Total Sumber Order Pihak Ke-3 Harus Sama Dengan Maskimal Nominal Pencairan Pihak Ke-3"
          );
        } else if (parseFloat(totTPBeforeTax) != parseFloat(tpMax)) {
          Common.Alert.Warning(
            "Total Sumber Order TAC Tidak Boleh Lebih Dari Maksimal Nominal Pencairan TAC\n" +
              "Total Sumber Order Pihak Ke-3 Harus Sama Dengan Maskimal Nominal Pencairan Pihak Ke-3"
          );
        } else if (
          parseFloat(totTacBeforeTax) > parseFloat(tacMax) &&
          parseFloat(totTPBeforeTax) != parseFloat(tpMax)
        ) {
          Common.Alert.Warning(
            "Total Sumber Order TAC Tidak Boleh Lebih Dari Maksimal Nominal Pencairan TAC\n" +
              "Total Sumber Order Pihak Ke-3 Harus Sama Dengan Maskimal Nominal Pencairan Pihak Ke-3"
          );
        } else {
          npp.editDataCar("btnRFA_Edit");
        }

        break;
      default:
        Common.Alert.Warning(
          "Sistem tidak menemukan secara spesifik aksi apa yg anda lakukan, Harap hubungi IT"
        );
        break;
    }
  },
};

var npp_paging = {
  handle_btn: function (pageIndex, searchTerm) {
    var params = {
      pageIndex: pageIndex,
      searchTerm: searchTerm,
    };
    $.ajax({
      url: "Index",
      type: "POST",
      data: params,
      success: function (data) {
        $("#containerDealerBank").html(data);
      },
      error: function (error) {
        var d = error;
      },
    });
  },
};

var npp = {
  addDataMotorCyle: function (elmn) {
    //var ldBtn = Ladda.create(document.getElementById(elmn));
    var dataForm = $("#formAddNPP").serialize();

    console.log("In AddDataMotor before ajax");
    $.ajax({
      type: "POST",
      url: "InsertNPPMotorcycle",
      contentType: "application/x-www-form-urlencoded; charset=utf-8",
      data: dataForm,
      beforeSend: function () {
        //ldBtn.start();
        $("#i" + elmn).show();
        $("#" + elmn + "").prop("disabled", true);
      },
      success: function (data) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        if (data.success) {
          Common.Alert.SuccessThenRoute(data.messages, data.action);
        } else {
          Common.Alert.Error(data.messages);
        }
      },
      error: function (error) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        Common.Alert.Error(error);
      },
    });
  },
  addDataCar: function (elmn) {
    //var ldBtn = Ladda.create(document.getElementById(elmn));
    //var dataForm = $("#formAddNPP").serialize();

    let dataNpp = {
      CreditId: $("#creditId").val(),
      strBillReceivedDate: $("#strBillReceivedDate").val(),
      strBillReceiptDate: $("#strBillReceiptDate").val(),
      DownPaymentReceiptNo: $("#downPaymentReceiptNo").val(),
      strDownPaymentReceiptDate: $("#strDownPaymentReceiptDate").val(),
      ReceiptNo: $("#receiptNo").val(),
      strReceiptDate: $("#strReceiptDate").val(),
      BpkbLetterNo: $("#bpkbLetterNo").val(),
      strBpkbLetterDate: $("#strBpkbLetterDate").val(),
      BastNo: $("#bastNo").val(),
      strBastDate: $("#strBastDate").val(),
      strInstallmentDate: $("#strInstallmentDate").val(),
      BankReferenceId: $("#hdnBankReferenceId").val(),
      NfaPercent: $("#hdnNFAPercent").val(),
      OtherApValue: $("#hdnOtherAPValue").val(),
      AgreementValue: $("#hdnAgreementValue").val(),
    };

    let dataItem = {
      CreditId: $("#creditId").val(),
      ChasisNo: $("#chasisNo").val(),
      MachineNo: $("#machineNo").val(),
      ItemColor: $("#itemColor").val(),
    };

    var TACDataCount = $("#WrapperOrderSource_TAC").children().length;
    var TPDataCount = $("#WrapperOrderSource_TP").children().length;

    let dataOSTAC = [];
    let dataOSTP = [];

    //Order Source TAC Collect Data
    for (let i = 0; i < TACDataCount; i++) {
      let tac = {
        JobTitleId: $("#ddwnJobTitleId_TAC_" + i).val(),
        PersonelId: $("#ddwnPersonnelId_TAC_" + i).val(),
        PersonelName: "Andrean TAC " + i,
        RateTacRefund: 2,
        AmountTacRefund: $("#txtAmount_TAC_" + i).val(),
        AmountTacRefundAfterTax: $("#txtAmountAfterTax_TAC_" + i).val(),
        BankAccountNumber: "9" + i + "9" + i + "9" + i + "9" + i,
        BankAccountName: "Atas Nama TAC " + i,
      };

      dataOSTAC.push(tac);
    }

    //Order Source TP Collect Data
    for (let i = 0; i < TPDataCount; i++) {
      let tp = {
        JobTitleId: $("#ddwnJobTitleId_TP_" + i).val(),
        PersonelId: $("#ddwnPersonnelId_TP_" + i).val(),
        PersonelName: "Andrean TP " + i,
        RateProvisiRefund: 2,
        AmountProvisiRefund: $("#txtAmount_TP_" + i).val(),
        AmountProvisiRefundAfterTax: $("#txtAmountAfterTax_TP_" + i).val(),
        BankAccountNumber: "8" + i + "8" + i + "8" + i + "8" + i,
        BankAccountName: "Atas Nama TP " + i,
      };

      dataOSTP.push(tp);
    }

    $.ajax({
      type: "POST",
      url: "InsertNPPCar",
      //contentType: "application/json;charset=utf-8",
      //data: JSON.stringify({
      //    npp: dataNpp,
      //    item: dataItem,
      //    osTAC: dataOSTAC,
      //    osTP: dataOSTP
      //}),
      //dataType: "json",
      //contentType: "application/x-www-form-urlencoded; charset=utf-8",
      data: {
        npp: dataNpp,
        item: dataItem,
        osTAC: dataOSTAC,
        osTP: dataOSTP,
      },
      beforeSend: function () {
        //ldBtn.start();
        $("#i" + elmn).show();
        $("#" + elmn + "").prop("disabled", true);
      },
      success: function (data) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        if (data.success) {
          Common.Alert.SuccessThenRoute(data.messages, data.action);
        }
      },
      error: function (error) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        Common.Alert.Error(error);
      },
    });
  },
  editDataMotorCyle: function (elmn) {
    //var ldBtn = Ladda.create(document.getElementById(elmn));
    var dataForm = $("#formAddNPP").serialize();

    $.ajax({
      type: "POST",
      url: "UpdateNPPMotorcycle",
      contentType: "application/x-www-form-urlencoded; charset=utf-8",
      data: dataForm,
      beforeSend: function () {
        //ldBtn.start();
        $("#i" + elmn).show();
        $("#" + elmn + "").prop("disabled", true);
      },
      success: function (data) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        if (data.success) {
          Common.Alert.SuccessThenRoute(data.messages, data.action);
        } else {
          Common.Alert.Error(data.messages);
        }
      },
      error: function (error) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        Common.Alert.Error(error);
      },
    });
  },
  editDataCar: function (elmn) {
    //var ldBtn = Ladda.create(document.getElementById(elmn));
    //var dataForm = $("#formAddNPP").serialize();

    let dataNpp = {
      CreditId: $("#creditId").val(),
      strBillReceivedDate: $("#strBillReceivedDate").val(),
      strBillReceiptDate: $("#strBillReceiptDate").val(),
      DownPaymentReceiptNo: $("#downPaymentReceiptNo").val(),
      strDownPaymentReceiptDate: $("#strDownPaymentReceiptDate").val(),
      ReceiptNo: $("#receiptNo").val(),
      strReceiptDate: $("#strReceiptDate").val(),
      BpkbLetterNo: $("#bpkbLetterNo").val(),
      strBpkbLetterDate: $("#strBpkbLetterDate").val(),
      BastNo: $("#bastNo").val(),
      strBastDate: $("#strBastDate").val(),
      strInstallmentDate: $("#strInstallmentDate").val(),
      BankReferenceId: $("#hdnBankReferenceId").val(),
      NfaPercent: $("#hdnNFAPercent").val(),
      OtherApValue: $("#hdnOtherAPValue").val(),
      AgreementValue: $("#hdnAgreementValue").val(),
      ApplicationTypeId: $("#hdnApplicationTypeId").val(),
    };

    let dataItem = {
      CreditId: $("#creditId").val(),
      ChasisNo: $("#chasisNo").val(),
      MachineNo: $("#machineNo").val(),
      ItemColor: $("#itemColor").val(),
    };

    var TACDataCount = $("#WrapperOrderSource_TAC").children().length;
    var TPDataCount = $("#WrapperOrderSource_TP").children().length;

    let dataOSTAC = [];
    let dataOSTP = [];

    //Order Source TAC Collect Data
    for (let i = 0; i < TACDataCount; i++) {
      let tac = {
        JobTitleId: $("#ddwnJobTitleId_TAC_" + i).val(),
        PersonelId: $("#ddwnPersonnelId_TAC_" + i).val(),
        PersonelName: "Andrean TAC " + i,
        RateTacRefund: 2,
        AmountTacRefund: $("#txtAmount_TAC_" + i).val(),
        AmountTacRefundAfterTax: $("#txtAmountAfterTax_TAC_" + i).val(),
        BankAccountNumber: "9" + i + "9" + i + "9" + i + "9" + i,
        BankAccountName: "Atas Nama TAC " + i,
      };

      dataOSTAC.push(tac);
    }

    //Order Source TP Collect Data
    for (let i = 0; i < TPDataCount; i++) {
      let tp = {
        JobTitleId: $("#ddwnJobTitleId_TP_" + i).val(),
        PersonelId: $("#ddwnPersonnelId_TP_" + i).val(),
        PersonelName: "Andrean TP " + i,
        RateProvisiRefund: 2,
        AmountProvisiRefund: $("#txtAmount_TP_" + i).val(),
        AmountProvisiRefundAfterTax: $("#txtAmountAfterTax_TP_" + i).val(),
        BankAccountNumber: "8" + i + "8" + i + "8" + i + "8" + i,
        BankAccountName: "Atas Nama TP " + i,
      };

      dataOSTP.push(tp);
    }

    $.ajax({
      type: "POST",
      url: "UpdateNPPCar",
      //contentType: "application/json;charset=utf-8",
      //data: JSON.stringify({
      //    npp: dataNpp,
      //    item: dataItem,
      //    osTAC: dataOSTAC,
      //    osTP: dataOSTP
      //}),
      //dataType: "json",
      //contentType: "application/x-www-form-urlencoded; charset=utf-8",
      data: {
        npp: dataNpp,
        item: dataItem,
        osTAC: dataOSTAC,
        osTP: dataOSTP,
      },
      beforeSend: function () {
        //ldBtn.start();
        $("#i" + elmn).show();
        $("#" + elmn + "").prop("disabled", true);
      },
      success: function (data) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        if (data.success) {
          Common.Alert.SuccessThenRoute(data.messages, data.action);
        }
      },
      error: function (error) {
        //ldBtn.stop();
        $("#i" + elmn).hide();
        $("#" + elmn + "").prop("disabled", false);
        Common.Alert.Error(error);
      },
    });
  },
};

//var cmProcess = {
//    handle_btn: function (elmn) {
//        var creditId = $('#creditId').val();
//        //var ldBtn = Ladda.create(document.getElementById(elmn));
//        var carForm = document.getElementById('CarForm');
//        var btnRapindo = document.getElementById('btnCheckRapindo');
//        var disbursalTypeForm = document.getElementById('DisbursalTypeForm');
//        var topupNomField = document.getElementById('TopupAmountField');

//        $.ajax({
//            url: 'GetDataCMByCreditId?creditId=' + creditId,
//            type: 'POST',
//            beforeSend: function () {
//                //ldBtn.start();
//                $("#i" + elmn).show();
//            },
//            success: function (data) {
//                //ldBtn.stop();
//                $("#i" + elmn).hide();

//                if (data != null) {
//                    if (data.creditId != null && data.isNppExists == 0) {
//                        var txtFlag = "addMotorcycle";
//                        var item = 'MOTOR';

//                        $('#CMDate').val(data.creditDate);
//                        $('#PONumber').val(data.poNumber);
//                        $('#ConstomerName').val(data.customerName);
//                        $('#installmentDate').val(data.installmentDate);
//                        $('#DealerCode').val(data.dealerCode);
//                        $('#DealerName').val(data.dealerName);
//                        $('#DealerAddress').val(data.dealerAddress);
//                        $('#PaymentDealerAmount').val(data.agreementValue);
//                        $('#PaymentDealerTopUpAmount').val(data.agreementValue);
//                        $('#TotalKewajibanNPPLama').val(data.TotalKewajibanNPPLama);

//                        if (data.itemId === '002') {
//                            carForm.style.display = 'block';
//                            btnRapindo.style.display = 'block';

//                            $('#OS_HDR_DealerCode').val(data.dealerCode + ' - ' + data.dealerName);
//                            $('#OS_HDR_OTRAmount').val(data.assetCost);
//                            $('#OS_HDR_InsuranceAmount').val(data.insuranceAmount);

//                            $('#OS_TAC_DealerName').val(data.dealerCode + ' ' + data.dealerName);
//                            $('#OS_TAC_Amount').val(data.tacMax);
//                            $('#OS_TP_DealerName').val(data.dealerCode + ' ' + data.dealerName);
//                            $('#OS_TP_Amount').val(data.tacMax);

//                            item = 'MOBIL';
//                            txtFlag = "addCar";
//                        }
//                        else {
//                            carForm.style.display = 'none';
//                            btnRapindo.style.display = 'none';
//                            item = 'MOTOR';
//                            txtFlag = "addMotorcycle";
//                        }

//                        if (data.applicationTypeId == '03') {
//                            disbursalTypeForm.style.display = 'block';

//                            fnDisbursalTypeUMCView(data.branchId);

//                            $('#chasisNo').val(data.ChasisNo);
//                            $('#machineNo').val(data.MachineNo);
//                            $('#BankAccountNumber').val(data.CustomerBankAccountNoUMC);
//                            $('#BankAccountName').val(data.CustomerNameUMC);

//                            $('#InsentifPayment').val(data.incentiveNominal);

//                        }

//                        if (data.isTopupMS) {
//                            topupNomField.style.display = "block";

//                            $('#PaymentDealerTopUpAmount').val(data.topupNominal);
//                        }

//                        //Hidden Value
//                        $('#hdnApplicationTypeId').val(data.applicationTypeId);
//                        $('#hdnDealerCode').val(data.dealerCode);
//                        $('#hdnNFAPercent').val(data.nfaPercent);
//                        $('#hdnOtherAPValue').val(data.otherAPValue);
//                        $('#hdnAgreementValue').val(data.agreementValue);
//                        $('#hdnFlag').val(txtFlag);

//                        $('#hdnIsValidCreditId').val(true);

//                        var mssg = 'CreditId ' + data.creditId + ' Ditemukan\n'
//                            + 'Data ini merupakan item ' + item;
//                        Common.Alert.Success(mssg);
//                    }
//                    else if (data.isNppExists == 1) {
//                        Common.Alert.Error("Credit Id : " + creditId + " - Sudah Tersedia Di NPP");
//                    }
//                }
//                else {
//                    Common.Alert.Error("Data Tidak Ditemukan, Cek Status Verifikasi Konsumen untuk Credit Id : " + creditId);
//                }
//            },
//            error: function (error) {
//                //ldBtn.stop();
//                $("#i" + elmn).hide();
//                Common.Alert.Error(error);
//            }
//        });
//    }
//}

var Rapindo = {
  handle_btn: function (elmn) {
    var chassisno = $("#chasisNo").val();
    //var ldBtn = Ladda.create(document.getElementById(elmn));

    $.ajax({
      url: "CheckRapindo?chassisno=" + chassisno,
      type: "POST",
      contentType: "application/json; charset=utf-8",
      beforeSend: function () {
        //ldBtn.start();
        $("#i" + elmn).show();
      },
    })
      .done(function (data, textStatus, jqXHR) {
        var succ = data;
        if (succ == "Success") {
          var btn = document.getElementById("btnSaveDraft");
          btn.disabled = false;
          var btnrfa = document.getElementById("btnRFA");
          var btnapprove = document.getElementById("btnApprove");
          if (btnrfa !== null) {
            btnrfa.disabled = false;
          }
          if (btnapprove !== null) {
            btnapprove.disabled = false;
          }
          Common.Alert.Success(succ);
        } else {
          Common.Alert.Error(succ);
        }
        //ldBtn.stop
        $("#i" + elmn).hide();
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        var err = textStatus;
        $("#i" + elmn).hide();
        Common.Alert.Error(err);
      });
  },
};

//////////////////////////////
//Function Modifier Html
//////////START///////////////

function fnValidationChasisNo(elmn) {
  //var ldBtn = Ladda.create(document.getElementById(elmn.id));
  var isValidCreditId = $("#hdnIsValidCreditId").val();

  if (elmn.value != "" && elmn.value.length == 17) {
    var chasisCode = elmn.value.substring(9, 10);
    var creditId = $("#creditId").val();

    if (isValidCreditId == "true") {
      $.ajax({
        url:
          "CheckChasisCode?creditId=" + creditId + "&chassisCode=" + chasisCode,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
          $("#i" + elmn.id).show();
        },
        success: function (result) {
          $("#i" + elmn.id).hide();
          if (result != null) {
            var dataResult = result.data;

            if (result.status == "Success" && dataResult.status) {
              Common.Alert.Success(dataResult.message);

              $("#hdnIsValidChasisCode").val(true);
            } else if (result.status == "Success" && !dataResult.status) {
              Common.Alert.Warning(dataResult.message);
              $("#" + elmn.id).val("");
            }
          } else {
            Common.Alert.Error("Validasi Nomor Rangka Gagal");
          }
        },
        error: function (error) {
          $("#i" + elmn.id).hide();
          Common.Alert.Error(error);
        },
      });
    } else {
      Common.Alert.Error(
        "Pastikan Credit Id Sudah Berhasil Di Proses Dengan Menekan Tombol 'Process Credit ID'"
      );
      $("#" + elmn.id).val("");
    }
  } else {
    Common.Alert.Error("Pastikan Nomor Rangka Memiliki Panjang 17 Digit");
    $("#" + elmn.id).val("");
  }
}

function fnValidationBillReceivedDate(elmn) {
  var isValidCreditId = $("#hdnIsValidCreditId").val();
  var dateValue = new Date(elmn.value);
  var date = new Date();

  var min_date = date.setDate(date.getDate() - 3);
  var max_date = date.setDate(date.getDate() + 3);
  //var insert_date = dateValue.getFullYear() + "-" + (dateValue.getMonth() + 1) + "-" + dateValue.getDate();

  if (isValidCreditId == "true") {
    //Validation : Tanggal Terima Tagihan
    //- Tanggal tidak boleh lebih dari tanggal input NPP
    if (dateValue <= date) {
      //Common.Alert.Success(insert_date + "\n" + current_date);

      $.ajax({
        url: "CheckBillingReceivedDate?day=" + dateValue.getDate(),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {},
        success: function (result) {
          if (result.code == "200") {
            var instDate = new Date(
              dateValue.getMonth() +
                2 +
                "/" +
                result.data.consumenInstallmentDate +
                "/" +
                dateValue.getFullYear()
            );
            $("#installmentDate").attr("type", "text");
            $("#installmentDate").val(
              instDate.getDate() +
                "/" +
                (instDate.getMonth() + 1) +
                "/" +
                dateValue.getFullYear()
            );
            $("#installmentDate").attr("readonly", "readonly");
          } else if (result.code == "204") {
            $("#installmentDate").removeAttr("readonly");
            $("#installmentDate").attr("type", "date");
            //$('#installmentDate').val(dateValue.getDate() + "/" + (dateValue.getMonth() + 1) + "/" + dateValue.getFullYear());
          } else {
            Common.Alert.Success(
              "Validasi Tanggal Terima Tagihan Gagal, Coba Input Tanggal Beberapa saat lagi"
            );
            $("#installmentDate").val("");
          }
        },
        error: function (error) {
          Common.Alert.Error(error);
        },
      });
    } else {
      Common.Alert.Error(
        "Tanggal Terima Tagihan Tidak Boleh Melebihi Dari Tanggal Pembuatan NPP"
      );
      $("#" + elmn.id).val("");
    }
  } else {
    Common.Alert.Error(
      "Pastikan Credit Id Sudah Berhasil Di Proses Dengan Menekan Tombol 'Process Credit ID'"
    );
    $("#" + elmn.id).val("");
  }
}

function fnValidationInstallmentDate(elmn) {
  var dateValue = new Date(elmn.value);
  var date = new Date();

  var min_date = new Date();
  var max_date = new Date();
  min_date.setDate(date.getDate() - 4);
  max_date.setDate(date.getDate() + 4);
  //var insert_date = dateValue.getFullYear() + "-" + (dateValue.getMonth() + 1) + "-" + dateValue.getDate();

  if (min_date > dateValue || dateValue > max_date) {
    Common.Alert.Warning(
      "Tanggal Angsuran, Minimal H-3 atau Maksimal H+3 dari hari ini"
    );
    $("#" + elmn.id).val("");
  }
}

function fnJobTitleChangeValue(elmn, prefix, seq) {
  var ddwnValue = elmn.value;
  var DataCount = $("#WrapperOrderSource_" + prefix).children().length;

  var decision = false;
  var xValue = $("#ddwnJobTitleId_" + prefix + "_" + seq).val();

  for (let i = 0; i < DataCount; i++) {
    var yValue = $("#ddwnJobTitleId_" + prefix + "_" + i).val();
    if (i != seq && xValue === yValue) {
      $("#ddwnJobTitleId_" + prefix + "_" + seq).val();
      decision = true;
      break;
    }
  }

  if (decision) {
    $("#ddwnJobTitleId_" + prefix + "_" + seq).val("");
    Common.Alert.Error(
      "Job Title Sudah Terpilih, Silahkan Pilih Job Title Lain"
    );
  } else {
    var ddwnElmn = $("#ddwnPersonnelId_" + prefix + "_" + seq);
    ddwnElmn.empty();

    $.ajax({
      url: "GetDealerPersonnelListByJobTitle?jobTitleId=" + ddwnValue,
      type: "POST",
      success: function (data) {
        if (data.length > 0) {
          ddwnElmn.append(
            $("<option selected disabled></option>").html("Pilih Personnel")
          );
          $.each(data, function (key, value) {
            ddwnElmn.append(
              $("<option></option>")
                .val(value.personnelId)
                .html(value.personnelName)
            );
          });
        } else {
          ddwnElmn.append(
            $("<option selected disabled></option>").html(
              "Personnel Tidak Tersedia"
            )
          );
        }
      },
      error: function (error) {
        Common.Alert.Error(error);
      },
    });
  }
}

function fnDisbursalTypeUMCView(branchId) {
  var ddwnElmn = $("#ddwnDisbursalType");
  ddwnElmn.empty();

  $.ajax({
    url: "GetDisbursalTypeUMCListByBranchId?branchId=" + branchId,
    type: "POST",
    success: function (data) {
      if (data.length > 0) {
        ddwnElmn.append(
          $("<option selected disabled></option>").html(
            "Pilih Metode Pembayaran"
          )
        );
        $.each(data, function (key, value) {
          ddwnElmn.append(
            $("<option></option>")
              .val(value.disbursalTypeUmc)
              .html(value.description)
          );
        });
      } else {
        ddwnElmn.append(
          $("<option selected disabled></option>").html(
            "Metode Pembayaran Tidak Tersedia"
          )
        );
      }
    },
    error: function (error) {
      Common.Alert.Error(error);
    },
  });
}

function fnCheckMetode() {
  var dropdown = document.getElementById("ddwnDisbursalType");
  var current_value = dropdown.options[dropdown.selectedIndex].value;

  if (current_value == "C") {
    document.getElementById("UangAngsuran").style.display = "block";
  } else {
    document.getElementById("UangAngsuran").style.display = "none";
  }
}

function fnCalculateAmountOrderSource(elmn, prefix) {
  var DataCount = $("#WrapperOrderSource_" + prefix).children().length;
  var maxAmount = $("#OS_" + prefix + "_Amount").val();
  var totalAmountBeforeTax = 0;
  var totalAmountAfterTax = 0;

  var IdElement = elmn.id;
  var seqId = IdElement.substring(IdElement.length - 1);

  $("#txtAmountAfterTax_" + prefix + "_" + seqId).val(elmn.value * 1.02);

  for (let i = 0; i < DataCount; i++) {
    var amountX = $("#txtAmount_" + prefix + "_" + i).val();
    var amountY = $("#txtAmountAfterTax_" + prefix + "_" + i).val();
    totalAmountBeforeTax = parseInt(totalAmountBeforeTax) + parseInt(amountX);
    totalAmountAfterTax = parseInt(totalAmountAfterTax) + parseInt(amountY);
  }

  $("#Total" + prefix + "AmountBeforeTax").val(totalAmountBeforeTax);
  $("#Total" + prefix + "AmountAfterTax").val(totalAmountAfterTax);
}

function fnAddFieldInputOrderSource(prefixWrapper) {
  var wrapper = $("#WrapperOrderSource_" + prefixWrapper);
  var seq = wrapper.children().length;
  var optionValue = "";

  $.ajax({
    url: "GetDealerJobTitleList",
    type: "POST",
    success: function (data) {
      if (data.length > 0) {
        optionValue += "";
        $.each(data, function (key, value) {
          optionValue +=
            '<option value="' +
            value.jobTitleId +
            '">' +
            value.jobTitleDescription +
            "</option>";
        });
      }

      var elmn =
        '<div class="col-lg-10 col-md-10 col-sm-10 ' +
        prefixWrapper +
        "_" +
        seq +
        '">' +
        '<div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">' +
        '<select id="ddwnJobTitleId_' +
        prefixWrapper +
        "_" +
        seq +
        '" name="JobTitleId" class="form-control" onchange="fnJobTitleChangeValue(this,\'' +
        prefixWrapper +
        "'," +
        seq +
        ')">' +
        '<option disabled selected value="">Pilih Owner / Dealer / Office</option>' +
        optionValue +
        "</select>" +
        "</div>" +
        '<div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">' +
        '<select id="ddwnPersonnelId_' +
        prefixWrapper +
        "_" +
        seq +
        '" name="PersonnelId" class="form-control">' +
        "<option disabled selected>Pilih Personnel</option>" +
        "</select>" +
        "</div>" +
        '<div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">' +
        '<input id="txtAmount_' +
        prefixWrapper +
        "_" +
        seq +
        '" class="form-control" type="number" value="0" placeholder="Rp" oninput="fnCalculateAmountOrderSource(this,\'' +
        prefixWrapper +
        "')\">" +
        "</div>" +
        '<div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">' +
        '<input id="txtAmountAfterTax_' +
        prefixWrapper +
        "_" +
        seq +
        '" value="0" class="form-control" placeholder="Rp" readonly>' +
        "</div>" +
        "</div>";

      wrapper.append(elmn);

      $("#btnDeleteOrderSource" + prefixWrapper).show();
    },
    error: function (error) {
      Common.Alert.Error(error);
    },
  });
}

//'@*<div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">' +
//    '<button type="button" class="btn-delete" style="width: 100%;" onclick="fnRemoveFieldInputOrderSource(\'' + prefixWrapper + '_' + seq + '\')">Delete</button>' +
//    '</div>*@' +

function fnRemoveFieldInputOrderSource(elmn) {
  var DataCount = $("#WrapperOrderSource_" + elmn).children().length;

  if (DataCount === 2) {
    $("#btnDeleteOrderSource" + elmn).hide();
  }

  $("div." + elmn + "_" + (DataCount - 1)).remove();
}

////////////END///////////////

//$('#billReceivedDate, #billReceiptDate, #downPaymentReceiptDate, #receiptDate, #bpkbLetterDate, #bastDate, #installmentDate').datepicker({
//    format: "mm/dd/yyyy",
//    autoclose: true,
//    todayHighlight: true
//});

$("#installmentdate").datepicker({
  format: "mm/dd/yyyy",
  autoclose: true,
  todayhighlight: true,
});

//SHOW HIDE
//document.getElementById("CMPO_more").addEventListener('click', changeClass);

function changeClass() {
  var content = document.getElementById("CMPO");

  var identitas = document.getElementById("CMPO_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "Select CM / PO";
  } else {
    identitas.innerHTML = "Select CM / PO";
  }
}

document
  .getElementById("MetodePembayaran_more")
  .addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("MetodePembayaran");

  var identitas = document.getElementById("MetodePembayaran_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "NPP Add";
  } else {
    identitas.innerHTML = "NPP Add";
  }
}

document
  .getElementById("MetodePembayaranInsentif_more")
  .addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("MetodePembayaranInsentif");

  var identitas = document.getElementById("MetodePembayaranInsentif_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "NPP Add";
  } else {
    identitas.innerHTML = "NPP Add";
  }
}

document.getElementById("AddData_more").addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("AddData");

  var identitas = document.getElementById("AddData_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "CF Sumber Order Dealer Hdr";
  } else {
    identitas.innerHTML = "CF Sumber Order Dealer Hdr";
  }
}

document
  .getElementById("DataPersonal_more")
  .addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("DataPersonal");

  var identitas = document.getElementById("DataPersonal_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "Sumber Order TAC";
  } else {
    identitas.innerHTML = "Sumber Order TAC";
  }
}

document
  .getElementById("DataPersonalRate_more")
  .addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("DataPersonalRate");

  var identitas = document.getElementById("DataPersonalRate_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "Sumber Order Subsidi Pihak Ke-3 ";
  } else {
    identitas.innerHTML = "Sumber Order Subsidi Pihak Ke-3 ";
  }
}

document.getElementById("DataItem_more").addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("DataItem");

  var identitas = document.getElementById("DataItem_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "Data Item";
  } else {
    identitas.innerHTML = "Data Item";
  }
}

document
  .getElementById("DataPenagihan_more")
  .addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("DataPenagihan");

  var identitas = document.getElementById("DataPenagihan_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "Data Penagihan";
  } else {
    identitas.innerHTML = "Data Penagihan";
  }
}

document
  .getElementById("DataPencairan_more")
  .addEventListener("click", changeClass);

function changeClass() {
  var content = document.getElementById("DataPencairan");

  var identitas = document.getElementById("DataPencairan_more");
  content.classList.toggle("show");

  if (content.classList.contains("Show")) {
    identitas.innerHTML = "Data Pencairan";
  } else {
    identitas.innerHTML = "Data Pencairan";
  }
}

//SELECT CM / PO

function CheckCMPO(current_value) {
  if (current_value == "CreditId") {
    document.getElementById("CreditId").style.display = "block";
    document.getElementById("PONo").style.display = "none";
  } else if (current_value == "PONo") {
    document.getElementById("CreditId").style.display = "none";
    document.getElementById("PONo").style.display = "block";
  }
}
