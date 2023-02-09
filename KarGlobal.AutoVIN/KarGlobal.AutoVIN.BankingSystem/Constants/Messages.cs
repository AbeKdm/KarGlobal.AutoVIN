using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Constants
{
    public static class Messages
    {
        public const string ERROR_DEPOSIT_NEGATIVE = "Deposit should be grater than zero";
        public const string ERROR_WITHDRAW_NEGATIVE = "Withdraw should be grater than zero";
        public const string WITHDRAW_LIMIT = "There is a withdraw limit of $1";
        public const string OVERDRAFT = "Cannot overdraft";
        public const string ACCOUNTTYPE_INVESTMENT_WITH_NO_SUBTYPE = "AccountType.Investment Has to have an account sub type";
        public const string ACCOUNT_OPENING_ERROR = "Error in opening an account";
        public const string SOURCE_ACCOUNT_TARGET_ACCOUNT_CANNOT_BE_EQUAL = "Source account and target account must be different";
    }
}
