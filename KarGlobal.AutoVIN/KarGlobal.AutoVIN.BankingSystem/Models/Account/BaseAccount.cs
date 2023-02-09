using KarGlobal.AutoVIN.BankingSystem.Constants;
using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public abstract class BaseAccount : IBaseAccount
    {
        public BaseAccount(IClient accountOwner, double initialDeposit)
        {
            AccountOwner = accountOwner;
            Deposit(initialDeposit);
            WithdrawalLimit = null;
            OverdraftLimit = null;
            AccountNumber = $"{accountOwner.ClientId}-{AccountType}-{Guid.NewGuid()}";
        }

        public string AccountNumber { get; private set; }

        public IClient AccountOwner { get; private set; }

        public double Balance { get; private set; }

        public abstract AccountType AccountType { get; }

        public bool HasWithdrawalLimit => this.WithdrawalLimit.HasValue && this.WithdrawalLimit.Value >= 0;

        public double? WithdrawalLimit { get; set; }

        public double? OverdraftLimit { get; set; }

        public void Deposit(double amount)
        {
            // validation
            if (amount <= 0)
                throw new Exception(Messages.ERROR_DEPOSIT_NEGATIVE);
            Balance += amount;
        }

        public double Withdraw(double amount)
        {
            // validation
            if (amount <= 0)
                throw new Exception(Messages.ERROR_WITHDRAW_NEGATIVE);

            var withdrawResults = Balance - amount;

            if (HasWithdrawalLimit && amount > WithdrawalLimit)
                throw new Exception(Messages.WITHDRAW_LIMIT.Replace("$1", WithdrawalLimit.ToString()));

            if (OverdraftLimit.HasValue && withdrawResults < 0 && Math.Abs(withdrawResults) > OverdraftLimit.Value)
                throw new Exception(Messages.OVERDRAFT);

            Balance -= amount;

            return Balance;
        }

    }
}
