using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CommonLayer;

namespace LogicLayer
{
    public class LogicManager
    {
        /// <summary>
        /// Lock object for thread synchronization.
        /// </summary>
        private static object m_lockObject = new Object();

        /// <summary>
        /// The singleton field.
        /// </summary>
        private static LogicManager m_insance = null;

        /// <summary>
        /// The singleton property.
        /// </summary>
        public static LogicManager Insance 
        {
            get
            {
                lock (m_lockObject)
                {
                    if (m_insance == null)
                    {
                        m_insance = new LogicManager();
                    }
                }

                return m_insance;
            }
        }

        /// <summary>
        /// The private constructor.
        /// </summary>
        private LogicManager()
        {
        }

        /// <summary>
        /// Save the EPG to the database.
        /// </summary>
        /// <param name="epgPath">The output epg.</param>
        public void SaveEPG(string epgPath)
        {
            tv epg = Utils.DeserializeXml<tv>(epgPath);
            DataManager.Instance.SaveEPG(epg);
        }


        /// <summary>
        /// Build the new epg.
        /// </summary>       
        public void BuildEPG(string newFile)
        {
            // Variables
            tv epg;

            // Code

            epg = new tv();
            programme p = new programme();
            List<desc> l = new List<desc>();
            desc d = new desc();
            BinaryFormatter b = new BinaryFormatter();
            d.lang = "TVAdviser";
            MemoryStream ms = new MemoryStream();
            CostumeData c = new CostumeData();
            c.Rating = 9;
            b.Serialize(ms, c);
            d.Value = Convert.ToString((ms.ToArray()));
            l.Add(d);
            p.desc = l.ToArray();

            // Create the new file.
            Utils.SerializeXML<tv>(epg, newFile);
        }
    }

    [Serializable]
    class CostumeData
    {
        public int Rating { get; set; }
    }
}