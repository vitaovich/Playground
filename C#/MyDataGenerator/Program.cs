using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace MyDataGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MyResult test = new MyResult() {
                SerialNumber = "Test123",
                PassFail = false,
                TestStartTime = DateTime.UtcNow,
                TestEndTime = DateTime.Now
            };

            using (StreamWriter i = File.AppendText("./output/test.csv"))
            {
                i.WriteLine(ReflectObjectPropertyHeaders(test));
                i.WriteLine(ReflectObjectPropertyValues(test));
            }
        }

        public static string ReflectObjectPropertyHeaders (object obj) 
        {
            Type t = obj.GetType();
            PropertyInfo[] propertyInfos = t.GetProperties();
            string v = string.Join(',',propertyInfos.Select(m => m.Name).ToList());
            return v;
        }

        public static string ReflectObjectPropertyValues (object obj) 
        {
            Type t = obj.GetType();
            PropertyInfo[] propertyInfos = t.GetProperties();


            string v = string.Join(',',propertyInfos.Select(m => CheckValue(m.GetValue(obj))).ToList());
            return v;
        }

        public static string CheckValue(object obj) 
        {
            if(obj == null) {
                return "";
            }
            string typeName = obj.GetType().Name;
            string value = obj.ToString();
            if(typeName.Equals("DateTime"))
            {
                DateTime v = (DateTime)obj;
                value = v.ToString("yyyy-MM-dd HH:mm:ss zzz");
            } 
            else if(typeName.Equals("String"))
            {
                value = $"\"{obj}\"";
            }
            return value;
        }
    }

    public abstract class BaseResult 
    {
        public string SerialNumber { get; set; }
        public bool PassFail { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TestStartTime { get; set; }
        public DateTime TestEndTime { get; set; }
    }

    public class MyResult : BaseResult
    {
        public double Measurement1 { get; set; }
    }
}
