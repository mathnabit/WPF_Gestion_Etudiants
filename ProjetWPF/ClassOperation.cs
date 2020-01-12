using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetWPF
{
    class ClassOperation
    {
        private ObservableCollection<admin> listAdmin;
        public ObservableCollection<admin> GetAdmins() { return listAdmin; }

        private ObservableCollection<Filiere> listFiliere;
        public ObservableCollection<Filiere> GetFilieres() { return listFiliere; }

        private ObservableCollection<etudiant> listEtudiant;
        public ObservableCollection<etudiant> GetEtudiants() { return listEtudiant; }

        private ObservableCollection<Responsable> listRespoonsable;
        public ObservableCollection<Responsable> GetResponsables() { return listRespoonsable; }

        private ObservableCollection<etudiantRad> listEtudiantRad;
        public ObservableCollection<etudiantRad> GetEtudiantsRad() { return listEtudiantRad; }

        DataClasses1DataContext dc;


        public ClassOperation()
        {
            dc= new DataClasses1DataContext();
            listAdmin = new ObservableCollection<admin>(dc.admin.ToList());
            listFiliere = new ObservableCollection<Filiere>(dc.Filiere.ToList());
            listEtudiant = new ObservableCollection<etudiant>(dc.etudiant.ToList());
            listRespoonsable = new ObservableCollection<Responsable>(dc.Responsable.ToList());

            listEtudiantRad = new ObservableCollection<etudiantRad>();

            foreach(etudiant e in this.GetEtudiants())
            {
                etudiantRad er = new etudiantRad();
                er.cne = e.cne;
                er.nom = e.nom;
                er.prenom = e.prenom;
                er.date_naiss = e.date_naiss;
                listEtudiantRad.Add(er);
            }

        }

        internal void deleteEtudiant(int cne)
        {
            var x = (from e in dc.etudiant
                     where e.cne == cne
                     select e).SingleOrDefault();

            this.listEtudiant.Remove(x);
            dc.etudiant.DeleteOnSubmit(x);
            dc.SubmitChanges();
        }

        internal void deletFiliere(int id_filiere)
        {
            foreach(etudiant e in this.GetEtudiants())
            {
                if (e.id_fil == id_filiere)
                {
                    this.listEtudiant.Remove(e);
                    dc.etudiant.DeleteOnSubmit(e);
                    dc.SubmitChanges();
                }
            }
            foreach(Filiere f in this.GetFilieres())
            {
                if (f.Id_filiere == id_filiere)
                {
                    this.listFiliere.Remove(f);
                    dc.Filiere.DeleteOnSubmit(f);
                    dc.SubmitChanges();
                }
            }
        }
        public void modifier(etudiantRad etudiant)
        {
            var x = (from f in dc.etudiant
                     where f.cne == etudiant.cne
                     select f).SingleOrDefault();

            x.nom = etudiant.nom;
            x.prenom = etudiant.prenom;
            x.date_naiss = etudiant.date_naiss;
            dc.SubmitChanges();
        }
    }

    public class etudiantRad
    {
        public int cne { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public DateTime? date_naiss { get; set; }
    }
}
