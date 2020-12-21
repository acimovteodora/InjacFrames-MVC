﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.DataAccessLayer;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class LajsnaController : Controller
    {
        private ILajsnaLogic _lajsnaLogic;
        public LajsnaController(ILajsnaLogic lajsnaLogic)
        {
            _lajsnaLogic = lajsnaLogic;
        }
        // GET: LajsnaController
        public ActionResult Index(string searchString)
        {
            List<Lajsna> list = new List<Lajsna>();
            if (!string.IsNullOrEmpty(searchString))
                list = _lajsnaLogic.ReturnByCriteria($"WHERE NazivLajsne LIKE '%{searchString}%' OR NazivTipa LIKE '%{searchString}%'", new Lajsna());
            else list = _lajsnaLogic.ReturnAll(new Lajsna());
            return View(list);
        }

        // GET: LajsnaController/Details/5
        public ActionResult Details(int id)
        {
            Lajsna lajsna = _lajsnaLogic.ReturnObject(new Lajsna() { Id = id });
            return View(lajsna);
        }

        // GET: LajsnaController/Create
        public ActionResult Create()
        {
            List<TipLajsne> types = _lajsnaLogic.GetTipoviLajsni();
            ViewBag.Types = types;
            return View();
        }

        // POST: LajsnaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LajsnaController/Edit/5
        public ActionResult Edit(int id)
        {
            List<TipLajsne> types = _lajsnaLogic.GetTipoviLajsni();
            //ViewBag.Types = types;
            Lajsna lajsna = _lajsnaLogic.ReturnObject(new Lajsna() { Id = id });
            ViewBag.Types = new SelectList(types, "Id", "Id", $"{lajsna.TipLajsne.Id}");
            return View(lajsna);
        }

        // POST: LajsnaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Lajsna lajsna)
        {
            try
            {
                _lajsnaLogic.UpdateObject(lajsna);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View("Error", new Error() { Greska = ex.Message });
            }
        }

        // GET: LajsnaController/Delete/5
        public ActionResult Delete(int id)
        {
            Lajsna lajsna = _lajsnaLogic.ReturnObject(new Lajsna() { Id = id });
            return View(lajsna);
        }

        // POST: LajsnaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Lajsna lajsna)
        {
            try
            {
                Debug.WriteLine("Usao u zimenu");
                if (_lajsnaLogic.DeleteObject(lajsna) == 1)
                    return RedirectToAction(nameof(Index));
                throw new Exception();
            }
            catch
            {
                return View();
            }
        }
    }
}
