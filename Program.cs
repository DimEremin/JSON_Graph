
using Newtonsoft.Json;
using System.Reflection;
using System.Collections.ObjectModel;

namespace JSON_Study
{

    class Program
    {
        static InternalObject resultObject;
        static bool Property;
        static Stack<string> objectPath = new Stack<string>();
        static Stack<string> propertyPath = new Stack<string>();
        public static void Main(string[] args)
        {


                JsonSerializer serializer = new JsonSerializer();
                Root root = JsonConvert.DeserializeObject<Root>(File.ReadAllText(@"Graph.json"));



            string id = "5948a953-c9d5-4e75-98eb-514e5437257f";
            SearchInternalObject(root.InternalObjects, id);
            objectPath.Push("1");

            string tag = "Info11";
            propertyPath.Push("Profile");
             SearchProperty(resultObject.Profile, tag);


            Console.WriteLine("\n" + string.Join(",", propertyPath));
        }

        public static void SearchInternalObject (List<InternalObject> internalObjects, string id )
        {
                foreach (InternalObject internalObject in internalObjects)
            {
               
                if (resultObject != null)
                {
                    objectPath.Push(internalObjects.IndexOf(internalObject).ToString());
                    objectPath.Push("Internal objects");
                    return;
                }
                if (internalObject.uuid == id)
                {
                   // Console.WriteLine("\n" + "УСПЕХ");
                    resultObject = internalObject;
                    objectPath.Push((internalObjects.IndexOf(internalObject)+1).ToString());
                    objectPath.Push("Internal objects");
                    
                    return;
                }

                    SearchInternalObject(internalObject.InternalObjects, id);
            }
            if (resultObject != null)
            {
                objectPath.Push("1");
                objectPath.Push("Internal objects");
            }
        }

        public static void SearchProperty(Object obj, string tag)
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (Property)
                {
                    propertyPath.Push(property.Name);
                    return;
                }
                if (property.Name == tag)
                {
                    Console.WriteLine("\n" + "УСПЕХ Свойства");
                    Console.WriteLine("\n" + property.GetValue(obj).ToString());
                    propertyPath.Push(property.GetValue(obj).ToString());
                    propertyPath.Push(property.Name);
                    Property = true;
                    return;
                }
                if (obj.GetType().IsEnum)
                {
                    Collection<Object> collection = (Collection<Object>)property.GetValue(obj);
                    foreach (Object n in collection)
                    {
                        SearchProperty(n, tag);
                    }
                }
                try
                {
                    SearchProperty(property.GetValue(obj), tag);
                }
                catch
                {

                }
                
                Console.WriteLine(property.Name);
        

                
            }

        }



    }



    public class Info1
    {
        [JsonProperty("Info 1.1")]
        public string Info11 { get; set; }

        [JsonProperty("Info 1.2")]
        public List<string> Info12 { get; set; }

        [JsonProperty("Info 1.3")]
        public List<string> Info13 { get; set; }
    }

    public class Info2
    {
        [JsonProperty("Info 2.1")]
        public string Info21 { get; set; }

        [JsonProperty("Info 2.2")]
        public Info22 Info22 { get; set; }
    }

    public class Info22
    {
        [JsonProperty("Path 1")]
        public string Path1 { get; set; }

        [JsonProperty("Path 2")]
        public string Path2 { get; set; }

        [JsonProperty("Path 3")]
        public string Path3 { get; set; }
    }

    public class Info3
    {
        [JsonProperty("Info 3.1")]
        public string Info31 { get; set; }

        [JsonProperty("Info 3.2")]
        public string Info32 { get; set; }

        [JsonProperty("Info 3.3")]
        public string Info33 { get; set; }

        [JsonProperty("Info 3.4")]
        public string Info34 { get; set; }
    }

    public class Info4
    {
        [JsonProperty("Info 4.1")]
        public List<object> Info41 { get; set; }

        [JsonProperty("Info 4.2")]
        public List<object> Info42 { get; set; }
    }

    public class InternalObject
    {
        public string uuid { get; set; }

        [JsonProperty("Object Name")]
        public string ObjectName { get; set; }

        [JsonProperty("Object Type")]
        public string ObjectType { get; set; }

        [JsonProperty("Internal objects")]
        public List<InternalObject> InternalObjects { get; set; }
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public string Status { get; set; }

        [JsonProperty("Info 1")]
        public Info1 Info1 { get; set; }

        [JsonProperty("Info 2")]
        public Info2 Info2 { get; set; }

        [JsonProperty("Info 3")]
        public Info3 Info3 { get; set; }

        [JsonProperty("Info 4")]
        public Info4 Info4 { get; set; }

        [JsonProperty("Info 5")]
        public List<object> Info5 { get; set; }
    }

    public class Root
    {
        public string uuid { get; set; }

        [JsonProperty("Object Name")]
        public string ObjectName { get; set; }

        [JsonProperty("Object Type")]
        public string ObjectType { get; set; }

        [JsonProperty("Internal objects")]
        public List<InternalObject> InternalObjects { get; set; }
    }

}