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
        internal static class QChar
        {
            /// <summary>
            /// Width of a character.
            /// </summary>
            /// <remarks>This should include one column of whitespace.</remarks>
            public const int Width = 6;
            /// <summary>
            /// Height of a character.
            /// </summary>
            public const int Height = 8;

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
            public static readonly Qit[] a =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case B <code>b</code>
            /// </summary>
            public static readonly Qit[] b =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case C <code>c</code>
            /// </summary>
            public static readonly Qit[] c =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case D <code>d</code>
            /// </summary>
            public static readonly Qit[] d =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};


            /// <summary>
            /// Lower case E <code>e</code>
            /// </summary>
            public static readonly Qit[] e =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case F <code>f</code>
            /// </summary>
            public static readonly Qit[] f =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case G <code>g</code>
            /// </summary>
            public static readonly Qit[] g =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case H <code>h</code>
            /// </summary>
            public static readonly Qit[] h =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case I <code>i</code>
            /// </summary>
            public static readonly Qit[] i =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case J <code>j</code>
            /// </summary>
            public static readonly Qit[] j =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case K <code>k</code>
            /// </summary>
            public static readonly Qit[] k =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case L <code>l</code>
            /// </summary>
            public static readonly Qit[] l =
            {
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case M <code>m</code>
            /// </summary>
            public static readonly Qit[] m =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case N <code>n</code>
            /// </summary>
            public static readonly Qit[] n =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case O <code>o</code>
            /// </summary>
            public static readonly Qit[] o =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case P <code>p</code>
            /// </summary>
            public static readonly Qit[] p =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case Q <code>q</code>
            /// </summary>
            public static readonly Qit[] q =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case R <code>r</code>
            /// </summary>
            public static readonly Qit[] r =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case S <code>s</code>
            /// </summary>
            public static readonly Qit[] s =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case T <code>t</code>
            /// </summary>
            public static readonly Qit[] t =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case U <code>u</code>
            /// </summary>
            public static readonly Qit[] u =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case V <code>v</code>
            /// </summary>
            public static readonly Qit[] v =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case W <code>w</code>
            /// </summary>
            public static readonly Qit[] w =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case X <code>x</code>
            /// </summary>
            public static readonly Qit[] x =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case Y <code>y</code>
            /// </summary>
            public static readonly Qit[] y =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Lower case Z <code>z</code>
            /// </summary>
            public static readonly Qit[] z =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};


            /// <summary>
            /// Upper case A <code>A</code>
            /// </summary>
            public static readonly Qit[] A =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case B <code>B</code>
            /// </summary>
            public static readonly Qit[] B =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   #   
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case C <code>C</code>
            /// </summary>
            public static readonly Qit[] C =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case D <code>D</code>
            /// </summary>
            public static readonly Qit[] D =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case E <code>E</code>
            /// </summary>
            public static readonly Qit[] E =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case F <code>F</code>
            /// </summary>
            public static readonly Qit[] F =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case G <code>G</code>
            /// </summary>
            public static readonly Qit[] G =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, // # ### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case H <code>H</code>
            /// </summary>
            public static readonly Qit[] H =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case I <code>I</code>
            /// </summary>
            public static readonly Qit[] I =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case J <code>J</code>
            /// </summary>
            public static readonly Qit[] J =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case K <code>K</code>
            /// </summary>
            public static readonly Qit[] K =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case L <code>L</code>
            /// </summary>
            public static readonly Qit[] L =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case M <code>M</code>
            /// </summary>
            public static readonly Qit[] M =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.A, Qit.U, Qit.U, Qit.A, // ## ## 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case N <code>N</code>
            /// </summary>
            public static readonly Qit[] N =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, // ##  # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, // #  ## 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case O <code>O</code>
            /// </summary>
            public static readonly Qit[] O =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case P <code>P</code>
            /// </summary>
            public static readonly Qit[] P =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case Q <code>Q</code>
            /// </summary>
            public static readonly Qit[] Q =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.U, Qit.A, //  ## # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case R <code>R</code>
            /// </summary>
            public static readonly Qit[] R =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, // # #   
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case S <code>S</code>
            /// </summary>
            public static readonly Qit[] S =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case T <code>T</code> 
            /// </summary>
            public static readonly Qit[] T =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case U <code>U</code>
            /// </summary>
            public static readonly Qit[] U =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case V <code>V</code>
            /// </summary>
            public static readonly Qit[] V =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case W <code>W</code>
            /// </summary>
            public static readonly Qit[] W =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.U, Qit.A, Qit.U, Qit.U, Qit.A, // ## ## 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case X <code>X</code>
            /// </summary>
            public static readonly Qit[] X =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case Y <code>Y</code>
            /// </summary>
            public static readonly Qit[] Y =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Upper case Z <code>Z</code>
            /// </summary>
            public static readonly Qit[] Z =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Open parenthesis <code>(</code>
            /// </summary>
            public static readonly Qit[] OPPR =
            {
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Close parenthesis <code>)</code>
            /// </summary>
            public static readonly Qit[] CLPR =
            {
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##     
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Open square brackets <code>[</code>
            /// </summary>
            public static readonly Qit[] OPSQ =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // #####
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Close square brackets <code>]</code>
            /// </summary>
            public static readonly Qit[] CLSQ =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Open brace <code>{</code>
            /// </summary>
            public static readonly Qit[] OPBR =
            {
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, //   ### 
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, //   ### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Close brace <code>}</code>
            /// </summary>
            public static readonly Qit[] CLBR =
            {
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Less than <code>&lt;</code>
            /// </summary>
            public static readonly Qit[] LT =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, //    ## 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Greater than <code>&gt;</code>
            /// </summary>
            public static readonly Qit[] GT =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, // ##    
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Zero <code>0</code>
            /// </summary>
            public static readonly Qit[] _0 =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// One <code>1</code>
            /// </summary>
            public static readonly Qit[] _1 =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, // ###   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Two <code>2</code>
            /// </summary>
            public static readonly Qit[] _2 =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Three <code>3</code>
            /// </summary>
            public static readonly Qit[] _3 =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Four <code>4</code>
            /// </summary>
            public static readonly Qit[] _4 =
            {
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Five <code>5</code>
            /// </summary>
            public static readonly Qit[] _5 =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Six <code>6</code>
            /// </summary>
            public static readonly Qit[] _6 =
            {
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Seven <code>7</code>
            /// </summary>
            public static readonly Qit[] _7 =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Eight <code>8</code>
            /// </summary>
            public static readonly Qit[] _8 =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Nine <code>9</code>
            /// </summary>
            public static readonly Qit[] _9 =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, //  #### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, Qit.A, //  ##   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Space <code> </code>
            /// </summary>
            public static readonly Qit[] SPC =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Full stop <code>.</code>
            /// </summary>
            public static readonly Qit[] STOP =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Comma <code>,</code>
            /// </summary>
            public static readonly Qit[] COMMA =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Question mark <code>?</code>
            /// </summary>
            public static readonly Qit[] QMARK =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Exclamation mark <code>!</code>
            /// </summary>
            public static readonly Qit[] EMARK =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Colon <code>:</code>
            /// </summary>
            public static readonly Qit[] COLON =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Semicolon <code>;</code>
            /// </summary>
            public static readonly Qit[] SCOLON =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Apostrophe <code>'</code>
            /// </summary>
            public static readonly Qit[] APOS =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Speech mark <code>"</code>
            /// </summary>
            public static readonly Qit[] SMARK =
            {
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, // # #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, // # #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Degree symbol <code>­°</code>
            /// </summary>
            public static readonly Qit[] DEG =
            {
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Plus sign <code>+</code>
            /// </summary>
            public static readonly Qit[] PLUS =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Minus sign <code>-</code>
            /// </summary>
            public static readonly Qit[] MINUS =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Asterisk <code>*</code>
            /// </summary>
            public static readonly Qit[] ASTERX =
            {
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Equals sign <code>=</code>
            /// </summary>
            public static readonly Qit[] EQUALS =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Ampersand <code>&</code>
            /// </summary>
            public static readonly Qit[] AMP =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.U, Qit.A, //  ## # 
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, // #  #  
                Qit.A, Qit.U, Qit.U, Qit.A, Qit.U, Qit.A, //  ## # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Hash mark <code>#</code>
            /// </summary>
            public static readonly Qit[] HASH =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Forward slash <code>/</code>
            /// </summary>
            public static readonly Qit[] FSLASH =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Back slash <code>\</code>
            /// </summary>
            public static readonly Qit[] BSLASH =
            {
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// At symbol <code>@</code>
            /// </summary>
            public static readonly Qit[] ATSYM =
            {
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, // #   # 
                Qit.U, Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, // # ### 
                Qit.U, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, // # # # 
                Qit.U, Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, // # ### 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Percent symbol <code>%</code>
            /// </summary>
            public static readonly Qit[] PCENT =
            {
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, // ##  # 
                Qit.U, Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, // ##  # 
                Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, //    #  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, // #  ## 
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.U, Qit.U, // #  ## 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Vertical Bar <code>|</code>
            /// </summary>
            public static readonly Qit[] VBAR =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Underscore <code>_</code>
            /// </summary>
            public static readonly Qit[] USCR =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*      */};

            /// <summary>
            /// Caret / Circumflex <code>^</code>
            /// </summary>
            public static readonly Qit[] CARET =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Tilde <code>~</code>
            /// </summary>
            public static readonly Qit[] TILDE =
            {
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, //   # # 
                Qit.A, Qit.U, Qit.A, Qit.U, Qit.A, Qit.A, //  # #  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, //       
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Pound sign <code>£</code>
            /// </summary>
            public static readonly Qit[] PND =
            {
                Qit.A, Qit.A, Qit.U, Qit.U, Qit.A, Qit.A, //   ##  
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.U, Qit.A, //  #  # 
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, // ####  
                Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, //  #    
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.U, Qit.U, Qit.U, Qit.U, Qit.U, Qit.A, // ##### 
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};

            /// <summary>
            /// Dollar sign <code>$</code>
            /// </summary>
            public static readonly Qit[] DOL =
            {
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.U, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, // #     
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.U, Qit.A, //     # 
                Qit.A, Qit.U, Qit.U, Qit.U, Qit.A, Qit.A, //  ###  
                Qit.A, Qit.A, Qit.U, Qit.A, Qit.A, Qit.A, //   #   
                Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, Qit.A, /*        */};
        }
    }
}
