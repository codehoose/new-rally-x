using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Use MakeMap {path to map}");
                return;
            }

            using (Bitmap img = new Bitmap(args[0]))
            {
                int[] map = new int[120 * 192];
                FillWithFauna(map);

                // Sand road = 0
                // Block is 25

                int xOffset = 12;
                int yOffset = 12;
                const int BLOCK = 25;
                const int TL = 3;
                const int TM = 4;
                const int TR = 11;
                const int ML = 24;
                const int MR = 32;
                const int BL = 171;
                const int BM = 172;
                const int BR = 179;


                for (int y = 0; y < img.Height; y++)
                {
                    for (int x = 0; x < img.Width; x++)
                    {
                        int actualY = (yOffset + y) * 120;
                        int index = actualY + xOffset + x;

                        if (img.GetPixel(x, y).G == 0)
                        {
                            map[index] = 0;
                        }
                        else
                        {
                            // Check the block's position
                            if (x - 1 >= 0 && img.GetPixel(x - 1, y).G == 0 &&
                                y - 1 >= 0 && img.GetPixel(x - 1, y - 1).G == 0 &&
                                img.GetPixel(x, y - 1).G == 0)
                            {
                                map[index] = TL;
                            }
                            else if (x+1 < img.Width && img.GetPixel(x+1,y).G == 0 &&
                                y -1 >= 0 && img.GetPixel(x+1, y-1).G==0 &&
                                img.GetPixel(x,y-1).G == 0)
                            {
                                map[index] = TR;
                            }

                            else if (x - 1 >= 0 && img.GetPixel(x - 1, y).G == 0 &&
                                y + 1 < img.Height && img.GetPixel(x - 1, y + 1).G == 0 &&
                                img.GetPixel(x, y + 1).G == 0)
                            {
                                map[index] = BL;
                            }
                            else if (x + 1 < img.Width && img.GetPixel(x + 1, y).G == 0 &&
                                y + 1 < img.Width && img.GetPixel(x + 1, y + 1).G == 0 &&
                                img.GetPixel(x, y + 1).G == 0)
                            {
                                map[index] = BR;
                            }

                            else if (x-1 >= 0 && img.GetPixel(x-1, y).G == 0 &&
                                x+1 < img.Width && img.GetPixel(x+1, y).G != 0)
                            {
                                map[index] = ML;
                            }
                            else if (x +1 < img.Width && img.GetPixel(x+1, y).G == 0 &&
                                x-1 >= 0 && img.GetPixel(x-1, y).G != 0)
                            {
                                map[index] = MR;
                            }
                            else if (y - 1 >= 0 && img.GetPixel(x, y - 1).G == 0)
                            {
                                map[index] = TM;
                            }
                            else if (y + 1 < img.Height && img.GetPixel(x, y + 1).G == 0)
                            {
                                map[index] = BM;
                            }
                            else
                            {
                                map[index] = BLOCK;
                            }
                        }
                    }
                }

                var result = string.Join(",", map.Select(x => x.ToString()).ToArray());
                Console.WriteLine(result);
            }          
        }

        private static void FillWithFauna(int[] map)
        {
            int[] blocks = new int[] { 189, 210, 231 };

            for (int y = 0; y < 192; y++)
            {
                int yclamped = y % 3;
                int startBlockId = blocks[yclamped];

                for (int x = 0; x < 120; x++)
                {
                    int xclamped = x % 3;
                    int block = startBlockId + xclamped;
                    map[y * 120 + x] = block;
                }
            }
        }
    }
}
