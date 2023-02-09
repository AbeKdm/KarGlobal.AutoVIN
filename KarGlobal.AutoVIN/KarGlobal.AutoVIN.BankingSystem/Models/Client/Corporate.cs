using KarGlobal.AutoVIN.BankingSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public class Corporate : BaseClient
    {
        public override ClientType Type => ClientType.Corporate;

        public Corporate(string clientId, string clientName) : base(clientId, clientName)
        {
        }
    }
}
