using Xunit;
using BankSystem.Domain.Models;
using ExportTool;
using BankSystem.App.Services;
using BankSystem.Data.Storages;

namespace ExportToolsTests
{
    public class ExportServiceTests
    {
        [Fact]
        public void WriteElementsToCsvClientsPositiveTest()
        {
            using (ClientStorageEF clientStorageEF = new ClientStorageEF())
            {
                ClientService clientService = new ClientService(clientStorageEF);
                List<Client> clients = clientService.GetClients(c => true);
                string[] path = { "C:", "1", "2"};

                ExportService.WriteElementsToCsv<Client>(clients, path, "test.csv");
            }
        }

        [Fact]
        public void WriteElementsToCsvClientsNegativeTest()
        {
            using (ClientStorageEF clientStorageEF = new ClientStorageEF())
            {
                ClientService clientService = new ClientService(clientStorageEF);
                List<Client> clients = clientService.GetClients(c => true);
                string[] path = { "C:", "1", "2" };

                Assert.Throws<FormatException>(() => ExportService.WriteElementsToCsv<Client>(clients, path, "test.txt"));   
            }
        }

        [Fact]
        public void ReadElementsFromCsvClientsPositiveTest()
        {
            string[] path = { "C:", "1", "2" };

            List<Client> clients = ExportService.ReadElementsFromCsv<Client>(path, "test.csv");

            Assert.NotEmpty(clients);
        }

        [Fact]
        public void ReadElementsFromCsvClientsNegativeTest()
        {
            string[] path = { "C:", "Диплом" };

            Assert.Throws<DirectoryNotFoundException>(() => ExportService.ReadElementsFromCsv<Client>(path, "test.csv"));
        }
    }
}