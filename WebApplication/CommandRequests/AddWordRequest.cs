namespace WebApplication.CommandRequests;

public class AddWordRequest
{
    public string WordToAddEnglish { get; set; }
    public string WordToAddRussian { get; set; }
    public string WordToAddUserLevel { get; set; }
}