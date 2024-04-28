using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTasker.Models
{
    public class Progress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgressId { get; set; }
        public Models.Task Task { get; set; }
        public int Procent {  get; set; }
        public string Description { get; set; }
        public Progress(Models.Task task, int procent, string description)
        {
            Task = task;
            Procent = procent;
            Description = description;
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
