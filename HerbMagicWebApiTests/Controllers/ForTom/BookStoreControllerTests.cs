using Microsoft.VisualStudio.TestTools.UnitTesting;
using HerbMagicWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static HerbMagicWebApi.Models.BookStoreModels;

namespace HerbMagicWebApi.Controllers.Tests
{
    [TestClass()]
    public class BookStoreControllerTests
    {
      

        [TestMethod()]
        public void MainUnitTestTest()
        {
            BookStoreController b = new BookStoreController();
            var q = b.MainUnitTest(5);
            Assert.AreEqual(8, q);
        }
        [TestMethod()]
        public void MainUnitTestLessTest()
        {
            BookStoreController b = new BookStoreController();
            var q2 = b.MainUnitTest(5);
            Assert.AreNotEqual(6, q2);
        }
    }
}