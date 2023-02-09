using KarGlobal.AutoVIN.BankingSystem.Enums;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public interface IClientFactory
    {
        IClient CreateClient(string clientId, string clientName, ClientType clientType);
    }
}