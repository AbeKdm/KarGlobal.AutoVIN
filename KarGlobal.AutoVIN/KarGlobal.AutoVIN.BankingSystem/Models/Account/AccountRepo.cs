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
    public class AccountRepo : IAccountRepo
    {
        private Dictionary<string, IBaseAccount> repo;
        private readonly IAccountRefactory _accountFactory;

        public AccountRepo(IAccountRefactory accountFactory)
        {
            repo = new Dictionary<string, IBaseAccount>();
            _accountFactory = accountFactory;
        }

        public IBaseAccount CreateAccount(IClient accountOwner, AccountType accountType, double initialDeposit = 0, params dynamic[] extraValues)
        {
            var newAccount = _accountFactory.CreateAccount(accountOwner, accountType, initialDeposit, extraValues);
            if (newAccount == null)
                throw new Exception(Messages.ACCOUNT_OPENING_ERROR);

            repo.Add(newAccount.AccountNumber, newAccount);

            return newAccount;
        }
    }
}
