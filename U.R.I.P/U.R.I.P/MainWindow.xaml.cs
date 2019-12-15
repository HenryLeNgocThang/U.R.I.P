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
        List<Angestellter> angestellterList = new List<Angestellter>();
        List<Lehrer> lehrerList = new List<Lehrer>();
        List<Schueler> schuelerList = new List<Schueler>();

        int angestellterID = 1000;
        int lehrerID = 3000;
        int schuelerID = 5000;

        public MainWindow()
        {
            InitializeComponent();

            /* Preselect "Bitte Wählen" */
            NutzerButton.IsSelected = true;
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

        private void ComboBoxItem_NutzerButton(object sender, RoutedEventArgs e)
        {
            Angestellter.Visibility = Visibility.Hidden;
            HinzufuegenGrid.Visibility = Visibility.Hidden;
            Lehrer.Visibility = Visibility.Hidden;
            Schueler.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AngestellterListe.Visibility = Visibility.Visible;
            LehrerListe.Visibility = Visibility.Hidden;
            SchuelerListe.Visibility = Visibility.Hidden;

            /* Lade die Listen Inhalte in die ListView */
            AngestellterListe.ItemsSource = angestellterList;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LehrerListe.Visibility = Visibility.Visible;
            AngestellterListe.Visibility = Visibility.Hidden;
            SchuelerListe.Visibility = Visibility.Hidden;

            /* Lade die Listen Inhalte in die ListView */
            LehrerListe.ItemsSource = lehrerList;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SchuelerListe.Visibility = Visibility.Visible;
            AngestellterListe.Visibility = Visibility.Hidden;
            LehrerListe.Visibility = Visibility.Hidden;

            /* Lade die Listen Inhalte in die ListView */
            SchuelerListe.ItemsSource = schuelerList;
        }

        /* Knopf zum hinzufügen des Objekts */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Angestellter.Visibility == Visibility.Visible)
            {
                angestellterList.Add(
                    new Angestellter()
                    {
                        Nr = angestellterID,
                        Name = AngestellterName.Text,
                        Aufgabe = AngestellterAufgabe.Text
                    }
                );
                angestellterID++;

                MessageBox.Show("Neuer Angestellter wurde erfolgreich angelegt!");
            }
            else if (Lehrer.Visibility == Visibility.Visible)
            {
                /* Speichert alle Items in der Combobox */
                ItemCollection comboitems = LehrerFach.Items;
                List<string> checkedItems = new List<string>();
                
                /* Für jede CheckBox die gecheckt ist wird zur List checkedItems hinzugefügt */
                foreach (CheckBox item in comboitems)
                {
                    if (item.IsChecked == true)
                    {
                        checkedItems.Add(item.Content.ToString());
                    }
                }

                lehrerList.Add(
                    new Lehrer()
                    {
                        Nr = lehrerID,
                        Name = LehrerName.Text,
                        /* Verbindet alle strings in checkedItems zusammen und trennt sie mit einem Komma */
                        Fach = String.Join(", ", checkedItems)
                    }
                );
                lehrerID++;

                MessageBox.Show("Neuer Lehrer wurde erfolgreich angelegt!");
            }
            else if (Schueler.Visibility == Visibility.Visible)
            {
                schuelerList.Add(
                    new Schueler()
                    {
                        Nr = schuelerID,
                        Name = SchuelerName.Text,
                        Mail = SchuelerEMail.Text
                    }
                );
                schuelerID++;

                MessageBox.Show("Neuer Schüler wurde erfolgreich angelegt!");
            }
        }

        /* Knopf zum Löschen der Datensätze */
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (Angestellter angestellter in AngestellterListe.SelectedItems)
            {
                angestellterList.Remove(angestellter);
            }

            AngestellterListe.Items.Refresh();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Lehrer lehrer in LehrerListe.SelectedItems)
            {
                lehrerList.Remove(lehrer);
            }

            LehrerListe.Items.Refresh();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (Schueler schueler in SchuelerListe.SelectedItems)
            {
                schuelerList.Remove(schueler);
            }

            SchuelerListe.Items.Refresh();
        }
    }
}