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

namespace Sample._008_DeriveStyle
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Style Style_ReadOnlyTextBox = new Style();
            Style_ReadOnlyTextBox.TargetType = typeof(TextBox);
            Style_ReadOnlyTextBox.BasedOn = (Style)this.grdTest.FindResource("Style_ControlBase");
            Setter setterIsReadOnly = new Setter(TextBox.IsReadOnlyProperty, true);
            Setter setterBackgroundS = new Setter(TextBox.BackgroundProperty, Brushes.LightGray);
            Style_ReadOnlyTextBox.Setters.Add(setterIsReadOnly);
            Style_ReadOnlyTextBox.Setters.Add(setterBackgroundS);
            this.txbTest.Style = Style_ReadOnlyTextBox;

            //this.txbTest.Style = (Style)this.grdTest.FindResource("Style_ReadOnlyTextBox");
        }
    }
}
