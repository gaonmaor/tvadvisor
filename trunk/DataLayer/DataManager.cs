﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataLayer.Properties;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

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
            m_connection = new MySqlConnection(Settings.Default.ConString);
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

        public void getFreeBaseData()
        {
            StreamReader reader = new StreamReader(@"C:\tv_program.tsv");
            string[] a;
            string str;

            for (int i = 0; i < 1000; i++)
            {
                str = reader.ReadLine();
                a = str.Split('\t');
                string query = "INSERT INTO Program (id, name, country_of_origin) (" + i + ", " + a[0] + ", " + a[7] + ");";
            }
        }
        #endregion

     
    }
}