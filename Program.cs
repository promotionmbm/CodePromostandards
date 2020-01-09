using Newtonsoft.Json;
using PricingAndConfigurationTest.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PricingAndConfigurationTest
{
    class Program
    {
        
        static void Main(string[] args)
        {

            //PricingAndConfigurationClient pricingAndConfigurationTester = new PricingAndConfigurationClient();

            //getFobPoints test
            /*var fobPointsResponse = pricingAndConfigurationTester.getFobPoints();
            printResponse(fobPointsResponse, pricingAndConfigurationTester.path, pricingAndConfigurationTester.validTypes);

            //getConfigurationAndPricing test
            var configurationAndPricesResponse = pricingAndConfigurationTester.getConfigurationAndPricing();
            printResponse(configurationAndPricesResponse, pricingAndConfigurationTester.path, pricingAndConfigurationTester.validTypes);*/

            //getProductData test
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            /*var productDataTester = new ProductDataClient();
            var productDataResponse = productDataTester.getProductDateModified();
            printResponse(productDataResponse, productDataTester.path, productDataTester.validTypes);

            Console.WriteLine("Finish response processing, files saved on " + Environment.ExpandEnvironmentVariables(Path.Combine(ConfigurationManager.AppSettings.Get("responseSavePath"))));
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();*/

            /*//getMediaContent test
            var mediaContentTester = new ProductMediaClient();
            var mediaContentResponse = mediaContentTester.getMediaContent();
            printResponse(mediaContentResponse, mediaContentTester.path, mediaContentTester.validTypes);*/
        }

         public static void printResponse(object response, string jsonFile, List<Type> validTypes)
        {
                var responseType = response.GetType();
                var output = new StringBuilder();
                StreamWriter file = File.CreateText(jsonFile);
                JsonWriter writer = new JsonTextWriter(file);
                writer.Formatting = Formatting.Indented;
                output.AppendLine("Response Object: " + responseType.Name);
                HierarchyPrinter.validTypes = validTypes;
                writer.WriteStartObject();
                foreach (var property in responseType.GetProperties())
                {
                    //HierarchyPrinter.printMembersValues(property, property.GetValue(response), ref output, 1);
                    JsonUtils.printJson(property, property.GetValue(response), ref writer, 1);
                }
                writer.WriteEndObject();
                writer.Flush();
                writer.Close();
        }
    }
}
