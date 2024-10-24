using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BankSystem.Domain.Models;
using ExportTool;
using BankSystem.App.Services;
using BankSystem.Data.Storages;

namespace ExportToolsTests
{
    public class ThreadAndTaskTests
    {
        [Fact]
        public void СonveyorProcessingClients()
        {
            List <Client> clients = TestDataGenerator.GenerateClientList(10000);





        }


    }
}
