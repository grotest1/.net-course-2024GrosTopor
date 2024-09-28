﻿using Xunit;
using BancSystem.App;
using BankSystem.Domain.Models;
using BankSystem.App.Services;


namespace BancSystem.App.Tests
{
    public class EquivalenceTests
    {

        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            //Arrange
            Dictionary<Client, Account> testData = TestDataGenerator.GenerateSomeData_Simple(10);
            KeyValuePair<Client, Account> firstElement = testData.FirstOrDefault();

            Client clientFromTestData = firstElement.Key;
            Client newClient = new Client
            {
                Name = clientFromTestData.Name,
                Birthday = clientFromTestData.Birthday,
                PersonalPhoneNumber = clientFromTestData.PersonalPhoneNumber
            };

            //Act
            Account accountResult = testData[newClient];

            //Assert
            Assert.Equal(firstElement.Value, accountResult);
        }

        [Fact]
        public void GetHashCodeNecessityPositivTest_Array()
        {
            //Arrange
            Dictionary<Client, Account[]> testData = TestDataGenerator.GenerateSomeData_Array(10);
            KeyValuePair<Client, Account[]> firstElement = testData.FirstOrDefault();

            Client clientFromTestData = firstElement.Key;
            Client newClient = new Client
            {
                Name = clientFromTestData.Name,
                Birthday = clientFromTestData.Birthday,
                PersonalPhoneNumber = clientFromTestData.PersonalPhoneNumber
            };

            //Act
            Account[] accountResult = testData[newClient];

            //Assert
            Assert.Equal(firstElement.Value, accountResult);
        }

    }
}
