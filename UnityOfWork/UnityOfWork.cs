using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamTasker.EntityModels;

namespace TeamTasker.UnityOfWork
{
    public class UnityOfWorkClass:IDisposable
    {
        private TeamTaskerContext db=new TeamTaskerContext();
        private DeveloperRepository _developerRepository;
        private ProjectRepository _projectRepository;
        private bool disposed = false;
        public DeveloperRepository Developers
        {
            get
            {
                if (_developerRepository == null)
                {
                    _developerRepository = new DeveloperRepository(db);
                }
                return _developerRepository;
            }
        }
        public ProjectRepository Projects
        {
            get
            {
                if(_projectRepository == null)
                {
                    _projectRepository = new ProjectRepository(db);
                }
                return _projectRepository;
            }
        }
        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
