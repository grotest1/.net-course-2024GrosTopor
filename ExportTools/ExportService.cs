using BankSystem.Domain.Models;
using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;

namespace ExportTool
{
    public static class ExportService
    {

        public static void WriteClientsToCsv(List<Client> clients, string[] paths)
        {

            if (paths.Length == 0)
                return;

            string[] dirPath = new string[paths.Length - 1];
            for (int i = 0; i < paths.Length - 1; i++)
            {
                dirPath[i] = paths[i];
            }

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(dirPath));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }


            string fullPath = Path.Combine(paths);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    using (var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        writer.WriteHeader<Client>();
                        writer.NextRecord();

                        foreach (Client client in clients)
                        {
                            writer.WriteRecord<Client>(client);
                        }

                        writer.Flush();
                    }
                }
            }
        }

        public static List<Client> ReadClientsFromCsv(string[] paths)
        {
            
            List<Client> clients = new List<Client>();
            
            string fullPath = Path.Combine(paths);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        while (reader.Read())
                        { 
                            clients.Add(reader.GetRecord<Client>());
                        }
                    }
                }
            }
            return clients;
        }
    }
}