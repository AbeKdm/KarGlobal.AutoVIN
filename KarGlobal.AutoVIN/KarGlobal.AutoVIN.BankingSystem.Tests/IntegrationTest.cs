using FakeItEasy;
using KarGlobal.AutoVIN.BankingSystem.Models;
using KarGlobal.AutoVIN.BankingSystem.Models.Account;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using Shouldly;
using System.Collections.Generic;

namespace KarGlobal.AutoVIN.BankingSystem.Tests
{
    public class IntegrationTest
    {
        [Fact]
        public void Test1()
        {
            /*
             
                1st iteration / How I invision it : 

                Bank BofA = new Bank("Bank of America);
                
                IClient clientA = Clients.Search("UniqueID"); // seach a client by unique identifier (to simplify things)
                
                if (clientA == null)
                    clientA = Clients.CreateClient("Individua", "Client Params"); // Factory

                var clientACheckingAccountNumber = BofA.AddAccount(clientA,Enum.Checking);

                var clientAInvestmentAccountNumber = BofA.AddAccount(clientA,Enum.Investment); // a client can have multiple investment accounts and a single checking account

                var clientAInvestmentAccount2Number = BofA.AddAccount(clientA,Enum.Investment); // a client can have multiple investment accounts and a single checking account
                
                for each IAccount in clientA.Accounts
                    IAccount.Number
                    IAccount.Owner
                    IAccount.Type
                    IAccount.Balance

                clientA.GetAccount(..).Deposit(500.5);
                clientA.GetAccount(..).Withdraw(300);
                clientA.Transfer(AccountA,AccountB,55.5);

                

             */
            //_clientFactory.CreateClient(clientId, clientName, clientType));

            // PREPS

            IClientFactory clientFactory = new ClientFactory(); // of fake
            IClientReop clientRepo = new ClientReop(clientFactory); // of fake

            IAccountRefactory accountFactory = new AccountRefactory();
            IAccountRepo accountRepo = new AccountRepo(accountFactory);

            var bofa = new Bank("Bank of America", clientRepo, accountRepo);
            var bofaClientAvi = bofa.CreateClient("Client-Avi-Kedem-Ind", "Avi Kedem", Enums.ClientType.Individual);
            var bofaClientCorp1 = bofa.CreateClient("Client-Corp1-Cor", "Corp 1", Enums.ClientType.Corporate);
            var bofaClientCorp1_secondTry = bofa.CreateClient("Client-Corp1-Cor", "XXXXXXXXXXX", Enums.ClientType.Corporate);

            var checkingAccountAvi = bofa.CreateCheckingAccount(bofaClientAvi, 500);
            var investmentCorporateAccountAvi = bofa.CreateInvestmentAccount(bofaClientAvi, Enums.InvestmentAccountType.Corporate, 1000);
            var investmentIndividualAccountAvi = bofa.CreateInvestmentAccount(bofaClientAvi, Enums.InvestmentAccountType.Individual, 1500);

            checkingAccountAvi.AccountOwner.ClientId.ShouldBe("Client-Avi-Kedem-Ind");
            investmentCorporateAccountAvi.AccountOwner.ClientId.ShouldBe("Client-Avi-Kedem-Ind");
            investmentIndividualAccountAvi.AccountOwner.ClientId.ShouldBe("Client-Avi-Kedem-Ind");

            checkingAccountAvi.Balance.ShouldBe(500d);
            investmentCorporateAccountAvi.Balance.ShouldBe(1000d);
            investmentIndividualAccountAvi.Balance.ShouldBe(1500d);

            checkingAccountAvi.Deposit(5);
            investmentCorporateAccountAvi.Deposit(6);
            investmentIndividualAccountAvi.Deposit(7);

            checkingAccountAvi.Balance.ShouldBe(505d);
            investmentCorporateAccountAvi.Balance.ShouldBe(1006d);
            investmentIndividualAccountAvi.Balance.ShouldBe(1507d);

            checkingAccountAvi.Withdraw(5);
            investmentCorporateAccountAvi.Withdraw(6);
            investmentIndividualAccountAvi.Withdraw(7);

            checkingAccountAvi.Balance.ShouldBe(500d);
            investmentCorporateAccountAvi.Balance.ShouldBe(1000d);
            investmentIndividualAccountAvi.Balance.ShouldBe(1500d);


            checkingAccountAvi.Withdraw(2000);
            investmentCorporateAccountAvi.Withdraw(2000);
            Exception WithdrawLimitIssue = Assert.Throws<Exception>( () => investmentIndividualAccountAvi.Withdraw(2000));
            WithdrawLimitIssue.Message.ShouldBe("There is a withdraw limit of 500");


            // ASSERTS

            bofaClientAvi.Type.ShouldBe(Enums.ClientType.Individual);
            bofaClientCorp1.Type.ShouldBe(Enums.ClientType.Corporate);
            bofaClientCorp1_secondTry.Type.ShouldBe(Enums.ClientType.Corporate);

            bofaClientAvi.ClientName.ShouldBe("Avi Kedem");
            bofaClientCorp1.ClientName.ShouldBe("Corp 1");
            bofaClientCorp1_secondTry.ClientName.ShouldBe("Corp 1");

            bofa.Name.ShouldBe("Bank of America");

        }
    }
}