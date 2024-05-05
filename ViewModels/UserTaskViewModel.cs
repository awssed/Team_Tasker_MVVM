using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;
using TeamTasker.Views;

namespace TeamTasker.ViewModels
{
    public class UserTaskViewModel:ObservableObject
    {
        private UnityOfWorkClass db = new UnityOfWorkClass();
        private ObservableCollection<Models.Task> _toDoTasks;
        public ObservableCollection<Models.Task> ToDoTasks
        {
            get
            {
                return _toDoTasks;
            }
            set
            {
                _toDoTasks = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Models.Task> _doingTasks;
        public ObservableCollection<Models.Task> DoingTasks
        {
            get
            {
                return _doingTasks;
            }
            set
            {
                _doingTasks = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Models.Task> _doneTasks;
        public ObservableCollection<Models.Task> DoneTasks
        {
            get
            {
                return _doneTasks;
            }
            set
            {
                _doneTasks = value;
                OnPropertyChanged();
            }
        }
        private Models.Task _selectedTask;
        public Models.Task SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = null;
                OnPropertyChanged();
                _selectedTask = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Project> _searchProjects;
        public ObservableCollection<Project> SearchProjects
        {
            get
            {
                return _searchProjects;
            }

            set
            {
                _searchProjects = value;
                OnPropertyChanged();
            }
        }
        private string _searchString;
        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                SearchProjects = new ObservableCollection<Project>(Projects.Where(p => p.Name.Contains(_searchString)));
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                OnPropertyChanged();
            }
        }
        private Project _currentProject;
        public Project CurrentProject
        {
            get
            {
                return _currentProject;
            }
            set
            {
                _currentProject = value;
                ToDoTasks = db.Tasks.GetToDoUserTasks(_currentProject,MainViewModel.CurrentUser);
                DoingTasks = db.Tasks.GetDoingUserTasks(_currentProject, MainViewModel.CurrentUser);
                DoneTasks = db.Tasks.GetDoneUserTasks(_currentProject, MainViewModel.CurrentUser);
                OnPropertyChanged();
            }
        }
        private ProgressViewModel _progressViewModel;
        public RelayCommand OpenProgressCommand { get; set; }

        public UserTaskViewModel()
        {
            Projects = new ObservableCollection<Project>(db.Projects.GetAll());
            if (MainViewModel.CurrentUser != null)
                Projects = new ObservableCollection<Project>(Projects.Where(p => p.Developers.Any(d => d.DeveloperId == MainViewModel.CurrentUser.DeveloperId)));
        SearchProjects = Projects;

           
            OpenProgressCommand = new RelayCommand(o =>
            {
                if (SelectedTask == null)
                    return;
                if (SelectedTask.IsCompleted)
                    return;
                _progressViewModel = new ProgressViewModel(SelectedTask);
                var progressView = new ProgressView();
                progressView.Owner = App.Current.MainWindow;

                progressView.DataContext = _progressViewModel;
                progressView.Closed += (object sender, EventArgs e) => { _progressViewModel = null;CurrentProject = CurrentProject; };
                progressView.Show();
            });
        }
    }
}
