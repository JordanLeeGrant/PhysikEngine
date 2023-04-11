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
        Canvas cAnimatioLayer;

        // GUI Element Declaration-------------------------------------------
        public View()
        {
            //Default Global Variable Settings//-----------------------------
            placementMode = false;
            //GUI Initialization//-------------------------------------------

            InitializeComponent();
            //Grid Settup//--------------------------------------------------
            cAnimatioLayer = new Canvas();
            cAnimatioLayer.MaxWidth = 980;
            cAnimatioLayer.MaxHeight = 430;
            cAnimatioLayer.Opacity = 100;
            cAnimatioLayer.Background = Brushes.White;

            cSimArea.Children.Add(cAnimatioLayer);
            //Timer setup for animation Frames//-----------------------------
            animationInterval.Interval = TimeSpan.FromMilliseconds(100);
            animationInterval.Tick += AnimationUpdate;
           /*Del later*/ animationInterval.Start();
        }
        private void AnimationUpdate(object sender, EventArgs e)
        {
            if (placementMode) { PlaceballUpdate(); }

        }
        private void PlaceballUpdate()
        {
            cAnimatioLayer.Children.Clear();
            int[] pos = CurrentMousePosition();
            //Labels--------------------------------------------------------- 
            if (withinCanvas(pos[0],pos[1])) { 
                lplacementX.Content = "|X| :" + pos[0];
                lplacementY.Content = "|Y| :" + pos[1];}
            else {
                lplacementX.Content = "|X| : Invalid";
                lplacementY.Content = "|Y| : Invalid";}

            //Ellipses-------------------------------------------------------
            CreateBall(pos[0],pos[1]);
        }
        private void bPlaceObject_Click(object sender, RoutedEventArgs e)
        {
            //Set flipflop for Update flag
            switch (placementMode) {
                case false:
                    placementMode = true;
                        break;
                case true:
                    placementMode=false;
                        break;
            }
        }
        private int[] CurrentMousePosition()
        {
            int[] mPosition = new int[] { (int)Mouse.GetPosition(cSimArea).X, (int)Mouse.GetPosition(cSimArea).Y };

            return mPosition;
        }
        private void CreateBox(int xpos,int ypos)
        {
        }
        private void CreateBall(int xpos, int ypos)
        {
            
            // Creates a circle Of desired size at Desired Location
            // Creating Object-----------------------------------------------
            Ellipse nextCircle  = new Ellipse();
            // Characteristics for the Ellipse-------------------------------
            nextCircle.Width = 100;
            nextCircle.Height = 100;
            nextCircle.Stroke = Brushes.Aquamarine;
            // Placement / Position------------------------------------------
            Canvas.SetLeft(nextCircle,xpos - nextCircle.Width / 2);
            Canvas.SetTop(nextCircle,ypos - nextCircle.Height / 2);
            // Add to Canvas-------------------------------------------------
            cAnimatioLayer.Children.Add(nextCircle);

        }
        private bool withinCanvas(int xCoord,int yCoord)
        {
            if (xCoord > cSimArea.Width | xCoord < 0) { return false; }
            if (yCoord > cSimArea.Width | yCoord < 0) { return false; }
            return true;
        }
    }
}
