using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TeamTasker.Models
{
    public class Project : IDataErrorInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
       public virtual ICollection<Developer>? Developers { get; set; }
        public Project(int projectId, string name, DateTime startTime, DateTime endTime)
        {
            ProjectId = projectId;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Developers = new ObservableCollection<Developer>();
        }
        public Project(int projectId, string name, DateTime startTime, DateTime endTime, ObservableCollection<Developer> developers)
        {
            ProjectId = projectId;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Developers = developers;
        }
        public Project()
        {
            ProjectId = 0;
            Name = string.Empty;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Developers=new ObservableCollection<Developer>();
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
                    case nameof(StartTime):
                        if (StartTime > EndTime)
                            error = "Start time must be earlier than end time.";
                        break;
                    case nameof(EndTime):
                        if (EndTime < StartTime)
                            error = "End time must be later than start time.";
                        break;
                    case nameof(Description):
                        if(string.IsNullOrEmpty(Description))
                        {
                            error = "Description is required";
                        }
                        break;
                    case nameof(Developers):
                        if(Developers.IsNullOrEmpty())
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
