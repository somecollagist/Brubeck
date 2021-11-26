using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Brubeck;

namespace Brubeck.Peripheral
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Monitor MonitorInstance { get; private set; } = new();

        [STAThread]
        public static void Start()
        {
            //Make all objects here so we don't share any instances over threads
            MonitorInstance = new();

            App Peripheral = new();
            Console.WriteLine("Running...");
            Peripheral.Run(MonitorInstance);
            Console.WriteLine("Run!");
        }
    }
}
