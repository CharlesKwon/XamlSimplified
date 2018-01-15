using System.Collections.Generic;
using System.Windows;

namespace Sample._006_ClassModel
{
    public class Person
    {
        public string RealName { get; set; }
        public int Age { get; set; }
    }

    public class Family : List<Person>
    {
        public Family()
        {
            Add(new Person() { RealName = "길동", Age = 40 });
            Add(new Person() { RealName = "철수", Age = 30 });
            Add(new Person() { RealName = "영희", Age = 20 });
        }
    }

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
