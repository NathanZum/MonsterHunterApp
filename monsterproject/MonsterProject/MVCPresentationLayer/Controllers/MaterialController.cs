using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class MaterialController : Controller
    {
        private IMonsterManager _monsterManager = null;
        private List<MaterialVM> _materials = new List<MaterialVM>();

        public MaterialController()
        {
            _monsterManager = new MonsterManager();
        }

        public MaterialController(IMonsterManager monsterManager)
        {
            _monsterManager = monsterManager;
        }

        // GET: Material
        public ActionResult Index()
        {
            ViewBag.Title = "Material List";
            try
            {
                _materials = _monsterManager.RetreiveMaterialsByActive(true);
                String[] icons = new string[_materials.Count];
                int i = 0;
                foreach (MaterialVM m in _materials)
                {
                    string name = _monsterManager.MonsterImageName(m.MonsterName);
                    icons[i] = "/Content/Images/" + name + "Icon.jpg";
                    i++;
                }
                ViewBag.monsterIcons = icons;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return View(_materials);
        }

        // GET: Material/Details/5
        public ActionResult Details(int? id)
        {
            MaterialVM material = new MaterialVM();

            try
            {
                material = _monsterManager.RetreiveMaterialByMaterialId((int)id);
                material.DropRates = _monsterManager.RetreiveDropRatesByMaterialID((int)id);
                material.PartDropRates = _monsterManager.RetreivePartDropRatesByMaterialID((int)id);
                string name = _monsterManager.MonsterImageName(material.MonsterName);
                ViewBag.Image = "/Content/Images/" + name + ".jpg";
                ViewBag.Title = "Monster Details";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Material Could not be found" + ex.Message;
                return View("Error");
            }
            return View(material);
        }

        // GET: Material/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            MaterialVM material = new MaterialVM();
            material.DropRates = _monsterManager.NewMaterialDropRates();
            List<MonsterVM> monsters = new List<MonsterVM>();
            try{
                monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }
            ViewBag.monsters = monsters;
            return View(material);
        }

        // POST: Material/Create
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(FormCollection collection)
        {
            MaterialVM material = new MaterialVM();
            int id;
            List<Drop> droprates = _monsterManager.NewMaterialDropRates();
            if (ModelState.IsValid)
            {
                material.MaterialName = collection.Get(1);
                material.MonsterID = int.Parse(collection.Get(2));
                material.Price = int.Parse(collection.Get(3));
                droprates.ElementAt(0).DropRate = (decimal) int.Parse(collection.Get(4)) / 100;
                droprates.ElementAt(1).DropRate = (decimal) int.Parse(collection.Get(5)) / 100;
                droprates.ElementAt(2).DropRate = (decimal) int.Parse(collection.Get(6)) / 100;
                droprates.ElementAt(3).DropRate = (decimal) int.Parse(collection.Get(7)) / 100;
                try
                {
                    // TODO: Add insert logic here
                    id = _monsterManager.AddMaterialGetId(material);
                    //material = _monsterManager.R
                    foreach (var droprate in droprates)
                    {
                        if (droprate.DropRate != 0)
                        {
                            droprate.MaterialID = id;
                            _monsterManager.AddDroprate(droprate);
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            return View();
        }

        // GET: Material/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            MaterialVM material = new MaterialVM();
            List<MonsterVM> monsters = new List<MonsterVM>();
            try
            {
                monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                ViewBag.monsters = monsters;
                material = _monsterManager.RetreiveMaterialByMaterialId((int)id);
                material.DropRates = _monsterManager.NewMaterialDropRates();
                List<Drop> drops = _monsterManager.RetreiveDropRatesByMaterialID((int)id);
                foreach (var drop in drops)
                {
                    foreach (var md in material.DropRates)
                    {
                        if (md.DropTypeID == drop.DropTypeID)
                        {
                            md.MaterialID = drop.MaterialID;
                            md.DropRate = (int) (drop.DropRate * 100);
                            break;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }
            return View(material);
        }

        // POST: Material/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var newMaterial = new MaterialVM();
            var newDroprates = _monsterManager.NewMaterialDropRates();
            if (ModelState.IsValid)
            {
                newMaterial.MaterialID = id;
                newMaterial.MaterialName = collection.Get(1);
                newMaterial.MonsterID = int.Parse(collection.Get(2));
                newMaterial.Price = int.Parse(collection.Get(3));
                newDroprates.ElementAt(0).DropRate = (decimal)int.Parse(collection.Get(4)) / 100;
                newDroprates.ElementAt(1).DropRate = (decimal)int.Parse(collection.Get(5)) / 100;
                newDroprates.ElementAt(2).DropRate = (decimal)int.Parse(collection.Get(6)) / 100;
                newDroprates.ElementAt(3).DropRate = (decimal)int.Parse(collection.Get(7)) / 100;
                try
                {
                    // TODO: Add update logic here
                    var oldMaterial = _monsterManager.RetreiveMaterialByMaterialId(id);
                    _monsterManager.EditMaterial(oldMaterial, newMaterial);
                    var oldDroprates = _monsterManager.RetreiveDropRatesByMaterialID(id);
                    foreach (var newDrop in newDroprates)
                    {
                        newDrop.MaterialID = id;
                        bool needsinserted = true;
                        foreach(var oldDrop in oldDroprates)
                        {
                            if (newDrop.DropTypeID == oldDrop.DropTypeID && newDrop.DropRate != oldDrop.DropRate)
                            {
                                _monsterManager.EditDroprate(newDrop, oldDrop);
                                needsinserted = false;
                            }
                            else if (newDrop.DropTypeID == oldDrop.DropTypeID && newDrop.DropRate == oldDrop.DropRate)
                            {
                                needsinserted = false;
                            }
                        }
                        if (needsinserted && newDrop.DropRate != 0)
                        {
                            _monsterManager.AddDroprate(newDrop);
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            return View();
        }

        // GET: Part/AddPartDrop
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AddPartDrop(int? id)
        {
            MaterialVM material = new MaterialVM();
            PartDrop partDrop = new PartDrop();
            partDrop.MaterialID = (int)id;
            try
            {
                material = _monsterManager.RetreiveMaterialByMaterialId((int)id);
                ViewBag.Material = material;
                var drops = _monsterManager.RetreivePartDropRatesByMaterialID((int)id);
                ViewBag.Drops = drops;
                List<Part> unassignedParts = new List<Part>();
                var parts = _monsterManager.RetreivePartsByMonsterID(material.MonsterID);
                foreach (var p in parts)
                {
                    bool assignedPart = false;
                    foreach (var d in drops)
                    {
                        if (p.PartID == d.PartID)
                        {
                            assignedPart = true;
                        }
                    }
                    if (!assignedPart)
                    {
                        unassignedParts.Add(p);
                    }
                }

                ViewBag.Parts = unassignedParts;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return View(partDrop);
        }

        // POST: Part/AddPartDrop
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AddPartDrop(FormCollection collection)
        {
            PartDrop partDrop = new PartDrop();
            if (ModelState.IsValid)
            {
                partDrop.MaterialID = int.Parse(collection.Get(1));
                partDrop.PartID = int.Parse(collection.Get(2));
                partDrop.DropRate = (decimal)int.Parse(collection.Get(3)) / 100;
                try
                {
                    _monsterManager.AddPartDrop(partDrop);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            return View();
        }
    }
}
