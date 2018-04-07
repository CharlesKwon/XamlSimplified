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

namespace Sample._030_DataTemplate
{
    public class ExchangeMessage
    {
        public bool IsMine { get; set; }            // 메시지 구분자
        public string Name { get; set; }            // 발신자 이름
        public string Message { get; set; }         // 발신 내용
        public DateTime ConfirmedTime { get; set; } // 확인 시간
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
