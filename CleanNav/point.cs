using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETGeographicLib;

namespace CleanNav
{
    public class point
    {
        private
         int ti;
         double xi, yi, xx,yy;
         int zonei;
         bool north;

        public point(int t, double x, double y)
        {
            ti = t;
            xi = x;
            yi = y;
            int setzone = Convert.ToInt32(NETGeographicLib.UTMUPS.ZoneSpec.STANDARD);
            NETGeographicLib.UTMUPS.Forward(xi, yi, out zonei, out north, out xx, out yy, setzone, false);


        }

        public int t
        {
            get
            {
                return ti;
            }
            set
            {
                ti = value;
            }
        }

        public double x
        {
            get
            {
                return xi;
            }
            set
            {
                xi = value;
            }
        }

        public double y
        {
            get
            {
                return yi;
            }
            set
            {
                yi = value;
            }
        }
        public int zone
        {
            get
            {
                return zonei;
            }
            set
            {
                zonei = value;
            }
        }

        public int tsec()
        {
            // seconds since start of the year
            // assume dddhhmmss format on time code
            int t = ti;
            int days = t / 1000000;
            int r = t - days * 1000000;
            int hrs = r / 10000;
            r = r - hrs * 10000;
            int min = r / 100;
            int sec = r - min * 100;

            int tsec = sec + (min * 60) + hrs * 3600 + days * (3600 * 24);
            return tsec;

        }
        public double thr()
        {

            int ttt = tsec();

            return ttt/3600.0 ;

        }

        public double xm()
        {
            // caculate position using mean earth radius relative to 1 st point
            int setzone = zonei;
            NETGeographicLib.UTMUPS.Forward(xi, yi, out zonei, out north, out xx, out yy,setzone,false);

            return xx;
        }
        public double ym()
        {
            // caculate position using mean earth radius relative to 1 st point
            int setzone = zonei;
            NETGeographicLib.UTMUPS.Forward(xi, yi, out zonei, out north, out xx, out yy, setzone, false);
            return yy;
        }

    }
}
