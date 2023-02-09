using KarGlobal.AutoVIN.BankingSystem.Enums;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public interface IClientReop
    {
        IClient CreateClient(string clientId, string clientName, ClientType clientType);
        IClient GetClient(string uniqueClientId);
        bool ClientExists(string uniqueClientId);
    }
}