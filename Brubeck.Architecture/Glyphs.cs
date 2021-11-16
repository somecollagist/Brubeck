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

            public static Qyte[] ConvertCharToQyteArray(Qit[] map)
            {
                if (map.Length != Width * Height) throw new SegmentationFaultException($"Given Qitmap's length is not {Width * Height}");
                return Enumerable.Range(0, Width * Height / 3)
                    .Select(t => new Qyte(map[(t * 3)..((t + 1) * 3)]))
                    .ToArray();
            }

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
        }
    }
}
