using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace BankSystem.App.Services
{
    public static class TestDataGenerator
    {
        public static List<Client> GenerateClientList(int countElements)
        {
            var faker = new Faker<Client>("ru")
               .RuleFor(p => p.Name, f => f.Name.FirstName())
               .RuleFor(p => p.Age, f => f.Random.Int(1, 120))
               .RuleFor(p => p.PersonalPhoneNumber, f => f.Phone.PhoneNumber("########"));

            return faker.Generate(countElements);
        }

        public static List<Account> GenerateAccountList(int countElements)
        {
            var faker = new Faker<Account>("ru")
               .RuleFor(p => p.Amount, f => f.Random.Int(-1000, 1000))
               .RuleFor(p => p.Currency.Name, f => f.Name.FirstName())
               .RuleFor(p => p.Currency.Code, f => f.Random.Int(100, 600));

            return faker.Generate(countElements);
        }

        public static Dictionary<string, Client> GenerateClientDictionary(int countElements)
        {
            Dictionary<string, Client> resultDictionary = new Dictionary<string, Client>();
            List<Client> randomClients = GenerateClientList(countElements);

            foreach (Client rndClient in randomClients)
            {
                resultDictionary.TryAdd(rndClient.PersonalPhoneNumber, rndClient);
            }

            return resultDictionary;
        }

        public static List<Employee> GenerateEmployeeList(int countElements)
        {
            var faker = new Faker<Employee>("ru")
               .RuleFor(p => p.Name, f => f.Name.FirstName())
               .RuleFor(p => p.Age, f => f.Random.Int(1, 120))
               .RuleFor(p => p.PersonalPhoneNumber, f => f.Phone.PhoneNumber("########"))
               .RuleFor(p => p.Salary, f => f.Random.Int(100, 10000));

            return faker.Generate(countElements);
        }

        public static Dictionary<Client, Account> GenerateSomeDataSimple(int countElements)
        {
            Dictionary<Client, Account> resultDictionary = new Dictionary<Client, Account>();
            List<Client> randomClients = GenerateClientList(countElements);

            Random rnd = new Random();

            Currency currencyRub = new Currency { Name = "Rub", Code = 456 };

            for (int i = 0; i < countElements; i++)
            {
                Account account = new Account { Amount = rnd.Next(10, 100), Currency = currencyRub };
                resultDictionary.TryAdd(randomClients[i], account);
            }

            return resultDictionary;
        }

        public static Dictionary<Client, Account[]> GenerateSomeDataArray(int countElements)
        {
            Dictionary<Client, Account[]> resultDictionary = new Dictionary<Client, Account[]>();
            List<Client> randomClients = GenerateClientList(countElements);

            Random rnd = new Random();

            Currency currencyRub = new Currency { Name = "Rub", Code = 456 };

            for (int i = 0; i < countElements; i++)
            {

                int rndCountAccounts = rnd.Next(1, 5);
                Account[] clientAccounts = new Account[rndCountAccounts];

                for (int j = 0; j < rndCountAccounts; j++)
                    clientAccounts[j] = new Account { Amount = rnd.Next(10, 100), Currency = currencyRub };
 
                resultDictionary.TryAdd(randomClients[i], clientAccounts);
            }

            return resultDictionary;
        }

    }
}
