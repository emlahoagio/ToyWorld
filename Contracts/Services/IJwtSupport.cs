namespace Contracts
{
    public interface IJwtSupport
    {
        string CreateToken(int role, int accountId);
    }
}
