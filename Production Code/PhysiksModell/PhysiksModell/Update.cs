using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysiksModell
{
    internal abstract class Update
    {

    }
    internal class PreviewBallUpdate:Update
    {
        Ball subject;
        public void BallUpdate(Ball ball)
        {
            subject = ball;
        }  


    }
    
}
