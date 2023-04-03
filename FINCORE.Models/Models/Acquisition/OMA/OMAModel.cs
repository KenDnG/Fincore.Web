﻿using System.ComponentModel.DataAnnotations;

namespace FINCORE.Models.Models.Acquisition.OMA
{
    public class OMAModel
    {
        public string? created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string? last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }
        public string? order_id { get; set; }
        public DateTime? order_date { get; set; }
        public string? company_id { get; set; }
        public string? branch_id { get; set; }
        public string? customer_name { get; set; }
        public string? identity_type_id { get; set; }
        public string? identity_number { get; set; }
        public string? survey_address { get; set; }
        public int? province_survey_id { get; set; }
        public int? regency_survey_id { get; set; }
        public int? district_survey_id { get; set; }
        public int? village_survey_id { get; set; }
        public string? survey_address_desc { get; set; }
        public DateTime? survey_appointment { get; set; }
        public string? survey_time { get; set; }
        public string? phone1 { get; set; }
        public string? phone2 { get; set; }
        public string? item_merk_type_id { get; set; }
        public byte? tenor { get; set; }
        public decimal? gross_down_payment { get; set; }
        public string? status_order { get; set; }
        public string? NIK_surveyor_code { get; set; }
        public string? approve_by { get; set; }
        public DateTime? approve_date { get; set; }
        public string? reject_by { get; set; }
        public DateTime? reject_date { get; set; }
        public string? profession_id { get; set; }
        public int? industry_type_id { get; set; }
        public string? position_id { get; set; }
        public int? lama_bekerja_id { get; set; }
        public int? income_id { get; set; }
        public int? kepemilikan_usaha_id { get; set; }
        public string? home_status_id { get; set; }
        public int? home_time_stay_id { get; set; }
        public int? marital_id { get; set; }
        public string? photo_KTP_pemohon { get; set; }
        public string? photo_KTP_pasangan { get; set; }
        public string? photo_NPWP { get; set; }
        public string? APPI_result { get; set; }
        public string? recommendation { get; set; }

        [Required]
        public string? APPI_file_name { get; set; }

        public string? APPI_file_path { get; set; }
        public string? pefindo_file_name { get; set; }
        public string? pefindo_file_path { get; set; }
        public string? pefindo_score { get; set; }
        public string? pefindo_result { get; set; }
        public string? description { get; set; }
        public string? application_type_id { get; set; }
        public DateTime? birth_date { get; set; }
        public string? marital_status { get; set; }
        public string? spouse_name { get; set; }
        public string? spouse_identity_number { get; set; }
        public string? spouse_birth_date { get; set; }
        public DateTime? sync_date { get; set; }
        public string? asset_brand_id { get; set; }
        public string? SLIK_result_pemohon { get; set; }

        [Required]
        public string? SLIK_file_name_pemohon { get; set; }

        public string? SLIK_file_path_pemohon { get; set; }
        public string? SLIK_result_pasangan { get; set; }

        [Required]
        public string? SLIK_file_name_pasangan { get; set; }

        public string? SLIK_file_path_pasangan { get; set; }
        public string? Nama { get; set; }
        public string? NoPeg { get; set; }
        public string? asset_type_description { get; set; }
        public string? province_name { get; set; }
        public string? regency_name { get; set; }
        public string? district_name { get; set; }
        public string? village_name { get; set; }
    }
}