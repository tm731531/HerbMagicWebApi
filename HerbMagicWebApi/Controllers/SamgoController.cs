using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HerbMagicWebApi.Controllers
{
    public class SamgoController : Controller
    {
        // GET: Samgo

        public ActionResult Index()
        {
            return View("Game");
        }

        public ActionResult Game()
        {
            return View();
        }

        public ActionResult Control()
        {
            return View( );
        }
        public ActionResult Modal()
        {
            return View();
        }
        public ActionResult Raider()
        {
            return View();
        }



        // GET: Samgo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Samgo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Samgo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Samgo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Samgo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Samgo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Samgo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
