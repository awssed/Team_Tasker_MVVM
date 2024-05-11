using TeamTasker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security;
using System.Windows.Input;
using TeamTasker.Models;
using TeamTasker.EntityModels;

namespace TeamTasker.ViewModels
{
    class SignViewModel
    {
        public delegate void ViewChanger();
        public static event ViewChanger SignAdmin;
        public static event ViewChanger SignUser;
        public static RelayCommand SignCommand { get; set; }

        private static string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private static string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public bool CanSign(object parameter)
        {
            bool a= !string.IsNullOrEmpty(_login) && !string.IsNullOrEmpty(_password);
            if (a)
                return a;
            return false;
        }
        public SignViewModel()
        {
            SignCommand = new RelayCommand((o) =>
            {
                using (TeamTaskerContext db = new TeamTaskerContext())  
                {
                    var user = db.Developers.FirstOrDefault(d => d.DeveloperId == Login);
                    if (user!=null && Developer.Verify(user.Salt,user.HashPassword,Password))
                    {
                        MainViewModel.CurrentUser = user;
                        if (user.isAdmin)
                        {
                            SignAdmin?.Invoke();
                        }
                        else
                        {
                            SignUser?.Invoke();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong password or login");
                    }
                }
            },CanSign);
        }

    }
}
