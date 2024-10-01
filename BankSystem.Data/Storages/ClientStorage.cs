﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        public readonly List<Client> collection = new List<Client>();

        public void Add(Client client)
        {
            collection.Add(client);
        }
    }
}
