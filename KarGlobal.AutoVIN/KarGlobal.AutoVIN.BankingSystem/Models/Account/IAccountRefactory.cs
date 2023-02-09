using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public interface IAccountRefactory
    {
        IBaseAccount? CreateAccount(IClient accountOwner, AccountType accountType, double initialDeposit = 0, params dynamic[] extraValues);
    }
}