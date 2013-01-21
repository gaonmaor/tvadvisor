﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataLayer.Properties;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using ObjectLayer;
using CommonLayer;

namespace DataLayer
{
    /// <summary>
    /// Manage comunication with the Database.
    /// </summary>
    public class DataManager
    {
        #region Fields

        /// <summary>
        /// Lock object for thread synchronization.
        /// </summary>
        private static object m_lockObject = new Object();

        /// <summary>
        /// The SQL data connection. 
        /// </summary>
        private MySqlConnection m_connection;

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static DataManager m_instance;

        private static int id = 0;

        #endregion

        #region Properties

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static DataManager Instance
        {
            get
            {
                lock (m_lockObject)
                {
                    if (m_instance == null)
                    {
                        m_instance = new DataManager();
                    }
                }

                return m_instance;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Init the connection string.
        /// </summary>
        private DataManager()
        {
            // server=localhost;User Id=DbMysql05;Persist Security Info=True;Ssl Mode=None;port=3305;database=DbMysql05
            //string s = "Server=localhost;Port=3305;Database=DbMysql05;Uid=DbMysql05;Pwd=DbMysql05;";

            //                                  server=localhost;User Id=DbMysql05;password=DbMysql05;Persist Security Info=True;port=3305;database=DbMysql05
            m_connection = new MySqlConnection("server=localhost;User Id=DbMysql05;password=DbMysql05;Persist Security Info=True;Ssl Mode=None;port=3305;database=DbMysql05");
            //m_connection = new MySqlConnection(Settings.Default.ConString);


            //m_connection = new SqlConnection("server=localhost;User Id=DbMysql05;Persist Security Info=True;Ssl Mode=None;port=3305;database=DbMysql05");
            //m_connection = new SqlConnection(Settings.Default.ConString);
        }

        #endregion

        #region Operations

        /// <summary>
        /// Save epg to the database.
        /// </summary>
        /// <param name="epg">The epg to save.</param>
        public void SaveEPG(tv epg)
        {
            // Variables

            // The sql transaction.
            MySqlTransaction transaction;

            // The SQL data command.
            MySqlCommand cmd;

            // Code

            lock (m_lockObject)
            {
                m_connection.Open();
                transaction = m_connection.BeginTransaction();

                try
                {
                    // Save the channels.
                    foreach (var channel in epg.channel)
                    {
                        cmd = new MySqlCommand("INSERT INTO tvadviser.channel (id, display_name) VALUES ('" +
                            channel.id + "','" + channel.displayname + "')", m_connection, transaction);
                        cmd.ExecuteNonQuery();
                    }
                    // Save the programs.
                    foreach (var program in epg.programme)
                    {
                        cmd = new MySqlCommand("INSERT INTO tvadviser.category (Value, lang) VALUES ('" +
                            (from c in program.category select c.Value) + "', '" + (from c in program.category select c.lang) + "'");
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand("INSERT INTO tvadviser.programme " + 
                            "(title_id, sub_title_id, desc_id, category_id, episode_num_id, start, stop, channel_id)" +
                            " VALUES (,,,,,'','',);", m_connection, transaction);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    m_connection.Close();
                }
            }
        }

        public void BuildEPG(string oldFile, string newFile)
        {
            //m_connection = new SqlConnection("server=localhost;User Id=DbMysql05;Persist Security Info=True;Ssl Mode=None;port=3305;database=DbMysql05");
            //MySqlConnection conn = new MySqlConnection(Settings.Default.ConString);

            //conn.Open();
            string defaultLang = "he";
            string defaultDesc = "";
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

                string d = null;
                string query = "SELECT description FROM Program WHERE name=@name";
                MySqlCommand cmd = new MySqlCommand(query, m_connection);
                cmd.Parameters.AddWithValue("@name", name);
                try
                {
                    //server=localhost;User Id=DbMysql05;
                    //password=DbMysql05;Persist Security Info=True;
                    //port=3305;database=DbMysql05
                    //m_connection.Close();
                    m_connection.ConnectionString = Settings.Default.ConString;

                    //MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder(Settings.Default.ConString);
                    //sb.
                    //m_connection.

                    m_connection.Open();

                    object dbresult = cmd.ExecuteScalar();
                    d = (string)dbresult;
                }
                catch
                {
                    throw;
                }
                m_connection.Close();

                if (d == null)
                {
                    TVProgram.TVProgramJSON tvpo = ObjectLayer.Importer.getProgramByName(name);
                    d = tvpo.getDescription();

                    if (d != null)
                    {
                        //update DB
                        query = "UPDATE Program SET description=@description WHERE name=@name";
                        cmd = new MySqlCommand(query, m_connection);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@description", d);
                        try
                        {
                            m_connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            throw;
                        }
                        m_connection.Close();
                    }
                }

                if (d == null)
                {
                    d = defaultDesc;
                }

                bool flag = false;
                foreach (desc da in p.desc)
                {
                    if (da.lang.Equals(defaultLang))
                    {
                        flag = true;
                        da.Value = d;
                    }
                }
                if (flag == false)
                {
                    List<desc> ld = p.desc.ToList();
                    ld.Add(new desc() { Value = d, lang = defaultLang });
                    desc[] description = ld.ToArray();
                    p.desc = description;
                }

            }

            Utils.SerializeXML<tv>(epg, newFile);
        }

        public void BuildEPG2(string oldFile, string newFile)
        {
            string dl = "he";
            tv epg = new tv();
            programme prog = new programme();
            //title[] t = new title[1] { new title() { Value = "Firefly", lang = dl } };
            title[] t = new title[1] { new title() { Value = "blabla", lang = dl } };
            desc[] description = new desc[1] { new desc() { Value = "something else", lang = dl } };

            prog.title = t;
            prog.desc = description;
            epg.programme = new programme[1] { prog };

            Utils.SerializeXML<tv>(epg, newFile);
        }

        public void GetRating()
        {
            // Variables

            // The sql transaction.
            MySqlTransaction transaction;

            // The SQL data command.
            MySqlCommand cmd;

            // Code

            lock (m_lockObject)
            {
                m_connection.Open();
                transaction = m_connection.BeginTransaction();

                try
                {
                    cmd = new MySqlCommand("");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    m_connection.Close();
                }
            }
            throw new NotImplementedException();
        }

        public void GetFreebaseData()
        {
            //  The dump file reader
            StreamReader data_reader = new StreamReader(@"C:\tv_program.tsv"); //Environment.CurrentDirectory

            // The sql transaction.
            MySqlTransaction transaction;

            // The SQL data command.
            MySqlCommand cmd;

            lock (m_lockObject)
            {
                string[] crr_line_words;
                string line = data_reader.ReadLine(); ;
                string query;

                m_connection.Open();
                transaction = m_connection.BeginTransaction();
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        line = data_reader.ReadLine();
                        crr_line_words = line.Split('\t');

                        query = "INSERT INTO Program (name, country_of_origin, freebase_id, description)" +
                                "VALUES (@name, @country_of_origin, @freebase_id, @description)";
                        cmd = new MySqlCommand(query, m_connection, transaction);

                        string id = crr_line_words[1];
                        TVProgram.TVProgramJSON oTv = ObjectLayer.Importer.getProgramByMID(id);
                        string description = oTv.getDescription();

                        cmd.Parameters.AddWithValue("@name", crr_line_words[0]);
                        cmd.Parameters.AddWithValue("@freebase_id", id);
                        cmd.Parameters.AddWithValue("@country_of_origin", crr_line_words[7]);
                        cmd.Parameters.AddWithValue("@description", description);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }

                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    m_connection.Close();
                }
            }
        }

        public void insertUser(string name, string password){
            string query = "INSERT INTO User (name,password) VALUES (@name, @password)";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                m_connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            m_connection.Close();
        }

        public int GetUserID(string name, string password)
        {
            int id = -1;
            string query = "SELECT id, name FROM User WHERE name=@name AND password=@password";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                m_connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    id = (int)result;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                m_connection.Close();
            }
            return id;
        }

        #endregion

     
    }
}