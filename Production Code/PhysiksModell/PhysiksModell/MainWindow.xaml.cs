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
using System.Numerics;
using System.Text.RegularExpressions;

namespace PhysiksModell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        // Timers-------------------------------------------------------------
        private readonly DispatcherTimer animationInterval = new DispatcherTimer();
        private readonly DispatcherTimer simulationInterval = new DispatcherTimer();
        // Flags--------------------------------------------------------------
        bool placementMode, placementClick, simActive;
        // List<UpdateBase> updates = new List<UpdateBase>();
        // Object Tracking Lists ---------------------------------------------
        List<Ball> ballsInPlay;
        List<Rectangle> squaresInPlay;
        // GUI Elements ------------------------------------------------------
        Canvas cPreviewLayer, cPlacementLayer;
        public View()
        {
            //Default Global Variable Settings//-----------------------------
            placementMode = false;
            placementClick = false;
            simActive = false;
            ballsInPlay = new List<Ball>();
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
            //Timer setup for Simulation cycles//----------------------------
            simulationInterval.Interval = TimeSpan.FromMilliseconds(20);
            simulationInterval.Tick += SimulationUpdate;

        }
        /*
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
        */
        private void AnimationUpdate(object sender, EventArgs e)
        {
            /*
             lock (updates)
             {
                 foreach (var item in updates)
                 {
                     item.Update();
                 }
                 updates.Clear();
             }
            */
            if (placementMode) { PlacePreview(); }
            if (simActive) { PlaceSimObj(); }
        }
        private void SimulationUpdate(object sender, EventArgs e)
        {
            if (simActive) {
                foreach (var item in ballsInPlay)
                {
                    item.BallUpdate();
                }
            }
        }
        private void PlacePreview()
        {
            cPreviewLayer.Children.Clear();

            int[] pos = CurrentMousePosition();
            bool oncanvas = WithinCanvas(pos[0], pos[1]);
            //Labels--------------------------------------------------------- 
            if (oncanvas)
            {
                lplacementX.Content = "|X| :" + pos[0];
                lplacementY.Content = "|Y| :" + pos[1];
            }
            else
            {
                lplacementX.Content = "|X| : Invalid";
                lplacementY.Content = "|Y| : Invalid";
            }

            //Ellipses-------------------------------------------------------
            if (oncanvas)
            {
                if (!WithinCanvas(pos[0] - 25, pos[1] - 25)) { return; }
                if (!WithinCanvas(pos[0] + 25, pos[1] + 25)) { return; }
                List<int> sizevalues = new List<int>();
                sizevalues.Add(50);
                float accinX = (float)Convert.ToDecimal(tbAccellerationX.Text);
                float accinY = (float)Convert.ToDecimal(tbAccellerationY.Text);
                Ball previewball = new Ball(new Vector2(pos[0], pos[1]), new Vector2(accinY, accinX), sizevalues,0.01f);
                cPreviewLayer.Children.Add(CreateBall(previewball));
            }
        }
        private void PlaceInSim()
        {
            int[] pos = CurrentMousePosition();
            bool oncanvas = WithinCanvas(pos[0], pos[1]);

            if (oncanvas)
            {
                if (!WithinCanvas(pos[0] - 25, pos[1] - 25)) { return; }
                if (!WithinCanvas(pos[0] + 25, pos[1] + 25)) { return; }
                List<int> sizevalues = new List<int>();
                sizevalues.Add(50);
                float accinX = (float)Convert.ToDecimal(tbAccellerationX.Text);
                float accinY = (float)Convert.ToDecimal(tbAccellerationY.Text);
                Ball previewball = 
                    new Ball(
                        new Vector2(pos[0], pos[1]), 
                        new Vector2(accinX, accinY), 
                        sizevalues,0.02f);
                cPlacementLayer.Children.Add(CreateBall(previewball));
                ballsInPlay.Add(previewball);
            }
            
        }
        private void PlaceSimObj()
        {
            cPlacementLayer.Children.Clear();
            //Rectangles
            //Ellipses
            foreach (var item in ballsInPlay)
            {
                cPlacementLayer.Children.Add(CreateBall(item));
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
        private void CreateBox(int xpos, int ypos)
        {
        }
        private Ellipse CreateBall(Ball ellipse)
        {

            // Creates a circle Of desired size at Desired Location
            // Creating Object-----------------------------------------------
            Ellipse nextCircle = new Ellipse();
            // Characteristics of the Ellipse-------------------------------
            nextCircle.Width = ellipse.Size[0];
            nextCircle.Height = ellipse.Size[0];
            nextCircle.Stroke = Brushes.White;
            nextCircle.Fill = Brushes.Blue;
            // Placement / Position------------------------------------------
            if (WithinCanvas((int)ellipse.Position.X, (int)ellipse.Position.Y))
            {
                Canvas.SetLeft(nextCircle, ellipse.Position.X - nextCircle.Width / 2);
                Canvas.SetTop(nextCircle, ellipse.Position.Y - nextCircle.Height / 2);
            }
            else 
            {
                ellipse.SetPosition((int)cSimArea.Width / 2, (int)cSimArea.Height / 2);
            }
          
            return nextCircle;

        }
        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (!placementMode) { return; }
            PlaceInSim();
        }
        private void MouseMoveUpdate(object sender, MouseEventArgs e)
        {
            /* if (!placementMode) { return; }
             PlaceballUpdate update = new PlaceballUpdate(new View());
             updates.Add(update);*/
        }
        private void bReset_Click(object sender, RoutedEventArgs e)
        {
            cPlacementLayer.Children.Clear();
        }
        private void tbAccellerationNumCheck(object sender, TextCompositionEventArgs e)
        {
            //Überprüfung mit einer Rgex ob der user eine Zahl eingegeben hat
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void tbAccellerationX_GotFocus(object sender, RoutedEventArgs e)
        {
            tbAccellerationX.Text = "";
        }
        private void tbAccellerationY_GotFocus(object sender, RoutedEventArgs e)
        {
            tbAccellerationY.Text = "";
        }
        private void tbAccelerationY_KeyUp(object sender, KeyEventArgs e)
        {
            if (placementMode)
            {
                
            }
        }
        private void tbAccelerationX_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void bStartStop_Click(object sender, RoutedEventArgs e)
        {
            switch (simActive)
            {
                case false:
                    simActive = true;
                    bStartStop.Content = "Stop";
                    simulationInterval.Start();
                    break;
                case true:
                    simActive = false;
                    bStartStop.Content = "Start";
                    simulationInterval.Stop();
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
            if (yCoord > cSimArea.Width | yCoord < 0) { return false; }
            return true;
        }

    }
}
