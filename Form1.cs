using Newtonsoft.Json;
using PricingAndConfigurationTest.Requests;
using PricingAndConfigurationTest.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PricingAndConfigurationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionSetter connection = new ConnectionSetter();
            String originalFolder = @"C:\Users\Caroline\Documents\json data";
            String folder = "";
            String dateModifiedJson = "";
            if (comboBox1.Text.Equals("Debco"))
            {
                folder = FileFolderUtils.addAndCreateFolder(originalFolder, "\\debco data");
                connection.readConnectionConfig("..\\..\\config\\Debco.cfg");
                connection.productDataEndpoint = "ProductDataServiceDebco";
                dateModifiedJson = folder+"\\debcoDateModified.json";
                if (comboBox2.Text.Equals("Descriptions"))
                {
                    connection.endpoint = connection.productDataEndpoint;
                }
                else if (comboBox2.Text.Equals("Prices"))
                {
                    connection.endpoint = "ProductPricingServiceDebco";
                }
                else if (comboBox2.Text.Equals("Pictures"))
                {
                    connection.endpoint = "ProductMediaServiceDebco";
                }
            }
            else if(comboBox1.Text.Equals("Cap America"))
            {
                folder = FileFolderUtils.addAndCreateFolder(originalFolder, "\\capam data");
                connection.readConnectionConfig("..\\..\\config\\CapAmerica.cfg");
                connection.productDataEndpoint = "ProductDataServiceCapAm";
                dateModifiedJson = folder+"\\capamDateModified.json";
                if (comboBox2.Text.Equals("Descriptions"))
                {
                    connection.endpoint = connection.productDataEndpoint;
                }
                else if (comboBox2.Text.Equals("Prices"))
                {
                    connection.endpoint = "ProductPricingServiceCapAm";
                }
            }
            else if (comboBox1.Text.Equals("PCNA"))
            {
                folder = FileFolderUtils.addAndCreateFolder(originalFolder, "\\pcna data");
                connection.readConnectionConfig("..\\..\\config\\Pcna.cfg");
                connection.productDataEndpoint = "ProductDataServicePcna";
                dateModifiedJson = folder + "\\pcnaDateModified.json";
                if (comboBox2.Text.Equals("Descriptions"))
                {
                    connection.endpoint = connection.productDataEndpoint;
                }
                else if (comboBox2.Text.Equals("Prices"))
                {
                    connection.endpoint = "ProductPricingServicePcna";
                }
                else if (comboBox2.Text.Equals("Pictures"))
                {
                    connection.endpoint = "ProductMediaServicePcna";
                }
            }
            else if (comboBox1.Text.Equals("Starline"))
            {
                folder = FileFolderUtils.addAndCreateFolder(originalFolder, "\\starline data");
                connection.readConnectionConfig("..\\..\\config\\Starline.cfg");
                connection.productDataEndpoint = "ProductDataServiceStarline";
                dateModifiedJson = folder + "\\starlineDateModified.json";
                if (comboBox2.Text.Equals("Descriptions"))
                {
                    connection.endpoint = connection.productDataEndpoint;
                }
                else if (comboBox2.Text.Equals("Prices"))
                {
                    connection.endpoint = "ProductPricingServiceStarline";
                }
                else if (comboBox2.Text.Equals("Pictures"))
                {
                    connection.endpoint = "ProductMediaServiceStarline";
                }
            }
            ConfigurationManager.AppSettings.Set("wsUsername", connection.username);
            ConfigurationManager.AppSettings.Set("wsPassword", connection.password);
            var productDataTester = new ProductDataClient(connection.productDataEndpoint);
            var productDataResponse = productDataTester.getProductDateModified();
            Program.printResponse(productDataResponse, dateModifiedJson, productDataTester.validTypes);
            List<String> articleIds = JsonUtils.readJson(dateModifiedJson);
            StreamWriter fileStream = new StreamWriter(originalFolder+"\\debug.txt");
            if (comboBox2.Text.Equals("Descriptions"))
            {
                DataRequest frRequest = new DataRequest();
                frRequest.version = "1.0.0";
                frRequest.country = "CA";
                frRequest.language = "fr";
                DataRequest enRequest = new DataRequest();
                enRequest.version = "1.0.0";
                enRequest.country = "US";
                enRequest.language = "en";
                /*if (comboBox1.Text.Equals("Starline"))
                {
                    frRequest.version = "2.0.0";
                    enRequest.version = "2.0.0";
                }*/
                articleIds = ArrayListUtils.RemoveDuplicates(articleIds);
                String frFolder = FileFolderUtils.addAndCreateFolder(folder, "\\fr\\");
                String enFolder = FileFolderUtils.addAndCreateFolder(folder, "\\en\\");
                foreach (String id in articleIds)
                {
                    Console.WriteLine(id);
                    var output = new StringBuilder();
                    var enResponse = productDataTester.getProductData(enRequest, id);
                    var frResponse = productDataTester.getProductData(frRequest, id);
                    try
                    {
                        var enResponseType = enResponse.GetType();
                        var frResponseType = frResponse.GetType();
                        output.AppendLine("Response Object: " + frResponseType.Name);
                        Console.WriteLine(frResponseType.GetProperties()[0].GetValue(frResponse));
                        enResponseType.GetProperties()[0].GetValue(enResponse).GetType();
                        frResponseType.GetProperties()[0].GetValue(frResponse).GetType();

                        StreamWriter frFile = File.CreateText(frFolder + id + ".json");
                        JsonWriter frWriter = new JsonTextWriter(frFile);
                        frWriter.Formatting = Formatting.Indented;
                        fileStream.WriteLine(id);
                        PropertyInfo frProperty = frResponseType.GetProperties()[0];
                        JsonUtils.printJson(frProperty, frProperty.GetValue(frResponse), ref frWriter, 1);
                        //writer.WriteEndObject();
                        frWriter.Flush();
                        frWriter.Close();
                        StreamWriter enFile = File.CreateText(enFolder + id + ".json");
                        JsonWriter enWriter = new JsonTextWriter(enFile);
                        enWriter.Formatting = Formatting.Indented;
                        PropertyInfo enProperty = enResponseType.GetProperties()[0];
                        JsonUtils.printJson(enProperty, enProperty.GetValue(enResponse), ref enWriter, 1);
                        //writer.WriteEndObject();
                        enFile.Flush();
                        enFile.Close();
                    }
                    catch (Exception error)
                    {
                        //error.ToString();
                    }
                }
                fileStream.Close();
            }
            else if (comboBox2.Text.Equals("Prices"))
            {
                var pricingConfigurationClient = new PricingAndConfigurationClient(connection.endpoint);
                PricingRequest pricingRequest = new PricingRequest();
                pricingRequest.version = "1.0.0";
                pricingRequest.priceType = WSPricingAndConfiguration.priceType.List;
                pricingRequest.currency = WSPricingAndConfiguration.CurrencyCodeType.CAD;
                pricingRequest.fobId = "1";
                string priceFolder = FileFolderUtils.addAndCreateFolder(folder, "\\prix\\");
                foreach (String id in articleIds)
                {
                    Console.WriteLine(id);
                    var output = new StringBuilder();
                    pricingRequest.productId = id;
                    var pricingResponse = pricingConfigurationClient.getConfigurationAndPricing(pricingRequest);
                    try
                    {
                        var pricingResponseType = pricingResponse.GetType();
                        output.AppendLine("Response Object: " + pricingResponseType.Name);
                        pricingResponseType.GetProperties()[0].GetValue(pricingResponse).GetType();

                        StreamWriter pricingFile = File.CreateText(priceFolder + id + ".json");
                        JsonWriter pricingWriter = new JsonTextWriter(pricingFile);

                        pricingWriter.Formatting = Formatting.Indented;
                        fileStream.WriteLine(id);
                        foreach (var property in pricingResponseType.GetProperties())
                        {
                            JsonUtils.printJson(property, property.GetValue(pricingResponse), ref pricingWriter, 1);
                        }
                        //writer.WriteEndObject();
                        pricingWriter.Flush();
                        pricingWriter.Close();
                    }
                    catch (Exception error)
                    {
                        //error.ToString();
                    }
                }
                fileStream.Close();
            }
            else if (comboBox2.Text.Equals("Pictures"))
            {
                var productMediaClient = new ProductMediaClient(connection.endpoint);
                MediaRequest mediaRequest = new MediaRequest();
                mediaRequest.version = "1.1.0";
                articleIds = ArrayListUtils.RemoveDuplicates(articleIds);
                string photoFolder = FileFolderUtils.addAndCreateFolder(folder, "\\photo\\");
                foreach (String id in articleIds)
                {
                    var output = new StringBuilder();
                    mediaRequest.productId = id;
                    var pricingResponse = productMediaClient.getMediaContent(mediaRequest);
                    try
                    {
                        var pricingResponseType = pricingResponse.GetType();
                        output.AppendLine("Response Object: " + pricingResponseType.Name);
                        Console.WriteLine(pricingResponseType.GetProperties()[1].GetValue(pricingResponse).GetType().GetProperties()[0].GetValue(pricingResponseType.GetProperties()[1].GetValue(pricingResponse)));
                        pricingResponseType.GetProperties()[0].GetValue(pricingResponse).GetType();
                        StreamWriter pricingFile = File.CreateText(photoFolder + id + ".json");
                        JsonWriter pricingWriter = new JsonTextWriter(pricingFile);
                        pricingWriter.Formatting = Formatting.Indented;
                        fileStream.WriteLine(id);
                        pricingWriter.WriteStartObject();
                        foreach (var property in pricingResponseType.GetProperties())
                        {
                            Console.WriteLine(id);
                            JsonUtils.printJson(property, property.GetValue(pricingResponse), ref pricingWriter, 1);
                        }
                        Console.WriteLine("test");
                        pricingWriter.WriteEndObject();
                        pricingWriter.Flush();
                        pricingWriter.Close();
                    }
                    catch (Exception error)
                    {
                        //error.ToString();
                    }
                }
                fileStream.Close();
                Console.WriteLine("End");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
