//untuk handle button Search, GO, Previous, Next in Paging List pada Partial View
$(document).ready(function () {
  $("#btnProcessCreditId").click(function () {
    nppProcessCredit.paging("btnProcessCreditId");
  });

  $("#btnLookupDealerBankRef").click(function () {
    dealerBankReff.paging("btnLookupDealerBankRef");
  });

  $("#btnApprove").click(function () {
    approvalActionNPP.dDownReason("A", "1", "btnApprove");
  });

  $("#btnReject").click(function () {
    approvalActionNPP.dDownReason("R", "2", "btnReject");
  });

  $("#btnReview").click(function () {
    approvalActionNPP.dDownReason("C", "3", "btnReview");
  });

  //$("#btnApprovalProcess").click(function () {
  //    ChooseReasonApproval();
  //});
});

function ChooseReasonApproval(btn) {
  var ldBtn = Ladda.create(document.getElementById(btn.id));
  var ReasonId = $("#DDownReason").val();
  var ReasonDesc = $("#TxtReasonDesc").val();
  var CreditId = $("#CreditId").val();
  var StatusApproval = $("#txtStatusApproval").val();

  var params = {
    ReasonId: ReasonId,
    ReasonDesc: ReasonDesc,
    CreditId: CreditId,
    StatusApproval: StatusApproval,
  };

  $.ajax({
    url: "../ApprovalNppProcess",
    type: "POST",
    data: params,
    beforeSend: function () {
      ldBtn.start();
    },
    success: function (data) {
      ldBtn.stop();
      if (data.success) {
        Common.Alert.SuccessThenRoute(data.messages, data.action);
      } else {
        Common.Alert.Error(data.messages);
      }
      //window.location.href = "/NPP/";
    },
    error: function (error) {
      ldBtn.stop();
      Common.Alert.Error(error);
    },
  });
}

var nppProcessCredit = {
  //handle pagination view
  paging: function (elmn) {
    var params = {
      pageIndex: 1,
      searchTerm: "",
    };

    $.ajax({
      url: "PaginationLookupNppProcessCredit",
      type: "POST",
      data: params,
      beforeSend: function () {
        $("#i" + elmn).show();
      },
      success: function (data) {
        $("#i" + elmn).hide();
        $("#mdlNppProcessCredit").modal({
          backdrop: false,
        });
        $("#containerNppProcessCredit").html(data);
      },
      error: function (error) {
        $("#i" + elmn).hide();
        var d = error;
      },
    });
  },
  //handle button Search, GO, Previous, Next in Paging List
  handle_btn: function (pageIndex, searchTerm) {
    var params = {
      pageIndex: pageIndex,
      searchTerm: searchTerm,
    };
    $.ajax({
      url: "PaginationLookupNppProcessCredit",
      type: "POST",
      data: params,
      success: function (data) {
        $("#containerNppProcessCredit").html(data);
      },
      error: function (error) {
        var d = error;
      },
    });
  },
  //handle button when selected data in lookup
  selectedNppProcessCredit: function (creditId) {
    //var creditId = $('#creditId').val();
    //var ldBtn = Ladda.create(document.getElementById(elmn.id));
    var carForm = document.getElementById("CarForm");
    var btnRapindo = document.getElementById("btnCheckRapindo");
    var disbursalTypeForm = document.getElementById("DisbursalTypeForm");
    var topupNomField = document.getElementById("TopupAmountField");
    var currentDate = new Date();

    $.ajax({
      url: "GetDataCMByCreditId?creditId=" + creditId,
      type: "POST",
      beforeSend: function () {
        //ldBtn.start();
        $("#ibtnProcessCreditId").show();
      },
      success: function (data) {
        //ldBtn.stop();
        $("#ibtnProcessCreditId").hide();

        if (data != null) {
          $("#ddwnDisbursalType").removeAttr("required");

          var txtFlag = "addMotorcycle";
          var item = "MOTOR";

          $("#creditId").val(data.creditId);
          $("#CMDate").val(data.creditDate);
          $("#PONumber").val(data.poNumber);
          $("#ConstomerName").val(data.customerName);

          //KETENTUAN TANGGAL JATUH TEMPO KONSUMEN PADA AKHIR BULAN 27 - 31
          if (currentDate.getDate() > 26) {
            $("#installmentDate").attr("readonly", "readonly");
          }

          $("#installmentDate").val(data.installmentDate);
          $("#DealerCode").val(data.dealerCode);
          $("#DealerName").val(data.dealerName);
          $("#DealerAddress").val(data.dealerAddress);
          $("#PaymentDealerAmount").val(data.agreementValue);
          $("#PaymentDealerTopUpAmount").val(data.agreementValue);
          $("#TotalKewajibanNPPLama").val(data.totalKewajibanNPPLama);

          if (data.itemId === "002") {
            carForm.style.display = "block";
            btnRapindo.style.display = "block";

            $("#OS_HDR_DealerCode").val(
              data.dealerCode + " - " + data.dealerName
            );
            $("#OS_HDR_OTRAmount").val(data.assetCost);
            $("#OS_HDR_InsuranceAmount").val(data.insuranceAmount);

            $("#OS_TAC_DealerName").val(
              data.dealerCode + " " + data.dealerName
            );
            $("#OS_TAC_Amount").val(data.tacMax);
            $("#OS_TP_DealerName").val(data.dealerCode + " " + data.dealerName);
            $("#OS_TP_Amount").val(data.tacMax);

            item = "MOBIL";
            txtFlag = "addCar";
          } else {
            carForm.style.display = "none";
            btnRapindo.style.display = "none";
            item = "MOTOR";
            txtFlag = "addMotorcycle";
          }

          if (data.applicationTypeId == "03") {
            disbursalTypeForm.style.display = "block";

            fnDisbursalTypeUMCView(data.branchId);

            $("#chasisNo").val(data.chasisNo);
            $("#machineNo").val(data.machineNo);
            $("#BankAccountNumber").val(data.customerBankAccountNoUMC);
            $("#BankAccountName").val(data.customerNameUMC);

            $("#InsentifPayment").val(data.incentiveNominal);
            $("#ddwnDisbursalType").attr("required", true);
          }

          if (data.isTopupMS) {
            topupNomField.style.display = "block";

            $("#PaymentDealerTopUpAmount").val(data.topupNominal);
          }

          //Hidden Value
          $("#hdnApplicationTypeId").val(data.applicationTypeId);
          $("#hdnDealerCode").val(data.dealerCode);
          $("#hdnNFAPercent").val(data.nfaPercent);
          $("#hdnOtherAPValue").val(data.otherAPValue);
          $("#hdnAgreementValue").val(data.agreementValue);
          $("#hdnFlag").val(txtFlag);

          $("#hdnIsValidCreditId").val(true);

          var mssg =
            "CreditId " +
            data.creditId +
            " Ditemukan\n" +
            "Data ini merupakan item " +
            item;
          //Common.Alert.Success(mssg);
        } else {
          Common.Alert.Error(
            "Data Tidak Ditemukan, Cek Status Verifikasi Konsumen untuk Credit Id : " +
              creditId
          );
        }
      },
      error: function (error) {
        //ldBtn.stop();
        $("#ibtnProcessCreditId").hide();
        Common.Alert.Error(error);
      },
    });

    $("#mdlNppProcessCredit").modal("hide");
  },
};

var dealerBankReff = {
  //handle pagination view
  paging: function (elmn) {
    var dealerCode = $("#hdnDealerCode").val();
    var params = {
      pageIndex: 1,
      searchTerm: dealerCode,
    };

    $.ajax({
      url: "PaginationDealerBankRef",
      type: "POST",
      data: params,
      beforeSend: function () {
        $("#i" + elmn).show();
      },
      success: function (data) {
        $("#i" + elmn).hide();
        $("#mdlDealerBank").modal({
          backdrop: false,
        });
        $("#containerDealerBank").html(data);
      },
      error: function (error) {
        $("#i" + elmn).hide();
        var d = error;
      },
    });
  },
  //handle button Search, GO, Previous, Next in Paging List
  handle_btn: function (pageIndex, searchTerm) {
    var params = {
      pageIndex: pageIndex,
      searchTerm: searchTerm,
    };
    $.ajax({
      url: "PaginationDealerBankRef",
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
  //handle button when selected data in lookup
  selectedBankRef: function (
    dealerBnkRef,
    accNumber,
    accName,
    bnkName,
    bnkDesc
  ) {
    $("#BankAccountNumber").val(accNumber);
    $("#hdnBankReferenceId").val(dealerBnkRef);
    $("#BankAccountName").val(accName);
    $("#BankName").val(bnkName);
    $("#PaymentDesc").val(bnkDesc);

    $("#mdlDealerBank").modal("hide");
  },
};

var approvalActionNPP = {
  //handle pagination view
  dDownReason: function (statusApproval, typeApproval, elmn) {
    var dealerCode = $("#hdnDealerCode").val();
    var ldBtn = Ladda.create(document.getElementById(elmn));
    var params = {
      pageIndex: 1,
      searchTerm: dealerCode,
    };
    $.ajax({
      url:
        "../PopupApprovalReason?statusApproval=" +
        statusApproval +
        "&typeApproval=" +
        typeApproval,
      type: "POST",
      data: params,
      beforeSend: function () {
        ldBtn.start();
      },
      success: function (data) {
        ldBtn.stop();
        $("#mdlApprovalAction").modal({
          backdrop: false,
        });
        $("#containerApprovalReason").html(data);

        $("#txtStatusApproval").val(statusApproval);
      },
      error: function (error) {
        ldBtn.stop();
        var d = error;
      },
    });
  },
  //handle button Search, GO, Previous, Next in Paging List
  handle_btn: function (pageIndex, searchTerm) {
    var params = {
      pageIndex: pageIndex,
      searchTerm: searchTerm,
    };
    $.ajax({
      url: "PaginationDealerBankRef",
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
  //handle button when selected data in lookup
  selectedBankRef: function (
    dealerBnkRef,
    accNumber,
    accName,
    bnkName,
    bnkDesc
  ) {
    $("#BankAccountNumber").val(accNumber);
    $("#hdnBankReferenceId").val(dealerBnkRef);
    $("#BankAccountName").val(accName);
    $("#BankName").val(bnkName);
    $("#PaymentDesc").val(bnkDesc);

    $("#mdlDealerBank").modal("hide");
  },
};
