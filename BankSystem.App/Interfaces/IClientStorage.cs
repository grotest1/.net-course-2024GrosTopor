using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public interface IClientStorage : IStorage<Client>
    {

        public void Add() { }
        public void Update() { }
        public void Delete() { }
        
        public void AddAccount() { }
        public void UpdateAccount() { }
        public void DeleteAccount() { }

        Dictionary<Client, List<Account>> Data { get; }
    }
}
