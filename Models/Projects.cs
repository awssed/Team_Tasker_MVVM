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
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string TeamLead { get; set; }
        public virtual ICollection<Developer>? Developers { get; set; }
        public virtual ICollection<Models.Task> Tasks { get; set; }=new ObservableCollection<Models.Task>();
        public Project(int projectId, string name, DateTime startTime, DateTime endTime)
        {
            ProjectId = projectId;
            Name = name;
            EndTime = endTime;
            Developers = new ObservableCollection<Developer>();

        }
        public Project(int projectId, string name, DateTime startTime, DateTime endTime, ObservableCollection<Developer> developers)
        {
            ProjectId = projectId;
            Name = name;
            EndTime = endTime;
            Developers = developers;
        }
        public Project()
        {
            ProjectId = 0;
            Name = string.Empty;
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
                    
                    case nameof(EndTime):
                        if (EndTime.Date < DateTime.Now.Date)
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
                    case nameof(TeamLead):
                        if(TeamLead.IsNullOrEmpty())
                        {
                            error = "Teamled cant be empty";
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
