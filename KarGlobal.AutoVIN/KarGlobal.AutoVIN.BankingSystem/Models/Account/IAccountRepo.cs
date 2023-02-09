using KarGlobal.AutoVIN.BankingSystem.Enums;
using KarGlobal.AutoVIN.BankingSystem.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Account
{
    public interface IAccountRepo
    {
        IBaseAccount CreateAccount(IClient accountOwner, AccountType accountType, double initialDeposit = 0, params dynamic[] extraValues);
    }
}
