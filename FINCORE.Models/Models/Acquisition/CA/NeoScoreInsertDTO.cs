namespace FINCORE.Models.Models.Acquisition.CA;

public class NeoScoreInsertDTO
{
    public string? CreditId { get; set; }
    public string? UserId { get; set; }
    public string? Method { get; set; } = "POST";
    public string? Menu { get; set; } = "ScoreCard";
    public string? MenuDetail { get; set; } = "NeoScoreHTML";
    public string? Parameter { get; set; } = "";
    public string? Result { get; set; }
}