using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.Logic.Interfaces;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class GradController : Controller
    {
        private readonly IGradLogic _logic;
        public GradController(IGradLogic logic)
        {
            _logic = logic;
        }
        // GET: GradController
        public ActionResult Index()
        {
            try
            {
                List<Grad> cities = _logic.SelectAll(new Grad());
                return View(cities);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: GradController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Grad city = _logic.SelectObject(new Grad() { Id = id });
                List<Drzava> countries = _logic.GetCountries();
                ViewBag.Countries = new SelectList(countries, "Id", "NazivDrzave", $"{city.Drzava.NazivDrzave}");
                return View(city);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: GradController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Grad objekat = new Grad()
                {
                    Id = id,
                    NazivGrada = collection["NazivGrada"],
                    Drzava = new Drzava() { Id = (long)Convert.ToDecimal(collection["Drzava.Id"])},
                    PostanskiBroj = collection["PostanskiBroj"]
                };
                _logic.UpdateObject(objekat);

                //redirect na drugi kontroler, kontroler Adresa, Index action
                return RedirectToAction("Index","Adresa");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }
    }
}
