using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        /// <summary>
        /// Stores a qitmap of characters.
        /// </summary>
        private static class Char
        {
            /// <summary>
            /// Width of a character.
            /// </summary>
            /// <remarks>This should include one column of whitespace.</remarks>
            public const int Width = 6;
            /// <summary>
            /// Height of a character.
            /// </summary>
            public const int Height = 7;

            /// <summary>
            /// Converts a Qit array of the correct size to a Qyte array which can be stored in data RAM.
            /// </summary>
            /// <param name="map">Qit array to convert.</param>
            public static Qyte[] ConvertCharQitArrayToQyteArray(Qit[] map)
            {
                if (map.Length != Width * Height) throw new SegmentationFaultException($"Given Qitmap's length is not {Width * Height}");
                return Enumerable.Range(0, Width * Height / 3)
                    .Select(t => new Qyte(map[(t * 3)..((t + 1) * 3)]))
                    .ToArray();
            }

            /// <summary>
            /// Lower case A <code>a</code>
            /// </summary>
            public static Qit[] a =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A  //  #### 
            };

            /// <summary>
            /// Lower case B <code>b</code>
            /// </summary>
            public static Qit[] b =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A  // ####  
            };

            /// <summary>
            /// Lower case C <code>c</code>
            /// </summary>
            public static Qit[] c =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Lower case D <code>d</code>
            /// </summary>
            public static Qit[] d =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
            };


            /// <summary>
            /// Lower case E <code>e</code>
            /// </summary>
            public static Qit[] e =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Lower case F <code>f</code>
            /// </summary>
            public static Qit[] f =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
            };

            /// <summary>
            /// Lower case G <code>g</code>
            /// </summary>
            public static Qit[] g =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ### 
            };

            /// <summary>
            /// Lower case H <code>h</code>
            /// </summary>
            public static Qit[] h =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
            };

            /// <summary>
            /// Lower case I <code>i</code>
            /// </summary>
            public static Qit[] i =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
            };

            /// <summary>
            /// Lower case J <code>j</code>
            /// </summary>
            public static Qit[] j =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
            };

            /// <summary>
            /// Lower case K <code>k</code>
            /// </summary>
            public static Qit[] k =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
            };

            /// <summary>
            /// Lower case L <code>l</code>
            /// </summary>
            public static Qit[] l =
            {
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
            };

            /// <summary>
            /// Lower case M <code>m</code>
            /// </summary>
            public static Qit[] m =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Lower case N <code>n</code>
            /// </summary>
            public static Qit[] n =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Lower case O <code>o</code>
            /// </summary>
            public static Qit[] o =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###   
            };

            /// <summary>
            /// Lower case P <code>p</code>
            /// </summary>
            public static Qit[] p =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
            };

            /// <summary>
            /// Lower case Q <code>q</code>
            /// </summary>
            public static Qit[] q =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
            };

            /// <summary>
            /// Lower case R <code>r</code>
            /// </summary>
            public static Qit[] r =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
            };

            /// <summary>
            /// Lower case S <code>s</code>
            /// </summary>
            public static Qit[] s =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Lower case T <code>t</code>
            /// </summary>
            public static Qit[] t =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
            };

            /// <summary>
            /// Lower case U <code>u</code>
            /// </summary>
            public static Qit[] u =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###   
            };

            /// <summary>
            /// Lower case V <code>v</code>
            /// </summary>
            public static Qit[] v =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
            };

            /// <summary>
            /// Lower case W <code>w</code>
            /// </summary>
            public static Qit[] w =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
            };

            /// <summary>
            /// Lower case X <code>x</code>
            /// </summary>
            public static Qit[] x =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Lower case Y <code>y</code>
            /// </summary>
            public static Qit[] y =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Lower case Z <code>z</code>
            /// </summary>
            public static Qit[] z =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
            };


            /// <summary>
            /// Upper case A <code>A</code>
            /// </summary>
            public static Qit[] A =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case B <code>B</code>
            /// </summary>
            public static Qit[] B =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   #   
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
            };

            /// <summary>
            /// Upper case C <code>C</code>
            /// </summary>
            public static Qit[] C =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Upper case D <code>D</code>
            /// </summary>
            public static Qit[] D =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // #### 
            };

            /// <summary>
            /// Upper case E <code>E</code>
            /// </summary>
            public static Qit[] E =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
            };

            /// <summary>
            /// Upper case F <code>F</code>
            /// </summary>
            public static Qit[] F =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
            };

            /// <summary>
            /// Upper case G <code>G</code>
            /// </summary>
            public static Qit[] G =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, // # ### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Upper case H <code>H</code>
            /// </summary>
            public static Qit[] H =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case I <code>I</code>
            /// </summary>
            public static Qit[] I =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
            };

            /// <summary>
            /// Upper case J <code>J</code>
            /// </summary>
            public static Qit[] J =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
            };

            /// <summary>
            /// Upper case K <code>K</code>
            /// </summary>
            public static Qit[] K =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case L <code>L</code>
            /// </summary>
            public static Qit[] L =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
            };

            /// <summary>
            /// Upper case M <code>M</code>
            /// </summary>
            public static Qit[] M =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.A, Qit.U, Qit.U, Qit.A, // ## ## 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case N <code>N</code>
            /// </summary>
            public static Qit[] N =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, // ##  # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, // #  ## 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case O <code>O</code>
            /// </summary>
            public static Qit[] O =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
            };

            /// <summary>
            /// Upper case P <code>P</code>
            /// </summary>
            public static Qit[] P =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
            };

            /// <summary>
            /// Upper case Q <code>Q</code>
            /// </summary>
            public static Qit[] Q =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.U, Qit.A, //  ## # 
            };

            /// <summary>
            /// Upper case R <code>R</code>
            /// </summary>
            public static Qit[] R =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case S <code>S</code>
            /// </summary>
            public static Qit[] S =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
            };

            /// <summary>
            /// Upper case T <code>T</code> 
            /// </summary>
            public static Qit[] T =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
            };

            /// <summary>
            /// Upper case U <code>U</code>
            /// </summary>
            public static Qit[] U =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ### 
            };

            /// <summary>
            /// Upper case V <code>V</code>
            /// </summary>
            public static Qit[] V =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
            };

            /// <summary>
            /// Upper case W <code>W</code>
            /// </summary>
            public static Qit[] W =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.U, Qit.A, Qit.U, Qit.U, Qit.A, // ## ## 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case X <code>X</code>
            /// </summary>
            public static Qit[] X =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
            };

            /// <summary>
            /// Upper case Y <code>Y</code>
            /// </summary>
            public static Qit[] Y =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
            };

            /// <summary>
            /// Upper case Z <code>Z</code>
            /// </summary>
            public static Qit[] Z =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
            };

            /// <summary>
            /// Space <code> </code>
            /// </summary>
            public static Qit[] SPC =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
            };

            /// <summary>
            /// Open Parenthesis <code>(</code>
            /// </summary>
            public static Qit[] OPPR =
            {
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
            };

            /// <summary>
            /// Close Parenthesis <code>)</code>
            /// </summary>
            public static Qit[] CLPR =
            {
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##     
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
            };

            /// <summary>
            /// Open Square Brackets <code>[</code>
            /// </summary>
            public static Qit[] OPSQ =
            {
                // ##### 
                // #     
                // #     
                // #     
                // #     
                // #     
                // #####
            };
        }
    }
}
