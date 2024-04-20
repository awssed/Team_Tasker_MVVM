using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using TeamTasker.Core;
using TeamTasker.Models;
using System.Windows.Interactivity;
using TeamTasker.EntityModels;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    public class DevelopersViewModel:ObservableObject
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();
        public delegate void ViewChanger(Developer dev);

        public static event ViewChanger ProfileChanger; 
        public RelayCommand GoToProfile { get; set; }
        public ObservableCollection<Developer> Developers { get; set; }
        public ObservableCollection<Developer> SearchDevelopers { get; set; }
        public Developer SelectedDeveloper { get; set; }
        public static RelayCommand AddDeveloperView { get; set; }
        private string _searchName;
        public string SearchName {
            get
            {
                return _searchName;
            }
            set
            {
                _searchName = value;
                OnPropertyChanged();
            }
        }
        private Position _searchPosition;
        public Position SearchPosition
        {
            get
            {
                return _searchPosition;
            }
            set
            {
                _searchPosition = value;
                OnPropertyChanged();
            }
        }

        public DevelopersViewModel()
        {
            try
            {
                Developers = new ObservableCollection<Developer>(db.Developers.GetAll());
                SearchDevelopers = new ObservableCollection<Developer>(db.Developers.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GoToProfile = new RelayCommand((o) =>
            {
                if(SelectedDeveloper != null)
                    ProfileChanger?.Invoke(SelectedDeveloper);
            });
        }
        
    }
}
