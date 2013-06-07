using System.ComponentModel;

namespace Laurus.TaskBoss.Service
{
    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
        /// <summary> 
        /// Required designer variable. 
        /// </summary> private System.ComponentModel.Container components = null;

        public ProjectInstaller()
        {
            // This call is required by the Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call 
        }
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 =
              new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 =
              new System.ServiceProcess.ServiceInstaller();
            // serviceProcessInstaller1 
            this.serviceProcessInstaller1.Account =
              System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // serviceInstaller1 
            this.serviceInstaller1.ServiceName = "TaskBossService";
            this.serviceInstaller1.StartType =
              System.ServiceProcess.ServiceStartMode.Automatic;

            // ProjectInstaller 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] 
            { 
                this.serviceInstaller1, 
                this.serviceProcessInstaller1
            });
        }
    }
}
