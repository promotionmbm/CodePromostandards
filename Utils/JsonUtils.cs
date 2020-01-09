using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest.Utils
{
    class JsonUtils
    {
        public static void printKeywords(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                String[] array = (String[])property.GetValue(currentObject);
                if (array.Length > 0)
                {
                    writer.WriteStartArray();
                    foreach (String description in (String[])property.GetValue(currentObject))
                    {
                        writer.WriteValue(description);
                    }
                    writer.WriteEndArray();
                }
                else
                {
                    writer.WriteValue("");
                }
            }catch(Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printProductMarketingPoint(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSProductData.ProductMarketingPoint[] marketingPoints=(WSProductData.ProductMarketingPoint[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach(WSProductData.ProductMarketingPoint marketingPoint in marketingPoints)
                {
                    writer.WriteStartObject();
                    foreach (var innerProperty in marketingPoints[0].GetType().GetProperties())
                    {
                        writer.WritePropertyName(innerProperty.Name);
                        writer.WriteValue(innerProperty.GetValue(marketingPoints[0]));
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }catch(Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printKeyWordArray(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSProductData.ProductKeyword[] keywords=(WSProductData.ProductKeyword[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach (var keyword in keywords)
                {
                    writer.WriteValue(keyword.GetType().GetProperties()[0].GetValue(keyword));
                }
                writer.WriteEndArray();
            }catch(Exception e)
            {
                writer.WriteValue("");
            }
}

        public static void printBrand(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                Console.WriteLine(property.GetValue(currentObject).GetType());
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printExport(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                Console.WriteLine(property.GetValue(currentObject).GetType());
                writer.WriteValue(property.GetValue(currentObject));
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printProductCategories(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSProductData.ProductCategory[] categories = (WSProductData.ProductCategory[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach (WSProductData.ProductCategory category in categories)
                {
                    writer.WriteStartObject();
                    foreach(PropertyInfo innerproperty in category.GetType().GetProperties())
                    {
                        writer.WritePropertyName(innerproperty.Name);
                        writer.WriteValue(innerproperty.GetValue(category));
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printRelatedProducts(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSProductData.RelatedProduct[] relatedProducts=(WSProductData.RelatedProduct[])property.GetValue(currentObject);
                writer.WriteValue("");
                //Console.WriteLine("Related products: "+relatedProducts.Length);
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printProductPart(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSProductData.ProductProductPart[] productParts = (WSProductData.ProductProductPart[])property.GetValue(currentObject);
                writer.WriteValue("");
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printEndDate(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                Console.WriteLine(property.GetValue(currentObject).GetType());
                writer.WriteValue(property.GetValue(currentObject));
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printIsCaution(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                Console.WriteLine(property.GetValue(currentObject).GetType());
                writer.WriteValue(property.GetValue(currentObject));
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printParts(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSPricingAndConfiguration.Part[] parts = (WSPricingAndConfiguration.Part[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach(WSPricingAndConfiguration.Part part in parts)
                {
                    writer.WriteStartObject();
                    foreach (PropertyInfo innerproperty in part.GetType().GetProperties())
                    {
                        writer.WritePropertyName(innerproperty.Name);
                        if (innerproperty.Name.Equals("PartPriceArray"))
                        {
                            printPartPriceArray(innerproperty, part, ref writer);
                        }
                        else
                        {
                            writer.WriteValue(innerproperty.GetValue(part));
                        }
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printPartPriceArray(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSPricingAndConfiguration.PartPrice[] partPrices = (WSPricingAndConfiguration.PartPrice[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach (WSPricingAndConfiguration.PartPrice partPrice in partPrices)
                {
                    writer.WriteStartObject();
                    foreach (PropertyInfo innerproperty in partPrice.GetType().GetProperties())
                    {
                        writer.WritePropertyName(innerproperty.Name);
                        if (innerproperty.Name.Equals("priceExpiryDate"))
                        {
                            printEndDate(innerproperty, partPrice, ref writer);
                        }
                        else
                        {
                            writer.WriteValue(innerproperty.GetValue(partPrice));
                        }
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printLocationArray(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSPricingAndConfiguration.Location[] locations = (WSPricingAndConfiguration.Location[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach (WSPricingAndConfiguration.Location location in locations)
                {
                    writer.WriteStartObject();
                    foreach (PropertyInfo innerproperty in location.GetType().GetProperties())
                    {
                        writer.WritePropertyName(innerproperty.Name);
                        if (innerproperty.Name.Equals("FobArray"))
                        {
                            printFobArray(innerproperty, currentObject, ref writer);
                        }
                        else
                        {
                            writer.WriteValue(innerproperty.GetValue(location));
                        }
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printFobArray(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                WSPricingAndConfiguration.FobArrayFob[] fobs = (WSPricingAndConfiguration.FobArrayFob[])property.GetValue(currentObject);
                writer.WriteStartArray();
                foreach (WSPricingAndConfiguration.FobArrayFob fob in fobs)
                {
                    writer.WriteStartObject();
                    foreach (PropertyInfo innerproperty in fob.GetType().GetProperties())
                    {
                        writer.WritePropertyName(innerproperty.Name);
                        if (innerproperty.Name.Equals("priceExpiryDate"))
                        {
                            printEndDate(innerproperty, fob, ref writer);
                        }
                        else
                        {
                            writer.WriteValue(innerproperty.GetValue(fob));
                        }
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printProductId(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try {
                property.GetValue(currentObject).GetType();
                writer.WriteValue(property.GetValue(currentObject));
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static void printObjectToConsole(PropertyInfo property, Object currentObject, ref JsonWriter writer)
        {
            try
            {
                //Console.WriteLine("Type: "+property.GetValue(currentObject).GetType());
                //WSPricingAndConfiguration.Part[] parts = (WSPricingAndConfiguration.Part[])property.GetValue(currentObject);
                foreach (PropertyInfo innerProperty in property.GetValue(currentObject).GetType().GetProperties())
                {
                    Console.WriteLine(innerProperty);
                }
                writer.WriteValue(property.GetValue(currentObject));
            }
            catch (Exception e)
            {
                writer.WriteValue("");
            }
        }

        public static List<String> readJson(String file)
        {
            Console.WriteLine("test");
            string text = System.IO.File.ReadAllText(file);
            JsonTextReader jsonReader = new JsonTextReader(new StringReader(text));
            List<String> values = new List<String>();
            Boolean isId = false;
            while (jsonReader.Read())
            {
                if (jsonReader.Value!=null) {
                    if (isId)
                    {
                        values.Add(jsonReader.Value.ToString());
                        isId = !isId;
                    }
                    else if (jsonReader.Value.Equals("productId"))
                    {
                        isId = !isId;
                    }
                }
            }
            return values;
        }

        public static void printJson(PropertyInfo property, Object currentObject, ref JsonWriter writer, int indent)
        {
            Type[] printableTypes = { typeof(string), typeof(decimal), typeof(DateTime), typeof(String[]), typeof(Boolean), typeof(Boolean?) };
            if (currentObject!= null) { 
                if (printableTypes.Contains(currentObject.GetType()) || currentObject.GetType().IsPrimitive || currentObject.GetType().GenericTypeArguments.Contains(typeof(DateTime)))
                {
                    if (property.PropertyType == typeof(String[]))
                    {
                        writer.WritePropertyName(property.Name);
                        writer.WriteStartArray();
                        foreach (String value in (string[])currentObject)
                        {
                            writer.WriteValue(value);
                        }
                        writer.WriteEndArray();
                    }
                    else
                    {
                        Console.WriteLine(currentObject.ToString());
                        writer.WriteStartObject();
                        writer.WritePropertyName(property.Name);
                        writer.WriteValue(currentObject);
                        writer.WriteEndObject();
                    }
                    return;
                }
            }
            if (property.PropertyType.BaseType == typeof(Enum))
            {
                writer.WritePropertyName(property.Name);
                writer.WriteValue(currentObject);
                return;
            }
            else if (property.PropertyType.BaseType == typeof(Array))
            {
                writer.WritePropertyName(property.Name);
                writer.WriteStartArray();
                if (currentObject != null)
                {
                    indent++;
                    foreach (var item in (Array)currentObject)
                    {
                        writer.WriteStartObject();
                        var innerCurrentObjectType = item.GetType();
                        foreach (var innerProperty in innerCurrentObjectType.GetProperties())
                        {
                            if (printableTypes.Contains(innerProperty.PropertyType) || innerProperty.PropertyType.IsPrimitive || innerProperty.PropertyType.GenericTypeArguments.Contains(typeof(DateTime)))
                            {
                                writer.WritePropertyName(innerProperty.Name);
                                writer.WriteValue(innerProperty.GetValue(item));
                            }
                            else if (innerProperty.GetValue(item) != null)
                            {
                                if (!innerProperty.Name.Equals("ClassTypeArray"))
                                {
                                    writer.WritePropertyName(innerProperty.Name);
                                    writer.WriteValue(innerProperty.GetValue(item));

                                }
                            }
                        }
                        writer.WriteEndObject();
                    }
                }
                writer.WriteEndArray();
            }
            else if (currentObject != null)
            {
                var currentObjType = currentObject.GetType();
                writer.WriteStartObject();
                foreach (var innerProperty in currentObjType.GetProperties())
                {
                    //Console.WriteLine(innerProperty.GetType());
                    //Console.WriteLine(innerProperty.Name);
                    try
                    {
                        if (innerProperty.Name.Equals("description"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printKeywords(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("ProductMarketingPointArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printProductMarketingPoint(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("ProductKeywordArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printKeyWordArray(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("productBrand"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printBrand(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("export"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printExport(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("ProductCategoryArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printProductCategories(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("RelatedProductArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printRelatedProducts(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("ProductPartArray"))
                        {
                            /*writer.WritePropertyName(innerProperty.Name);
                            printProductPart(innerProperty, currentObject, ref writer);*/
                        }
                        else if (innerProperty.Name.Equals("endDate"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printProductPart(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("isCaution") || innerProperty.Name.Equals("cautionComment") || innerProperty.Name.Equals("lineName"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printIsCaution(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("PartArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printParts(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("PartPriceArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printPartPriceArray(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("LocationArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printLocationArray(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("FobArray"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printFobArray(innerProperty, currentObject, ref writer);
                        }
                        else if (innerProperty.Name.Equals("productId")|| innerProperty.Name.Equals("productName"))
                        {
                            writer.WritePropertyName(innerProperty.Name);
                            printProductId(innerProperty, currentObject, ref writer);
                        }
                        else
                        {
                            //Console.WriteLine(innerProperty.Name);
                            var propertyValue = innerProperty.GetValue(currentObject);
                            writer.WritePropertyName(innerProperty.Name);
                            var type = innerProperty.GetValue(currentObject).GetType();
                            Boolean isArray = innerProperty.GetValue(currentObject).GetType().IsArray;
                            if (isArray)
                            {
                                writer.WriteStartArray();
                                foreach (var value in (Array)innerProperty.GetValue(currentObject))
                                {
                                    if (value.GetType().ToString().Equals("PricingAndConfigurationTest.WSProductData.ProductKeyword"))
                                    {
                                        foreach (var valueProperty in value.GetType().GetProperties())
                                            printJson(valueProperty, valueProperty.GetType(), ref writer, 1);
                                    }
                                    writer.WriteValue(value.ToString());
                                }
                                writer.WriteEndArray();
                            }
                            else
                            {
                                writer.WriteValue(propertyValue.ToString());
                                Console.WriteLine(propertyValue.ToString());
                            }
                        }
                    }catch(Exception e)
                    {
                        //Console.WriteLine(e.ToString());
                    }
                }
                writer.WriteEndObject();
            }
        }
    }
}
