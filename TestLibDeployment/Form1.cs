using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NETGeographicLib;

namespace TestLibDeployment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // load Oracle
            

            this.oracleConnection1.ConnectionString =  "user id=" + "coreview"
                                                     + ";password=" +"coreview"
                                              + ";data source=" + "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)"
                                              + "(HOST=" + "gdratl.ess.nrcan.gc.ca"
                                              + ")(PORT=" + "1521"
                                              + "))(CONNECT_DATA="
                                              + "(SERVICE_NAME=" + "gdratl.ess.nrcan.gc.ca"
                                              + ")))";
                // load ED expeds
                this.oracleConnection1.Open();
                this.oracleCommand1.Connection = this.oracleConnection1;
                DataSet ds = new DataSet();
                this.oracleCommand1.CommandText = "SELECT  EXPED_CD, EXPED_YEAR FROM CORE.EXPEDITION WHERE (EXPED_CD IN (SELECT DISTINCT EXPED_CD FROM CORE.SEISMIC_PARAMETER)) ORDER BY EXPED_YEAR, EXPED_CD";
                this.oracleDataAdapter1.SelectCommand = this.oracleCommand1;
                this.oracleDataAdapter1.Fill(ds, "Cruise List");

                NETGeographicLib.TransverseMercator tm = new TransverseMercator();


        }
    }
}
