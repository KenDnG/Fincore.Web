$(document).ready(function () {
  $("#DownloadFPK1").hide();
  $("#DownloadFPK2").hide();

  $("#MegaProteksiGold").val(numberFormat("35000"));

  document.getElementById("divOldNPP").style.display = "none";
  $("#txOldNPP").val("");
  $("#txOldNPP").removeAttr("required");

  document.getElementById("divUMC").style.display = "none";
  $("#tipekerjasama").removeAttr("required");
  $("#tipeperantara").removeAttr("required");
  $("#txPerantara").removeAttr("required");
  $("#pemilikrekening").removeAttr("required");
  $("#rekeningAtasNama").removeAttr("required");
  $("#txBankName").removeAttr("required");
  $("#NoRekening").removeAttr("required");

  $("#destination_bank_account_id_umc").removeAttr("required");
  $("#destination_bank_id_umc").removeAttr("required");
  $("#destination_account_no_umc").removeAttr("required");
  $("#destination_account_name_umc").removeAttr("required");
  $("#STNK_status_id").removeAttr("required");
  $("#BPKB_invoice").removeAttr("required");

  document.getElementById("divPengaliDeposit").style.display = "none";
  $("#Deposit").val("");
  $("#Deposit").removeAttr("required");

  document.getElementById("divMarketPrice").style.display = "none";
  document.getElementById("WarningMarketPrice").style.display = "none";

  //get produk marketing
  GetMarketingProduct();

  //get nama stnk
  GetSTNKName();

  //get Asal PO
  GetAsalPO();

  //get Tipe Penggunaan
  GetTipePenggunaan();

  //get AR
  GetAR();

  //get Cover Insurance Type
  GetCoverInsuranceType();

  //get Insurance Type
  GetInsuranceType();

  //get Interest Rate Type
  GetInterestRateType();

  var Flag = $("#txhiddenFlag").val();
  var asset_kind_id = $("#txhiddenasset_kind_id").val();
  var application_type_id = $("#txhiddenapplication_type_id").val();
  var is_item_new = $("#txhiddenis_item_new").val();
  var product_finance_id = $("#txhiddenproduct_finance_id").val();
  var item_brand_id = $("#txhiddenitem_brand_id").val();
  var asset_kind_class_id = $("#txhiddenasset_kind_class_id").val();
  var item_brand_type_id = $("#txhiddenitem_brand_type_id").val();
  var item_type_name = $("#txhiddenitem_type_name").val();
  var CC = $("#txhiddenCC").val();
  var product_id = $("#txhiddenproduct_id").val();
  var dealercode = $("#txhiddendealer_code").val();
  var dealername = $("#txhiddendealer_name").val();
  var surveyorcode = $("#txhiddensurveyor_id").val();
  var surveyorname = $("#txhiddensurveyor_name").val();
  var marketinghead_id = $("#txhiddenmarketinghead_id").val();
  var marketinghead_name = $("#txhiddenmarketinghead_name").val();
  var CGSCabangNo = $("#txhiddenCGSCabangNo").val();
  var product_marketing_id = $("#txhiddenproduct_marketing_id").val();
  var STNKNameId = $("#txhiddenSTNK_name_id").val();
  var STNKName = $("#txhiddenSTNK_name_description").val();
  var provenance_PO_id = $("#txhiddenprovenance_PO_id").val();
  var usage_type_id = $("#txhiddenusage_type_id").val();
  var AR = $("#txhiddenar").val();
  var tipe_cover = $("#txhiddentipe_cover").val();
  var insurance_type_id = $("#txhiddeninsurance_type_id").val();
  var interest_rate_type_id = $("#txhiddeninterest_rate_type_id").val();
  var installment_code = $("#txhiddeninstallment_id").val();
  var tenor = $("#txhiddentenor").val();
  var OTR = $("#txhiddenasset_cost").val();
  var gross_down_payment = $("#txhiddengross_down_payment").val();
  var admin_fee = $("#txhiddenadmin_fee").val();
  var biaya_provisi = $("#txhiddenbiaya_provisi").val();
  var insurance_fee = $("#txhiddeninsurance_fee").val();
  var uang_muka_murni_kons = $("#txhiddenuang_muka_murni_kons").val();
  var jml_pembiayaan = $("#txhiddenjml_pembiayaan").val();
  var amount_installment = $("#txhiddenamount_installment").val();
  var effective_rate = $("#txhiddeneffective_rate").val();
  var flat_rate = $("#txhiddenflat_rate").val();
  var disc_deposit = $("#txhiddendisc_deposit").val();
  var overdue_rate = $("#txhiddenoverdue_rate").val();
  var year = $("#txhiddenyear").val();
  var disc_type = $("#txhiddendisc_type").val();
  var TAC_max = $("#txhiddenTAC_max").val();
  var komper_max = $("#txhiddenkomper_max").val();
  var deposit_installment = $("#txhiddendeposit_installment").val();
  var is_topup_ms = $("#txhiddenis_topup_ms").val();
  var old_npp = $("#txhiddenold_npp").val();
  var skema_id = $("#txhiddenskema_id").val();
  var perantara_type_id = $("#txhiddenperantara_type_id").val();
  var agent_id = $("#txhiddenagent_id").val();
  var agent_name = $("#txhiddenagent_name").val();
  var ownership_account_type_id = $("#txhiddenownership_account_type_id").val();
  var bank_account_id_umc = $("#txhiddenbank_account_id_umc").val();
  var bank_id_umc = $("#txhiddenbank_id_umc").val();
  var bank_name_umc = $("#txhiddenbank_name_umc").val();
  var account_no_umc = $("#txhiddenaccount_no_umc").val();
  var account_name_umc = $("#txhiddenaccount_name_umc").val();
  var IsMegaProteksiGold = $("#hdnIsMegaProteksiGold").val();
  var MegaProteksiGoldAmount = $("#hdnMegaProteksiGoldAmount").val();
  var chasis_no = $("#hdnchasis_no").val();
  var machine_no = $("#hdnmachine_no").val();
  var prefix_plat = $("#hdnprefix_plat").val();
  var plat_no = $("#hdnplat_no").val();
  var plat_code = $("#hdnplat_code").val();
  var FPK1 = $("#hdnFPK1").val();
  var FPK2 = $("#hdnFPK2").val();
  var customer_name = $("#hdnCustomerName").val();
  var customer_spouse_name = $("#hdnCustomerSpouseName").val();
  var destination_bank_account_id_umc = $(
    "#hdndestination_bank_account_id_umc"
  ).val();
  var destination_account_no_umc = $("#hdndestination_account_no_umc").val();
  var destination_account_name_umc = $(
    "#hdndestination_account_name_umc"
  ).val();
  var destination_bank_id_umc = $("#hdndestination_bank_id_umc").val();
  var STNK_status_id = $("#hdnSTNK_status_id").val();
  var BPKB_invoice = $("#hdnBPKB_invoice").val();
  var market_price = $("#hdnmarket_price").val();
  var max_plafon_pencairan = $("#hdnmax_plafon_pencairan").val();

  if (Flag != "Add") {
    if (FPK1 != "") {
      $("#DownloadFPK1").show();
    }

    if (FPK2 != "") {
      $("#DownloadFPK2").show();
    }

    $("#txtNoRangka").val(chasis_no);
    $("#txtNoMesin").val(machine_no);
    $("#ddlPlatPrefix").val(prefix_plat);
    $("#txtNoPlat").val(plat_no);
    $("#txtPlatCode").val(plat_code);
    $("#hdnNoPlat").val(prefix_plat + plat_no + plat_code);

    $("#tipekerjasama").val(skema_id);
    $("#tipeperantara").val(perantara_type_id);
    $("#txPerantaraId").val(agent_id);
    $("#txPerantara").val(agent_name);
    $("#pemilikrekening").val(ownership_account_type_id);
    $("#hdBankAccountID").val(bank_account_id_umc);
    $("#txBankNameId").val(bank_id_umc);
    $("#txBankName").val(bank_name_umc);
    $("#NoRekening").val(account_no_umc);
    $("#rekeningAtasNama").val(account_name_umc);

    VisibilityMarketPrice(application_type_id, is_item_new, "1");

    //get merk item
    GetMerkItem(asset_kind_id, "Y");

    //get model item
    GetModelItem(asset_kind_id, "Y");

    //get produk
    GetProduct(asset_kind_id, "Y");

    $("#ms_application_type")
      .find('option[value="' + application_type_id + '"]')
      .attr("selected", "selected");
    $("#ms_product_finance")
      .find('option[value="' + product_finance_id + '"]')
      .attr("selected", "selected");
    $("#is_item_new")
      .find('option[value="' + is_item_new + '"]')
      .attr("selected", "selected");
    $("#hdis_item_newSelecytedValue").val(is_item_new);
    $("#asset_kind_id")
      .find('option[value="' + asset_kind_id + '"]')
      .attr("selected", "selected");

    $("#txitem_brand_type_id").val(item_brand_type_id);
    $("#txItemname").val(item_type_name);
    $("#CC").val(CC);

    $("#txdealercode").val(dealercode);
    $("#txdealername").val(dealername);
    $("#txSurveyorid").val(surveyorcode);
    $("#txSurveyorname").val(surveyorname);
    $("#txMarketingHeadid").val(marketinghead_id);
    $("#txMarketingHeadname").val(marketinghead_name);
    $("#txCGSNo").val(CGSCabangNo);

    $("#PartnerSTNKName").val(STNKName);

    $("#hdnCustomerName").val(customer_name);
    $("#hdnCustomerSpouseName").val(customer_spouse_name);

    $("#insurance_type_id")
      .find('option[value="' + insurance_type_id + '"]')
      .attr("selected", "selected");

    document.getElementById("divPengaliDeposit").style.display = "none";
    $("#DepositAngsuran")
      .find('option[value="0"]')
      .attr("selected", "selected");

    if (deposit_installment != "" && deposit_installment != "0") {
      document.getElementById("divPengaliDeposit").style.display = "block";
      $("#DepositAngsuran")
        .find('option[value="1"]')
        .attr("selected", "selected");
      $("#Deposit")
        .find('option[value="' + deposit_installment + '"]')
        .attr("selected", "selected");
    } else {
      document.getElementById("divPengaliDeposit").style.display = "none";
      $("#DepositAngsuran")
        .find('option[value="0"]')
        .attr("selected", "selected");
    }

    $("#topupmegasolusi")
      .find('option[value="' + is_topup_ms + '"]')
      .attr("selected", "selected");
    if (is_topup_ms == "1") {
      document.getElementById("divOldNPP").style.display = "block";
      $("#txOldNPP").attr("required", true);
      $("#txOldNPP").val(old_npp);
    }

    $("#txtenor").val(tenor);
    $("#OTR").val(numberFormat(OTR));
    $("#DPGross").val(numberFormat(gross_down_payment));
    $("#AdminFee").val(numberFormat(admin_fee));
    $("#BiayaProvisi").val(numberFormat(biaya_provisi));
    $("#InsuranceFee").val(numberFormat(insurance_fee));
    $("#UangMukaMurni").val(numberFormat(uang_muka_murni_kons));
    $("#JlhPembiayaan").val(numberFormat(jml_pembiayaan));
    $("#AmtInstallment").val(numberFormat(amount_installment));
    $("#BungaEfektif").val(effective_rate);
    $("#BungaFlat").val(flat_rate);
    $("#BiayaAdminDealer").val(numberFormat(disc_deposit));
    $("#overdue_rate").val(overdue_rate);

    $("#KodeAngsuran")
      .find('option[value="' + installment_code + '"]')
      .attr("selected", "selected");

    if (installment_code != "" && asset_kind_id == "001") {
      $("#txhiddeninstallment_id").attr("name", "installment_id");
      $("#KodeAngsuran").attr("disabled", true);
    } else {
      $("#KodeAngsuran").attr("name", "installment_id");
    }

    $("#disc_type").val(disc_type);

    $("#TAC_max").val(TAC_max);
    $("#komper_max").val(numberFormat(komper_max));

    //Standart
    if (application_type_id == "02") {
      $("#disc_type").removeAttr("disabled");
      $("#is_item_new").removeAttr("disabled");
    }
    //UMC - 0
    else if (application_type_id == "03") {
      $("#disc_type").attr("disabled", true);
      $("#is_item_new").attr("disabled", true);

      document.getElementById("divUMC").style.display = "block";
      $("#tipekerjasama").attr("required", true);
      $("#tipeperantara").attr("required", true);
      $("#txPerantara").attr("required", true);
      $("#pemilikrekening").attr("required", true);
      $("#rekeningAtasNama").attr("required", true);
      $("#txBankName").attr("required", true);
      $("#NoRekening").attr("required", true);

      $("#destination_bank_account_id_umc").attr("required", true);
      $("#destination_bank_id_umc").attr("required", true);
      $("#destination_account_no_umc").attr("required", true);
      $("#destination_account_name_umc").attr("required", true);

      $("#STNK_status_id").attr("required", true);
      $("#BPKB_invoice").attr("required", true);
    } else {
      $("#disc_type").removeAttr("disabled");
      $("#is_item_new").removeAttr("disabled");
    }

    //get tahun kendaraan
    GetYear(
      asset_kind_id,
      item_brand_id,
      asset_kind_class_id,
      item_brand_type_id,
      "Y"
    );

    var NominalPencairanDealer =
      parseFloat(OTR) - parseFloat(gross_down_payment);
    $("#NominalPencairanDealer").val(numberFormat(NominalPencairanDealer));

    hitungPersentaseTACMax();

    $("#product_id").attr("disabled", true);
    $("#provenance_PO_id").attr("disabled", true);
    $("#insurance_cover_type_id").attr("disabled", true);
    $("#insurance_type_id").attr("disabled", true);

    if (IsMegaProteksiGold == "1") {
      $("#cbxMegaProteksiGold").prop("checked", true);
    } else {
      $("#cbxMegaProteksiGold").prop("checked", false);
    }

    $("#hdnOldTenor").val(tenor);

    $("#destination_bank_account_id_umc")
      .find('option[value="' + destination_bank_account_id_umc + '"]')
      .attr("selected", "selected");

    GetBankNameDestination(destination_bank_account_id_umc);

    $("#destination_account_no_umc").val(destination_account_no_umc);
    $("#destination_account_name_umc").val(destination_account_name_umc);
    $("#STNK_status_id").val(STNK_status_id);
    $("#BPKB_invoice")
      .find('option[value="' + BPKB_invoice + '"]')
      .attr("selected", "selected");

    var market_price = $("#hdnmarket_price").val();
    var max_plafon_pencairan = $("#hdnmax_plafon_pencairan").val();
    $("#MarketPrice").val(numberFormat(market_price));
    $("#MaxPencairan").val(numberFormat(max_plafon_pencairan));

    if (numberFormat(market_price) == "0") {
      if (application_type_id == "03") {
        document.getElementById("WarningMarketPrice").style.display = "block";
      }
    }
  }

  if (Flag == "Approval" || Flag == "View") {
    $("#ms_application_type").attr("disabled", true);
    $("#ms_product_finance").attr("disabled", true);
    $("#is_item_new").attr("disabled", true);
    $("#asset_kind_id").attr("disabled", true);
    $("#ms_item_brand").attr("disabled", true);
    $("#ms_asset_kind_class").attr("disabled", true);
    $("#txItemname").attr("disabled", true);
    $("#btnshow_Item").hide();
    $("#CC").attr("disabled", true);
    $("#year").attr("disabled", true);
    $("#product_id").attr("disabled", true);
    $("#txdealername").attr("disabled", true);
    $("#btnshow_Dealer").hide();
    $("#txSurveyorname").attr("disabled", true);
    $("#btnshow_Surveyor").hide();
    $("#txMarketingHeadname").attr("disabled", true);
    $("#btnshow_MarketingHead").hide();
    $("#txCGSNo").attr("disabled", true);
    $("#btnshow_CGSNo").hide();
    $("#product_marketing_id").attr("disabled", true);
    $("#STNKName").attr("disabled", true);
    $("#PartnerSTNKName").attr("disabled", true);
    $("#provenance_PO_id").attr("disabled", true);
    $("#usage_type_id").attr("disabled", true);
    $("#AR_id").attr("disabled", true);
    $("#insurance_cover_type_id").attr("disabled", true);
    $("#insurance_type_id").attr("disabled", true);
    $("#interest_rate_type_id").attr("disabled", true);
    $("#txtenor").attr("disabled", true);
    $("#MarketPrice").attr("disabled", true);
    $("#MaxPencairan").attr("disabled", true);
    $("#OTR").attr("disabled", true);
    $("#DPGross").attr("disabled", true);
    $("#NominalPencairanDealer").attr("disabled", true);
    $("#AdminFee").attr("disabled", true);
    $("#BiayaProvisi").attr("disabled", true);
    $("#InsuranceFee").attr("disabled", true);
    $("#UangMukaMurni").attr("disabled", true);
    $("#JlhPembiayaan").attr("disabled", true);
    $("#AmtInstallment").attr("disabled", true);
    $("#BungaEfektif").attr("disabled", true);
    $("#BungaFlat").attr("disabled", true);
    $("#BiayaAdminDealer").attr("disabled", true);
    $("#disc_type").attr("disabled", true);
    $("#TAC_maxPersentase").attr("disabled", true);
    $("#komper_max").attr("disabled", true);
    $("#TAC_maxKomperPersentase").attr("disabled", true);
    $("#txOldNPP").attr("disabled", true);
    $("#tipekerjasama").attr("disabled", true);
    $("#tipeperantara").attr("disabled", true);
    $("#txPerantara").attr("disabled", true);
    $("#btnshow_Perantara").hide();
    $("#pemilikrekening").attr("disabled", true);
    $("#txBankName").attr("disabled", true);
    $("#btnshow_BankName").hide();
    $("#NoRekening").attr("disabled", true);
    $("#rekeningAtasNama").attr("disabled", true);
    $("#DepositAngsuran").attr("disabled", true);
    $("#Deposit").attr("disabled", true);
    $("#btnshow_OldNPP").hide();
    $("#topupmegasolusi").attr("disabled", true);
    $("#KodeAngsuran").attr("disabled", true);
    $("#cbxMegaProteksiGold").attr("disabled", true);
    $("#txtNoRangka").attr("disabled", true);
    $("#txtNoMesin").attr("disabled", true);
    $("#ddlPlatPrefix").attr("disabled", true);
    $("#txtNoPlat").attr("disabled", true);
    $("#txtPlatCode").attr("disabled", true);
    $("#destination_bank_account_id_umc").attr("disabled", true);
    $("#destination_bank_id_umc").attr("disabled", true);
    $("#destination_account_no_umc").attr("disabled", true);
    $("#destination_account_name_umc").attr("disabled", true);
    $("#STNK_status_id").attr("disabled", true);
    $("#BPKB_invoice").attr("disabled", true);
  }
});

jQuery(document).ready(function () {
  cm_load.init();

  //Setup responsiove UI on Mobile
  const pnlAsset = document.getElementById("Asset_more");
  pnlAsset?.addEventListener("click", () => {
    cm_load.changeClass_Asset();
  });

  const pnlPrice = document.getElementById("Price_more");
  pnlPrice?.addEventListener("click", () => {
    cm_load.changeClass_Price();
  });

  $("#is_item_new").change(function () {
    $("#hdis_item_newSelecytedValue").val($("#is_item_new").val());
  });

  $("#ms_item_brand").change(function () {
    //get tahun kendaraan
    var item_id = $("#asset_kind_id").val();
    var Item_Brand_Id = $("#ms_item_brand").val();
    var asset_kind_class_id = $("#ms_asset_kind_class").val();
    var asset_type_id = $("#txitem_brand_type_id").val();

    GetYear(
      asset_kind_id,
      item_brand_id,
      asset_kind_class_id,
      item_brand_type_id,
      "N"
    );
  });

  $("#ms_asset_kind_class").change(function () {
    //get tahun kendaraan
    var item_id = $("#asset_kind_id").val();
    var Item_Brand_Id = $("#ms_item_brand").val();
    var asset_kind_class_id = $("#ms_asset_kind_class").val();
    var asset_type_id = $("#txitem_brand_type_id").val();

    GetYear(
      asset_kind_id,
      item_brand_id,
      asset_kind_class_id,
      item_brand_type_id,
      "N"
    );
  });

  $("#STNKName").change(function () {
    $("#lblNamaSTNK2").val(
      "Nama STNK " + $("#STNKName option:selected").text()
    );
    selectedTextarea = $("#PartnerSTNKName")[0];
    selectedTextarea.placeholder =
      "Nama STNK " + $("#STNKName option:selected").text();

    var currentValue = $("#STNKName").val();
    var customerName = $("#hdnCustomerName").val();
    var customerSpouseName = $("#hdnCustomerSpouseName").val();

    $("#PartnerSTNKName").val("");

    if (currentValue == "1") {
      $("#PartnerSTNKName").val(customerName);
    } else if (currentValue == "2") {
      $("#PartnerSTNKName").val(customerSpouseName);
    }
  });

  $("#is_item_new").change(function () {
    var TipeAplikasi = $("#ms_application_type").val();
    var isItemNew = $("#is_item_new").val();
    VisibilityMarketPrice(TipeAplikasi, isItemNew, "0");
  });

  $("#AR_id").change(function () {
    var result = Calculate();
    if (result != "Success") {
      $("#AR_id").val("");
      Common.Alert.Error(result);
    }
  });

  $("#txtenor").change(function () {
    var result = Calculate();
    if (result != "Success") {
      $("#txtenor").val("");
      Common.Alert.Error(result);
    }

    GetInsuranceFee();
  });

  $("#OTR").keyup(function () {
    $("#OTR").val(numberFormat($("#OTR").val().replaceAll(".", "")));

    var result = Calculate();
    if (result != "Success") {
      $("#OTR").val("");
      $("#NominalPencairanDealer").val("");
      Common.Alert.Error(result);
    }
  });

  $("#OTR").change(function () {
    GetInsuranceFee();
  });

  $("#year").change(function () {
    var TipeAplikasi = $("#ms_application_type").val();
    var Year = $("#year").val();
    VisibilityMarketPrice(TipeAplikasi, $("#is_item_new").val(), "0");
    GetChasisCode(Year);
  });

  $("#DPGross").keyup(function () {
    $("#DPGross").val(numberFormat($("#DPGross").val().replaceAll(".", "")));
  });

  $("#DPGross").change(function () {
    var result = Calculate();
    if (result != "Success") {
      $("#DPGross").val("");
      Common.Alert.Error(result);
    }
  });

  $("#AdminFee").keyup(function () {
    $("#AdminFee").val(numberFormat($("#AdminFee").val().replaceAll(".", "")));

    var result = Calculate();
    if (result != "Success") {
      $("#AdminFee").val("");
      Common.Alert.Error(result);
    }
  });

  $("#BiayaProvisi").keyup(function () {
    $("#BiayaProvisi").val(
      numberFormat($("#BiayaProvisi").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#BiayaProvisi").val("");
      Common.Alert.Error(result);
    }
  });

  $("#InsuranceFee").keyup(function () {
    $("#InsuranceFee").val(
      numberFormat($("#InsuranceFee").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#InsuranceFee").val("");
      Common.Alert.Error(result);
    }
  });

  $("#UangMukaMurni").keyup(function () {
    $("#UangMukaMurni").val(
      numberFormat($("#UangMukaMurni").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#UangMukaMurni").val("");
      Common.Alert.Error(result);
    }
  });

  $("#JlhPembiayaan").keyup(function () {
    $("#JlhPembiayaan").val(
      numberFormat($("#JlhPembiayaan").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#JlhPembiayaan").val("");
      Common.Alert.Error(result);
    }
  });

  $("#AmtInstallment").keyup(function () {
    $("#AmtInstallment").val(
      numberFormat($("#AmtInstallment").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#AmtInstallment").val("");
      Common.Alert.Error(result);
    }
  });

  $("#BungaFlat").keyup(function () {
    HitungJumlahAngsuran();
    HitungEffRate(1);
  });

  $("#BiayaAdminDealer").keyup(function () {
    $("#BiayaAdminDealer").val(
      numberFormat($("#BiayaAdminDealer").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#BiayaAdminDealer").val("");
      Common.Alert.Error(result);
    }
  });

  $("#komper_max").keyup(function () {
    $("#komper_max").val(
      numberFormat($("#komper_max").val().replaceAll(".", ""))
    );

    var result = Calculate();
    if (result != "Success") {
      $("#komper_max").val("");
      Common.Alert.Error(result);
    }
  });

  $("#asset_kind_id").change(function () {
    //e.preventDefault();

    var id = $(this).val();

    GetDataDropDown(id);
    GetInsuranceFee();

    var TipeAplikasi = $("#ms_application_type").val();
    VisibilityMarketPrice(TipeAplikasi, $("#is_item_new").val(), "0");
  });

  $("#DepositAngsuran").change(function () {
    var DepositAngsuran = $("#DepositAngsuran").val();

    document.getElementById("divPengaliDeposit").style.display = "none";
    $("#Deposit").val("");
    $("#Deposit").removeAttr("required");

    if (DepositAngsuran == "1") {
      document.getElementById("divPengaliDeposit").style.display = "block";
      $("#Deposit").attr("required", true);
    }
  });

  $("#Deposit").change(function () {
    var result = Calculate();
    if (result != "Success") {
      $("#Deposit").val("");
      Common.Alert.Error(result);
    }
  });

  $("#topupmegasolusi").change(function () {
    var topupmegasolusi = $("#topupmegasolusi").val();

    document.getElementById("divOldNPP").style.display = "none";
    $("#txOldNPP").val("");
    $("#txOldNPP").removeAttr("required");

    if (topupmegasolusi == "1") {
      document.getElementById("divOldNPP").style.display = "block";
      $("#txOldNPP").attr("required", true);
    }
  });

  $("#ms_application_type").change(function () {
    var application_type = $("#ms_application_type").val();

    document.getElementById("divUMC").style.display = "none";
    $("#tipekerjasama").removeAttr("required");
    $("#tipeperantara").removeAttr("required");
    $("#txPerantara").removeAttr("required");
    $("#pemilikrekening").removeAttr("required");
    $("#rekeningAtasNama").removeAttr("required");
    $("#txBankName").removeAttr("required");
    $("#NoRekening").removeAttr("required");
    $("#txtNoRangka").removeAttr("required");
    $("#txtNoMesin").removeAttr("required");
    $("#ddlPlatPrefix").removeAttr("required");
    $("#txtNoPlat").removeAttr("required");

    $("#tipekerjasama").val("");
    $("#tipeperantara").val("");
    $("#txPerantaraId").val("");
    $("#txPerantara").val("");
    $("#pemilikrekening").val("");
    $("#rekeningAtasNama").val("");
    $("#NoRekening").val("");
    $("#hdBankAccountID").val("");
    $("#txBankNameId").val("");
    $("#txBankName").val("");
    $("#txtNoRangka").val("");
    $("#txtNoMesin").val("");
    $("#ddlPlatPrefix").val("");
    $("#txtNoPlat").val("");

    //Standart
    if (application_type == "02") {
      $("#disc_type").removeAttr("disabled");
      $("#disc_type").val("1");

      $("#is_item_new").removeAttr("disabled");
      $("#is_item_new").val("");
      $("#hdis_item_newSelecytedValue").val("");
    }
    //UMC - 0
    else if (application_type == "03") {
      $("#disc_type").val("0");
      $("#disc_type").attr("disabled", true);

      $("#is_item_new").val("0");
      $("#hdis_item_newSelecytedValue").val("0");
      $("#is_item_new").attr("disabled", true);

      document.getElementById("divUMC").style.display = "block";
      $("#tipekerjasama").attr("required", true);
      $("#tipeperantara").attr("required", true);
      $("#txPerantara").attr("required", true);
      $("#pemilikrekening").attr("required", true);
      $("#rekeningAtasNama").attr("required", true);
      $("#txBankName").attr("required", true);
      $("#NoRekening").attr("required", true);
      $("#txtNoRangka").attr("required", true);
      $("#txtNoMesin").attr("required", true);
      $("#ddlPlatPrefix").attr("required", true);
      $("#txtNoPlat").attr("required", true);
    } else {
      $("#disc_type").removeAttr("disabled");
      $("#disc_type").val("1");

      $("#is_item_new").removeAttr("disabled");
      $("#is_item_new").val("");
      $("#hdis_item_newSelecytedValue").val("");
    }

    VisibilityMarketPrice(application_type, $("#is_item_new").val(), "0");
  });

  $("#pemilikrekening").change(function () {
    $("#hdBankAccountID").val("");
    $("#txBankNameId").val("");
    $("#txBankName").val("");
    $("#NoRekening").val("");
    $("#rekeningAtasNama").val("");
  });

  $("#tipeperantara").change(function () {
    "#txPerantaraId".val("");
    $("#txPerantara").val("");
  });

  $("#tipekerjasama").change(function () {
    GetMarketPrice2();
  });

  $("#product_id").change(function () {
    $("#txhiddenproduct_id").val($("#product_id").val());
  });

  $("#provenance_PO_id").change(function () {
    $("#txhiddenprovenance_PO_id").val($("#provenance_PO_id").val());
  });

  $("#insurance_cover_type_id").change(function () {
    $("#txhiddentipe_cover").val($("#insurance_cover_type_id").val());
  });

  $("#insurance_type_id").change(function () {
    $("#txhiddeninsurance_type_id").val($("#insurance_type_id").val());
  });

  $("#cbxMegaProteksiGold").change(function () {
    var result = Calculate();
    if (result != "Success") {
      Common.Alert.Error(result);
    }
  });

  $("#ddlPlatPrefix").change(function () {
    SetFullPlatNo();
  });

  $("#txtNoPlat").keyup(function () {
    SetFullPlatNo();
  });

  $("#txtPlatCode").keyup(function () {
    SetFullPlatNo();
  });

  $("#txtNoRangka").change(function () {
    let norangka = $("#txtNoRangka").val();
    if (norangka.length != 17) {
      $("#txtNoRangka").val("");
      Common.Alert.Error("No Rangka harus 17 karakter");
    } else {
      let chasis_code = $("#hdnchasis_code").val();

      let result = norangka.substring(17, 9);
      let result2 = result.substring(0, 1);

      if (result2 != chasis_code) {
        $("#txtNoRangka").val("");
        Common.Alert.Error(
          "Format No Rangka harus sesuai dengan Tahun Kendaraan"
        );
      }
    }
  });

  $("#destination_bank_account_id_umc").change(function () {
    GetBankNameDestination($("#destination_bank_account_id_umc").val());
  });

  $("#BPKB_invoice").change(function () {
    GetMarketPrice2();
  });

  $("#STNK_status_id").change(function () {
    GetMarketPrice2();
  });

  $("#destination_bank_account_id_umc").change(function () {
    GetMarketPrice2();
  });
});

var cm_load = {
  init: function () {
    this.TogglePanel();
  },
  changeClass_Asset: function () {
    var content = document.getElementById("Asset");

    var asset = document.getElementById("Asset_more");
    content.classList.toggle("show");

    if (content.classList.contains("AssetShow")) {
      asset.innerHTML = "Data Asset";
    } else {
      asset.innerHTML = "Data Asset";
    }
  },
  changeClass_Price: function () {
    var content = document.getElementById("Price");

    var price = document.getElementById("Price_more");
    content.classList.toggle("show");

    if (content.classList.contains("PriceShow")) {
      price.innerHTML = "Data Price";
    } else {
      price.innerHTML = "Data Price";
    }
  },
  TogglePanel: function () {
    $("btnAsset").click(function () {
      $("Asset").toggle();
    });

    $("btnPrice").click(function () {
      $("Price").toggle();
    });
  },
};

var cm_lookup = {
  LookupType: function (mKey, mBtn) {
    var mUrl = "";
    switch (mKey) {
      case "Item":
        mUrl = "../CM/PaginationItem";
        break;
      case "Dealer":
        mUrl = "../CM/PaginationDealer";
        break;
      case "Surveyor":
        mUrl = "../CM/PaginationSurveyor";
        break;
      case "MarketingHead":
        mUrl = "../CM/PaginationMarketingHead";
        break;
      case "CGSNo":
        mUrl = "../CM/PaginationCGSNo";
        break;
      case "OldNPP":
        mUrl = "../CM/PaginationOldNPP";
        break;
      case "Perantara":
        mUrl = "../CM/PaginationPerantara";
        break;
      case "BankName":
        mUrl = "../CM/PaginationBankName";
        break;
    }

    var itemid = $("#asset_kind_id").val();
    var itembrandid = $("#ms_item_brand").val();
    var isitemnew = $("#is_item_new").val();
    var assetkindclassid = $("#ms_asset_kind_class").val();
    var BranchId = $("#txhiddenBranchId").val();
    var CompanyId = $("#txhiddenCompanyId").val();
    var ItemMerkTypeID = $("#txitem_brand_type_id").val();
    var TipePerantara = $("#tipeperantara").val();
    var PemilikRekening = $("#pemilikrekening").val();

    if (mKey == "Item") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        item_id: itemid,
        item_brand_id: itembrandid,
        asset_kind_class_id: assetkindclassid,
      };

      $.ajax({
        url: mUrl,
        //+ "item_id=" + itemid + "&item_brand_id=" + itembrandid + "&asset_kind_class_id=" + assetkindclassid,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlitem").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerItem").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup Item Type. Error " + d);
        },
      });
    } else if (mKey == "Dealer") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        item_id: itemid,
        is_item_new: isitemnew,
        item_merk: itembrandid,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdldealer").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerDealer").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup Dealer. Error " + d);
        },
      });
    } else if (mKey == "Surveyor") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        item_id: itemid,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlsurveyor").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerSurveyor").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup Surveyor. Error " + d);
        },
      });
    } else if (mKey == "MarketingHead") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        item_id: itemid,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlmarketinghead").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerMarketingHead").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error(
            "Failed to show Lookup Marketing Head. Error " + d
          );
        },
      });
    } else if (mKey == "CGSNo") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        BranchId: BranchId,
        CompanyId: CompanyId,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlCGSNo").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerCGSNo").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup CGS No. Error " + d);
        },
      });
    } else if (mKey == "OldNPP") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        BranchId: BranchId,
        CompanyId: CompanyId,
        ItemMerkTypeID: ItemMerkTypeID,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlOldNPP").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerOldNPP").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup OldNPP. Error " + d);
        },
      });
    } else if (mKey == "Perantara") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        BranchId: BranchId,
        CompanyId: CompanyId,
        TipePerantara: TipePerantara,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlPerantara").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerPerantara").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup Perantara. Error " + d);
        },
      });
    } else if (mKey == "BankName") {
      var params = {
        pageIndex: 1,
        searchTerm: "",
        BranchId: BranchId,
        CompanyId: CompanyId,
        PemilikRekening: PemilikRekening,
      };

      $.ajax({
        url: mUrl,
        type: "POST",
        data: params,
        beforeSend: function () {
          $("#i" + mBtn).addClass("fa-spinner fa-spin");
        },
        success: function (data) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");

          $("#mdlBankName").modal({
            backdrop: false,
          });
          document.getElementById(mBtn).style.display = "none";
          $("#containerBankName").html(data);
        },
        error: function (error) {
          $("#i" + mBtn).removeClass("fa-spinner fa-spin");
          var d = error;
          Common.Alert.Error("Failed to show Lookup BankName. Error " + d);
        },
      });
    }
  },
  SelectedItemType: function (item_brand_type_id, item_type_name, CC) {
    $("#txitem_brand_type_id").val(item_brand_type_id);
    $("#txItemname").val(item_type_name);
    $("#CC").val(CC);

    //get tahun kendaraan
    var item_id = $("#asset_kind_id").val();
    var Item_Brand_Id = $("#ms_item_brand").val();
    var asset_kind_class_id = $("#ms_asset_kind_class").val();

    GetYear(
      item_id,
      Item_Brand_Id,
      asset_kind_class_id,
      item_brand_type_id,
      "N"
    );

    var TipeAplikasi = $("#ms_application_type").val();
    VisibilityMarketPrice(TipeAplikasi, $("#is_item_new").val(), "0");

    $("#txOldNPP").val("");

    $("#mdlitem").modal("hide");
  },
  SelectedDealer: function (dealer_code, dealer_name) {
    $("#txdealercode").val(dealer_code);
    $("#txdealername").val(dealer_name);
    $("#mdldealer").modal("hide");
  },
  SelectedSurveyor: function (surveyor_id, surveyor_name) {
    $("#txSurveyorid").val(surveyor_id);
    $("#txSurveyorname").val(surveyor_name);
    $("#mdlsurveyor").modal("hide");
  },
  SelectedMarketingHead: function (marketinghead_id, marketinghead_name) {
    $("#txMarketingHeadid").val(marketinghead_id);
    $("#txMarketingHeadname").val(marketinghead_name);
    $("#mdlmarketinghead").modal("hide");
  },
  SelectedCGSNo: function (CGSNo) {
    $("#txCGSNo").val(CGSNo);
    $("#mdlCGSNo").modal("hide");
  },
  SelectedOldNPP: function (OldNPP) {
    $("#txOldNPP").val(OldNPP);
    $("#mdlOldNPP").modal("hide");
  },
  SelectedPerantara: function (PerantaraId, PerantaraName) {
    $("#txPerantaraId").val(PerantaraId);
    $("#txPerantara").val(PerantaraName);
    $("#mdlPerantara").modal("hide");
  },
  SelectedBankName: function (
    BankAccountID,
    BankID,
    BankName,
    BankAccountNo,
    BankAccountName
  ) {
    $("#hdBankAccountID").val(BankAccountID);
    $("#txBankNameId").val(BankID);
    $("#txBankName").val(BankName);
    $("#NoRekening").val(BankAccountNo);
    $("#rekeningAtasNama").val(BankAccountName);
    $("#mdlBankName").modal("hide");
  },
};

function numberFormat(value) {
  if (value != undefined) {
    let nf = new Intl.NumberFormat("id-ID");

    if (nf.format(value) == "NaN") {
      return "";
    } else {
      return nf.format(value);
    }
  }
}

function GetDataDropDown(id) {
  //get merk item
  GetMerkItem(id, "N");

  //get model item
  GetModelItem(id, "N");

  //get tahun kendaraan
  var item_id = $("#asset_kind_id").val();
  var Item_Brand_Id = $("#ms_item_brand").val();
  var asset_kind_class_id = $("#ms_asset_kind_class").val();
  var asset_type_id = $("#txitem_brand_type_id").val();

  GetYear(
    asset_kind_id,
    item_brand_id,
    asset_kind_class_id,
    item_brand_type_id,
    "N"
  );

  //get produk
  GetProduct(id, "N");
}

function GetYear(
  item_id,
  Item_Brand_Id,
  asset_kind_class_id,
  asset_type_id,
  firstload
) {
  //get tahun kendaraan

  var Flag = $("#txhiddenFlag").val();
  var year = $("#txhiddenyear").val();

  var params = {
    item_id: item_id,
    Item_Brand_Id: Item_Brand_Id,
    asset_kind_class_id: asset_kind_class_id,
    asset_type_id: asset_type_id,
  };

  $.ajax({
    type: "GET",
    url: "../CM/GetYear",
    data: params,
  }).then(function (data) {
    $("#year").find("option").remove();
    $("#year").append(
      $("<option></option>").val("").html("Pilih Tahun Kendaraan")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (year == value.year && firstload == "Y") {
          $("#year").append(
            $("<option selected></option>").val(value.year).html(value.year)
          );
        } else {
          $("#year").append(
            $("<option></option>").val(value.year).html(value.year)
          );
        }
      } else {
        $("#year").append(
          $("<option></option>").val(value.year).html(value.year)
        );
      }
    });
  });
}

function GetModelItem(id, firstload) {
  //get model item

  var Flag = $("#txhiddenFlag").val();
  var asset_kind_class_id = $("#txhiddenasset_kind_class_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetAssetKindClass?item_id=" + id,
    data: "item_id=" + id,
  }).then(function (data) {
    $("#ms_asset_kind_class").find("option").remove();
    //$("#ms_asset_kind_class").append($("<option></option>").val("").html("Pilih Model Item"));

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (
          asset_kind_class_id == value.asset_kind_class_id &&
          firstload == "Y"
        ) {
          $("#ms_asset_kind_class").append(
            $("<option selected></option>")
              .val(value.asset_kind_class_id)
              .html(value.asset_kind_class_name)
          );
        } else {
          $("#ms_asset_kind_class").append(
            $("<option></option>")
              .val(value.asset_kind_class_id)
              .html(value.asset_kind_class_name)
          );
        }
      } else {
        $("#ms_asset_kind_class").append(
          $("<option></option>")
            .val(value.asset_kind_class_id)
            .html(value.asset_kind_class_name)
        );
      }
    });
  });
}

function GetMerkItem(id, firstload) {
  //get merk item

  var Flag = $("#txhiddenFlag").val();
  var item_brand_id = $("#txhiddenitem_brand_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetItemBrand?item_id=" + id,
    data: "item_id=" + id,
  }).then(function (data) {
    $("#ms_item_brand").find("option").remove();
    $("#ms_item_brand").append(
      $("<option></option>").val("").html("Pilih Merk Item")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (item_brand_id == value.item_brand_id && firstload == "Y") {
          $("#ms_item_brand").append(
            $("<option selected></option>")
              .val(value.item_brand_id)
              .html(value.item_brand)
          );
        } else {
          $("#ms_item_brand").append(
            $("<option></option>")
              .val(value.item_brand_id)
              .html(value.item_brand)
          );
        }
      } else {
        $("#ms_item_brand").append(
          $("<option></option>").val(value.item_brand_id).html(value.item_brand)
        );
      }
    });
  });
}

function GetProduct(id, firstload) {
  //get produk

  var Flag = $("#txhiddenFlag").val();
  var product_id = $("#txhiddenproduct_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsProduct?item_id=" + id,
    data: "item_id=" + id,
  }).then(function (data) {
    $("#product_id").find("option").remove();
    $("#product_id").append(
      $("<option></option>").val("").html("Pilih Produk")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (product_id == value.product_id && firstload == "Y") {
          $("#product_id").append(
            $("<option selected></option>")
              .val(value.product_id)
              .html(value.product_name)
          );
        } else {
          $("#product_id").append(
            $("<option></option>")
              .val(value.product_id)
              .html(value.product_name)
          );
        }
      } else {
        $("#product_id").append(
          $("<option></option>").val(value.product_id).html(value.product_name)
        );
      }
    });
  });
}

function GetMarketingProduct() {
  //get produk marketing
  var Flag = $("#txhiddenFlag").val();
  var product_marketing_id = $("#txhiddenproduct_marketing_id").val();
  var CompanyId = $("#txhiddenCompanyId").val();

  var params = {
    company_id: CompanyId,
  };

  $.ajax({
    type: "GET",
    url: "../CM/GetMsProductMarketing",
    data: params,
  }).then(function (data) {
    $("#product_marketing_id").find("option").remove();
    $("#product_marketing_id").append(
      $("<option></option>").val("").html("Pilih Produk Marketing")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (product_marketing_id == value.product_marketing_id) {
          $("#product_marketing_id").append(
            $("<option selected></option>")
              .val(value.product_marketing_id)
              .html(value.product_marketing_name)
          );
        } else {
          $("#product_marketing_id").append(
            $("<option></option>")
              .val(value.product_marketing_id)
              .html(value.product_marketing_name)
          );
        }
      } else {
        $("#product_marketing_id").append(
          $("<option></option>")
            .val(value.product_marketing_id)
            .html(value.product_marketing_name)
        );
      }
    });
  });
}

function GetSTNKName() {
  //get nama stnk
  var Flag = $("#txhiddenFlag").val();
  var STNK_name_id = $("#txhiddenSTNK_name_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsSTNKName?where=-",
    data: "where=-",
  }).then(function (data) {
    $("#STNKName").find("option").remove();
    $("#STNKName").append(
      $("<option></option>").val("").html("Pilih Nama STNK")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (STNK_name_id == value.stnk_name_id) {
          $("#STNKName").append(
            $("<option selected></option>")
              .val(value.stnk_name_id)
              .html(value.stnk_name_description)
          );

          $("#lblNamaSTNK2").val("Nama STNK " + value.stnk_name_description);
          selectedTextarea = $("#PartnerSTNKName")[0];
          selectedTextarea.placeholder =
            "Nama STNK " + value.stnk_name_description;
        } else {
          $("#STNKName").append(
            $("<option></option>")
              .val(value.stnk_name_id)
              .html(value.stnk_name_description)
          );
        }
      } else {
        $("#STNKName").append(
          $("<option></option>")
            .val(value.stnk_name_id)
            .html(value.stnk_name_description)
        );
      }
    });
  });
}

function GetAsalPO() {
  //get Asal PO

  var Flag = $("#txhiddenFlag").val();
  var provenance_PO_id = $("#txhiddenprovenance_PO_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsProvenancePurchaseOrder?where=-",
    data: "where=-",
  }).then(function (data) {
    $("#provenance_PO_id").find("option").remove();
    $("#provenance_PO_id").append(
      $("<option></option>").val("").html("Pilih Asal PO")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (provenance_PO_id == value.provenance_PO_id) {
          $("#provenance_PO_id").append(
            $("<option selected></option>")
              .val(value.provenance_PO_id)
              .html(value.provenance_PO_description)
          );
        } else {
          $("#provenance_PO_id").append(
            $("<option></option>")
              .val(value.provenance_PO_id)
              .html(value.provenance_PO_description)
          );
        }
      } else {
        $("#provenance_PO_id").append(
          $("<option></option>")
            .val(value.provenance_PO_id)
            .html(value.provenance_PO_description)
        );
      }
    });
  });
}

function GetTipePenggunaan() {
  var Flag = $("#txhiddenFlag").val();
  var usage_type_id = $("#txhiddenusage_type_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsUsageType?where=-",
    data: "where=-",
  }).then(function (data) {
    $("#usage_type_id").find("option").remove();
    $("#usage_type_id").append(
      $("<option></option>").val("").html("Pilih Tipe Penggunaan")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (usage_type_id == value.usage_type_id) {
          $("#usage_type_id").append(
            $("<option selected></option>")
              .val(value.usage_type_id)
              .html(value.usage_type_name)
          );
        } else {
          $("#usage_type_id").append(
            $("<option></option>")
              .val(value.usage_type_id)
              .html(value.usage_type_name)
          );
        }
      } else {
        $("#usage_type_id").append(
          $("<option></option>")
            .val(value.usage_type_id)
            .html(value.usage_type_name)
        );
      }
    });
  });
}

function GetAR() {
  //get AR

  var Flag = $("#txhiddenFlag").val();
  var AR = $("#txhiddenar").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsAR?where=-",
    data: "where=-",
  }).then(function (data) {
    $("#AR_id").find("option").remove();
    $("#AR_id").append($("<option></option>").val("").html("Pilih AR"));

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (AR == value.ar_id) {
          $("#AR_id").append(
            $("<option selected></option>")
              .val(value.ar_id)
              .html(value.ar_name_description)
          );
        } else {
          $("#AR_id").append(
            $("<option></option>")
              .val(value.ar_id)
              .html(value.ar_name_description)
          );
        }
      } else {
        $("#AR_id").append(
          $("<option></option>")
            .val(value.ar_id)
            .html(value.ar_name_description)
        );
      }
    });
  });
}

function GetCoverInsuranceType() {
  //get Cover Insurance Type
  var Flag = $("#txhiddenFlag").val();
  var tipe_cover = $("#txhiddentipe_cover").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsInsuranceCoverType?where=-",
    data: "where=-",
  }).then(function (data) {
    $("#insurance_cover_type_id").find("option").remove();
    //$("#insurance_cover_type_id").append($("<option></option>").val("").html("Pilih Tipe Cover Asuransi"));

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (tipe_cover == value.insurance_cover_type_id) {
          $("#insurance_cover_type_id").append(
            $("<option selected></option>")
              .val(value.insurance_cover_type_id)
              .html(value.insurance_cover_type_name)
          );
        } else {
          $("#insurance_cover_type_id").append(
            $("<option selected></option>")
              .val(value.insurance_cover_type_id)
              .html(value.insurance_cover_type_name)
          );
        }
      } else {
        $("#insurance_cover_type_id").append(
          $("<option selected></option>")
            .val(value.insurance_cover_type_id)
            .html(value.insurance_cover_type_name)
        );
      }
    });
  });
}

function GetInsuranceType() {
  //get Insurance Type

  var Flag = $("#txhiddenFlag").val();
  var insurance_type_id = $("#txhiddeninsurance_type_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsInsuranceType?where=001",
    data: "where=001",
  }).then(function (data) {
    $("#insurance_type_id").find("option").remove();
    /*$("#insurance_type_id").append($("<option></option>").val("").html("Pilih Tipe Asuransi"));*/

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (insurance_type_id == value.insurance_type_id) {
          $("#insurance_type_id").append(
            $("<option selected></option>")
              .val(value.insurance_type_id)
              .html(value.insurance_type_name)
          );
        } else {
          $("#insurance_type_id").append(
            $("<option selected></option>")
              .val(value.insurance_type_id)
              .html(value.insurance_type_name)
          );
        }
      } else {
        $("#insurance_type_id").append(
          $("<option selected></option>")
            .val(value.insurance_type_id)
            .html(value.insurance_type_name)
        );
      }
    });
  });
}

function GetInterestRateType() {
  //get Interest Rate Type

  var Flag = $("#txhiddenFlag").val();
  var interest_rate_type_id = $("#txhiddeninterest_rate_type_id").val();

  $.ajax({
    type: "GET",
    url: "../CM/GetMsInterestRateType?where=-",
    data: "where=-",
  }).then(function (data) {
    $("#interest_rate_type_id").find("option").remove();
    //$("#interest_rate_type_id").append($("<option></option>").val("").html("Pilih Interest Rate Type"));

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (interest_rate_type_id == value.interest_rate_type_id) {
          $("#interest_rate_type_id").append(
            $("<option></option>")
              .val(value.interest_rate_type_id)
              .html(value.interest_rate_type_name)
          );
        } else {
          $("#interest_rate_type_id").append(
            $("<option></option>")
              .val(value.interest_rate_type_id)
              .html(value.interest_rate_type_name)
          );
        }
      } else {
        $("#interest_rate_type_id").append(
          $("<option></option>")
            .val(value.interest_rate_type_id)
            .html(value.interest_rate_type_name)
        );
      }
    });
  });
}

function Calculate() {
  //reset biaya admin
  var IsMegaProteksiGold = $("#hdnIsMegaProteksiGold").val();
  let BiayaAdmin = $("#AdminFee").val().replaceAll(".", "");
  let OldTenor = $("#hdnOldTenor").val();

  let MegaProteksiGold = $("#MegaProteksiGold").val().replaceAll(".", "");
  let OldPengaliMegaProteksiGold = Math.round(OldTenor / 12);

  let tenor = $("#txtenor").val();
  let PengaliMegaProteksiGold = Math.round(tenor / 12);

  if (IsMegaProteksiGold == "1") {
    BiayaAdmin =
      parseFloat(BiayaAdmin) -
      parseFloat(MegaProteksiGold) * parseFloat(OldPengaliMegaProteksiGold);
  }
  //end reset biaya admin

  var is_item_new = $("#is_item_new").val();
  var TipeAplikasi = $("#ms_application_type").val();
  var OTR = $("#OTR").val().replaceAll(".", "");
  var DPGross = $("#DPGross").val().replaceAll(".", "");
  var BiayaProvisi = $("#BiayaProvisi").val().replaceAll(".", "");
  var InsuranceFee = $("#InsuranceFee").val().replaceAll(".", "");
  var AmtInstallment = $("#AmtInstallment").val().replaceAll(".", "");

  if ($("#cbxMegaProteksiGold").is(":checked")) {
    BiayaAdmin =
      parseFloat(BiayaAdmin) +
      parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold);
  }
  //else {
  //    BiayaAdmin = parseFloat(BiayaAdmin) - (parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold));
  //}

  $("#AdminFee").val(numberFormat(BiayaAdmin));

  var AR_id = $("#AR_id").val();
  var ARx = 0;

  var DepositAngsuran = $("#DepositAngsuran").val();
  var PenambahanDepositAngsuran = 0;

  if (DepositAngsuran == "1") {
    var PengaliDepositAngsuran = $("#Deposit").val();
    if (PengaliDepositAngsuran == "") {
      PengaliDepositAngsuran = "0";
    }
    PenambahanDepositAngsuran =
      parseFloat(AmtInstallment) * parseFloat(PengaliDepositAngsuran);
  }

  var CompanyId = $("#txhiddenCompanyId").val();
  var BranchId = $("#txhiddenBranchId").val();
  var asset_kind_id = $("#asset_kind_id").val();
  var JlhPembiayaan = $("#JlhPembiayaan").val().replaceAll(".", "");

  //arrear
  if (AR_id == "1") {
    ARx = 0;
  }
  // advance
  else {
    ARx = AmtInstallment;
  }

  var UangMukaMurni =
    parseFloat(DPGross) -
    parseFloat(BiayaAdmin) -
    parseFloat(BiayaProvisi) -
    parseFloat(InsuranceFee) -
    parseFloat(ARx) +
    PenambahanDepositAngsuran;

  //if (IsMegaProteksiGold == "1") {
  //    UangMukaMurni = parseFloat(UangMukaMurni) - (parseFloat(MegaProteksiGold) * parseFloat(OldPengaliMegaProteksiGold));
  //}

  //if ($("#cbxMegaProteksiGold").is(":checked")) {
  //    UangMukaMurni = parseFloat(UangMukaMurni) + (parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold));
  //}
  ////else {
  ////    UangMukaMurni = parseFloat(UangMukaMurni) - (parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold));
  ////}

  var JlhPembiayaan = "0";

  //if (IsMegaProteksiGold == "1") {
  //    JlhPembiayaan = parseFloat(JlhPembiayaan) - (parseFloat(MegaProteksiGold) * parseFloat(OldPengaliMegaProteksiGold));
  //}

  //if ($("#cbxMegaProteksiGold").is(":checked")) {
  //    JlhPembiayaan = parseFloat(JlhPembiayaan) + (parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold));
  //}
  ////else {
  ////    JlhPembiayaan = parseFloat(JlhPembiayaan) - (parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold));
  ////}

  JlhPembiayaan = parseFloat(OTR) - parseFloat(UangMukaMurni);

  var ResidualValueInp = 0;
  var InAdvanceOrArrear;
  if (AR_id == "1") InAdvanceOrArrear = "A";
  //in arrear
  else InAdvanceOrArrear = "D"; //in advanced

  var EffRate = GetEffectiveRate(
    AmtInstallment,
    tenor,
    JlhPembiayaan,
    ResidualValueInp,
    InAdvanceOrArrear
  );

  var Bunga =
    parseFloat(AmtInstallment) * parseFloat(tenor) - parseFloat(JlhPembiayaan);
  var FlatRate = GetPerBungaFlatTetap(JlhPembiayaan, Bunga, tenor); //Tetap

  var NominalPencairanDealer = parseFloat(OTR) - parseFloat(DPGross);

  //bekas & UMC-0
  if (is_item_new == "0" && TipeAplikasi == "03") {
    var MarketPrice = $("#MarketPrice").val();
    var MaxPencairan = $("#MaxPencairan").val().replaceAll(".", "");
    if (MaxPencairan == "") {
      MaxPencairan = "0";
    }

    if (
      parseFloat(NominalPencairanDealer) > parseFloat(MaxPencairan) &&
      parseFloat(OTR) > parseFloat(MarketPrice)
    ) {
      return "Nominal Pencairan Dealer tidak boleh lebih besar dari Maksimal Pencairan";
    } else {
      $("#NominalPencairanDealer").val(numberFormat(NominalPencairanDealer));
      $("#UangMukaMurni").val(numberFormat(UangMukaMurni));
      $("#JlhPembiayaan").val(numberFormat(JlhPembiayaan));

      if (isNaN(EffRate)) {
        $("#BungaEfektif").val("");
      } else {
        $("#BungaEfektif").val(EffRate);
      }

      if (isNaN(FlatRate)) {
        $("#BungaFlat").val("");
      } else {
        $("#BungaFlat").val(FlatRate);
      }

      hitungPersentaseTACMax();

      if ($("#cbxMegaProteksiGold").is(":checked")) {
        $("#hdnIsMegaProteksiGold").val("1");
      } else {
        $("#hdnIsMegaProteksiGold").val("0");
      }

      $("#hdnOldTenor").val(tenor);

      if ($("#cbxMegaProteksiGold").is(":checked")) {
        $("#hdnMegaProteksiGoldAmount").val(
          parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold)
        );
      } else {
        $("#hdnMegaProteksiGoldAmount").val("0");
      }

      return "Success";
    }
  } else {
    $("#NominalPencairanDealer").val(numberFormat(NominalPencairanDealer));
    $("#UangMukaMurni").val(numberFormat(UangMukaMurni));
    $("#JlhPembiayaan").val(numberFormat(JlhPembiayaan));

    if (isNaN(EffRate)) {
      $("#BungaEfektif").val("");
    } else {
      $("#BungaEfektif").val(EffRate);
    }

    if (isNaN(FlatRate)) {
      $("#BungaFlat").val("");
    } else {
      $("#BungaFlat").val(FlatRate);
    }

    hitungPersentaseTACMax();

    if ($("#cbxMegaProteksiGold").is(":checked")) {
      $("#hdnIsMegaProteksiGold").val("1");
    } else {
      $("#hdnIsMegaProteksiGold").val("0");
    }

    $("#hdnOldTenor").val(tenor);

    if ($("#cbxMegaProteksiGold").is(":checked")) {
      $("#hdnMegaProteksiGoldAmount").val(
        parseFloat(MegaProteksiGold) * parseFloat(PengaliMegaProteksiGold)
      );
    } else {
      $("#hdnMegaProteksiGoldAmount").val("0");
    }

    return "Success";
  }
}

function VisibilityMarketPrice(TipeAplikasi, isItemNew, IsEdit) {
  //bekas & UMC-0
  if (isItemNew == "0" && TipeAplikasi == "03") {
    var CompanyId = $("#txhiddenCompanyId").val();
    var BranchId = $("#txhiddenBranchId").val();
    var credit_id = $("#txcredit_id").val();
    var tipe_kerja_sama = $("#tipekerjasama").val();

    document.getElementById("divMarketPrice").style.display = "block";

    if (IsEdit == "1") {
      var asset_kind_id = $("#txhiddenasset_kind_id").val();
      var asset_type_id = $("#txhiddenitem_brand_type_id").val();
      var Year = $("#txhiddenyear").val();
    } else {
      var asset_kind_id = $("#asset_kind_id").val();
      var asset_type_id = $("#txitem_brand_type_id").val();
      var Year = $("#year").val();
    }

    var STNK_status_id = $("#STNK_status_id").val();
    var BPKB_invoice = $("#BPKB_invoice").val();
    var destination_bank_account_id_umc = $(
      "#destination_bank_account_id_umc"
    ).val();

    if (STNK_status_id == "") {
      STNK_status_id = "1";
    }

    GetMarketPrice(
      asset_kind_id,
      CompanyId,
      BranchId,
      asset_type_id,
      Year,
      credit_id,
      tipe_kerja_sama,
      STNK_status_id,
      BPKB_invoice,
      destination_bank_account_id_umc
    );
  } else {
    document.getElementById("divMarketPrice").style.display = "none";
    $("#MarketPrice").val("");
    $("#MaxPencairan").val("");
  }
}

function GetInsuranceFee() {
  var OTR = $("#OTR").val().replaceAll(".", "");
  var CompanyId = $("#txhiddenCompanyId").val();
  var BranchId = $("#txhiddenBranchId").val();
  var asset_kind_id = $("#asset_kind_id").val();
  var tenor = $("#txtenor").val();

  var params = {
    asset_kind_id: asset_kind_id,
    OTR: OTR,
    CompanyId: CompanyId,
    BranchId: BranchId,
    Tenor: tenor,
  };

  $.ajax({
    type: "GET",
    url: "../CM/GetInsuranceFee",
    data: params,
  }).then(function (data) {
    $.each(data, function (key, value) {
      $("#InsuranceFee").val(numberFormat(value.insurance_fee));
    });
  });
}

function GetMarketPrice(
  asset_kind_id,
  CompanyId,
  BranchId,
  asset_type_id,
  Year,
  credit_id,
  tipe_kerja_sama,
  status_stnk_id,
  Faktur_BPKB,
  destination_bank_account_id_umc
) {
  document.getElementById("WarningMarketPrice").style.display = "none";

  var paramsMarketPrice = {
    asset_kind_id: asset_kind_id,
    CompanyId: CompanyId,
    BranchId: BranchId,
    asset_type_id: asset_type_id,
    Year: Year,
    credit_id: credit_id,
    tipe_kerja_sama: tipe_kerja_sama,
    status_stnk_id: status_stnk_id,
    Faktur_BPKB: Faktur_BPKB,
    destination_bank_account_id_umc: destination_bank_account_id_umc,
  };

  $.ajax({
    type: "GET",
    url: "../CM/GetMarketPrice",
    data: paramsMarketPrice,
  }).then(function (data) {
    $.each(data, function (key, value) {
      $("#MarketPrice").val(numberFormat(value.market_price));
      $("#MaxPencairan").val(numberFormat(value.max_plafon_pencairan));

      if (numberFormat(value.market_price) == "0") {
        document.getElementById("WarningMarketPrice").style.display = "block";
      }
    });
  });
}

function GetChasisCode(year) {
  var params = {
    year: year,
  };

  $.ajax({
    type: "GET",
    url: "../CM/GetChasisCode",
    data: params,
  }).then(function (data) {
    $("#hdnchasis_code").val(data.chasis_code);
  });
}

var GetEffectiveRate = function (
  Installment,
  Tenor,
  JmlPembiayaan,
  OSPembiayaan,
  AdvanceOrArrear
) {
  if (AdvanceOrArrear == "D") {
    Tenor = Tenor - 1;
    JmlPembiayaan = JmlPembiayaan - Installment;
  }
  var f_IRR_rate = 0.005;
  var f_addition = 0.05;
  var f_temp_irr = f_IRR_rate + f_addition;
  var f_loop_period = 1;
  var f_trial_counter = 1;
  var f_npv_temp = 0;
  var f_npv_update = 0;
  var f_comparison_check = 3;
  while (f_trial_counter <= 1000) {
    f_loop_period = 1;
    f_npv_update = 0;
    while (f_loop_period <= Tenor) {
      if (f_loop_period == Tenor) {
        f_npv_update =
          f_npv_update +
          (parseFloat(Installment) + parseFloat(OSPembiayaan)) /
            Math.pow(1 + f_temp_irr, f_loop_period);
      } else {
        f_npv_update =
          f_npv_update + Installment / Math.pow(1 + f_temp_irr, f_loop_period);
      }
      f_loop_period = f_loop_period + 1;
    }

    if (f_npv_update == JmlPembiayaan) {
      break;
    }
    if (f_npv_update > JmlPembiayaan) {
      f_addition = f_addition / 2;
      f_temp_irr = f_temp_irr + f_addition;
    } else {
      f_addition = f_addition * 2;
      f_temp_irr = f_temp_irr - f_addition;
    }
    if (f_npv_update == f_npv_temp) {
      if (f_comparison_check == 0) {
        break;
      }
      f_comparison_check = f_comparison_check - 1;
    } else {
      f_comparison_check = 3;
      f_npv_temp = f_npv_update;
    }
    f_trial_counter = f_trial_counter + 1;
  }
  f_IRR_rate = Math.round(f_temp_irr * 1200 * 10000) / 10000;
  return f_IRR_rate;
};

var GetPerBungaFlatTetap = function (Pembiayaan, Bunga, Tenor) {
  var FlatRate = 0;
  if (Tenor == 0 || Pembiayaan == 0) {
    FlatRate = 0;
  } else {
    FlatRate =
      Math.round(((Bunga * (12 / Tenor)) / Pembiayaan) * 100 * 10000) / 10000;
  }
  return FlatRate;
};

function hitungPersentaseTACMax() {
  var tenor = $("#txtenor").val();
  var AmtInstallment = $("#AmtInstallment").val().replaceAll(".", "");
  var JlhPembiayaan = $("#JlhPembiayaan").val().replaceAll(".", "");
  var AdminFee = $("#AdminFee").val().replaceAll(".", "");
  var Provisi = $("#BiayaProvisi").val().replaceAll(".", "");
  var Insurance = $("#InsuranceFee").val().replaceAll(".", "");
  var discUM = $("#BiayaAdminDealer").val();
  var komperMax = $("#komper_max").val().replaceAll(".", "");
  var tac_maxDesc = $("#tac_maxDesc").val().replaceAll(".", "");

  if (discUM == "") {
    discUM = "0";
  }

  if (komperMax == "") {
    komperMax = "0";
  }

  var arTAC = parseFloat(tenor) * parseFloat(AmtInstallment);
  var opTAC = parseFloat(JlhPembiayaan);
  var uliTAC = parseFloat(arTAC) - parseFloat(opTAC);
  //var biayaAdmin = THEWEBFORM.cbxAdminFeeKredit.Checked ? (decimal)THEWEBFORM.nmbAdminFee2.decimalValue : (decimal)THEWEBFORM.txbBiayaAdmin.decimalValue;
  var biayaAdmin = AdminFee;
  var biayaProvisi = 0;
  if (Provisi != "") {
    biayaProvisi = Provisi;
  }

  var insuranceFee = 0;
  if (Insurance != "") {
    insuranceFee = Insurance;
  }

  var tac =
    0.15 *
    (parseFloat(uliTAC) +
      parseFloat(biayaAdmin) +
      0.25 * parseFloat(insuranceFee) +
      parseFloat(biayaProvisi));
  $("#TAC_max").val(tac);

  var PersentaseTACMax =
    (parseFloat(discUM) / (parseFloat(tac) / parseFloat(tac_maxDesc))) * 100;
  $("#TAC_maxPersentase").val(PersentaseTACMax.toFixed(4) + " %");

  var tacpersenKomper =
    (parseFloat(komperMax) / (parseFloat(tac) / parseFloat(tac_maxDesc))) * 100;
  $("#TAC_maxKomperPersentase").val(tacpersenKomper.toFixed(4) + " %");
}

var GetAngsuranTetap = function (Pembiayaan, Tenor, PersenFlat) {
  var GetAngs = 0;
  if (Tenor == 0) {
    GetAngs = 0;
  } else {
    GetAngs =
      ((((Pembiayaan * PersenFlat) / 100) * Tenor) / 12 + Pembiayaan) / Tenor;
    Math.round(GetAngs);
  }
  return Math.round(GetAngs);
};

var HitungEffRate = function (val) {
  var TenorGet = $("#txtenor").val(); //remove comma ; effect numeric box
  var AssetCostGet = $("#OTR").val().replaceAll(".", "");
  var AdminFeeGet = $("#AdminFee").val().replaceAll(".", "");
  var InsuranceGet = $("#InsuranceFee").val().replaceAll(".", "");
  var AngsuranGet = $("#AmtInstallment").val().replaceAll(".", "");

  var DepositGet = $("#DPGross").val().replaceAll(".", "");

  var Tenor = parseInt(TenorGet);
  var AssetCost = parseFloat(AssetCostGet);
  var AdminFee = parseFloat(AdminFeeGet);
  var Insurance = parseFloat(InsuranceGet);
  var Angsuran = parseFloat(AngsuranGet);
  var Deposit = parseFloat(DepositGet);

  var AR = parseInt($("#AR_id").val());
  var tipeAngsuran = "0002";
  var TipeKendaraan = $("#asset_kind_id").val();
  var IsItemNew = $("#is_item_new").val();
  var KaliDimuka;

  // tambahan mokas mega solusi
  var jmlhpencairan;

  if ($("#ms_application_type").val() == "03") {
    jmlhpencairan = AssetCostGet - DepositGet;
  } else {
    jmlhpencairan = 0;
  }

  if (isNaN(jmlhpencairan)) {
    $("#NominalPencairanDealer").val("0");
  } else {
    $("#NominalPencairanDealer").val(numberFormat(jmlhpencairan));
  }

  var Pembiayaan;
  var Bunga;

  var EffRate;
  var FlatRate;

  var InsTipeBayar;

  var InsuranceLossGet;

  var ProvisiFee = 0;
  var ProvisiFeeINS = 0;

  if (AR == 0) KaliDimuka = 1;
  else KaliDimuka = 0;

  var InAdvanceOrArrear;
  if (AR == 1) InAdvanceOrArrear = "A";
  //'R'; //in arrear
  else InAdvanceOrArrear = "D"; //'A'; //in advanced

  var biayaProvisiINS = $("#BiayaProvisi").val().replaceAll(".", "");
  ProvisiFeeINS = biayaProvisiINS;

  var DPMurni = 0;

  DPMurni = Deposit - Insurance - AdminFee - ProvisiFeeINS;

  if (isNaN(DPMurni)) {
    $("#UangMukaMurni").val("");
  } else {
    $("#UangMukaMurni").val(numberFormat(DPMurni));
  }

  Pembiayaan = AssetCost - DPMurni;

  if (isNaN(Pembiayaan)) {
    $("#JlhPembiayaan").val("");
  } else {
    $("#JlhPembiayaan").val(numberFormat(Pembiayaan));
  }

  Bunga = Angsuran * Tenor - Pembiayaan;

  var ResidualValueInp = 0;

  FlatRate = $("#BungaFlat").val();
  Angsuran = GetAngsuranTetap(Pembiayaan, Tenor, FlatRate); //Tetap
  $("#AmtInstallment").val(numberFormat(Angsuran));

  EffRate = GetEffectiveRate(
    Angsuran,
    Tenor,
    Pembiayaan,
    ResidualValueInp,
    InAdvanceOrArrear
  );
  if (isNaN(EffRate)) {
    $("#BungaEfektif").val("");
  } else {
    $("#BungaEfektif").val(EffRate);
  }
};

function SetFullPlatNo() {
  var prefix_plat = $("#ddlPlatPrefix").val();
  var plat_no = $("#txtNoPlat").val();
  var plat_code = $("#txtPlatCode").val();

  $("#hdnNoPlat").val(
    prefix_plat + plat_no.replaceAll(" ", "") + plat_code.replaceAll(" ", "")
  );
}

function GetBankNameDestination(destination_bank_account_id_umc) {
  var Flag = $("#txhiddenFlag").val();

  var params = {
    destination_bank_account_id_umc: destination_bank_account_id_umc,
  };

  $.ajax({
    type: "GET",
    url: "../CM/GetBankNameDestination",
    data: params,
  }).then(function (data) {
    $("#destination_bank_id_umc").find("option").remove();
    $("#destination_bank_id_umc").append(
      $("<option></option>").val("").html("Pilih Nama Bank")
    );

    $.each(data, function (key, value) {
      if (Flag != "Add") {
        if (destination_bank_account_id_umc == value.bank_id) {
          $("#destination_bank_id_umc").append(
            $("<option selected></option>")
              .val(value.bank_id)
              .html(value.bank_name)
          );
        } else {
          $("#destination_bank_id_umc").append(
            $("<option></option>").val(value.bank_id).html(value.bank_name)
          );
        }

        var destination_bank_id_umc = $("#hdndestination_bank_id_umc").val();
        if (destination_bank_id_umc != "") {
          $("#destination_bank_id_umc")
            .find('option[value="' + destination_bank_id_umc + '"]')
            .attr("selected", "selected");
        }
      } else {
        $("#destination_bank_id_umc").append(
          $("<option></option>").val(value.bank_id).html(value.bank_name)
        );
      }
    });
  });
}

function GetMarketPrice2() {
  var CompanyId = $("#txhiddenCompanyId").val();
  var BranchId = $("#txhiddenBranchId").val();
  var credit_id = $("#txcredit_id").val();
  var tipe_kerja_sama = $("#tipekerjasama").val();
  var asset_kind_id = $("#asset_kind_id").val();
  var asset_type_id = $("#txitem_brand_type_id").val();
  var Year = $("#year").val();

  var STNK_status_id = $("#STNK_status_id").val();
  var BPKB_invoice = $("#BPKB_invoice").val();
  var destination_bank_account_id_umc = $(
    "#destination_bank_account_id_umc"
  ).val();

  if (STNK_status_id == "") {
    STNK_status_id = "1";
  }

  GetMarketPrice(
    asset_kind_id,
    CompanyId,
    BranchId,
    asset_type_id,
    Year,
    credit_id,
    tipe_kerja_sama,
    STNK_status_id,
    BPKB_invoice,
    destination_bank_account_id_umc
  );
}
