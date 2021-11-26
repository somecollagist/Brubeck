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
        public static readonly int ResWidth = 240;

        /// <summary>
        /// Height of the monitor in pixels.
        /// </summary>
        public static readonly int ResHeight = 184;

        /// <summary>
        /// Scale factor of the monitor.
        /// </summary>
        /// <remarks>How many pixels on a real monitor make up a pixel on the emulated monitor.</remarks>
        private const int ScaleFactor = 4;

        /// <summary>
        /// Constructor for a monitor.
        /// </summary>
        public Monitor()
        {
            InitializeComponent();

            //Set the grid's size to the size in "real pixels" i.e. number of pixels on a real monitor
            Screen.Height = ResHeight * ScaleFactor;
            Screen.Width = ResWidth * ScaleFactor;

            //Dynamically create cells in the uniformgrid
            Pixels.Rows = ResHeight;
            Pixels.Columns = ResWidth;

            //Make each cell of the uniformgrid a rectangle which can be coloured (i.e. a pixel)
            for(int y = 0; y < ResHeight; y++)
            {
                for(int x = 0; x < ResWidth; x++)
                {
                    Pixels.Children.Insert((y * ResWidth) + x, new Rectangle());
                }
            }
        }

        /// <summary>
        /// Displays the given video signal.
        /// </summary>
        /// <param name="input">Qyte array of the visual signal to display.</param>
        public async void Display(Qyte[] input)
        {
            //Qyte[] might become Qit[] in future, since that'd align more closely with how it works in the real world

            //Convert to qitmap since that's how we'll store the state
            Qit[] VisualQitMap = string.Join("", input.Select(t => t.ToString())).Select(t => QitConverter.GetQitFromChar(t)).ToArray();
            //Each qit will be used to set the colour of each pixel
            for(int index = 0; index < VisualQitMap.Length; index++)
            {
                SetPixel(index, QitColourMapping[VisualQitMap[index]]);
            }
        }

        /// <summary>
        /// Dictionary pairing Qits to colours.
        /// </summary>
        private static readonly Dictionary<Qit, SolidColorBrush> QitColourMapping = new()
        {
            {Qit.A, new(Color.FromRgb(0, 0, 0)) },  //Black
            {Qit.E, new(Color.FromRgb(0, 0, 0)) },  //Black
            {Qit.I, new(Color.FromRgb(0, 0, 0)) },  //Black
            {Qit.O, new(Color.FromRgb(0, 0, 0)) },  //Black
            {Qit.U, new(Color.FromRgb(0, 255, 0)) } //Green
        };

        /// <summary>
        /// Sets a pixel at a given coordinate to a certain colour.
        /// </summary>
        /// <param name="x">Pixel's altitude from the top.</param>
        /// <param name="y">Pixel's offset from the left.</param>
        /// <param name="colour">The colour to be assigned.</param>
        private async void SetPixel(int x, int y, SolidColorBrush colour)
        {
            await Task.Run(() => SetPixel(y * ResWidth + x, colour));
        }

        /// <summary>
        /// Sets a pixel at a given index to a certain colour. 
        /// </summary>
        /// <param name="index">The pixels index, computed left to right, top to bottom.</param>
        /// <param name="colour">the colour to be assigned.</param>
        /// <returns></returns>
        private async void SetPixel(int index, SolidColorBrush colour)
        {
            await Task.Run(() => ((Rectangle)Pixels.Children[index]).Fill = colour);
        }
    }
}
