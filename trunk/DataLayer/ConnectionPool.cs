using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    public static class ConnectionPool
    {
        public class DBPoolCon
        {
            public MySqlConnection con;
            public bool busy = false;
            public int index = -1;
            public bool open = false;
            public void Open()
            {
                if (!open)
                {
                    con.Open();
                }
                open = true;
            }
            public void Close()
            {
                con.Close();
                open = false;
            }
            public MySqlTransaction BeginTransaction()
            {
                return con.BeginTransaction();
            }
            
        }

        private static List<DBPoolCon> pool;
        private static bool initialized = false;
        private static int numOfCons = 0;

        public static DBPoolCon getConnection()
        {
            DBPoolCon result = null;
            if (initialized == false)
            {
                initialized = true;
                numOfCons++;
                pool = new List<DBPoolCon>();//[numOfCons];
                DBPoolCon poolCon = null;
                for (int i = 0; i < pool.Count(); i++)
                {
                    poolCon = new DBPoolCon();
                    poolCon.con = new MySqlConnection(DataLayer.Properties.Settings.Default.ConString);
                    poolCon.index = i;
                    pool.Add(poolCon);
                }
            }
            for (int i = 0; i < pool.Count(); i++)
            {
                if (pool[i].busy == false)
                {
                    result = pool.ElementAt(i);
                    result.busy = true;
                }
            }
            if (result == null)
            {
                DBPoolCon poolCon = new DBPoolCon();
                poolCon.con = new MySqlConnection(DataLayer.Properties.Settings.Default.ConString);
                poolCon.index = pool.Count();
                pool.Add(poolCon);
                numOfCons++;
                result = poolCon;
                result.busy = true;
            }
            return result;
        }

        public static void freeConnection(DBPoolCon poolCon)
        {
            if (poolCon.index >= 0 && poolCon.index <= numOfCons)
            {
                poolCon.busy = false;
                //poolCon.Close();
            }
        }

        public static void emptyPool()
        {
            for (int i = 0; i < pool.Count(); i++)
            {
                pool[i].Close();
            }
            pool = null;
            initialized = false;
            numOfCons = 0;
        }
    }
}
