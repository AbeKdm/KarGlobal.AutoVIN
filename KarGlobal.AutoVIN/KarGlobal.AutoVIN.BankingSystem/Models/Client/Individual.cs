using KarGlobal.AutoVIN.BankingSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public class Individual : BaseClient
    {
        public override ClientType Type => ClientType.Individual;
        public Individual(string clientId, string clientName) : base(clientId, clientName)
        {
        }
    }
}
