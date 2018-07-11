using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssurityAPITest.APITest
{
    public class Global
    {
        //Common Variables

        public enum RequestMethods
        {
            GET,
            POST
        }

        public static string RequestContentType= "application/json";        
        public static int RunID;
        public static string[] StringSeparatorsComma = new string[] { "," };

        //Log File Strings

        public static StreamWriter sw;
        public static StringBuilder sb = new StringBuilder();

        //Report info
        public static ExtentReports ExtRep;
        public static ExtentHtmlReporter ExtHTMLRep;
        public static ExtentTest ExtTest;

       

      

        //Directory
        public static string LogDirPath = @"C:\AssurityTest\LogFile\";
        public static string ReportDirPath = @"C:\AssurityTest\Report\";


        //File Paths
        public static string LogFilePath = @"C:\AssurityTest\LogFile\APITestLog_";
        public static string ReportFilePath = @"C:\AssurityTest\Report\APIReport_";
        public static string ReportConfigPath = @"C:\AssurityTest\AssurityAPITestProj\AssurityAPITestProj\extent-config.xml";



        //Test URL
        public static string Testurl = "https://api.tmsandbox.co.nz/v1/Categories/6327/Details.json?catalogue=false";


        //Expected Values
        public static string Carboncredits = "Name:Carbon credits";
        public static string CanRelist = "CanListAuctions:true";
        public static string PromotionsElement = "Name:Gallery";
        public static string PromotionsElementDescription = "\\u000a2x larger image";



    }
}
