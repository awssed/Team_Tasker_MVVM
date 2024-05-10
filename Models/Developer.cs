using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TeamTasker.Models
{
    public enum Position
    {
        Junior,
        Middle,
        Senior
    }

    public class Developer : IDataErrorInfo
    {
        public string DeveloperId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public Position Position { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public bool isAdmin { get; set; }
        public virtual ICollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
        public virtual ICollection<Models.Task> Tasks { get; set; }=new ObservableCollection<Models.Task>();

        public Developer(string login, string name, Position position, string email, string password, byte[] image, bool isAdmin)
        {
            DeveloperId = login;
            Name = name;
            Position = position;
            Image = image;
            Email = email;
            Password = password;
            this.isAdmin = isAdmin;
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "DeveloperId":
                        if(string.IsNullOrEmpty(DeveloperId))
                        {
                            error = "Invalid login";
                        }
                        break;
                    case "Name":
                        Regex regex = new Regex(@"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$");
                        if (!regex.IsMatch(Name))
                        {
                            error = "Incorrect name";
                        }
                        break;
                    case "Email":
                        // Дополнительная проверка валидности email
                        if (!IsValidEmail(Email))
                        {
                            error = "Invalid email";
                        }
                        break;
                    case "Login":
                        if (string.IsNullOrWhiteSpace(DeveloperId))
                        {
                            error = "Invalid login";
                        }
                        break;
                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            error = "Invalid password";
                        }
                        break;
                }
                return error;
            }
        }

        [JsonIgnore]
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public Developer(string login, string name, string password)
        {
            DeveloperId = login;
            Name = name;
            Password = password;
            Image = null;
            Email = "@email.com";
            isAdmin = false;
            using (WebClient webClient = new WebClient())
            {
                Image = webClient.DownloadData("D:\\Учёба\\4сем\\ОПП\\TeamTasker\\Images\\defaultPerson.png");

                // Теперь у вас есть массив байт с данными изображения
                // Можно выполнить необходимую с ними обработку
            }
        }
        public Developer(string login, string name, Position position, string password)
        {
            DeveloperId = login;
            Name = name;
            Position = position;
            Password = password;
        }

        public Developer()
        {
            DeveloperId = "";
            Name = String.Empty;
            Position = Position.Junior;
            Email = "@email.com";
            isAdmin = false;
            using (WebClient webClient = new WebClient())
            {
                Image = webClient.DownloadData("D:\\Учёба\\4сем\\ОПП\\TeamTasker\\Images\\defaultPerson.png");

                // Теперь у вас есть массив байт с данными изображения
                // Можно выполнить необходимую с ними обработку
            }
        }

        public Developer(string developerId, string name, Position position, string email, string password) : this(developerId, name, position, email)
        {
            DeveloperId = developerId;
            Name = name;
            Position = position;
            Email = email;
            Password = password;
            isAdmin=false;
            using (WebClient webClient = new WebClient())
            {
                Image = webClient.DownloadData("D:\\Учёба\\4сем\\ОПП\\TeamTasker\\Images\\defaultPerson.png");

                // Теперь у вас есть массив байт с данными изображения
                // Можно выполнить необходимую с ними обработку
            }
        }
        public Developer(string developerId, string name, Position position, string email, string password,bool isadmin) : this(developerId, name, position, email)
        {
            DeveloperId = developerId;
            Name = name;
            Position = position;
            Email = email;
            Password = password;
            isAdmin = isadmin;
            using (WebClient webClient = new WebClient())
            {
                Image = webClient.DownloadData("D:\\Учёба\\4сем\\ОПП\\TeamTasker\\Images\\defaultPerson.png");

                // Теперь у вас есть массив байт с данными изображения
                // Можно выполнить необходимую с ними обработку
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
            
    }
}