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
    public class DeveloperRepository : IRepository<Developer>
    {
        private TeamTaskerContext db;
        public DeveloperRepository(TeamTaskerContext context)
        {
            this.db = context;
        }
        public void Create(Developer item)
        {
            try
            {
                db.Developers.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(object id)
        {
            try
            {
                var dev = db.Developers.FirstOrDefault(p => p.DeveloperId == (string)id);
                if(dev != null)
                    db.Developers.Remove(dev);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Developer Get(object id)
        {

            try
            {
                var dev = db.Developers.FirstOrDefault(p => p.DeveloperId == (string)id);
                if(dev != null)
                    return dev;
            }
            catch (Exception e )
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public IEnumerable<Developer> GetAll()
        {
            try
            {
                return db.Developers.ToList<Developer>();
            }
            catch( Exception e )
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public void Update(Developer item)
        {
            try
            {
                db.Developers.Update(item);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
