using KarGlobal.AutoVIN.BankingSystem.Constants;
using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Account;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models
{
    public class Bank
    {
        public string Name { get; set; }

        private IAccountRepo _accounts;

        private IClientReop _clients;


        public Bank(string BankName, IClientReop clientReop, IAccountRepo accountRepo) {
            this.Name = BankName;
            _clients = clientReop;
            _accounts = accountRepo;
        }

        public IClient SearchClient(string uniqueClientId)
        {
            return _clients.GetClient(uniqueClientId);
        }

        public IClient CreateClient(string clientId, string clientName, ClientType clientType)
        {
            return _clients.CreateClient(clientId, clientName, clientType);
        }

        /*
            1. create accounts can definitely be a function of a client , i.e. client.CreateAccount
            2. can add a functionality of fast retrieval of accounts by clients and vice versa with double referece dictionary
         */

        public IBaseAccount CreateCheckingAccount(IClient accountOwner, double initialDeposit = 0)
        {
            return _accounts.CreateAccount(accountOwner, AccountType.Checking, initialDeposit);
        }

        public IBaseAccount CreateInvestmentAccount(IClient accountOwner, InvestmentAccountType investmentAccountType, double initialDeposit = 0)
        {
            return _accounts.CreateAccount(accountOwner, AccountType.Investment, initialDeposit, investmentAccountType);
        }

        public void Transfer(IBaseAccount sourceAccount, IBaseAccount destinationAccount, double amount)
        {
            if (sourceAccount.AccountNumber.Equals(destinationAccount.AccountNumber))
                throw new Exception(Messages.SOURCE_ACCOUNT_TARGET_ACCOUNT_CANNOT_BE_EQUAL);

            // In real world that function would have been atomic
            sourceAccount.Withdraw(amount);
            destinationAccount.Deposit(amount);
        }

    }
}
