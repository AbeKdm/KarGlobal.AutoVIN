using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public interface IBaseAccount
    {
        string AccountNumber { get; }
        IClient AccountOwner { get; }
        double Balance { get; }
        abstract AccountType AccountType { get; }

        bool HasWithdrawalLimit { get; }
        double? WithdrawalLimit { get; }
        double? OverdraftLimit { get; }
        void Deposit(double amount);
        double Withdraw(double amount);
    }
}