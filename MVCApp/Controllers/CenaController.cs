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
    public class CenaController : Controller
    {
        private readonly ICenaLogic _logic;
        public CenaController(ICenaLogic logic)
        {
            _logic = logic;
        }
        // GET: CenaController
        public ActionResult Index()
        {
            try
            {
                List<Cena> list = _logic.SelectAll(new Cena());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: CenaController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Cena objekat = _logic.SelectObject(new Cena() { Lajsna = new Lajsna() { Id = id } });
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: CenaController/Create
        public ActionResult Create(int id)
        {
            try
            {
                Cena objekat = new Cena() { Lajsna = new Lajsna() { Id = id } };
                List<Lajsna> lajsne = _logic.GetLajsne();
                ViewBag.Lajsne = new SelectList(lajsne, "Id", "NazivLajsne", $"{objekat.Lajsna.NazivLajsne}");
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: CenaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cena cena)
        {
            try
            {
                cena.DatumOd = DateTime.Now;
                _logic.CreateObject(cena);
                return RedirectToAction(nameof(Index),"Lajsna");
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: CenaController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Cena objekat = _logic.SelectObject(new Cena() { Lajsna = new Lajsna() { Id = id } });
                List<Lajsna> lajsne = _logic.GetLajsne();
                ViewBag.Lajsne = new SelectList(lajsne, "Id", "NazivLajsne", $"{objekat.Lajsna.NazivLajsne}");
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: CenaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cena cena)
        {
            try
            {
                _logic.UpdateObject(cena);
                return RedirectToAction(nameof(Index),"Lajsna");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: CenaController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _logic.DeleteObject(new Cena() { Lajsna = new Lajsna() { Id = id } });
                return RedirectToAction(nameof(Index), "Lajsna");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: CenaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Cena cena)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }
    }
}
