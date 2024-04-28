using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.Models;

namespace TeamTasker.ViewModels
{
    public class ProgressViewModel:ObservableObject
    {
        public Progress CurrentProgress { get; set; }
        public Models.Task CurrentTask { get; set; }
        public ProgressViewModel()
        {
            CurrentProgress = new Progress();
        }
        public ProgressViewModel(Models.Task task)
        {
            CurrentProgress= new Progress(task);
        }
    }
}
