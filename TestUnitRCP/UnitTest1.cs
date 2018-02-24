using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCP.Models;
using RCP.Controllers;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;


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
            Assert.AreEqual(null, wejscie.Karta );
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
    }
}
