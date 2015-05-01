using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace AudioDivider
{
    class AutoInjectHandler
    {
        Configuration configuration;
        Communication communication;

        ManagementEventWatcher managementEventWatcher1;
        ManagementEventWatcher managementEventWatcher2;
        Logger logger;

        public AutoInjectHandler(Configuration configuration, Communication communication)
        {
            this.logger = Logger.getLogger();

            this.configuration = configuration;
            this.communication = communication;

            managementEventWatcher1 = new ManagementEventWatcher();
            managementEventWatcher1.EventArrived += EventProcessStart1;
            managementEventWatcher1.Query = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa 'Win32_Process'");
            managementEventWatcher1.Start();

            managementEventWatcher2 = new ManagementEventWatcher();
            managementEventWatcher2.EventArrived += EventProcessStart2;
            managementEventWatcher2.Query = new WqlEventQuery("Win32_ProcessStartTrace", new TimeSpan(0, 0, 1));
            managementEventWatcher2.Start();

            RunInjectsAll();
        }

        public void Stop()
        {
            managementEventWatcher1.Stop();
            managementEventWatcher2.Stop();
        }

        void EventProcessStart1(object sender, EventArrivedEventArgs e)
        {
            try
            {
                uint processID = 0;
                foreach (var property in e.NewEvent.Properties)
                {
                    if (property.Name == "TargetInstance")
                    {
                        var dataCollection = (ManagementBaseObject)property.Value;
                        processID = (uint)dataCollection["ProcessID"];
                        RunInject((int)processID, false);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        void EventProcessStart2(object sender, EventArrivedEventArgs e)
        {
            try
            {
                uint processID = (uint)e.NewEvent.Properties["ProcessID"].Value;
                RunInject((int)processID, false);
            }
            catch (Exception)
            {
            }

        }

        void RunInject(int pid, bool always)
        {
            Process process = Process.GetProcessById(pid);
            var autoControls = configuration.GetAutoControls;

            foreach (ProgramAutoInfo autoControl in autoControls)
            {
                try
                {
                    if (process.Modules[0].FileName == autoControl.programPath && autoControl.selectBy == ProgramAutoInfo.SelectBy.ProgramPath || process.MainWindowTitle == autoControl.windowName && autoControl.selectBy == ProgramAutoInfo.SelectBy.Windowname)
                    {
                        if (autoControl.instantHook || always)
                        {
                            Injector injector = new Injector();
                            injector.Inject(process.Id);
                            Thread.Sleep(500);

                            communication.ServerSend(process.Id, 1, autoControl.deviceId);
                            SoundHandler.switchDefaultDevice();
                        }
                        else
                        {
                            if(!delayedInjectPids.Contains(pid))
                                delayedInjectPids.Add(pid);
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        List<int> delayedInjectPids = new List<int>();

        public void RunDelayedInject(int pid)
        {
            if (delayedInjectPids.Contains(pid))
            {
                RunInject(pid, true);
            }
            delayedInjectPids.Remove(pid);
        }

        public void RunInjectsAll()
        {
            var autoControls = configuration.GetAutoControls;

            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                RunInject(process.Id, true);
            }
        }
    }
}
