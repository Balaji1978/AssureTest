using System;


using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AssurityAPITest.APITest
{
    [TestFixture]
    public class APITesting
    {
        [SetUp]
        public static void Initialize()
        {
            if (!Directory.Exists(Global.ReportDirPath))
            {
                Directory.CreateDirectory(Global.ReportDirPath);
            }
            if (!Directory.Exists(Global.LogDirPath))
            {
                Directory.CreateDirectory(Global.LogDirPath);
            }

            Global.sb.Append(Functions.WriteToLog("Test Initialized...."));

            //Initialising RUNID
            Functions.GenerateRunID();
            Global.RunID = Functions.GeneratedNumber;

            //Initialising HTML Report
            Global.ExtHTMLRep = new ExtentHtmlReporter(Global.ReportFilePath + Global.RunID + ".html");
            Global.ExtRep = new ExtentReports();
            Global.ExtRep.AttachReporter(Global.ExtHTMLRep);

            //Initializing File System
            FileStream fs = new FileStream(Global.LogFilePath + Global.RunID + ".txt", FileMode.Create);
            Global.sw = new StreamWriter(fs);

            //Update Reports, logs with Test name
            Global.sb.Append(Functions.WriteToLog("-----------------------------------------------------------------------------------"));
            Global.sb.Append(Functions.WriteToLog("Test Case: " + TestContext.CurrentContext.Test.Name + " Execution Started...."));
            Global.sb.Append(Functions.WriteToLog("-----------------------------------------------------------------------------------"));
            Global.ExtTest = Global.ExtRep.CreateTest("Test Report: " + TestContext.CurrentContext.Test.Name);
            Global.ExtTest.Log(Status.Info, "-----------------------------------------------------------------------------------");
            Global.ExtTest.Log(Status.Info, "Test Case: " + TestContext.CurrentContext.Test.Name + " Execution Started....");
            Global.ExtTest.Log(Status.Info, "-----------------------------------------------------------------------------------");
        }


        [TearDown]
        public void TestCleanup()
        {
            Functions.WriteToLog("Test CleanUP Initiated.......");
            Global.ExtHTMLRep.LoadConfig(Global.ReportConfigPath);
            Global.ExtRep.Flush();
            Global.sb.Append(Functions.WriteToLog(TestContext.CurrentContext.Test.FullName + " Execution Completed..."));
            Functions.WriteToLog("Test CleanUP Completed.......");
            Global.sw.WriteLine(Global.sb.ToString());
            Global.sw.Flush();
            Global.sb.Clear();
        }

        //Main Test Start
        [Test]
        public void APITestMain()
        {
            var request = WebRequest.Create(Global.Testurl);
            request.Method = Global.RequestMethods.GET.ToString();
            request.ContentType = Global.RequestContentType;
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = reader.ReadToEnd();

            var responseTextReplace = responseText.Replace("\"", "").Split(Global.StringSeparatorsComma, StringSplitOptions.None);
            List<string> OutputValues = new List<string>();

            for (int i = 0; i < responseTextReplace.Length; i++)
            {
                if (responseTextReplace[i].Contains(Global.Carboncredits) ||
                    responseTextReplace[i].Contains(Global.CanRelist) ||
                    responseTextReplace[i].Contains(Global.PromotionsElement) ||
                    responseTextReplace[i].Contains(Global.PromotionsElementDescription))
                {
                    OutputValues.Add(responseTextReplace[i]);
                }

            }

            if (OutputValues.Contains(Global.Carboncredits) ||
                OutputValues.Contains(Global.CanRelist) ||
                OutputValues.Contains(Global.PromotionsElement) ||
                OutputValues.Contains(Global.PromotionsElementDescription))
            {
                Global.sb.Append(Functions.WriteToLog("Output Values Are as expected: " + OutputValues[0] + " , " + OutputValues[1] + " ," + OutputValues[2] + " , " + OutputValues[3]));

                Global.ExtTest.Log(Status.Pass, "Output Values Are as expected: " + OutputValues[0]);
                Global.ExtTest.Log(Status.Pass, "Output Values Are as expected: " + OutputValues[1]);
                Global.ExtTest.Log(Status.Pass, "Output Values Are as expected: " + OutputValues[2]);
                Global.ExtTest.Log(Status.Pass, "Output Values Are as expected: " + OutputValues[3]);


                Assert.AreEqual(Global.Carboncredits, OutputValues[0], "Output Values Are as expected: " + OutputValues[0]);
                Assert.AreEqual(Global.CanRelist, OutputValues[1], "Output Values Are as expected: " + OutputValues[1]);
                Assert.AreEqual(Global.PromotionsElement, OutputValues[2], "Output Values Are as expected: " + OutputValues[2]);
                StringAssert.Contains(Global.PromotionsElementDescription, OutputValues[3], "Output Values Are as expected: " + OutputValues[3]);
            }
            else
            {
                Global.sb.Append(Functions.WriteToLog("Output Values Are not as expected: " + OutputValues[0] + " , " + OutputValues[1] + " ," + OutputValues[2] + " , " + OutputValues[3]));

                Global.ExtTest.Log(Status.Fail, "Output Values Are as expected: " + OutputValues[0]);
                Global.ExtTest.Log(Status.Fail, "Output Values Are as expected: " + OutputValues[1]);
                Global.ExtTest.Log(Status.Fail, "Output Values Are as expected: " + OutputValues[2]);
                Global.ExtTest.Log(Status.Fail, "Output Values Are as expected: " + OutputValues[3]);


                Assert.AreEqual(Global.Carboncredits, OutputValues[0], "Output Values Are not as expected: " + OutputValues[0]);
                Assert.AreEqual(Global.CanRelist, OutputValues[1], "Output Values Are not as expected: " + OutputValues[1]);
                Assert.AreEqual(Global.PromotionsElement, OutputValues[2], "Output Values Are not as expected: " + OutputValues[2]);
                StringAssert.Contains(Global.PromotionsElementDescription, OutputValues[3], "Output Values Are not as expected: " + OutputValues[3]);

            }
        }





    }
}

