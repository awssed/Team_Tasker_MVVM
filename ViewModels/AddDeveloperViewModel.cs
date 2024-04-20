using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.EntityModels;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    class AddDeveloperViewModel:ObservableObject
    {
        private UnityOfWorkClass bd=new UnityOfWorkClass();
        public delegate void ViewChanger();

        public static event ViewChanger Changer;
        public RelayCommand AddDeveloperCommand { get; set; } 
        private Developer _developer;
        
        public Developer Developer
        {
            get { return _developer; }
            set {
                _developer = value; 
                OnPropertyChanged();
            }
        }
        public AddDeveloperViewModel()
        {
            Developer = new Developer();
            AddDeveloperCommand=new RelayCommand(AddDeveloper,CanAddDeveloper);
        }
        private void AddDeveloper(object parametr)
        {
            bd.Developers.Create(Developer);
            bd.Save();
            Changer?.Invoke();
        }
        private bool CanAddDeveloper(object parametr)
        {
            string error = Developer["Name"];
            error += Developer["DeveloperId"];
            error += Developer["Password"];
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
    }
}
