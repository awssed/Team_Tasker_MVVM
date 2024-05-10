using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
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
    public class TaskViewModel:ObservableObject
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();
        private Models.Task _currentTask { get; set; }
        public Models.Task CurrentTask
        {
            get
            {
                return _currentTask;
            }
            set
            {
                _currentTask = value;
                OnPropertyChanged();
            }
        }
        public string Description { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CompleteCommand { get; set; }
        public RelayCommand RegretCommand { get; set; }
        public RelayCommand AcceptProgressCommand { get; set; }
        public RelayCommand RegretProgressCommand { get; set; }
        public TaskViewModel() { }
        public TaskViewModel( Models.Task task) 
        {
            CurrentTask = task;
            Description=task.Description;
            SaveChangesCommand = new RelayCommand(SaveChanges,CanSaveChanges);
            DeleteCommand = new RelayCommand(DeleteTask);
            CompleteCommand = new RelayCommand(CompleteTask, CanComleteTask);
            RegretCommand = new RelayCommand(RegretTask,CanComleteTask);
            AcceptProgressCommand = new RelayCommand(AcceptProgress);
            RegretProgressCommand = new RelayCommand(RegretProgress);
        }
        public void AcceptProgress(object parametr)
        {
            Progress progress = (Progress)parametr;
            progress.IsCommited = true;
            db.Progress.Update(progress);
            db.Save();
            CurrentTask = CurrentTask;
            OnPropertyChanged(nameof(progress.IsCommited));
        }
        public void RegretProgress(object parametr)
        {
            Progress progress = (Progress)parametr;
            db.Progress.Delete(progress);
            CurrentTask.Progress.Remove(progress);
            CurrentTask = CurrentTask;
        }
        public void SaveChanges(object parametr)
        {
            CurrentTask.Description = Description;
            db.Tasks.Update(CurrentTask);
            db.Save();
        }
        public bool CanSaveChanges(object parametr)
        {
            if(CurrentTask.Description!=Description)
            {
                return true;
            }
            return false;
        }
        public void DeleteTask(object parametr)
        {
            db.Tasks.Delete(CurrentTask);
            db.Save();
            CloseWindow();
        }
        public bool CanComleteTask(object parametr)
        {
            if (CurrentTask.IsCompleted)
                return false;
            int sum= 0;
            foreach(var p in CurrentTask.Progress)
            {
                sum += p.Procent;
                if (!p.IsCommited)
                    return false;
            }
            if (sum < 100)
                return false;
            return true;
        }
        public void CompleteTask(object parametr)
        {
            CurrentTask.IsCompleted = true;
            db.Tasks.Update(CurrentTask);
            db.Save();
            CloseWindow();
        }
        public void RegretTask(object parametr)
        {
            db.Progress.Delete(CurrentTask.Progress.Last().ProgressId);
            CurrentTask.Progress.Remove(CurrentTask.Progress.Last());
            int sum = 0;
            foreach (var p in CurrentTask.Progress)
            {
                sum += p.Procent;
            }
            CurrentTask.Procent = sum;
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
