using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace DataAccessLayer
{
    public class MonsterAccessor : IMonsterAccessor
    {
        public int InsertAreaByMonsterId(int monster_id, int terrain_id)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_area_by_monster_id_and_terrain_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @monster_id		int,
	            @terrain_id		int	
            */
            cmd.Parameters.AddWithValue("@monster_id", monster_id);
            cmd.Parameters.AddWithValue("@terrain_id", terrain_id);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rows;
        }

        public int DeleteAreaByMonsterId(int monster_id, int terrain_id)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_area_by_monster_id_and_terrain_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @monster_id		int,
	            @terrain_id		int	
            */
            cmd.Parameters.AddWithValue("@monster_id", monster_id);
            cmd.Parameters.AddWithValue("@terrain_id", terrain_id);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rows;
        }

        public int InsertDroprate(Drop droprate)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_droprate";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @material_id	int,
	            @droptype_id	int,
	            @droprate		decimal(3, 2)
            */
            cmd.Parameters.AddWithValue("@material_id", droprate.MaterialID);
            cmd.Parameters.AddWithValue("@droptype_id", droprate.DropTypeID);
            cmd.Parameters.AddWithValue("@droprate", droprate.DropRate);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rows;
        }

        public int InsertMaterial(Material material)
        {
            int id = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_material";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @monster_id			int,
	            @material_name		nvarchar(30),	
	            @price				int		
            */
            cmd.Parameters.AddWithValue("@monster_id", material.MonsterID);
            cmd.Parameters.AddWithValue("@material_name", material.MaterialName);
            cmd.Parameters.AddWithValue("@price", material.Price);
            
            try
            {
                conn.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;
        }

        public int InsertMonster(Monster monster)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_monster";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
            @appuser_id         int,
            @monster_name       nvarchar(30),	
	        @monster_type nvarchar(15),		
	        @Poison             int,
            @Stun               int,
            @Paralysis          int,
            @Sleep              int,
            @Blast              int,
            @Exhaust            int,
            @Fireblight         int,
            @Waterblight        int,
            @Thunderblight      int,
            @Iceblight          int
            */

            cmd.Parameters.AddWithValue("@appuser_id", monster.UserID);
            cmd.Parameters.AddWithValue("@monster_name", monster.MonsterName);
            cmd.Parameters.AddWithValue("@monster_type", monster.MonsterType);
            cmd.Parameters.AddWithValue("@Poison", monster.Weaknesses.ElementAt(0).Effectiveness);
            cmd.Parameters.AddWithValue("@Stun", monster.Weaknesses.ElementAt(1).Effectiveness);
            cmd.Parameters.AddWithValue("@Paralysis", monster.Weaknesses.ElementAt(2).Effectiveness);
            cmd.Parameters.AddWithValue("@Sleep", monster.Weaknesses.ElementAt(3).Effectiveness);
            cmd.Parameters.AddWithValue("@Blast", monster.Weaknesses.ElementAt(4).Effectiveness);
            cmd.Parameters.AddWithValue("@Exhaust", monster.Weaknesses.ElementAt(5).Effectiveness);
            cmd.Parameters.AddWithValue("@Fireblight", monster.Weaknesses.ElementAt(6).Effectiveness);
            cmd.Parameters.AddWithValue("@Waterblight", monster.Weaknesses.ElementAt(7).Effectiveness);
            cmd.Parameters.AddWithValue("@Thunderblight", monster.Weaknesses.ElementAt(8).Effectiveness);
            cmd.Parameters.AddWithValue("@Iceblight", monster.Weaknesses.ElementAt(9).Effectiveness);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int InsertPart(Part part)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_part";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
            @monster_id		int,			
	        @part_name		nvarchar(30),	
	        @Fire			int,			
	        @Water			int,			
	        @Thunder		int,			
	        @Ice			int,			
	        @Dragon			int,			
	        @Cut			int,			
	        @Blunt			int,			
	        @Ammo			int	
            */
            cmd.Parameters.AddWithValue("@monster_id", part.MonsterID);
            cmd.Parameters.AddWithValue("@part_name", part.PartName);
            cmd.Parameters.AddWithValue("@Fire", part.Fire);
            cmd.Parameters.AddWithValue("@Water", part.Water);
            cmd.Parameters.AddWithValue("@Thunder", part.Thunder);
            cmd.Parameters.AddWithValue("@Ice", part.Ice);
            cmd.Parameters.AddWithValue("@Dragon", part.Dragon);
            cmd.Parameters.AddWithValue("@Cut", part.Cut);
            cmd.Parameters.AddWithValue("@Blunt", part.Blunt);
            cmd.Parameters.AddWithValue("@Ammo", part.Ammo);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        public int InsertTerrain(Terrain terrain)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_terrain";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@terrain_name", terrain.TerrainName);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        public List<Drop> SelectDropRatesByMaterialID(int material_id)
        {
            List<Drop> droprates = new List<Drop>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_droprates_by_materialid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@material_id", SqlDbType.Int);
            cmd.Parameters["@material_id"].Value = material_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var droprate = new Drop();
                        droprate.MaterialID = reader.GetInt32(0);
                        droprate.DropTypeID = reader.GetInt32(1);
                        droprate.DropTypeName = reader.GetString(2);
                        droprate.DropRate = reader.GetDecimal(3);
                        droprates.Add(droprate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return droprates;
        }

        public MaterialVM SelectMaterialByMaterialId(int material_id)
        {
            var material = new MaterialVM();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_material_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@material_id", SqlDbType.Int);
            cmd.Parameters["@material_id"].Value = material_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        material.MaterialID = reader.GetInt32(0);
                        material.MonsterID = reader.GetInt32(1);
                        material.MaterialName = reader.GetString(2);
                        material.Price = reader.GetInt32(3);
                        material.Active = reader.GetBoolean(4);
                        material.MonsterName = reader.GetString(5);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return material;
        }

        public List<MaterialVM> SelectMaterialsByActive(bool active)
        {
            List<MaterialVM> materials = new List<MaterialVM>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_material_by_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@active", SqlDbType.Bit);
            cmd.Parameters["@active"].Value = active;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var material = new MaterialVM();
                        material.MaterialID = reader.GetInt32(0);
                        material.MonsterID = reader.GetInt32(1);
                        material.MaterialName = reader.GetString(2);
                        material.Price = reader.GetInt32(3);
                        material.Active = reader.GetBoolean(4);
                        material.MonsterName = reader.GetString(5);
                        materials.Add(material);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return materials;
        }

        public List<MaterialVM> SelectMaterialsByMonsterID(int monster_id)
        {
            List<MaterialVM> materials = new List<MaterialVM>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_materials_by_monster_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@monster_id", SqlDbType.Int);
            cmd.Parameters["@monster_id"].Value = monster_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var material = new MaterialVM();
                        material.MaterialID = reader.GetInt32(0);
                        material.MonsterID = reader.GetInt32(1);
                        material.MaterialName = reader.GetString(2);
                        material.Price = reader.GetInt32(3);
                        material.Active = reader.GetBoolean(4);
                        materials.Add(material);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return materials;
        }

        public MonsterVM SelectMonsterByMonsterID(int monster_id)
        {
            MonsterVM monster = new MonsterVM();
            List<Weakness> weaknesses;
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_monster_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = monster_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        weaknesses = new List<Weakness>();
                        monster.MonsterID = reader.GetInt32(0);
                        monster.UserID = reader.GetInt32(1);
                        monster.MonsterName = reader.GetString(2);
                        monster.MonsterType = reader.GetString(3);
                        monster.Active = reader.GetBoolean(4);
                        for (int i = 5; i < 15; i++)
                        {
                            Weakness weakness = new Weakness();
                            weakness.Name = reader.GetName(i);
                            weakness.Effectiveness = reader.GetInt32(i);
                            weaknesses.Add(weakness);
                        }
                        monster.Weaknesses = weaknesses;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return monster;
        }

        public List<MonsterVM> SelectMonstersByActive(bool active)
        {
            List<MonsterVM> monsters = new List<MonsterVM>();
            List<Weakness> weaknesses;
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_monster_by_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@active", SqlDbType.Bit);
            cmd.Parameters["@active"].Value = active;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        weaknesses = new List<Weakness>();
                        var monster = new MonsterVM();
                        monster.MonsterID = reader.GetInt32(0);
                        monster.UserID = reader.GetInt32(1);
                        monster.MonsterName = reader.GetString(2);
                        monster.MonsterType = reader.GetString(3);
                        monster.Active = reader.GetBoolean(4);
                        for (int i = 5; i < 15; i++)
                        {
                            Weakness weakness = new Weakness();
                            weakness.Name = reader.GetName(i);
                            weakness.Effectiveness = reader.GetInt32(i);
                            weaknesses.Add(weakness);
                        }
                        monster.Weaknesses = weaknesses;
                        monsters.Add(monster);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return monsters;
        }

        public PartVM SelectPartByPartId(int part_id)
        {
            var part = new PartVM();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_part_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@part_id", SqlDbType.Int);
            cmd.Parameters["@part_id"].Value = part_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        part.PartID = reader.GetInt32(0);
                        part.MonsterID = reader.GetInt32(1);
                        part.MonsterName = reader.GetString(2);
                        part.PartName = reader.GetString(3);
                        part.Fire = reader.GetInt32(4);
                        part.Water = reader.GetInt32(5);
                        part.Thunder = reader.GetInt32(6);
                        part.Ice = reader.GetInt32(7);
                        part.Dragon = reader.GetInt32(8);
                        part.Cut = reader.GetInt32(9);
                        part.Blunt = reader.GetInt32(10);
                        part.Ammo = reader.GetInt32(11);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return part;
        }

        public List<PartDrop> SelectPartDropRatesByMaterialID(int material_id)
        {
            List<PartDrop> partdroprates = new List<PartDrop>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_part_droprates_by_materialid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@material_id", SqlDbType.Int);
            cmd.Parameters["@material_id"].Value = material_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var partdroprate = new PartDrop();
                        partdroprate.MaterialID = reader.GetInt32(0);
                        partdroprate.PartID = reader.GetInt32(1);
                        partdroprate.PartName = reader.GetString(2);
                        partdroprate.DropRate = reader.GetDecimal(3);
                        partdroprates.Add(partdroprate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return partdroprates;
        }

        public List<PartVM> SelectParts()
        {
            List<PartVM> parts = new List<PartVM>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_parts";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var part = new PartVM();
                        part.PartID = reader.GetInt32(0);
                        part.MonsterID = reader.GetInt32(1);
                        part.MonsterName = reader.GetString(2);
                        part.PartName = reader.GetString(3);
                        part.Fire = reader.GetInt32(4);
                        part.Water = reader.GetInt32(5);
                        part.Thunder = reader.GetInt32(6);
                        part.Ice = reader.GetInt32(7);
                        part.Dragon = reader.GetInt32(8);
                        part.Cut = reader.GetInt32(9);
                        part.Blunt = reader.GetInt32(10);
                        part.Ammo = reader.GetInt32(11);
                        parts.Add(part);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return parts;
        }

        public List<Part> SelectPartsByMonsterID(int monster_id)
        {
            List<Part> parts = new List<Part>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_parts_by_monster_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@monster_id", SqlDbType.Int);
            cmd.Parameters["@monster_id"].Value = monster_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var part = new Part();
                        part.PartID = reader.GetInt32(0);
                        part.MonsterID = reader.GetInt32(1);
                        part.PartName = reader.GetString(2);
                        part.Fire = reader.GetInt32(3);
                        part.Water = reader.GetInt32(4);
                        part.Thunder = reader.GetInt32(5);
                        part.Ice = reader.GetInt32(6);
                        part.Dragon = reader.GetInt32(7);
                        part.Cut = reader.GetInt32(8);
                        part.Blunt = reader.GetInt32(9);
                        part.Ammo = reader.GetInt32(10);
                        parts.Add(part);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return parts;
        }

        public Terrain SelectTerrainByTerrainId(int terrain_id)
        {
            var terrain = new Terrain();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_terrain_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@terrain_id", SqlDbType.Int);
            cmd.Parameters["@terrain_id"].Value = terrain_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        terrain.TerrainID = reader.GetInt32(1);
                        terrain.TerrainName = reader.GetString(2);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return terrain;
        }

        public List<Terrain> SelectTerrains()
        {
            List<Terrain> terrains = new List<Terrain>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_terrains";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var terrain = new Terrain();
                        terrain.TerrainID = reader.GetInt32(0);
                        terrain.TerrainName = reader.GetString(1);
                        terrains.Add(terrain);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return terrains;
        }

        public List<Terrain> SelectTerrainsByMonsterID(int monster_id)
        {
            List<Terrain> terrains = new List<Terrain>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_terrain_by_monster_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@monster_id", SqlDbType.Int);
            cmd.Parameters["@monster_id"].Value = monster_id;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var terrain = new Terrain();
                        terrain.TerrainID = reader.GetInt32(1);
                        terrain.TerrainName = reader.GetString(2);
                        terrains.Add(terrain);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return terrains;
        }

        public int UpdateDroprate(Drop newDroprate, Drop oldDroprate)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_droprate";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @material_id	int,
	            @droptype_id	int,
	            @droprate		decimal(3, 2),
	            @oldmaterial_id	int,
	            @olddroptype_id	int,
	            @olddroprate	decimal(3, 2)
            */
            cmd.Parameters.AddWithValue("@material_id", newDroprate.MaterialID);
            cmd.Parameters.AddWithValue("@droptype_id", newDroprate.DropTypeID);
            cmd.Parameters.AddWithValue("@droprate", newDroprate.DropRate);
            cmd.Parameters.AddWithValue("@oldmaterial_id", oldDroprate.MaterialID);
            cmd.Parameters.AddWithValue("@olddroptype_id", oldDroprate.DropTypeID);
            cmd.Parameters.AddWithValue("@olddroprate", oldDroprate.DropRate);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rows;
        }

        public int UpdateMaterial(Material oldMaterial, Material newMaterial)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_material";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @material_id        int,
	            @monster_id			int,
	            @material_name		nvarchar(30),	
	            @price				int,
	            @oldmonster_id		int,
	            @oldmaterial_name	nvarchar(30),	
	            @oldprice			int
            */
            cmd.Parameters.AddWithValue("@material_id", oldMaterial.MaterialID);
            cmd.Parameters.AddWithValue("@monster_id", newMaterial.MonsterID);
            cmd.Parameters.AddWithValue("@material_name", newMaterial.MaterialName);
            cmd.Parameters.AddWithValue("@price", newMaterial.Price);
            cmd.Parameters.AddWithValue("@oldmonster_id", oldMaterial.MonsterID);
            cmd.Parameters.AddWithValue("@oldmaterial_name", oldMaterial.MaterialName);
            cmd.Parameters.AddWithValue("@oldprice", oldMaterial.Price);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        public int UpdateMonster(Monster oldMonster, Monster newMonster)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_monster";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @monster_id			int,
	            @monster_name		nvarchar(30),	
	            @monster_type		nvarchar(15),		
	            @Poison				int,			
	            @Stun				int,			
	            @Paralysis			int,			
	            @Sleep				int,			
	            @Blast				int,			
	            @Exhaust			int,			
	            @Fireblight			int,			
	            @Waterblight		int,			
	            @Thunderblight		int,			
	            @Iceblight			int,
	            @oldmonster_name	nvarchar(30),	
	            @oldmonster_type	nvarchar(15),		
	            @oldPoison			int,			
	            @oldStun			int,			
	            @oldParalysis		int,			
	            @oldSleep			int,			
	            @oldBlast			int,			
	            @oldExhaust			int,			
	            @oldFireblight		int,			
	            @oldWaterblight		int,			
	            @oldThunderblight	int,			
	            @oldIceblight		int
            */
            cmd.Parameters.AddWithValue("@monster_id", oldMonster.MonsterID);
            cmd.Parameters.AddWithValue("@monster_name", newMonster.MonsterName);
            cmd.Parameters.AddWithValue("@monster_type", newMonster.MonsterType);
            cmd.Parameters.AddWithValue("@Poison", newMonster.Weaknesses.ElementAt(0).Effectiveness);
            cmd.Parameters.AddWithValue("@Stun", newMonster.Weaknesses.ElementAt(1).Effectiveness);
            cmd.Parameters.AddWithValue("@Paralysis", newMonster.Weaknesses.ElementAt(2).Effectiveness);
            cmd.Parameters.AddWithValue("@Sleep", newMonster.Weaknesses.ElementAt(3).Effectiveness);
            cmd.Parameters.AddWithValue("@Blast", newMonster.Weaknesses.ElementAt(4).Effectiveness);
            cmd.Parameters.AddWithValue("@Exhaust", newMonster.Weaknesses.ElementAt(5).Effectiveness);
            cmd.Parameters.AddWithValue("@Fireblight", newMonster.Weaknesses.ElementAt(6).Effectiveness);
            cmd.Parameters.AddWithValue("@Waterblight", newMonster.Weaknesses.ElementAt(7).Effectiveness);
            cmd.Parameters.AddWithValue("@Thunderblight", newMonster.Weaknesses.ElementAt(8).Effectiveness);
            cmd.Parameters.AddWithValue("@Iceblight", newMonster.Weaknesses.ElementAt(9).Effectiveness);
            cmd.Parameters.AddWithValue("@oldmonster_name", oldMonster.MonsterName);
            cmd.Parameters.AddWithValue("@oldmonster_type", oldMonster.MonsterType);
            cmd.Parameters.AddWithValue("@oldPoison", oldMonster.Weaknesses.ElementAt(0).Effectiveness);
            cmd.Parameters.AddWithValue("@oldStun", oldMonster.Weaknesses.ElementAt(1).Effectiveness);
            cmd.Parameters.AddWithValue("@oldParalysis", oldMonster.Weaknesses.ElementAt(2).Effectiveness);
            cmd.Parameters.AddWithValue("@oldSleep", oldMonster.Weaknesses.ElementAt(3).Effectiveness);
            cmd.Parameters.AddWithValue("@oldBlast", oldMonster.Weaknesses.ElementAt(4).Effectiveness);
            cmd.Parameters.AddWithValue("@oldExhaust", oldMonster.Weaknesses.ElementAt(5).Effectiveness);
            cmd.Parameters.AddWithValue("@oldFireblight", oldMonster.Weaknesses.ElementAt(6).Effectiveness);
            cmd.Parameters.AddWithValue("@oldWaterblight", oldMonster.Weaknesses.ElementAt(7).Effectiveness);
            cmd.Parameters.AddWithValue("@oldThunderblight", oldMonster.Weaknesses.ElementAt(8).Effectiveness);
            cmd.Parameters.AddWithValue("@oldIceblight", oldMonster.Weaknesses.ElementAt(9).Effectiveness);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        public int UpdatePart(Part oldPart, Part newPart)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_part";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
            @part_id		int,			
	        @part_name		nvarchar(30),	
	        @Fire			int,			
	        @Water			int,			
	        @Thunder		int,			
	        @Ice			int,			
	        @Dragon			int,			
	        @Cut			int,			
	        @Blunt			int,			
	        @Ammo			int,
            @oldpart_name	nvarchar(30),	
	        @oldFire		int,			
	        @oldWater		int,			
	        @oldThunder		int,			
	        @oldIce			int,			
	        @oldDragon		int,			
	        @oldCut			int,			
	        @oldBlunt		int,			
	        @oldAmmo		int
            */
            cmd.Parameters.AddWithValue("@part_id", oldPart.PartID);
            cmd.Parameters.AddWithValue("@part_name", newPart.PartName);
            cmd.Parameters.AddWithValue("@Fire", newPart.Fire);
            cmd.Parameters.AddWithValue("@Water", newPart.Water);
            cmd.Parameters.AddWithValue("@Thunder", newPart.Thunder);
            cmd.Parameters.AddWithValue("@Ice", newPart.Ice);
            cmd.Parameters.AddWithValue("@Dragon", newPart.Dragon);
            cmd.Parameters.AddWithValue("@Cut", newPart.Cut);
            cmd.Parameters.AddWithValue("@Blunt", newPart.Blunt);
            cmd.Parameters.AddWithValue("@Ammo", newPart.Ammo);
            cmd.Parameters.AddWithValue("@oldpart_name", oldPart.PartName);
            cmd.Parameters.AddWithValue("@oldFire", oldPart.Fire);
            cmd.Parameters.AddWithValue("@oldWater", oldPart.Water);
            cmd.Parameters.AddWithValue("@oldThunder", oldPart.Thunder);
            cmd.Parameters.AddWithValue("@oldIce", oldPart.Ice);
            cmd.Parameters.AddWithValue("@oldDragon", oldPart.Dragon);
            cmd.Parameters.AddWithValue("@oldCut", oldPart.Cut);
            cmd.Parameters.AddWithValue("@oldBlunt", oldPart.Blunt);
            cmd.Parameters.AddWithValue("@oldAmmo", oldPart.Ammo);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        public int InsertPartDrop(PartDrop partDrop)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_part_droprate";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@material_id", partDrop.MaterialID);
            cmd.Parameters.AddWithValue("@part_id", partDrop.PartID);
            cmd.Parameters.AddWithValue("@droprate", partDrop.DropRate);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }
    }
}
