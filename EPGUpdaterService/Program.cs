using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPGGruberService
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Variables

            // Refference to the epg uploader.
            EPGUpdater epgu;

            // Code;
#if (DEBUG)
            // Create the EPG uploader.
            epgu = new EPGUpdater(new System.Diagnostics.EventLog());
            epgu.start();
#else
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new Service() 
			};
			ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
