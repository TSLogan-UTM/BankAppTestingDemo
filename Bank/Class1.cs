﻿// Name: Thomas Logan
// Date: 11/27/2019
// File: Class1.cs
// Description: A simple banking app used as an example for building tests.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountNS
{
    /// <summary>  
    /// Bank Account demo class.  
    /// </summary>  
    public class BankAccount
    {
        private string m_customerName;

        private double m_balance;

        private bool m_frozen = false;

        private BankAccount()
        {
        }

        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        public string CustomerName
        {
            get { return m_customerName; }
        }

        public double Balance
        {
            get { return m_balance; }
        }

        // Strings for when DEBIT class is under test.
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";
        public const string DebitAmountLessThanZeroMessage = "Debit amount less than zero";
        public void Debit(double amount)
        {
            if (m_frozen)
            {
                throw new Exception("Account frozen");
            }

            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);  
            }

            m_balance -= amount; //<-- FIXED
        }

        // Strings for when CREDIT class is under test.
        public const string CreditAccountFrozenMessage = "Sorry, your account is frozen.";
        public const string CreditAmountLessThanZeroMessage = "Credit amount is less than zero";
        public void Credit(double amount)
        {
            if (m_frozen)
            {
                throw new ArgumentOutOfRangeException("amount", amount, CreditAccountFrozenMessage);
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, CreditAmountLessThanZeroMessage);
            }

            m_balance += amount;
        }

        private void FreezeAccount()
        {
            m_frozen = true;
        }

        private void UnfreezeAccount()
        {
            m_frozen = false;
        }


        // To use to test the Credit function when the account is frozen.
        public void ToggleFreeze()

        {

            m_frozen = !m_frozen;

        }

        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Bryan Walton", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
        }

    }
}