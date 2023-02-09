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
    public class AccountRefactory : IAccountRefactory
    {
        public IBaseAccount? CreateAccount(IClient accountOwner, AccountType accountType, double initialDeposit = 0, params dynamic[] extraValues)
        {
            switch (accountType)
            {
                case AccountType.Checking:
                    return new Account.Checking(accountOwner, initialDeposit);
                case AccountType.Investment:
                    if (extraValues[0] is InvestmentAccountType)
                    {

                        return new Account.Investment(accountOwner, initialDeposit, extraValues[0]);
                    }
                    else
                    {
                        throw new Exception(Messages.ACCOUNTTYPE_INVESTMENT_WITH_NO_SUBTYPE);
                    }
            }
            return null;
        }
    }
}
