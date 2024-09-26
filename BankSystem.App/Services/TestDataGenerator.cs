﻿using BankSystem.Domain.Models;
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
        public static List<Client> GenerateClientList1000()
        {
            List<Client> resultList = new List<Client>();

            Random rnd = new Random();

            for (int i = 0; i < 1000; i++)
            {
                SetRandomePersonData(rnd, out string name, out DateOnly birthday, out string phoneNumber, out int salary);
                resultList.Add(new Client() { Name = name, Birthday = birthday, PersonalPhoneNumber = phoneNumber });
            }

            return resultList;
        }

        public static Dictionary<string, Client> GenerateClientDictionary1000()
        {
            Dictionary<string, Client> resultDictionary = new Dictionary<string, Client>();

            Random rnd = new Random();

            for (int i = 0; i < 1000; i++)
            {
                SetRandomePersonData(rnd, out string name, out DateOnly birthday, out string phoneNumber, out int salary);
                Client client = new Client() { Name = name, Birthday = birthday, PersonalPhoneNumber = phoneNumber };
                resultDictionary.TryAdd(phoneNumber, client);
            }

            return resultDictionary;
        }

        public static List<Employee> GenerateEmployeeList1000()
        {
            List<Employee> result_list = new List<Employee>();

            Random rnd = new Random();

            for (int i = 0; i < 1000; i++)
            {
                SetRandomePersonData(rnd, out string name, out DateOnly birthday, out string phoneNumber, out int salary);
                result_list.Add(new Employee() { Name = name, Birthday = birthday, PersonalPhoneNumber = phoneNumber, Salary = salary });
            }

            return result_list;
        }

        private static void SetRandomePersonData(Random rnd, out string name, out DateOnly birthday, out string phoneNumber, out int salary)
        {
            string[] names = {"Вася", "Петя", "Дима", "Сергей", "Костя", "Никита", "Саша", "Жора", "Максим"};
            name = names[rnd.Next(0, names.Length - 1)];

            int yearNow = DateTime.Now.Year;
            birthday = new DateOnly(rnd.Next(yearNow - 100, yearNow - 1), rnd.Next(1, 12), rnd.Next(1, 28));

            string[] phoneCodes = {"775", "777", "778", "779", "67", "68", "69"};
            phoneNumber = phoneCodes[rnd.Next(0, phoneCodes.Length - 1)];
            while (phoneNumber.Length < 8)
                phoneNumber += rnd.Next(1, 9).ToString();

            salary = rnd.Next(100, 10_000);
        }

        public static List<Client> GenerateClientList1000Bogus()
        {
            var faker = new Faker<Client>("ru")
               .RuleFor(p => p.Name, f => f.Name.FirstName())
               .RuleFor(p => p.Birthday, f => f.Date.BetweenDateOnly(new DateOnly(1950, 1, 1), new DateOnly(2020, 1, 1)))
               .RuleFor(p => p.PersonalPhoneNumber, f => f.Phone.PhoneNumber("########"));

            return faker.Generate(1000);
        }

        public static Dictionary<string, Client> GenerateClientDictionary1000Bogus()
        {
            Dictionary<string, Client> resultDictionary = new Dictionary<string, Client>();
            List<Client> randomClients = GenerateClientList1000Bogus();

            foreach (Client rndClient in randomClients)
            {
                resultDictionary.TryAdd(rndClient.PersonalPhoneNumber, rndClient);
            }

            return resultDictionary;
        }

        public static List<Employee> GenerateEmployeeList1000Bogus()
        {
            var faker = new Faker<Employee>("ru")
               .RuleFor(p => p.Name, f => f.Name.FirstName())
               .RuleFor(p => p.Birthday, f => f.Date.BetweenDateOnly(new DateOnly(1950, 1, 1), new DateOnly(2020, 1, 1)))
               .RuleFor(p => p.PersonalPhoneNumber, f => f.Phone.PhoneNumber("########"))
               .RuleFor(p => p.Salary, f => f.Random.Int(100, 10000));

            return faker.Generate(1000);
        }
    }
}