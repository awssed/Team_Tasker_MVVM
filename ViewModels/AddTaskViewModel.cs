using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    class AddTaskViewModel
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();

        public AddTaskViewModel( Project currentProject)
        {
            
            CurrentProject = currentProject;
            CurrentTask = new Models.Task();
            CurrentTask.Project = currentProject;
            AddTask = new RelayCommand((o)=>
            {
                db.Tasks.Create(CurrentTask);
                db.Save();
                CloseWindow();
            });
        }
        public AddTaskViewModel()
        { }
        public Project CurrentProject { get; set; }
        public Models.Task CurrentTask { get; set; }
        public RelayCommand AddTask { get; set; }
        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
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
