jQuery(document).ready(function () {
});

var login_load = {
    signin: function () {
        if ($('#txtUserDomain').val() == '') {
            Common.Alert.Info("Silahkan masukkan user domain.");
            $('#txtUserDomain').focus();
            return false;
        }
        if ($('#txtUserPassword').val() == '') {
            Common.Alert.Info("Password tidak boleh kosong");
            $('#txtUserPassword').focus();
            return false;
        }
        if ($('#selCompany').val() == '0') {
            Common.Alert.Info("Pilih perusahaan");
            $('#selCompany').focus();
            return false;
        }
        var dto =
        {
            Username: $('#txtUserDomain').val(),
            Password: $('#txtUserPassword').val(),
            CompanyId: $('#selCompany').val(),
        };
        $("#ibtnNext").addClass("fa-spinner fa-spin");

        var company = $("#selCompany").val();

        $("#imgCompanyLogoMAF").hide();
        $("#imgCompanyLogoMCF").hide();

        if (company == "2") {
            $("#imgCompanyLogoMAF").show();
        }
        else if (company == "3") {
            $("#imgCompanyLogoMCF").show();
        }

        $.ajax({
            type: "POST",
            url: "/Login/SignIn",
            data: JSON.stringify(dto),
            datatype: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.code == 200) {
                    $('#SelBranch').empty().append($("<option />").val('0').text('Pilih Branch Office'));
                    $("#ibtnNext").removeClass("fa-spinner fa-spin");
                    if (result.data.length == 0) {
                        Common.Alert.Warning("User tidak terdaftar di semua cabang.");
                    }
                    else {
                        $("#ibtnNext").removeClass("fa-spinner fa-spin");
                        $(".login-box").toggleClass('flipped')
                        $.each(result.data, function (key, value) {
                            $("#SelBranch").append($("<option />").val(value.branch_id).text(value.branch_id + " - " + value.branch_name));
                        })

                        if (result.data.length == 1) {
                            document.getElementById("SelBranch").selectedIndex = "1";
                            $("#SelBranch").attr('disabled', 'disabled')
                        }

                        $('#SelBranch').trigger('change');
                    }
                }
                else {
                    $("#ibtnNext").removeClass("fa-spinner fa-spin");
                    alert(result.message);
                }
            },
            error: function (textStatus, errorThrown) {
                $.unblockUI();
                alert('Failed');
            }
        });
    },
    btnBranchClick: function (branch_id, branch_name) {
        if ($('#SelBranch').val() == '0') {
            alert('Pilih branch.');
            $('#SelBranch').focus();
            return false;
        }
        var url = '/Home/Index?branch_id=' + $('#SelBranch').val() + '&branch_name=' + $("#SelBranch option:selected").text();
        window.location.href = url;
    },
    btnLogout: function () {
        if ($('#SelBranch').val() == '0') {
            alert('Pilih branch.');
            $('#SelBranch').focus();
            return false;
        }
        var url = '/Home/Index?branch_id=' + $('#SelBranch').val() + '&branch_name=' + $("#SelBranch option:selected").text();
        window.location.href = url;
    }
}