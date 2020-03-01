using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HerbMagicWebApi.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using static HerbMagicWebApi.Models.BookStoreModels;

namespace HerbMagicWebApi.Tests.Controllers
{
    [TestClass]
    public class HistoryControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // 排列
            HistoryController controller = new HistoryController();

            // 作用
            HttpResponseMessage result = controller.Get();

            MainHistory value = new MainHistory();
            // 判斷提示
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.TryGetContentValue<MainHistory>(out value));
        }

        [TestMethod]
        public void GetById()
        {
            // 排列
            HistoryController controller = new HistoryController();

            // 作用
            HttpResponseMessage result = controller.Get("aa");

            // 判斷提示
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // 排列
            HistoryController controller = new HistoryController();

            MainHistory value = new MainHistory();
            // 作用
            HttpResponseMessage result = controller.Post(value);

            // 判斷提示
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Put()
        {
            // 排列
            HistoryController controller = new HistoryController();


            MainHistory value = new MainHistory();
            // 作用
            HttpResponseMessage result = controller.Put(value);

            // 判斷提示
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Delete()
        {
            // 排列
            HistoryController controller = new HistoryController();

            MainHistory value = new MainHistory();
            // 作用
            HttpResponseMessage result = controller.Delete(value);

            // 判斷提示
            Assert.AreEqual(15, result);
        }
    }
}
