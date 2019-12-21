using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace U.R.I.P
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* Deklarationen */
        List<Angestellter> angestellterList = new List<Angestellter>();
        List<Lehrer> lehrerList = new List<Lehrer>();
        List<Schueler> schuelerList = new List<Schueler>();
        ListViewItem listViewItem;

        int angestellterID = 1000;
        int lehrerID = 1000;
        int schuelerID = 1000;
        // Boolean um zu prüfen ob ein Datensatz gerade bearbeitet wird.
        bool bearbeitung = false;

        public MainWindow()
        {
            InitializeComponent();

            // Preselect "Bitte Wählen".
            NutzerButton.IsSelected = true;
        }

        /// Elemente die ein- und ausgeblendet werden sollen je nachdem auf was man drückt.
        // Wechseln zu Listen Ansicht.
        private void Button_Ansicht(object sender, RoutedEventArgs e)
        {
            Ansicht.Visibility = Visibility.Visible;
            Hinzufuegen.Visibility = Visibility.Hidden;
            // Beim wechseln zur Listen Ansicht müssen die Listen neu geladen werden.
            AngestellterListe.Items.Refresh();
            LehrerListe.Items.Refresh();
            SchuelerListe.Items.Refresh();
        }

        // Wechseln zum Erstellen Ansicht.
        private void Button_Hinzufuegen(object sender, RoutedEventArgs e)
        {
            if (bearbeitung)
            {
                MessageBox.Show("Bitte schließe erst deine Bearbeitung ab!");
            }
            else
            {
                Hinzufuegen.Visibility = Visibility.Visible;
                Ansicht.Visibility = Visibility.Hidden;
            }
        }

        // Zeigt das Angestellten/Lehrer/Schüler Formular an und blendet die anderen Schüler/Angestellter/Lehrer aus.
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

        // Wenn im "Nutzer Typ wählen" nichts ausgewählt ist oder "Bitte wählen" dann blende alles andere aus.
        private void ComboBoxItem_NutzerButton(object sender, RoutedEventArgs e)
        {
            Angestellter.Visibility = Visibility.Hidden;
            HinzufuegenGrid.Visibility = Visibility.Hidden;
            Lehrer.Visibility = Visibility.Hidden;
            Schueler.Visibility = Visibility.Hidden;
        }

        /// Blende die jeweiligen Nutzertypen Listen ein/aus.
        // Für Angestellte.
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AngestellterListe.Visibility = Visibility.Visible;
            LehrerListe.Visibility = Visibility.Hidden;
            SchuelerListe.Visibility = Visibility.Hidden;

            // Lade die Listen Inhalte in die ListView.
            AngestellterListe.ItemsSource = angestellterList;
        }

        // Für Lehrer.
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LehrerListe.Visibility = Visibility.Visible;
            AngestellterListe.Visibility = Visibility.Hidden;
            SchuelerListe.Visibility = Visibility.Hidden;

            // Lade die Listen Inhalte in die ListView.
            LehrerListe.ItemsSource = lehrerList;
        }

        // Für Schüler.
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SchuelerListe.Visibility = Visibility.Visible;
            AngestellterListe.Visibility = Visibility.Hidden;
            LehrerListe.Visibility = Visibility.Hidden;

            // Lade die Listen Inhalte in die ListView.
            SchuelerListe.ItemsSource = schuelerList;
        }

        // Knopf zum hinzufügen des Objekts (Hinzufügen Button).
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Angestellter.Visibility == Visibility.Visible)
            {
                if (AngestellterName.Text != "" && AngestellterAufgabe.Text != "")
                {
                    // Fügt ein neues Objekt hinzu mit dem Inhalt der TextBoxen.
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
                else
                {
                    MessageBox.Show("Bitte überprüfe deine Angaben!");
                }
            }
            else if (Lehrer.Visibility == Visibility.Visible)
            {
                // Zum speichern aller Items in der Combobox.
                ItemCollection comboitems = LehrerFach.Items;
                List<string> checkedItems = new List<string>();

                // Für jede CheckBox die gecheckt ist wird zur List checkedItems hinzugefügt.
                foreach (CheckBox item in comboitems)
                {
                    if (item.IsChecked == true)
                    {
                        checkedItems.Add(item.Content.ToString());
                    }
                }

                if (LehrerName.Text != "" && checkedItems.Count != 0)
                {
                    lehrerList.Add(
                        new Lehrer()
                        {
                            Nr = lehrerID,
                            Name = LehrerName.Text,
                            // Verbindet alle strings in checkedItems zusammen und trennt sie mit einem Komma
                            Fach = String.Join(", ", checkedItems)
                        }
                    );
                    lehrerID++;

                    MessageBox.Show("Neuer Lehrer wurde erfolgreich angelegt!");
                }
                else
                {
                    MessageBox.Show("Bitte überprüfe deine Angaben!");
                }
            }
            else if (Schueler.Visibility == Visibility.Visible)
            {
                if (SchuelerName.Text != "" && SchuelerEMail.Text != "")
                {
                    // Prüft mit einem Methodenaufruf ob die E-Mail valide ist.
                    if (EMailIstValide(SchuelerEMail.Text))
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
                    else
                    {
                        MessageBox.Show("Bitte überprüfe deine E-Mail!");
                    }
                }
                else
                {
                    MessageBox.Show("Bitte überprüfe deine Angaben!");
                }
            }
        }

        // Knopf zum Löschen der Datensätze
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Entfernt jeden ausgewählten Steuerelement (Datensatz) aus der List.
            foreach (Angestellter angestellter in AngestellterListe.SelectedItems)
            {
                angestellterList.Remove(angestellter);
            }

            // Setzt bearbeiten auf false, falls das Element das gelöscht wurde in Bearbeitung war, ansonsten kann man nicht mehr bearbeiten.
            if (bearbeitung)
            {
                bearbeitung = false;
            }

            AngestellterListe.Items.Refresh();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Lehrer lehrer in LehrerListe.SelectedItems)
            {
                lehrerList.Remove(lehrer);
            }

            if (bearbeitung)
            {
                bearbeitung = false;
            }

            LehrerListe.Items.Refresh();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (Schueler schueler in SchuelerListe.SelectedItems)
            {
                schuelerList.Remove(schueler);
            }

            if (bearbeitung)
            {
                bearbeitung = false;
            }

            SchuelerListe.Items.Refresh();
        }

        /// Beim drücken auf "Bearbeiten" der jeweiligen Datensätze ändert sich das Styling und die TextBox wird bearbeitbar.
        // Für Angestellte
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            if (AngestellterListe.SelectedItem != null && !bearbeitung)
            {
                listViewItem = (ListViewItem)AngestellterListe.ItemContainerGenerator.ContainerFromItem(AngestellterListe.SelectedItem);

                // Für jede TextBox innerhalb vom ListView x:Name="AngestellterListe"
                foreach (TextBox textBox in VisualChildFinden<TextBox>(listViewItem))
                {
                    textBox.Background = Brushes.White;
                    textBox.BorderThickness = new Thickness(1);
                    textBox.IsReadOnly = false;
                    // TextBox wird über GridView gelayert. TextBox hat keine Response mehr für Maus, dadurch kann man dort rechtsklicken.
                    textBox.IsHitTestVisible = true;
                }

                bearbeitung = true;
            }
        }

        // Für Lehrer
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (LehrerListe.SelectedItem != null && !bearbeitung)
            {
                listViewItem = (ListViewItem)LehrerListe.ItemContainerGenerator.ContainerFromItem(LehrerListe.SelectedItem);

                foreach (TextBox textBox in VisualChildFinden<TextBox>(listViewItem))
                {
                    textBox.Background = Brushes.White;
                    textBox.BorderThickness = new Thickness(1);
                    textBox.IsReadOnly = false;
                    textBox.IsHitTestVisible = true;
                }

                bearbeitung = true;
            }
        }

        // Für Schüler
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (SchuelerListe.SelectedItem != null && !bearbeitung)
            {
                listViewItem = (ListViewItem)SchuelerListe.ItemContainerGenerator.ContainerFromItem(SchuelerListe.SelectedItem);

                foreach (TextBox textBox in VisualChildFinden<TextBox>(listViewItem))
                {
                    textBox.Background = Brushes.White;
                    textBox.BorderThickness = new Thickness(1);
                    textBox.IsReadOnly = false;
                    textBox.IsHitTestVisible = true;
                }

                bearbeitung = true;
            }
        }
        
        /// Führt Logik aus wenn in einer der ausgewählten TextBoxen Enter gedrückt wird.
        // Für Angestellte
        private void AngestellterTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                // Wird verwendet um Text von TextBoxen zu speichern und dann in der Liste des jeweiligen Indexes zu überschreiben.
                int i = 0;
                string[] neueWerte = new string[2] { "", "" };

                foreach (TextBox textBox in VisualChildFinden<TextBox>(listViewItem))
                {
                    textBox.Background = Brushes.Transparent;
                    textBox.BorderThickness = new Thickness(0);
                    textBox.IsReadOnly = true;
                    textBox.IsHitTestVisible = false;

                    neueWerte[i] = textBox.Text;
                    i++;
                }

                angestellterList[AngestellterListe.SelectedIndex].Name = neueWerte[0];
                angestellterList[AngestellterListe.SelectedIndex].Aufgabe = neueWerte[1];
                AngestellterListe.Items.Refresh();

                bearbeitung = false;
            }
        }

        // Für Lehrer
        private void LehrerTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                int i = 0;
                string[] neueWerte = new string[2] { "", "" };

                foreach (TextBox textBox in VisualChildFinden<TextBox>(listViewItem))
                {
                    textBox.Background = Brushes.Transparent;
                    textBox.BorderThickness = new Thickness(0);
                    textBox.IsReadOnly = true;
                    textBox.IsHitTestVisible = false;

                    neueWerte[i] = textBox.Text;
                    i++;
                }

                lehrerList[LehrerListe.SelectedIndex].Name = neueWerte[0];
                lehrerList[LehrerListe.SelectedIndex].Fach = neueWerte[1];
                LehrerListe.Items.Refresh();

                bearbeitung = false;
            }
        }

        // Für Schüler
        private void SchuelerTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                int i = 0;
                string[] neueWerte = new string[2] { "", "" };

                foreach (TextBox textBox in VisualChildFinden<TextBox>(listViewItem))
                {
                    textBox.Background = Brushes.Transparent;
                    textBox.BorderThickness = new Thickness(0);
                    textBox.IsReadOnly = true;
                    textBox.IsHitTestVisible = false;

                    neueWerte[i] = textBox.Text;
                    i++;
                }

                schuelerList[SchuelerListe.SelectedIndex].Name = neueWerte[0];

                if (EMailIstValide(neueWerte[1]))
                {
                    schuelerList[SchuelerListe.SelectedIndex].Mail = neueWerte[1];
                }
                else
                {
                    MessageBox.Show("Bitte überprüfe deine E-Mail!");
                }

                SchuelerListe.Items.Refresh();

                bearbeitung = false;
            }
        }

        /* Private Methoden*/
        /// <summary>
        /// Sucht nach dem Steuerelementen innerhalb des System.Windows.Controls und gibt diese Zurück.
        /// </summary>
        /// <typeparam name="T">Für T kann ein beliebiges Steuerelement angegeben werden.</typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        static IEnumerable<T> VisualChildFinden<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    // Wenn das child dem angegebenen Steuerelement des Parameters entspricht soll er das zurückgegben.
                    if (child != null && child is T)
                    {
                        // yield return nutzen um jedes Element einzeln zurückzugeben.
                        yield return (T)child;
                    }

                    // Rekursion
                    foreach (T childOfChild in VisualChildFinden<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// Methode zum prüfen bestimmter Zeichen für eine E-Mail.
        /// </summary>
        /// <returns></returns>
        bool EMailIstValide(string email)
        {
            // Regex für die Filterkriterien in der Textsuche
            string regexPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            try
            {
                if (Regex.IsMatch(email, regexPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}