using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    public class TaskViewModel:ObservableObject
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();
        public Models.Task CurrentTask { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }
        public TaskViewModel() { }
        public TaskViewModel( Models.Task task) 
        {
            CurrentTask = task;
            SaveChangesCommand = new RelayCommand(o =>
            {
                SaveChanges();
            });
        }
        public void SaveChanges()
        {
            db.Tasks.Update(CurrentTask);
            db.Save();
        }
    }
}
