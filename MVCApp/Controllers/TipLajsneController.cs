using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCApp.DataAccessLayer.Interfaces;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class TipLajsneController : Controller
    {
        private readonly ITipLajsneLogic _logic;
        public TipLajsneController(ITipLajsneLogic logic)
        {
            _logic = logic;
        }
        // GET: TipLajsneController
        public ActionResult Index()
        {
            try
            {
                List<TipLajsne> list = _logic.SelectAll(new TipLajsne());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: TipLajsneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipLajsneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                TipLajsne obj = new TipLajsne()
                {
                    NazivTipa = collection["NazivTipa"],
                    Dimenzije = new Assembly.Dimenzije()
                    {
                        Duzina = Convert.ToInt32(collection["Dimenzije.Duzina"]),
                        Visina = Convert.ToInt32(collection["Dimenzije.Visina"])
                    }
                };
                _logic.CreateObject(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: TipLajsneController/Edit/5
        public ActionResult Edit(int id)
        {
            TipLajsne obj = _logic.SelectObject(new TipLajsne() { Id = id});
            return View(obj);
        }

        // POST: TipLajsneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                TipLajsne obj = new TipLajsne()
                {
                    Id = (long)Convert.ToDecimal(collection["Id"]),
                    NazivTipa = collection["NazivTipa"],
                    Dimenzije = new Assembly.Dimenzije()
                    {
                        Duzina = Convert.ToInt32(collection["Dimenzije.Duzina"]),
                        Visina = Convert.ToInt32(collection["Dimenzije.Visina"])
                    }
                };
                _logic.UpdateObject(obj);
                return RedirectToAction("Index", "Lajsna");
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

    }
}
