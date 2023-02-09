using KarGlobal.AutoVIN.BankingSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public abstract class BaseClient : IClient
    {
        public abstract ClientType Type { get; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }

        public BaseClient(string clientId, string clientName)
        {
            ClientId = clientId;
            ClientName = clientName;
        }
    }
}
