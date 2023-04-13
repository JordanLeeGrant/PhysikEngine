using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysiksModell
{
    /// <summary>
    ///  Logic for the underlying Model
    /// </summary>
    internal class Controller
    {
        Vector gravityAccelerationConstant,windAccelerationConstant;
        
        public Controller()
        {
            gravityAccelerationConstant = new Vector(0,9.8);
            windAccelerationConstant = new Vector(0, 0);
        }
        public Vector ObjectAcceleration(object subject)
        {
            return new Vector();
        }
    }
}
