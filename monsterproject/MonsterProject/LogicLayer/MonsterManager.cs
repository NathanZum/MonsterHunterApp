using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using LogicLayerInterfaces;
using System.Text.RegularExpressions;
using System.Threading;

namespace LogicLayer
{
    public class MonsterManager : IMonsterManager
    {
        IMonsterAccessor _monsterAccessor = null;

        public MonsterManager()
        {
            _monsterAccessor = new DataAccessLayer.MonsterAccessor();
        }

        public MonsterManager(IMonsterAccessor monsterAccessor)
        {
            _monsterAccessor = monsterAccessor;
        }

        public bool AddMonster(Monster monster)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.InsertMonster(monster));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Drop> NewMaterialDropRates()
        {
            List<Drop> droprates = new List<Drop>();
            // [material_id], [MaterialDropRate].[droptype_id], [DropType].[droptype_name], [droprate]
            // droptypeid      10000, 10001, 10002, 10003
            // droptype name   Carve, Capture, Target, Dropped
            Drop drop;
            for (int i = 0; i < 4; i++)
            {
                drop = new Drop();
                if (i == 0)
                {
                    drop.DropTypeID = 10000;
                    drop.DropTypeName = "Carve";
                }
                else if(i == 1)
                {
                    drop.DropTypeID = 10001;
                    drop.DropTypeName = "Capture";
                }
                else if (i == 2)
                {
                    drop.DropTypeID = 10002;
                    drop.DropTypeName = "Target";
                }
                else if (i == 3)
                {
                    drop.DropTypeID = 10003;
                    drop.DropTypeName = "Dropped";
                }
                drop.DropRate = (decimal)0.00;
                droprates.Add(drop);
            }
            return droprates;
        }

        public List<Weakness> NewMonsterWeaknesses()
        {
            List<Weakness> monsterWeaknesses = new List<Weakness>();

            // [Poison], [Stun], [Paralysis],		
            // [Sleep], [Blast], [Exhaust], [Fireblight], [Waterblight], [Thunderblight],	
            // [Iceblight]
            var poison = new Weakness();
            poison.Name = "Poison";
            poison.Effectiveness = 0;
            monsterWeaknesses.Add(poison);
            var stun = new Weakness();
            stun.Name = "Stun";
            stun.Effectiveness = 0;
            monsterWeaknesses.Add(stun);
            var para = new Weakness();
            para.Name = "Paralysis";
            para.Effectiveness = 0;
            monsterWeaknesses.Add(para);
            var sleep = new Weakness();
            sleep.Name = "Sleep";
            sleep.Effectiveness = 0;
            monsterWeaknesses.Add(sleep);
            var blast = new Weakness();
            blast.Name = "Blast";
            blast.Effectiveness = 0;
            monsterWeaknesses.Add(blast);
            var exhaust = new Weakness();
            exhaust.Name = "Exhaust";
            exhaust.Effectiveness = 0;
            monsterWeaknesses.Add(exhaust);
            var fb = new Weakness();
            fb.Name = "Fireblight";
            fb.Effectiveness = 0;
            monsterWeaknesses.Add(fb);
            var wb = new Weakness();
            wb.Name = "Waterblight";
            wb.Effectiveness = 0;
            monsterWeaknesses.Add(wb);
            var tb = new Weakness();
            tb.Name = "Thunderblight";
            tb.Effectiveness = 0;
            monsterWeaknesses.Add(tb);
            var ib = new Weakness();
            ib.Name = "Iceblight";
            ib.Effectiveness = 0;
            monsterWeaknesses.Add(ib);

            return monsterWeaknesses;
        }

        public List<Drop> RetreiveDropRatesByMaterialID(int material_id)
        {
            List<Drop> droprates = new List<Drop>();
            try
            {
                droprates = _monsterAccessor.SelectDropRatesByMaterialID(material_id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return droprates;
        }

        public List<MaterialVM> RetreiveMaterialsByActive(bool active)
        {
            List<MaterialVM> materials = new List<MaterialVM>();
            try
            {
                materials = _monsterAccessor.SelectMaterialsByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return materials;
        }

        public List<MonsterVM> RetreiveMonsterVMsByActive(bool active)
        {
            List<MonsterVM> monsters = new List<MonsterVM>();
            try
            {
                monsters = _monsterAccessor.SelectMonstersByActive(active);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return monsters;
        }

        public List<PartDrop> RetreivePartDropRatesByMaterialID(int material_id)
        {
            List<PartDrop> partdroprates = new List<PartDrop>();
            try
            {
                partdroprates = _monsterAccessor.SelectPartDropRatesByMaterialID(material_id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return partdroprates;
        }

        public List<MaterialVM> RetreiveMaterialsByMonsterID(int monster_id)
        {
            List<MaterialVM> materials = new List<MaterialVM>();
            try
            {
                materials = _monsterAccessor.SelectMaterialsByMonsterID(monster_id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return materials;
        }

        public List<Part> RetreivePartsByMonsterID(int monster_id)
        {
            List<Part> parts = new List<Part>();
            try
            {
                parts = _monsterAccessor.SelectPartsByMonsterID(monster_id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return parts;
        }

        public List<Terrain> RetreiveTerrainsByMonsterID(int monster_id)
        {
            List<Terrain> terrains = new List<Terrain>();
            try
            {
                terrains = _monsterAccessor.SelectTerrainsByMonsterID(monster_id);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return terrains;
        }

        public bool EditMonster(Monster oldMonster, Monster newMonster)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.UpdateMonster(oldMonster, newMonster));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool AddMaterial(Material material)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.InsertMaterial(material));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool EditMaterial(Material oldMaterial, Material newMaterial)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.UpdateMaterial(oldMaterial, newMaterial));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Terrain> RetreiveTerrains()
        {
            List<Terrain> terrains = new List<Terrain>();
            try
            {
                terrains = _monsterAccessor.SelectTerrains();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return terrains;
        }

        public bool AddTerrain(Terrain terrain)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.InsertTerrain(terrain));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool AssignTerrain(int monster_id, int terrain_id)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.InsertAreaByMonsterId(monster_id, terrain_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<PartVM> RetreiveParts()
        {
            List<PartVM> parts = new List<PartVM>();
            try
            {
                parts = _monsterAccessor.SelectParts();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return parts;
        }

        public bool AddPart(Part part)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.InsertPart(part));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool EditPart(Part oldPart, Part newPart)
        {
            bool result = false;
            try
            {
                result = (1 == _monsterAccessor.UpdatePart(oldPart, newPart));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
