using FINCORE.Models.Models.Acquisition.CA;
using FINCORE.Services.Helpers.Response;

namespace FINCORE.Services.IServices.IAcquisition
{
    public interface ICACarServices
    {
        Task<APIResponse> Get_ApprovalReason(string UserId, string reason_id);

        Task<APIResponse> Get_ms_photo_type(string credit_id);

        Task<APIResponse> Get_Tr_CM_photo_detail(string credit_id, string photo_type_id, string photo_id);

        Task<APIResponse> Get_ca_car_detail(string credit_id, string CreatedBy);

        Task<APIResponse> InsertCMOAnalisa(CACarModels item);

        Task<APIResponse> InsertCMPhotoDetail(ViewModelCAPhotoType item);

        Task<APIResponse> ApproveCM(CACarModels item);

        Task<APIResponse> Get_History_Approval(string credit_id);
    }
}