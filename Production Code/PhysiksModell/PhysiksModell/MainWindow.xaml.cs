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
            //Grid Settup//--------------------------------------------------
            
            //Timer setup for animation Frames//-----------------------------
            animationInterval.Interval = TimeSpan.FromMilliseconds(100);
            animationInterval.Tick += AnimationUpdate;
        }
        private void AnimationUpdate(object sender, EventArgs e)
        {
            if (!placementMode){
                animationInterval.Stop();
                return;
            }
            int[] pos = CurrentMousePosition();
            //Labels--------------------------------------------------------- 
            //Check if on Canvas for X
            if (pos[0] < 0) { labelXCoord.Content = "|X| : Invalid"; }
            else if (pos[0] > canvasSimulationSpace.ActualWidth) { labelXCoord.Content = "|X| : Invalid"; }
            else { labelXCoord.Content = "|X| :" + pos[0]; }
            //Check if on Canvas for Y
            if (pos[1] < 0) { labelYCoord.Content = "|Y| : Invalid"; }
            else if(pos[1] > canvasSimulationSpace.ActualHeight) { labelYCoord.Content = "|Y| : Invalid"; }
            else { labelYCoord.Content = "|Y| :" + pos[1]; }

        }

        private void buttonPlaceBall_Click(object sender, RoutedEventArgs e)
        {
            placementMode = true;
            animationInterval.Start();
        }
        private int[] CurrentMousePosition()
        {
            int[] mPosition = new int[] { (int)Mouse.GetPosition(canvasSimulationSpace).X, (int)Mouse.GetPosition(canvasSimulationSpace).Y };

            return mPosition;
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
