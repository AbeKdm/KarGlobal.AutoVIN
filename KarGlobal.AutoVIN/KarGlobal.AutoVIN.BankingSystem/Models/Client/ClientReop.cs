using KarGlobal.AutoVIN.BankingSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarGlobal.AutoVIN.BankingSystem.Models.Client
{
    public class ClientReop : IClientReop
    {

        private Dictionary<string, IClient> repo;
        private readonly IClientFactory _clientFactory;

        public ClientReop(IClientFactory clientFactory)
        {
            repo = new Dictionary<string, IClient>();
            _clientFactory = clientFactory;
        }

        // if not exists - creates a client , else - return existing one
        public IClient CreateClient(string clientId, string clientName, ClientType clientType)
        {
            // Idempotent 
            if (!ClientExists(clientId))
                repo.Add(clientId, _clientFactory.CreateClient(clientId, clientName, clientType));

            return repo[clientId];
        }

        public IClient? GetClient(string uniqueClientId) => ClientExists(uniqueClientId) ? repo[uniqueClientId] : null;

        public bool ClientExists(string uniqueClientId) => repo.ContainsKey(uniqueClientId);

    }
}
