using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public class Checking : BaseAccount
    {
        public Checking(IClient accountOwner, double initialDeposit) : base(accountOwner, initialDeposit)
        {
        }

        public override AccountType AccountType => AccountType.Checking;
    }
}
