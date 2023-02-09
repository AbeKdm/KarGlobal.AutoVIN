using KarGlobal.AutoVIN.BankingSystem.Enums;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public interface IClient
    {
        abstract ClientType Type { get; }
        string ClientId { get; set; }
        string ClientName { get; set; }
    }
}