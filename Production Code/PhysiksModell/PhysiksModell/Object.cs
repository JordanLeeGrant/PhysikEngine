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
       
        private Vector2 _velocity;
        private List<Vector2> _influences;
        // Velocity ------------------------------------------------------------------
        //Calculate actual V
        private Vector2 NextVelocity()
        {
            Vector2 truevelocity = new Vector2();
            foreach (var item in Influences)
            {
                truevelocity += item;
            }

            return truevelocity;
        }
        // Calculate V
        public void AddInfluenceVector (Vector2 a, float t)
        {
            
            // Berechnung der Geschwindigkeit des sich bewegenden Objektes

            Vector2 newV = new Vector2();
           
            newV = Velocity + a * t;

            Influences.Add(newV);
        }
        //Set V0
        private void UpdateVelocity()
        {
            Velocity = NextVelocity();
        }
        //Distance--------------------------------------------------------------------
        //Calculate S
        private Vector2 NextDistance(Vector2 a,float t)
        {
            Vector2 S = new Vector2();
            S = Position + Velocity * t + 0.5f * a* (float)Math.Pow(t,2);
            return S;
        }
        //Set S0
        private void UpdatePosition()
        {
            
        }
        //GetSet----------------------------------------------------------------------
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public List<Vector2> Influences { get => _influences; set => _influences = value; }
    }
    internal class Ball : DynamicObject
    {

        public Ball(Vector2 startposition, Vector2 startvelocity, List<int> size,float t) {
            Velocity = startvelocity;
            Position = startposition;
            Size = size;
            Influences = new List<Vector2>();
            AddInfluenceVector(new Vector2(0,9.85f),1);
        }
        
        public void BallUpdate()
        {
            
        }

    }
}
