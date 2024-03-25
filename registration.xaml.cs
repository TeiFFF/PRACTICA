using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace praktik
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        public registration()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            erlog.Text = "";
            ermail.Text = "";
            erpass.Text = "";
            erpass2.Text = "";
            Login.BorderBrush = new SolidColorBrush(Colors.Gray);
      Email.BorderBrush = new SolidColorBrush(Colors.Gray);
            Password.BorderBrush = new SolidColorBrush(Colors.Gray);
            Password1.BorderBrush = new SolidColorBrush(Colors.Gray);

            int errors = 0;


            var login = Login.Text;

            var email = Email.Text;

            var pass = Password.Text;

            var password = Password1.Text;

            var context = new AppDbContext();

            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists is not null)
            {
                succsefull.Text = ("Пользователь с таким логином уже зарегистрирован");
                return;
            }
            do
            {
                if (login.Length == 0)
                {
                    erlog.Text = ("Логин не может быть пустым");
                    Login.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors++;
                }
                if (email.Length == 0)
                {
                    ermail.Text = ("Email не может быть пустым");
                    Email.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors++;
                }
                if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@(mail\.ru|gmail\.com|yandex\.ru)$"))
                {
                    Email.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors++;
                }


                if (email.Length == 0)
                {

                    ermail.Text = ("Email не содержит домена.");
                    Email.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors++;
                }

                if (password.Length < 8)
                {
                    erpass.Text = ("Пороль не может быть меньше 8 символов");
                    Password.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors++;
                }
                if (password != password)
                {
                    erpass2.Text = ("Пороли не совпадают");
                    Password1.BorderBrush = new SolidColorBrush(Colors.Red);
                    errors++;
                }

                break;
            }
            while (errors != 0);

            if (errors == 0)
            {
                var user = new User { Login = login, Email = email, Password = pass };
                context.Users.Add(user);
                context.SaveChanges();
                succsefull.Text = ("Вы успешно зарегистрировались");
            }
        }
            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                this.Hide();
                MainWindow MainWindow = new MainWindow();
                MainWindow.Show();
            }
        }
    }

