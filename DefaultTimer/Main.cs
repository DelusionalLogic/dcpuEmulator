using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PluginInterface;

namespace DefaultTimer
{
    public class Main : ITimer
    {
        public IPluginHost Host { get; set; }

        public void initialize()
        {
        }

        public void start()
        {
            new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    DateTime dateTime = new DateTime();
                    if(((dateTime.Ticks / 10) * 10) / (Host.getCPU().getCycles()+1) > 100000)
                        Thread.Sleep(1);
                    Host.getCPU().tick();
                }
            }).Start();
        }

        public void openConfig()
        {
        }

        public void dispose()
        {
        }

        public string Name
        {
            get { return "Default Timer"; }
        }
        public string Description
        {
            get { return "Timer ticking the cpu at 100KHz"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.5"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }
}
