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
        bool placementMode,placementClick,simActive;
        List<UpdateBase> updates = new List<UpdateBase>();
        // Object Tracking Lists ---------------------------------------------
        List<Ellipse> ballsInPlay;
        List<Rectangle> squaresInPlay;
        // GUI Elements ------------------------------------------------------
        Canvas cPreviewLayer,cPlacementLayer;

        public View()
        {
            //Default Global Variable Settings//-----------------------------
            placementMode = false;
            placementClick = false;
            simActive = false;
            ballsInPlay = new List<Ellipse>();
            squaresInPlay = new List<Rectangle>();
            //GUI Initialization//-------------------------------------------

            InitializeComponent();
            //Grid Settup//--------------------------------------------------
            cPreviewLayer = new Canvas();
            cPlacementLayer = new Canvas();

            cSimArea.Children.Add(cPlacementLayer);
            cSimArea.Children.Add(cPreviewLayer);
            //Timer setup for animation Frames//-----------------------------
            animationInterval.Interval = TimeSpan.FromMilliseconds(10);
            animationInterval.Tick += AnimationUpdate;
            animationInterval.Start();
        }
        public abstract class UpdateBase
        {
            public abstract void Update();
        }
        public class PlaceballUpdate : UpdateBase
        {
            private readonly View view;

            public PlaceballUpdate(View view)
            {
                this.view = view;
            }
            public override void Update()
            {
                view.cPreviewLayer.Children.Clear();

                int[] pos = view.CurrentMousePosition();
                bool oncanvas = view.WithinCanvas(pos[0], pos[1]);
                //Labels--------------------------------------------------------- 
                if (oncanvas)
                {
                    view.lplacementX.Content = "|X| :" + pos[0];
                    view.lplacementY.Content = "|Y| :" + pos[1];
                }
                else
                {
                    view.lplacementX.Content = "|X| : Invalid";
                    view.lplacementY.Content = "|Y| : Invalid";
                }

                //Ellipses-------------------------------------------------------
                if (oncanvas)
                {
                    if (!view.WithinCanvas(pos[0] - 25, pos[1] - 25)) { return; }
                    if (!view.WithinCanvas(pos[0] + 25, pos[1] + 25)) { return; }
                    view.CreateBall(pos[0], pos[1]);
                }
            }
        }
        private void AnimationUpdate(object sender, EventArgs e)
        {
            lock (updates)
            {
                foreach (var item in updates)
                {
                    item.Update();
                }
                updates.Clear();
            }
        }
       
        private void bPlaceObject_Click(object sender, RoutedEventArgs e)
        {
            //Set flipflop for Update flag
            switch (placementMode)
            {
                case false:
                    placementMode = true;
                    break;
                case true:
                    placementMode = false;
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
            if (placementMode) 
            {cPreviewLayer.Children.Add(nextCircle);}

        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (!placementMode) { return; }
            CreateBall(1, 1);
        }

        private void MouseMoveUpdate(object sender, MouseEventArgs e)
        {
            if (!placementMode) { return; }
            PlaceballUpdate update = new PlaceballUpdate(new View());
            updates.Add(update);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (simActive)
            {
                case false:
                    simActive = true;
                    bStartStop.Content = "Stop";
                    break;
                case true:
                    simActive = false;
                    bStartStop.Content = "Start";
                    break;
            }
        }

        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            if (placementMode)
            {
                placementMode = false; 
                cPreviewLayer.Children.Clear();
            }
        }

        private bool WithinCanvas(int xCoord,int yCoord)
        {
            if (xCoord > cSimArea.Width | xCoord < 0) { return false; }
            if (yCoord > cSimArea.Height | yCoord < 0) { return false; }
            return true;
        }
    }
}
