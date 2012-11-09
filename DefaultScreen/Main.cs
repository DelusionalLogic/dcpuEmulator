using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace DefaultScreen
{
    public class Main : IScreen
    {
        public IPluginHost Host { get; set; }

        public void openConfig()
        {
        }

        public void initialize()
        {
        }

        public void dispose()
        {
        }

        public string Name
        {
            get { return "Default Screen"; }
        }
        public string Description
        {
            get { return "A normal screen following the spec"; }
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
