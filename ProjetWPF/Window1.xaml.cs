using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Drawing;
using System.IO;
using Image = System.Drawing.Image;
using Telerik.Windows.Controls;

namespace ProjetWPF
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ClassOperation cf = new ClassOperation();
        DataClasses1DataContext dc = new DataClasses1DataContext();
        String res;
        int id;

        public Window1()
        {
            InitializeComponent();

            foreach(Filiere f in cf.GetFilieres()) {
                combo1.Items.Add(f.Nom_filiere);
            }
            combo1.SelectedIndex = 0;
            initialiser();

        }

        private void initialiser()
        {
            String file = Convert.ToString(combo1.SelectedItem);
            labfil.Content = file;
            titre.Content = file;
            res = null;
            id = 0;
            foreach (Filiere f in cf.GetFilieres())
            {
                if (f.Nom_filiere.Equals(file))
                {
                    id = f.Id_filiere;
                    foreach(Responsable r in cf.GetResponsables())
                    {
                        if (id == r.filière)
                        {
                            res = r.prenom + " " + r.nom;
                        }
                    }

                    
                } 
            }

            labres.Content = "Responsable : "+res;

            var x = from c in dc.etudiant
                    join d in dc.Filiere on
                    c.id_fil equals d.Id_filiere
                    where c.id_fil == id
                    select new { c.cne, c.nom, c.prenom, c.date_naiss, Photo = Picture(c.image) };

            radgrid1.ItemsSource = x;
            radcarousel1.ItemsSource = cf.GetFilieres();

            
            
        }

        private Image Picture(String path)
        {
            if (path != null)
            {
                return Image.FromFile(path);
            }
            else
            {
                return null;
            }
        }

        private void RadMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            initialiser();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            
                /*String str = Convert.ToString(radgrid1.SelectedItem);
                String[] sp = str.Split(' ',',');
                sp[3];*/
                this.Hide();
                Window2 w = new Window2();
                w.Show();
            
        }

   

      

        private void Radcarousel1_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            Filiere f = radcarousel1.CurrentItem as Filiere;
            tf1.Text = Convert.ToString(f.Id_filiere);
            tf2.Text = f.Nom_filiere;
        }
    }




    public class FiliereCount

    {

        public int nbEtudiant { get; set; }

        public string Filiere { get; set; }

    }



    public class UsersViewModel

    {
        static DataClasses1DataContext cl = new DataClasses1DataContext();

        public ObservableCollection<FiliereCount> ListF

        {

            get; set;

        }

        public UsersViewModel()

        {

            this.ListF = new ObservableCollection<FiliereCount>();

            var filCount = from etudiant in cl.etudiant
                           join filiere in cl.Filiere
                           on etudiant.id_fil equals filiere.Id_filiere
                           group new { etudiant, filiere } by filiere.Nom_filiere
                      into grouping
                           select new
                           {
                               grouping.Key,
                               nbEtudiant = grouping.Count()

                           };
            foreach (var gr in filCount)
            {
                ListF.Add(new FiliereCount { Filiere = gr.Key, nbEtudiant = gr.nbEtudiant });

            }

        }


    }
}
