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
            var login = LoginBox.Text;

            var email = emailbox.Text;

            var pass = passwordBox.Text;

            var password = passwordBox1.Text;

            var context = new AppDbContext();

            var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists is not null)
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован");
                return;
            }
            else if (login.Length == 0)
            {
                MessageBox.Show("Логин не может быть пустым");
            }
            else if (email.Length == 0)
            {
                MessageBox.Show("Email не может быть пустым");
            }
            else if  (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@(mail\.ru|gmail\.com|yandex\.ru)$"))

            {
                MessageBox.Show("Email не содержит домена.");
            }
            
            else if (password.Length  < 8 )
            {
                MessageBox.Show("Пороль не может быть меньше 8 символов");
            }
            else if (pass != password)
            {
                MessageBox.Show("Пороли не совпадают");
            }
            else
            {
                var user = new User { Login = login, Email = email, Password = pass };
                context.Users.Add(user);
                context.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались");
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
