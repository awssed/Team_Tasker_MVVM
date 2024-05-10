using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore.Query;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamTasker.Core;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    class AddTaskViewModel
    {
        private UnityOfWorkClass db=new UnityOfWorkClass();

        public AddTaskViewModel( Project currentProject)
        {
            
            CurrentProject = currentProject;
            CurrentTask = new Models.Task();
            CurrentTask.Project = currentProject;
            AddTask = new RelayCommand(Add, CanAdd);
        }
        public AddTaskViewModel()
        { }
        public Project CurrentProject { get; set; }
        public Models.Task CurrentTask { get; set; }
        public RelayCommand AddTask { get; set; }
        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
        private void Add(object parametr)
        {
            try
            {
                db.Tasks.Create(CurrentTask);
                db.Save();
                //SendTaskToDeveloper(CurrentTask.Developer.Email, CurrentTask.Name, CurrentTask.Project.Name);
                CloseWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanAdd(object parametr)
        {
            string error = CurrentTask["Name"]; 
            error += CurrentTask["EndTime"];
            error += CurrentTask["Description"];
            if (string.IsNullOrEmpty(error))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async void SendTaskToDeveloper(string email, string task, string project)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администратор", "mail"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Новый пользователь";
            string textBody = $"У вас появилась новая задача {task} в проекте {project}";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = textBody
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("mail", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
