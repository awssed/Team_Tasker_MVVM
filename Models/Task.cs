using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace TeamTasker.Models
{
    public class Task : IDataErrorInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
        public DateTime EndTime { get; set; }
        public Developer Developer { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int Procent { get; set; } = 0;
        public ObservableCollection<Progress> Progress { get; set; }
        public Task(String name,Project project, DateTime endTime, Developer developer,string description, bool isCompleted)
        {
            Name = name;
            Project = project;
            EndTime = endTime;
            Developer = developer;
            Description = description;
            IsCompleted = isCompleted;
            Progress = new ObservableCollection<Progress>();
        }
        public Task(String name, Project project, DateTime endTime, Developer developer, string description, bool isCompleted,ObservableCollection<Progress> progress)
        {
            Name = name;
            Project = project;
            EndTime = endTime;
            Developer = developer;
            Description = description;
            IsCompleted = isCompleted;
            Progress = progress;
        }
        public Task()
        {
            Project = null;
            EndTime=DateTime.Now;
            IsCompleted=false;
            Description = "";
            Progress = new ObservableCollection<Progress>();
        }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                            error = "Name is required.";
                        break;
                    case nameof(EndTime):
                        if (EndTime < DateTime.Now)
                            error = "End time must be later than start time.";
                        break;
                    case nameof(Description):
                        if (string.IsNullOrEmpty(Description))
                        {
                            error = "Description is required";
                        }
                        break;
                    case nameof(Developer):
                        if (Developer==null)
                        {
                            error = "Developers are reqired";
                        }
                        break;
                }
                return error;
            }
        }

        [JsonIgnore]
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
