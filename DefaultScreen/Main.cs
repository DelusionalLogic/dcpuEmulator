﻿using System;
using System.Threading;
using System.Windows.Forms;
using PluginInterface;

namespace DefaultScreen
{
    /// <summary>
    /// The plugin interface
    /// </summary>
    public class Main : IHardware
    {
        public IPluginHost Host { get; set; }

        private ScreenGui screen;

        public bool configPossible { get { return false; } }

        public void openConfig()
        {
        }

        public void initialize()
        {
            new Thread(openGui).Start();
        }

        [STAThread]
        private void openGui()
        {
            //Open the screen gui
            screen = new ScreenGui(Host);
            Application.Run(screen);
        }

        public ushort[] interrupt(ushort[] registers)
        {
            while (screen == null) ;
            return screen.interrupt(registers);
        }

        public void dispose()
        {
            throw new NotImplementedException();
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
            get { return "0.9"; }
        }

        public uint ID { get { return 0x7349f615; } }
        public ushort HVersion { get { return 0x1802; } }
        public uint ManufacturerID { get { return 0x1c6c8b36; } }
    }
}