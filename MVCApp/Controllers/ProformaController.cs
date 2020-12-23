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
    public class ProformaController : Controller
    {
        private readonly IProformaLogic _logic;
        public ProformaController(IProformaLogic logic)
        {
            _logic = logic;
        }
        // GET: ProformaController
        public ActionResult Index()
        {
            try
            {
                List<Proforma> list = _logic.SelectAll(new Proforma());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: ProformaController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Proforma objekat = _logic.SelectObject(new Proforma() { Id = id });
                List<NacinIsporuke> isporuke = _logic.GetNaciniIsporuke();
                List<Racun> racuni = _logic.GetRacuni();
                List<Lajsna> lajsne = _logic.GetLajsne();
                ViewBag.Isporuke = new SelectList(isporuke, "Id", "NazivIsporuke", $"{objekat.NacinIsporuke.NazivIsporuke}");
                ViewBag.Racuni = new SelectList(racuni, "Id", "Vrednost", $"{objekat.Racun.Vrednost}");
                ViewBag.Lajsne = lajsne;
                return View(objekat);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: ProformaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proforma objekat)
        {
            try
            {
                _logic.UpdateObject(objekat);
                return RedirectToAction(nameof(Index),"Proforma");
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: ProformaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProformaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Proforma objekat)
        {
            try
            {
                return RedirectToAction(nameof(Index), "Proforma");
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: ProformaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProformaController/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: ProformaController/Create
        public ActionResult CreateRacun()
        {
            try
            {
                List<Banka> banke = _logic.GetBanke();
                ViewBag.Banks = banke;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: ProformaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRacun(Racun racun)
        {
            try
            {
                _logic.CreateObject(racun);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }
    }
}
