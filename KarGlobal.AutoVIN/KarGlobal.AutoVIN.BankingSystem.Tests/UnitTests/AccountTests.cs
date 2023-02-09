using FakeItEasy;
using KarGlobal.AutoVIN.BankingSystem.Constants;
using KarGlobal.AutoVIN.BankingSystem.Models;
using KarGlobal.AutoVIN.BankingSystem.Models.Account;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using Shouldly;
using System.Collections.Generic;

namespace KarGlobal.AutoVIN.BankingSystem.Tests.UnitTests
{
    public class AccountTests
    {
        [Fact]
        public void Deposit_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            account.Deposit(500);

            account.Balance.ShouldBe(600d);  // initial deposit was 100
        }

        [Fact]
        public void Deposit_Negative_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            var error = Assert.Throws<Exception>(() => account.Deposit(-1));
            error.Message.ShouldBe(Messages.ERROR_DEPOSIT_NEGATIVE);
        }

        [Fact]
        public void Withdraw_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            account.Withdraw(51);

            account.Balance.ShouldBe(49d);  // initial deposit was 100
        }


        [Fact]
        public void Withdraw_Nagative_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            var error = Assert.Throws<Exception>(() => account.Withdraw(-1));
            error.Message.ShouldBe(Messages.ERROR_WITHDRAW_NEGATIVE);
        }

        [Fact]
        public void Withdraw_WithdrawLimit_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            account.Withdraw(500);
            account.Withdraw(500);
            account.Withdraw(500);
            account.Withdraw(500);

            account.Balance.ShouldBe(-1900);
        }

        [Fact]
        public void Withdraw_WithdrawLimit_Over_Limit_Test()
        {
            IClientFactory clientFactory = A.Fake<IClientFactory>();
            A.CallTo(() => clientFactory.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual)).Returns(new Models.Client.Individual("Individual-1", "Avi Kedem"));
            var clientReop = new ClientReop(clientFactory);
            var client = clientReop.CreateClient("Individual-1", "Avi Kedem", Enums.ClientType.Individual);

            IAccountRefactory accountRefactory = A.Fake<IAccountRefactory>();
            A.CallTo(() => accountRefactory.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual)).Returns(new Models.Account.Investment(client, 100, Enums.InvestmentAccountType.Individual));

            var target = new AccountRepo(accountRefactory);
            var account = target.CreateAccount(client, Enums.AccountType.Investment, 100, Enums.InvestmentAccountType.Individual);

            account.Withdraw(500);
            account.Withdraw(500);
            account.Withdraw(500);
            account.Withdraw(500);

            account.Balance.ShouldBe(-1900);

            var error = Assert.Throws<Exception>(() => account.Withdraw(501));
            var excpectedErrorMessage = Messages.WITHDRAW_LIMIT.Replace("$1", account.WithdrawalLimit.ToString());
            error.Message.ShouldBe(excpectedErrorMessage);
        }

    }
}
