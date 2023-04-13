using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysiksModell
{
    /// <summary>
    /// Vector Struct
    /// Origin of the Vector ALWAYS the Canvas origin
    /// </summary>
    internal struct Vector
    {
        public Vector(double x, double y){
            X = x;
            Y = y; 
        }
        public double X { get;}
        public double Y { get;}
    }
}
