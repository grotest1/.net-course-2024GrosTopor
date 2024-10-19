using BankSystem.Domain.Models;
using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;

namespace ExportTool
{
    public static class ExportService
    {

        public static void WriteClientsToCsv(List<Client> persons, string[] paths)
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
                        writer.WriteField(nameof(Person.Name));
                        writer.WriteField(nameof(Person.Age));

                        writer.NextRecord();

                        foreach (Person person in persons)
                        {
                            writer.WriteField(person.Name);
                            writer.WriteField(person.Age);
                            writer.NextRecord();
                        }

                        writer.Flush();
                    }
                }
            }
        }

    }
}