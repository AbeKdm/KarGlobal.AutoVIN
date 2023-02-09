using KarGlobal.AutoVIN.BankingSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public class ClientFactory : IClientFactory
    {
        public IClient? CreateClient(string clientId, string clientName, ClientType clientType)
        {
            switch (clientType)
            {
                case ClientType.Individual:
                    return new Individual(clientId, clientName);
                    break;
                case ClientType.Corporate:
                    return new Corporate(clientId, clientName);
                    break;
            }
            return null;
        }
    }
}
