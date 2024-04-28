using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamTasker.EntityModels;
using TeamTasker.Models;

namespace TeamTasker.UnityOfWork
{
    public class ProjectRepository:IRepository<Project>
    {
        private TeamTaskerContext db;
        public ProjectRepository(TeamTaskerContext context)
        {
            this.db = context;
        }
        public void Create(Project item)
        {
            try
            {
                db.Projects.Add(item);
            }
            catch
            (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(object id)
        {
            try
            {
                var project = db.Projects.FirstOrDefault(x => x.ProjectId == (int)id);
                if (project != null) 
                     db.Projects.Remove(project);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Project Get(object id)
        {
            try
            {
                var project=db.Projects.FirstOrDefault(p=>p.ProjectId == (int)id);
                if(project != null)
                    return project;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public IEnumerable<Project> GetAll()
        {
            try
            {
                ObservableCollection<Project> Projects = new ObservableCollection<Project>();
                foreach (Project project in db.Projects.Include(project => project.Developers))
                {
                    Projects.Add(project);
                }
                return Projects;
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void Update(Project item)
        {
            try
            {
                if(item != null)
                {
                    db.Projects.Update(item);
                    foreach (Developer dev in item.Developers)
                    {
                        dev.Projects.Add(item);
                        db.Update(dev);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
