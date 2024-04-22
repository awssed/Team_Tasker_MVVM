using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTasker.Core;
using TeamTasker.EntityModels;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    internal class ProjectsViewModel : ObservableObject
    {
        private UnityOfWorkClass bd=new UnityOfWorkClass();
        private ObservableCollection<Developer> _allDevelopers;
        public RelayCommand AddProjectCommand { get; set; }
        public RelayCommand SaveProjectCommand { get; set; }
        public RelayCommand AddDeveloperCommand { get; set; }
        public RelayCommand DeleteDeveloperCommand { get; set; }
        public RelayCommand DeleteProjectCommand { get; set; }
        public Developer SelectedDeveloper { get; set; }
        private Developer _selectedTeamLead;
        public Developer SelectedTeamLead
        {
            get { return _selectedTeamLead; }
            set
            {
                SelectedDeveloper = value;
                CurrentProject.TeamLeadId = SelectedDeveloper.DeveloperId;
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
        public string SearchString { 
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
        public ObservableCollection<Developer> AllDevelopers
        {
            get { return _allDevelopers; }
            set
            {
                _allDevelopers = value;
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
        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }
        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }
        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set 
            {
                _endTime = value;
                OnPropertyChanged(); 
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Developer> _projectDevelopers;
        public ObservableCollection<Developer> ProjectDevelopers
        {
            get { return _projectDevelopers; }
            set
            {
                _projectDevelopers = value;
                OnPropertyChanged();
            }
        }
        private Project _currentProject;
        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                ProjectName = value.Name;
                StartTime = value.StartTime;
                EndTime = value.EndTime;
                Description = value.Description;
                ProjectDevelopers= (ObservableCollection<Developer>)value.Developers;
                OnPropertyChanged();
            }
        }
        public ProjectsViewModel()
        {
            CurrentProject = new Project();
            AddProjectCommand = new RelayCommand(AddProject);
            SaveProjectCommand = new RelayCommand(SaveProject,CanSaveProject);
            AddDeveloperCommand = new RelayCommand(AddDeveloper);
            DeleteDeveloperCommand=new RelayCommand(DeleteDeveloper);
            DeleteProjectCommand = new RelayCommand(DeleteProject, CanDeleteProject);
            Projects= (ObservableCollection<Project>)bd.Projects.GetAll();
            SearchProjects = Projects;
            AllDevelopers= new ObservableCollection<Developer>(bd.Developers.GetAll());
        }
        private void AddDeveloper(object parametr)
        {
            ProjectDevelopers.Add(SelectedDeveloper);
            AllDevelopers.Remove(SelectedDeveloper);
        }
        private void DeleteDeveloper(object parametr)
        {
            AllDevelopers.Add(SelectedDeveloper);
            ProjectDevelopers.Remove(SelectedDeveloper);
        }
        private void AddProject(object paramert)
        {
            CurrentProject = new Project();
        }
        private void SaveProject(object paramert)
        {
            if(CurrentProject.ProjectId == 0)
            {
                bd.Projects.Create(CurrentProject);
                bd.Save();
                Projects = (ObservableCollection<Project>)bd.Projects.GetAll();
                SearchString = "";
                CurrentProject=new Project();
            }
        }
        private bool CanSaveProject(object paramert)
        {
            String error = CurrentProject["StartTime"];
            error += CurrentProject["EndTime"];
            error += CurrentProject["Description"];
            error += CurrentProject["Developers"];

            if (string.IsNullOrEmpty(error))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DeleteProject(object paramert)
        {
            if (CurrentProject.ProjectId != 0)
            {
                bd.Projects.Delete(CurrentProject.ProjectId);
                bd.Save();
                Projects = (ObservableCollection<Project>)bd.Projects.GetAll();
                SearchString = "";
                CurrentProject =new Project(); 
            }
        }
        private bool CanDeleteProject(object paramert)
        {
            if (CurrentProject.ProjectId != 0)
                return true;
            return false;
        }
    }
}
//после удаления индекс выбранного преокта не обнуляется