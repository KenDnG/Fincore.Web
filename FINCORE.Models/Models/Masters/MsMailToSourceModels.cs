namespace FINCORE.Models.Models.Masters
{
    public record MsMailToSourceModels(
                string CreatedBy,
                DateTime? CreatedOn,
                string LastUpdatedBy,
                DateTime? LastUpdatedOn,
                int MailToSourceId,
                string MailToSourceDesc,
                bool IsActive
        );
}