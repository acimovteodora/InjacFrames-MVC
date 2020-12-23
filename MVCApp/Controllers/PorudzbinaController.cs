using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCApp.DataAccessLayer.Interfaces;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class PorudzbinaController : Controller
    {
        private readonly IPorudzbinaLogic _logic;
        public PorudzbinaController(IPorudzbinaLogic logic)
        {
            _logic = logic;
        }
        // GET: PorudzbinaController
        public ActionResult Index()
        {
            try
            {
                List<Porudzbina> list = _logic.SelectAll(new Porudzbina());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }
        [Route("PorudzbinaController/uslovi")]
        public ActionResult IndexUslovi()
        {
            try
            {
                List<Porudzbina> list = _logic.SelectByCriteria("WHERE UsloviIsporuke IS NOT NULL AND UsloviIsporuke NOT LIKE ''", new Porudzbina());
                return View(list);
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: PorudzbinaController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: PorudzbinaController/Create
        public ActionResult Create()
        {
            try
            {
                List<NacinIsporuke> isporuke = _logic.GetNaciniIsporuke();
                List<NacinPlacanja> placanja = _logic.GetNaciniPlacanja();
                List<Klijent> klijenti = _logic.GetKlijenti();
                List<Lajsna> lajsne = _logic.GetLajsne();
                ViewBag.Placanja = placanja;
                ViewBag.Klijenti = klijenti;
                ViewBag.Isporuke = isporuke;
                ViewBag.Lajsne = lajsne;
                return View(new Porudzbina());
            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // POST: PorudzbinaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Porudzbina porudzbina)
        {
            try
            {
                if (!string.IsNullOrEmpty(porudzbina.Button))
                {
                    if (ModelState.IsValid)
                    {
                        if (porudzbina.Stavke == null)
                            porudzbina.Stavke = new List<StavkaPorudzbine>();
                        porudzbina.NovaStavka.Lajsna = _logic.GetLajsna(porudzbina.NovaStavka.Lajsna.NazivLajsne);
                        List<NacinIsporuke> isporuke = _logic.GetNaciniIsporuke();
                        List<NacinPlacanja> placanja = _logic.GetNaciniPlacanja();
                        List<Klijent> klijenti = _logic.GetKlijenti();
                        List<Lajsna> lajsne = _logic.GetLajsne();
                        ViewBag.Placanja = placanja;
                        ViewBag.Klijenti = klijenti;
                        ViewBag.Isporuke = isporuke;
                        ViewBag.Lajsne = lajsne;
                        porudzbina.Stavke.Add(porudzbina.NovaStavka);
                        porudzbina.NovaStavka = new StavkaPorudzbine();
                        return View(porudzbina);
                    }
                }
                _logic.CreateObject(porudzbina);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        private DateTime CreateDate(string stringValues)
        {
            string[] split = stringValues.Split('T');
            string date = string.Concat(split[0], " ", split[1]);
            DateTime dateTime;
            DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            return dateTime;

        }


        // GET: PorudzbinaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PorudzbinaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PorudzbinaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PorudzbinaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
