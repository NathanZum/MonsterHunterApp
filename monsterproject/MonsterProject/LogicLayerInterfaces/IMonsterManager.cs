using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IMonsterManager
    {
        List<MonsterVM> RetreiveMonsterVMsByActive(bool active);

        List<Terrain> RetreiveTerrainsByMonsterID(int monster_id);

        List<Part> RetreivePartsByMonsterID(int monster_id);

        List<MaterialVM> RetreiveMaterialsByMonsterID(int monster_id);

        List<MaterialVM> RetreiveMaterialsByActive(bool active);

        List<Drop> RetreiveDropRatesByMaterialID(int material_id);

        List<PartDrop> RetreivePartDropRatesByMaterialID(int material_id);

        List<Weakness> NewMonsterWeaknesses();

        List<Drop> NewMaterialDropRates();

        List<Terrain> RetreiveTerrains();

        bool AddMonster(Monster monster);

        bool EditMonster(Monster oldMonster, Monster newMonster);

        bool AddMaterial(Material material);

        bool EditMaterial(Material oldMaterial, Material newMaterial);

        bool AddTerrain(Terrain terrain);

        bool AssignTerrain(int monster_id, int terrain_id);

        List<PartVM> RetreiveParts();

        bool AddPart(Part part);

        bool EditPart(Part oldPart, Part newPart);

    }
}
