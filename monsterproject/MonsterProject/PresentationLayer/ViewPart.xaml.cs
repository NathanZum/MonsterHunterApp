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
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using System.Windows.Media.Media3D;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewPart.xaml
    /// </summary>
    public partial class ViewPart : Window
    {
        Part _part;
        MonsterManager _monsterManager;
        List<MonsterVM> _monsters;
        bool _add = false;
        bool _update = false;

        public ViewPart(Part part, MonsterManager monsterManager, bool add, bool update)
        {
            _part = part;
            _monsterManager = monsterManager;
            _add = add;
            _update = update;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                lblAddPart.Content = "Add Monster Part";
                lblMonster.Visibility = Visibility.Visible;
                cmbMonster.Visibility = Visibility.Visible;
                btnAddPart.Content = "Add Part";
                _monsters = new List<MonsterVM>();
                try
                {
                    _monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                    foreach (var monster in _monsters)
                    {
                        cmbMonster.Items.Add(monster.MonsterName);
                    }
                    cmbMonster.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (_update)
            {
                lblAddPart.Content = "Update Monster Part";
                lblMonster.Visibility = Visibility.Hidden;
                cmbMonster.Visibility = Visibility.Hidden;
                btnAddPart.Content = "Update Part";
                txtPartName.Text = _part.PartName;
                txtFire.Text = _part.Fire.ToString();
                txtWater.Text = _part.Water.ToString();
                txtThunder.Text = _part.Thunder.ToString();
                txtIce.Text = _part.Ice.ToString();
                txtDragon.Text = _part.Dragon.ToString();
                txtCut.Text = _part.Cut.ToString();
                txtBlunt.Text = _part.Blunt.ToString();
                txtAmmo.Text = _part.Ammo.ToString();
            }
        }

        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {

            if (txtPartName.Text == "")
            {
                MessageBox.Show("You must enter a part name.");
                txtPartName.Focus();
                return;
            }
            int fire;
            try
            {
                fire = int.Parse(txtFire.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtFire.SelectAll();
                txtFire.Focus();
                return;
            }
            if (fire < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtFire.SelectAll();
                txtFire.Focus();
                return;
            }
            int water;
            try
            {
                water = int.Parse(txtWater.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtWater.SelectAll();
                txtWater.Focus();
                return;
            }
            if (water < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtWater.SelectAll();
                txtWater.Focus();
                return;
            }
            int thunder;
            try
            {
                thunder = int.Parse(txtThunder.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtThunder.SelectAll();
                txtThunder.Focus();
                return;
            }
            if (thunder < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtThunder.SelectAll();
                txtThunder.Focus();
                return;
            }
            int ice;
            try
            {
                ice = int.Parse(txtIce.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtIce.SelectAll();
                txtIce.Focus();
                return;
            }
            if (ice < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtIce.SelectAll();
                txtIce.Focus();
                return;
            }
            int dragon;
            try
            {
                dragon = int.Parse(txtDragon.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtDragon.SelectAll();
                txtDragon.Focus();
                return;
            }
            if (dragon < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtDragon.SelectAll();
                txtDragon.Focus();
                return;
            }
            int cut;
            try
            {
                cut = int.Parse(txtCut.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtCut.SelectAll();
                txtCut.Focus();
                return;
            }
            if (cut < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtCut.SelectAll();
                txtCut.Focus();
                return;
            }
            int blunt;
            try
            {
                blunt = int.Parse(txtBlunt.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtBlunt.SelectAll();
                txtBlunt.Focus();
                return;
            }
            if (blunt < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtBlunt.SelectAll();
                txtBlunt.Focus();
                return;
            }
            int ammo;
            try
            {
                ammo = int.Parse(txtAmmo.Text.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("You must enter a valid number.");
                txtAmmo.SelectAll();
                txtAmmo.Focus();
                return;
            }
            if (ammo < 0)
            {
                MessageBox.Show("Number cannot be less than 0.");
                txtAmmo.SelectAll();
                txtAmmo.Focus();
                return;
            }

            Part newPart = new Part();
            newPart.PartName = txtPartName.Text;
            newPart.Fire = fire;
            newPart.Water = water;
            newPart.Thunder = thunder;
            newPart.Ice = ice;
            newPart.Dragon = dragon;
            newPart.Cut = cut;
            newPart.Blunt = blunt;
            newPart.Ammo = ammo;

            if (_add)
            {
                string monstername = cmbMonster.Text;
                foreach (var monster in _monsters)
                {
                    if (monster.MonsterName == monstername)
                    {
                        newPart.MonsterID = monster.MonsterID;
                    }
                }
                if (_monsterManager.AddPart(newPart))
                {
                    this.DialogResult = true;
                }

            }
            else if (_update)
            {
                if (_monsterManager.EditPart(_part, newPart))
                {
                    this.DialogResult = true;
                }
            }

        }

        private void btnAddPartCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_add)
            {
                var result = MessageBox.Show("Continuing will cancel the addition of a part record.",
                                             "Cancel Addition?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
            else if (_update)
            {
                var result = MessageBox.Show("Continuing will cancel the update of a part record.",
                                             "Cancel Update?", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
        }
    }
}
