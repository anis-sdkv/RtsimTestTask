namespace RtsimTestTask.Infrastructure.Persistence.Options;

public class InfrastructureOptions
{
    public string ConnectionString { get; init; }
    public AdminData AdminAccountData { get; init; }

    public class AdminData
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}