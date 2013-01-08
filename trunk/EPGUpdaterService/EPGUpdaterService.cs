using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace EPGGruberService
{
    /// <summary>
    /// The service for the EPG UPloader.
    /// </summary>
    public partial class EPGUpdaterService : ServiceBase
    {
        /// <summary>
        /// The EPG Uploader main refference.
        /// </summary>
        private EPGUpdater epgu;

        public EPGUpdaterService()
        {

            // Variables

            // Code

            InitializeComponent();

            this.ServiceName = "EPGUpdater";
            this.EventLog.Log = "Application";

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            // Create the EPG Uploader service.
            epgu = new EPGUpdater(this.EventLog);
        }

        protected override void OnStart(string[] args)
        {
            // Variables

            // The thread start and thread running the EPG update service.
            ThreadStart ts;
            Thread t;

            // Code

            // Start the EPG Uploader on a different thread.
            ts = new ThreadStart(epgu.start);
            t = new Thread(ts);
            t.Start();
        }

        protected override void OnStop()
        {
            // Code

            // Stop the EPG uploader.
            epgu.stop();
        }
    }
}
