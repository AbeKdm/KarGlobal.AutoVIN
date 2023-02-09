using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public class Investment : BaseAccount, IInvestmentAccount
    {
        private InvestmentAccountType _investmentAccountType;

        public Investment(IClient accountOwner, double initialDeposit, InvestmentAccountType investmentAccountType) : base(accountOwner, initialDeposit)
        {
            this.InvestmentAccountType = investmentAccountType;
        }

        public override AccountType AccountType => AccountType.Investment;

        public InvestmentAccountType InvestmentAccountType {
            get
            {
                return _investmentAccountType;
            }
            set
            {
                _investmentAccountType = value;
                switch (value)
                {
                    case InvestmentAccountType.Individual:
                        WithdrawalLimit = 500;
                        break;
                    case InvestmentAccountType.Corporate:
                        WithdrawalLimit = null;
                        break;
                    default:
                        WithdrawalLimit = null;
                        break;
                }

            }
        }
    }
}
