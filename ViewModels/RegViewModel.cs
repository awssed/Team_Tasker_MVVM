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
    internal class RegViewModel
    {
        public delegate void ViewChanger();
        public static event ViewChanger Changer;
        public RelayCommand RegisterCommand { get; set; }
        public Developer Developer { get; set; }
        public RegViewModel()
        {

            Developer = new Developer();
            
            RegisterCommand = new RelayCommand((o) =>
            {
                using(TeamTaskerContext db=new TeamTaskerContext())
                {
                    db.Developers.Add(Developer);
                    db.SaveChanges();
                }
                Changer.Invoke();
            },CanRegister);
        }
        private bool CanRegister(object parametr)
        {
            string error = Developer["Name"];      // Check the validity of the Name property
            error += Developer["DeveloperId"];     // Check the validity of the DeveloperId property
            error += Developer["Password"];        // Check the validity of the Password property

            // Perform additional checks or validations if needed

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
