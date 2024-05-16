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
    public class ProgressViewModel:ObservableObject
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();
        public Progress CurrentProgress { get; set; }
        public Models.Task CurrentTask { get; set; }
        public RelayCommand PushProgressCommand { get; set; }
        public ProgressViewModel()
        {
            CurrentProgress = new Progress();
            PushProgressCommand = new RelayCommand(PushProgress,CanPushProgress);
        }
        public ProgressViewModel(Models.Task task)
        {
            CurrentProgress= new Progress(task);
            CurrentTask = task;
            PushProgressCommand = new RelayCommand(PushProgress, CanPushProgress);
        }
        public bool CanPushProgress(object parametr)
        {
            if(CurrentProgress.Procent>0&& CurrentTask.Procent!=100 && !String.IsNullOrEmpty(CurrentProgress.Description))
            {
                return true;
            }
            return false;
        }
        public void PushProgress(object o)
        {
            int sumProgress = 0;
            foreach(var p in CurrentTask.Progress)
            {
                sumProgress += p.Procent;
            }
            if(sumProgress+CurrentProgress.Procent > 100)
            {
                CurrentProgress.Procent = 100-sumProgress;
                CurrentTask.Procent = 100;
            }
            else
            {
                CurrentTask.Procent = sumProgress+CurrentProgress.Procent;
            }
            CurrentTask.LeadCheck = true;
            db.Progress.Create(CurrentProgress);
            db.Tasks.Update(CurrentTask);
            db.Save();
            CloseWindow();
        }
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
