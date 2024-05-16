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
        public bool UserCheck { get; set; } = false;
        public bool LeadCheck { get; set; } = false;
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
        [NotMapped]
        [JsonIgnore]
        public bool IsExpired
        {
            get
            {
                if (this.IsCompleted)
                    return false;
                if (this.EndTime.Date < DateTime.Now.Date)
                    return true;
                return false;
            }
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
                        if (EndTime.Date < DateTime.Now.Date)
                            error = "End time must be later than start time.";
                        if (EndTime.Date > Project.EndTime.Date)
                            error = "End time must be earlier than end time of project.";
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
