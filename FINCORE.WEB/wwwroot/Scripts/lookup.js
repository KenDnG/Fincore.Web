jQuery(document).ready(function () {
});
var LookupDialog = {
    showLookup: function (controllerName, actionName, whereClouse) {
        this.getData(controllerName, actionName, whereClouse);
        //$("#prtDialog").modal({
        //    backdrop: "static"
        //});
        //var modal = document.getElementById("prtDialog");
        //modal.style.display = "block";
    },
    closeLookup: function () {
        var modal = document.getElementById("prtDialog");
        var span = document.getElementsByClassName("close")[0];
        span.onclick = function () {
            modal.style.display = "none";
        }
    },
    getData: function (controllerName, actionName) {
        var params = {
        }
        $.ajax({
            url: "/" + controllerName + "/" + actionName + "",
            type: "POST",
            success: function (data) {
                $("#divPartialModal").html(data);
                $("#prtDialog").modal({
                    backdrop: "static",
                    keyboard: false
                });
            },
            error: function (textStatus, errorThrown) {
                $("#divPartialModal").html(textStatus.responseText);
                //$("#prtDialog").modal({
                //    backdrop: "static",
                //    keyboard: false
                //});
                $("#prtDialog").modal("show");
            }
        })
    }
}