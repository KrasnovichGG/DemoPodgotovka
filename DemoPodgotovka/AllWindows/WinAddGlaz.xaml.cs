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
    /// Логика взаимодействия для WinAddGlaz.xaml
    /// </summary>
    public partial class WinAddGlaz : Window
    {
        public event Action AddAgent;
        public WinAddGlaz()
        {
            InitializeComponent();
            cmbTypeAgent.ItemsSource = App.agentsDB.AgentType.ToList();
        }
        private void ClearBox()
        {
            AgentNameTB.Clear();
            AddressTBagent.Clear();
            INNagent.Clear();
            kppagent.Clear();
            directorAgent.Clear();
            cmbTypeAgent.SelectedItem = -1;
            PhoneAgent.Clear();
            EmailAgent.Clear();
            TbPrioritet.Clear();
        }
        private void INNagent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только 11 цифр!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void kppagent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только 11 цифр!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void TbPrioritet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только 3 цифры!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void PhoneAgent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только 3 цифры!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearBox();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Закрыли");
            Close();
        }

        private void btnAddAgent_Click(object sender, RoutedEventArgs e)
        {
            if (TbPrioritet.Text == "" || AddressTBagent.Text == "" || AgentNameTB.Text == ""
                || INNagent.Text == "" || kppagent.Text == "" || directorAgent.Text == "" || cmbTypeAgent.SelectedIndex == -1
                || PhoneAgent.Text == "" || EmailAgent.Text == "")
            {
                MessageBox.Show("Не оставляйте незаполенные поля, или же информация не сохранится", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (App.agentsDB.Agent.Where(x => x.Title.ToLower() == AgentNameTB.Text.ToLower()).FirstOrDefault() != null)
            {
                MessageBox.Show("Такой агент уже существует!😘", "Да, это проверка на одинаковых агентах", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearBox();
            }
            else
            {
                Agent agent = new Agent();
                agent.Title = AgentNameTB.Text;
                var selectedTypeAgent = cmbTypeAgent.SelectedItem as Agent;
                agent.AgentType = selectedTypeAgent.AgentType;
                agent.KPP = kppagent.Text;
                agent.INN = INNagent.Text;
                agent.Logo = tbImageAgent.Text;
                agent.Phone = PhoneAgent.Text;
                agent.Priority = Convert.ToInt32(TbPrioritet.Text);
                agent.Address = AddressTBagent.Text;
                agent.DirectorName = directorAgent.Text;
                agent.Email = EmailAgent.Text;
                App.agentsDB.Agent.Add(agent);
                App.agentsDB.SaveChanges();
                AddAgent?.Invoke();
                MessageBox.Show("Успешное добавление!", "Агент добавлен", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearBox();
            }
        }
    }
}
