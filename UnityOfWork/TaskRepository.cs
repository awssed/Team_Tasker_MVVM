using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public class TaskRepository:IRepository<Models.Task>
    {
        private TeamTaskerContext db;
        public TaskRepository(TeamTaskerContext db)
        {
            this.db = db;
        }

        public void Create(Models.Task item)
        {
            try
            {
                // Проверка, что связанные объекты уже существуют в базе данных
                var existingProject = db.Projects.FirstOrDefault(p => p.ProjectId == item.Project.ProjectId);
                var existingDeveloper = db.Developers.FirstOrDefault(d => d.DeveloperId == item.Developer.DeveloperId);

                if (existingProject == null || existingDeveloper == null)
                {
                    // Обработка случая, когда связанные объекты не найдены в базе данных
                    throw new Exception("Cannot create task. Related project or developer does not exist.");
                }

                // Присоединение связанных объектов к контексту базы данных (если они не были присоединены ранее)
                if (!db.Projects.Local.Contains(existingProject))
                {
                    db.Projects.Attach(existingProject);
                }

                if (!db.Developers.Local.Contains(existingDeveloper))
                {
                    db.Developers.Attach(existingDeveloper);
                }

                // Создание нового объекта Task
                var newTaskEntity = new Models.Task
                {
                    Name = item.Name,
                    Project = existingProject,
                    EndTime = item.EndTime,
                    Developer = existingDeveloper,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted
                };

                // Добавление нового задания в базу данных
                db.Tasks.Add(newTaskEntity);

                // Сохранение изменений
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ObservableCollection<Models.Task> GetToDoTasks(Project project)
        {
            try
            {
                return new ObservableCollection<Models.Task>(db.Tasks.Include(t => t.Progress)
                    .Where(t => t.Project == project && t.IsCompleted == false && t.Progress.Count == 0)
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public ObservableCollection<Models.Task> GetDoingTasks(Project project)
        {
            try
            {
                return new ObservableCollection<Models.Task>(db.Tasks.Include(t => t.Progress)
                    .Where(t => t.Project == project && t.IsCompleted == false && t.Progress.Count > 0)
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public ObservableCollection<Models.Task> GetDoneTasks(Project project)
        {
            try
            {
                return new ObservableCollection<Models.Task>(db.Tasks.Include(t => t.Progress)
                    .Where(t => t.Project == project && t.IsCompleted == true)
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public ObservableCollection<Models.Task> GetToDoUserTasks(Project project, Developer user)
        {
            try
            {
                return new ObservableCollection<Models.Task>(db.Tasks.Include(t => t.Progress)
                    .Where(t => t.Project == project && t.IsCompleted == false && t.Progress.Count == 0 && t.Developer.DeveloperId == user.DeveloperId)
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public ObservableCollection<Models.Task> GetDoingUserTasks(Project project, Developer user)
        {
            try
            {
                return new ObservableCollection<Models.Task>(db.Tasks.Include(t => t.Progress)
                    .Where(t => t.Project == project && t.IsCompleted == false && t.Progress.Count > 0 && t.Developer.DeveloperId == user.DeveloperId)
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public ObservableCollection<Models.Task> GetDoneUserTasks(Project project, Developer user)
        {
            try
            {
                return new ObservableCollection<Models.Task>(db.Tasks.Include(t => t.Progress)
                    .Where(t => t.Project == project && t.IsCompleted == true && t.Developer.DeveloperId == user.DeveloperId)
                    .ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        public void Delete(object task)
        {
            try
            {
                db.Tasks.Remove((Models.Task)task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Models.Task Get(object id)
        {
            try
            {
                var task = db.Tasks.FirstOrDefault(p => p.TaskId == (int)id);
                if(task != null)
                {
                    return task;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public IEnumerable<Models.Task> GetAll()
        {
            try
            {
                return db.Tasks.Include(t => t.Progress).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public void Update(Models.Task item)
        {
            try
            {
                Models.Task taskToUpdate = db.Tasks.FirstOrDefault(t => t.TaskId == item.TaskId);

                if (taskToUpdate != null)
                {
                    // Обновление свойств объекта Task
                    taskToUpdate.Name = item.Name;
                    taskToUpdate.EndTime = item.EndTime;
                    taskToUpdate.Description = item.Description;
                    taskToUpdate.Procent= item.Procent;
                    taskToUpdate.IsCompleted=item.IsCompleted;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
