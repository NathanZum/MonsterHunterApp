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
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MaterialView.xaml
    /// </summary>
    public partial class MaterialView : Window
    {
        private MaterialVM _material = null;
        private WPFUser _user = null;
        private MonsterManager _monsterManager = null;
        private bool _add = false;
        private bool _update = false;
        private List<MonsterVM> monsters;
        public MaterialView(WPFUser user, MaterialVM material, MonsterManager monsterManager, bool add, bool update)
        {
            _user = user;
            _material = material;
            _monsterManager = monsterManager;
            _add = add;
            _update = update;
            InitializeComponent();
        }

        private void btnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                Material newMaterial = new Material();
                if (txtMaterialName.Text.ToString() == "")
                {
                    MessageBox.Show("You must enter a monster name.");
                    txtMaterialName.Focus();
                    return;
                }
                int price;
                try
                {
                    price = int.Parse(txtPrice.Text.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("You must enter a valid price.");
                    txtPrice.SelectAll();
                    txtPrice.Focus();
                    return;
                }
                if (price < 0)
                {
                    MessageBox.Show("Price cannot be less than 0.");
                    txtPrice.SelectAll();
                    txtPrice.Focus();
                    return;
                }
                newMaterial.MaterialName = txtMaterialName.Text;
                newMaterial.Price = price;
                string monstername = cmbMatMonsterName.Text;
                foreach(var monster in monsters)
                {
                    if (monster.MonsterName == monstername)
                    {
                        newMaterial.MonsterID = monster.MonsterID;
                    }
                }
                if (_monsterManager.AddMaterial(newMaterial))
                {
                    this.DialogResult = true;
                }
            }
            else if (_update)
            {
                Material newMaterial = new Material();
                if (txtMaterialName.Text.ToString() == "")
                {
                    MessageBox.Show("You must enter a material name.");
                    txtMaterialName.Focus();
                    return;
                }
                int price;
                try
                {
                    price = int.Parse(txtPrice.Text.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("You must enter a valid price.");
                    txtPrice.SelectAll();
                    txtPrice.Focus();
                    return;
                }
                if (price < 0)
                {
                    MessageBox.Show("Price cannot be less than 0.");
                    txtPrice.SelectAll();
                    txtPrice.Focus();
                    return;
                }
                newMaterial.MaterialName = txtMaterialName.Text;
                newMaterial.Price = price;
                string monstername = cmbMatMonsterName.Text;
                foreach (var monster in monsters)
                {
                    if (monster.MonsterName == monstername)
                    {
                        newMaterial.MonsterID = monster.MonsterID;
                    }
                }
                if (_monsterManager.EditMaterial(_material, newMaterial))
                {
                    this.DialogResult = true;
                }
            }
        }

        private void btnCancelMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                var result = MessageBox.Show("Continuing will cancel the addition of a material record.",
                                             "Cancel Addition?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
            else if (_update)
            {
                var result = MessageBox.Show("Continuing will cancel the update of a material record.",
                                             "Cancel Update?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
        }

        private void MaterialView_Loaded(object sender, RoutedEventArgs e)
        {
            txtMaterialName.Text = _material.MaterialName;
            txtViewMatMonsterName.Text = _material.MonsterName;
            txtPrice.Text = _material.Price.ToString();
            datDropRates.ItemsSource = _material.DropRates;
            datPartDropRates.ItemsSource = _material.PartDropRates;
            if (_add == false && _update == false)
            {
                btnAddMaterial.Visibility = Visibility.Hidden;
                btnCancelMaterial.Visibility = Visibility.Hidden;
                lblAddMatMonsterName.Visibility = Visibility.Hidden;
                cmbMatMonsterName.Visibility = Visibility.Hidden;
                datDropRates.Columns.RemoveAt(0);
                datDropRates.Columns.RemoveAt(0);
                datPartDropRates.Columns.RemoveAt(0);
                datPartDropRates.Columns.RemoveAt(0);
            }
            else if (_add || _update)
            {
                if (_add)
                {
                    lblMaterialView.Content = "Add Material";
                }
                else
                {
                    lblMaterialView.Content = "Update Material";
                    btnAddMaterial.Content = "Update";
                }
                lblPartDropRates.Visibility = Visibility.Hidden;
                datPartDropRates.Visibility = Visibility.Hidden;
                txtMaterialName.IsReadOnly = false;
                txtPrice.IsReadOnly = false;
                lblViewMatMonsterName.Visibility = Visibility.Hidden;
                txtViewMatMonsterName.Visibility = Visibility.Hidden;
                lblRates.Visibility = Visibility.Hidden;
                lblDropRates.Visibility = Visibility.Hidden;
                datDropRates.Visibility = Visibility.Hidden;
                //datDropRates.IsReadOnly = false;
                //datDropRates.Columns[0].IsReadOnly = true;
                monsters = new List<MonsterVM>();
                try
                {
                    monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                    int index = 0;
                    bool foundIndex = false;
                    foreach (var monster in monsters)
                    {
                        cmbMatMonsterName.Items.Add(monster.MonsterName);
                        if (monster.MonsterID == _material.MonsterID)
                        {
                            foundIndex = true;
                        }
                        if (!foundIndex)
                        {
                            index++;
                        }
                    }
                    if (foundIndex)
                    {
                        cmbMatMonsterName.SelectedIndex = index;
                    }
                    else
                    {
                        cmbMatMonsterName.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
