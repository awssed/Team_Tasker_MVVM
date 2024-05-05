using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;

namespace TeamTasker.Models
{
    public class Progress:ObservableObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgressId { get; set; }
        public Models.Task Task { get; set; }
        public int Procent {  get; set; }
        public string Description { get; set; }
        private bool _isCommited;
        public bool IsCommited
        {
            get { return _isCommited; }
            set
            {
                if (_isCommited != value)
                {
                    _isCommited = value;
                    OnPropertyChanged();
                }
            }
        }
        public Progress(Models.Task task, int procent, string description)
        {
            Task = task;
            Procent = procent;
            Description = description;
            IsCommited = false;
        }
        public Progress(Models.Task task)
        {
            Task=task;
        }
        public Progress() 
        {
            Task = null;
            Procent = 0;
            Description = "";
        }
    }
}
