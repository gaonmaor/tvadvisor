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
            public void Open()
            {
                con.Open();
            }
            public void Close()
            {
                con.Close();
            }
            public MySqlTransaction BeginTransaction()
            {
                return con.BeginTransaction();
            }
            
        }

        private static DBPoolCon[] pool;
        private static bool initialized = false;
        private static int numOfCons = 10;

        public static DBPoolCon getConnection()
        {
            DBPoolCon result = null;
            if (initialized == false)
            {
                initialized = true;
                pool = new DBPoolCon[numOfCons];
                DBPoolCon poolCon = null;
                for (int i = 0; i < pool.Length; i++)
                {
                    poolCon = new DBPoolCon();
                    poolCon.con = new MySqlConnection(DataLayer.Properties.Settings.Default.ConString);
                    poolCon.index = i;
                    pool[i] = poolCon;
                }
            }
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].busy == false)
                {
                    result = pool[i];
                    result.busy = true;
                }
            }
            return result;
        }

        public static void freeConnection(DBPoolCon poolCon)
        {
            if (poolCon.index >= 0 && poolCon.index <= numOfCons)
            {
                poolCon.busy = false;
                poolCon.Close();
            }
        }
    }
}
