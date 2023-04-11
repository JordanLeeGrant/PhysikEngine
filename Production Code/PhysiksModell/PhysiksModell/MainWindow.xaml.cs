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
using System.Windows.Threading;
namespace PhysiksModell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        private readonly DispatcherTimer animationInterval = new DispatcherTimer();
        bool placementMode;
        public View()
        {
            //Default Global Variable Settings//-----------------------------
            placementMode = false;
            //GUI Initialization//-------------------------------------------
            InitializeComponent();
            //Grid Settup//----------------------------------------------
            
            //Timer setup for animation Frames//-----------------------------
            animationInterval.Interval = TimeSpan.FromMilliseconds(100);
            animationInterval.Tick += AnimationUpdate;
        }
        private void AnimationUpdate(object sender, EventArgs e)
        {


        }

        private void buttonPlaceBall_Click(object sender, RoutedEventArgs e)
        {
            if (placementMode != true) { return; }


        }
        private void CreateBox()
        {
           // Creates A desired rectangle of desired size at desired Location
            
        }
        private void CreateBall()
        {

        }
    }
}
