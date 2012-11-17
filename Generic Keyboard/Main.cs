using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace Generic_Keyboard
{
    public class Main : IHardware
    {
        public IPluginHost Host { get; set; }

        public bool configPossible { get { return false; } }

        public void openConfig()
        {
        }

        public void initialize()
        {
        }

        public ushort[] interrupt(ushort[] registers)
        {
        }

        public void dispose()
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get { return "Generic Keyboard"; }
        }

        public string Description
        {
            get { return "Generic Keyboard"; }
        }

        public string Author
        {
            get { return "DelusionalLogic"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public uint ID { get { return 0x30cf7406; } }
        public ushort HVersion { get { return 1; } }
        public uint ManufacturerID { get { return 0x0; } }
    }
}
