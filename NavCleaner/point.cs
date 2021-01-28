using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETGeographicLib;

namespace NavCleaner
{
    public class point
    {
       long ti; // coded time DDDHHMMSSmmm with millisecond accuracy
       double xi, yi, xx,yy;
       int zonei;
       bool north;
       public bool active;
       int iyear;
       double gamma;
       int iTraceNumber;

        // projection parameters for Transverse meractor

       double referenceMeridian;
       double a = 6378137;
       double f = 298.257222101;
       double k1 = 1;

        public point(long t, double x, double y, bool polar, double _referenceMeridian )
        {
            ti = t;
            xi = x;
            yi = y;
            zonei = zone;
            active = true;
            referenceMeridian = _referenceMeridian;

            if ( x > -360.0 && x <= 360.0 && y >= -90.0 && y  <= 90.0)
            { 
                NETGeographicLib.TransverseMercator p = new TransverseMercator(a,f,k1); 
                p.Forward(referenceMeridian, y,x, out xx, out yy);
            } else {
                xx = x;
                yy = y;
            }

        }

        // create an interpolated point between point a and b at time t
        public point interpolatedPoint( point p1, point p2, long t )
        {


            double ts = point.codedTimeIntoSeconds(t);
            double xx = p1.xm() + (ts - p1.tsec()) * (p2.xm() - p1.xm()) / (p2.tsec() - p1.tsec());
            double yy = p1.ym() + (ts - p1.tsec())* (p2.ym() - p1.ym()) / (p2.tsec() - p1.tsec());
            double x, y;

            if (p1.x > -360.0 && p1.x <= 360.0 && p1.y >= -90.0 && p1.y <= 90.0)
            { 
                NETGeographicLib.TransverseMercator p = new TransverseMercator(a, f, k1);
                p.Reverse(referenceMeridian, xx,yy, out y, out x);
            }
            else
            {
                x = xx;
                y = yy;
            }

            point newpont = new point(t, x, y, true, referenceMeridian);

            return newpont;

        }

        /// <summary>
        /// dddhhmmss format on time code
        /// </summary>
        public long t
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

        /// <summary>
        /// longitude
        /// </summary>
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

        /// <summary>
        /// latitude
        /// </summary>
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
        /// <summary>
        /// zone
        /// </summary>
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

        public int year
        {
            get
            {
                return iyear;
            }
            set
            {
                iyear = value;
            }
        }

        public int traceNumber
        {
            get
            {
                return iTraceNumber;
            }
            set
            {
                iTraceNumber = value;
            }
        }

        public DateTime tDateTime
        {
            get
            {
                DateTime tdt = new DateTime(iyear, 1, 1,0,0,0);
                return (tdt.AddMilliseconds((double)tsec())).AddDays(-1);
            }
            set
            {
                settime(value);
            }
        }

        public long tsec()
        {
            return (long)codedTimeIntoSeconds(t);

        }

        public static double codedTimeIntoSeconds( long ti)
        {
            // seconds since start of the iyear
            // assume dddhhmmss format on time code
            //changed to msec precision
            long t = ti;
            long days = (int) (t / 1000000000);
            long r = (t - days * 1000000000);
            long hrs = ( r / 10000000);
            r = r - hrs * 10000000;
            long min = (r / 100000);
            r = r - min * 100000;
            long sec = r / 1000;
            r = r - sec * 1000;
            long msec = (int)r;

            double tsec = msec + 1000*(sec + (min * 60) + hrs * 3600 + days * (3600 * 24));
            return tsec;
        }
        public double thr()
        {


            return tsec() / 3600.0 / 24.0/1e3;

        }

        public double xm()
        {
            // caculate position using mean earth radius relative to 1 st point
            int setzone = zonei;

            return xx;
        }
        public double ym()
        {

            return yy;
        }

        public void settime(DateTime dt)
        {
            // use this to generate dddhhmmss formatted time code
            long day = dt.DayOfYear;
            long hr = dt.Hour;
            long min = dt.Minute;
            long sec = dt.Second;
            long msec = dt.Millisecond;
            this.ti = msec + 1000*(sec + 100 * (min + 100 * (hr + 100 * day)));
            this.iyear = dt.Year;
        }

    }
}
