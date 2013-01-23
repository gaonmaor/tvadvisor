using System;
using System.Threading;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using EPGGruberService.Properties;
using LogicLayer;

namespace EPGGruberService
{
    /// <summary>
    /// This class update the EPG into the database each given interval.
    /// </summary>
    class EPGUpdater
    {
        #region Fields

        /// <summary>
        /// The interacttion with the event loger.
        /// </summary>
        private EventLog m_eventLog;

        /// <summary>
        /// Indicate if the service is active or not.
        /// </summary>
        private bool m_running = true;

        private int userId = 2;

        #endregion

        #region Constructors

        /// <summary>
        /// Create the EPG Uploader.
        /// </summary>
        /// <param name="eventLog">The main log of the program.</param>
        public EPGUpdater(EventLog eventLog)
        {
            // Code

            // Make sure the custom even is exsist.
            m_eventLog = eventLog;
            if (!EventLog.SourceExists("EPGUpdater"))
            {
                EventLog.CreateEventSource("EPGUpdater", "Application");
            }
            m_eventLog.Source = "EPGUpdater";
        }

        #endregion

        #region Operations

        /// <summary>
        /// Takes each interval the epg data - convert to UTf and plant in the database.
        /// </summary>
        public void start()
        {
            // Variables

            // The time between each pull.
            int sleepTime;

            // Code

            // Get the amount of sleep minutes and convert them to milliseconds.
            sleepTime = Convert.ToInt16(Settings.Default.SleepTime) * 60000;

            // The main loop of the service.
            while (m_running)
            {
                // Get the current epg.
                GetCurrentEPG();

                // Insert the data into the EPG server.
                InsertEPG();

                // Build the new epg.
                BuildEPG(userId, Settings.Default.DefaultLang);

#if (DEBUG)
                m_running = false;
#else
				Thread.Sleep(sleepTime);
#endif
            }
        }

        /// <summary>
        /// Get the current EPG using xmltv.
        /// </summary>
        private void GetCurrentEPG()
        {
            log("Grabbing epg: source = " + Settings.Default.OutputFile + " days = " + Settings.Default.Days);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Settings.Default.XMLTVPath;
            psi.Arguments = String.Format("{0} --days {1} --output {2}", Settings.Default.GrabSourceEn, Settings.Default.Days, Settings.Default.OutputFile);
            //psi.Arguments = String.Format("{0} --configure", Settings.Default.GrabSourceEn);
            //psi.Verb = @"runas";
            psi.WindowStyle = ProcessWindowStyle.Normal;
            psi.UseShellExecute = false;
            psi.ErrorDialog = false;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            StreamReader outputReader = p.StandardOutput;
            p.WaitForExit();
            Console.WriteLine(outputReader.ReadToEnd());
            log("Grabbing epg: source = " + Settings.Default.OutputFile + " finished");
        }

        /// <summary>
        /// Stop the service.
        /// </summary>
        public void stop()
        {
            if (!m_running)
            {
                return;
            }
            m_running = false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Insert EPG into DB SP_InsertEPG.
        /// </summary>
        private void InsertEPG()
        {
            log("Inserting to the database.");
            LogicManager.Instance.SaveEPG(Settings.Default.OutputFile);
            log("Insert to the database finished.");
        }

        private void BuildEPG(int userId, string defaultLang)
        {
            log("Build the new EPG.");
            LogicManager.Instance.BuildEPG(Settings.Default.OutputFile, Settings.Default.NewFile, userId, defaultLang);
            log("New epg was created saved in: " + Settings.Default.NewFile);
        }

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="msg"></param>
        private void log(string msg)
        {
#if (DEBUG)
            Console.WriteLine(msg);
#else
            m_eventLog.WriteEntry(msg);
#endif
        }
            
        #endregion
    }
}
