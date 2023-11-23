namespace Domain;

public class AppSettings
{
    public string APIGetCustomerExternal { get; init; } = string.Empty;
    public string SystemID { get; set; } = string.Empty;
    public bool DevMode { get; set; } = false;
    public string LinkAS400 { get; init; } = string.Empty;
    public Endpoints Endpoints { get; set; } = new();
}

public class Endpoints
{
    public string LinkAS400 { get; init; } = string.Empty;
    public string LinkUserManagement { get; init; } = string.Empty;
}
