using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;
using LogicLayerInterfaces;
using Microsoft.AspNet.Identity;
using MVCPresentationLayer.Models;
using MVCPresentationLayer.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;

namespace MVCPresentationLayer.Controllers
{
    public class MonsterController : Controller
    {
        private IMonsterManager _monsterManager = null;
        private List<MonsterVM> _monsters = new List<MonsterVM>();

        public MonsterController()
        {
            _monsterManager = new MonsterManager();
        }

        public MonsterController(IMonsterManager monsterManager)
        {
            _monsterManager = monsterManager;
        }

        // GET: Monster
        public ActionResult Index()
        {
            ViewBag.Title = "Monster List";
            try
            {
                _monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                String[] icons = new string[_monsters.Count];
                int i = 0;
                foreach (MonsterVM m in _monsters)
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
            return View(_monsters);
        }

        // GET: Monster/Details/5
        public ActionResult Details(int? id)
        {
            MonsterVM monster = new MonsterVM();
            
            try
            {
                monster = _monsterManager.RetreiveMonsterByMonsterId((int)id);
                monster.Terrains = _monsterManager.RetreiveTerrainsByMonsterID(monster.MonsterID);
                monster.Parts = _monsterManager.RetreivePartsByMonsterID(monster.MonsterID);
                monster.Materials = _monsterManager.RetreiveMaterialsByMonsterID(monster.MonsterID);
                string name =  _monsterManager.MonsterImageName(monster.MonsterName); 
                ViewBag.Image = "/Content/Images/" + name + ".jpg";
                ViewBag.Title = "Monster Details";
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Monster Could not be found" + ex.Message;
                return View("Error");
            }
            return View(monster);
        }

        // GET: Monster/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            Monster monster = new Monster();
            var usrMgr = new UserManager();
            try
            {
                if (usrMgr.FindUser(User.Identity.GetUserName()))
                {
                    monster.UserID = usrMgr.RetrieveUserIDFromUsername(User.Identity.GetUserName());
                }
                else
                {
                    View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }

            monster.Weaknesses = _monsterManager.NewMonsterWeaknesses();
            return View(monster);
        }

        // POST: Monster/Create
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(FormCollection collection)
        {
            Monster monster = new Monster();
            List<Weakness> weaknesses = _monsterManager.NewMonsterWeaknesses();
            if (ModelState.IsValid)
            {
                monster.MonsterName = collection.Get(1);
                monster.MonsterType = collection.Get(2);
                weaknesses.ElementAt(0).Effectiveness = int.Parse(collection.Get(3));
                weaknesses.ElementAt(1).Effectiveness = int.Parse(collection.Get(4));
                weaknesses.ElementAt(2).Effectiveness = int.Parse(collection.Get(5));
                weaknesses.ElementAt(3).Effectiveness = int.Parse(collection.Get(6));
                weaknesses.ElementAt(4).Effectiveness = int.Parse(collection.Get(7));
                weaknesses.ElementAt(5).Effectiveness = int.Parse(collection.Get(8));
                weaknesses.ElementAt(6).Effectiveness = int.Parse(collection.Get(9));
                weaknesses.ElementAt(7).Effectiveness = int.Parse(collection.Get(10));
                weaknesses.ElementAt(8).Effectiveness = int.Parse(collection.Get(11));
                weaknesses.ElementAt(9).Effectiveness = int.Parse(collection.Get(12));
                monster.Weaknesses = weaknesses;
                try
                {
                    // TODO: Add insert logic here
                    var usrMgr = new UserManager();
                    monster.UserID = usrMgr.RetrieveUserIDFromUsername(User.Identity.GetUserName());
                    _monsterManager.AddMonster(monster);
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

        // GET: Monster/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int id)
        {
            Monster monster = new Monster();
            try
            {
                monster = _monsterManager.RetreiveMonsterByMonsterId(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }
            return View(monster);
        }

        // POST: Monster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Monster newMonster = new Monster();
                List<Weakness> weaknesses = _monsterManager.NewMonsterWeaknesses();
                newMonster.MonsterID = int.Parse(collection.Get(1));
                newMonster.MonsterName = collection.Get(2);
                newMonster.MonsterType = collection.Get(3);
                weaknesses.ElementAt(0).Effectiveness = int.Parse(collection.Get(4));
                weaknesses.ElementAt(1).Effectiveness = int.Parse(collection.Get(5));
                weaknesses.ElementAt(2).Effectiveness = int.Parse(collection.Get(6));
                weaknesses.ElementAt(3).Effectiveness = int.Parse(collection.Get(7));
                weaknesses.ElementAt(4).Effectiveness = int.Parse(collection.Get(8));
                weaknesses.ElementAt(5).Effectiveness = int.Parse(collection.Get(9));
                weaknesses.ElementAt(6).Effectiveness = int.Parse(collection.Get(10));
                weaknesses.ElementAt(7).Effectiveness = int.Parse(collection.Get(11));
                weaknesses.ElementAt(8).Effectiveness = int.Parse(collection.Get(12));
                weaknesses.ElementAt(9).Effectiveness = int.Parse(collection.Get(13));
                newMonster.Weaknesses = weaknesses;
                try
                {
                    // TODO: Add update logic here
                    Monster oldMonster = _monsterManager.RetreiveMonsterByMonsterId(id);
                    _monsterManager.EditMonster(oldMonster, newMonster);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //GET: Monster/CreateTerrain
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateTerrain()
        {
            Terrain terrain = new Terrain();
            List<Terrain> terrains = new List<Terrain>();
            try
            {
                terrains = _monsterManager.RetreiveTerrains();
                ViewBag.Terrains = terrains;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }

            
            return View(terrain);
        }

        // POST: Monster/CreateTerrain
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateTerrain(FormCollection collection)
        {
            Terrain terrain = new Terrain();
            terrain.TerrainName = collection.Get(1);
            try
            {
                List<Terrain> terrains = _monsterManager.RetreiveTerrains();
                ViewBag.Terrains = terrains;
                foreach (Terrain t in terrains)
                {
                    if (t.TerrainName == terrain.TerrainName)
                    {
                        ModelState.AddModelError("TerrainName", "Terrain " + terrain.TerrainName + " already exists.");
                    }
                }
                if (ModelState.IsValid)
                {
                    _monsterManager.AddTerrain(terrain);
                }
            }
            catch(Exception ex) {
                ViewBag.Error = ex.Message;
                View("Error");
            }

            return RedirectToAction("CreateTerrain", "Monster");
        }

        //GET: Monster/AssignTerrain
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AssignTerrain(int id)
        {
            MonsterVM monster = new MonsterVM();
            List<Terrain> terrains = new List<Terrain>();
            List<Terrain> unassignedTerrains = new List<Terrain>();
            try
            {
                monster = _monsterManager.RetreiveMonsterByMonsterId(id);
                string name = _monsterManager.MonsterImageName(monster.MonsterName);
                ViewBag.Image = "/Content/Images/" + name + ".jpg";
                terrains = _monsterManager.RetreiveTerrains();
                monster.Terrains = _monsterManager.RetreiveTerrainsByMonsterID(id);
                foreach (var t in terrains)
                {
                    bool unassigned = true;
                    foreach (var terrain in monster.Terrains)
                    {
                        if (terrain.TerrainID == t.TerrainID)
                        {
                            unassigned = false;
                        }
                    }
                    if (unassigned)
                    {
                        unassignedTerrains.Add(t);
                    }
                }
                ViewBag.Terrains = unassignedTerrains;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }

            return View(monster);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult RemoveTerrain(int monsterid, int terrainid)
        {
            try
            {
                _monsterManager.UnassignTerrain(monsterid, terrainid);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }
            return RedirectToAction("AssignTerrain", "Monster", new { id = monsterid });
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AddTerrain(int monsterid, int terrainid)
        {
            try
            {
                _monsterManager.AssignTerrain(monsterid, terrainid);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                View("Error");
            }
            return RedirectToAction("AssignTerrain", "Monster", new { id = monsterid });
        }

        public ActionResult MaterialRedirect(int id)
        {
            return RedirectToAction("Details", "Material", new { id = id });
        }
    }
}
