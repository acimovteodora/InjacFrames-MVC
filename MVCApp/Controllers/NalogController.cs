using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using MVCApp.DataAccessLayer.Interfaces;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class NalogController : Controller
    {
        private readonly INalogLogic _logic;
        public NalogController(INalogLogic logic)
        {
            _logic = logic;
        }
        // GET: NalogController
        public ActionResult Index()
        {
            try
            {
                List<NalogZaUtovar> list = _logic.SelectAll(new NalogZaUtovar());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: NalogController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                NalogZaUtovar objekat = _logic.SelectObject(new NalogZaUtovar() { Id = id });
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: NalogController/Create
        public ActionResult Create()
        {
            try
            {
                List<Porudzbina> porudzbine = _logic.GetPorudzbineInsert();
                List<Prevoznik> prevoznici = _logic.GetPrevoznici();
                List<Carinik> carinici = _logic.GetCarinici();
                ViewBag.Prevoznici = prevoznici;
                ViewBag.Carinici = carinici;
                ViewBag.Porudzbine = porudzbine;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: NalogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NalogZaUtovar objekat)
        {
            try
            {
                _logic.CreateObject(objekat);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: NalogController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                NalogZaUtovar objekat = _logic.SelectObject(new NalogZaUtovar() { Id = id });
                List<Carinik> carinici = _logic.GetCarinici();
                List<Porudzbina> porudzbine = _logic.GetPorudzbineUpdate();
                List<Prevoznik> prevoznici = _logic.GetPrevoznici();
                ViewBag.Porudzbine = new SelectList(porudzbine, "Id", "Id", $"{objekat.Porudzbina.Id}");
                ViewBag.Prevoznici = new SelectList(prevoznici, "Id", "NazivKompanije", $"{objekat.Prevoznik.NazivKompanije}");
                ViewBag.CariniciIzvoz = new SelectList(carinici, "Id", "NazivKompanije", $"{objekat.CarinikIzvoz.NazivKompanije}");
                ViewBag.CariniciUvoz = new SelectList(carinici, "Id", "NazivKompanije", $"{objekat.CarinikIzvoz.NazivKompanije}");
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: NalogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NalogZaUtovar objekat)
        {
            try
            {
                _logic.UpdateObject(objekat);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: NalogController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                NalogZaUtovar objekat = _logic.SelectObject(new NalogZaUtovar() { Id = id });
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: NalogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, NalogZaUtovar objekat)
        {
            try
            {
                _logic.DeleteObject(objekat);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }
    }
}
