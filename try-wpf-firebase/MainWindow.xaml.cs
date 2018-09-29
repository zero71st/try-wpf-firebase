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
            btDelete.Click += btDelete_Click;
        }

        async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            FirebaseResponse response = await _client.DeleteAsync("students/" + tbID.Text);

            
        }

        async void btSet_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student
            {
                ID = tbID.Text,
                Name = tbName.Text,
                SureName = tbSername.Text,
            };

            SetResponse response = await _client.SetAsync("students/"+student.ID,student);

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _client = new FirebaseClient(_config);

        }

        public class Student
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string SureName { get; set; }
        }
    }
}
