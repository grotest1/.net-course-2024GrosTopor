using BankSystem.Domain.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.Xml.Linq;

namespace ExportTool
{
    public static class ExportService
    {

        public static void WriteElementsToCsv<TElement>(List<TElement> items, string[] pathToDirectory, string fileName, char delimiter = ';') where TElement : class
        {

            if (pathToDirectory.Length == 0)
                throw new FormatException("Некорректный путь директории.");

            if (!fileName.EndsWith(".svg"))
                throw new FormatException("Некорректный формат файла. Требуется '.svg'");

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(dirInfo.Name, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = delimiter.ToString()
                    };

                    using (var writer = new CsvWriter(streamWriter, config))
                    {
                        writer.WriteHeader<TElement>();
                        writer.NextRecord();

                        foreach (TElement item in items)
                        {
                            writer.WriteRecord<TElement>(item);
                        }

                        writer.Flush();
                    }
                }
            }
        }

        public static List<TElement> ReadElementsFromCsv<TElement>(string[] pathToDirectory, string fileName)
        {

            if (pathToDirectory.Length == 0)
                throw new FormatException("Некорректный путь директории.");

            if (!fileName.EndsWith(".svg"))
                throw new FormatException("Некорректный формат файла. Требуется '.svg'");


            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            if (!dirInfo.Exists)
                throw new DirectoryNotFoundException(dirInfo.Name);

            string fullPath = Path.Combine(dirInfo.Name, fileName);
            
            //if (!File.Exists(fullPath))
            //    throw new FileNotFoundException(fileName);

            List<TElement> items = new List<TElement>();

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        while (reader.Read())
                        { 
                            items.Add(reader.GetRecord<TElement>());
                        }
                    }
                }
            }
            return items;
        }
    }
}