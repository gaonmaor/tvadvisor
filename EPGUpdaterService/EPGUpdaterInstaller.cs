using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace EPGGruberService
{
    [RunInstaller(true)]
    public partial class EPGUpdaterInstaller : System.Configuration.Install.Installer
    {
        public EPGUpdaterInstaller()
        {
            InitializeComponent();

            // Variables

            // The installation process.
            ServiceProcessInstaller serviceProcessInstaller;

            // The service which handle the installation.
            System.ServiceProcess.ServiceInstaller serviceInstaller;

            // Code

            // Create the process and service.
            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.DisplayName = "EPG Updater";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //# This must be identical to the WindowsService.ServiceBase name
            //# set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = "EPGUpdater";
            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
