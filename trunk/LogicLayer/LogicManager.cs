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
using System.Net;

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
        public void BuildEPG(string oldFile, string newFile, int userId, string defaultLang)
        {
            tv epg = Utils.DeserializeXml<tv>(oldFile);
            programme[] ps = epg.programme;
            if (ps == null)
            {
                Utils.SerializeXML<tv>(epg, newFile);
                return;
            }

            foreach (programme p in ps)
            {
                string name = null;
                if (p.title != null)
                {
                    foreach (title t in p.title)
                    {
                        if (t.lang.Equals(defaultLang))
                        {
                            name = t.Value;
                        }
                    }
                }
                if (name == null)
                {
                    //return;
                    continue;
                }
                //get rating
                int r = calculateRating(name, userId);
                rating rate = new rating();
                rate.value = r.ToString();
                p.rating = new rating[1] { rate };
            }
            DataManager.Instance.BuildEPG(oldFile, newFile, epg, defaultLang);
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

        public int calculateRating(string prog, int userID)
        {
            int ret = 0, progID = 0;
            progID = DataManager.Instance.getProgID(prog);
            if (progID == -1) return 5;
            try
            {
                ret = DataManager.Instance.getProgRating(progID, userID);
            }
            catch
            {
                throw;
            }
            if (ret != -1) return ret;
            try
            {
                ret = DataManager.Instance.getActorsRating(progID, userID);
            }
            catch
            {
                throw;
            }
            if (ret == -1)
                return 5;
            return ret;
        }

        public void rateProgram(string prog, int userID, int rating)
        {
            int progID = 0;
            progID = DataManager.Instance.getProgID(prog);
            try
            {
                int prev = DataManager.Instance.getProgRating(progID, userID);
                DataManager.Instance.setProgRating(progID, userID, rating);
                DataManager.Instance.setActorRating(progID, userID, rating, prev);
            }
            catch
            {
                throw;
            }

        }

        public void userRegister(string name, string password){
            string hash = Utils.GetHashedPassword(password);
            try
            {
                DataManager.Instance.insertUser(name, hash);
            }
            catch
            {
                throw;
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
                
            }
            return ID;
        }

        public int GetUserID(string name, System.Security.SecureString secureString)
        {
            IntPtr ptr =
                System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            string sDecrypString =
               System.Runtime.InteropServices.Marshal.PtrToStringUni(ptr);
            return DataManager.Instance.GetUserID(name, Utils.GetHashedPassword(sDecrypString));
        }

        /// <summary>
        /// Save the user orders to the database.
        /// </summary>
        /// <param name="lstOrderDetails">The orders to sace.</param>
        public void SaveOrders(List<OrderDetail> lstOrderDetails)
        {
            DataManager.Instance.SaveOrders(lstOrderDetails);
        }

        /// <summary>
        /// Load the user orders from the database.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The orders.</returns>
        public List<OrderDetail> LoadOrders(int userId)
        {
            return DataManager.Instance.LoadOrders(userId);
        }

        /// <summary>
        /// Get the users.
        /// </summary>
        /// <returns></returns>
        public List<UserDetail> GetUsers()
        {
            return DataManager.Instance.GetUsers();
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="userId">The user to delete.</param>
        public void DeleteUser(int userId)
        {
            DataManager.Instance.DeleteUser(userId);
        }

        /// <summary>
        /// Update the admin value.
        /// </summary>
        /// <param name="userId">The user.</param>
        /// <param name="isAdmin">The </param>
        public void ChangeAdmin(int userId, bool isAdmin)
        {
            DataManager.Instance.ChangeAdmin(userId, isAdmin);
        }

        /// <summary>
        /// Reload data dumps.
        /// </summary>
        public void ReloadProgramDumps(UpdateProgressEvent progressEvent, int numRecords, string url, string outputFolder)
        {
            WebClient client = new WebClient();
            outputFolder = validateFolder(outputFolder);
            string programFP = outputFolder + @"\programs.tsv";
            progressEvent("Download program dumps.", 0);
            client.DownloadFile(url, programFP);
            progressEvent("Download program dumps finished.", 100);
            DataManager.Instance.GetFreebaseData(progressEvent, numRecords, programFP);
        }

        /// <summary>
        /// Reload actors data dumps.
        /// </summary>
        public void ReloadActorsDataDumps(UpdateProgressEvent progressEvent, int numRecords, string url, string outputFolder)
        {
            WebClient client = new WebClient();
            outputFolder = validateFolder(outputFolder);
            string actorsFP = outputFolder + @"\actors.tsv";
            progressEvent("Download actors dumps.", 0);
            client.DownloadFile(url, actorsFP);
            progressEvent("Download actors dumps finished.", 100);
            DataManager.Instance.GetFreebaseActor(progressEvent, numRecords, actorsFP);
        }

        /// <summary>
        /// Full data dumps
        /// </summary>
        public void ReloadFullDataDumps(UpdateProgressEvent progressEvent, int numRecords, List<string> urls, string outputFolder)
        {
            WebClient client = new WebClient();
            outputFolder = validateFolder(outputFolder);
            string programFP = outputFolder + @"\programs.tsv";
            progressEvent("Download program dumps.", 0);
            client.DownloadFile(urls[0], programFP);
            progressEvent("Download actors dumps." , 33);
            string actorsFP = outputFolder + @"\actors.tsv";
            client.DownloadFile(urls[1], actorsFP);
            progressEvent("Download regular dumps.", 66);
            string regFP = outputFolder + @"\reg.tsv";
            client.DownloadFile(urls[2], regFP);
            progressEvent("Get freebase programs data.", 0);
            DataManager.Instance.GetFreebaseData(progressEvent, numRecords, programFP);
            progressEvent("Get freebase actors data.", 0);
            DataManager.Instance.GetFreebaseActor(progressEvent, numRecords, actorsFP);
            progressEvent("Get freebase regular data.", 0);
            DataManager.Instance.GetFreebaseActorProgram(progressEvent, numRecords, regFP);
        }

        /// <summary>
        /// Validate the existence of the folder.
        /// </summary>
        /// <param name="outputFolder"></param>
        private string validateFolder(string outputFolder)
        {
            outputFolder = Environment.CurrentDirectory + "\\" + outputFolder;
            DirectoryInfo dir = new DirectoryInfo(outputFolder);
            if (!dir.Exists)
            {
                dir.Create();
            }

            return outputFolder;
        }

        /// <summary>
        /// Get the actor details.
        /// </summary>
        /// <param name="actorName">The actor name.</param>
        /// <returns>The actor detail object.</returns>
        public ActorDetail LoadActor(string actorName)
        {
            return DataManager.Instance.LoadActor(actorName);
        }
    }

    [Serializable]
    class CostumeData
    {
        public int Rating { get; set; }
    }
}