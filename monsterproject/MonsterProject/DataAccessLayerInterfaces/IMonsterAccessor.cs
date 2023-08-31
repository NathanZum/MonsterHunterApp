using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IMonsterAccessor
    {
        List<MonsterVM> SelectMonstersByActive(bool active);

        List<Terrain> SelectTerrainsByMonsterID(int monster_id);

        Terrain SelectTerrainByTerrainId(int terrain_id);

        List<Part> SelectPartsByMonsterID(int monster_id);

        PartVM SelectPartByPartId(int part_id);

        List<MaterialVM> SelectMaterialsByMonsterID(int monster_id);

        MaterialVM SelectMaterialByMaterialId(int material_id);

        List<MaterialVM> SelectMaterialsByActive(bool active);

        List<Drop> SelectDropRatesByMaterialID(int material_id);

        List<PartDrop> SelectPartDropRatesByMaterialID(int material_id);

        MonsterVM SelectMonsterByMonsterID(int monster_id);

        int InsertMonster(Monster monster);

        int UpdateMonster(Monster oldMonster, Monster newMonster);

        int InsertMaterial(Material material);

        int UpdateMaterial(Material oldMaterial, Material newMaterial);

        List<Terrain> SelectTerrains();

        int InsertTerrain(Terrain terrain);

        int InsertAreaByMonsterId(int monster_id, int terrain_id);

        int DeleteAreaByMonsterId(int monster_id, int terrain_id);

        List<PartVM> SelectParts();

        int InsertPart(Part part);

        int UpdatePart(Part oldPart, Part newPart);

        int InsertDroprate(Drop droprate);

        int UpdateDroprate(Drop newDroprate, Drop oldDroprate);

        int InsertPartDrop(PartDrop partDrop);
    }
}
