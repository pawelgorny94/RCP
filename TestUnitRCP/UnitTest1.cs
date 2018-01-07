using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RCP.Models;
using RCP.Controllers;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace TestUnitRCP
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            WorkPlanController workController = new WorkPlanController();
            string result = ((ViewResult)workController.Create()).ViewName;
            Assert.AreEqual("Create",result );
        }
    }
}
