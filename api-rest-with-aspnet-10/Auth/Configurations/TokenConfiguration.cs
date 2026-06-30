namespace api_rest_with_aspnet_10.Auth.Configurations;

public class TokenConfiguration
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Secret { get; set; }
    public int Minutes { get; set; }
    public int DaysToExpiry { get; set; }
}
