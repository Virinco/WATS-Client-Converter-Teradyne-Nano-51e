using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TeradyneNano51e;
using Virinco.WATS.Interface;

namespace TeradyneNano51e
{
    [TestClass]
    public class ConverterTests : TDM
    {

        [TestMethod]
        public void SetupClient()
        {
            SetupAPI(null, "TeradyneNano51e", "ICT Tester", true);
            InitializeAPI(true);
        }

        [TestMethod]
        public void TestTeradyneNano51eConverter()
        {
            string fn = @"Data\exmaplefile.csv";
            InitializeAPI(true);
            Dictionary<string, string> arguments = new TeradyneNano51eConverter().ConverterParameters;
            TeradyneNano51eConverter converter = new TeradyneNano51eConverter(arguments);
            using (FileStream file = new FileStream(fn, FileMode.Open))
            {
                SetConversionSource(new FileInfo(fn), converter.ConverterParameters, null);
                Report uut = converter.ImportReport(this, file);
            }
            SubmitPendingReports();
        }

        [TestMethod]
        public void TestTeradyneNano51eConverterFolder()
        {
            InitializeAPI(true);
            Dictionary<string, string> arguments = new TeradyneNano51eConverter().ConverterParameters;
            TeradyneNano51eConverter converter = new TeradyneNano51eConverter(arguments);
            string[] fileNames = Directory.GetFiles(@"Data\", "*.csv", SearchOption.AllDirectories);
        
            Console.WriteLine(fileNames);
            int count = 0;
            foreach (string fileName in fileNames)
            {
                Console.WriteLine($"Converting {fileName}, ({++count} of {fileNames.Length})");
                SetConversionSource(new FileInfo(fileName), converter.ConverterParameters, null);
                using (FileStream file = new FileStream(fileName, FileMode.Open))
                {
                    converter.ImportReport(this, file);
                }
            }
            SubmitPendingReports();
        }
    }
}
