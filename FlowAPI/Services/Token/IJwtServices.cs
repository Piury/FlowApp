namespace FlowDb.Services.Token;
public interface IJwtServices
{
    public string GetToken(string name, string email, bool superAdmin);
}