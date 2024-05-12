using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamTasker.EntityModels;
using TeamTasker.Models;

namespace TeamTasker.UnityOfWork
{
    public class ProgressRepository : IRepository<Models.Progress>
    {
        public TeamTaskerContext db;
        public ProgressRepository(TeamTaskerContext db)
        {
            this.db = db;
        }
        public void Create(Progress item)
        {
            try
            {
                var exestingTask = db.Tasks.FirstOrDefault(t => t.TaskId == item.Task.TaskId);
                if (exestingTask == null)
                {
                    throw new Exception("Cannot create Progress. Related task does not exist.");
                }
                if (!db.Tasks.Local.Contains(exestingTask))
                {
                    db.Tasks.Attach(exestingTask);
                }
                Progress newProg= new Progress();
                newProg.Procent = item.Procent;
                newProg.Task = exestingTask;
                newProg.Description = item.Description;
                db.Progresss.Add(newProg);
                exestingTask.Progress.Add(newProg);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(object id)
        {
            try
            {
                Progress item=new Progress();
                if (id != null)
                {
                    item = (Progress)id;
                }
                if (item != null)
                {
                    db.Progresss.Remove(item);
                    item.Task.Procent -= item.Procent;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Progress Get(object id)
        {
            try
            {
                Progress p=db.Progresss.FirstOrDefault(p => p.ProgressId == (int)id);
                return p;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public IEnumerable<Progress> GetAll()
        {
            try
            {
                return db.Progresss.ToList<Progress>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void Update(Progress item)
        {
            try
            {
                Progress progress = db.Progresss.FirstOrDefault(t=>t.ProgressId == (int)item.ProgressId);
                if(progress != null)
                {
                    progress.Procent = item.Procent;
                    progress.Description= item.Description;
                    progress.IsCommited = item.IsCommited;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
