using FakeItEasy;
using KarGlobal.AutoVIN.BankingSystem.Constants;
using KarGlobal.AutoVIN.BankingSystem.Models;
using KarGlobal.AutoVIN.BankingSystem.Models.Account;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using Shouldly;
using System.Collections.Generic;

namespace KarGlobal.AutoVIN.BankingSystem.Tests.UnitTests
{
    public class ReposTests
    {
        [Fact]
        public void Create_Client_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));

            var target = new ClientReop(clientFactory);
            var clientExists = target.ClientExists("Individual-1");
            clientExists.ShouldBeFalse();

            var client = target.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);
            client.ShouldNotBeNull();
            client.Type.ShouldBe(Enums.ClientType.Individual);
        }

        [Fact]
        public void Create_An_Existing_Client_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));

            var target = new ClientReop(clientFactory);
            var clientExists = target.ClientExists("Individual-1");
            clientExists.ShouldBeFalse();

            var client = target.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);
            client.ShouldNotBeNull();


            var client_same = target.CreateClient("Individual-1", "XXX", Enums.ClientType.Individual);
            client_same.ShouldNotBeNull();
            client_same.ClientName.ShouldNotBe("XXX");
            client_same.ClientName.ShouldBe("Avi Kedem");
        }

        [Fact]
        public void Create_Checking_Account_Test()
        {

            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Checking, 100)).Returns(new Models.Account.Checking(client, 100));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Checking, 100);

            account.ShouldNotBeNull();
            account.AccountType.ShouldBe(Enums.AccountType.Checking);
            account.Balance.ShouldBe(100d);
            account.AccountOwner.ClientId.ShouldBe("Individual-1");
            account.AccountOwner.ClientName.ShouldBe("Avi Kedem");
            account.AccountNumber.ShouldStartWith($"Individual-1-{Enums.AccountType.Checking}"); // $"{accountOwner.ClientId}-{AccountType}-{Guid.NewGuid()}";
        }

        [Fact]
        public void Create_Individual_Investing_Account_Test()
        {

            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            account.ShouldNotBeNull();
            account.AccountType.ShouldBe(Enums.AccountType.Investment);
            account.Balance.ShouldBe(100d);
            account.AccountOwner.ClientId.ShouldBe("Individual-1");
            account.AccountOwner.ClientName.ShouldBe("Avi Kedem");
            account.AccountNumber.ShouldStartWith($"Individual-1-{Enums.AccountType.Investment}"); // $"{accountOwner.ClientId}-{AccountType}-{Guid.NewGuid()}";
            account.HasWithdrawalLimit.ShouldBeTrue();
            account.WithdrawalLimit.ShouldBe(500d);
        }

        [Fact]
        public void Create_Corporate_Investing_Account_Test()
        {

            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Corporate)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Corporate));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Corporate);

            account.ShouldNotBeNull();
            account.AccountType.ShouldBe(Enums.AccountType.Investment);
            account.Balance.ShouldBe(100d);
            account.AccountOwner.ClientId.ShouldBe("Individual-1");
            account.AccountOwner.ClientName.ShouldBe("Avi Kedem");
            account.AccountNumber.ShouldStartWith($"Individual-1-{Enums.AccountType.Investment}"); // $"{accountOwner.ClientId}-{AccountType}-{Guid.NewGuid()}";
            account.HasWithdrawalLimit.ShouldBeFalse();
            account.WithdrawalLimit.HasValue.ShouldBeFalse();
        }

        [Fact]
        public void Create_Account_Error_Test()
        {

            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Corporate)).Returns(null);

            var target = new AccountRepo(accountRefactory);
            var error = Assert.Throws<Exception>(() => target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Corporate));

            error.Message.ShouldBe(Messages.ACCOUNT_OPENING_ERROR);
        }

    }
}
