using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class PartController : Controller
    {
        private IMonsterManager _monsterManager = null;

        public PartController()
        {
            _monsterManager = new MonsterManager();
        }

        public PartController(IMonsterManager monsterManager)
        {
            _monsterManager = monsterManager;
        }

        // GET: Part
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Index()
        {
            List<PartVM> parts = new List<PartVM>();
            try
            {
                parts = _monsterManager.RetreiveParts();
                String[] icons = new string[parts.Count];
                int i = 0;
                foreach (PartVM m in parts)
                {
                    string name = _monsterManager.MonsterImageName(m.MonsterName);
                    icons[i] = "/Content/Images/" + name + "Icon.jpg";
                    i++;
                }
                ViewBag.monsterIcons = icons;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            return View(parts);
        }

        // GET: Part/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            PartVM part = new PartVM();
            try
            {
                ViewBag.Monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }
            return View(part);
        }

        // POST: Part/Create
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(FormCollection collection)
        {
            Part part = new Part();
            List<Part> monsterparts = new List<Part>();
            try
            {
                ViewBag.Monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                int monsterid = int.Parse(collection.Get(2));
                monsterparts = _monsterManager.RetreivePartsByMonsterID(monsterid);
                foreach (var p in monsterparts)
                {
                    if(p.PartName == collection.Get(1))
                    {
                        ModelState.AddModelError("PartName", "Part " + p.PartName + " already exists for this monster.");
                        break;
                    }
                }
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    part.PartName = collection.Get(1);
                    part.MonsterID = monsterid;
                    part.Fire = int.Parse(collection.Get(3));
                    part.Water = int.Parse(collection.Get(4));
                    part.Thunder = int.Parse(collection.Get(5));
                    part.Ice = int.Parse(collection.Get(6));
                    part.Dragon = int.Parse(collection.Get(7));
                    part.Cut = int.Parse(collection.Get(8));
                    part.Blunt = int.Parse(collection.Get(9));
                    part.Ammo = int.Parse(collection.Get(10));
                    _monsterManager.AddPart(part);

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
               ViewBag.Error = ex.Message;
               return View("Error");
            }
            return View();
        }

        // GET: Part/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int id)
        {
            PartVM part = new PartVM();
            try
            {
                part = _monsterManager.RetreivePartByPartId(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return View(part);
        }

        // POST: Part/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            PartVM part = new PartVM();
            List<Part> monsterparts = new List<Part>();
            PartVM oldPart = new PartVM();
            try
            {
                oldPart = _monsterManager.RetreivePartByPartId(id);
                ViewBag.Monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                int monsterid = int.Parse(collection.Get(2));
                monsterparts = _monsterManager.RetreivePartsByMonsterID(monsterid);
                foreach (var p in monsterparts)
                {
                    if (p.PartName == collection.Get(3) && p.PartID != int.Parse(collection.Get(1)))
                    {
                        ModelState.AddModelError("PartName", "Part " + p.PartName + " already exists for this monster.");
                        break;
                    }
                }

                part.PartID = id;
                part.MonsterID = monsterid;
                part.PartName = collection.Get(3);
                part.Fire = int.Parse(collection.Get(4));
                part.Water = int.Parse(collection.Get(5));
                part.Thunder = int.Parse(collection.Get(6));
                part.Ice = int.Parse(collection.Get(7));
                part.Dragon = int.Parse(collection.Get(8));
                part.Cut = int.Parse(collection.Get(9));
                part.Blunt = int.Parse(collection.Get(10));
                part.Ammo = int.Parse(collection.Get(11));

                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _monsterManager.EditPart(oldPart, part);

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return View(part);
        }
    }
}
