using Xunit;
using BankSystem.Domain.Models;
using ExportTool;

namespace ExportToolsTests
{
    public class ExportServiceTests
    {

        [Fact]
        public void WritePersonsToCsvPositiveTest()
        {
            List<Client> clients = new List<Client>();
            clients.Add(new Client() { Name = "fdsfsa", Age = 55 });
            string[] path = { "C:", "1", "2", "test.csv" };

            ExportService.WriteClientsToCsv(clients, path);


        }

    }
}