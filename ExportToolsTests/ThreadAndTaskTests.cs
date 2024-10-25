using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ExportTool;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using Newtonsoft.Json;
using System.IO;

namespace ExportToolsTests
{
    public class ThreadAndTaskTests
    {
        [Fact]
        public void СonveyorProcessingClients()
        {
            List<Client> clients1 = TestDataGenerator.GenerateClientList(500);
            List<Client> clients2 = TestDataGenerator.GenerateClientList(500);
            List<Client> clients3 = TestDataGenerator.GenerateClientList(500);
            int countClients = clients1.Count + clients2.Count + clients3.Count;

            Queue<Client> conveyor = [];

            var lockerСonveyor = new object();

            int queue = 0;

            Random pauseMS = new Random();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int index1 = 0; index1 < clients1.Count; index1++) {
                    lock (lockerСonveyor)
                    {
                        conveyor.Enqueue(clients1[index1]);
                        queue++;
                    }
                    Thread.Sleep(pauseMS.Next(100, 500));
                }

            });            
            
            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int index2 = 0; index2 < clients2.Count; index2++) {
                    lock (lockerСonveyor)
                    {
                        conveyor.Enqueue(clients2[index2]);
                        queue++;
                    }
                    Thread.Sleep(pauseMS.Next(100, 500));
                }

            });            
            
            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int index3 = 0; index3 < clients3.Count; index3++) {
                    lock (lockerСonveyor)
                    {
                        conveyor.Enqueue(clients3[index3]);
                        queue++;
                    }
                    Thread.Sleep(pauseMS.Next(100, 500));
                }

            });



            string[] pathToDirectory = ["C:", "1", "2"];
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

             

            ThreadPool.QueueUserWorkItem(_ =>
            {
                int fileCounter = 0;
                long fileStreamLength = 0;

                while (queue < countClients)
                {
               
                    while (conveyor.Count == 0)
                        Thread.Sleep(200);

                    Client clientToFile;
                    lock (lockerСonveyor)
                    {
                        clientToFile = conveyor.Dequeue();
                    }

                    if (fileStreamLength > 100)
                        fileCounter++;

                    string fullPath = Path.Combine(dirInfo.FullName, $"conveyor_{fileCounter.ToString()}.json");

                    using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.Write(JsonConvert.SerializeObject(clientToFile));
                        }

                        fileStreamLength = fileStream.Length;
                    }
                
                    Thread.Sleep(800);
                }
            });            
        }
    }
}
