using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TeamTasker
{
    public class Commands
    {
        public static RoutedUICommand Edit { get; set; }
        public static RoutedUICommand Exit { get; set; }
        public static RoutedUICommand Register { get; set; }
        public static RoutedUICommand GoToRegister { get; set; }
        public static RoutedUICommand SignUp { get; set; }
        public static RoutedUICommand GoToSignUp { get; set; }
        public static RoutedUICommand Save { get; set; }
        public static RoutedUICommand Cancel { get; set; }
        static Commands()
        {
            Edit = new RoutedUICommand("Edit", "Edit", typeof(Commands));
            Exit = new RoutedUICommand("Exit", "Exit", typeof(Commands));
            Register = new RoutedUICommand("Register", "Register", typeof(Commands));
            GoToRegister=new RoutedUICommand("GoToRegister","GoToRegister",typeof(Commands));
            SignUp = new RoutedUICommand("SignUp", "SignUp", typeof(Commands));
            GoToSignUp = new RoutedUICommand("GoToSignUp","GoToSignUp",typeof(Commands));
            Save = new RoutedUICommand("Save", "Save", typeof(Commands));
            Cancel = new RoutedUICommand("Cancel", "Cancel", typeof(Commands));
        }
    }
}
