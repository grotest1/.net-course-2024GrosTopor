using Xunit;
using BankSystem.Domain.Models;
using ExportTool;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.App.Exceptions;
using System.Security.Principal;

namespace ExportToolsTests
{
    public class ExportServiceTests
    {

        [Fact]
        public void WriteElementsToCsvClientsPositiveTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            List<Client> clients = clientService.GetClients(c => true);
            string[] path = { "C:", "1", "2"};

            ExportService.WriteElementsToCsv<Client>(clients, path, "test.csv");
        }

        [Fact]
        public void WriteElementsToCsvClientsNegativeTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            List<Client> clients = clientService.GetClients(c => true);
            string[] path = { "C:", "1", "2" };

            Assert.Throws<FormatException>(() => ExportService.WriteElementsToCsv<Client>(clients, path, "test.txt"));   
        }

        [Fact]
        public void ReadElementsFromCsvClientsPositiveTest()
        {
            string[] path = { "C:", "1", "2" };

            var clients = ExportService.ReadElementsFromCsv<Client>(path, "test.csv");
        }

        [Fact]
        public void ReadElementsFromCsvClientsNegativeTest()
        {
            string[] path = { "C:", "Диплом", "авыафы" };

            Assert.Throws<DirectoryNotFoundException>(() => ExportService.ReadElementsFromCsv<Client>(path, "test.csv"));
        }

    }
}