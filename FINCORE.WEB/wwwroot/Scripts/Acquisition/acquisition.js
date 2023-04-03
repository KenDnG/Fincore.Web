jQuery(document).ready(function () {
});

var OnActionPost = function (url, params) {
    var formElement = document.createElement("form");
    formElement.setAttribute("method", "post");
    formElement.setAttribute("action", url);
    formElement.setAttribute("target", "_parent");

    for (param in params) {
        var hiddenField = document.createElement("input");
        hiddenField.setAttribute("name", param);
        hiddenField.setAttribute("value", params[param]);
        formElement.appendChild(hiddenField);
    }

    document.body.appendChild(formElement);
    formElement.submit();
}

var acquisition_load = {
    init: function () {
    },
    open_cm_confirm: function (poNo, creditId, item) {
        swal({
            title: "Open CM",
            text: "Anda yakin ingin melakukan Open CM pada: <b>" + creditId + "</b> ?",
            type: "warning",
            html: true,
            showCancelButton: true,
            allowOutsideClick: false,
            cancelButtonClass: 'btn-secondary',
            cancelButtonText: 'Tidak',
            confirmButtonText: 'Ya, Open CM',
            confirmButtonClass: "btn-warning"
        }, function (isConfirm) {
            if (isConfirm) {
                acquisition_load.open_cm(poNo, creditId, item);
            }
        });
    },
    open_cm: function (poNo, creditId, itemId) {
        var params = {
            poNo: poNo,
            creditId: creditId,
            itemId: itemId
        }
        $.ajax({
            url: "OpenCM",
            type: "POST",
            data: params,
            success: function (data) {
                if (data.actionUrl == "") {
                    Common.Alert.Error("Failed to Open CM");
                } else {
                    //var urlTo = data;
                    //window.location.href = urlTo;

                    OnActionPost(data.actionUrl, data.parameters);
                }
            },
            error: function (error) {
                Common.Alert.Error("Failed to Open CM");
            }
        });
    },
    AcquisitionBikePageSearchTerm: function () {
        var params = {
            searchTerm1: $("#ddlpilihsumberorder").val(),
            searchTerm2: $("#ddlpilihstatus_ro").val()
        }
        $.ajax({
            url: "IndexMotor",
            type: "POST",
            data: params,
            success: function (data) {
            },
            error: function (error) {
            }
        });
    }
}