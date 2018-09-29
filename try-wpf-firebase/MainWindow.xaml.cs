using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MySchool_Firebase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFirebaseConfig _config;
        private IFirebaseClient _client;

        public MainWindow()
        {
            InitializeComponent();

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "27hUj3bDEkV4CccU0ugZdEM6QTJEzUpikO06ch4X",
                BasePath = "https://my-school-8bb61.firebaseio.com/",
            };

            _config = config;

            this.Loaded += MainWindow_Loaded;
            btSet.Click += btSet_Click;
            btPush.Click += btPush_Click;
            btGet.Click +=btGet_Click;
            btUpdate.Click += btUpdate_Click;
            btDelete.Click += btDelete_Click;
        }
        async void btGet_Click(object sender, RoutedEventArgs e)
        {
            string studentID = tbID.Text;

            FirebaseResponse response = await _client.GetAsync("students/" + tbID.Text);

            Student student = response.ResultAs<Student>();

            lbResponse.Text = string.Format("Found {0} {1} {2} in realtime database",student.ID,student.Name,student.SureName);
        }

        async void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            var student = GetStudent();

            FirebaseResponse response = await _client.UpdateAsync("students/" + student.ID, student);

            lbResponse.Text = String.Format("Updated {0}",response.ResultAs<Student>().Name);
        }

        async void btPush_Click(object sender, RoutedEventArgs e)
        {
            var student = GetStudent();

            PushResponse response = await _client.PushAsync("students/", student);

            lbResponse.Text = response.Result.name;
        }

        async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            FirebaseResponse response = await _client.DeleteAsync("students/" + tbID.Text);

            lbResponse.Text = response.StatusCode.ToString();
        }

        async void btSet_Click(object sender, RoutedEventArgs e)
        {

            var student = GetStudent();

            SetResponse response = await _client.SetAsync("students/"+student.ID,student);

            lbResponse.Text = String.Format("Added {0}",response.ResultAs<Student>().Name);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _client = new FirebaseClient(_config);
        }

        Student GetStudent()
        {
            return new Student(Convert.ToInt32(tbID.Text), tbName.Text, tbSername.Text);
        }

        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string SureName { get; set; }

            public Student()
            {

            }

            public Student(int id,string name,string surename):this()
            {
                ID = id;
                Name = name;
                SureName = surename;
            }
        }
    }
}
