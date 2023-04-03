jQuery(document).ready(function () {
  cas_load.init();
  $(
    "#tanggallahirnasabah, #tanggalaktaperusahaan, #tanggalaktaterakhir, #tanggalterbitsiup, #tanggaljatuhtemposiup, #tanggalterbittdp, #tanggaljatuhtempotdp, #tanggalterbitkelengkapan, #tanggaljatuhtempokelengkapan"
  ).datepicker({
    format: "dd/mm/yyyy",
    autoclose: true,
    todayHighlight: true,
  });
  $("#btnshowbank").click(function () {
    cas_lookup.msbank();
  });
  $("#btnshoworderid").click(function () {
    cas_lookup.poolingorder();
  });
  $("#btnshownik_sr1").click(function () {
    cas_lookup.Nik(this.id);
  });
  $("#btnshownik_sr4").click(function () {
    cas_lookup.Nik(this.id);
  });

  //Setup responsiove UI on Mobile
  const pnlHeader = document.getElementById("CASheader_more");
  pnlHeader?.addEventListener("click", () => {
    cas_load.changeClass_CASHeader();
  });
  const pnlPembatalan = document.getElementById("Pembatalan_more");
  pnlPembatalan?.addEventListener("click", () => {
    cas_load.changeClass_Pembatalan();
  });
  const pnlReferensi = document.getElementById("Referensi_more");
  pnlReferensi?.addEventListener("click", () => {
    cas_load.changeClass_Referensi();
  });
  const pnlTagihan = document.getElementById("Tagihan_more");
  pnlTagihan?.addEventListener("click", () => {
    cas_load.changeClass_Tagihan();
  });
  const pnlPenjamin = document.getElementById("Penjamin_more");
  pnlPenjamin?.addEventListener("click", () => {
    cas_load.changeClass_Penjamin();
  });
  const pnlPasangan = document.getElementById("Pasangan_more");
  pnlPasangan?.addEventListener("click", () => {
    cas_load.changeclass_Pasangan();
  });
  const pnlRumah = document.getElementById("Rumah_more");
  pnlRumah?.addEventListener("click", () => {
    cas_load.changeClass_Rumah();
  });
  const pnlTanggungan = document.getElementById("Tanggungan_more");
  pnlTanggungan?.addEventListener("click", () => {
    cas_load.changeClass_Tanggungan();
  });
  const pnlPenghasilan = document.getElementById("Penghasilan_more");
  pnlPenghasilan?.addEventListener("click", () => {
    cas_load.changeClass_Penghasilan();
  });
  const pnlKarakter = document.getElementById("Karakter_more");
  pnlKarakter?.addEventListener("click", () => {
    cas_load.changeClass_Karater();
  });
  const pnlBU = document.getElementById("BadanUsaha_more");
  pnlBU?.addEventListener("click", () => {
    cas_load.changeClass_BadanUsaha();
  });
  const pnlIdentitas = document.getElementById("Identitas_more");
  pnlIdentitas?.addEventListener("click", () => {
    cas_load.changeClass_Identitas();
  });
  const pnlNasabah = document.getElementById("Nasabah_more");
  pnlNasabah?.addEventListener("click", () => {
    cas_load.changeClass_Nasabah();
  });
  const pnlAnalisa = document.getElementById("Analisa_more");
  pnlAnalisa?.addEventListener("click", () => {
    cas_load.changeClass_Analisa();
  });
  //Setup responsiove UI on Mobile

  //Remove auto-multiple in Select/dropdown when type data array
  $("#ddlkewarganegaraan").removeAttr("multiple");
  $("#ddlvisiting").removeAttr("multiple");
});
var cas_load = {
  init: function () {
    /*document.getElementById('IANoCheck').checked = true;*/
    this.HandleBlacklistBtn();
    this.TogglePanel();
    this.CheckStatusPernikahan(); //trigger dropdown when ViewEdit
    this.CheckSeumurHidup();
    this.CheckTipeKonsumen();
    this.CustomerROCheck();
    this.CheckSumberReferensi();
    this.CheckGenderByNIK();
    this.InstantApprovalCheck();
    this.DataPenjaminCheck();
    this.DataReferensiCheck();
    this.OtherIndustryDesc();

    this.InitialConvertMoney();

    //hardcode temp
    if ($("#txtcreditid").val() != "") {
      this.CheckFileDokumen();
    }

    $("#btnShowApuppt").click(function () {
      cas_load.CheckApuppt();
    });
    $("#btnsavechange").click(function () {});

    var penghasilanUtamaNasabah = $("#txpenghasilanutama").val();
    if (penghasilanUtamaNasabah == "0" || penghasilanUtamaNasabah == "") {
      $("#lblpenghasilanutama").html("Penghasilan Utama tidak boleh 0");
    }
    $("#btnNext").click(function () {
      if (
        $("#txpenghasilanutama").val() == "0" ||
        $("#txpenghasilanutama").val() == ""
      ) {
        Common.Alert.Info(
          "Mohon masukan Penghasilan Utama nasabah pada panel Data Penghasilan. Silahkan cek dan coba kembali."
        );
        return false;
      }
    });
  },
  setup_select2: function () {
    $("#ddlLocRef1").select2({ width: "100%" });
  },
  CheckNPWPLength: function (value) {
    if (value != "") {
      if (value.length <= 14) {
        Common.Alert.Warning(
          "Mohon masukan No NPWP dengan benar. Silahkan check dan coba kembali."
        );
      }
    }
  },
  InitialConvertMoney: function () {
    var PenghasilanUtama = $("#txpenghasilanutama").val().replaceAll(".", "");
    $("#txpenghasilanutama").val(cas_load.FormatNumberMoney(PenghasilanUtama));
    $("#txpenghasilanutama").keyup(function (e) {
      $("#txpenghasilanutama").val(
        cas_load.FormatNumberMoney(
          $("#txpenghasilanutama").val().replaceAll(".", "")
        )
      );
      if (e.target.value == 0) {
        $("#lblpenghasilanutama").html("Penghasilan Utama tidak boleh 0");
        Common.Alert.Info(
          "Mohon masukan Penghasilan Utama nasabah pada panel Data Penghasilan. Silahkan cek dan coba kembali."
        );
        return false;
      } else {
        $("#lblpenghasilanutama").html("");
      }
    });

    $("#txpenghasilantambahan").val(
      cas_load.FormatNumberMoney(
        $("#txpenghasilantambahan").val().replaceAll(".", "")
      )
    );
    $("#txpenghasilantambahan").keyup(function () {
      $("#txpenghasilantambahan").val(
        cas_load.FormatNumberMoney(
          $("#txpenghasilantambahan").val().replaceAll(".", "")
        )
      );
    });

    $("#txtbiayapengeluaranRT").val(
      cas_load.FormatNumberMoney(
        $("#txtbiayapengeluaranRT").val().replaceAll(".", "")
      )
    );
    $("#txtbiayapengeluaranRT").keyup(function () {
      $("#txtbiayapengeluaranRT").val(
        cas_load.FormatNumberMoney(
          $("#txtbiayapengeluaranRT").val().replaceAll(".", "")
        )
      );
    });

    $("#txbiayapendidikan").val(
      cas_load.FormatNumberMoney(
        $("#txbiayapendidikan").val().replaceAll(".", "")
      )
    );
    $("#txbiayapendidikan").keyup(function () {
      $("#txbiayapendidikan").val(
        cas_load.FormatNumberMoney(
          $("#txbiayapendidikan").val().replaceAll(".", "")
        )
      );
    });

    $("#txbiayakesehatan").val(
      cas_load.FormatNumberMoney(
        $("#txbiayakesehatan").val().replaceAll(".", "")
      )
    );
    $("#txbiayakesehatan").keyup(function () {
      $("#txbiayakesehatan").val(
        cas_load.FormatNumberMoney(
          $("#txbiayakesehatan").val().replaceAll(".", "")
        )
      );
    });

    $("#txpenghasilanutama_pasangan").val(
      cas_load.FormatNumberMoney(
        $("#txpenghasilanutama_pasangan").val().replaceAll(".", "")
      )
    );
    $("#txpenghasilanutama_pasangan").keyup(function () {
      $("#txpenghasilanutama_pasangan").val(
        cas_load.FormatNumberMoney(
          $("#txpenghasilanutama_pasangan").val().replaceAll(".", "")
        )
      );
    });

    $("#txpenghasilantambahan_pasangan").val(
      cas_load.FormatNumberMoney(
        $("#txpenghasilantambahan_pasangan").val().replaceAll(".", "")
      )
    );
    $("#txpenghasilantambahan_pasangan").keyup(function () {
      $("#txpenghasilantambahan_pasangan").val(
        cas_load.FormatNumberMoney(
          $("#txpenghasilantambahan_pasangan").val().replaceAll(".", "")
        )
      );
    });

    $("#txtcicilanlain").val(
      cas_load.FormatNumberMoney($("#txtcicilanlain").val().replaceAll(".", ""))
    );
    $("#txtcicilanlain").keyup(function () {
      $("#txtcicilanlain").val(
        cas_load.FormatNumberMoney(
          $("#txtcicilanlain").val().replaceAll(".", "")
        )
      );
    });
  },
  FormatNumberMoney: function (value) {
    if (value != undefined) {
      let nf = new Intl.NumberFormat("id-ID");

      if (nf.format(value) == "NaN") {
        return "";
      } else {
        return nf.format(value);
      }
    }
  },
  data_location_references: function () {
    var params = {
      searchTerm: "",
    };
    $.ajax({
      type: "POST",
      url: "/CAS/LocationReferenceData",
      data: params,
      datatype: "json",
      contentType: "application/json; charset=utf-8",
      success: function (results) {
        var d = results;
      },
      error: function (textStatus, errorThrown) {
        var err = textStatus;
      },
    });
  },
  IscheckSeumurHidup: function () {
    if (($("#chkSeumurHidup").isChecked = true)) {
      $("#tanggalberlakuktp").prop("readonly", true);
    } else if (($("#chkSeumurHidup").isChecked = false)) {
      $("#tanggalberlakuktp").val("");
      $("#tanggalberlakuktp").prop("readonly", false);
    }
  },
  save_draft: function () {
    //TO DO: Logic for save if you want to use JQuery Ajax.
  },
  DataPenjaminCheck: async function () {
    if (document.getElementById("PenjaminYesCheck").checked) {
      document.getElementById("PenjaminYes").style.display = "block";
      document.getElementById("PenjaminYes1").style.display = "block";
      $(".type-jamin").prop("required", true);
    } else {
      document.getElementById("PenjaminYes").style.display = "none";
      document.getElementById("PenjaminYes1").style.display = "none";
      $(".type-jamin").prop("required", false);
    }
  },
  DataReferensiCheck: async function () {
    if (document.getElementById("ReferensiYesCheck").checked) {
      var mNik = $("#txKTP").val();
      if (mNik == "") {
        Common.Alert.Warning("Isi Identitas No KTP terlebih dahulu !");
        $("#ReferensiYesCheck").prop("checked", false);
        $("#ReferensiNoCheck").prop("checked", true);
      } else {
        document.getElementById("ReferensiYes").style.display = "block";
        document.getElementById("ReferensiYes1").style.display = "block";
        //this.GetColumnDataReference();
      }
      //document.getElementById('ReferensiYes').style.display = 'block';
      //document.getElementById('ReferensiYes1').style.display = 'block';
    } else {
      document.getElementById("ReferensiYes").style.display = "none";
      document.getElementById("ReferensiYes1").style.display = "none";
    }
  },
  CheckStatusPernikahan: async function () {
    var dropdown = document.getElementById("ddlStatusPernikahan");
    var current_value = dropdown.options[dropdown.selectedIndex].text;
    if (current_value == "Menikah") {
      $("#rbMerried").attr("checked", true);
      $("#rbMerried").prop("value", true);
      document.getElementById("StatusMenikah").style.display = "block";
      document.getElementById("StatusMenikah1").style.display = "block";
      $(".type-merried").prop("required", true);
    } else {
      $("#rbMerried").attr("checked", false);
      $("#rbMerried").prop("value", false);
      document.getElementById("StatusMenikah").style.display = "none";
      document.getElementById("StatusMenikah1").style.display = "none";
      $(".type-merried").prop("required", false);
    }
  },
  CustomerROCheck: function () {
    document.getElementById("divAlasanRO").style.display = "none";
    $("#repeat_order_reason").removeAttr("required");

    if (document.getElementById("ROyesCheck").checked) {
      document.getElementById("ROYes").style.display = "block";
      $(".type-ro").prop("required", true);

      document.getElementById("divAlasanRO").style.display = "block";
      $("#repeat_order_reason").attr("required", true);
    } else {
      document.getElementById("ROYes").style.display = "none";
      document.getElementById("IAYes").style.display = "none";
      document.getElementById("IANo").style.display = "none";
      $(".type-ro").prop("required", false);

      $("#repeat_order_reason").val("");
    }
  },
  CheckTipeKonsumen: async function () {
    var dropdown = document.getElementById("ddltipekonsumen");
    var current_value = dropdown.options[dropdown.selectedIndex].value;

    $("#txofficename_financial").removeAttr("required");

    if (current_value == "P") {
      document.getElementById("Perseorangan").style.display = "block";
      document.getElementById("Perseorangan1").style.display = "block";
      document.getElementById("pnlBtnApuppt").style.display = "block";
      $(".type-perorang").prop("required", true);
    } else {
      document.getElementById("Perseorangan").style.display = "none";
      document.getElementById("Perseorangan1").style.display = "none";
      document.getElementById("pnlBtnApuppt").style.display = "none";
      $(".type-perorang").prop("required", false);
    }
    if (current_value == "C") {
      document.getElementById("Corporate").style.display = "block";
      document.getElementById("Corporate1").style.display = "block";
      document.getElementById("Corporate2").style.display = "block";
      $(".type-corporate").prop("required", true);
      $("#txofficename_financial").attr("required", true);
    } else {
      document.getElementById("Corporate").style.display = "none";
      document.getElementById("Corporate1").style.display = "none";
      document.getElementById("Corporate2").style.display = "none";
      $(".type-corporate").prop("required", false);
    }
  },
  CheckOrderIdByCreditSource: function () {
    var btn = document.getElementById("btnshoworderid");
    var dropdown = document.getElementById("ddlcreditsource");
    var current_value = dropdown.options[dropdown.selectedIndex].value;
    if (current_value == "3") {
      btn.disabled = false;
    } else {
      btn.disabled = true;
      $("#txtorderId").val("");
    }
  },
  CheckSumberReferensi: async function () {
    var dropdown = document.getElementById("ddlsumberreferensi");
    var current_value = dropdown.options[dropdown.selectedIndex].value;

    if (current_value == "2") {
      document.getElementById("SR1").style.display = "block";
    } else {
      document.getElementById("SR1").style.display = "none";
    }

    if (current_value == "3" || current_value == "4") {
      document.getElementById("SR2").style.display = "block";
    } else {
      document.getElementById("SR2").style.display = "none";
    }

    if (current_value == "6") {
      document.getElementById("SR4").style.display = "block";
    } else {
      document.getElementById("SR4").style.display = "none";
    }
  },
  changeClass_CASHeader: function () {
    var content = document.getElementById("CASheader");

    var nasabah = document.getElementById("CASheader_more");
    content.classList.toggle("show");

    if (content.classList.contains("CASheaderShow")) {
      nasabah.innerHTML = "CAS Header";
    } else {
      nasabah.innerHTML = "CAS Header";
    }
  },
  changeClass_Pembatalan: function () {
    var content = document.getElementById("Pembatalan");

    var identitas = document.getElementById("Pembatalan_more");
    content.classList.toggle("show");

    if (content.classList.contains("Show")) {
      identitas.innerHTML = "Data Pembatalan";
    } else {
      identitas.innerHTML = "Data Pembatalan";
    }
  },
  changeClass_Tagihan: function () {
    var content = document.getElementById("Tagihan");

    var identitas = document.getElementById("Tagihan_more");
    content.classList.toggle("show");

    if (content.classList.contains("Show")) {
      identitas.innerHTML = "Data Pembayaran Tagihan";
    } else {
      identitas.innerHTML = "Data Pembayaran Tagihan";
    }
  },
  changeClass_Referensi: function () {
    var content = document.getElementById("Referensi");

    var identitas = document.getElementById("Referensi_more");
    content.classList.toggle("show");

    if (content.classList.contains("Show")) {
      identitas.innerHTML = "Data Referensi";
    } else {
      identitas.innerHTML = "Data Referensi";
    }
  },
  changeClass_Penjamin: function () {
    var content = document.getElementById("Penjamin");

    var identitas = document.getElementById("Penjamin_more");
    content.classList.toggle("show");

    if (content.classList.contains("PenjaminShow")) {
      identitas.innerHTML = "Data Penjamin";
    } else {
      identitas.innerHTML = "Data Penjamin";
    }
  },
  changeclass_Pasangan: function () {
    var content = document.getElementById("Pasangan");

    var identitas = document.getElementById("Pasangan_more");
    content.classList.toggle("show");

    if (content.classList.contains("PasanganShow")) {
      identitas.innerHTML = "Data Pasangan";
    } else {
      identitas.innerHTML = "Data Pasangan";
    }
  },
  changeClass_Rumah: function () {
    var content = document.getElementById("Rumah");

    var identitas = document.getElementById("Rumah_more");
    content.classList.toggle("show");

    if (content.classList.contains("RumahShow")) {
      identitas.innerHTML = "Data Kepemilikan Rumah";
    } else {
      identitas.innerHTML = "Data Kepemilikan Rumah";
    }
  },
  changeClass_Tanggungan: function () {
    var content = document.getElementById("Tanggungan");

    var identitas = document.getElementById("Tanggungan_more");
    content.classList.toggle("show");

    if (content.classList.contains("TanggunganShow")) {
      identitas.innerHTML = "Data Biaya & Tanggungan";
    } else {
      identitas.innerHTML = "Data Biaya & Tanggungan";
    }
  },
  changeClass_Penghasilan: function () {
    var content = document.getElementById("Penghasilan");

    var identitas = document.getElementById("Penghasilan_more");
    content.classList.toggle("show");

    if (content.classList.contains("PenghasilanShow")) {
      identitas.innerHTML = "Data Penghasilan";
    } else {
      identitas.innerHTML = "Data Penghasilan";
    }
  },
  changeClass_Karater: function () {
    var content = document.getElementById("Karakter");

    var identitas = document.getElementById("Karakter_more");
    content.classList.toggle("show");

    if (content.classList.contains("KarakterShow")) {
      identitas.innerHTML = "Data Karakter";
    } else {
      identitas.innerHTML = "Data Karakter";
    }
  },
  changeClass_BadanUsaha: function () {
    var content = document.getElementById("BadanUsaha");

    var identitas = document.getElementById("BadanUsaha_more");
    content.classList.toggle("show");

    if (content.classList.contains("BadanUsahaShow")) {
      identitas.innerHTML = "Badan Usaha";
    } else {
      identitas.innerHTML = "Badan Usaha";
    }
  },
  changeClass_Identitas: function () {
    var content = document.getElementById("Identitas");

    var identitas = document.getElementById("Identitas_more");
    content.classList.toggle("show");

    if (content.classList.contains("IdentitasShow")) {
      identitas.innerHTML = "Data Identitas";
    } else {
      identitas.innerHTML = "Data Identitas";
    }
  },
  changeClass_Nasabah: function () {
    var content = document.getElementById("Nasabah");

    var nasabah = document.getElementById("Nasabah_more");
    content.classList.toggle("show");

    if (content.classList.contains("NasabahShow")) {
      nasabah.innerHTML = "Data Nasabah";
    } else {
      nasabah.innerHTML = "Data Nasabah";
    }
  },
  changeClass_Analisa: function () {
    var content = document.getElementById("Analisa");

    var identitas = document.getElementById("Analisa_more");
    content.classList.toggle("show");

    if (content.classList.contains("Show")) {
      identitas.innerHTML = "Analisa & Kesimpulan (AO)";
    } else {
      identitas.innerHTML = "Analisa & Kesimpulan (AO)";
    }
  },
  InstantApprovalCheck: async function () {
    if (document.getElementById("IAYesCheck").checked) {
      document.getElementById("IAYes").style.display = "block";
      document.getElementById("IAYes1").style.display = "block";
      document.getElementById("IANo").style.display = "none";
      document.getElementById("IANo1").style.display = "none";
    } else if (document.getElementById("IANoCheck").checked) {
      document.getElementById("IAYes").style.display = "none";
      document.getElementById("IAYes1").style.display = "none";
      document.getElementById("IANo").style.display = "block";
      document.getElementById("IANo1").style.display = "block";
    }
  },
  TogglePanel: function () {
    $("btnCAS").click(function () {
      $("CASheader").toggle();
    });

    $("btnIdentitas").click(function () {
      $("Identitas").toggle();
    });

    $("btnBadanUsaha").click(function () {
      $("BadanUsaha").toggle();
    });

    $("btnRepeatOrder").click(function () {
      $("RepeatOrder").toggle();
    });

    $("btnKarakter").click(function () {
      $("Karakter").toggle();
    });

    $("btnPenghasilan").click(function () {
      $("Penghasilan").toggle();
    });

    $("btnTanggungan").click(function () {
      $("Tanggungan").toggle();
    });

    $("btnRumah").click(function () {
      $("Rumah").toggle();
    });

    $("btnPasangan").click(function () {
      $("Pasangan").toggle();
    });

    $("btnPenjamin").click(function () {
      $("Penjamin").toggle();
    });

    $("btnReferensi").click(function () {
      $("Referensi").toggle();
    });

    $("btnTagihan").click(function () {
      $("Tagihan").toggle();
    });

    $("btnPembatalan").click(function () {
      $("Pembatalan").toggle();
    });

    $("btnAnalisa").click(function () {
      $("Analisa").toggle();
    });
  },
  OtherIndustryDesc: function () {
    var dropdown = document.getElementById("ddlbidangusaha_c");
    var current_value = dropdown.options[dropdown.selectedIndex].text;
    if (current_value == "Lainnya") {
      document.getElementById("pnlOtherIndustry").style.display = "block";
      $("#txotherindustrydesc").val("");
      $("#txotherindustrydesc").attr("required", true);
    } else {
      document.getElementById("pnlOtherIndustry").style.display = "none";
      $("#txotherindustrydesc").removeAttr("required");
    }
  },
  CheckSeumurHidup: function () {
    checkBox = document
      .getElementById("chkSeumurHidup")
      .addEventListener("click", (event) => {
        if (event.target.checked) {
          $("#tanggalberlakuktp").prop("readonly", true);
        } else {
          $("#tanggalberlakuktp").val("");
          $("#tanggalberlakuktp").prop("readonly", false);
        }
      });
  },
  CheckGenderByNIK: function () {
    $("#txKTP").keyup(function () {
      if (this.value.length === 16) {
        var params = {
          nik: this.value,
        };
        $.ajax({
          url: "GetGenderByNIK",
          type: "GET",
          data: params,
          success: function (data) {
            var mGender = data;
            if (mGender == "P") {
              $("#rbPerempuan").prop("checked", true);
              $("#rbLaki").prop("checked", false);
            } else if (mGender == "L") {
              $("#rbLaki").prop("checked", true);
              $("#rbPerempuan").prop("checked", false);
            } else {
              $("#rbLaki").prop("checked", false);
              $("#rbPerempuan").prop("checked", false);
            }
          },
          error: function (error) {},
        });
      }
    });
  },
  LoadIdentityType: function (custType) {
    var params = {
      customerType: custType,
    };
    $.ajax({
      url: "IdentityTypeByCustomer",
      type: "GET",
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      data: params,
      cache: false,
    })
      .done(function (data, textStatus, jqXHR) {
        var xhtml = "";
        xhtml +=
          '<option selected="selected" disabled="disabled" value="">Pilih Sumber CAS</option>';
        for (var i = 0; i < data.length; i++) {
          xhtml +=
            '<option value="' +
            data[i].credit_source_id +
            '">' +
            data[i].credit_source_desc +
            "</option>";
        }
        $("#ddlcreditsource").html(xhtml);
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        var err = textStatus;
        Common.Alert.Error(err);
      });
  },
  CheckApuppt: function () {
    var mKtp = $("#txKTP").val();
    var mKtp1 = $("#txKTP").val();
    var mKtp2 = $("#txKTP").val();
    var nameCustomer = $("#txnamanasabah_p").val();
    var mBirthDate = $("#tanggallahir_p").val();
    var mBirthPlace = $("#txtempatlahir_p").val();
    var mAlamat = $("#txtalamatnasabah").val();
    var mAccupation = $("#txposition_financial").val();
    if (
      mKtp == "" ||
      nameCustomer == "" ||
      mBirthDate == "" ||
      mBirthPlace == "" ||
      mAlamat == "" ||
      mAccupation == ""
    ) {
      Common.Alert.Warning("Mohon lengkapi data terlebih dahulu !");
      return;
    }
    var items = {
      ktpNo: mKtp,
      ktpNo1: mKtp1,
      ktpNo2: mKtp2,
      name: nameCustomer,
      birthDate: mBirthDate,
      birthPlace: mBirthPlace,
      alamatDomisili: mAlamat,
      accupation: mAccupation,
    };
    var ldBtn = Ladda.create(document.getElementById("btnShowApuppt"));
    $.ajax({
      url: "CheckApuppt",
      type: "GET",
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      data: items,
      cache: false,
      beforeSend: function () {
        ldBtn.start();
      },
    })
      .done(function (data, textStatus, jqXHR) {
        ldBtn.stop();
        if (data[0].is_apuppt_result) {
          $("#hdn_isapuppt").val("1");
          document.getElementById("pnlApuppt").style.display = "block";
          Common.Alert.Info("Pengecekan selesai. Form APUPPT tersedia!");
        } else {
          $("#hdn_isapuppt").val("0");
          document.getElementById("pnlApuppt").style.display = "none";
          Common.Alert.Warning(
            "Pengecekan selesai. Form APUPPT TIDAK tersedia!"
          );
        }
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        ldBtn.stop();
        var err = textStatus;
        Common.Alert.Error(err);
      });
  },
  ShowPanelApuppt: function () {
    var x = document.getElementById("pnlApuppt");
    if (x.style.display === "none") {
      x.style.display = "block";
    } else {
      x.style.display = "none";
    }
  },
  CheckBlacklist: function (idx, elementShowAlert, itemid) {
    var lblMsg = document.getElementById(elementShowAlert);
    var params = {
      ktp: idx,
      itemId: itemid,
    };
    $.ajax({
      url: "CheckBlacklist",
      type: "GET",
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      data: params,
      cache: false,
    })
      .done(function (data, textStatus, jqXHR) {
        if (data.length > 0) {
          if (data[0].is_blacklist) {
            lblMsg.style.display = "block";
            lblMsg.innerHTML = data[0].message_error;
            lblMsg.style.color = "red";
            $("#txt_hidden_blacklist").val("true");
            Common.Alert.Warning(
              "Checking Blacklist Result: KTP " +
                idx +
                ", " +
                data[0].message_error
            );
          } else {
            lblMsg.style.display = "block";
            lblMsg.innerHTML = "Tidak Blacklist";
            lblMsg.style.color = "#38E54D";
            $("#txt_hidden_blacklist").val("false");
          }
        } else {
          lblMsg.style.display = "block";
          lblMsg.innerHTML = "Tidak Blacklist";
          lblMsg.style.color = "#38E54D";
          $("#txt_hidden_blacklist").val("false");
        }
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        var err = textStatus;
        Common.Alert.Error(err);
      });
  },
  HandleBlacklistBtn: function () {
    $("#btncheckBL_identity").click(function (e) {
      var itemId = e.target.attributes.value.value;
      var ktp = $("#txKTP").val();
      if (ktp == "" || ktp == null) {
        Common.Alert.Warning("Mohon masukan No KTP !");
      } else {
        cas_load.CheckBlacklist(ktp, "lblblacklist", itemId);
      }
    });
    $("#btncheckBL_pasangan").click(function (e) {
      var itemId = e.target.attributes.value.value;
      var ktp = $("#txKtpPasangan").val();
      if (ktp == null || ktp == null) {
        Common.Alert.Warning("Mohon masukan No Identitas Pasangan !");
      } else {
        cas_load.CheckBlacklist(ktp, "lblblacklist_pasangan", itemId);
      }
    });
    $("#btnCheckBL_penjamin").click(function (e) {
      var itemId = e.target.attributes.value.value;
      var ktp = $("#txtKtpPenjamin").val();
      if (ktp == null || ktp == "") {
        Common.Alert.Warning("Mohon masukan No Identitas Penjamin !");
      } else {
        cas_load.CheckBlacklist(ktp, "lblblacklist_penjamin", itemId);
      }
    });
  },
  GetPoolingByOrderId: function (orderId) {
    var params = {
      orderId: orderId,
    };
    $.ajax({
      url: "GetDataPoolingOrder",
      type: "GET",
      data: params,
      beforeSend: function () {},
      success: function (data) {
        $("#txKTP").val(data.ktp_no);
        $("#txtalamatnasabah").val(data.alamat_survey);
        $("#txtelephone").val(data.phone1);
        $("#txMobilePhone").val(data.phone2);
        $("#tanggallahir_p").val(data.birth_date);
        $("#ddlStatusPernikahan").val(data.marital_id);
        $("#ddlProfesi").val(data.profession_id);
        $("#ddlStatusPernikahan").val(data.marital_id);
        $("#ddlResidenceState").val(data.home_status_id);
        $("#ddlbuktikepemilikan").val(data.home_time_stay_id);
        $("#ddlBidangUsaha").val(data.industry_type_id);
        $("#txResidenceDistance").val(1); //default 1 KM
        $("#txnamanasabah_p").val(data.customer_name);
        if (data.marital_id == 2) {
          //menikah
          cas_load.CheckStatusPernikahan();
          $("#txtnamapasangan").val(data.spouse_name);
          $("#tanggallahirpasangan").val(data.spouse_birth_date);
          $("#txKtpPasangan").val(data.spouse_id_no);
          $("#txtnamapasangan").val(data.spouse_name);
        }
      },
      error: function (error, jqXHR) {
        var d = error;
        Common.Alert.Error("Failed to Get Data Pooling Order. Error " + d);
      },
    });
  },
  CheckPaymentPointPlan: function (selectedValueId, element) {
    var patternId = "ddlprior_planpay_";
    var idName = $(element).attr("id"); //exp: ddlprior_planpay_1

    var idNameLength = idName.replace(patternId, "");
    var findIdCount = document.getElementsByClassName("planpay").length;
    for (var i = 0; i < findIdCount; i++) {
      var mI = i + 1;
      if (idNameLength != mI) {
        var valueAlreadySelected = $("#" + patternId + mI).val();
        if (selectedValueId == valueAlreadySelected) {
          Common.Alert.Warning(
            "Pembayaran Angsuran tidak boleh sama dengan Skala Prioritas yang sudah terpilih !"
          );
          $("#" + idName).val("");
        }
      }
    }
  },
  CheckPaymentLoc: function (selectedValueId, element) {
    var patternId = "ddlprior_locpay_";
    var idName = $(element).attr("id"); //exp: ddlprior_locpay_1

    var idNameLength = idName.replace(patternId, "");
    var findIdCount = document.getElementsByClassName("locpay").length;
    for (var i = 0; i < findIdCount; i++) {
      var mI = i + 1;
      if (idNameLength != mI) {
        var valueAlreadySelected = $("#" + patternId + mI).val();
        if (selectedValueId == valueAlreadySelected) {
          Common.Alert.Warning(
            "Lokasi Pembayaran tidak boleh sama dengan Skala Prioritas yang sudah terpilih !"
          );
          $("#" + idName).val("");
        }
      }
    }
  },
  RemoveTagRequired: function () {
    //Remove and ignore Required tag when Add New Condition
    $("input").removeAttr("required");
    $("select").removeAttr("required");
    $("textarea").removeAttr("required");
  },
  SetAlamatTagihan: function () {
    var dropdown = document.getElementById("ddlmailtosource");
    var user_selected = dropdown.options[dropdown.selectedIndex].text;
    if (user_selected == "Nasabah") {
      document.getElementById("txmailtoaddress").value = $(
        "#txtalamatnasabah"
      ).val();
      //$("#txmailtoaddress").val();
      $("#txmailtotelepon").val($("#txtelephone").val());
      $("#txmailtolocationid").val($("#txlocationid").val());
      $("#txmailtolocationname").val($("#txlocationname").val());
      $("#txmailtoaddress").prop("readonly", true);
      $("#txmailtotelepon").prop("readonly", true);
    } else if (user_selected == "Perusahaan") {
      $("#txmailtoaddress").val($("#txofficeaddress_financial").val());
      $("#txmailtotelepon").val($("#txofficephone_financial").val());
      $("#txmailtolocationid").val($("#txofficelocationid_financial").val());
      $("#txmailtolocationname").val(
        $("#txofficelocationname_financial").val()
      );
      $("#txmailtoaddress").prop("readonly", true);
      $("#txmailtotelepon").prop("readonly", true);
    } else if (user_selected == "Pasangan") {
      document.getElementById("txmailtoaddress").value = $(
        "#txtalamat_pasangan"
      ).val();
      //$("#txmailtoaddress").val($("#txtalamat_pasangan").val());
      $("#txmailtotelepon").val($("#txttelp_pasangan").val());
      $("#txmailtolocationid").val($("#txlokpasanganid").val());
      $("#txmailtolocationname").val($("#txlokpasanganname").val());
      $("#txmailtoaddress").prop("readonly", true);
      $("#txmailtotelepon").prop("readonly", true);
    } else if (user_selected == "Penjamin") {
      document.getElementById("txmailtoaddress").value = $(
        "#txtalamat_penjamin"
      ).val();
      //$("#txmailtoaddress").val($("#txtalamat_penjamin").val());
      $("#txmailtotelepon").val("");
      $("#txmailtolocationid").val($("#txlokpenjaminid").val());
      $("#txmailtolocationname").val($("#txtlokasipenjamin").val());
      $("#txmailtoaddress").prop("readonly", true);
      $("#txmailtotelepon").prop("readonly", true);
    } else if (user_selected == "Referensi") {
      document.getElementById("txmailtoaddress").value = $(
        "#txtalamat_reference_0"
      ).val();
      //$("#txmailtoaddress").val($("#txtalamat_reference_0").val());
      $("#txmailtotelepon").val($("#txttelepon_reference_0").val());
      $("#txmailtolocationid").val($("#txlokref1id").val());
      $("#txmailtolocationname").val($("#txlokref1name").val());
      $("#txmailtoaddress").prop("readonly", true);
      $("#txmailtotelepon").prop("readonly", true);
    } else if (user_selected == "Lainnya") {
      //Lainnya
      //document.getElementById("txmailtoaddress").value = $("#txtalamatnasabah").val();*/
      $("#txmailtoaddress").val("");
      $("#txmailtoaddress").prop("readonly", false);
      $("#txmailtotelepon").prop("readonly", false);
      $("#txmailtotelepon").val("");
      $("#txmailtolocationid").val("");
      $("#txmailtolocationname").val("");
    }
  },
  GetColumnDataReference: function () {
    var mNik = $("#txKTP").val();
    var items = {
      nik: mNik,
    };
    $.ajax({
      url: "GetDataReferensi",
      type: "GET",
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      data: items,
      cache: false,
      beforeSend: function () {
        $("#lblmaxcol").text("Loading ...");
      },
    })
      .done(function (data, textStatus, jqXHR) {
        $("#lblmaxcol").text(
          "Anda diharuskan isi " + data + " Data Referensi."
        );
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        $("#lblmaxcol").text("Gagal memuat data. Silahkan coba kembali");
        var err = textStatus;
      });
  },
  CheckFileDokumen: function () {
    var items = {
      credit_id: $("#txtcreditid").val(),
    };
    $.ajax({
      url: "CheckFotoDokumenIsFound",
      type: "GET",
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      data: items,
      cache: false,
    })
      .done(function (data, textStatus, jqXHR) {
        if (data[0] == true && data[1] == true) {
          $("#dropdownMenuButton").prop("disabled", true);
        }
        $("#btnViewKtpIdentitas").prop("disabled", data[0]);
        $("#btnViewKK").prop("disabled", data[1]);
        $("#btnViewSlipGaji").prop("disabled", data[2]);
        $("#btnViewKtpPasangan").prop("disabled", data[3]);
        $("#btnViewPbbListrik").prop("disabled", data[4]);
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        var err = textStatus;
      });
  },
  PrintLahs: function (itemId) {
    var items = {
      creditId: $("#txtcreditid").val(),
      itemType: itemId,
    };
    var ldBtn = Ladda.create(document.getElementById("btnprintlahs"));
    $.ajax({
      url: "PrintLAHS",
      type: "GET",
      data: items,
      beforeSend: function () {
        ldBtn.start();
      },
    })
      .done(function (data, textStatus, jqXHR) {
        var d = data;
        ldBtn.stop();
      })
      .fail(function (jqXHR, textStatus, errorThrown) {
        var err = textStatus;
        ldBtn.stop();
      });
  },
};

var cas_lookup = {
  location: async function (mKey, mBtn) {
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
      pageIndex: 1,
      searchTerm: "",
    };
    $.ajax({
      url: mUrl,
      type: "POST",
      data: params,
      beforeSend: function () {
        $("#" + mBtn + "").prop("disabled", true);
        $("#i" + mBtn + "").addClass("fa-spinner fa-spin");
      },
      success: function (data) {
        if (mKey == "tagih") {
          $("#containerTagihan").html(data);
          $("#mdltagihan").modal({
            backdrop: false,
          });
        } else if (mKey == "penjamin") {
          $("#containerPenjamin").html(data);
          $("#mdlpenjamin").modal({
            backdrop: false,
          });
        } else if (mKey == "spouse") {
          $("#containerPasangan").html(data);
          $("#mdlpasangan").modal({
            backdrop: false,
          });
        } else if (mKey == "corp") {
          $("#containercorp").html(data);
          $("#mdlcorp").modal({
            backdrop: false,
          });
        } else {
          $("#containerLocation").html(data);
          $("#mdllocation").modal({
            backdrop: false,
          });
        }
        $("#" + mBtn + "").prop("disabled", false);
        $("#i" + mBtn + "").removeClass("fa-spinner fa-spin");
      },
      error: function (error) {
        $("#" + mBtn + "").prop("disabled", false);
        $("#i" + mBtn + "").removeClass("fa-spinner fa-spin");
        //ldBtn.stop();
        var d = error;
        Common.Alert.Error("Failed to show Lookup Location. Error " + d);
      },
    });
  },
  SelectedLocation: function (locId, locname) {
    $("#txlocationid").val(locId);
    $("#txlocationname").val(locname);
    $("#mdllocation").modal("hide");
    $(".modal-body").html("");
  },
  SelectedLocationKtp: function (locId, locname) {
    $("#txlocationktpid").val(locId);
    $("#txlocationktpname").val(locname);
    $("#mdllocation").modal("hide");
  },
  SelectedLocationCorp: function (locId, locname) {
    $("#txofficelocationid_financial").val(locId);
    $("#txofficelocationname_financial").val(locname);
    $("#mdlcorp").modal("hide");
  },
  SelectedBank: function (bnkid, bnkname) {
    $("#txbankid_sr2").val(bnkid);
    $("#txbankname_sr2").val(bnkname);
    $("#mdlbank").modal("hide");
  },
  SelectPenjamin: function (locId, locname) {
    $("#txlokpenjaminid").val(locId);
    $("#txtlokasipenjamin").val(locname);
    $("#mdlpenjamin").modal("hide");
  },
  SelectRef1: function (locId, locname) {
    $("#txlokref1id").val(locId);
    $("#txlokref1name").val(locname);
    $("#mdlreferensi").modal("hide");
    $(".modal-body").html("");
  },
  SelectRef2: function (locId, locname) {
    $("#txlokref2id").val(locId);
    $("#txlokref2name").val(locname);
    $("#mdlreferensi2").modal("hide");
    $(".modal-body").html("");
  },
  SelectRef3: function (locId, locname) {
    $("#txlokref3id").val(locId);
    $("#txlokref3name").val(locname);
    $("#mdlreferensi3").modal("hide");
    $(".modal-body").html("");
  },
  SelectSpouse: function (locId, locname) {
    $("#txlokpasanganid").val(locId);
    $("#txlokpasanganname").val(locname);
    $("#mdlpasangan").modal("hide");
  },
  SelectTagih: function (locId, locname) {
    $("#txmailtolocationid").val(locId);
    $("#txmailtolocationname").val(locname);
    $("#mdltagihan").modal("hide");
  },
  SelectOrderId: function (orderId) {
    cas_load.GetPoolingByOrderId(orderId);
    $("#txtorderId").val(orderId);
    $("#mdlorder").modal("hide");
  },
  SelectNppLama: function (npp, merk, type) {
    $("textarea#txtia_npplama").val(npp);
    $("textarea#txtia_merkitem").val(merk);
    $("textarea#txtia_tipeitem").val(type);
    $("#mdlNppLama").modal("hide");
  },
  SelectNik: function (nik) {
    $("#txtnik_konsumen_sr1").val(nik);
    $("#mdlNik").modal("hide");
  },
  msbank: function () {
    var params = {
      pageIndex: 1,
      searchTerm: "",
    };
    var ldBtn = Ladda.create(document.getElementById("btnshowbank"));
    $.ajax({
      url: "PaginationMsBank",
      type: "POST",
      data: params,
      beforeSend: function () {
        ldBtn.start();
      },
      success: function (data) {
        $("#containerBank").html(data);
        ldBtn.stop();
        $("#mdlbank").modal({
          backdrop: false,
        });
      },
      error: function (error) {
        ldBtn.stop();
        Common.Alert.Error(
          "Failed to show Lookup Bank. please check internet connection or contact administrator. Error " +
            d
        );
      },
    });
  },
  msloc_ref: async function (mKey, mBtn) {
    var mUrl = "";
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
      pageIndex: 1,
      searchTerm: "",
    };

    $.ajax({
      url: mUrl,
      type: "POST",
      data: params,
      beforeSend: function () {
        $("#i" + mBtn + "").addClass("fa-spinner fa-spin");
        $("#" + mBtn + "").prop("disabled", true);
      },
      success: function (data) {
        if (mKey == "ref1") {
          $("#containerReferensi").html(data);
          $("#mdlreferensi").modal({
            backdrop: false,
          });
        } else if (mKey == "ref2") {
          $("#containerReferensi2").html(data);
          $("#mdlreferensi2").modal({
            backdrop: false,
          });
        } else if (mKey == "ref3") {
          $("#containerReferensi3").html(data);
          $("#mdlreferensi3").modal({
            backdrop: false,
          });
        }
        $("#i" + mBtn + "").removeClass("fa-spinner fa-spin");
        $("#" + mBtn + "").prop("disabled", false);
      },
      error: function (error) {
        $("#i" + mBtn + "").removeClass("fa-spinner fa-spin");
        $("#" + mBtn + "").prop("disabled", false);
        var d = error;
        Common.Alert.Error(
          "Failed to show Lookup Location. please check internet connection or contact administrator.. Error " +
            d
        );
      },
    });
  },
  poolingorder: function () {
    var params = {
      tipeOrder: $("#ddlcreditsource").val(),
      pageIndex: 1,
      searchTerm: "",
    };
    var ldBtn = Ladda.create(document.getElementById("btnshoworderid"));
    $.ajax({
      url: "PaginationPoolingOrder",
      type: "POST",
      data: params,
      beforeSend: function () {
        ldBtn.start();
      },
      success: function (data) {
        $("#containerOrder").html(data);
        ldBtn.stop();
        $("#mdlorder").modal({
          backdrop: false,
        });
      },
      error: function (error) {
        ldBtn.stop();
        Common.Alert.Error("Failed to show Lookup Order ID. Error " + error);
      },
    });
  },
  AgreementOld: function () {
    if ($("#txKTP").val() == "") {
      Common.Alert.Warning("Mohon masukan No KTP Identitas terlebih dahulu !");
      return;
    }
    var params = {
      searchTerm: "",
      lesseeId: $("#txKTP").val(),
      pageIndex: 1,
    };
    $.ajax({
      url: "PaginationAgreementOld",
      type: "POST",
      data: params,
      beforeSend: function () {
        $("#ibtnshownpplama").addClass("fa-spinner fa-spin");
      },
      success: function (data) {
        $("#containerNppLama").html(data);
        $("#mdlNppLama").modal({
          backdrop: false,
        });
        $("#ibtnshownpplama").removeClass("fa-spinner fa-spin");
        document.getElementById("IAYesCheck").checked = true;
      },
      error: function (error) {
        $("#ibtnshownpplama").removeClass("fa-spinner fa-spin");
        Common.Alert.Error("Failed to show Lookup Order ID. Error " + error);
        document.getElementById("IAYesCheck").checked = true;
      },
    });
  },
  Nik: function (btn) {
    var params = {
      employeeName: "",
      pageIndex: 1,
    };

    $.ajax({
      url: "PaginationNIKKonsumen",
      type: "POST",
      data: params,
      beforeSend: function () {
        $("#" + btn + "").prop("disabled", true);
        $("#i" + btn + "").addClass("fa-spinner fa-spin");
      },
      success: function (data) {
        $("#mdlNik").modal({
          backdrop: false,
        });
        $("#containerNik").html(data);

        $("#" + btn + "").prop("disabled", false);
        $("#i" + btn + "").removeClass("fa-spinner fa-spin");
      },
      error: function (error) {
        ldBtn.stop();
        Common.Alert.Error(
          "Failed to show Lookup NIK Konsumen. Error " + error
        );
        $("#" + btn + "").prop("disabled", false);
        $("#i" + btn + "").removeClass("fa-spinner fa-spin");
      },
    });
  },
};
