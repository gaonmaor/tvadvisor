using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CommonLayer
{
    /// <summary>
    /// General use utils.
    /// </summary>
    public class Utils
    {
        /// <summary>
        ///  Deserialize object from xml.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="type">The xml class type.</param>
        /// <returns>The class represents the xml.</returns>
        public static T DeserializeXml<T>(string fileName)
        {
            T res = default(T);
            try
            {
                if (fileName != string.Empty)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XDocument xdoc = XDocument.Load(fileName);
                    SetDefaultXmlNamespace(xdoc.Root, "http://tempuri.org/xmltv");
                    using (XmlReader xreader = xdoc.CreateReader())
                    {
                        res = (T)serializer.Deserialize(xreader);
                    }                     
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return res;
        }

        /// <summary>
        /// Serialize the xml into a file.
        /// </summary>
        /// <typeparam name="T">The xml type to serialize.</typeparam>
        /// <param name="xml">The xml to serialize</param>
        /// <param name="newFile">The new file to create.</param>
        public static void SerializeXML<T>(T xml, string newFile)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using(StreamWriter writer = new StreamWriter(newFile))
	            {
                    serializer.Serialize(writer, xml);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Change recursievly the namespace.
        /// </summary>
        /// <param name="xelem">The current element.</param>
        /// <param name="xmlns">The namespace.</param>
        public static void SetDefaultXmlNamespace(XElement xelem, XNamespace xmlns)
        {
            if (xelem.Name.NamespaceName == string.Empty)
            {
                xelem.Name = xmlns + xelem.Name.LocalName;
            }
            foreach (var e in xelem.Elements())
            {
                SetDefaultXmlNamespace(e, xmlns);
            }
        }

        /// <summary>
        /// Convert epg format date to datetime string.
        /// </summary>
        /// <param name="date">The epg format date.</param>
        /// <returns>The result date string.</returns>
        public static string EPGDateToString(string date)
        {
            DateTime result = GetEPGDate(date);
            return result.ToShortDateString() + " " + result.ToShortTimeString();
        }

        /// <summary>
        /// Convert epg format date to datetime.
        /// </summary>
        /// <param name="date">The epg format date.</param>
        /// <returns>The result date time.</returns>
        public static DateTime GetEPGDate(string date)
        {
            DateTimeOffset result;
            if (date == null)
            {
                return new DateTime();
            }
            result = DateTimeOffset.ParseExact(
                        date,
                        "yyyyMMddHHmmss zzzzz",
                      System.Globalization.CultureInfo.InvariantCulture);

            return result.DateTime;
        }
    }
}
