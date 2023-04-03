$(document).ready(function () {
    document.getElementById("divTipeKerjasamaPerantara").style.display = "none";
    $('#tipekerjasama').removeAttr("required");
    $('#tipeperantara').removeAttr("required");
    $('#txPerantara').removeAttr("required");
    $('#pemilikrekening').removeAttr("required");
    $('#rekeningAtasNama').removeAttr("required");
    $('#txBankName').removeAttr("required");
    $('#NoRekening').removeAttr("required");

    document.getElementById("divBiayaAsuransiMega").style.display = "none";
    document.getElementById("divBiayaHP").style.display = "none";

    $("#cbxKreditAdminFee").prop("checked", false);
    $("#cbxKreditBiayaProses").prop("checked", false);
    $("#cbxKreditBiayaProvisiDealer").prop("checked", false);
    $("#cbxKreditBiayaAsuransiMega").prop("checked", false);
    $("#cbxKreditBiayaHandphone").prop("checked", false);

    //get produk marketing
    GetMarketingProduct("Y");

    //get nama stnk
    GetSTNKName("Y");

    //get Asal PO
    GetAsalPO("Y");

    //get Tipe Penggunaan
    GetTipePenggunaan("Y");

    //get AR
    GetAR("Y");

    //get Cover Insurance Type
    GetCoverInsuranceType("Y");

    ////get Insurance Type
    //GetInsuranceType();

    //get Interest Rate Type
    GetInterestRateType("Y");

    var Flag = $('#txhiddenFlag').val();
    var asset_kind_id = $('#txhiddenasset_kind_id').val();
    var application_type_id = $('#txhiddenapplication_type_id').val();
    var is_item_new = $('#txhiddenis_item_new').val();
    var product_finance_id = $('#txhiddenproduct_finance_id').val();
    var item_brand_id = $('#txhiddenitem_brand_id').val();
    var asset_kind_class_id = $('#txhiddenasset_kind_class_id').val();
    var item_brand_type_id = $('#txhiddenitem_brand_type_id').val();
    var item_type_name = $('#txhiddenitem_type_name').val();
    var CC = $('#txhiddenCC').val();
    var product_id = $('#txhiddenproduct_id').val();
    var dealercode = $('#txhiddendealer_code').val();
    var dealername = $('#txhiddendealer_name').val();
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

    var interest_rate_type_id = $("#txhiddeninterest_rate_type_id").val();
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

    var is_avalis = $("#txhiddenis_avalis").val();
    var installment_type = $("#txhiddeninstallment_type").val();
    var paket_pembiayaan = $("#txhiddenpaket_pembiayaan").val();
    var biaya_proses = $("#txhiddenbiaya_proses").val();

    var admin_fee_kredit = $("#txhiddenadmin_fee_kredit").val();

    var biaya_provisi = $("#txhiddenbiaya_provisi").val();
    var biaya_provisi_kredit = $("#txhiddenbiaya_provisi_kredit").val();

    var nominal_biaya_proses = $("#txhiddennominal_biaya_proses").val();
    var nominal_biaya_proses_kredit = $("#txhiddennominal_biaya_proses_kredit").val();
    var loss_fee = $("#txhiddenloss_fee").val();
    var loss_fee_kredit = $("#txhiddenloss_fee_kredit").val();
    var provisi_ins_fee = $("#txhiddenprovisi_ins_fee").val();
    var provisi_ins_fee_kredit = $("#txhiddenprovisi_ins_fee_kredit").val();
    var customer_pay_amount = $("#txhiddencustomer_pay_amount").val();
    var installment_code = $("#txhiddeninstallment_code").val();
    var residual_value = $("#txhiddenresidual_value").val();
    var discount_dealer = $("#txhiddendiscount_dealer").val();
    var ongkos_BBN = $("#txhiddenongkos_BBN").val();
    var ongkos_tagih = $("#txhiddenongkos_tagih").val();
    var SubsidiFinance = $("#txhiddenSubsidiFinance").val();
    var SubsidiDealer = $("#txhiddenSubsidiDealer").val();
    var SubsidiMainDealer = $("#txhiddenSubsidiMainDealer").val();
    var SubsidiATPM = $("#txhiddenSubsidiATPM").val();
    var SubsidiPihakKetiga = $("#txhiddenSubsidiPihakKetiga").val();
    var SubsidiBunga = $("#txhiddenSubsidiBunga").val();
    var mega_insurance_fee = $("#txhiddenmega_insurance_fee").val();
    var mega_insurance_fee_kredit = $("#txhiddenmega_insurance_fee_kredit").val();
    var handphone_fee = $("#txhiddenhandphone_fee").val();
    var handphone_fee_kredit = $("#txhiddenhandphone_fee_kredit").val();
    var is_life_ins = $("#txhiddenis_life_ins").val();

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

    if (Flag != "Add") {
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

        //get merk item
        GetMerkItem(asset_kind_id, "Y");

        //get model item
        GetModelItem(asset_kind_id, "Y");

        //get produk
        GetProduct(asset_kind_id, "Y");

        $("#hdis_item_newSelecytedValue").val(is_item_new);
        $("#ms_application_type").find('option[value="' + application_type_id + '"]').attr('selected', 'selected');
        $("#ms_product_finance").find('option[value="' + product_finance_id + '"]').attr('selected', 'selected');
        $("#is_item_new").find('option[value="' + is_item_new + '"]').attr('selected', 'selected');
        $("#asset_kind_id").find('option[value="' + asset_kind_id + '"]').attr('selected', 'selected');

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
        $("#txtenor").val(tenor);
        $("#OTR").val(numberFormat(OTR));
        $("#DPGross").val(numberFormat(gross_down_payment));

        GetAdminProvisiInterestFee(paket_pembiayaan, tenor, OTR.replaceAll(".", ""), AR);
        GetProcessProvisiIns(biaya_proses, tenor, OTR, tipe_cover, $("#txhiddenBranchId").val(), year, $("#txcredit_id").val(), asset_kind_class_id, loss_fee.replaceAll(".", ""), loss_fee_kredit.replaceAll(".", ""), usage_type_id);

        AngsuranChange();
        HitungEffRate(1);

        $("#AdminFee").val(numberFormat(admin_fee));
        $("#BiayaProvisiDealer").val(numberFormat(biaya_provisi));
        $("#InsuranceFee").val(numberFormat(insurance_fee));
        $("#UangMukaMurni").val(numberFormat(uang_muka_murni_kons));
        $("#JlhPembiayaan").val(numberFormat(jml_pembiayaan));
        $("#AmtInstallment").val(numberFormat(amount_installment));
        $("#BungaEfektif").val(effective_rate);
        $("#BungaFlat").val(flat_rate);
        $("#BiayaAdminDealer").val(numberFormat(disc_deposit));
        $("#overdue_rate").val(overdue_rate);
        $("#disc_type").val(disc_type);
        $("#TAC_max").val(TAC_max);

        //Standart
        if (application_type_id == "02") {
            $('#disc_type').removeAttr("disabled");
            $('#is_item_new').removeAttr("disabled");
        }
        //UMC - 0
        else if (application_type_id == "03") {
            $('#disc_type').attr("disabled", true);
            $('#is_item_new').attr("disabled", true);

            document.getElementById("divTipeKerjasamaPerantara").style.display = "block";
            $('#tipekerjasama').attr("required", true);
            $('#tipeperantara').attr("required", true);
            $('#txPerantara').attr("required", true);
            $('#pemilikrekening').attr("required", true);
            $('#rekeningAtasNama').attr("required", true);
            $('#txBankName').attr("required", true);
            $('#NoRekening').attr("required", true);
        }
        else {
            $('#disc_type').removeAttr("disabled");
            $('#is_item_new').removeAttr("disabled");
        }

        //get tahun kendaraan
        GetYear(asset_kind_id, item_brand_id, asset_kind_class_id, item_brand_type_id, "Y");

        var NominalPencairanDealer = parseFloat(OTR) - parseFloat(gross_down_payment);
        $("#NominalPencairanDealer").val(numberFormat(NominalPencairanDealer));

        $("#avalis").find('option[value="' + is_avalis + '"]').attr('selected', 'selected');
        $("#AngsuranTipe").find('option[value="' + installment_type + '"]').attr('selected', 'selected');

        //get paket pembiayaan
        GetFinancingPackage(tenor, OTR.replaceAll(".", ""), AR, "Y");
        GetProcessFee(tenor, OTR.replaceAll(".", ""), tipe_cover, "Y");

        $("#AdminFee").attr("readonly", false);
        $("#KreditAdminFee").attr("readonly", true);

        $("#KreditAdminFee").val(numberFormat(admin_fee_kredit));
        if ($("#KreditAdminFee").val() != "0") {
            $("#cbxKreditAdminFee").prop("checked", true);
            $("#AdminFee").attr("readonly", true);
            $("#KreditAdminFee").attr("readonly", false);
        }

        $("#BiayaProvisiDealer").val(numberFormat(biaya_provisi));
        $("#KreditBiayaProvisiDealer").val(numberFormat(biaya_provisi_kredit));
        if ($("#KreditBiayaProvisiDealer").val() != "0") {
            $("#cbxKreditBiayaProvisiDealer").prop("checked", true);
            $("#BiayaProvisiDealer").attr("readonly", true);
            $("#KreditBiayaProvisiDealer").attr("readonly", false);
        }

        $("#BiayaProses").val(numberFormat(nominal_biaya_proses));
        $("#KreditBiayaProses").val(numberFormat(nominal_biaya_proses_kredit));
        if ($("#KreditBiayaProses").val() != "0") {
            $("#cbxKreditBiayaProses").prop("checked", true);
        }

        $("#BiayaAsuransiKerugian").val(numberFormat(loss_fee));
        $("#KreditBiayaAsuransiKerugian").val(numberFormat(loss_fee_kredit));

        $("#BiayaProvisi").val(numberFormat(provisi_ins_fee));
        $("#KreditBiayaProvisiAsuransi").val(numberFormat(provisi_ins_fee_kredit));

        $("#KodeAngsuran").find('option[value="' + installment_code + '"]').attr('selected', 'selected');

        $("#PembayaranKonsumen").val(numberFormat(customer_pay_amount));
        $("#ResidualValue").val(numberFormat(residual_value));
        $("#BiayaDiscountDealer").val(numberFormat(discount_dealer));
        $("#BiayaBBN").val(numberFormat(ongkos_BBN));
        $("#BiayaTagih").val(numberFormat(ongkos_tagih));
        $("#SubsidiFinance").val(numberFormat(SubsidiFinance));
        $("#SubsidiDealer").val(numberFormat(SubsidiDealer));
        $("#SubsidiMainDealer").val(numberFormat(SubsidiMainDealer));
        $("#SubsidiATPM").val(numberFormat(SubsidiATPM));
        $("#SubsidiPihakKetiga").val(numberFormat(SubsidiPihakKetiga));
        $("#SubsidiBunga").val(numberFormat(SubsidiBunga));
        $("#BiayaAsuransiMega").val(numberFormat(mega_insurance_fee));
        $("#KreditBiayaAsuransiMega").val(numberFormat(mega_insurance_fee_kredit));
        $("#BiayaHandphone").val(numberFormat(handphone_fee));
        $("#KreditBiayaHandphone").val(numberFormat(handphone_fee_kredit));

        $("#BiayaAsuransiMega").val(numberFormat(mega_insurance_fee));
        $("#KreditBiayaAsuransiMega").val(numberFormat(mega_insurance_fee_kredit));
        if ($("#KreditBiayaProses").val() != "0") {
            $("#cbxKreditBiayaAsuransiMega").prop("checked", true);
        }

        $("#BiayaHandphone").val(numberFormat(handphone_fee));
        $("#KreditBiayaHandphone").val(numberFormat(handphone_fee_kredit));
        if ($("#KreditBiayaProses").val() != "0") {
            $("#cbxKreditBiayaHandphone").prop("checked", true);
        }

        $("#cbxKreditBiayaAsuransiMega").attr("readonly", true);
        $("#cbxKreditBiayaHandphone").attr("readonly", true);

        $("#asuransi_jiwa_mega").find('option[value="' + is_life_ins + '"]').attr('selected', 'selected');

        document.getElementById("divBiayaAsuransiMega").style.display = "none";
        document.getElementById("divBiayaHP").style.display = "none";

        if (is_life_ins == "1") {
            document.getElementById("divBiayaAsuransiMega").style.display = "block";
            document.getElementById("divBiayaHP").style.display = "block";
        }

        hitungSubsidiPihakKetiga();
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
        $("#interest_rate_type_id").attr("disabled", true);
        $("#txtenor").attr("disabled", true);
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
        $("#avalis").attr("disabled", true);
        $("#AngsuranTipe").attr("disabled", true);
        $("#paket_pembiayaan").attr("disabled", true);
        $("#biaya_proses").attr("disabled", true);
        $("#KreditAdminFee").attr("disabled", true);
        $("#BiayaProses").attr("disabled", true);
        $("#KreditBiayaProses").attr("disabled", true);
        $("#BiayaAsuransiKerugian").attr("disabled", true);
        $("#KreditBiayaAsuransiKerugian").attr("disabled", true);
        $("#BiayaProvisi").attr("disabled", true);
        $("#KreditBiayaProvisiAsuransi").attr("disabled", true);
        $("#PembayaranKonsumen").attr("disabled", true);
        $("#KodeAngsuran").attr("disabled", true);
        $("#ResidualValue").attr("disabled", true);
        $("#BiayaDiscountDealer").attr("disabled", true);
        $("#BiayaBBN").attr("disabled", true);
        $("#BiayaTagih").attr("disabled", true);
        $("#SubsidiFinance").attr("disabled", true);
        $("#SubsidiDealer").attr("disabled", true);
        $("#SubsidiMainDealer").attr("disabled", true);
        $("#SubsidiATPM").attr("disabled", true);
        $("#SubsidiPihakKetiga").attr("disabled", true);
        $("#SubsidiBunga").attr("disabled", true);
        $("#BiayaAsuransiMega").attr("disabled", true);
        $("#KreditBiayaAsuransiMega").attr("disabled", true);
        $("#BiayaHandphone").attr("disabled", true);
        $("#KreditBiayaHandphone").attr("disabled", true);
        $("#asuransi_jiwa_mega").attr("disabled", true);
        $("#BiayaProvisiDealer").attr("disabled", true);
        $("#NominalPencairanDealer").attr("disabled", true);
        $("#KreditBiayaProvisiDealer").attr("disabled", true);
        $("#cbxKreditAdminFee").attr("disabled", true);
        $("#cbxKreditBiayaProses").attr("disabled", true);
        $("#cbxKreditBiayaProvisiDealer").attr("disabled", true);

        $("#tipekerjasama").attr("disabled", true);
        $("#tipeperantara").attr("disabled", true);
        $("#txPerantara").attr("disabled", true);
        $("#btnshow_Perantara").hide();
        $("#pemilikrekening").attr("disabled", true);
        $("#txBankName").attr("disabled", true);
        $("#btnshow_BankName").hide();
        $("#NoRekening").attr("disabled", true);
        $("#rekeningAtasNama").attr("disabled", true);
    }
});

jQuery(document).ready(function () {
    cm_load.init();

    //Setup responsiove UI on Mobile
    const pnlAsset = document.getElementById("Asset_more");
    pnlAsset?.addEventListener('click', () => {
        cm_load.changeClass_Asset();
    });

    const pnlPrice = document.getElementById("Price_more");
    pnlPrice?.addEventListener('click', () => {
        cm_load.changeClass_Price();
    });

    $('#is_item_new').change(function () {
        $("#hdis_item_newSelecytedValue").val($('#is_item_new').val());
    });

    $('#paket_pembiayaan').change(function () {
        GetAdminProvisiInterestFee($('#paket_pembiayaan').val(), $('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#AR_id').val());
        HitungEffRate(1);
    });

    $('#biaya_proses').change(function () {
        var BiayaProsesID = $('#biaya_proses').val();
        var Tenor = $('#txtenor').val();
        var OTR = $('#OTR').val().replaceAll(".", "");
        var InsCoverType = $('#insurance_cover_type_id').val();
        var BranchId = $('#txhiddenBranchId').val();
        var ItemYear = $('#year').val();
        var credit_id = $('#txcredit_id').val();
        var modelid = $('#ms_asset_kind_class').val();
        var loss_fee = $('#BiayaAsuransiKerugian').val();
        var loss_fee_kredit = $('#KreditBiayaAsuransiKerugian').val();
        var usage_type_id = $('#usage_type_id').val();

        GetProcessProvisiIns(BiayaProsesID, Tenor, OTR, InsCoverType, BranchId, ItemYear, credit_id, modelid, loss_fee.replaceAll(".", ""), loss_fee_kredit.replaceAll(".", ""), usage_type_id);
        HitungEffRate(1);
        HitungJumlahAngsuran();
        //GetBiayaProvisiDealer(1);
    });

    $('#asuransi_jiwa_mega').change(function () {
        $("#BiayaAsuransiMega").val("0");
        $("#BiayaHandphone").val("0");
        $("#KreditBiayaAsuransiMega").val("0");
        $("#KreditBiayaHandphone").val("0");

        $("#cbxKreditBiayaAsuransiMega").prop("checked", false);
        $("#cbxKreditBiayaHandphone").prop("checked", false);
        $("#cbxKreditBiayaAsuransiMega").attr("disabled", false);
        $("#cbxKreditBiayaHandphone").attr("disabled", false);

        document.getElementById("divBiayaAsuransiMega").style.display = "none";
        document.getElementById("divBiayaHP").style.display = "none";

        if ($('#asuransi_jiwa_mega').val() == "1") {
            document.getElementById("divBiayaAsuransiMega").style.display = "block";
            document.getElementById("divBiayaHP").style.display = "block";

            var OTR = $('#OTR').val();
            var DP = $('#UangMukaMurni').val();
            var AdminFeeKredit = $('#KreditAdminFee').val();
            var ProvisiFeeKredit = $('#KreditBiayaProvisiDealer').val();
            var BiayaProsesKredit = $('#KreditBiayaProses').val();
            var BranchIdAsuransi = $('#txhiddenBranchId').val();
            var Tenor = $('#txtenor').val();

            if (OTR == "") {
                OTR = "0"
            }

            if (DP == "") {
                DP = "0"
            }

            if (AdminFeeKredit == "") {
                AdminFeeKredit = "0"
            }

            if (ProvisiFeeKredit == "") {
                ProvisiFeeKredit = "0"
            }

            if (BiayaProsesKredit == "") {
                BiayaProsesKredit = "0"
            }

            if (Tenor == "") {
                Tenor = "0"
            }

            GetLifeInsuranceCredit(OTR, DP, AdminFeeKredit, ProvisiFeeKredit, BiayaProsesKredit, BranchIdAsuransi, Tenor);

            $("#BiayaAsuransiMega").val("0");
            $("#BiayaHandphone").val("0");

            $("#cbxKreditBiayaAsuransiMega").prop("checked", true);
            $("#cbxKreditBiayaHandphone").prop("checked", true);

            $("#cbxKreditBiayaAsuransiMega").attr("disabled", true);
            $("#cbxKreditBiayaHandphone").attr("disabled", true);
        }

        HitungEffRate(1);
    });

    $("#cbxKreditAdminFee").change(function () {
        $("#KreditAdminFee").removeAttr("readonly");
        $("#AdminFee").removeAttr("readonly");

        if ($("#cbxKreditAdminFee").is(":checked")) {
            $("#AdminFee").attr("readonly", true);
            $("#KreditAdminFee").val($("#AdminFee").val());
            $("#AdminFee").val("0");
        }
        else {
            $("#KreditAdminFee").attr("readonly", true);
            $("#AdminFee").val($("#KreditAdminFee").val());
            $("#KreditAdminFee").val("0");
        }

        HitungEffRate(1);
    });

    $("#cbxKreditBiayaProses").change(function () {
        if ($("#cbxKreditBiayaProses").is(":checked")) {
            $("#KreditBiayaProses").val($("#BiayaProses").val());
            $("#BiayaProses").val("0");

            $("#KreditBiayaAsuransiKerugian").val($("#BiayaAsuransiKerugian").val());
            $("#BiayaAsuransiKerugian").val("0");

            $("#KreditBiayaProvisiAsuransi").val($("#BiayaProvisi").val());
            $("#BiayaProvisi").val("0");
        }
        else {
            $("#BiayaProses").val($("#KreditBiayaProses").val());
            $("#KreditBiayaProses").val("0");

            $("#BiayaAsuransiKerugian").val($("#KreditBiayaAsuransiKerugian").val());
            $("#KreditBiayaAsuransiKerugian").val("0");

            $("#BiayaProvisi").val($("#KreditBiayaProvisiAsuransi").val());
            $("#KreditBiayaProvisiAsuransi").val("0");
        }

        HitungEffRate(1);
    });

    $("#cbxKreditBiayaProvisiDealer").change(function () {
        $("#KreditBiayaProvisiDealer").removeAttr("readonly");
        $("#BiayaProvisiDealer").removeAttr("readonly");

        if ($("#cbxKreditBiayaProvisiDealer").is(":checked")) {
            $("#BiayaProvisiDealer").attr("readonly", true);
            $("#KreditBiayaProvisiDealer").val($("#BiayaProvisiDealer").val());
            $("#BiayaProvisiDealer").val("0");
        }
        else {
            $("#KreditBiayaProvisiDealer").attr("readonly", true);
            $("#BiayaProvisiDealer").val($("#KreditBiayaProvisiDealer").val());
            $("#KreditBiayaProvisiDealer").val("0");
        }

        HitungEffRate(1);
    });

    $("#ms_item_brand").change(function () {
        //get tahun kendaraan
        var item_id = $("#asset_kind_id").val();
        var Item_Brand_Id = $("#ms_item_brand").val();
        var asset_kind_class_id = $("#ms_asset_kind_class").val();
        var asset_type_id = $("#txitem_brand_type_id").val();

        GetYear(asset_kind_id, item_brand_id, asset_kind_class_id, item_brand_type_id, "N");
    });

    $("#ms_asset_kind_class").change(function () {
        //get tahun kendaraan
        var item_id = $("#asset_kind_id").val();
        var Item_Brand_Id = $("#ms_item_brand").val();
        var asset_kind_class_id = $("#ms_asset_kind_class").val();
        var asset_type_id = $("#txitem_brand_type_id").val();

        GetYear(asset_kind_id, item_brand_id, asset_kind_class_id, item_brand_type_id, "N");
    });

    $("#STNKName").change(function () {
        $("#lblNamaSTNK2").val("Nama STNK " + $("#STNKName option:selected").text());
        selectedTextarea = $('#PartnerSTNKName')[0];
        selectedTextarea.placeholder = "Nama STNK " + $("#STNKName option:selected").text();
    });

    $("#AR_id").change(function () {
        GetFinancingPackage($('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#AR_id').val(), "N");
        HitungEffRate(1);
    });

    $("#txtenor").change(function () {
        GetFinancingPackage($('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#AR_id').val(), "N");
        GetProcessFee($('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#insurance_cover_type_id').val(), "N");
        GetInsuranceFee();
        HitungEffRate(1);
    });

    $("#OTR").keyup(function () {
        $("#OTR").val(numberFormat($("#OTR").val().replaceAll(".", "")));
    });

    $("#OTR").change(function () {
        GetFinancingPackage($('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#AR_id').val(), "N");
        GetProcessFee($('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#insurance_cover_type_id').val(), "N");
        GetInsuranceFee();
        HitungEffRate(1);
    });

    $("#DPGross").keyup(function () {
        $("#DPGross").val(numberFormat($("#DPGross").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#AdminFee").keyup(function () {
        $("#AdminFee").val(numberFormat($("#AdminFee").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#BiayaProvisi").keyup(function () {
        $("#BiayaProvisi").val(numberFormat($("#BiayaProvisi").val().replaceAll(".", "")));
        HitungEffRate(1);
    });

    $("#BiayaProvisiDealer").keyup(function () {
        $("#BiayaProvisiDealer").val(numberFormat($("#BiayaProvisiDealer").val().replaceAll(".", "")));
        HitungEffRate(1);
    });

    $("#KreditBiayaProvisiDealer").keyup(function () {
        $("#KreditBiayaProvisiDealer").val(numberFormat($("#KreditBiayaProvisiDealer").val().replaceAll(".", "")));
        HitungEffRate(1);
    });

    $("#InsuranceFee").keyup(function () {
        $("#InsuranceFee").val(numberFormat($("#InsuranceFee").val().replaceAll(".", "")));
        HitungEffRate(1);
    });

    $("#UangMukaMurni").keyup(function () {
        $("#UangMukaMurni").val(numberFormat($("#UangMukaMurni").val().replaceAll(".", "")));
    });

    $("#PembayaranKonsumen").keyup(function () {
        $("#PembayaranKonsumen").val(numberFormat($("#PembayaranKonsumen").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#JlhPembiayaan").keyup(function () {
        $("#JlhPembiayaan").val(numberFormat($("#JlhPembiayaan").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#AmtInstallment").keyup(function () {
        $("#AmtInstallment").val(numberFormat($("#AmtInstallment").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#BiayaDiscountDealer").keyup(function () {
        $("#BiayaDiscountDealer").val(numberFormat($("#BiayaDiscountDealer").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#ResidualValue").keyup(function () {
        $("#ResidualValue").val(numberFormat($("#ResidualValue").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#BiayaAdminDealer").keyup(function () {
        $("#BiayaAdminDealer").val(numberFormat($("#BiayaAdminDealer").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#KreditAdminFee").keyup(function () {
        $("#KreditAdminFee").val(numberFormat($("#KreditAdminFee").val().replaceAll(".", "")));

        HitungEffRate(1);
    });

    $("#BungaFlat").keyup(function () {
        HitungJumlahAngsuran();
        HitungEffRate(1);
    });

    $("#BiayaBBN").keyup(function () {
        $("#BiayaBBN").val(numberFormat($("#BiayaBBN").val().replaceAll(".", "")));
    });

    $("#BiayaTagih").keyup(function () {
        $("#BiayaTagih").val(numberFormat($("#BiayaTagih").val().replaceAll(".", "")));
    });

    $("#SubsidiFinance").keyup(function () {
        $("#SubsidiFinance").val(numberFormat($("#SubsidiFinance").val().replaceAll(".", "")));
    });

    $("#SubsidiDealer").keyup(function () {
        $("#SubsidiDealer").val(numberFormat($("#SubsidiDealer").val().replaceAll(".", "")));
    });

    $("#SubsidiMainDealer").keyup(function () {
        $("#SubsidiMainDealer").val(numberFormat($("#SubsidiMainDealer").val().replaceAll(".", "")));
    });

    $("#SubsidiATPM").keyup(function () {
        $("#SubsidiATPM").val(numberFormat($("#SubsidiATPM").val().replaceAll(".", "")));
    });

    $("#SubsidiPihakKetiga").keyup(function () {
        $("#SubsidiPihakKetiga").val(numberFormat($("#SubsidiPihakKetiga").val().replaceAll(".", "")));
    });

    $("#SubsidiBunga").keyup(function () {
        $("#SubsidiBunga").val(numberFormat($("#SubsidiBunga").val().replaceAll(".", "")));
    });

    $("#NominalPencairanDealer").keyup(function () {
        $("#NominalPencairanDealer").val(numberFormat($("#NominalPencairanDealer").val().replaceAll(".", "")));
    });

    $("#btnshow_Item").click(function () {
        cm_lookup.LookupType('Item', this.id);
    });

    $("#btnshow_Dealer").click(function () {
        cm_lookup.LookupType('Dealer', this.id);
    });

    $("#btnshow_Surveyor").click(function () {
        cm_lookup.LookupType('Surveyor', this.id);
    });

    $("#btnshow_MarketingHead").click(function () {
        cm_lookup.LookupType('MarketingHead', this.id);
    });

    $("#btnshow_CGSNo").click(function () {
        cm_lookup.LookupType('CGSNo', this.id);
    });

    $("#btnshow_Perantara").click(function () {
        cm_lookup.LookupType('Perantara', this.id);
    });

    $("#btnshow_BankName").click(function () {
        cm_lookup.LookupType('BankName', this.id);
    });

    $("#asset_kind_id").change(function () {
        //e.preventDefault();

        var id = $(this).val();

        GetDataDropDown(id);
        GetInsuranceFee();
    });

    $("#ms_application_type").change(function () {
        var application_type = $('#ms_application_type').val();

        document.getElementById("divTipeKerjasamaPerantara").style.display = "none";
        $('#tipekerjasama').removeAttr("required");
        $('#tipeperantara').removeAttr("required");
        $('#txPerantara').removeAttr("required");
        $('#pemilikrekening').removeAttr("required");
        $('#rekeningAtasNama').removeAttr("required");
        $('#txBankName').removeAttr("required");
        $('#NoRekening').removeAttr("required");

        $('#tipekerjasama').val("");
        $('#tipeperantara').val("");
        $('#txPerantaraId').val("");
        $('#txPerantara').val("");
        $('#pemilikrekening').val("");
        $('#rekeningAtasNama').val("");
        $('#NoRekening').val("");
        $("#hdBankAccountID").val("");
        $("#txBankNameId").val("");
        $("#txBankName").val("");

        //Standart
        if (application_type == "02") {
            $('#disc_type').removeAttr("disabled");

            $('#is_item_new').removeAttr("disabled");
            $("#is_item_new").val("");
            $("#hdis_item_newSelecytedValue").val("");

            //$('#lblNoteTipeAplikasi').text('');
        }
        //UMC - 0
        else if (application_type == "03") {
            $("#disc_type").val("0");
            $('#disc_type').attr("disabled", true);

            $("#is_item_new").val("0");
            $("#hdis_item_newSelecytedValue").val("0");
            $('#is_item_new').attr("disabled", true);

            document.getElementById("divTipeKerjasamaPerantara").style.display = "block";
            $('#tipekerjasama').attr("required", true);
            $('#tipeperantara').attr("required", true);
            $('#txPerantara').attr("required", true);
            $('#pemilikrekening').attr("required", true);
            $('#rekeningAtasNama').attr("required", true);
            $('#txBankName').attr("required", true);
            $('#NoRekening').attr("required", true);
        }
        else {
            $('#disc_type').removeAttr("disabled");
            //$("#disc_type").val("1");

            $('#is_item_new').removeAttr("disabled");
            $("#is_item_new").val("");
            $("#hdis_item_newSelecytedValue").val("");

            //$('#lblNoteTipeAplikasi').text('');
        }
    });

    $("#insurance_cover_type_id").change(function () {
        GetProcessFee($('#txtenor').val(), $('#OTR').val().replaceAll(".", ""), $('#insurance_cover_type_id').val(), "N");
    });

    $('#KodeAngsuran').change(function () {
        AngsuranChange();
    });

    $('#AngsuranTipe').change(function () {
        HitungAngsuran(1, 1); //floating
    });

    $('#nmbResidualValue').keyup(function () {
        HitungAngsuran(1, 1); //floating
    });

    $("#pemilikrekening").change(function () {
        $("#hdBankAccountID").val("");
        $("#txBankNameId").val("");
        $("#txBankName").val("");
        $("#NoRekening").val("");
        $("#rekeningAtasNama").val("");
    });

    $("#tipeperantara").change(function () {
        ("#txPerantaraId").val("");
        $("#txPerantara").val("");
    });
});

var cm_load = {
    init: function () {
        this.TogglePanel();
    },
    changeClass_Asset: function () {
        var content = document.getElementById("Asset");

        var asset = document.getElementById("Asset_more");
        content.classList.toggle('show');

        if (content.classList.contains("AssetShow")) {
            asset.innerHTML = "Data Asset";
        } else {
            asset.innerHTML = "Data Asset";
        }
    },
    changeClass_Price: function () {
        var content = document.getElementById("Price");

        var price = document.getElementById("Price_more");
        content.classList.toggle('show');

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
    }
}

var cm_lookup = {
    LookupType: function (mKey, mBtn) {
        var mUrl = "";
        switch (mKey) {
            case "Item":
                mUrl = "../CMCar/PaginationItem";
                break;
            case "Dealer":
                mUrl = "../CMCar/PaginationDealer";
                break;
            case "Surveyor":
                mUrl = "../CMCar/PaginationSurveyor";
                break;
            case "MarketingHead":
                mUrl = "../CMCar/PaginationMarketingHead";
                break;
            case "CGSNo":
                mUrl = "../CMCar/PaginationCGSNo";
                break;
            case "Perantara":
                mUrl = "../CMCar/PaginationPerantara";
                break;
            case "BankName":
                mUrl = "../CMCar/PaginationBankName";
                break;
        }

        var itemid = $("#asset_kind_id").val();
        var itembrandid = $("#ms_item_brand").val();
        var isitemnew = $("#is_item_new").val();
        var assetkindclassid = $("#ms_asset_kind_class").val();
        var BranchId = $("#txhiddenBranchId").val();
        var CompanyId = $("#txhiddenCompanyId").val();

        var TipePerantara = $("#tipeperantara").val();
        var PemilikRekening = $("#pemilikrekening").val();

        var ldBtn = Ladda.create(document.getElementById(mBtn));

        if (mKey == "Item") {
            var params = {
                pageIndex: 1
                , searchTerm: ""
                , item_id: itemid
                , item_brand_id: itembrandid
                , asset_kind_class_id: assetkindclassid
            }

            $.ajax({
                url: mUrl,
                //+ "item_id=" + itemid + "&item_brand_id=" + itembrandid + "&asset_kind_class_id=" + assetkindclassid,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdlitem").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerItem").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup Item Type. Error " + d);
                }
            });
        }

        else if (mKey == "Dealer") {
            var params = {
                pageIndex: 1,
                searchTerm: "",
                item_id: itemid,
                is_item_new: isitemnew,
                item_merk: itembrandid
            }

            $.ajax({
                url: mUrl,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdldealer").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerDealer").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup Dealer. Error " + d);
                }
            });
        }

        else if (mKey == "Surveyor") {
            var params = {
                pageIndex: 1,
                searchTerm: "",
                item_id: itemid
            }

            $.ajax({
                url: mUrl,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdlsurveyor").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerSurveyor").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup Surveyor. Error " + d);
                }
            });
        }

        else if (mKey == "MarketingHead") {
            var params = {
                pageIndex: 1,
                searchTerm: "",
                item_id: itemid
            }

            $.ajax({
                url: mUrl,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdlmarketinghead").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerMarketingHead").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup Marketing Head. Error " + d);
                }
            });
        }

        else if (mKey == "CGSNo") {
            var params = {
                pageIndex: 1,
                searchTerm: "",
                BranchId: BranchId,
                CompanyId: CompanyId
            }

            $.ajax({
                url: mUrl,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdlCGSNo").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerCGSNo").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup CGS No. Error " + d);
                }
            });
        }

        else if (mKey == "Perantara") {
            var params = {
                pageIndex: 1,
                searchTerm: "",
                BranchId: BranchId,
                CompanyId: CompanyId,
                TipePerantara: TipePerantara
            }

            $.ajax({
                url: mUrl,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdlPerantara").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerPerantara").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup Perantara. Error " + d);
                }
            });
        }

        else if (mKey == "BankName") {
            var params = {
                pageIndex: 1,
                searchTerm: "",
                BranchId: BranchId,
                CompanyId: CompanyId,
                PemilikRekening: PemilikRekening
            }

            $.ajax({
                url: mUrl,
                type: "POST",
                data: params,
                beforeSend: function () {
                    ldBtn.start();
                }
                , success: function (data) {
                    ldBtn.stop();

                    $("#mdlBankName").modal({
                        backdrop: false
                    });
                    document.getElementById(mBtn).style.display = "none";
                    $("#containerBankName").html(data);
                }
                , error: function (error) {
                    ldBtn.stop();
                    var d = error;
                    Common.Alert.Error("Failed to show Lookup BankName. Error " + d);
                }
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

        GetYear(item_id, Item_Brand_Id, asset_kind_class_id, item_brand_type_id, "N");

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
    SelectedPerantara: function (PerantaraId, PerantaraName) {
        $("#txPerantaraId").val(PerantaraId);
        $("#txPerantara").val(PerantaraName);
        $("#mdlPerantara").modal("hide");
    },
    SelectedBankName: function (BankAccountID, BankID, BankName, BankAccountNo, BankAccountName) {
        $("#hdBankAccountID").val(BankAccountID);
        $("#txBankNameId").val(BankID);
        $("#txBankName").val(BankName);
        $("#NoRekening").val(BankAccountNo);
        $("#rekeningAtasNama").val(BankAccountName);
        $("#mdlBankName").modal("hide");
    }
}

function numberFormat(value) {
    if (value != undefined) {
        let nf = new Intl.NumberFormat('id-ID');

        if (nf.format(value) == "NaN") {
            return ""
        }
        else {
            return nf.format(value);
        }
    }
}

function GetDataDropDown(id) {
    //get merk item
    GetMerkItem(id, "N");

    //get model item
    GetModelItem(id, "N");

    //get produk
    GetProduct(id, "Y");

    //get tahun kendaraan
    var item_id = $("#asset_kind_id").val();
    var Item_Brand_Id = $("#ms_item_brand").val();
    var asset_kind_class_id = $("#ms_asset_kind_class").val();
    var asset_type_id = $("#txitem_brand_type_id").val();

    GetYear(asset_kind_id, item_brand_id, asset_kind_class_id, item_brand_type_id, "N");
}

function GetYear(item_id, Item_Brand_Id, asset_kind_class_id, asset_type_id, firstload) {
    //get tahun kendaraan

    var Flag = $('#txhiddenFlag').val();
    var year = $("#txhiddenyear").val();

    var params = {
        item_id: item_id
        , Item_Brand_Id: Item_Brand_Id
        , asset_kind_class_id: asset_kind_class_id
        , asset_type_id: asset_type_id
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetYear',
        data: params
    }).then(function (data) {
        $("#year").find("option").remove();
        $("#year").append($("<option></option>").val("").html("Pilih Tahun Kendaraan"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (year == value.year && firstload == "Y") {
                    $("#year").append($("<option selected></option>").val(value.year).html(value.year));
                }
                else {
                    $("#year").append($("<option></option>").val(value.year).html(value.year));
                }
            }
            else {
                $("#year").append($("<option></option>").val(value.year).html(value.year));
            }
        });
    });
}

function GetModelItem(id, firstload) {
    //get model item

    var Flag = $('#txhiddenFlag').val();
    var asset_kind_class_id = $('#txhiddenasset_kind_class_id').val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetAssetKindClass?item_id=' + id,
        data: 'item_id=' + id
    }).then(function (data) {
        $("#ms_asset_kind_class").find("option").remove();
        //$("#ms_asset_kind_class").append($("<option></option>").val("").html("Pilih Model Item"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (asset_kind_class_id == value.asset_kind_class_id && firstload == "Y") {
                    $("#ms_asset_kind_class").append($("<option selected></option>").val(value.asset_kind_class_id).html(value.asset_kind_class_name));
                }
                else {
                    $("#ms_asset_kind_class").append($("<option></option>").val(value.asset_kind_class_id).html(value.asset_kind_class_name));
                }
            }
            else {
                $("#ms_asset_kind_class").append($("<option></option>").val(value.asset_kind_class_id).html(value.asset_kind_class_name));
            }
        });
    });
}

function GetMerkItem(id, firstload) {
    //get merk item

    var Flag = $('#txhiddenFlag').val();
    var item_brand_id = $('#txhiddenitem_brand_id').val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetItemBrand?item_id=' + id,
        data: 'item_id=' + id
    }).then(function (data) {
        $("#ms_item_brand").find("option").remove();
        $("#ms_item_brand").append($("<option></option>").val("").html("Pilih Merk Item"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (item_brand_id == value.item_brand_id && firstload == "Y") {
                    $("#ms_item_brand").append($("<option selected></option>").val(value.item_brand_id).html(value.item_brand));
                }
                else {
                    $("#ms_item_brand").append($("<option></option>").val(value.item_brand_id).html(value.item_brand));
                }
            }
            else {
                $("#ms_item_brand").append($("<option></option>").val(value.item_brand_id).html(value.item_brand));
            }
        });
    });
}

function GetProduct(id, firstload) {
    //get produk

    var Flag = $('#txhiddenFlag').val();
    var product_id = $('#txhiddenproduct_id').val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsProduct?item_id=' + id,
        data: 'item_id=' + id
    }).then(function (data) {
        $("#product_id").find("option").remove();
        $("#product_id").append($("<option></option>").val("").html("Pilih Produk"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (product_id == value.product_id && firstload == "Y") {
                    $("#product_id").append($("<option selected></option>").val(value.product_id).html(value.product_name));
                }
                else {
                    $("#product_id").append($("<option></option>").val(value.product_id).html(value.product_name));
                }
            }
            else {
                $("#product_id").append($("<option></option>").val(value.product_id).html(value.product_name));
            }
        });
    });
}

function GetMarketingProduct(firstload) {
    //get produk marketing
    var Flag = $('#txhiddenFlag').val();
    var product_marketing_id = $("#txhiddenproduct_marketing_id").val();
    var CompanyId = $("#txhiddenCompanyId").val();

    var params = {
        company_id: CompanyId
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsProductMarketing',
        data: params
    }).then(function (data) {
        $("#product_marketing_id").find("option").remove();
        $("#product_marketing_id").append($("<option></option>").val("").html("Pilih Produk Marketing"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (product_marketing_id == value.product_marketing_id && firstload == "Y") {
                    $("#product_marketing_id").append($("<option selected></option>").val(value.product_marketing_id).html(value.product_marketing_name));
                }
                else {
                    $("#product_marketing_id").append($("<option></option>").val(value.product_marketing_id).html(value.product_marketing_name));
                }
            }
            else {
                $("#product_marketing_id").append($("<option></option>").val(value.product_marketing_id).html(value.product_marketing_name));
            }
        });
    });
}

function GetSTNKName(firstload) {
    //get nama stnk
    var Flag = $('#txhiddenFlag').val();
    var STNK_name_id = $("#txhiddenSTNK_name_id").val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsSTNKName?where=-',
        data: 'where=-'
    }).then(function (data) {
        $("#STNKName").find("option").remove();
        $("#STNKName").append($("<option></option>").val("").html("Pilih Nama STNK"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (STNK_name_id == value.stnk_name_id && firstload == "Y") {
                    $("#STNKName").append($("<option selected></option>").val(value.stnk_name_id).html(value.stnk_name_description));

                    $("#lblNamaSTNK2").val("Nama STNK " + value.stnk_name_description);
                    selectedTextarea = $('#PartnerSTNKName')[0];
                    selectedTextarea.placeholder = "Nama STNK " + value.stnk_name_description;
                }
                else {
                    $("#STNKName").append($("<option></option>").val(value.stnk_name_id).html(value.stnk_name_description));
                }
            }
            else {
                $("#STNKName").append($("<option></option>").val(value.stnk_name_id).html(value.stnk_name_description));
            }
        });
    });
}

function GetAsalPO(firstload) {
    //get Asal PO

    var Flag = $('#txhiddenFlag').val();
    var provenance_PO_id = $("#txhiddenprovenance_PO_id").val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsProvenancePurchaseOrder?where=-',
        data: 'where=-'
    }).then(function (data) {
        $("#provenance_PO_id").find("option").remove();
        $("#provenance_PO_id").append($("<option></option>").val("").html("Pilih Asal PO"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (provenance_PO_id == value.provenance_PO_id && firstload == "Y") {
                    $("#provenance_PO_id").append($("<option selected></option>").val(value.provenance_PO_id).html(value.provenance_PO_description));
                }
                else {
                    $("#provenance_PO_id").append($("<option></option>").val(value.provenance_PO_id).html(value.provenance_PO_description));
                }
            }
            else {
                $("#provenance_PO_id").append($("<option></option>").val(value.provenance_PO_id).html(value.provenance_PO_description));
            }
        });
    });
}

function GetTipePenggunaan(firstload) {
    var Flag = $('#txhiddenFlag').val();
    var usage_type_id = $("#txhiddenusage_type_id").val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsUsageType?where=-',
        data: 'where=-'
    }).then(function (data) {
        $("#usage_type_id").find("option").remove();
        $("#usage_type_id").append($("<option></option>").val("").html("Pilih Tipe Penggunaan"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (usage_type_id == value.usage_type_id && firstload == "Y") {
                    $("#usage_type_id").append($("<option selected></option>").val(value.usage_type_id).html(value.usage_type_name));
                }
                else {
                    $("#usage_type_id").append($("<option></option>").val(value.usage_type_id).html(value.usage_type_name));
                }
            }
            else {
                $("#usage_type_id").append($("<option></option>").val(value.usage_type_id).html(value.usage_type_name));
            }
        });
    });
}

function GetAR(firstload) {
    //get AR

    var Flag = $('#txhiddenFlag').val();
    var AR = $("#txhiddenar").val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsAR?where=-',
        data: 'where=-'
    }).then(function (data) {
        $("#AR_id").find("option").remove();
        $("#AR_id").append($("<option></option>").val("").html("Pilih AR"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (AR == value.ar_id && firstload == "Y") {
                    $("#AR_id").append($("<option selected></option>").val(value.ar_id).html(value.ar_name_description));
                }
                else {
                    $("#AR_id").append($("<option></option>").val(value.ar_id).html(value.ar_name_description));
                }
            }
            else {
                $("#AR_id").append($("<option></option>").val(value.ar_id).html(value.ar_name_description));
            }
        });
    });
}

function GetCoverInsuranceType(firstload) {
    //get Cover Insurance Type
    var Flag = $('#txhiddenFlag').val();
    var tipe_cover = $("#txhiddentipe_cover").val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsInsuranceCoverType?where=-',
        data: 'where=-'
    }).then(function (data) {
        $("#insurance_cover_type_id").find("option").remove();
        $("#insurance_cover_type_id").append($("<option></option>").val("").html("Pilih Tipe Cover Asuransi"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (tipe_cover == value.insurance_cover_type_id && firstload == "Y") {
                    $("#insurance_cover_type_id").append($("<option selected></option>").val(value.insurance_cover_type_id).html(value.insurance_cover_type_name));
                }
                else {
                    $("#insurance_cover_type_id").append($("<option></option>").val(value.insurance_cover_type_id).html(value.insurance_cover_type_name));
                }
            }
            else {
                $("#insurance_cover_type_id").append($("<option></option>").val(value.insurance_cover_type_id).html(value.insurance_cover_type_name));
            }
        });
    });
}

function GetInterestRateType(firstload) {
    //get Interest Rate Type

    var Flag = $('#txhiddenFlag').val();
    var interest_rate_type_id = $("#txhiddeninterest_rate_type_id").val();

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetMsInterestRateType?where=-',
        data: 'where=-'
    }).then(function (data) {
        $("#interest_rate_type_id").find("option").remove();
        //$("#interest_rate_type_id").append($("<option></option>").val("").html("Pilih Interest Rate Type"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (interest_rate_type_id == value.interest_rate_type_id && firstload == "Y") {
                    $("#interest_rate_type_id").append($("<option></option>").val(value.interest_rate_type_id).html(value.interest_rate_type_name));
                }
                else {
                    $("#interest_rate_type_id").append($("<option></option>").val(value.interest_rate_type_id).html(value.interest_rate_type_name));
                }
            }
            else {
                $("#interest_rate_type_id").append($("<option></option>").val(value.interest_rate_type_id).html(value.interest_rate_type_name));
            }
        });
    });
}

function GetInsuranceFee() {
    var OTR = $("#OTR").val().replaceAll(".", "");
    var CompanyId = $("#txhiddenCompanyId").val();
    var BranchId = $("#txhiddenBranchId").val();
    var asset_kind_id = $("#asset_kind_id").val();
    var tenor = $("#txtenor").val();

    var params = {
        asset_kind_id: asset_kind_id
        , OTR: OTR
        , CompanyId: CompanyId
        , BranchId: BranchId
        , Tenor: tenor
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetInsuranceFee',
        data: params
    }).then(function (data) {
        $.each(data, function (key, value) {
            $("#InsuranceFee").val(numberFormat(value.insurance_fee));
        });
    });
}

function GetFinancingPackage(tenor, otr, arid, firstload) {
    //get Paket Pembiayaan
    var Flag = $('#txhiddenFlag').val();

    var param = {
        Tenor: tenor
        , OTR: otr
        , ARType: arid
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetFinancingPackage',
        data: param
    }).then(function (data) {
        $("#paket_pembiayaan").find("option").remove();
        $("#paket_pembiayaan").append($("<option></option>").val("").html("Pilih Paket Pembiayaan"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                var PackageID = $("#txhiddenpaket_pembiayaan").val();

                if (PackageID == value.packageID && firstload == "Y") {
                    $("#paket_pembiayaan").append($("<option selected></option>").val(value.packageID).html(value.packageName));
                }
                else {
                    $("#paket_pembiayaan").append($("<option></option>").val(value.packageID).html(value.packageName));
                }
            }
            else {
                $("#paket_pembiayaan").append($("<option></option>").val(value.packageID).html(value.packageName));
            }
        });
    });
}

function GetProcessFee(tenor, otr, inscovertype, firstload) {
    //get Biaya Proses
    var Flag = $('#txhiddenFlag').val();
    var biaya_proses = $("#txhiddenbiaya_proses").val();

    var param = {
        Tenor: tenor
        , OTR: otr
        , InsCoverType: inscovertype
        , BranchId: $('#txhiddenBranchId').val()
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetProcessFee',
        data: param
    }).then(function (data) {
        $("#biaya_proses").find("option").remove();
        $("#biaya_proses").append($("<option></option>").val("").html("Pilih Biaya Proses"));

        $.each(data, function (key, value) {
            if (Flag != "Add") {
                if (biaya_proses == value.biayaProsesID && firstload == "Y") {
                    $("#biaya_proses").append($("<option selected></option>").val(value.biayaProsesID).html(value.biayaProsesDesc));
                }
                else {
                    $("#biaya_proses").append($("<option></option>").val(value.biayaProsesID).html(value.biayaProsesDesc));
                }
            }
            else {
                $("#biaya_proses").append($("<option></option>").val(value.biayaProsesID).html(value.biayaProsesDesc));
            }

            //$("#biaya_proses").append($("<option></option>").val(value.biayaProsesID).html(value.biayaProsesDesc));
        });
    });
}

function GetLifeInsuranceCredit(OTR, DP, AdminFeeKredit, ProvisiFeeKredit, BiayaProsesKredit, BranchIdAsuransi, Tenor) {
    var params = {
        OTR: OTR
        , DP: DP
        , AdminFeeKredit: AdminFeeKredit
        , ProvisiFeeKredit: ProvisiFeeKredit
        , BiayaProsesKredit: BiayaProsesKredit
        , BranchIdAsuransi: BranchIdAsuransi
        , Tenor: Tenor
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetLifeInsuranceCredit',
        data: params
    }).then(function (data) {
        $.each(data, function (key, value) {
            $("#KreditBiayaAsuransiMega").val(numberFormat(value.mega_insurance_fee_kredit));
            $("#KreditBiayaHandphone").val(numberFormat(value.handphone_fee_kredit));
        });
    });
}

function GetAdminProvisiInterestFee(packageid, tenor, otr, arid) {
    $("#hdnInterestMin").val("0");
    $("#hdnInterestMax").val("0");
    $("#hdnRefundBunga").val("0");
    $("#hdnInsRefundMax").val("0");
    $("#hdnAdminFeeMin").val("0");
    $("#hdnAdminFeeMax").val("0");

    var param = {
        PackageID: packageid
        , Tenor: tenor
        , OTR: otr
        , ARType: arid
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetAdminProvisiInterestFee',
        data: param
    }).then(function (data) {
        $.each(data, function (key, value) {
            $("#hdAdminFeeMin").val(value.admin_fee_min);
            $("#hdAdminFeeMax").val(value.admin_fee_max);

            if ($("#cbxKreditAdminFee").is(":checked")) {
                $("#AdminFee").val("0");
                $("#KreditAdminFee").val(numberFormat(value.admin_fee_min));
            }
            else {
                $("#KreditAdminFee").val("0");
                $("#AdminFee").val(numberFormat(value.admin_fee_min));
            }

            $("#hdnBiayaProvisiDealerMin").val(value.provisi_dealer_min);
            $("#hdnBiayaProvisiDealerMax").val(value.provisi_dealer_max);

            //if ($("#cbxKreditBiayaProvisiDealer").is(":checked")) {
            //    $("#BiayaProvisiDealer").val("0");
            //    $("#KreditBiayaProvisiDealer").val(value.ProvisiDealerMin);
            //}
            //else {
            //    $("#KreditBiayaProvisiDealer").val("0");
            //    $("#BiayaProvisiDealer").val(value.ProvisiDealerMin);
            //}

            $("#BungaFlat").val(value.interest_min);
            $("#hdnInterestMin").val(value.interest_min);
            $("#hdnInterestMax").val(value.interest_max);
            $("#hdnRefundBunga").val(value.refund_bunga);
            $("#hdnInsRefundMax").val(value.ins_refund_max);
            $("#hdnAdminFeeMin").val(value.admin_fee_min);
            $("#hdnAdminFeeMax").val(value.admin_fee_max);
        });
    });
}

function GetProcessProvisiIns(BiayaProsesID, Tenor, OTR, InsCoverType, BranchId, ItemYear, credit_id, modelid, loss_fee, loss_fee_kredit, usage_type_id) {
    var param = {
        BiayaProsesID: BiayaProsesID
        , Tenor: Tenor
        , OTR: OTR
        , InsCoverType: InsCoverType
        , BranchId: BranchId
        , ItemYear: ItemYear
        , credit_id: credit_id
        , modelid: modelid
        , loss_fee: loss_fee
        , loss_fee_kredit: loss_fee_kredit
        , usage_type_id: usage_type_id
    }

    $.ajax({
        type: 'GET',
        url: '../CMCar/GetProcessProvisiIns',
        data: param
    }).then(function (data) {
        $.each(data, function (key, value) {
            $('#BiayaAsuransiKerugian').val("0");
            $('#KreditBiayaAsuransiKerugian').val("0");
            $('#hdnBiayaProses').val(value.biaya_proses);

            if ($("#cbxKreditBiayaProses").is(":checked")) {
                $("#BiayaProses").val("0");
                $("#KreditBiayaProses").val(numberFormat(value.nominal_biaya_proses));

                $("#BiayaProvisi").val("0");
                $("#KreditBiayaProvisiAsuransi").val(numberFormat(value.biaya_provisi_ins));

                $("#BiayaAsuransiKerugian").val("0");
                $("#KreditBiayaAsuransiKerugian").val(numberFormat(value.biaya_asuransi_kerugian));
            }
            else {
                $("#KreditBiayaProses").val("0");
                $("#BiayaProses").val(numberFormat(value.nominal_biaya_proses));

                $("#KreditBiayaProvisiAsuransi").val("0");
                $("#BiayaProvisi").val(numberFormat(value.biaya_provisi_ins));

                $("#KreditBiayaAsuransiKerugian").val("0");
                $("#BiayaAsuransiKerugian").val(numberFormat(value.biaya_asuransi_kerugian));
            }
        });
    });
}

var HitungEffRate = function (val) {
    var Tenor = parseInt(document.getElementById("txtenor").value.replaceAll(".", ""));
    var AssetCost = parseInt(document.getElementById("OTR").value.replaceAll(".", ""));
    var AdminFee = parseInt(document.getElementById("AdminFee").value.replaceAll(".", ""));
    var AdminFee2 = parseInt(document.getElementById("KreditAdminFee").value.replaceAll(".", ""));
    var Angsuran = parseInt(document.getElementById("AmtInstallment").value.replaceAll(".", ""));
    var UMCombo;

    var Deposit = parseInt(document.getElementById("DPGross").value.replaceAll(".", ""));
    var AR = parseInt(document.getElementById("AR_id").value);
    var TipeKendaraan = document.getElementById("asset_kind_id").value;
    var ProdukMarketing = document.getElementById("product_marketing_id").value;

    var BiayaProvisi = parseInt(document.getElementById("BiayaProvisi").value.replaceAll(".", ""));;  //provisi ins tunai
    var BiayaProvisi2 = parseInt(document.getElementById("KreditBiayaProvisiAsuransi").value.replaceAll(".", "")); //provisi ins kredit

    var BiayaPolis = 0;
    var Insurance = 0;
    var TipeIns = 0;
    TipeIns = $('#cbxKreditBiayaProses').is(":checked") ? "0" : "1";
    var Insurance2 = 0;
    var AsuransiKredit = 0;
    var AsuransiTunai = 0;

    Insurance = parseInt(document.getElementById("BiayaAsuransiKerugian").value.replaceAll(".", ""));
    Insurance2 = parseInt(document.getElementById("KreditBiayaAsuransiKerugian").value.replaceAll(".", ""));
    BiayaPolis = formatNaN(parseInt(document.getElementById("InsuranceFee").value.replaceAll(".", "")));

    var BiayaAsuransiJiwaMega;
    var BiayaHP;

    BiayaAsuransiJiwaMega = 0;
    BiayaHP = 0;

    var BiayaFidusia = parseInt(document.getElementById("BiayaFidusia").value.replaceAll(".", ""));

    var KaliDimuka;
    var DPMurni;
    var Pembiayaan;
    var Bunga;

    DPMurni = document.getElementById("UangMukaMurni").value;

    var EffRate;
    var FlatRate;

    if (AR == 0)
        KaliDimuka = 1;
    else
        KaliDimuka = 0;

    var InAdvanceOrArrear;
    if (AR == 1)
        InAdvanceOrArrear = 'A';
    else
        InAdvanceOrArrear = 'D';

    var BiayaProvisiDealerKredit = parseFloat(document.getElementById("KreditBiayaProvisiDealer").value.replaceAll(".", ""));
    var BiayaProvisiDealerTunai = parseFloat(document.getElementById("BiayaProvisiDealer").value.replaceAll(".", ""));

    if ((Deposit.toString() != '')) {
        if (AR == 1) {
            DPMurni = getNum(Deposit) - getNum(Insurance) - getNum(AdminFee) - getNum(BiayaProvisi) - getNum(BiayaPolis) - getNum(BiayaProvisiDealerTunai) - getNum(BiayaFidusia);
        } else {
            DPMurni = getNum(Deposit) - getNum(Insurance) - getNum(AdminFee) - getNum(BiayaProvisi) - getNum(Angsuran) - getNum(BiayaPolis) - getNum(BiayaProvisiDealerTunai) - getNum(BiayaFidusia);
        }
    }

    var AsuransiJiwa = document.getElementById("asuransi_jiwa_mega").value;
    if ((AsuransiJiwa == '1')) {
        BiayaAsuransiJiwaMega = parseInt(document.getElementById("KreditBiayaAsuransiMega").value.replaceAll(".", ""));
        BiayaHP = parseInt(document.getElementById("KreditBiayaHandphone").value.replaceAll(".", ""));

        Pembiayaan = getNum(AssetCost) - getNum(DPMurni) + (getNum(BiayaProvisiDealerKredit) + getNum(AdminFee2) + getNum(BiayaProvisi2) + getNum(Insurance2) + getNum(BiayaAsuransiJiwaMega) + getNum(BiayaHP));
    }
    else {
        BiayaAsuransiJiwaMega = 0;
        BiayaHP = 0;

        Pembiayaan = getNum(AssetCost) - getNum(DPMurni) + (getNum(BiayaProvisiDealerKredit) + getNum(AdminFee2) + getNum(BiayaProvisi2) + getNum(Insurance2));
    }

    if ((Angsuran.toString() != '') && (Tenor.toString() != '') && (Pembiayaan.toString() != ''))
        Bunga = (Angsuran * Tenor) - Pembiayaan;
    else
        Bunga = '';

    document.getElementById("hdnNilaiBunga").value = Bunga;

    if ((Pembiayaan.toString() != '') && (AR.toString() != '') && (Tenor.toString() != '') && (Angsuran.toString() != ''))
        EffRate = GetEffectiveRate(Angsuran, Tenor, Pembiayaan, 0, InAdvanceOrArrear);
    else
        EffRate = '';

    var TipeAplikasi = document.getElementById("ms_application_type").value;

    document.getElementById("UangMukaMurni").value = numberFormat(DPMurni);
    document.getElementById("JlhPembiayaan").value = numberFormat(Pembiayaan);
    document.getElementById("BungaEfektif").value = EffRate;

    if ($("#KreditBiayaHandphone").val() == "") {
        $("#KreditBiayaHandphone").val("0");
    }
    if ($("#KreditBiayaAsuransiMega").val() == "") {
        $("#KreditBiayaAsuransiMega").val("0");
    }

    if (TipeKendaraan == "002") {
        if (AR == 2) {
            if (TipeIns == '0') {
                FirstPayment = DPMurni + AdminFee + Angsuran;
            }
            else {
                FirstPayment = getNum(DPMurni) + getNum(AdminFee) + getNum(Angsuran) + getNum(Insurance);
            }
        }
        else {
            if (TipeIns == '0') {
                FirstPayment = DPMurni + AdminFee;
            }
            else {
                FirstPayment = getNum(DPMurni) + getNum(AdminFee) + getNum(Insurance);
            }
        }
    }

    var arTAC = Tenor * Angsuran;
    var opTAC = Pembiayaan;
    var uliTAC = arTAC - opTAC;
    var asuransi = 0;
    var biayaadmin = 0;
    var bprovisi = 0;
    var bprovisidealer = 0;

    if (TipeKendaraan == "002") {
        if (TipeIns == 0) {
            asuransi = $('#KreditBiayaAsuransiKerugian').val().replaceAll(".", "");
            bprovisi = $('#KreditBiayaProvisiAsuransi').val().replaceAll(".", "");
        } else {
            asuransi = $('#BiayaAsuransiKerugian').val().replaceAll(".", "");
            bprovisi = $('#BiayaProvisi').val().replaceAll(".", "");
        }
    }

    if ($('#cbxKreditBiayaProvisiDealer').is(':checked')) {
        bprovisidealer = BiayaProvisiDealerKredit;
    } else {
        bprovisidealer = BiayaProvisiDealerTunai;
    }

    if ($('#cbxKreditAdminFee').is(':checked')) {
        biayaadmin = $('#KreditAdminFee').val().replaceAll(".", "");
    } else {
        biayaadmin = $('#AdminFee').val().replaceAll(".", "");
    }

    var PersenBatasTACmax = parseFloat(document.getElementById("hdnBatasTACMax").value.replace(/,/g, '.')) / 100;
    var PersenAsuransiTACmax = parseFloat(document.getElementById("hdnParamAsuransiTACMax").value.replace(/,/g, '.')) / 100;
    var tac = Math.round((PersenBatasTACmax) * (uliTAC + parseFloat(biayaadmin) + (PersenAsuransiTACmax * parseFloat(asuransi)) + parseFloat(bprovisi) + parseFloat(bprovisidealer)));

    $('#hdnTACMax').val(tac);
    hitungPersentaseTACMax();

    if (TipeKendaraan == "002") {
        hitungSubsidiPihakKetiga();
    }
}

var GetEffectiveRate = function (Installment, Tenor, JmlPembiayaan, OSPembiayaan, AdvanceOrArrear) {
    if (AdvanceOrArrear == 'D') {
        Tenor = parseFloat(Tenor) - 1;
        JmlPembiayaan = parseFloat(JmlPembiayaan) - parseFloat(Installment);
    }
    var f_IRR_rate = 0.005;
    var f_addition = 0.0500000000;
    var f_temp_irr = parseFloat(f_IRR_rate) + parseFloat(f_addition);
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
                f_npv_update = (f_npv_update + ((parseFloat(Installment) + parseFloat(OSPembiayaan)) / Math.pow(1 + parseFloat(f_temp_irr), f_loop_period)));
            }
            else {
                f_npv_update = f_npv_update + ((parseFloat(Installment)) / Math.pow(1 + f_temp_irr, f_loop_period));
            }
            f_loop_period = f_loop_period + 1;
        }
        if (f_npv_update == JmlPembiayaan) {
            break;
        }
        if (f_npv_update > JmlPembiayaan) {
            f_addition = f_addition / 2;
            f_temp_irr = parseFloat(f_temp_irr) + parseFloat(f_addition);
        }
        else {
            f_addition = f_addition * 2;
            f_temp_irr = parseFloat(f_temp_irr) - parseFloat(f_addition);
        }
        if (f_npv_update == f_npv_temp) {
            if (f_comparison_check == 0) {
                break;
            }
            f_comparison_check = f_comparison_check - 1;
        }
        else {
            f_comparison_check = 3;
            f_npv_temp = f_npv_update;
        }
        f_trial_counter = f_trial_counter + 1;
    }
    f_IRR_rate = Math.round((parseFloat(f_temp_irr) * 1200) * 10000) / 10000;
    return f_IRR_rate;
}

function hitungPersentaseTACMax() {
    var discountUM = document.getElementById('BiayaAdminDealer').value.replaceAll(".", "");
    var tac = document.getElementById('hdnTACMax').value;
    var persenBatasTACmax = parseFloat(document.getElementById("hdnBatasTACMax").value.replace(/,/g, '.')) / 100;
    var tacpersen = (discountUM / (tac / persenBatasTACmax)) * 100;
    $('#TAC_maxPersentase').val(tacpersen.toFixed(4) + " % ");
}

function hitungSubsidiPihakKetiga() {
    var ParamRefundAsuransi = parseFloat(document.getElementById("hdnInsRefundMax").value.replace(/,/g, '.')) / 100;
    var tac = parseFloat(document.getElementById('hdnTACMax').value.replace(/,/g, '.'));
    var TipeKendaraan = document.getElementById("asset_kind_id").value;
    var OTR = parseFloat(document.getElementById("OTR").value.replaceAll(".", ""));
    var ProdukMarketing = document.getElementById("product_marketing_id").value;
    var IsBranchException = document.getElementById("hdnIsBranchException").value;

    /*REFUND A*/
    var persenMsBiayaProsesHdr = parseFloat(document.getElementById("hdnBiayaProses").value) / 100;
    var RefundA = Math.round(ParamRefundAsuransi * (persenMsBiayaProsesHdr * OTR));

    /*REFUND B*/
    var RefundB = 0;
    var UangMukaMurni = parseFloat(document.getElementById("UangMukaMurni").value.replaceAll(".", ""));

    var hdnProvisiMin = $("#hdnBiayaProvisiDealerMin").val() == "" ? 0 :
        parseFloat(document.getElementById('hdnBiayaProvisiDealerMin').value.replaceAll(".", ""));
    var hdnProvisiMax = $("#hdnBiayaProvisiDealerMax").val() == "" ? 0 :
        parseFloat(document.getElementById('hdnBiayaProvisiDealerMax').value.replaceAll(".", ""));

    var provisi = ($('#cbxKreditBiayaProvisiDealer').is(':checked')) ?
        parseFloat(document.getElementById('KreditBiayaProvisiDealer').value.replaceAll(".", "")) :
        parseFloat(document.getElementById('BiayaProvisiDealer').value.replaceAll(".", ""));

    var ProvisiMin = Math.round(hdnProvisiMin * (OTR - UangMukaMurni) / 100);
    var ProvisiMax = Math.round(hdnProvisiMax * (OTR - UangMukaMurni) / 100);

    $('#lblhdnNominalProvisiDealerMin').val(numberFormat(ProvisiMin));
    $('#lblhdnNominalProvisiDealerMax').val(numberFormat(ProvisiMax));

    RefundB = Math.round(provisi - ProvisiMin);

    /*REFUND C*/
    var RefundC = 0;

    var AdminFeeMin = $("#hdnAdminFeeMin").val() == "" ? 0 : parseFloat(document.getElementById('hdnAdminFeeMin').value.replaceAll(".", ""));
    var AdminFeeMax = $("#hdnAdminFeeMax").val() == "" ? 0 : parseFloat(document.getElementById('hdnAdminFeeMax').value.replaceAll(".", ""));

    var AdminFee = 0;
    if ($("#cbxKreditAdminFee").is(":checked")) {
        AdminFee = parseFloat(document.getElementById('KreditAdminFee').value.replaceAll(".", ""));
    }
    else {
        AdminFee = parseFloat(document.getElementById('AdminFee').value.replaceAll(".", ""));
    }

    RefundC = Math.round(AdminFee - AdminFeeMin);

    /*REFUND D*/
    var RefundD = 0;
    var JmlPembiayaan = parseFloat(document.getElementById('JlhPembiayaan').value.replaceAll(".", ""));
    var tenor = parseFloat(document.getElementById("txtenor").value.replaceAll(".", ""))
    var tenorTahun = parseFloat(Math.ceil(tenor / 12));

    var InterestMin = $("#hdnInterestMin").val() == "" ? 0 : parseFloat(document.getElementById("hdnInterestMin").value.replace(/,/g, '.'));
    var InterestMax = $("#hdnInterestMax").val() == "" ? 0 : parseFloat(document.getElementById("hdnInterestMax").value.replace(/,/g, '.'));

    var BungaFlat = $("#BungaFlat").val() == "" ? 0 : parseFloat(document.getElementById("BungaFlat").value.replace(/,/g, '.')).toFixed(2);

    var SelisihBunga = BungaFlat - InterestMin;
    if (SelisihBunga < 0) {
        SelisihBunga = 0;
    }

    RefundD = Math.round((JmlPembiayaan * (SelisihBunga / 100) * tenorTahun) / (1 + ((BungaFlat / 100) * tenorTahun)));

    $('#hdnNominalRefundBunga').val(RefundD);

    var TotalRefund = Math.round(RefundA + RefundB + RefundC + RefundD);

    $('#TACMaxX1').val("Total Refund : " + numberFormat(TotalRefund));
    $('#TACMaxX2').val("Refund Rate (TAC " + $("#hdnBatasTACMax").val() + " %) : " + numberFormat(tac));

    if (IsBranchException == "0") {//bukan cabang exception
        if (TotalRefund > tac) {
            var SubsidiPihakKetiga = Math.round(TotalRefund - tac);
            document.getElementById("SubsidiPihakKetiga").value = numberFormat(SubsidiPihakKetiga);
            $('#hdnSubsidiPihakKetigaMax').val(SubsidiPihakKetiga);
        }
        else {
            document.getElementById("SubsidiPihakKetiga").value = 0;
            $('#hdnSubsidiPihakKetigaMax').val(0);
        }
    }
}

function HitungJumlahAngsuran() {
    var a = parseFloat(document.getElementById("BungaFlat").value.replace(/,/g, '.')).toFixed(2);
    var b = parseFloat(document.getElementById("txtenor").value.replaceAll(".", ""));
    var c = parseFloat(document.getElementById("JlhPembiayaan").value.replaceAll(".", ""));
    var Installment = 0;
    var NominalBunga = 0;
    NominalBunga = a * ((b / 12) * c) / 100;
    Installment = (c + NominalBunga) / b;
    Installment = +(Math.round(Installment + "e-3") + "e+3"); //Math.round(Installment

    $('#AmtInstallment').val(numberFormat(Installment));
}

var AngsuranChange = function () {
    var itrtypeSt = $('#interest_rate_type_id').val();
    var itrtypeTr = $.trim(itrtypeSt);
    if (itrtypeTr == "2") { //Floating
        HitungAngsuran(1, 1);
    }
    else {
        HitungAngsuran(0, 0);   //fixed
    }
}

var HitungAngsuran = function (val, val2) {
    var TenorGet = $('#txtenor').val().replaceAll(".", "");  //remove comma ; effect numeric box
    var AssetCostGet = $('#OTR').val().replaceAll(".", "");
    var AdminFeeGet = $('#AdminFee').val().replaceAll(".", "");
    //var InsuranceGet = $('#txbInsuranceFee').val().replaceAll(".",""); //$('#nmbInsuranceFee').val().replaceAll(".","");
    var InsuranceGet = "0";
    var AngsuranGet = $('#AmtInstallment').val().replaceAll(".", "");

    var DepositGet = $('#DPGross').val().replaceAll(".", "");

    var Tenor = parseInt(TenorGet);
    var AssetCost = parseFloat(AssetCostGet);
    var AdminFee = parseFloat(AdminFeeGet);
    var Insurance = parseFloat(InsuranceGet);
    var Angsuran = parseFloat(AngsuranGet);

    var Deposit = parseFloat(DepositGet);

    var AR = parseInt($('#AR_id').val());
    var TipeKendaraan = $('#asset_kind_id').val();
    var KaliDimuka;
    var Pembiayaan;
    var Bunga;

    var GetAngs;
    var Efektif;

    var FlatRate = parseFloat($('#BungaFlat').val());

    //var BenchmarkRate = parseFloat($('#txbFloatingBenchmarkRatePercentage').val());
    //var MarginRate = parseFloat($('#txbFloatingMarginRate').val());
    //var FloatingRate = BenchmarkRate + MarginRate;

    var InsTipeBayar;

    var ProvisiFee = 0;
    var ProvisiFeeINS = 0;

    if (AR == 0)
        KaliDimuka = 1;
    else
        KaliDimuka = 0;

    var InAdvanceOrArrear;
    if (AR == 1)
        InAdvanceOrArrear = 'A'; //'R'; //in arrear
    else
        InAdvanceOrArrear = 'D';  //'A'; //in advanced

    //$('#lblFIDUangMukaMurni').val('');
    //$('#lblFPDUangMukaMurni').val('');
    //$('#lblFIDUangMukaMurni').remove();
    //$('#lblFPDUangMukaMurni').remove();

    if (TipeKendaraan == "002") { // Mobil
        //InsTipeBayar = parseInt($('#ddlInsTipeBayar').val());
        InsTipeBayar = 0;

        var BiayaPolis = $('#InsuranceFee').val().replaceAll(".", "");
        var BiayaPolisGet = parseFloat(BiayaPolis);

        var InsuranceLoss = $('#BiayaAsuransiKerugian').val().replaceAll(".", "");
        var InsuranceLossGet = parseFloat(InsuranceLoss);

        var biayaProvisi = $('#BiayaProvisiDealer').val().replaceAll(".", "");
        ProvisiFee = biayaProvisi;

        var biayaProvisiINS = $('#BiayaProvisi').val().replaceAll(".", "");
        ProvisiFeeINS = biayaProvisiINS;

        Insurance = InsuranceLossGet + BiayaPolisGet;
    }

    var DPMurni = 0;

    var BiayaFidusia = parseInt(document.getElementById("BiayaFidusia").value.replaceAll(".", ""));

    if (TipeKendaraan == "002") { // Mobil
        if (InAdvanceOrArrear == 'D')  ////in advanced
        {
            DPMurni = Deposit - AdminFee - Insurance - Angsuran - ProvisiFee - ProvisiFeeINS - BiayaFidusia;
        }
        else { //In ARREAR
            DPMurni = Deposit - Insurance - AdminFee - ProvisiFee - ProvisiFeeINS - BiayaFidusia;
        }
    }

    if (isNaN(DPMurni)) {
        $('#UangMukaMurni').val('');
    }
    else {
        $('#UangMukaMurni').val(DPMurni);
    }

    Pembiayaan = AssetCost - DPMurni;

    //flat rate
    if (val2 == 0) {
        if ($('#KodeAngsuran').val() == '0001')
            GetAngs = GetAngsuranMenurun(Pembiayaan, Tenor, FlatRate); //Menurun
        else if ($('#KodeAngsuran').val() == '0002')
            GetAngs = GetAngsuranTetap(Pembiayaan, Tenor, FlatRate); //Tetap
        else
            GetAngs = '';
    }

    ////floating rate
    //else if (val2 == 1) {
    //    var stepupdownSt = $('#AngsuranTipe').val();
    //    var stepupdownTr = $.trim(stepupdownSt);

    //    var PaymentInterval = $('#nmbPaymentInterval').val().replaceAll(".","");
    //    var pifloat = parseFloat(PaymentInterval);
    //    var IrMonth = FloatingRate / 100 / 12 * pifloat; //interest rate of month
    //    var irin = parseFloat(IrMonth);
    //    var irin = 0;

    //    var npIn = Tenor; //number of periods (months) tenor ?

    //    var pvIn = Pembiayaan; //present value ; jml pembiayaan
    //    var pvP = parseFloat(pvIn);
    //    pvP = -Math.abs(pvP);

    //    var fvIn = $('#ResidualValue').val().replaceAll(".",""); //future value (residual value)
    //    var fvP = parseFloat(fvIn);

    //    if (stepupdownTr == "2") { //Step Up Step Down
    //        var stepUD = $('#nmbStepAngsuran').val().replaceAll(".",""); //tenornya dari step
    //        npIn = parseFloat(stepUD);
    //    }
    //    //Normal
    //    else {
    //        npIn = Tenor;
    //    }

    //    GetAngs = PMT(irin, npIn, pvP, fvP);

    //}

    Bunga = (GetAngs * Tenor) - Pembiayaan;

    if (isNaN(Bunga)) {
        $('#hdnNilaiBunga').val('');
    }
    else {
        $('#hdnNilaiBunga').val(Bunga);
    }

    if (val != 0)//Tulis angsuran jika 1
    {
        if (isNaN(GetAngs)) {
            $('#AmtInstallment').val('');
        }
        else {
            $('#AmtInstallment').val(GetAngs);
        }
    }

    HitungEffRate(1);
}

var GetAngsuranMenurun = function (Pembiayaan, Tenor, PersenFlat) {
    var cfincome;
    var flatRate;
    var GetAngs = 0;

    if (Tenor == 0) {
        GetAngs = 0;
    }
    else {
        cfincome = Math.round((Pembiayaan / Tenor), -2);
        GetAngs = ((PersenFlat * Pembiayaan) / 100) + cfincome;
    }

    return Math.round(GetAngs);
}

var GetAngsuranTetap = function (Pembiayaan, Tenor, PersenFlat) {
    var GetAngs = 0;
    if (Tenor == 0) {
        GetAngs = 0;
    }
    else {
        GetAngs = ((getNum(Pembiayaan) * getNum(PersenFlat) / 100 * getNum(Tenor) / 12) + getNum(Pembiayaan)) / getNum(Tenor);
        Math.round(GetAngs);
    }
    return Math.round(GetAngs);
}

var getNum = function (val) {
    if (isNaN(val))
        return 0;
    else
        return val;
}

function formatNaN(nilai) {
    if ((isNaN(nilai)) || (nilai == 'undefined')) { return '' }
    else { return nilai }
}