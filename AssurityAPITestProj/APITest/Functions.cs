using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AssurityAPITest.APITest
{    
    class Functions
    {
        public static int GeneratedNumber;
        public static StringBuilder sbLocal = new StringBuilder();
        

        //Generating an unique ID for the Test
        public static int GenerateRunID()
        {
            Random rn = new Random();
            GeneratedNumber = rn.Next(10000);
            Global.sb.Append(Functions.WriteToLog("Run ID: " + GeneratedNumber));
            return GeneratedNumber;
        }

        //Writing to log
        public static StringBuilder WriteToLog(string logValue)
        {
            sbLocal = new StringBuilder();
            sbLocal.Append(DateTime.Now + ": " + logValue + "\r\n");
            return sbLocal;
        }



    }
}
