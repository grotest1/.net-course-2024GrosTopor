using BankSystem.Domain.Models;
using Xunit;
using ExportTool;
using BankSystem.App.Services;
using Newtonsoft.Json;

namespace ExportToolsTests
{
    public class ThreadAndTaskTests
    {
        [Fact]
        public void СonveyorProcessingClients()
        {
            List<Client> clients1 = TestDataGenerator.GenerateClientList(100);
            List<Client> clients2 = TestDataGenerator.GenerateClientList(200);
            List<Client> clients3 = TestDataGenerator.GenerateClientList(300);
            int countClients = clients1.Count + clients2.Count + clients3.Count;

            Queue<Client> conveyor = [];
            int addedToConveyor = 0;

            var lockerСonveyor = new object();

            Random pauseMS = new Random();
            WaitCallback waitCallback = delegate (object? clientsObj)
            {
                if (clientsObj is List<Client> clients)
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        lock (lockerСonveyor)
                        {
                            conveyor.Enqueue(clients[i]);
                            addedToConveyor++;
                        }
                        Thread.Sleep(pauseMS.Next(10, 300));
                    }
                }
            };
            ThreadPool.QueueUserWorkItem(waitCallback, clients1);
            ThreadPool.QueueUserWorkItem(waitCallback, clients2);
            ThreadPool.QueueUserWorkItem(waitCallback, clients3);

            string[] pathToDirectory = ["C:", "1", "2"];
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            int addedToFiles = 0;
            int fileCounter = 0;
            long fileStreamLength = 0;
            
            List<Client> clientsToFile = new List<Client>();

            while (addedToConveyor < countClients || conveyor.Count > 0)
            {
                while (conveyor.Count == 0)
                    Thread.Sleep(200);

                if (fileStreamLength > 10000)
                {
                    fileCounter++;
                    clientsToFile.Clear();
                }

                lock (lockerСonveyor)
                {
                    clientsToFile.Add(conveyor.Dequeue());
                }

                string fullPath = Path.Combine(dirInfo.FullName, $"conveyor_{fileCounter.ToString()}.json");
                
                using (FileStream fileStream = new FileStream(fullPath, File.Exists(fullPath) ? FileMode.Truncate : FileMode.Create))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine(JsonConvert.SerializeObject(clientsToFile)); 
                        addedToFiles++;
                        fileStreamLength = streamWriter.BaseStream.Length;
                    }
                }
            }

            Assert.True(addedToFiles == countClients);
        }

        [Fact]
        public void TestСonveyorProcessingClients()
        {
            string[] pathToDirectory = ["C:", "1", "2"];
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            Assert.True(dirInfo.Exists);

            FileInfo[] files = dirInfo.GetFiles();
            if(files.Length > 0)
            {
                foreach(FileInfo file in files)
                {
                    List<Client> clients = ExportService.ReadElementsFromJSON<Client>(pathToDirectory, file.Name);
                    Assert.NotEmpty(clients);
                }
            }
        }

        [Fact]
        public void ParallelAddingToAccount()
        {
            Account account = new Account();
            var locker = new Object();
            int completed = 0;

            Random pauseMS = new Random();
            WaitCallback waitCallback = delegate (object? _)
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (locker)
                    {
                        account.Amount += 100;
                    }
                    Thread.Sleep(pauseMS.Next(10, 200));
                }
                completed++;
            };

            ThreadPool.QueueUserWorkItem(waitCallback);
            ThreadPool.QueueUserWorkItem(waitCallback);

            while (completed < 2)
                Thread.Sleep(100);
               
            Assert.True(account.Amount == 2000);
        }

    }
}
