namespace HackNewsApp.Api.Authorisation
{
    public class AuthorisationPolicies
    {
        public const string CanViewHackNews = nameof(CanViewHackNews);
        public const string AdminOnly = nameof(AdminOnly);
    }
}
