using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicLayer;
using LogicLayerInterfaces;
using System.Linq.Expressions;
using System.Windows.Media.Media3D;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for TerrainView.xaml
    /// </summary>
    public partial class TerrainView : Window
    {
        List<Terrain> _terrains;
        MonsterVM _monster;
        MonsterManager _monsterManager;
        bool _add = false;
        bool _assign = false;
        public TerrainView(MonsterVM monster, MonsterManager monsterManager, bool add, bool assign)
        {
            _monster = monster;
            _monsterManager = monsterManager;
            _add = add;
            _assign = assign;
            try{
                _terrains = _monsterManager.RetreiveTerrains();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                lblTerrainView.Content = "Add Terrain";
                lblTerrainList.Content = "List of Terrains:";
                btnAddTerrain.Content = "Add";
                lblTerrain.Visibility = Visibility.Visible;
                txtTerrainName.Visibility = Visibility.Visible;
                lblMonsterName.Visibility = Visibility.Hidden;
                txtMonsterName.Visibility = Visibility.Hidden;
                lblTerrainSelect.Visibility = Visibility.Hidden;
                cmbTerrains.Visibility = Visibility.Hidden;

                try
                {
                    datTerrains.ItemsSource = _terrains;
                    datTerrains.Columns.RemoveAt(0);
                }
                catch (ArgumentOutOfRangeException)
                { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (_assign)
            {
                lblTerrainView.Content = "Assign Terrain";
                lblTerrainList.Content = "List of Terrains Assigned to Monster:";
                btnAddTerrain.Content = "Assign";
                lblTerrain.Visibility = Visibility.Hidden;
                txtTerrainName.Visibility = Visibility.Hidden;
                lblMonsterName.Visibility = Visibility.Visible;
                txtMonsterName.Visibility = Visibility.Visible;
                lblTerrainSelect.Visibility = Visibility.Visible;
                cmbTerrains.Visibility = Visibility.Visible;

                txtMonsterName.Text = _monster.MonsterName;
                try
                { 
                    List<Terrain> monsterTerrains = new List<Terrain>();
                    monsterTerrains = _monsterManager.RetreiveTerrainsByMonsterID(_monster.MonsterID);
                    datTerrains.ItemsSource = monsterTerrains;
                    datTerrains.Columns.RemoveAt(0);
                    List<Terrain> selectableTerrains = new List<Terrain>();
                    foreach (Terrain terrain in _terrains)
                    {
                        bool notAssigned = true;
                        foreach (Terrain t in monsterTerrains)
                        {
                            if(terrain.TerrainID == t.TerrainID)
                            {
                                notAssigned = false;
                            }
                        }
                        if (notAssigned)
                        {
                            selectableTerrains.Add(terrain);
                        }
                    }

                    foreach (var terrain in selectableTerrains)
                    {
                        cmbTerrains.Items.Add(terrain.TerrainName);
                    }
                    cmbTerrains.SelectedIndex = 0;
                }
                catch (ArgumentOutOfRangeException)
                { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void btnAddTerrain_Click(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                Terrain terrain = new Terrain();
                if (txtTerrainName.Text == "")
                {
                    MessageBox.Show("You must enter a terrain name.");
                    txtTerrainName.Focus();
                    return;
                }
                terrain.TerrainName = txtTerrainName.Text;
                if (_monsterManager.AddTerrain(terrain))
                {
                    this.DialogResult = true;
                }
            }
            else if (_assign)
            {
                int monster_id = _monster.MonsterID;
                int terrain_id = 0;
                string terrainname = cmbTerrains.Text;
                foreach (var terrain in _terrains)
                {
                    if (terrain.TerrainName == terrainname)
                    {
                        terrain_id = terrain.TerrainID;
                    }
                }
                if (_monsterManager.AssignTerrain(monster_id, terrain_id))
                {
                    this.DialogResult = true;
                }
            }
        }

        private void btnCancelTerrain_Click(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                var result = MessageBox.Show("Continuing will cancel the addition of a terrain record.",
                                             "Cancel Addition?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
            else if (_assign)
            {
                var result = MessageBox.Show("Continuing will cancel the assignment of a terrain.",
                                             "Cancel Assignment?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
        }
    }
}
