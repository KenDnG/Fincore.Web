﻿namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class PoolingOrderModels
    {
        public string order_id { get; set; }
        public string ktp_no { get; set; }
        public string customer_name { get; set; }
        public string branch_id { get; set; }
        public int? identitas_id { get; set; }
        public string id_no { get; set; }
        public string alamat_survey { get; set; }
        public int? province_survey_id { get; set; }
        public int? regency_survey_id { get; set; }
        public int? district_survey_id { get; set; }
        public int? village_survey_id { get; set; }
        public string penjelasan_alamat_survey { get; set; }
        public DateTime? janji_survey { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string dealer_code { get; set; }
        public string item_merk_type_id { get; set; }
        public byte? tenor { get; set; }
        public decimal? uang_muka { get; set; }
        public string status_order { get; set; }
        public string track_order { get; set; }
        public DateTime? tanggal_tagihan { get; set; }
        public string submit_by { get; set; }
        public DateTime? submit_date { get; set; }
        public string surcode { get; set; }
        public string approve_by { get; set; }
        public DateTime? approve_date { get; set; }
        public string reject_by { get; set; }
        public DateTime? reject_date { get; set; }
        public bool? is_sended_sms { get; set; }
        public DateTime? sended_sms_on { get; set; }
        public string created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string profession_id { get; set; }
        public int? industry_type_id { get; set; }
        public string jabatan_id { get; set; }
        public int? lama_bekerja { get; set; }
        public int? income_id { get; set; }
        public int? kepemilikan_usaha_id { get; set; }
        public string home_status_id { get; set; }
        public int? home_time_stay_id { get; set; }
        public int? marital_id { get; set; }
        public string photo_ktp_pemohon { get; set; }
        public string photo_ktp_pasangan { get; set; }
        public string photo_npwp { get; set; }
        public string appi_result { get; set; }
        public string rekomendasi { get; set; }
        public string appi_filename { get; set; }
        public string appi_filepath { get; set; }
        public string pefindo_filename { get; set; }
        public string pefindo_filepath { get; set; }
        public string pefindo_score { get; set; }
        public string pefindo_result { get; set; }
        public string keterangan { get; set; }
        public string tipe_aplikasi_id { get; set; }
        public DateTime? birth_date { get; set; }
        public string marital_status { get; set; }
        public string spouse_name { get; set; }
        public string spouse_id_no { get; set; }
        public string spouse_birth_date { get; set; }
        public DateTime? sync_date { get; set; }
        public string asset_brand_id { get; set; }
        public string slik_result_pemohon { get; set; }
        public string slik_filepath_pemohon { get; set; }
        public string slik_result_pasangan { get; set; }
        public string slik_filename_pasangan { get; set; }
        public string slik_filepath_pasangan { get; set; }
        public string is_pt { get; set; }
    }
}