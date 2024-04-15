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

namespace TeamTasker.ViewModels
{
    public class DevelopersViewModel
    {
        public delegate void ViewChanger(Developer dev);

        public static event ViewChanger ProfileChanger; 
        public RelayCommand GoToProfile { get; set; }
        public ObservableCollection<Developer> Developers { get; set; }
        public Developer SelectedDeveloper { get; set; }
        public static RelayCommand AddDeveloperView { get; set; }

        public DevelopersViewModel()
        {
            using(TeamTaskerContext db=new TeamTaskerContext())
            {
                Developers = new ObservableCollection<Developer>(db.Developers.ToList());
            }
            GoToProfile = new RelayCommand((o) =>
            {
                if(SelectedDeveloper != null)
                    ProfileChanger?.Invoke(SelectedDeveloper);
            });
        }
        
    }
}
