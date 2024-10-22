using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Globalization;


namespace ExportTool
{
    public static class ExportService
    {
        public static void WriteElementsToCsv<TElement>(List<TElement> items, string[] pathToDirectory, string fileName, char delimiter = ';') where TElement : class
        {
            BasicVerification(pathToDirectory, fileName, new string[] { "csv" });

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(dirInfo.FullName, fileName);

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
                            writer.NextRecord();
                        }

                        writer.Flush();
                    }
                }
            }
        }

        public static List<TElement> ReadElementsFromCsv<TElement>(string[] pathToDirectory, string fileName, char delimiter = ';') where TElement : class
        {
            BasicVerification(pathToDirectory, fileName, new string[] { "csv" });

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));

            string fullPath = Path.Combine(dirInfo.FullName, fileName);
            
            List<TElement> items = new List<TElement>();

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = delimiter.ToString()
                    };

                    using (var reader = new CsvReader(streamReader, config))
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


        public static void WriteElementsToJSON<TElement>(List<TElement> items, string[] pathToDirectory, string fileName) where TElement : class
        {
            BasicVerification(pathToDirectory, fileName, new string[] { "txt", "json" });

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(dirInfo.FullName, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(items));
                }
            }
        }


        public static List<TElement> ReadElementsFromJSON<TElement>(string[] pathToDirectory, string fileName) where TElement : class
        {
            BasicVerification(pathToDirectory, fileName, new string[] { "txt", "json" });

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(pathToDirectory));

            string fullPath = Path.Combine(dirInfo.FullName, fileName);

            List<TElement> items = new List<TElement>();

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string jsonStatham = streamReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(jsonStatham))
                        items = JsonConvert.DeserializeObject<List<TElement>>(jsonStatham);
                }
            }
            return items;
        }


        private static void BasicVerification(string[] pathToDirectory, string fileName, string[] extensions)
        {
            if (pathToDirectory.Length == 0)
                throw new FormatException("Некорректный путь директории.");


            bool goodExtention = false;
            string extentionString = "";
            foreach (string extention in extensions)
            {
                if (fileName.EndsWith($".{extention}"))
                { 
                    goodExtention = true;
                    break;
                }
                else
                    extentionString = (extentionString == "" ? "." : " или .") + extention;
            }
            if (!goodExtention) 
                throw new FormatException($"Некорректный формат файла. Требуется {extentionString}");
        }

    }
}