using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCP.Models;
using RCP.Controllers;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TestUnitRCP
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //sprawdzamy czy zarejestrowane zdarzenie wejścia/wyjścia ma przypisaną kartę
        public void CheckCardIsNull()
        {
            Wejscia wejscie = new Wejscia();
            wejscie.Tryb = 1;
            wejscie.Typ = 2;
            wejscie.CzasWejscia = DateTime.Now;
            Assert.AreEqual(null, wejscie.Karta);
        }

        [TestMethod]
        //sprawdzamy czy pracownik ma zdefiniowany plan pracy
        public void EmployeeHasAssignmentWorkPlan()
        {
            Pracownicy pracownik = new Pracownicy();
            pracownik.Id = 1;

            List<PlanPracy> plany = new List<PlanPracy>();
            PlanPracy plan = new PlanPracy();
            plan.IdPracownika = 1;
            plany.Add(plan);
            plany.Add(new PlanPracy());
            bool contains = plany.Where(x => x.IdPracownika == pracownik.Id) != null ? true : false;

            Assert.IsFalse(!contains);
        }

        [TestMethod]
        public void CorrectLogin()
        {
            string haslo = "P@ssw0rd";
            Pracownicy pracownik = new Pracownicy();
            pracownik.Pass = "P@ssw0rd";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, pracownik.Pass);
                Assert.IsTrue(VerifyMd5Hash(md5Hash, haslo, hash));
            }
        }

        [TestMethod]        
        public void InCorrectLogin()
        {
            string haslo = "P@ssw0rdxx";
            Pracownicy pracownik = new Pracownicy();
            pracownik.Pass = "P@ssw0rd";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, pracownik.Pass);
                Assert.IsFalse(VerifyMd5Hash(md5Hash, haslo, hash));
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        [TestMethod]
        public void InCorrectLogizzn()
        {
            string a = null;

            if (a.Equals("3"))
            {
                a = "3";
            }

            Assert.AreEqual(a, "3");
           
        }
    }
    }
