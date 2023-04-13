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
        // Timers-------------------------------------------------------------
        private readonly DispatcherTimer animationInterval = new DispatcherTimer();
        // Flags--------------------------------------------------------------
        bool placementMode,placementClick;
        // Object Tracking Lists ---------------------------------------------
        List<Ellipse> ballsInPlay;
        List<Rectangle> squaresInPlay;
        // GUI Elements ------------------------------------------------------
        Canvas cPlacementLayer;

        public View()
        {
            //Default Global Variable Settings//-----------------------------
            placementMode = false;
            placementClick = false;
            ballsInPlay = new List<Ellipse>();
            squaresInPlay = new List<Rectangle>();
            //GUI Initialization//-------------------------------------------

            InitializeComponent();
            //Grid Settup//--------------------------------------------------
            cPlacementLayer = new Canvas();
            
            cPlacementLayer.Background = Brushes.White;

            cSimArea.Children.Add(cPlacementLayer);
            //Timer setup for animation Frames//-----------------------------
            animationInterval.Interval = TimeSpan.FromMilliseconds(10);
            animationInterval.Tick += AnimationUpdate;
           /*Del later*/ animationInterval.Start();
        }
        private void AnimationUpdate(object sender, EventArgs e)
        {
            //Flag check-----------------------------------------------------
            if (placementMode) { PlaceballUpdate(); }

        }
        private void PlaceballUpdate()
        {
            if (placementClick) 
            { }
            cPlacementLayer.Children.Clear();
            
            int[] pos = CurrentMousePosition();
            bool oncanvas = WithinCanvas(pos[0], pos[1]);
            //Labels--------------------------------------------------------- 
            if (oncanvas) { 
                lplacementX.Content = "|X| :" + pos[0];
                lplacementY.Content = "|Y| :" + pos[1];}
            else {
                lplacementX.Content = "|X| : Invalid";
                lplacementY.Content = "|Y| : Invalid";}

            //Ellipses-------------------------------------------------------
            if (oncanvas)
            {
                CreateBall(pos[0], pos[1]);
            }

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
            // Characteristics of the Ellipse-------------------------------
            nextCircle.Width = 50;
            nextCircle.Height = 50;
            nextCircle.Stroke = Brushes.Aquamarine;
            nextCircle.Fill = Brushes.Black;
            // Placement / Position------------------------------------------
            Canvas.SetLeft(nextCircle,xpos - nextCircle.Width / 2);
            Canvas.SetTop(nextCircle,ypos - nextCircle.Height / 2);
            // Add to Canvas-------------------------------------------------
            cPlacementLayer.Children.Add(nextCircle);

        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (!placementMode) { return; }
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            if (placementMode)
            {
                placementMode = false; 
                cPlacementLayer.Children.Clear();
            }
        }

        private bool WithinCanvas(int xCoord,int yCoord)
        {
            if (xCoord > cSimArea.Width | xCoord < 0) { return false; }
            if (yCoord > cSimArea.Width | yCoord < 0) { return false; }
            return true;
        }
    }
}
