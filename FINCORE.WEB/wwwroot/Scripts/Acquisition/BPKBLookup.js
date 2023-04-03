jQuery(document).ready(function () {
    $('#ModalTemplate').on('hidden.bs.modal', function () {
        // do something…
        if ($('#btnLoadModalReceiver').length) {
            document.getElementById("btnLoadModalReceiver").style.display = "block";
        }
        if ($('#btnLoadModal').length) {
            document.getElementById("btnLoadModal").style.display = "block";
        }
        if ($('#btnLoadModalAsuransi').length) {
            document.getElementById("btnLoadModalAsuransi").style.display = "block";
        }
        if ($('#btnLoadModalDealer').length) {
            document.getElementById("btnLoadModalDealer").style.display = "block";
        }
        if ($('#btnLoadModalReceiverOut').length) {
            document.getElementById("btnLoadModalReceiverOut").style.display = "block";
        }
        if ($('#btnLoadModalBiroJasa').length) {
            document.getElementById("btnLoadModalBiroJasa").style.display = "block";
        }
        if ($('#btnLoadModalKaryawan').length) {
            document.getElementById("btnLoadModalKaryawan").style.display = "block";
        }
    })
});

var npp_paging ={
    handle_btn: async function (pageIndex, searchTerm, mKey) {
        var mUrl = "";
        switch (mKey) {
            case "npp":
                mUrl = "../BPKB/BPKBPopupTest";
                break;
            case "rcv":
                mUrl = "../BPKB/BPKBPopupReceiver";
                break;
            case "deal":
                mUrl = "../BPKB/BPKBPopupDealer";
                break;
            case "rcvo":
                mUrl = "../BPKB/BPKBPopupReceiverOut";
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
        var params = {
            pageIndex: pageIndex,
            searchTerm: searchTerm
        }
        $.ajax({
            url: mUrl,
            type: "POST",
            data: params,
            success: function (data) {
                $("#ModalBody").html(data);
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup Location. Error " + d);
            }
        });
    }
}