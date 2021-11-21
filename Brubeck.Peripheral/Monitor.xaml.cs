using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Brubeck.Core;

namespace Brubeck.Peripheral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Monitor : Window
    {
        /// <summary>
        /// Width of the monitor in pixels.
        /// </summary>
        private const int ResWidth = 240;

        /// <summary>
        /// Height of the monitor in pixels.
        /// </summary>
        private const int ResHeight = 184;

        /// <summary>
        /// Scale factor of the monitor.
        /// </summary>
        private const int ScaleFactor = 4;

        public Monitor()
        {
            InitializeComponent();

            Screen.Height = ResHeight * ScaleFactor;
            Screen.Width = ResWidth * ScaleFactor;

            Pixels.Rows = ResHeight;
            Pixels.Columns = ResWidth;

            for(int y = 0; y < ResHeight; y++)
            {
                for(int x = 0; x < ResWidth; x++)
                {
                    Pixels.Children.Insert((y * ResWidth) + x, new Rectangle());
                }
            }
        }

        public async void Display(Qyte[] input)
        {
            Qit[] VisualQitMap = string.Join("", input.Select(t => t.ToString())).Select(t => QitConverter.GetQitFromChar(t)).ToArray();
            for(int index = 0; index < VisualQitMap.Length; index++)
            {
                await SetPixel(index, QitColourMapping[VisualQitMap[index]]);
            }
        }

        private readonly Dictionary<Qit, SolidColorBrush> QitColourMapping = new()
        {
            {Qit.A, new(Color.FromRgb(0, 0, 0)) },
            {Qit.E, new(Color.FromRgb(0, 0, 0)) },
            {Qit.I, new(Color.FromRgb(0, 0, 0)) },
            {Qit.O, new(Color.FromRgb(0, 0, 0)) },
            {Qit.U, new(Color.FromRgb(0, 255, 0)) }
        };

        private async void SetPixel(int x, int y, SolidColorBrush colour)
        {
            await Task.Run(() => SetPixel(y * ResWidth + x, colour));
        }

        private async Task SetPixel(int index, SolidColorBrush colour)
        {
            await Task.Run(() => ((Rectangle)Pixels.Children[index]).Fill = colour);
        }
    }
}
