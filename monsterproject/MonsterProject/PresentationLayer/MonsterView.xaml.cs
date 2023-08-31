using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MonsterView.xaml
    /// </summary>
    public partial class MonsterView : Window
    {
        private MonsterVM _monster;
        private Monster _Monster;
        private WPFUser _user;
        private MonsterManager _monsterManager;
        private List<Weakness> newWeaknesses = new List<Weakness>();
        private bool _add = false;
        private bool _update = false;
        public MonsterView(WPFUser user, Monster monster, MonsterManager monsterManager, bool add, bool update)
        {
            _user = user;
            _Monster = monster;
            _monsterManager = monsterManager;
            _add = add;
            _update = update;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _monster = new MonsterVM();
            _monster.MonsterID = _Monster.MonsterID;
            _monster.MonsterType = _Monster.MonsterType;
            _monster.MonsterName = _Monster.MonsterName;
            _monster.UserID = _Monster.UserID;
            _monster.Weaknesses = _Monster.Weaknesses;
            try
            {
                _monster.Terrains = _monsterManager.RetreiveTerrainsByMonsterID(_monster.MonsterID);
                _monster.Parts = _monsterManager.RetreivePartsByMonsterID(_monster.MonsterID);
                _monster.Materials = _monsterManager.RetreiveMaterialsByMonsterID(_monster.MonsterID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            txtMonsterName.Text = _monster.MonsterName;
            txtMonsterType.Text = _monster.MonsterType;
            if (!_add && !_update)
            {
                datWeakness.ItemsSource = _monster.Weaknesses;

                datTerrain.ItemsSource = _monster.Terrains;
                datPart.ItemsSource = _monster.Parts;
                datMaterial.ItemsSource = _monster.Materials;
                datTerrain.Columns.RemoveAt(0);
                datPart.Columns.RemoveAt(0);
                datPart.Columns.RemoveAt(0);
                datMaterial.Columns.RemoveAt(0);
                datMaterial.Columns.RemoveAt(0);
                datMaterial.Columns.RemoveAt(0);
                datMaterial.Columns.RemoveAt(0);
                datMaterial.Columns.RemoveAt(0);
                datMaterial.Columns.RemoveAt(2);
                btnMonAdd.Visibility = Visibility.Hidden;
                btnMonCancel.Visibility = Visibility.Hidden;
            }
            else if (_add)
            {
                datWeakness.ItemsSource = _monster.Weaknesses;
                lblMonsterView.Content = "Add Monster";
                btnMonAdd.Content = "Add Monster";
                lblMaterials.Visibility = Visibility.Hidden;
                lblPart.Visibility = Visibility.Hidden;
                lblTerrain.Visibility = Visibility.Hidden;
                datTerrain.Visibility = Visibility.Hidden;
                datMaterial.Visibility = Visibility.Hidden;
                datPart.Visibility = Visibility.Hidden;
                txtMonsterName.Clear();
                txtMonsterType.Clear();
                
                txtMonsterName.IsReadOnly = false;
                txtMonsterType.IsReadOnly = false;
                datWeakness.IsReadOnly = false;

                // Weakness weakness = (Weakness)datWeakness.SelectedItem;
                datWeakness.Columns[0].IsReadOnly = true;
            }
            else if (_update)
            {
                for (int i = 0; i < _Monster.Weaknesses.Count; i++)
                {
                    Weakness weakness = new Weakness();
                    weakness.Name = _Monster.Weaknesses[i].Name;
                    weakness.Effectiveness = _Monster.Weaknesses[i].Effectiveness;
                    newWeaknesses.Add(weakness);
                }
                datWeakness.ItemsSource = newWeaknesses;
                lblMonsterView.Content = "Update Monster";
                btnMonAdd.Content = "Update Monster";
                lblMaterials.Visibility = Visibility.Hidden;
                lblPart.Visibility = Visibility.Hidden;
                lblTerrain.Visibility = Visibility.Hidden;
                datTerrain.Visibility = Visibility.Hidden;
                datMaterial.Visibility = Visibility.Hidden;
                datPart.Visibility = Visibility.Hidden;

                txtMonsterName.IsReadOnly = false;
                txtMonsterType.IsReadOnly = false;
                datWeakness.IsReadOnly = false;

                datWeakness.Columns[0].IsReadOnly = true;
            }
        }

        private void btnMonAdd_Click(object sender, RoutedEventArgs e)
        {   
            if (_add)
            {
                if (txtMonsterName.Text.ToString() == "")
                {
                    MessageBox.Show("You must enter a monster name.");
                    txtMonsterName.Focus();
                    return;
                }
                if (txtMonsterType.Text.ToString() == "")
                {
                    MessageBox.Show("You must enter a monster type.");
                    txtMonsterType.Focus();
                    return;
                }
                _Monster.MonsterName = txtMonsterName.Text;
                _Monster.MonsterType = txtMonsterType.Text;
                _Monster.UserID = _user.UserID;
                List<Weakness> weaknesses = new List<Weakness>();
                try
                {
                    for (int i = 0; i < _Monster.Weaknesses.Count; i++)
                    {
                        var weakness = (Weakness)datWeakness.Items[i];
                        weakness.Effectiveness = ValidateInteger(weakness.Effectiveness);
                        weaknesses.Add(weakness);
                    }
                    if (_monsterManager.AddMonster(_Monster))
                    {
                        this.DialogResult = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }   
            }
            else if(_update){
                if (txtMonsterName.Text.ToString() == "")
                {
                    MessageBox.Show("You must enter a monster name.");
                    txtMonsterName.Focus();
                    return;
                }
                if (txtMonsterType.Text.ToString() == "")
                {
                    MessageBox.Show("You must enter a monster type.");
                    txtMonsterType.Focus();
                    return;
                }
                var newMonster = new Monster();
                newMonster.MonsterID = _Monster.MonsterID;
                newMonster.MonsterName = txtMonsterName.Text;
                newMonster.MonsterType = txtMonsterType.Text;
                newMonster.UserID = _Monster.UserID;
                List<Weakness> weaknesses = new List<Weakness>();
                try
                {
                    for (int i = 0; i < _Monster.Weaknesses.Count; i++)
                    {
                        var weakness = (Weakness)datWeakness.Items[i];
                        weakness.Effectiveness = ValidateInteger(weakness.Effectiveness);
                        weaknesses.Add(weakness);
                    }
                    newMonster.Weaknesses = weaknesses;
                    if (_monsterManager.EditMonster(_Monster, newMonster))
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            }
        }

        private void btnMonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                var result = MessageBox.Show("Continuing will cancel the addition of a monster record.",
                                             "Cancel Addition?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
            else if (_update)
            {
                var result = MessageBox.Show("Continuing will cancel the update of a monster record.",
                                             "Cancel Update?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
        }

        public int ValidateInteger(int number)
        {
                if (number >= 0 && number <= 5)
                {
                    return number;
                }
                else
                {
                    return 0;
                }
        }
        
    }
}
