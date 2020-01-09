using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest
{
    class ProductDataClient
    {
        private WSProductData.ProductDataServiceClient wsClient;
        public List<Type> validTypes;
        public string path;


        public ProductDataClient()
        {
            wsClient = new WSProductData.ProductDataServiceClient();
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "PricingAndConfigurationTest.WSProductData"
                    select t;
            validTypes = q.ToList();
        }

        public ProductDataClient(String name)
        {
            wsClient = new WSProductData.ProductDataServiceClient(name);
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "PricingAndConfigurationTest.WSProductData"
                    select t;
            validTypes = q.ToList();
        }


        public WSProductData.GetProductResponse getProductData()
        {
            this.setPath("productDataOutput");
            var request = new WSProductData.GetProductRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.localizationCountry = "CA";
            request.localizationLanguage = "en";
            request.productId = "A3621";
            var request1 = new WSProductData.getProductRequest1(request);
            WSProductData.getProductResponse1 response1 = wsClient.getProduct(request1);
            return response1.GetProductResponse;
        }

        public WSProductData.GetProductResponse getProductData(String productId)
        {
            this.setPath("productDataOutput");
            var request = new WSProductData.GetProductRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.localizationCountry = "CA";
            request.localizationLanguage = "fr";
            request.productId = productId;
            var request1 = new WSProductData.getProductRequest1(request);
            WSProductData.getProductResponse1 response1 = wsClient.getProduct(request1);
            return response1.GetProductResponse;
        }

        public WSProductData.GetProductResponse getProductData(DataRequest requestBuilder, String productId)
        {
            this.setPath("productDataOutput");
            var request = new WSProductData.GetProductRequest();
            request.wsVersion = requestBuilder.version;
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.localizationCountry = requestBuilder.country;
            request.localizationLanguage = requestBuilder.language;
            request.productId = productId;
            var request1 = new WSProductData.getProductRequest1(request);
            try
            {
                WSProductData.getProductResponse1 response1 = wsClient.getProduct(request1);
                return response1.GetProductResponse;
            }
            catch(System.ServiceModel.FaultException e)
            {
                return null;
            }
        }

        public WSProductData.GetProductDateModifiedResponse getProductDateModified()
        {
            this.setPath("productDateModifiedOutput");
            var request = new WSProductData.GetProductDateModifiedRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.changeTimeStamp = new DateTime(1900, 1, 1);
            var request1 = new WSProductData.getProductDateModifiedRequest1(request);
            WSProductData.getProductDateModifiedResponse1 response1 = wsClient.getProductDateModified(request1);
            return response1.GetProductDateModifiedResponse;

        }

        public void setPath(string path)
        {
            this.path = Environment.ExpandEnvironmentVariables(Path.Combine(ConfigurationManager.AppSettings.Get("responseSavePath"), path + ".txt"));
            File.Delete(path);
        }
    }
}
