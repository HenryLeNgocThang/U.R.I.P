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

namespace U.R.I.P
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /* Preselect "Bitte Wählen" */
            RolleButton.IsSelected = true;
        }

        /* Elemente die ein- und ausgeblendet werden sollen je nachdem auf was man drückt */
        private void Button_Ansicht(object sender, RoutedEventArgs e)
        {
            Ansicht.Visibility = Visibility.Visible;
            Hinzufuegen.Visibility = Visibility.Hidden;
        }

        private void Button_Hinzufuegen(object sender, RoutedEventArgs e)
        {
            Hinzufuegen.Visibility = Visibility.Visible;
            Ansicht.Visibility = Visibility.Hidden;
        }

        private void ComboBoxItem_Angestellter(object sender, RoutedEventArgs e)
        {
            Angestellter.Visibility = Visibility.Visible;
            HinzufuegenGrid.Visibility = Visibility.Visible;
            Lehrer.Visibility = Visibility.Hidden;
            Schueler.Visibility = Visibility.Hidden;
        }

        private void ComboBoxItem_Lehrer(object sender, RoutedEventArgs e)
        {
            Lehrer.Visibility = Visibility.Visible;
            HinzufuegenGrid.Visibility = Visibility.Visible;
            Angestellter.Visibility = Visibility.Hidden;
            Schueler.Visibility = Visibility.Hidden;
        }

        private void ComboBoxItem_Schueler(object sender, RoutedEventArgs e)
        {
            Schueler.Visibility = Visibility.Visible;
            HinzufuegenGrid.Visibility = Visibility.Visible;
            Angestellter.Visibility = Visibility.Hidden;
            Lehrer.Visibility = Visibility.Hidden;
        }

        private void ComboBoxItem_RolleButton(object sender, RoutedEventArgs e)
        {
            Angestellter.Visibility = Visibility.Hidden;
            HinzufuegenGrid.Visibility = Visibility.Hidden;
            Lehrer.Visibility = Visibility.Hidden;
            Schueler.Visibility = Visibility.Hidden;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Schueler> items = new List<Schueler>();
            items.Add(new Schueler()
            {
                Nr = 42,
                Name = SchuelerName.Text,
                Mail = SchuelerEMail.Text
            });

            lvUsers.ItemsSource = items;

            MessageBox.Show("Neuer Schüler wurde erfolgreich angelegt!");
        }
    }
}