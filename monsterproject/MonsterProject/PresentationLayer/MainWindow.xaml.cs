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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WPFUser _user = null;
        private List<MonsterVM> _monsters = null;
        private List<MaterialVM> _materials = null;
        private List<PartVM> _parts = null;
        private MonsterManager _monsterManager = new MonsterManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void hideUserTabs()
        {
            tabTerrain.Visibility = Visibility.Collapsed;
            tabPart.Visibility = Visibility.Collapsed;
        }

        private void updateUIforLogout()
        {
            hideUserTabs();
            btnLogin.IsDefault = true;

            lblGreeting.Content = "You are not logged in.";
            statMessage.Content = "Welcome please browse, or login to add and update.";

            txtUsername.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Visible;
            lblUsername.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            btnChangePassword.Visibility = Visibility.Hidden;
            btnAddMonster.Visibility = Visibility.Hidden;
            btnUpdateMonster.Visibility = Visibility.Hidden;
            btnAddMaterial.Visibility = Visibility.Hidden;
            btnUpdateMaterial.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;

            tabMonster.Focus();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            updateUIforLogout();


        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log Out")
            {
                _user = null;
                updateUIforLogout();
                return;
            }
            UserManager _userManager = new UserManager();
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (username == "")
            {
                MessageBox.Show("You must enter a username");
                txtUsername.Focus();
                return;
            }
            if (password == "")
            {
                MessageBox.Show("You must enter a password");
                txtPassword.Focus();
                return;
            }
            try
            {
                _user = _userManager.LoginUser(username, password);
                if (txtPassword.Password == "newuser")
                {
                    var passwordWindow = new frmUpdatePassword(_user, _userManager, true);
                    if ((bool)passwordWindow.ShowDialog())
                    {
                        MessageBox.Show("Password Updated.");
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Goodbye");
                        _user = null;
                        txtUsername.Clear();
                        txtPassword.Clear();
                        updateUIforLogout();
                        return;
                    }
                }

                showTabsForUser();
                updateUIforUser();
            }
            catch (ArgumentOutOfRangeException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void showTabsForUser()
        {
            foreach (var role in _user.Roles)
            {
                switch (role)
                {
                    case "Admin":
                        break;
                    case "Manager":
                        tabTerrain.Visibility = Visibility.Visible;
                        tabPart.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
        }

        private void updateUIforUser()
        {
            string rolesList = "";

            for (int i = 0; i < _user.Roles.Count; i++)
            {
                rolesList += " " + _user.Roles[i];

                if (i == _user.Roles.Count - 2)
                {
                    if (_user.Roles.Count > 2)
                    {
                        rolesList += ",";
                    }
                    rolesList += " and";
                }
                else if (i < _user.Roles.Count - 2)
                {
                    rolesList += ",";
                }
            }
            lblGreeting.Content = "Welcome, " + _user.UserName + " You are logged in as: " + rolesList + ".";

            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString()
                + ". Please remember to log out before you leave.";

            txtUsername.Text = "";
            txtPassword.Password = "";
            txtUsername.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            lblUsername.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;
            btnChangePassword.Visibility = Visibility.Visible;
            btnAddMonster.Visibility = Visibility.Visible;
            btnUpdateMonster.Visibility = Visibility.Visible;
            btnAddMaterial.Visibility = Visibility.Visible;
            btnUpdateMaterial.Visibility = Visibility.Visible;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                return;
            }

            var _userManager = new UserManager();
            var passwordWindow = new frmUpdatePassword(_user, _userManager);
            // did it work?

            if ((bool)passwordWindow.ShowDialog())
            {
                MessageBox.Show("Password Updated.");
            }
            else
            {
                MessageBox.Show("Update failed. Goodbye");
            }
        }

        /*
        private void tabMonster_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_monsters == null)
            {
                try
                {
                    _monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                    datMonster.ItemsSource = _monsters;
                    
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(2);
                    datMonster.Columns.RemoveAt(2);


                    // datRental.Columns[4].Header = "Cabin Status";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n");
                }
            }
        }
        */

        private void datMonster_Loaded(object sender, RoutedEventArgs e)
        {
            if (_monsters == null)
            {
                try
                {
                    PopulateMonsterDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n");
                }
            }
        }

        private void datMonster_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMonster = (Monster)(datMonster.SelectedItem);
            try
            {
                //selectedMonster.Terrains = _monsterManager.RetriveTerrainsByMonsterID(selectedMonster.MonsterID);
                //selectedMonster.Parts = _monsterManager.RetrivePartsByMonsterID(selectedMonster.MonsterID);
                //selectedMonster.Materials = _monsterManager.RetriveMaterialsByMonsterID(selectedMonster.MonsterID);
                var monsterView = new MonsterView(_user, selectedMonster, _monsterManager, false, false);
                monsterView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void datMaterials_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMaterial = (MaterialVM)datMaterials.SelectedItem;
            try
            {
                selectedMaterial.DropRates = _monsterManager.RetreiveDropRatesByMaterialID(selectedMaterial.MaterialID);
                selectedMaterial.PartDropRates = _monsterManager.RetreivePartDropRatesByMaterialID(selectedMaterial.MaterialID);
                var materialView = new MaterialView(_user, selectedMaterial, _monsterManager, false, false);
                materialView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }


        private void btnAddMonster_Click(object sender, RoutedEventArgs e)
        {
            var selectedMonster = new Monster();
            selectedMonster.Weaknesses = _monsterManager.NewMonsterWeaknesses();
            try
            {
                var monsterView = new MonsterView(_user, selectedMonster, _monsterManager, true, false);
                monsterView.ShowDialog();
                PopulateMonsterDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnUpdateMonster_Click(object sender, RoutedEventArgs e)
        {
            var selectedMonster = (Monster)(datMonster.SelectedItem);
            if (selectedMonster != null) {
                try
                {
                    var monsterView = new MonsterView(_user, selectedMonster, _monsterManager, false, true);
                    monsterView.ShowDialog();
                    PopulateMonsterDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("To update a monster please" + '\n'
                    + "select a monster from the list, then click Update");
            }
        }

        private bool PopulateMaterialDataGrid()
        {
            try
            {
                datMaterials.ItemsSource = null;
                // datMaterials.Columns.Clear();
                _materials = _monsterManager.RetreiveMaterialsByActive(true);
                datMaterials.ItemsSource = _materials;
                if ((String)datMaterials.Columns[0].Header != "MonsterName")
                {
                    datMaterials.Columns.RemoveAt(0);
                    datMaterials.Columns.RemoveAt(0);
                    datMaterials.Columns.RemoveAt(1);
                    datMaterials.Columns.RemoveAt(1);
                    datMaterials.Columns.RemoveAt(2);
                    datMaterials.Columns.RemoveAt(2);
                }
            }
            catch (ArgumentOutOfRangeException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n");
            }
            return true;
        }

        private void PopulateMonsterDataGrid()
        {
            datMonster.ItemsSource = null;
            datMonsterTerrain.ItemsSource = null;
            try
            {
                _monsters = _monsterManager.RetreiveMonsterVMsByActive(true);
                datMonster.ItemsSource = _monsters;
                if ((String) datMonster.Columns[0].Header != "MonsterName")
                {
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(0);
                    datMonster.Columns.RemoveAt(2);
                    datMonster.Columns.RemoveAt(2);
                }
                datMonsterTerrain.ItemsSource = _monsters;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n");
            }
            return;
        }

        private void datMaterials_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateMaterialDataGrid();
            }
            catch (ArgumentOutOfRangeException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n");
            }
        }

        private void btnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            var selectedMaterial = new MaterialVM();
            selectedMaterial.DropRates = _monsterManager.NewMaterialDropRates();
            try
            {
                var materialView = new MaterialView(_user, selectedMaterial, _monsterManager, true, false);
                materialView.ShowDialog();
                PopulateMaterialDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnAddTerrain_Click(object sender, RoutedEventArgs e)
        {
            MonsterVM selectedMonster = new MonsterVM();
            try
            {
                var terrainView = new TerrainView(selectedMonster, _monsterManager, true, false);
                terrainView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAssignTerrain_Click(object sender, RoutedEventArgs e)
        {
            MonsterVM selectedMonster = (MonsterVM) datMonsterTerrain.SelectedItem;
            if (selectedMonster != null)
            {
                try
                {
                    var terrainView = new TerrainView(selectedMonster, _monsterManager, false, true);
                    terrainView.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("To assign a terrain to a monster please" + '\n'
                    + "select a monster from the list, then click Assign");
            }
        }

        private void tabTerrain_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((String) datMonsterTerrain.Columns[0].Header != "MonsterName")
            {
                try
                {

                    datMonsterTerrain.Columns.RemoveAt(0);
                    datMonsterTerrain.Columns.RemoveAt(0);
                    datMonsterTerrain.Columns.RemoveAt(0);
                    datMonsterTerrain.Columns.RemoveAt(0);
                    datMonsterTerrain.Columns.RemoveAt(0);
                    datMonsterTerrain.Columns.RemoveAt(2);
                    datMonsterTerrain.Columns.RemoveAt(2);

                }
                catch (ArgumentOutOfRangeException)
                { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n");
                }
            }
        }

        private void btnUpdateMaterial_Click(object sender, RoutedEventArgs e)
        {
            var selectedMaterial = (MaterialVM)(datMaterials.SelectedItem);
            if (selectedMaterial != null)
            {
                try
                {
                    var materialView = new MaterialView(_user, selectedMaterial, _monsterManager, false, true);
                    materialView.ShowDialog();
                    PopulateMaterialDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("To update a material please" + '\n'
                    + "select a material from the list, then click Update");
            }
        }

        private void datPart_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulatePartDataGrid();
            }
            catch (ArgumentOutOfRangeException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n");
            }
        }

        private void PopulatePartDataGrid()
        {
            datPart.ItemsSource = null;
            try
            {
                _parts = _monsterManager.RetreiveParts();
                datPart.ItemsSource = _parts;
                if ((String)datPart.Columns[1].Header != "PartName")
                {
                    datPart.Columns.RemoveAt(1);
                    datPart.Columns.RemoveAt(1);
                }
                datMonsterTerrain.ItemsSource = _monsters;
            }
            catch (ArgumentOutOfRangeException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n");
            }
            return;
        }

        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {
            PartVM part = new PartVM();
            try
            {
                var partView = new ViewPart(part, _monsterManager, true, false);
                partView.ShowDialog();
                PopulatePartDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdatePart_Click(object sender, RoutedEventArgs e)
        {
            var part = (PartVM) datPart.SelectedItem;
            if (part != null)
            {
                try
                {
                    var partView = new ViewPart(part, _monsterManager, false, true);
                    partView.ShowDialog();
                    PopulatePartDataGrid();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("To update a part please" + '\n'
                    + "select a part from the list, then click Update");
            }
        }
    }
}
