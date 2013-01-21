 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CommonLayer;
using ObjectLayer;
using System.Security.Cryptography;

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
        public static LogicManager Instance 
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
        public void BuildEPG(string oldFile, string newFile)
        {
            DataManager.Instance.BuildEPG(oldFile, newFile);
            //DataManager.Instance.BuildEPG2(oldFile, newFile);

            //// Variables
            //tv epg;

            //// Code

            //epg = new tv();
            //programme p = new programme();
            //List<desc> l = new List<desc>();
            //desc d = new desc();
            //BinaryFormatter b = new BinaryFormatter();
            //d.lang = "TVAdviser";
            //MemoryStream ms = new MemoryStream();
            //CostumeData c = new CostumeData();
            //c.Rating = 9;
            //b.Serialize(ms, c);
            //d.Value = Convert.ToString((ms.ToArray()));
            //l.Add(d);
            //p.desc = l.ToArray();
            //DataManager.Instance.GetRating();

            //// Create the new file.
            //Utils.SerializeXML<tv>(epg, newFile);
        }
        
        public int calculateRating(string prog, int userID){
            int ret=0;
            TVProgram.TVProgramJSON progObj=Importer.getProgramByName(prog);
            return ret;
        }

        public void userRegister(string name, string password){
            string hash = Utils.GetHashedPassword(password);
            try
            {
                DataManager.Instance.insertUser(name, hash);
            }
            catch
            {
                //can't create a user with this name
            }
        }

        public int userLogin(string name, string password)
        {
            int ID = 0;
            string hash = Utils.GetHashedPassword(password);
            try
            {
                DataManager.Instance.insertUser(name, hash);
            }
            catch
            {
                //can't find user
            }
            return ID;
        }


        string GetEncodedHash(string password){
            MD5 md5 = new MD5CryptoServiceProvider();
            byte [] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(digest, 0, digest.Length);
        }

        public int GetUserID(string name, System.Security.SecureString secureString)
        {
            IntPtr ptr =
                System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            string sDecrypString =
               System.Runtime.InteropServices.Marshal.PtrToStringUni(ptr);
            return DataManager.Instance.GetUserID(name, GetEncodedHash(sDecrypString));
        }
    }

    [Serializable]
    class CostumeData
    {
        public int Rating { get; set; }
    }
}