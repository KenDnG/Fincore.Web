jQuery(document).ready(function () {
    $("#mdllocation").on("hidden.bs.modal", function () {
        document.getElementById("btnshowlokasi").style.display = "block";
        document.getElementById("btnshowlokasiktp").style.display = "block";
        document.getElementById("btnshowlokasicorp").style.display = "block";
        document.getElementById("btnshowlokasipenjamin").style.display = "block";
        document.getElementById("btnshowlokasipasangan").style.display = "block";
        document.getElementById("btnshowlokasitagihan").style.display = "block";
    });
    $("#mdlbank").on("hidden.bs.modal", function () {
        document.getElementById("btnshowbank").style.display = "block";
    });
    $("#mdlreferensi").on("hidden.bs.modal", function () {
        document.getElementById("btnshowlokasiref1").style.display = "block";
        document.getElementById("btnshowlokasiref2").style.display = "block";
        document.getElementById("btnshowlokasiref3").style.display = "block";
    });
})

//untuk handle button Search, GO, Previous, Next in Paging List pada Partial View
var cas_paging = {
    handle_btn: async function (pageIndex, searchTerm, mKey) {
        var mUrl = "";
        switch (mKey) {
            case "mst":
                mUrl = "PaginationMsLocation";
                break;
            case "ktp":
                mUrl = "PaginationMsLocationKTP";
                break;
            case "corp":
                mUrl = "PaginationMsLocationCorp";
                break;
            case "penjamin":
                mUrl = "PaginationMsLocationPenjamin";
                break;
            case "spouse":
                mUrl = "PaginationMsLocationSpouse";
                break;
            case "tagih":
                mUrl = "PaginationMsLocationTagihan";
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
                if (mKey == "tagih") {
                    $("#containerTagihan").html(data);
                } else if (mKey == "penjamin") {
                    $("#containerPenjamin").html(data);
                } else if (mKey == "spouse") {
                    $("#containerPasangan").html(data);
                } else if (mKey == "corp") {
                    $("#containercorp").html(data);
                } else {
                    $("#containerLocation").html(data);
                }
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup Location. Error " + d);
            }
        });
    }
}

var ref_msloc_paging = {
    handle_btn: async function (pageIndex, searchTerm, mKey) {
        var mUrl = "";
        console.log(mKey);
        switch (mKey) {
            case "ref1":
                mUrl = "PaginationMsLocationRef1";
                break;
            case "ref2":
                mUrl = "PaginationMsLocationRef2";
                break;
            case "ref3":
                mUrl = "PaginationMsLocationRef3";
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
                console.log(data);
                if (mKey == "ref1") {
                    $("#containerReferensi").html(data);
                } else if (mKey == "ref2") {
                    $("#containerReferensi2").html(data);
                } else if (mKey == "ref3") {
                    $("#containerReferensi3").html(data);
                }
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup Location. Error " + d);
            }
        });
    }
}

var msbank_paging = {
    //handle button Search, GO, Previous, Next in Paging List
    handle_btn: async function (pageIndex, searchTerm) {
        var params = {
            pageIndex: pageIndex,
            searchTerm: searchTerm
        }
        $.ajax({
            url: "PaginationMsBank",
            type: "POST",
            data: params,
            success: function (data) {
                $("#containerBank").html(data);
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup Bank. Error " + d);
            }
        });
    }
}

var nik_paging = {
    //handle button Search, GO, Previous, Next in Paging List
    handle_btn: async function (pageIndex, searchTerm) {
        var params = {
            employeeName: searchTerm,
            pageIndex: pageIndex
        }
        $.ajax({
            url: "PaginationNIKKonsumen",
            type: "POST",
            data: params,
            success: function (data) {
                $("#containerNik").html(data);
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup NIK FU Konsumen. Error " + d);
            }
        });
    }
}

var pooling_paging = {
    //handle button Search, GO, Previous, Next in Paging List
    handle_btn: async function (pageIndex, searchTerm, tipeOrder) {
        var params = {
            tipeOrder: tipeOrder,
            pageIndex: pageIndex,
            searchTerm: searchTerm
        }
        $.ajax({
            url: "PaginationPoolingOrder",
            type: "POST",
            data: params,
            success: function (data) {
                $("#containerOrder").html(data);
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup Bank. Error " + d);
            }
        });
    }
}

var npp_paging = {
    //handle button Search, GO, Previous, Next in Paging List
    handle_btn: async function (pageIndex, searchTerm, lesseeId) {
        var params = {
            lesseeId: lesseeId,
            pageIndex: pageIndex,
            searchTerm: searchTerm
        }
        $.ajax({
            url: "PaginationAgreementOld",
            type: "POST",
            data: params,
            success: function (data) {
                $("#containerNppLama").html(data);
            },
            error: function (error) {
                var d = error;
                Common.Alert.Error("Failed to show Lookup NPP Lama. Error " + d);
            }
        });
    }
}