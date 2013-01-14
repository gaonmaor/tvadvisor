using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataLayer.Properties;
using System.Data;

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
        private SqlConnection m_connection;

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static DataManager m_instance;

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
            m_connection = new SqlConnection(Settings.Default.ConString);
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
            SqlTransaction transaction;

            // The SQL data command.
            SqlCommand cmd;

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
                        cmd = new SqlCommand("INSERT INTO tvadviser.channel (id, display_name) VALUES ('" +
                            channel.id + "','" + channel.displayname + "')", m_connection, transaction);
                        cmd.ExecuteNonQuery();
                    }
                    // Save the programs.
                    foreach (var program in epg.programme)
                    {
                        cmd = new SqlCommand("INSERT INTO tvadviser.category (Value, lang) VALUES ('" +
                            (from c in program.category select c.Value) + "', '" + (from c in program.category select c.lang) + "'");
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("INSERT INTO tvadviser.programme " + 
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
            SqlTransaction transaction;

            // The SQL data command.
            SqlCommand cmd;

            // Code

            lock (m_lockObject)
            {
                m_connection.Open();
                transaction = m_connection.BeginTransaction();

                try
                {
                    cmd = new SqlCommand("");
                    SqlDataReader reader = cmd.ExecuteReader();
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
        #endregion

     
    }
}
