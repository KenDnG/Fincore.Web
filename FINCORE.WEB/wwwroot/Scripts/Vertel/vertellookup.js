jQuery(document).ready(function () {
    vertellookup_load.init();
});

var vertellookup_load = {
    init: function () {
        this.next();
        this.prev();
        this.search();
        //this.LoadModal();
    },
    next: function () {
        $("#btnnext").click(function () {
            txsearch = $("#txsearch").val();
            //console.log
            var nextPage = parseInt($("#txpageslookup").val()) + 1;
            var dto = {
                pageIndex: nextPage,
                searchTerm: txSearch
            };

            $.ajax({
                type: "POST",
                url: "Vertel/VertelLookUp",
                data: dto,
                success: function (res) {
                    $("#exampleModalLong").find(".modal-body").html(res);
                    $("#exampleModalLong").modal({
                        backdrop: false
                    });
                }
            });
        })
    },
    closeModal: function () {
        $('#exampleModalLong').modal('hide');
    },
    SelectAndClose: function (CMNo, CustomerName, Address, Phone, ApprovedDate, Tipe, Installment, Tenor, DPSetor, NamaStnk, NamaPasanganStnk) {
        $("#InputCMNo").val(CMNo);
        $("#InputCustomerName").val(String(CustomerName));
        $("#InputAddress").val(Address);
        $("#InputPhone").val(Phone);
        $("#dtTglTerimaPinjaman").val(ApprovedDate);
        $("#InputTipeKendaraan").val(Tipe);
        $("#InputAngsuran").val(Installment);
        $("#InputTenor").val(Tenor);
        $("#InputDP").val(DPSetor);
        $("#InputNamaSTNK").val(NamaStnk);
        $("#InputNamaPasangan").val(NamaPasanganStnk);

        this.closeModal();
    },
    prev: function () {
        $("#btnprev").click(function () {
            txSearch = $("#txsearch").val();
            var prevPage = parseInt($("#txpages").val()) - 1;
            var dto = {
                pageIndex: nextPage,
                searchTerm: txSearch
            };

            $.ajax({
                type: "POST",
                url: "Vertel/VertelLookUp",
                data: dto,
                success: function (res) {
                    $("#exampleModalLong").find(".modal-body").html(res);
                    $("#exampleModalLong").modal({
                        backdrop: false
                    });
                }
            });
        })
    },
    search: function () {
        $("#btnsearch").click(function () {
            var currentPage = $("#txpages").val();
            txSearch = $("#txsearch").val();
            var dto = {
                pageIndex: nextPage,
                searchTerm: txSearch
            };

            $.ajax({
                type: "POST",
                url: "Vertel/VertelLookUp",
                data: dto,
                success: function (res) {
                    $("#exampleModalLong").find(".modal-body").html(res);
                    $("#exampleModalLong").modal({
                        backdrop: false
                    });
                }
            });
        })
    }
    //,
    //LoadModal: function () {
    //    $("#btnLoadModal").click(function () {
    //        $.ajax({
    //            url: "VertelLookUp",
    //            data: $(this).serialize(),
    //            type: "POST",
    //            success: function (data) {
    //                $('#exampleModalLong').find('.modal-body').html(data);
    //                $('#exampleModalLong').modal({
    //                    backdrop: false
    //                });
    //            },
    //            error: function (err) {
    //            }
    //        })
    //    })
    //}
}