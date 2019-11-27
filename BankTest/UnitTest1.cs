// Name: Thomas Logan
// Date: 11/27/2019
// File: UnitTest1.cs
// Description: The test file for Class1.cs that checks all of its functions are working correctly.

/* IMPORTANT NOTES -
 * - To create a test file go to: File -> Add -> New Project -> Add -> Unit Test Project (C# .NET) -> Name it (AKA: UnitTest1).
 * - To link the test file to its code file: Right click UnitTest1 -> Add -> Reference -> Projects -> Solution -> code file name.
 * - You must add the namespace of the code file to the test file. (AKA: "using codefile.NS;" must be at the top of UnitTest1).
 * - All test functions you want to run from test suite must have [TestMethod] on the line right above them, must be void, and
 *   can't take any parameters.
 * - To build a test file: Build -> Build Solution.
 * - To run a test file: Test -> Windows -> Test Explorer -> Run All (or select individual tests).
*/ 

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTest
{
    [TestClass]
    public class UnitTest1
    {
        /***************BEGIN DEBIT TESTS*******************/

        // Test the Debit function.
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;     //<--- If beginningBalance - debitAmount doesn't equal this, test should fail.
            BankAccount account = new BankAccount("Mr. Thomas Jefferson", beginningBalance);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        // Test to see if the Debit function throws an exception if the amount is less than 0. 
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = -10.00;         //<--- If this is greater than 0 test should fail.
            BankAccount account = new BankAccount("Mr. George Washington", beginningBalance);

            try
            {
                // act 
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        // Test to see if the Debit function throws an exception if the amount is MORE than the balance. 
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 12.00;     //<--- If this is less than beginningBalance test should fail.
            BankAccount account = new BankAccount("Mr. Theodore Roosevelt", beginningBalance);

            try
            {
                // act 
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        /*****************END DEBIT TESTS*******************/



        /***************BEGIN CREDIT TESTS*******************/

        // Test the Credit function.
        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 6.01;
            double creditAmount = 10.28;
            double expected = 16.29;    //<--- If beginningBalance + creditAmount doesn't equal this, test should fail.
            BankAccount account = new BankAccount("Mr. Ulysses S. Grant", beginningBalance);

            // act  
            account.Credit(creditAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        // Test to see if the Credit function throws an exception if the account is frozen.
        [TestMethod]
        public void Credit_WhenAccountIsFrozen_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 10.28;
            double creditAmount = 6.01;
            BankAccount account = new BankAccount("Mr. Grover Cleveland", beginningBalance);

            // Accounts are created as unfrozen. This will make the above account frozen.
            account.ToggleFreeze();     //<---- If this is commented out test should fail.

            try
            {
                // act 
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.CreditAccountFrozenMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        // Test to see if the Credit function throws an exception if the amount is less than 0. 
        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 10.28;
            double creditAmount = -10.00;         //<--- If this is more than 0 test should fail.
            BankAccount account = new BankAccount("Mr. James Monroe", beginningBalance);

            try
            {
                // act 
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert  
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        /*****************END CREDIT TESTS*********************/
    }
}
