﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth
{
    interface IOperations
    {
        void TransferMoney();
        void WithdrawMoney();
    }
    enum Day
    {
        Monday = 1, Thuesday, Wensday
    }
    struct Arr
    {
        private string inter;
    }
    abstract class Person
    {
        private string FirstName;
        private string Surname;
        private string Address;
        public Person(string name, string surname, string address)
        {
            FirstName = name;
            Surname = surname;
            Address = address;
        }
        public override string ToString()
        {
            return String.Format("Name - " + FirstName + " " + Surname + "\n" + "Address - " + Address + "\n");
        }
        public abstract bool Equals(Client obj);
    }
     partial class Client : Person
    {
       
    }
        abstract class Account : IOperations
    {
        private double sum;
        private string AccountNumber;
        public Account(string accountNumber, double Summa)
        {
            AccountNumber = accountNumber;
            sum =+ Summa;
        }
        public double Sum
        {
            get
            {
                return sum;
            }
        }
        public virtual void BlockAccount() { }
        public abstract void GetInformation();
        void IOperations.TransferMoney() { }
        void IOperations.WithdrawMoney() { }
        
        public override string ToString()
        {
            return String.Format("Номер счета: " + AccountNumber + "\n");
        }

    }
         sealed class CurrentAccount : Account //текущий
    {
        public bool AreBlocked = false;
        private double CurrentSum;
        public CurrentAccount(string accountNumber, double currentSum)
            : base(accountNumber, currentSum)
        {
            CurrentSum = currentSum;
        }
        public override void GetInformation()
        {
            Console.WriteLine($"На этом счете {CurrentSum} белорусских рублей");
        }
        public override void BlockAccount()
        {
            AreBlocked = true;
        }
    }
         sealed class DepositAccount : Account
    {
        public bool AreBlocked = false;
        private double Procent;
        public override void GetInformation()
        {
            Console.WriteLine($"Процент от вашей суммы на дебетовом счете состовляет {Procent}");
        }
        public DepositAccount(string accountNumber, double procent)
            : base(accountNumber, procent)
        {
            Procent = procent;
        }
        public override void BlockAccount()
        {
            AreBlocked = true;
        }
    }
         sealed class CurrencyAccount : Account //валютный
    {
        public bool AreBlocked = false;
        private string Currency; //валюта
        private double CurSum;
        public override void GetInformation()
        {
            Console.WriteLine($"На вашем валютном счете {Sum} {Currency}");
        }
        public CurrencyAccount(string accountNumber, string currency, double sum)
            : base(accountNumber, sum)
        {
            Currency = currency;
            CurSum = sum;
        }
        public override void BlockAccount()
        {
            AreBlocked = true;
        }
    }
    class Bank
    {
        private int numbersOfClients = 0;
        List<Client> clients = new List<Client>();
        public Bank()
        {
            
        }
        public int NumberOfClients
        {
            get
            {
                return numbersOfClients;
            }
        }
        public List<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                if(value.GetType() == typeof(Client))
                {
                    clients = value;
                }
            }
        }
        public Client this[int i]
        {
            get
            {
                if (i >= 0 && i < numbersOfClients)
                {
                    return clients[i];
                }
                else
                {
                    Console.WriteLine("Incorrect index");
                    return null;
                }
            }
            set
            {
                if (i >= 0 && i < numbersOfClients)
                {
                    clients[i] = value;
                }
                else
                {
                    Console.WriteLine("Incorrect index");
                }
            }
        }
        public void AddClient(Client client)
        {
            clients.Add(client);
            numbersOfClients++;

        }
        public void RemoveClient(Client client)
        {
            clients.Remove(client);
        }
        public void PrintClient()
        {
            Console.WriteLine("                   Список");
            for (int i = 0; i < clients.LongCount(); i++) {
                Console.WriteLine($"   {i + 1}.");
                Console.WriteLine($"{clients[i].ToString()}");
                    }
        }
    }
    class Controller
    {
        Bank Bank;
        public Controller(Bank bank)
        {
            Bank = bank;
        }
        public double GeneralSum(Client client)
        {
            Account[] accounts = client.Accounts;
            List<Client> clientiki = Bank.Clients;
            double generalSum = 0;
            for (int i = 0; i < Bank.NumberOfClients; i++)
            {
                if (clientiki[i].Equals(client))
                {
                    for (int j = 0; j < client.CounterAccount; j++)
                    {
                        generalSum += accounts[j].Sum;
                    }
                }
            }
            Console.WriteLine($"Общая сумма 1го клиента  = {generalSum}");
            return generalSum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client1 = new Client("Masha", "Ivanova", "Minsk str.Vaneeva h.130", "3432tfret534", "34342rfeer3454g");
            Client client2 = new Client("Alexander", "Petrov", "minsk pr.Rokossovskogo h.132", "45tfdrgdfg45", "454gtr565hu");

            CurrencyAccount currencyAccount1 = new CurrencyAccount("BY33frse3423fr283", "EUR", 200);
            DepositAccount depositAccount1 = new DepositAccount("gfdgrr4fer2fre6g", 100);
            client1.GetAccount(currencyAccount1);
            client1.GetAccount(depositAccount1);

            CurrentAccount currentAccount2 = new CurrentAccount("BY32gf43tgr5", 900);
            CurrencyAccount currencyAccount2 = new CurrencyAccount("BY33fr343423fr283", "BYN", 1200);
            client2.GetAccount(currentAccount2);
            client2.GetAccount(currencyAccount2);

            Bank Belarusbank = new Bank();
            Belarusbank.AddClient(client1);
            Belarusbank.AddClient(client2);
            Belarusbank.PrintClient();

            Controller nom1 = new Controller(Belarusbank);
            nom1.GeneralSum(client1);

            Console.ReadKey();
        }
    }
}
