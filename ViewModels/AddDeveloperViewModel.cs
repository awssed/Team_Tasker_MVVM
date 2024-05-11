using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamTasker.Core;
using TeamTasker.EntityModels;
using TeamTasker.Models;
using TeamTasker.UnityOfWork;

namespace TeamTasker.ViewModels
{
    class AddDeveloperViewModel:ObservableObject
    {
        private UnityOfWorkClass bd=new UnityOfWorkClass();
        public delegate void ViewChanger();

        public static event ViewChanger Changer;
        public RelayCommand AddDeveloperCommand { get; set; } 
        private Developer _developer;
        
        public Developer Developer
        {
            get { return _developer; }
            set {
                _developer = value; 
                OnPropertyChanged();
            }
        }
        public AddDeveloperViewModel()
        {
            Developer = new Developer();
            AddDeveloperCommand=new RelayCommand(AddDeveloper,CanAddDeveloper);
        }
        private void AddDeveloper(object parametr)
        {
            try
            {
                Developer.Salt = Developer.CreateSalt(Developer.Password);
                Developer.HashPassword = Developer.CreateHash(Developer.Concat(Developer.Password, Convert.FromBase64String(Developer.Salt)));
                bd.Developers.Create(Developer);
                //SendInfoToDeveloper(Developer.Email, Developer.DeveloperId, Developer.Password);
                Developer=new Developer();
                Changer?.Invoke();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    MessageBox.Show("Данный логин уже занят.");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanAddDeveloper(object parametr)
        {
            string error = Developer["Name"];
            error += Developer["DeveloperId"];
            error += Developer["Password"];
            error += Developer["Email"];
            if (string.IsNullOrEmpty(error))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async void SendInfoToDeveloper(string email,string login, string password)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администратор", "mail"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Новый пользователь";
            string textBody = $"Вы были добавлены в систему TeamTasker!\n Ваш логин: {login}\n Ваш пароль: {password}\n Удачи!";
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
