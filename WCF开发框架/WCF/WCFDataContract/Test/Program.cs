using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.IO;

namespace Test
{
    [Serializable]
    public class Order
    {
        private Guid _id;

        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime Date;
 
    }
    class Program
    {
        static void Main(string[] args)
        {
            SerializeViaBinaryFormatter();
            SerializerViaSoapFormatter();
            SerializerViaXmlSerilizer();
        }

        static void SerializeViaBinaryFormatter()
        {
            FileStream fs = new FileStream("binaryFile.bin", FileMode.OpenOrCreate);
            Order order = new Order() { ID = Guid.NewGuid()};
            BinaryFormatter binFormatter = new BinaryFormatter();
            try
            {
                binFormatter.Serialize(fs, order);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        static void SerializerViaSoapFormatter()
        {
            FileStream fs = new FileStream("soapFile.xml", FileMode.OpenOrCreate);
            Order order = new Order() { ID = Guid.NewGuid() };
            SoapFormatter soapFormatter = new SoapFormatter();
            try
            {
                soapFormatter.Serialize(fs, order);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

        }

        static void SerializerViaXmlSerilizer()
        {
            FileStream fs = new FileStream("xmlFile.xml", FileMode.OpenOrCreate);
            Order order = new Order() { ID = Guid.NewGuid() };
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(Order));
            try
            {
                xmlFormatter.Serialize(fs, order);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
