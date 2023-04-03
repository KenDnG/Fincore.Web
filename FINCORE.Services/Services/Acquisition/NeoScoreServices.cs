using FINCORE.Models.Models.Acquisition.CA;
using FINCORE.Services.Helpers.Request;
using FINCORE.Services.Helpers.Response;
using Newtonsoft.Json;
using RestSharp;
using System.Text.RegularExpressions;
using static FINCORE.Services.Helpers.Domain.EndpointAPI;

namespace FINCORE.Services.Services.Acquisition;

public sealed class NeoScoreServices
{
    private readonly RestClient client = new();

    public APIResponse GetNeoScoreData(string creditid, string userid)
    {
        try
        {
            // temporary hardcoded (comment on production)
            //userid = "220197518";
            //creditid = "0011520221100014";
            //string NeoAPIUrl = $"http://macfto.mcf.co.id/NeoScoreAPI/api/NeoScoreHTML?CasID=1152100061&UserID=220130650";
            // temporary hardcoded end

            //temporary comented(Uncomment on production)
            string NeoAPIUrl = $"http://macfto.mcf.co.id/NeoScoreAPI/api/NeoScoreHTML?CasID={creditid}&UserID={userid}";

            NeoScoreRequestDTO? resquestParam = GetNeoScoreParam(creditid);

            if (resquestParam is null)
                return new APIResponse { code = Collection.Codes.BAD_REQUEST, message = "", status = Collection.Status.FAILED, data = new { } };

            //access NEO API

            object bodyParam = MapRequestDtoToParam(resquestParam);

            RestResponse neoAPIResponse = GetApiResponseAsync(NeoAPIUrl, Method.Post, bodyParam);

            var stringResult = neoAPIResponse?.Content;

            if (neoAPIResponse?.StatusCode != System.Net.HttpStatusCode.OK || string.IsNullOrEmpty(stringResult))
                return new APIResponse { code = Collection.Codes.BAD_REQUEST, message = "Data Not found", status = Collection.Status.FAILED, data = new { } };

            stringResult = Regex.Unescape(stringResult);

            stringResult = stringResult[1..^1];

            if (stringResult.Contains("message"))
            {
                //var error  = JsonConvert.DeserializeObject<NeoScoreError>(stringResult);
                return new APIResponse
                {
                    code = Collection.Codes.BAD_REQUEST,
                    message = "", //error?.message??"",
                    status = Collection.Status.FAILED,
                    data = new { }
                };
            }

            //Save to API

            var paramJsonString = JsonConvert.SerializeObject(bodyParam);

            var result = InsertNeoLog(new NeoScoreInsertDTO { CreditId = creditid, UserId = userid, Result = stringResult, Parameter = paramJsonString });

            return new APIResponse
            {
                code = Collection.Codes.SUCCESS,
                message = Collection.Messages.SUCCESS,
                status = Collection.Status.SUCCESS,
                data = stringResult ?? ""
            };
        }
        catch (Exception ex)
        {
            return new APIResponse
            {
                code = Collection.Codes.INTERNAL_SERVER_ERROR,
                message = ex.Message + "|" + ex.InnerException?.Message,
                status = Collection.Status.FAILED,
                data = new { }
            };
        }
    }

    private bool InsertNeoLog(NeoScoreInsertDTO insertParam)
    {
        string CaNeoScoreInsertAPIUrl = $"{Endpoint.DOMAIN_CA}/{Route.ROUTE_NEO_SCORE}/insert-neoscore";

        var bodyParam = MapRequestDtoToParam(insertParam);

        var responseResult = GetApiResponseAsync(CaNeoScoreInsertAPIUrl, Method.Post, bodyParam);

        if (responseResult.IsSuccessful)
            return true;

        return false;
    }

    private static object MapRequestDtoToParam(NeoScoreInsertDTO insertParam)
    {
        return new
        {
            creditId = insertParam.CreditId,
            userId = insertParam.UserId,
            method = insertParam.Method,
            menu = insertParam.Menu,
            menuDetail = insertParam.MenuDetail,
            parameter = insertParam.Parameter,
            result = insertParam.Result
        };
    }

    private static object MapRequestDtoToParam(NeoScoreRequestDTO resquestParam)
    {
        // temporary hardcoded (comment on production)
        //return new
        //{
        //    profesi = "Karyawan Swasta",
        //    tipe_motor = "ALL NEW BEAT STREET FI ESP CW CBS",
        //    kode_pos = "16340",
        //    tipe_industri = "ANEKA BISNIS",
        //    jenis_kelamin = "Pria",
        //    tenor = "33",
        //    marriage_status = "Menikah",
        //    kepemilikan_rumah = "Milik Keluarga",
        //    penjamin = "Tidak",
        //    penghasilan_bulanan = 4150000,
        //    penghasilan_tambahan = "",
        //    installment = "777000",
        //    dp = "2700000",
        //    asset_cost = "17950000",
        //    umur = "35",
        //    jumlah_tanggungan = "3",
        //    tahun = "2022",
        //    nik = "3201340106870004",
        //    name = "TOPIK HIDAYAT",
        //    phone = "08397417907",
        //};

        //temporary comented(Uncomment on production)

        return new
        {
            profesi = resquestParam.Profession,
            tipe_motor = resquestParam.ItemTypeName,
            kode_pos = resquestParam.ZipCode,
            tipe_industri = resquestParam.IndustryType,
            jenis_kelamin = resquestParam.Gender,
            tenor = resquestParam.Tenor,
            marriage_status = resquestParam.MarriageStatus,
            kepemilikan_rumah = resquestParam.HomeOwnershipStatus,
            penjamin = resquestParam.HasGuarantor,
            penghasilan_bulanan = resquestParam.MonthlyIncome,
            penghasilan_tambahan = resquestParam.AdditionalIncome,
            installment = resquestParam.Installment,
            dp = resquestParam.DownPayment,
            asset_cost = resquestParam.AssetCost,
            umur = resquestParam.Age,
            jumlah_tanggungan = resquestParam.NumberOfDependents,
            tahun = resquestParam.Year,
            nik = resquestParam.NIK,
            name = resquestParam.Name,
            phone = resquestParam.Phone,
        };
    }

    public NeoScoreRequestDTO? GetNeoScoreParam(string creditid)
    {
        string CaNeoScoreGetAPIUrl = $"{Endpoint.DOMAIN_CA}/{Route.ROUTE_NEO_SCORE}/get-neoscore-param?creditid={creditid}";

        var responseResult = GetApiResponseAsync(CaNeoScoreGetAPIUrl, Method.Get);

        var jsonString = responseResult.Content;

        if (responseResult.StatusCode != System.Net.HttpStatusCode.OK || string.IsNullOrEmpty(jsonString))
            return null;

        var apiResponse = JsonConvert.DeserializeObject<APIResponse>(jsonString);

        if (apiResponse is null || apiResponse.data is null)
            return null;

        var result = JsonConvert.DeserializeObject<NeoScoreRequestDTO>(JsonConvert.SerializeObject(apiResponse.data));

        return result;
    }

    private RestResponse GetApiResponseAsync(string url, Method method, object? reqBody = null)
    {
        var req = new RestRequest(url, method);

        if (reqBody is not null)
            req.AddParameter(BaseRequest.Headers.CONTENT_TYPE, JsonConvert.SerializeObject(reqBody), ParameterType.RequestBody);

        RestResponse response = client.Execute<RestResponse>(req);

        return response;
    }
}