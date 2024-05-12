﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;
using TeamTasker.Views;

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
        public RelayCommand ChangePasswordCommand { get; set; }
        private ChangePasswordViewModel PasswordChangeView { get; set; }
        public static Developer? Developer { get; set; }
        private string _name;
        private string _email;
        private Position _position;
        private byte[] _image;
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
        public byte[] Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
        public UserProfileViewModel(Developer dev)
        {
            Developer = dev;
            Name = Developer.Name;
            Email = Developer.Email;
            Position = Developer.Position;
            Image = Developer.Image;
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute,CanSaveDeveloper);
            ChangePasswordCommand = new RelayCommand(OpenChangePasswordView);
        }
        public UserProfileViewModel()
        {
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute,CanSaveDeveloper);
            DeleteCommand = new RelayCommand(DeleteCommandExecute);
            ChangePasswordCommand = new RelayCommand(OpenChangePasswordView);
            if (Developer != null)
            {
                Name = Developer.Name;
                Email = Developer.Email;
                Position = Developer.Position;
                Image = Developer.Image;
            }
        }
        private void CancelCommandExecute(object parameter)
        {

            Name = Developer.Name;
            Email = Developer.Email;
            Position = Developer.Position;
            Image = Developer.Image;
            
            Changer?.Invoke();
        }
        private void SaveCommandExecute(object parametr)
        {
            Developer.Name = Name;
            Developer.Email = Email;
            Developer.Position = Position;
            Developer.Image = Image;
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
        private bool CanSaveDeveloper(object parametr)
        {
            string error = Developer["Name"];
            error += Developer["Email"];
            if (string.IsNullOrEmpty(error))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OpenChangePasswordView(object parametr)
        {
            PasswordChangeView = new ChangePasswordViewModel(Developer);
            var view = new ChangePasswordView();
            view.Owner = App.Current.MainWindow;
            view.DataContext = PasswordChangeView;
            view.Closed += (object sender, EventArgs e) =>
            {
                PasswordChangeView = null;
            };
            view.Show();
        }
    }
}
