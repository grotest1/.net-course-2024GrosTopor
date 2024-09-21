using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.App.Services
{
    public static class BankService
    {
        

        
        
        
        public static Employee SetClientAsEmployee(Client client)
        {
            Person person = client;
            return (Employee)person;
        }
        




    }
}
