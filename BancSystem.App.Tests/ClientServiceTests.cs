using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Services;
using Xunit;
using BankSystem.Domain.Models;

namespace BancSystem.App.Tests
{
    public class ClientServiceTests
    {
        [Fact]
        public void AddClientPositive()
        {
            ClientService clientService = new ClientService();
            
            int countBefore = clientService.GetClientsCount()
            clientService.AddClient(new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 });
            int countAfter = clientService.GetClientsCount();
            
            Assert.Equal(0, countBefore);
            Assert.Equal(1, countAfter);
        }


    }
}
