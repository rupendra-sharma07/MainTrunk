///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Services.TributePortalSchedulerService.TributeService.cs
///Author          : 
///Creation Date   : 
///Description     : This file starts the scheduler service
///Audit Trail     : Date of Modification  Modified By         Description


using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;


namespace TributePortalSchedulerService
{
    static class TributeService
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new TributeAdminstator() };

            ServiceBase.Run(ServicesToRun);
        }
    }
}