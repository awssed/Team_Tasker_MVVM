
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using TeamTasker.Core;
using TeamTasker.Views;
using System.Windows.Controls;
using TeamTasker.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TeamTasker.EntityModels;

namespace TeamTasker.ViewModels
{
    class MainViewModel:ObservableObject
    {
        public RelayCommand SignViewCommand { get; set; }
        public RelayCommand RegViewCommand { get; set; }
        public RelayCommand DevsViewCommand { get; set; }
        public RelayCommand ProjectsViewCommand { get; set; }
        public RelayCommand AddDeveloperViewCommand { get; set; }
        public RelayCommand UserProjectsViewCommand { get; set; }
        public RelayCommand UserTaskViewCommand { get; set; }
        public RelayCommand UserProfileViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public SignViewModel SignViewModel { get; set; }
        public RegViewModel RegViewModel { get; set; }
        public DeveloperInfViewModel DeveloperInfViewModel { get; set; }
        public DevelopersViewModel DevelopersViewModel { get; set; }
        public ProjectsViewModel ProjectsViewModel { get; set; }
        public UserProjectViewModel UserProjectViewModel { get; set; }
        public UserTaskViewModel UserTaskViewModel { get; set; }
        public static UserProfileViewModel UserProfileViewModel { get; set; }
        public AddDeveloperView AddDeveloperView { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }=new SettingsViewModel();
        private static Developer _currentDeveloper;
        public static Developer CurrentUser
        {
            get
            {
                return _currentDeveloper;
            }
            set
            {
                _currentDeveloper = value;
                if (_currentDeveloper != null)
                {
                    UserProfileViewModel = new UserProfileViewModel(_currentDeveloper);
                }
            }
        }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            {   if (value != _currentView)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }
        public MainViewModel()
        {
            SignViewModel=new SignViewModel();

            SignViewModel.SignAdmin += () =>
            {
               AnimateViewChange(DevelopersViewModel);
               Border menu= App.Current.MainWindow.FindName("Menu") as Border;
               Grid startMenu=App.Current.MainWindow.FindName("StartMenu") as Grid;
               menu.Visibility = Visibility.Visible;
               startMenu.Visibility = Visibility.Hidden;
            };
            SignViewModel.SignUser += () =>
            {
                AnimateViewChange(UserProjectViewModel);
                Border menu = App.Current.MainWindow.FindName("MenuUser") as Border;
                Grid startMenu = App.Current.MainWindow.FindName("StartMenu") as Grid;
                menu.Visibility = Visibility.Visible;
                startMenu.Visibility = Visibility.Hidden;
                UserTaskViewModel = new UserTaskViewModel();
            };

            DeveloperInfViewModel.Changer += () =>
            {
                AnimateViewChange(DevelopersViewModel);
            };
            RegViewModel = new RegViewModel();
            RegViewModel.Changer += () =>
            {
                AnimateViewChange(SignViewModel);
            };
            ProjectsViewModel=new ProjectsViewModel();
            DevelopersViewModel = new DevelopersViewModel();
            AddDeveloperView = new AddDeveloperView();
            AddDeveloperViewModel.Changer += () =>
            {
                AnimateViewChange(DevelopersViewModel);
            };
            DevelopersViewModel.ProfileChanger += ProfileChanger;
            DevelopersViewModel.AddDeveloperView = new RelayCommand(o =>
            {
                AnimateViewChange(AddDeveloperView);
            });

            CurrentView = SignViewModel;
            SignViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(SignViewModel);
            });
            RegViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(RegViewModel);
            });
            DevsViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(DevelopersViewModel);
            });
            ProjectsViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(ProjectsViewModel);
            });
            UserProjectViewModel = new UserProjectViewModel();
            UserProjectsViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(UserProjectViewModel);
            });
            UserTaskViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(UserTaskViewModel);
            });
            UserProfileViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(UserProfileViewModel);
            });
            SettingsViewCommand = new RelayCommand(o =>
            {
                AnimateViewChange(SettingsViewModel);
            });
            UserProfileViewModel.LogOut=new RelayCommand( (o) =>
            {
                CurrentUser = null;
                AnimateViewChange(SignViewModel);
            });
        }
        private void AnimateViewChange(object newView)
        {
            ContentControl contentControl = Application.Current.MainWindow.FindName("contentControl") as ContentControl;
            

            // Создаем анимацию плавного исчезновения текущего представления
            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            // Создаем обработчик события завершения анимации и обновления представления
            fadeOutAnimation.Completed += (sender, e) =>
            {

                // Создаем анимацию плавного появления нового представления
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1)
                };

                // Применяем анимацию к новому представлению
                if (newView is UIElement view)
                {
                    view.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                }
            };

            // Применяем анимацию к текущему представлению
            contentControl.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
            CurrentView = newView;

        }
        public void ProfileChanger(Developer dev)
        {
            DeveloperInfViewModel= new DeveloperInfViewModel(dev);
            AnimateViewChange(DeveloperInfViewModel);
        }
    }
}
