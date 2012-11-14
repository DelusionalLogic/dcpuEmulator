﻿using System;
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

        public void initialize()
        {
        }

        public void start()
        {
        }

        public void step()
        {
        }

        public void dispose()
        {
        }

        public void openConfig()
        {
        }

        public string Name
        {
            get { return "DefaultCPU"; }
        }
        public string Description
        {
            get { return "A CPU that is to spec"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.0(Not Started)"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }
}
