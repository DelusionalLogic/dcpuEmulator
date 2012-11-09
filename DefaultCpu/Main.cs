using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PluginInterface;

namespace DefaultCpu
{
    public class Main : ICpu
    {
        public IPluginHost Host { get; set; }

        public void openConfig()
        {
        }

        public void initialize()
        {
            new Thread(loop).Start();
        }

        private void loop()
        {
            while (true)
            {
                tick();
            }
        }

        private void tick()
        {
        }

        public void dispose()
        {
        }

        public string Name
        {
            get { return "Default Cpu"; }
        }
        public string Description
        {
            get { return "A normal cpu following the spec"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.1"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }
}
