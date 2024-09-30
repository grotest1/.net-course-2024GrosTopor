using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    internal class ClientStorage
    {
        private List<Client> _collection = new List<Client>
        
        public void Add(Client client)
        {
            _collection.Add(client);
        }

        public void Remove(Client client)
        {
            _collection.Remove(client);
        }



       
    }
}
