using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.EntityModels;
using TeamTasker.Models;

namespace TeamTasker.ViewModels
{
    class DeveloperInfViewModel:ObservableObject
    {
        public delegate void ViewChanger();
        public static event ViewChanger Changer;
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public static Developer? Developer { get; set; }
        private string _name;
        private string _email;
        private Position _position;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public Position Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }
        public DeveloperInfViewModel(Developer dev) { 
            Developer = dev;
            Name = Developer.Name;
            Email = Developer.Email;
            Position = Developer.Position;
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
        }
        public DeveloperInfViewModel()
        {
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            DeleteCommand=new RelayCommand(DeleteCommandExecute);
            if (Developer != null)
            {
                Name = Developer.Name;
                Email = Developer.Email;
                Position = Developer.Position;
            }
        }
        private void CancelCommandExecute(object parameter)
        {
            Name = Developer.Name;
            Email = Developer.Email;
            Position = Developer.Position;
        }
        private void SaveCommandExecute(object parametr)
        {
            using (TeamTaskerContext db=new TeamTaskerContext())
            {
                Developer updateDeveloper = db.Developers.FirstOrDefault(d => d.DeveloperId == Developer.DeveloperId);
                if (Developer != null)
                {
                    updateDeveloper.Name = Name;
                    updateDeveloper.Email = Email;
                    updateDeveloper.Position = Position;
                    db.Update(updateDeveloper);
                    db.SaveChanges();
                }
            }
            Developer.Name= Name;
            Developer.Email= Email;
            Developer.Position = Position;
        }
        private void DeleteCommandExecute(object parameter)
        {
            if (Developer != null)
            {
                using(TeamTaskerContext db=new TeamTaskerContext())
                {
                    Developer updateDeveloper = db.Developers.FirstOrDefault(d => d.DeveloperId == Developer.DeveloperId);
                    db.Developers.Remove(updateDeveloper);
                    db.SaveChanges();
                    Changer?.Invoke();
                }
            }
        }
    }
}
