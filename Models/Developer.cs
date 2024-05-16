using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
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
        private string _password;
        [NotMapped]
        public string Password 
        {
            get
            {
                return _password;
            }
            set
            {
                _password=value;
            }
        }
        private string _hashPassword;
        public string HashPassword
        {
            get { return _hashPassword; }
            set { _hashPassword = value; }
        }
        private string _salt;
        public string Salt
        {
            get
            {
                return _salt;
            }
            set
            {
                _salt= value;
            }
        }
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
                        if (Password != null)
                        {
                            if (Password.Length < 3)
                            {
                                error = "Зassword must be longer than 3 characters";
                            }
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
        public static byte[] Concat(string password, byte[] saltBytes)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return passwordBytes.Concat(saltBytes).ToArray();

        }
        public static string CreateSalt(string password)
        {
            var saltBytes = new byte[32];
            new Random().NextBytes(saltBytes);
            string Salt = Convert.ToBase64String(saltBytes);
            return Salt;
        }
        public static string CreateHash(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
            {
                return
            Convert.ToBase64String(sha256.ComputeHash(bytes));
            }
        }
        public static bool Verify(string salt, string hash, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var passwordAndSaltBytes = Concat(password, saltBytes);
            var hashAttempt = CreateHash(passwordAndSaltBytes);
            return hash == hashAttempt;
        }
       
    }
}