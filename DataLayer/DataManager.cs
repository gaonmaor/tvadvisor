using System;
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
        //private MySqlConnection m_connection;

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
            //m_connection = new MySqlConnection("server=localhost;User Id=DbMysql05;password=DbMysql05;Persist Security Info=True;Ssl Mode=None;port=3305;database=DbMysql05");

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

            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            dbcon.Open();
            transaction = dbcon.con.BeginTransaction();

            try
            {
                // Save the channels.
                foreach (var channel in epg.channel)
                {
                    if (channel.displayname != null && channel.displayname.Length > 0 && channel.displayname[0] != null)
                    {
                        cmd = new MySqlCommand("INSERT IGNORE INTO Channel (name, xmltv_id) VALUES (@name, @xmltv_id)",
                            dbcon.con, transaction);
                        cmd.Parameters.AddWithValue("@name", channel.displayname[0].Value);
                        cmd.Parameters.AddWithValue("@xmltv_id", channel.id);
                        cmd.ExecuteNonQuery();
                    }
                }
                // Save the programs.
                /*
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
                */

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
        }

        public void BuildEPG(string oldFile, string newFile, tv epg, string defaultLang)
        {
            //server=localhost;User Id=DbMysql05;password=DbMysql05;Persist Security Info=True;port=3305;database=DbMysql05;Connect Timeout=30
            //string constr = "server=localhost;User Id=DbMysql05;password=DbMysql05;port=3305;database=DbMysql05;Connect Timeout=30";
            //m_connection = new SqlConnection("server=localhost;User Id=DbMysql05;Persist Security Info=True;Ssl Mode=None;port=3305;database=DbMysql05");
            //MySqlConnection conn = new MySqlConnection(Settings.Default.ConString);

            //conn.Open();

            string defaultDesc = "";
            //tv epg = Utils.DeserializeXml<tv>(oldFile);
            programme[] ps = epg.programme;
            //if (ps == null)
            //{
            //    Utils.SerializeXML<tv>(epg, newFile);
            //    return;
            //}
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

                ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
                dbcon.Open();
                //MySqlTransaction transaction = m_connection.BeginTransaction();

                string d = null;
                string countryOfOrigin = null;
                //LogicManager lm = null;
                List<string> actorNames = new List<string>();
                MySqlDataReader actorsReader = null;
                string query = "SELECT description FROM Program WHERE name=@name";
                string query2 = "SELECT Actor.name FROM ProgramActor,Program,Actor " +
                                "WHERE Program.name=@name AND ProgramActor.program_id=Program.id " +
                                "AND ProgramActor.actor_id=Actor.id";
                string query3 = "SELECT country_of_origin FROM Program WHERE name=@name";
                MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
                MySqlCommand cmd2 = new MySqlCommand(query2, dbcon.con);
                MySqlCommand cmd3 = new MySqlCommand(query3, dbcon.con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd3.Parameters.AddWithValue("@name", name);
                try
                {
                    //server=localhost;User Id=DbMysql05;
                    //password=DbMysql05;Persist Security Info=True;
                    //port=3305;database=DbMysql05
                    //dbcon.Close();ConnectionPool.freeConnection(dbcon);
                    //m_connection.ConnectionString = Settings.Default.ConString;

                    //MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder(Settings.Default.ConString);
                    //sb.
                    //m_connection.

                    //ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();dbcon.Open();

                    object dbresult = cmd.ExecuteScalar();
                    object dbresult2 = cmd3.ExecuteScalar();
                    actorsReader = cmd2.ExecuteReader();
                    //int i = 0;
                    while (actorsReader.Read())
                    {
                        actorNames.Add(actorsReader.GetString(0));
                        //i++;
                    }
                    if (dbresult != DBNull.Value && dbresult != null)
                    {
                        d = (string)dbresult;
                    }
                    if (dbresult2 != DBNull.Value && dbresult2 != null)
                    {
                        countryOfOrigin = (string)dbresult2;
                    }
                    //transaction.Commit();

                }
                catch
                {
                    //transaction.Rollback();
                    throw;
                }
                finally
                {
                    if (actorsReader != null)
                    {
                        actorsReader.Close();
                    }

                    dbcon.Close();
                    ConnectionPool.freeConnection(dbcon);
                }


                //commented code updates XML file and database with Freebase JSON data

                //if (d == null || d.Equals(""))
                //{
                //    bool flag2 = (d == null ? false : true);
                //    TVProgram.TVProgramJSON tvpo = ObjectLayer.Importer.getProgramByName(name);
                //    if (tvpo != null)
                //    {
                //        d = tvpo.getDescription();
                //    }

                //    if (d != null)
                //    {
                //        ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();dbcon.Open();
                //        transaction = m_connection.BeginTransaction();

                //        if (flag2 == true)//description is "";
                //        {
                //            //update DB
                //            query = "UPDATE Program SET description=@description WHERE name=@name";
                //            cmd = new MySqlCommand(query, m_connection, transaction);
                //            cmd.Parameters.AddWithValue("@name", name);
                //            cmd.Parameters.AddWithValue("@description", d);
                //        }
                //        else//description is null;
                //        {
                //            //add entry to DB
                //            query = "INSERT INTO Program (name, country_of_origin, freebase_id, description)" +
                //                "VALUES (@name, @country_of_origin, @freebase_id, @description)";
                //            cmd = new MySqlCommand(query, m_connection, transaction);
                //            cmd.Parameters.AddWithValue("@name", name);
                //            cmd.Parameters.AddWithValue("@freebase_id", tvpo.getMID());
                //            //string country = "country";
                //            cmd.Parameters.AddWithValue("@country_of_origin", tvpo.getCountry());
                //            cmd.Parameters.AddWithValue("@description", d);
                //        }
                //        try
                //        {
                //            //ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();dbcon.Open();
                //            cmd.ExecuteNonQuery();
                //            transaction.Commit();
                //        }
                //        catch
                //        {
                //            transaction.Rollback();
                //            throw;
                //        }
                //        finally
                //        {
                //            dbcon.Close();ConnectionPool.freeConnection(dbcon);
                //        }
                //    }
                //}

                if (d == null)
                {
                    d = defaultDesc;
                }

                bool flag = false;
                if (p.desc != null)
                {
                    foreach (desc da in p.desc)
                    {
                        if (da.lang.Equals(defaultLang))
                        {
                            flag = true;
                            if (d != defaultDesc)
                            {
                                da.Value = da.Value + "\nFreebase:\n" + d;
                            }
                        }
                    }
                }
                if (flag == false)
                {
                    List<desc> ld = (p.desc == null ? new List<desc>() : p.desc.ToList());
                    ld.Add(new desc() { Value = d, lang = defaultLang });
                    desc[] description = ld.ToArray();
                    p.desc = description;
                }

                actor[] actors = new actor[actorNames.Count];
                actor curact;
                for (int j = 0; j < actorNames.Count; j++)
                {
                    curact = new actor();
                    curact.Value = actorNames.ElementAt(j);
                    actors[j] = curact;
                }
                if (p.credits == null)
                {
                    p.credits = new credits();
                }
                p.credits.actor = actors;
                if (countryOfOrigin != null)
                {
                    country c = new country() { Value = countryOfOrigin };
                    p.country = new country[1] { c };
                }
            }

            Utils.SerializeXML<tv>(epg, newFile);
        }

        public void BuildEPG2(string oldFile, string newFile)
        {
            string dl = "he";
            tv epg = new tv();
            programme prog = new programme();
            title[] t = new title[1] { new title() { Value = "Firefly", lang = dl } };
            //title[] t = new title[1] { new title() { Value = "blabla", lang = dl } };
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

            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            dbcon.Open();
            transaction = dbcon.con.BeginTransaction();

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
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
            throw new NotImplementedException();
        }

        public void GetFreebaseData(UpdateProgressEvent progressEvent, int num_records, string dataFile)
        {
            //  The dump file reader
            StreamReader data_reader = new StreamReader(dataFile); //Environment.CurrentDirectory
            String[] IDs = new String[num_records];
            string[] crr_line_words;
            string line = data_reader.ReadLine();

            for (int i = 0; i < num_records; i++)
            {
                try
                {
                    line = data_reader.ReadLine();
                    crr_line_words = line.Split('\t');
                    IDs[i] = crr_line_words[1];
                }
                catch (Exception)
                {
                    num_records = i;
                    break;
                }
            }

            data_reader.Close();

            String[] descriptions = ObjectLayer.Importer.getDescByMID(IDs);

            data_reader = new StreamReader(dataFile);

            // The sql transaction.
            MySqlTransaction transaction;

            // The SQL data command.
            MySqlCommand cmd;
            line = data_reader.ReadLine(); ;
            string query;

            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection(); 
            dbcon.Open();
            transaction = dbcon.con.BeginTransaction();
            try
            {
                for (int i = 0; i < num_records; i++)
                {
                    line = data_reader.ReadLine();
                    crr_line_words = line.Split('\t');

                    string program_freebase_id = crr_line_words[1];
                    string description = descriptions[i];

                    query = "INSERT IGNORE INTO Program (name, country_of_origin, freebase_id, description)" +
                        "VALUES (@name, @country_of_origin, @freebase_id, @description)";
                    cmd = new MySqlCommand(query, dbcon.con, transaction);

                    cmd.Parameters.AddWithValue("@name", crr_line_words[0]);
                    cmd.Parameters.AddWithValue("@freebase_id", program_freebase_id);
                    cmd.Parameters.AddWithValue("@country_of_origin", crr_line_words[7]);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.ExecuteNonQuery();

                    // Get last inserted id
                    query = "SELECT MAX(id) FROM Program";
                    cmd = new MySqlCommand(query, dbcon.con, transaction);
                    Object obj = cmd.ExecuteScalar();
                    int program_id = Convert.ToInt32(obj);

                    // Go through the actors and add to DB the connections between actors and programs

                    //string[] actors = crr_line_words[13].Split(',');
                    //long program_id = cmd.LastInsertedId; //Doen't work :-(

                    //string actors = "'" + crr_line_words[13].Replace(",", "','") + "'"; // Seperated by commas
                    //string[] apc = crr_line_words[13].Split(',');
                    //insert actor-program connections

                    //query = "SELECT id FROM Actor WHERE freebase_id IN (@actors)";
                    //cmd = new MySqlCommand(query, m_connection, transaction);

                    //cmd.Parameters.AddWithValue("@actors", actors);

                    //MySqlDataReader reader = cmd.ExecuteReader();

                    //bool b = reader.Read();
                    //foreach (string a in apc)//while (reader.Read())
                    //{
                    //    query = "INSERT INTO ProgramActor (program_id, apc_freebase_id)" +
                    //        "VALUES (@program_id, @apc_freebase_id)";
                    //    //int actor_id = reader.GetInt32(0);

                    //    cmd = new MySqlCommand(query, m_connection, transaction);

                    //    cmd.Parameters.AddWithValue("@program_id", program_id);
                    //    cmd.Parameters.AddWithValue("@apc_freebase_id", a);

                    //    cmd.ExecuteNonQuery();
                    //}
                    //reader.Close();

                    progressEvent("Save to database.", (int)(((double)i) / num_records * 100));
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
                data_reader.Close();
                dbcon.Close(); 
                ConnectionPool.freeConnection(dbcon);
            }
        }

        public void GetFreebaseActor(UpdateProgressEvent progressEvent, int num_records, string filePath)
        {
            //  The dump file reader
            StreamReader data_reader = new StreamReader(filePath); //Environment.CurrentDirectory
            String[] IDs = new String[num_records];
            string[] crr_line_words;
            string line = data_reader.ReadLine();

            for (int i = 0; i < num_records; i++)
            {
                line = data_reader.ReadLine();
                crr_line_words = line.Split('\t');
                IDs[i] = crr_line_words[1];
            }

            data_reader.Close();

            String[] descriptions = ObjectLayer.Importer.getDescByMID(IDs);

            data_reader = new StreamReader(filePath);

            // The sql transaction.
            MySqlTransaction transaction;

            // The SQL data command.
            MySqlCommand cmd;
            line = data_reader.ReadLine(); ;
            string query;

            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            dbcon.Open();
            transaction = dbcon.con.BeginTransaction();
            try
            {
                for (int i = 0; i < num_records; i++)
                {
                    line = data_reader.ReadLine();
                    crr_line_words = line.Split('\t');

                    string id = crr_line_words[1];
                    string description = descriptions[i];

                    /*
                     * query = "INSERT IGNORE INTO Actor (name, birth_date, gender, freebase_id, biography)" +
                        "VALUES (@name, @birth_date, @gender, @freebase_id, @biography)";
                     */

                    query = "INSERT IGNORE INTO Actor (name, freebase_id, biography)" +
                        "VALUES (@name, @freebase_id, @biography)";
                    cmd = new MySqlCommand(query, dbcon.con, transaction);


                    cmd.Parameters.AddWithValue("@name", crr_line_words[0]);
                    cmd.Parameters.AddWithValue("@freebase_id", id);
                    //cmd.Parameters.AddWithValue("@birth_date", crr_line_words[0]);
                    //cmd.Parameters.AddWithValue("@gender", crr_line_words[0]);
                    cmd.Parameters.AddWithValue("@biography", description);

                    cmd.ExecuteNonQuery();
                    progressEvent("Save actors to database.", (int)(((double)i) / num_records * 100));
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
                data_reader.Close();
                dbcon.Close(); 
                ConnectionPool.freeConnection(dbcon);
            }
        }

        public void GetFreebaseActorProgram(UpdateProgressEvent progressEvent, int num_records, string filePath)
        {
            //  The dump file reader
            StreamReader data_reader = new StreamReader(filePath); //Environment.CurrentDirectory
            string[] crr_line_words;
            string line = data_reader.ReadLine();

            // The sql transaction.
            MySqlTransaction transaction;

            // The SQL data command.
            MySqlCommand cmd;

            line = data_reader.ReadLine(); ;
            string query;

            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection(); 
            dbcon.Open();
            transaction = dbcon.con.BeginTransaction();
            try
            {
                for (int i = 0; i < num_records; i++)
                {
                    line = data_reader.ReadLine();
                    crr_line_words = line.Split('\t');

                    string actor = crr_line_words[2];
                    string program = crr_line_words[4];

                    query = "SELECT id FROM Actor WHERE name = @name";
                    cmd = new MySqlCommand(query, dbcon.con, transaction);

                    cmd.Parameters.AddWithValue("@name", actor);

                    Object obj = cmd.ExecuteScalar();
                    int actor_id = Convert.ToInt32(obj);

                    query = "SELECT id FROM Program WHERE name = @name";
                    cmd = new MySqlCommand(query, dbcon.con, transaction);

                    cmd.Parameters.AddWithValue("@name", program);

                    obj = cmd.ExecuteScalar();
                    int program_id = Convert.ToInt32(obj);

                    /*
                     * query = "INSERT IGNORE INTO Actor (name, birth_date, gender, freebase_id, biography)" +
                        "VALUES (@name, @birth_date, @gender, @freebase_id, @biography)";
                     */

                    query = "INSERT IGNORE INTO ProgramActor (program_id, actor_id)" +
                        "VALUES (@program_id, @actor_id)";
                    cmd = new MySqlCommand(query, dbcon.con, transaction);

                    cmd.Parameters.AddWithValue("@program_id", actor_id);
                    cmd.Parameters.AddWithValue("@actor_id", program_id);

                    cmd.ExecuteNonQuery();
                    progressEvent("Save actors to database.", (int)(((double)i) / num_records * 100));
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
                data_reader.Close();
                dbcon.Close(); ConnectionPool.freeConnection(dbcon);
            }
        }

        public void insertUser(string name, string password)
        {
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "INSERT INTO User (name,password) VALUES (@name, @password)";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                dbcon.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            dbcon.Close();
            ConnectionPool.freeConnection(dbcon);
        }

        public int GetUserID(string name, string password)
        {
            int id = -1;
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT id, name FROM User WHERE name=@name AND password=@password";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                dbcon.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
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
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
            return id;
        }

        public int getProgID(string name)
        {
            int id = -1;
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT id FROM Program WHERE name=@name";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@name", name);
            try
            {
                dbcon.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
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
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
            return id;
        }

        public int getProgRating(int pid, int uid)
        {
            int rating = -1;
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT rating FROM UserProgram WHERE user_id=@user_id AND program_id=@program_id";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@user_id", uid);
            cmd.Parameters.AddWithValue("@program_id", pid);
            try
            {
                dbcon.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    rating = (int)result;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
            return rating;
        }

        public void setProgRating(int pid, int uid, int rating)
        {
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "REPLACE INTO UserProgram (user_id,program_id,rating) VALUES (@user_id, @program_id,@rating)";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@user_id", uid);
            cmd.Parameters.AddWithValue("@program_id", pid);
            cmd.Parameters.AddWithValue("@rating", rating);
            try
            {
                dbcon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
        }

        public int getActorsRating(int pid, int uid)
        {
            int rating = -1;
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT avg(ua.rating) FROM UserActor as ua,ProgramActor as pa WHERE " +
                "ua.user_id=@user_id AND pa.program_id=@program_id AND ua.actor_id=pa.actor_id";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@user_id", uid);
            cmd.Parameters.AddWithValue("@program_id", pid);
            try
            {
                dbcon.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    float resf = float.Parse(result.ToString());
                    rating = (int)resf;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
            return rating;
        }

        public void setActorRating(int pid, int uid, int rating, int prev)
        {
            MySqlDataReader dr = null;
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT actor_id From ProgramActor WHERE " +
                            "program_id=@program_id";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@program_id", pid);
            try
            {
                dbcon.Open();
                dr = cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
            List<int> ids = new List<int>();
            while (dr.Read())
            {
                ids.Add(dr.GetInt32(0));
            }
            dbcon.Close();

            dbcon.Open();
            MySqlTransaction transaction = dbcon.con.BeginTransaction();
            try
            {
                foreach (int i in ids)
                {
                    if (prev == -1)
                    {
                        query = "INSERT IGNORE INTO UserActor (user_id,actor_id,rating,rated_num) " +
                             "VALUES (@user_id, @actor_id,0,0);" +
                              "UPDATE UserActor SET rating=((rating*rated_num)+@rating)/(rated_num+1)," +
                              "rated_num=rated_num+1 where user_id=@user_id AND actor_id=@actor_id";
                        cmd = new MySqlCommand(query, dbcon.con, transaction);
                    }
                    else
                    {
                        query = "UPDATE UserActor SET rating=((rating*(rated_num))+@rating-@prev)/(rated_num)," +
                             "rated_num=rated_num where user_id=@user_id AND actor_id=@actor_id";
                        cmd = new MySqlCommand(query, dbcon.con, transaction);
                        cmd.Parameters.AddWithValue("@prev", prev);
                    }
                    cmd.Parameters.AddWithValue("@user_id", uid);
                    cmd.Parameters.AddWithValue("@actor_id", i);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
        }

        /// <summary>
        /// Save the user orders to the database.
        /// </summary>
        /// <param name="lstOrderDetails">The orders to sace.</param>
         /// <summary>
        /// Save the user orders to the database.
        /// </summary>
        /// <param name="lstOrderDetails">The orders to sace.</param>
        public void SaveOrders(List<OrderDetail> lstOrderDetails)
        {
            // Variables

            // The sql transaction.
            MySqlTransaction transaction = null;

            // The SQL data command.
            MySqlCommand cmd;

            // Code

            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection(); 
            dbcon.Open();

            try
            {
                transaction = dbcon.con.BeginTransaction();
                cmd = new MySqlCommand("DELETE FROM UserOrderedPrograms WHERE userId = @userId", dbcon.con, transaction);
                cmd.Parameters.AddWithValue("@userId", lstOrderDetails[0].UserId);
                cmd.ExecuteNonQuery();

                // Save the channels.
                int i = 0;
                foreach (var order in lstOrderDetails)
                {
                    cmd = new MySqlCommand("SELECT id FROM Channel WHERE xmltv_id = @xmltv_id", dbcon.con);
                    cmd.Parameters.AddWithValue("@xmltv_id", lstOrderDetails[i++].ChanId);
                    object ret = cmd.ExecuteScalar();
                    if (ret != null && (ret is int))
                    {
                        int chanId = (int)ret;
                        cmd = new MySqlCommand(
                        "INSERT INTO UserOrderedPrograms (userId, channeId, start) VALUES (@userId, @channeId, @start)",
                        dbcon.con, transaction);
                        cmd.Parameters.AddWithValue("@userId", order.UserId);
                        cmd.Parameters.AddWithValue("@channeId", chanId);
                        cmd.Parameters.AddWithValue("@start", order.Start);
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                dbcon.Close(); 
                ConnectionPool.freeConnection(dbcon);
            }
        }

        /// <summary>
        /// Get the user orders.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The list of orders details.</returns>
        public List<OrderDetail> LoadOrders(int userId)
        {
            List<OrderDetail> lstOrders = new List<OrderDetail>();
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT xmltv_id, start FROM View_UserOrderedPrograms WHERE userId = @userId";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@userId", userId);
            
            try
            {
                dbcon.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrderDetail order = new OrderDetail(userId,
                        reader.GetString(0),
                        reader.GetDateTime(1), true);
                    lstOrders.Add(order);
                }

                return lstOrders;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
        }

        /// <summary>
        /// Get the users.
        /// </summary>
        /// <returns></returns>
        public List<UserDetail> GetUsers()
        {
            List<UserDetail> lstUsers = new List<UserDetail>();
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "SELECT id, name, admin FROM User";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);

            try
            {
                dbcon.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserDetail user = new UserDetail(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetBoolean(2));
                    lstUsers.Add(user);
                }

                return lstUsers;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();ConnectionPool.freeConnection(dbcon);
            }
        }

        public void DeleteUser(int userId)
        {
            string query = "DELETE FROM User WHERE id = @id";
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@id", userId);

            try
            {
                dbcon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
        }

        /// <summary>
        /// Change the admin.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="isAdmin">The admin value.</param>
        public void ChangeAdmin(int userId, bool isAdmin)
        {
            ConnectionPool.DBPoolCon dbcon = ConnectionPool.getConnection();
            string query = "UPDATE User SET admin = @admin WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, dbcon.con);
            cmd.Parameters.AddWithValue("@id", userId);
            cmd.Parameters.AddWithValue("@admin", isAdmin);

            try
            {
                dbcon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbcon.Close();
                ConnectionPool.freeConnection(dbcon);
            }
        }

        #endregion
    }
}
