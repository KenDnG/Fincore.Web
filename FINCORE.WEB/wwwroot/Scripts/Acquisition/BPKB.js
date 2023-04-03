jQuery(document).ready(function () {
    //$('#dateBPKB').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#fakturdate').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#datebpkbloan').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#datebpkbplan').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#inputBPKBOut').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#datebpkbreentry').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#deadlinedate').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});
    //$('#dateBPKBin').datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true,
    //    todayHighlight: true
    //});

    $("#btnLoadModal").click(function () {
        bpkb_lookup.paging('npp', this.id);
    })
    $("#btnLoadModalReceiver").click(function () {
        bpkb_lookup.paging('rcv', this.id);
    })
    $("#btnLoadModalDealer").click(function () {
        bpkb_lookup.paging('deal', this.id);
    })
    $("#btnLoadModalReceiverOut").click(function () {
        bpkb_lookup.paging('rcvo', this.id);
    })
    $("#btnLoadModalAsuransi").click(function () {
        bpkb_lookup.paging('asu', this.id);
    })
    $("#btnLoadModalBiroJasa").click(function () {
        bpkb_lookup.paging('bj', this.id);
    })
    $("#btnLoadModalKaryawan").click(function () {
        bpkb_lookup.paging('kary', this.id);
    })

    if (document.getElementById("isview").value == "true") {
        //$('button').prop('disabled', true);
        $('input').prop('disabled', true);
        $('div').prop('disabled', true);
        $("#btnsave").attr("disabled", "disabled");
        $('select').prop('disabled', true);
        //document.getElementById("SelectLocation").disabled = true;
        document.getElementById("btnLoadModal").style.display = "none";
    }

    $("btnAddDataBPKB").click(function () {
        $("AddDataBPKB").toggle();
    });

    $("btnBPKBIn").click(function () {
        $("BPKBIn").toggle();
    });

    $("btnBPKBPinjam").click(function () {
        $("BPKBPinjam").toggle();
    });

    $("btnBPKBEntry").click(function () {
        $("BPKBEntry").toggle();
    });

    $("btnBPKBOut").click(function () {
        $("BPKBOut").toggle();
    });
    //enable select on submit for model binding
    $('#contactform').submit(function () {
        document.getElementById("SelectLocation").disabled = false;
        document.getElementById("SelectReason").disabled = false;
    });

    const pnlAdd = document.getElementById("AddDataBPKB_more");
    pnlAdd?.addEventListener('click', () => {
        bpkb_load.changeClass_AddBPKB();
    });

    const pnlIn = document.getElementById("BPKBIn_more");
    pnlIn?.addEventListener('click', () => {
        bpbk_load.changeClass_InBPKB();
    });

    //Ladda.bind('button', {
    //    callback: function (instance) {
    //        var progress = 0;
    //        var interval = setInterval(function () {
    //            progress = Math.min(progress + Math.random() * 0.1, 1);
    //            instance.setProgress(progress);

    //            if (progress === 1) {
    //                instance.stop();
    //                clearInterval(interval);
    //            }
    //        }, 200);
    //    }
    //});

    //Ladda.bind(document.getElementById("btnLoadModal"), {
    //    callback: function (instance) {
    //        var progress = 0;
    //        var interval = setInterval(function () {
    //            progress = Math.min(progress + Math.random() * 0.1, 1);
    //            //instance.setProgress(progress);

    //            if (progress === 1) {
    //                //instance.stop();
    //                clearInterval(interval);
    //            }
    //        }, 200);
    //    }
    //});

    bpkb_load.init();
});
var bpkb_load = {
    init: function () {
        //this.listdata();
    },
    view: function (ids) {
        alert("BPKB selected:" + ids);
        //TO DO: View Data from DataTable List
    },
    changeClass_AddBPKB: function () {
        var content = document.getElementById("AddDataBPKB");

        var identitas = document.getElementById("AddDataBPKB_more");
        content.classList.toggle('show');

        if (content.classList.contains("Show")) {
            identitas.innerHTML = "Add Data BPKB";
        } else {
            identitas.innerHTML = "Add Data BPKB";
        }
    },
    changeClass_InBPKB: function () {
        var content = document.getElementById("BPKBIn");

        var identitas = document.getElementById("BPKBIn_more");
        content.classList.toggle('show');

        if (content.classList.contains("Show")) {
            identitas.innerHTML = "BPKB In";
        } else {
            identitas.innerHTML = "BPKB In";
        }
    }
}

var bpkb_lookup = {
    paging: function (mKey, mBtn) {
        $("#i"+mBtn).addClass("fa-spinner fa-spin");
        var mUrl = "";
        var params = {};
        params = {
            pageIndex: 1,
            searchTerm: ""
        }
        switch (mKey) {
            case "npp":
                mUrl = "../BPKB/BPKBPopupTest";
                break;
            case "rcv":
                mUrl = "../BPKB/BPKBPopupReceiver";
                break;
            case "deal":
                mUrl = "../BPKB/BPKBPopupDealer";
                params = {
                    pageIndex: 1,
                    searchTerm: "",
                    CreditId: document.getElementById("InputAgreementNumber").value
                }
                break;
            case "rcvo":
                mUrl =  '../BPKB/BPKBPopupReceiverOut';
                break;
            case "asu":
                mUrl = "../BPKB/BPKBPopupAsuransi";
                break;
            case "bj":
                mUrl = "../BPKB/BPKBPopupBiroJasa";
                break;
            case "kary":
                mUrl = "../BPKB/BPKBPopupKaryawan";
                break;
        }
        //var ldBtn = Ladda.create(document.getElementById(mBtn));
        $.ajax({
            url: mUrl,
            type: "POST",
            data: params,
            beforeSend: function () {
                //ldBtn.start();
            },
            success: function (data) {
                //ldBtn.stop();
                //Ladda.stopAll();
                $("#i" + mBtn).removeClass("fa-spinner fa-spin");
                $("#ModalTemplate").modal({
                    backdrop: false
                });
                document.getElementById(mBtn).style.display = "none";
                $("#ModalBody").html(data);
            },
            error: function (error) {
                //ldBtn.stop();
                $("#i" + mBtn).removeClass("fa-spinner fa-spin");
                var d = error;
                Common.Alert.Error("Failed to show Lookup Location. Error " + d);
            }
        });
    },
    SelectedNpp: function (BranchId, AgreementNumber, CustomerName,QQname,sBPKB,ChasisNo,MachineNo,ItemTypeName,ItemMerk,ItemColor,ItemYear,DealerCode,StatusNPP,StatusLunasNPP,StatusBPKB) {
        $("#InputAgreementNumber").val(AgreementNumber);
        $("#InputBranchId").val(BranchId);
        $("#InputCustomerName").val(CustomerName);
        $("#InputNamaBPKB").val(QQname);
        $("#InputSBPKB").val(sBPKB);
        $("#InputChasisNo").val(ChasisNo);
        $("#InputMachineNo").val(MachineNo);
        $("#InputTipeKendaraan").val(ItemTypeName);
        $("#InputBrand").val(ItemMerk);
        $("#InputWarna").val(ItemColor);
        $("#InputDealer").val(DealerCode);
        $("#InputStatusAgreement").val(StatusNPP);
        $("#InputStatusLunas").val(StatusLunasNPP);
        $("#InputStatusBPKB").val(StatusBPKB);
        $("#InputItemYear").val(ItemYear);

        $("#ModalTemplate").modal("hide");
    },
    SelectedReceiverPinjam: function (ReceiverCode, ReceiverName) {
        $("#InputReceiverCodePinjam").val(ReceiverCode);
        $("#InputDiterimaOlehPinjam").val(ReceiverName);
        if (ReceiverName == "Biro Jasa" || ReceiverName == "Dealer" || ReceiverName == "Asuransi" || ReceiverName == "Karyawan") {
            document.getElementById("divSpecial").removeAttribute("hidden");
            if (ReceiverName == "Biro Jasa") {
                document.getElementById("divBiroJasa1").removeAttribute("hidden");
                document.getElementById("divBiroJasa2").removeAttribute("hidden");
                document.getElementById("divDealer").setAttribute("hidden", true);
                document.getElementById("divAsuransi").setAttribute("hidden", true);
                document.getElementById("divKaryawan").setAttribute("hidden", true);
            }
            else if (ReceiverName == "Asuransi") {
                document.getElementById("divAsuransi").removeAttribute("hidden");
                document.getElementById("divDealer").setAttribute("hidden", true);
                document.getElementById("divBiroJasa1").setAttribute("hidden", true);
                document.getElementById("divBiroJasa2").setAttribute("hidden", true);
                document.getElementById("divKaryawan").setAttribute("hidden", true);
            }
            else if (ReceiverName == "Dealer") {
                document.getElementById("divDealer").removeAttribute("hidden");
                document.getElementById("divBiroJasa1").setAttribute("hidden", true);
                document.getElementById("divBiroJasa2").setAttribute("hidden", true);
                document.getElementById("divAsuransi").setAttribute("hidden", true);
                document.getElementById("divKaryawan").setAttribute("hidden", true);
            }
            else if (ReceiverName == "Karyawan") {
                document.getElementById("divKaryawan").removeAttribute("hidden");
                document.getElementById("divBiroJasa1").setAttribute("hidden", true);
                document.getElementById("divBiroJasa2").setAttribute("hidden", true);
                document.getElementById("divAsuransi").setAttribute("hidden", true);
                document.getElementById("divDealer").setAttribute("hidden", true);
            }
        }
        else
        {
            document.getElementById("divSpecial").setAttribute("hidden", true);
            document.getElementById("divBiroJasa1").setAttribute("hidden", true);
            document.getElementById("divBiroJasa2").setAttribute("hidden", true);
            document.getElementById("divDealer").setAttribute("hidden", true);
            document.getElementById("divAsuransi").setAttribute("hidden", true);
            document.getElementById("divKaryawan").setAttribute("hidden", true);
            $("#InputSpecialName").val('');
            $("#InputSpecialCode").val('');
            $("#SelectBureau").val("");
        }

        $("#ModalTemplate").modal("hide");
    },
    SelectedDealerPinjam: function (DealerName, DealerCode,Address,Phone) {
        $("#InputSpecialName").val(DealerName);
        $("#InputSpecialCode").val(DealerCode);
        $("#InputAddress").val(Address);
        $("#InputPhone").val(Phone);
        $("#ModalTemplate").modal("hide");
    },
    SelectedAsuransiPinjam: function (DealerName, DealerCode, Address, Phone) {
        $("#InputSpecialName").val(DealerName);
        $("#InputSpecialCode").val(DealerCode);
        $("#InputAddress").val(Address);
        $("#InputPhone").val(Phone);
        $("#ModalTemplate").modal("hide");
    },
    SelectedBiroJasaPinjam: function (DealerName, DealerCode, Address, Phone) {
        $("#InputSpecialName").val(DealerName);
        $("#InputSpecialCode").val(DealerCode);
        $("#InputAddress").val(Address);
        $("#InputPhone").val(Phone);
        $("#ModalTemplate").modal("hide");
    },
    SelectedKaryawanPinjam: function (DealerName, DealerCode, Address, Phone) {
        $("#InputSpecialName").val(DealerName);
        $("#InputSpecialCode").val(DealerCode);
        $("#InputAddress").val(Address);
        $("#InputPhone").val(Phone);
        $("#ModalTemplate").modal("hide");
    },
    SelectedReceiverOut: function (ReceiverCode, ReceiverName) {
        $("#InputReceiverCodeOut").val(ReceiverCode);
        $("#InputDiterimaOlehOut").val(ReceiverName);
        $("#ModalTemplate").modal("hide");
    }
}