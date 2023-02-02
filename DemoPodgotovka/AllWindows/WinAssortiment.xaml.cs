using DemoPodgotovka.DataBaseGlazki;
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

namespace DemoPodgotovka.AllWindows
{
    /// <summary>
    /// Логика взаимодействия для WinAssortiment.xaml
    /// </summary>
    public partial class WinAssortiment : Window
    {
        List<Agent> AgentList = new List<Agent>();
        public WinAssortiment()
        {
            InitializeComponent();
            LstGlaz.ItemsSource = App.agentsDB.Agent.ToList();
            FellCmbBox();
            felcmbBox();
            
        }
        private void felcmbBox()
        {
            cmbSort.Items.Add("Наименование от А до Я");
            cmbSort.Items.Add("Наименование от Я до А");
            cmbSort.Items.Add("Приоритет по убыванию");
            cmbSort.Items.Add("Приоритет по возрастанию");
        }
        private void FellCmbBox()
        {
            cmbFilter.ItemsSource = App.agentsDB.AgentType.ToList();
        }

        private void txtPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPoisk.Text.Trim() == "")
            {
                AgentList = App.agentsDB.Agent.ToList();
                cmbFilter_SelectionChanged(null, null);
                LstGlaz.ItemsSource = AgentList;
            }
            else
            {
                cmbFilter_SelectionChanged(null, null);
                LstGlaz.ItemsSource = AgentList.Where(x => x.Title.ToLower().Contains(txtPoisk.Text.ToLower())).ToList();
            }
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
            if (cmbFilter.SelectedIndex == -1)
                return;
            var ch = (AgentType)cmbFilter.SelectedItem;
            List<Agent> lst = App.agentsDB.Agent.ToList();
            AgentList = lst.Where(x => x.AgentType.Title.ToString() == ch.Title.ToString()).ToList();
            LstGlaz.ItemsSource = AgentList;
        }

        private void btnADDagent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Успешно!", "Переход на другую страницу!", MessageBoxButton.OK, MessageBoxImage.Information);
            WinAddGlaz WinAddglaz = new WinAddGlaz();
            WinAddglaz.AddAgent += () => { LstGlaz.ItemsSource = App.agentsDB.Agent.ToList(); };
            WinAddglaz.ShowDialog();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtPoisk.Clear();
            cmbFilter.SelectedIndex = -1;
            cmbSort.SelectedIndex = -1;
            LstGlaz.ItemsSource = App.agentsDB.Agent.ToList();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbSort.SelectedItem)
            {
                case "Наименование от А до Я":
                    LstGlaz.ItemsSource = null;
                    LstGlaz.ItemsSource = App.agentsDB.Agent.OrderBy(x => x.Title).ToList();
                    break;
                case "Наименование от Я до А":
                    LstGlaz.ItemsSource = null;
                    LstGlaz.ItemsSource = App.agentsDB.Agent.OrderByDescending(x => x.Title).ToList();
                    break;
                case "Приоритет по убыванию":
                    LstGlaz.ItemsSource = null;
                    LstGlaz.ItemsSource = App.agentsDB.Agent.OrderByDescending(x => x.Priority).ToList();
                    break;
                case "Приоритет по возрастанию":
                    LstGlaz.ItemsSource = null;
                    LstGlaz.ItemsSource = App.agentsDB.Agent.OrderBy(x => x.Priority).ToList();
                    break;
                    default:
                    LstGlaz.ItemsSource = App.agentsDB.Agent.ToList();
                    break;

            }
        }
    }
}
