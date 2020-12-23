using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.DataAccessLayer.Interfaces;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class StavkaProformeController : Controller
    {
        private readonly IStavkaProformeLogic _logic;
        public StavkaProformeController(IStavkaProformeLogic logic)
        {
            _logic = logic;
        }

        // GET: StavkaProformeController/Create
        [Route("StavkaProformeController/create/stavka")]
        public ActionResult Create(int id)
        {
            try
            {
                List<Lajsna> lajsne = _logic.GetLajsne();
                ViewBag.Lajsne = lajsne;
                return View(new StavkaProforme() { Proforma = new Proforma() { Id = id } });
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: StavkaProformeController/Create
        [HttpPost]
        [Route("StavkaProformeController/create/stavka")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StavkaProforme objekat)
        {
            try
            {
                _logic.CreateObject(objekat);
                return RedirectToAction(nameof(Index), "Proforma");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: StavkaProformeController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                StavkaProforme objekat = _logic.SelectObject(new StavkaProforme() { RedniBroj = id });
                List<Lajsna> lajsne = _logic.GetLajsne();
                ViewBag.Lajsne = new SelectList(lajsne, "Id", "NazivLajsne", $"{objekat.Lajsna.NazivLajsne}");
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: StavkaProformeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StavkaProforme objekat)
        {
            try
            {
                _logic.UpdateObject(objekat);
                return RedirectToAction(nameof(Index), "Proforma");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: StavkaProformeController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                StavkaProforme objekat = _logic.SelectObject(new StavkaProforme() { RedniBroj = id });
                _logic.DeleteObject(objekat);
                return RedirectToAction(nameof(Index), "Proforma");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: StavkaProformeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StavkaProforme objekat)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StavkaProformeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StavkaProformeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
