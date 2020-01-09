using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest.Requests
{
    class PricingRequest
    {
        public String version;
        public WSPricingAndConfiguration.priceType priceType;
        public WSPricingAndConfiguration.CurrencyCodeType currency;
        public String fobId;
        public String productId;

        public PricingRequest()
        {

        }
    }
}
