using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.DataAccessLayer.Interfaces;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class AdresaController : Controller
    {
        private readonly IAdresaLogic _logic;
        public AdresaController(IAdresaLogic logic)
        {
            _logic = logic;
        }
        // GET: AdresaController
        public ActionResult Index()
        {
            try
            {
                List<Adresa> list = _logic.SelectAll(new Adresa());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            };
        }

        // GET: AdresaController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Adresa adresa = _logic.SelectObject(new Adresa() { SifraAdrese = id });
                return View(adresa);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            };
        }

        // GET: AdresaController/Create
        public ActionResult Create()
        {
            try
            {
                List<Grad> cities = _logic.GetCities();
                List<Kompanija> companies = _logic.GetCompanies();
                ViewBag.Cities = cities;
                ViewBag.Companies = companies;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            };
        }

        // POST: AdresaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Adresa obj = new Adresa()
                {
                    Naziv = collection["Naziv"],
                    Broj = collection["Broj"],
                    NazivGrada = collection["NazivGrada"],
                    Grad = new Grad()
                    {
                        NazivGrada = collection["Grad.NazivGrada"]
                    },
                    Kompanija = new Kompanija()
                    {
                        NazivKompanije = collection["Kompanija.NazivKompanije"]
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

        // GET: AdresaController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Adresa adresa = _logic.SelectObject(new Adresa() { SifraAdrese = id });
                List<Grad> cities = _logic.GetCities();
                List<Kompanija> companies = _logic.GetCompanies();
                ViewBag.Cities = new SelectList(cities, "Id", "NazivGrada", $"{adresa.Grad.Id}");
                ViewBag.Companies = new SelectList(companies, "Id", "NazivKompanije", $"{adresa.Kompanija.Id}");
                return View(adresa);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            };
        }

        // POST: AdresaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Adresa obj = new Adresa()
                {
                    SifraAdrese = id,
                    Naziv = collection["Naziv"],
                    Broj = collection["Broj"],
                    NazivGrada = collection["NazivGrada"],
                    Grad = new Grad()
                    {
                        Id = (long)Convert.ToDecimal(collection["Grad.Id"])
                    },
                    Kompanija = new Kompanija()
                    {
                        Id = (long)Convert.ToDecimal(collection["Kompanija.Id"])
                    }
                };
                _logic.UpdateObject(obj);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }
    }
}
