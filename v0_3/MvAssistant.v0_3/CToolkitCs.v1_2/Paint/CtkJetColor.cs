using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Paint
{
    public class CtkJetColor
    {


        /*
   Return a RGB colour value given a scalar v in the range [vmin,vmax]
   In this case each colour component ranges from 0 (no contribution) to
   1 (fully saturated), modifications for other ranges is trivial.
   The colour is clipped at the end of the scales if v is outside
   the range [vmin,vmax]
*/



        public static Color Color(double v, double vmin, double vmax)
        {
            double r = 1, g = 1, b = 1;
            double dv;

            if (v < vmin)
                v = vmin;
            if (v > vmax)
                v = vmax;
            dv = vmax - vmin;

            if (v < (vmin + 0.25 * dv))
            {
                r = 0;
                g = 4 * (v - vmin) / dv;
            }
            else if (v < (vmin + 0.5 * dv))
            {
                r = 0;
                b = 1 + 4 * (vmin + 0.25 * dv - v) / dv;
            }
            else if (v < (vmin + 0.75 * dv))
            {
                r = 4 * (v - vmin - 0.5 * dv) / dv;
                b = 0;
            }
            else
            {
                g = 1 + 4 * (vmin + 0.75 * dv - v) / dv;
                b = 0;
            }

            return System.Drawing.Color.FromArgb(
             (int)(r * 255),
             (int)(g * 255),
             (int)(b * 255));
        }
    }
}
