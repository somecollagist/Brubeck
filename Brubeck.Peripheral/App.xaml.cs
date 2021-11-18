using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
        private string[]? args;

        private void StartUp(object Sender, StartupEventArgs e)
        {
            args = e.Args;
        }

        internal void RunEmulator()
        {
            try
            {
                Emulator.Main(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
