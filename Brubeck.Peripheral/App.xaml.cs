﻿using System;
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
        [STAThread]
        public static void Start()
        {
            //Make all obejcts here so we don't share any instances over threads
            App Peripheral = new();
            Console.WriteLine("Running...");
            Peripheral.Run(new Monitor());
            Console.WriteLine("Run!");
        }
    }
}
