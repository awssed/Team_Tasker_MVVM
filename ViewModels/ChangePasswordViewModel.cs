using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    public class ChangePasswordViewModel
    {
        private UnityOfWorkClass db = new UnityOfWorkClass();
        public Developer CurrentDeveloper { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public RelayCommand ChangePasswordCommand { get; set; }
        public ChangePasswordViewModel()
        {
            CurrentDeveloper = new Developer();
            ChangePasswordCommand = new RelayCommand(ChangePassword, CanChangePassword);

        }
        public ChangePasswordViewModel(Developer developer)
        {
            CurrentDeveloper = developer;
            ChangePasswordCommand = new RelayCommand(ChangePassword,CanChangePassword);
        }
        private void ChangePassword(object parametr)
        {
            CurrentDeveloper.Salt = Developer.CreateSalt(NewPassword);
            CurrentDeveloper.HashPassword = Developer.CreateHash(Developer.Concat(NewPassword, Convert.FromBase64String(CurrentDeveloper.Salt)));
            db.Developers.Update(CurrentDeveloper);
            db.Save();
            CloseWindow();
        }
        private bool CanChangePassword(object parametr)
        {
            if(NewPassword==ConfirmPassword && !String.IsNullOrEmpty(NewPassword) && !String.IsNullOrEmpty(ConfirmPassword))
            {
                return true;
            }
            return false;
        }
        private void CloseWindow()
        {
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
