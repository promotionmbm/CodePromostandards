using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest.Requests
{
    class ConnectionSetter
    {
        public String username;
        public String password;
        public String endpoint;
        public String productDataEndpoint;

        public ConnectionSetter() { }

        public void readConnectionConfig(String path) {
            string[] lines = File.ReadAllLines(path);
            foreach(string line in lines)
            {
                if (line.StartsWith("username"))
                {
                    this.username = line.Replace(" ", "").Replace("username:", "");
                }
                else if (line.StartsWith("password"))
                {
                    this.password = line.Replace(" ", "").Replace("password:", "");
                }
            }
        }
    }
}
