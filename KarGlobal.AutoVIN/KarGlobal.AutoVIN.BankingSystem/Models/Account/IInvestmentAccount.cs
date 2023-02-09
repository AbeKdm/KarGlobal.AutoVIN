using KarGlobal.AutoVIN.BankingSystem.Enums;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public interface IInvestmentAccount
    {
        InvestmentAccountType InvestmentAccountType { get; set; }
    }
}