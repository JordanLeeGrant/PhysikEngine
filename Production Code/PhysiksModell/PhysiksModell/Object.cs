using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PhysiksModell
{
    enum Material
    {
        Steel,
        Wood,
        Rubber
    }
    internal abstract class Object
    {
        private Material _material;
        private Vector2 _position;
        private List<int> _size;

        public void SetPosition(int X,int Y)
        {
            Position = new Vector2(X,Y);
        }
        public Vector2 Position { get => _position; set => _position = value; }
        internal Material Material1 { get => _material; set => _material = value; }
        public List<int> Size { get => _size; set => _size = value; }
    }
    internal abstract class DynamicObject : Object
    {
       
        private Vector2 _velocity,_acceleration;
        private List<Vector2> _influences;
        private float _deltat;
        // Velocity ------------------------------------------------------------------
        //Calculate actual V
        public Vector2 NextVelocity()
        {
            Vector2 influence = new Vector2();
            foreach (var item in Influences)
            {
                influence += item;
            }
            Vector2 truevelocity = Velocity + influence;
            return truevelocity;
        }
        // Calculate V
        public void AddInfluenceVector (Vector2 a)
        {
            
            // Berechnung der Geschwindigkeit des sich bewegenden Objektes

            Vector2 newV = new Vector2();
           
            newV = Velocity + a * Deltat;

            Influences.Add(newV);
        }
        
        //Distance--------------------------------------------------------------------
        //Calculate S
        public Vector2 NextDistance(Vector2 a,float t)
        {
            Vector2 S = new Vector2();
            S = Position + Velocity * t + 0.5f * a* (float)Math.Pow(t,2);
            return S;
        }
        //GetSet----------------------------------------------------------------------
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public List<Vector2> Influences { get => _influences; set => _influences = value; }
        public Vector2 Acceleration { get => _acceleration; set => _acceleration = value; }
        public float Deltat { get => _deltat; set => _deltat = value; }
    }
    internal class Ball : DynamicObject
    {

        public Ball(Vector2 startposition, Vector2 startvelocity, List<int> size,float t) {
            Velocity = startvelocity;
            Position = startposition;
            Size = size;
            Deltat = t;
            Influences = new List<Vector2>();
            //Erdanziehung in mm/S
            AddInfluenceVector(new Vector2(0,9810f));
        }
        
        public void BallUpdate()
        {
            Vector2 v = NextVelocity();
            Vector2 s = NextDistance(Acceleration, 0.02f);

            Velocity = v;
            SetPosition((int)s.X,(int)s.Y);

        }

    }
}
