using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    public class UserProfileViewModel:ObservableObject
    {
        private UnityOfWorkClass db = new UnityOfWorkClass();
        public delegate void ViewChanger();
        public static event ViewChanger Changer;
        public static RelayCommand LogOut { get; set; }
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
        public UserProfileViewModel(Developer dev)
        {
            Developer = dev;
            Name = Developer.Name;
            Email = Developer.Email;
            Position = Developer.Position;
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
        }
        public UserProfileViewModel()
        {
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            DeleteCommand = new RelayCommand(DeleteCommandExecute);
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
            Changer?.Invoke();
        }
        private void SaveCommandExecute(object parametr)
        {
            Developer.Name = Name;
            Developer.Email = Email;
            Developer.Position = Position;
            db.Developers.Update(Developer);
            db.Save();
        }
        private void DeleteCommandExecute(object parameter)
        {
            if (Developer != null)
            {
                db.Developers.Delete(Developer.DeveloperId);
                db.Save();
                Changer?.Invoke();
            }
        }
    }
}
