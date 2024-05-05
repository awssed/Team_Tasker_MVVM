using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;
using TeamTasker.Views;

namespace TeamTasker.ViewModels
{
    class UserProjectViewModel : ObservableObject
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();
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
                ToDoTasks = db.Tasks.GetToDoTasks(_currentProject);
                DoingTasks=db.Tasks.GetDoingTasks(_currentProject);
                DoneTasks=db.Tasks.GetDoneTasks(_currentProject);
                OnPropertyChanged();
            }
        }
        private AddTaskViewModel _addTaskViewModel;
        public AddTaskViewModel AddTaskViewModel
        {
            get { return _addTaskViewModel; }
            set
            {
                _addTaskViewModel = value;
                OnPropertyChanged();
            }
        }
        private AddTaskView AddTaskView { get; set; }
        public RelayCommand AddNewTaskCommand { get; set; }
        private AddTaskViewModel addTaskViewModel; // Добавьте приватное поле для хранения экземпляра AddTaskViewModel

        private TaskViewModel _taskViewModel;
        private TaskView _taskView;
        public RelayCommand OpenTaskCommand { get; set; }

        public UserProjectViewModel()
        {
            Projects = new ObservableCollection<Project>(db.Projects.GetAll());
            if(MainViewModel.CurrentUser!=null)
            Projects = new ObservableCollection<Project>(db.Projects.GetAll().Where(p => p.TeamLead == MainViewModel.CurrentUser.DeveloperId).ToList());
            SearchProjects = Projects;

            AddNewTaskCommand = new RelayCommand(o =>
            {
                // Создание экземпляра AddTaskViewModel и передача текущего проекта
                addTaskViewModel = new AddTaskViewModel(CurrentProject);
                var addTaskView = new AddTaskView();
                addTaskView.Owner = App.Current.MainWindow;

                // Открытие окна AddTaskView
                addTaskView.DataContext = addTaskViewModel;
                addTaskView.Closed += AddTaskView_Closed; // Добавьте обработчик события Closed для окна
                addTaskView.Show();
            },CanAddNewTask);
            OpenTaskCommand = new RelayCommand(o =>
            {
                if (SelectedTask == null)
                    return;
                _taskViewModel = new TaskViewModel(SelectedTask);
                var taskView=new TaskView();
                taskView.Owner = App.Current.MainWindow;

                taskView.DataContext = _taskViewModel;
                taskView.Closed += (object sender, EventArgs e) => { _taskViewModel = null;CurrentProject=CurrentProject; };
                taskView.Show();
            });
        }

        private void AddTaskView_Closed(object sender, EventArgs e)
        {
            // Очистка экземпляра AddTaskViewModel при закрытии окна
            addTaskViewModel = null;
            CurrentProject = CurrentProject;
        }
        public bool CanAddNewTask(object parametr)
        {
            if(CurrentProject!=null)
            {
                return true;
            }
            return false;
        }
    }
}
