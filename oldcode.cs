using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diablo
{
    class oldcapture
    {

        private void CaptureScreen()
        {

            Bitmap BMP = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Graphics GFX = System.Drawing.Graphics.FromImage(BMP);
            GFX.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size, System.Drawing.CopyPixelOperation.SourceCopy);
            DataContainer.img1 = BMP;
            pictureBox2.Image = DataContainer.img1;
            CropCapture();

        }

        private void CropCapture()
        {

            List<Point> TopLeft, BottomRight;
            int X1, X2, Y1, Y2, sizeX, sizeY;
            X1 = 20000; X2 = 20000; Y1 = 0; Y2 = 0;

            //Box topleft 3 pixels
            byte TL11, TL12, TL13, TL14, TL15, TL16, TL17, TL18, TL19;
            byte TL21, TL22, TL23, TL24, TL25, TL26, TL27, TL28, TL29;
            byte TL31, TL32, TL33, TL34, TL35, TL36, TL37, TL38, TL39;
            byte TL41, TL42, TL43, TL44, TL45, TL46, TL47, TL48, TL49;

            //box bottomright 3 pixels
            byte BR11, BR12, BR13, BR14, BR15, BR16, BR17, BR18, BR19;
            byte BR21, BR22, BR23, BR24, BR25, BR26, BR27, BR28, BR29;
            byte BR31, BR32, BR33, BR34, BR35, BR36, BR37, BR38, BR39;
            byte BR41, BR42, BR43, BR44, BR45, BR46, BR47, BR48, BR49;
            byte BR51, BR52, BR53, BR54, BR55, BR56, BR57, BR58, BR59;

            //EQUIPPED TEST
            int PixX1 = 0;
            int PixY1 = 0;
            int PixX2 = 0; int PixY2 = 0;


            TL11 = 0; TL12 = 0; TL13 = 0; TL14 = 0; TL15 = 0; TL16 = 0; TL17 = 0; TL18 = 0; TL19 = 0;
            TL21 = 0; TL22 = 0; TL23 = 0; TL24 = 0; TL25 = 0; TL26 = 0; TL27 = 0; TL28 = 0; TL29 = 0;
            TL31 = 0; TL32 = 0; TL33 = 0; TL34 = 0; TL35 = 0; TL36 = 0; TL37 = 0; TL38 = 0; TL39 = 0;
            TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 0; TL45 = 0; TL46 = 0; TL47 = 0; TL48 = 0; TL49 = 0;

            BR11 = 0; BR12 = 0; BR13 = 0; BR14 = 0; BR15 = 0; BR16 = 0; BR17 = 0; BR18 = 0; BR19 = 0;
            BR21 = 0; BR22 = 0; BR23 = 0; BR24 = 0; BR25 = 0; BR26 = 0; BR27 = 0; BR28 = 0; BR29 = 0;
            BR31 = 0; BR32 = 0; BR33 = 0; BR34 = 0; BR35 = 0; BR36 = 0; BR37 = 0; BR38 = 0; BR39 = 0;
            BR41 = 0; BR42 = 0; BR43 = 0; BR44 = 0; BR45 = 0; BR46 = 0; BR47 = 0; BR48 = 0; BR49 = 0;
            BR51 = 0; BR52 = 0; BR53 = 0; BR54 = 0; BR55 = 0; BR56 = 0; BR57 = 0; BR58 = 0; BR59 = 0;

            //richTextBox3.AppendText(pictureBox2.Height + ":" + pictureBox2.Width);
            if ((pictureBox2.Height == 1200) && (pictureBox2.Width == 1920))
            {
                TL11 = 0; TL12 = 0; TL13 = 0; TL14 = 93; TL15 = 69; TL16 = 22; TL17 = 93; TL18 = 69; TL19 = 22;
                TL21 = 0; TL22 = 0; TL23 = 0; TL24 = 90; TL25 = 67; TL26 = 21; TL27 = 93; TL28 = 69; TL29 = 22;
                TL31 = 2; TL32 = 2; TL33 = 1; TL34 = 93; TL35 = 69; TL36 = 22; TL37 = 93; TL38 = 69; TL39 = 22;
                TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 15; TL45 = 11; TL46 = 3; TL47 = 38; TL48 = 28; TL49 = 9;
                PixX1 = 192; PixY1 = 12; //EQUIPPED COORDS
            }
            if ((pictureBox2.Height == 1080) && (pictureBox2.Width == 1920))
            {
                TL11 = 0; TL12 = 0; TL13 = 0; TL14 = 16; TL15 = 12; TL16 = 4; TL17 = 38; TL18 = 28; TL19 = 9;
                TL21 = 0; TL22 = 0; TL23 = 0; TL24 = 33; TL25 = 25; TL26 = 8; TL27 = 87; TL28 = 65; TL29 = 21;
                TL31 = 0; TL32 = 0; TL33 = 0; TL34 = 36; TL35 = 26; TL36 = 8; TL37 = 87; TL38 = 65; TL39 = 21;
                TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 15; TL45 = 11; TL46 = 3; TL47 = 38; TL48 = 28; TL49 = 9;
                PixX1 = 173; PixY1 = 11; //EQUIPPED COORDS
            }

            if ((pictureBox2.Height == 1050) && (pictureBox2.Width == 1680))
            {
                TL11 = 0; TL12 = 0; TL13 = 0; TL14 = 68; TL15 = 51; TL16 = 16; TL17 = 87; TL18 = 65; TL19 = 21;
                TL21 = 0; TL22 = 0; TL23 = 0; TL24 = 25; TL25 = 18; TL26 = 6; TL27 = 32; TL28 = 23; TL29 = 7;
                TL31 = 0; TL32 = 0; TL33 = 0; TL34 = 26; TL35 = 19; TL36 = 6; TL37 = 32; TL38 = 23; TL39 = 7;
                TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 71; TL45 = 53; TL46 = 17; TL47 = 87; TL48 = 65; TL49 = 21;
                PixX1 = 168; PixY1 = 11; //EQUIPPED COORDS
            }

            if ((pictureBox2.Height == 1024) && (pictureBox2.Width == 1280))
            {
                TL11 = 0; TL12 = 0; TL13 = 0; TL14 = 32; TL15 = 23; TL16 = 7; TL17 = 32; TL18 = 23; TL19 = 7;
                TL21 = 0; TL22 = 0; TL23 = 0; TL24 = 84; TL25 = 62; TL26 = 20; TL27 = 84; TL28 = 62; TL29 = 20;
                TL31 = 0; TL32 = 0; TL33 = 0; TL34 = 26; TL35 = 19; TL36 = 6; TL37 = 32; TL38 = 23; TL39 = 7;
                TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 71; TL45 = 53; TL46 = 17; TL47 = 87; TL48 = 65; TL49 = 21;
                PixX1 = 150; PixY1 = 10; //EQUIPPED COORDS in E not Q
            }

            if ((pictureBox2.Height == 768) && (pictureBox2.Width == 1366))
            {
                TL11 = 0; TL12 = 1; TL13 = 0; TL14 = 78; TL15 = 55; TL16 = 14; TL17 = 72; TL18 = 52; TL19 = 13;
                TL21 = 0; TL22 = 2; TL23 = 0; TL24 = 77; TL25 = 54; TL26 = 14; TL27 = 73; TL28 = 52; TL29 = 13;
                TL31 = 0; TL32 = 0; TL33 = 0; TL34 = 26; TL35 = 19; TL36 = 6; TL37 = 32; TL38 = 23; TL39 = 7;
                TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 71; TL45 = 53; TL46 = 17; TL47 = 87; TL48 = 65; TL49 = 21;
                PixX1 = 123; PixY1 = 7; //EQUIPPED COORDS in E not Q
            }

            if ((pictureBox2.Height == 900) && (pictureBox2.Width == 1440))
            {
                TL11 = 0; TL12 = 0; TL13 = 0; TL14 = 46; TL15 = 33; TL16 = 8; TL17 = 64; TL18 = 46; TL19 = 12;
                TL21 = 0; TL22 = 0; TL23 = 0; TL24 = 43; TL25 = 31; TL26 = 7; TL27 = 65; TL28 = 47; TL29 = 13;
                TL31 = 0; TL32 = 0; TL33 = 0; TL34 = 26; TL35 = 19; TL36 = 6; TL37 = 32; TL38 = 23; TL39 = 7;
                TL41 = 0; TL42 = 0; TL43 = 0; TL44 = 71; TL45 = 53; TL46 = 17; TL47 = 87; TL48 = 65; TL49 = 21;
                PixX1 = 144; PixY1 = 9; //EQUIPPED COORDS Q
            }


            // find item box top left pixels
            TopLeft = FindAllPixelLocations(DataContainer.img1, TL11, TL12, TL13, TL14, TL15, TL16, TL17, TL18, TL19);
            foreach (Point test in TopLeft)
            {
                if (X1 > test.X)
                {
                    X1 = test.X; Y1 = test.Y;

                    //test for EQUIPPED
                    Color pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1, Math.Abs(test.Y - PixY1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                    pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1, Math.Abs(test.Y - PixY1 + 1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                    pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1 + 1, Math.Abs(test.Y - PixY1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                    pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1 + 1, Math.Abs(test.Y - PixY1 - 1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }

                    //test if pixel +1,+1 is black, it's not good
                    pixelColorz = DataContainer.img1.GetPixel(test.X + 1, Math.Abs(test.Y + 1));
                    if ((pixelColorz.R == 0) && (pixelColorz.B == 0)) { X1 = 20000; }
                }

            }

            if (X1.Equals(20000))
            {
                TopLeft = FindAllPixelLocations(DataContainer.img1, TL21, TL22, TL23, TL24, TL25, TL26, TL27, TL28, TL29);
                foreach (Point test in TopLeft)
                {
                    if (X1 > test.X)
                    {
                        X1 = test.X; Y1 = test.Y;
                        Color pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1, Math.Abs(test.Y - PixY1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1, Math.Abs(test.Y - PixY1 + 1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1 + 1, Math.Abs(test.Y - PixY1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1 + 1, Math.Abs(test.Y - PixY1 - 1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }

                        pixelColorz = DataContainer.img1.GetPixel(test.X + 1, Math.Abs(test.Y + 1));
                        if ((pixelColorz.R == 0) && (pixelColorz.B == 0)) { X1 = 20000; }
                    }

                }
            }

            if (X1.Equals(20000))
            {
                TopLeft = FindAllPixelLocations(DataContainer.img1, TL31, TL32, TL33, TL34, TL35, TL36, TL37, TL38, TL39);
                foreach (Point test in TopLeft)
                {
                    if (X1 > test.X)
                    {
                        X1 = test.X; Y1 = test.Y;
                        Color pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1, Math.Abs(test.Y - PixY1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1, Math.Abs(test.Y - PixY1 + 1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1 + 1, Math.Abs(test.Y - PixY1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(test.X + PixX1 + 1, Math.Abs(test.Y - PixY1 - 1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X1 = 20000; }

                        pixelColorz = DataContainer.img1.GetPixel(test.X + 1, Math.Abs(test.Y + 1));
                        if ((pixelColorz.R == 0) && (pixelColorz.B == 0)) { X1 = 20000; }

                    }

                }
            }
            if (X1.Equals(20000))
            {
                TopLeft = FindAllPixelLocations(DataContainer.img1, TL41, TL42, TL43, TL44, TL45, TL46, TL47, TL48, TL49);
                foreach (Point test in TopLeft)
                {
                    if (X1 > test.X) { X1 = test.X; Y1 = test.Y; }

                }
            }




            if ((pictureBox2.Height == 1200) && (pictureBox2.Width == 1920))
            {

                BR11 = 24; BR12 = 18; BR13 = 5; BR14 = 49; BR15 = 38; BR16 = 11; BR17 = 0; BR18 = 0; BR19 = 0;
                BR21 = 25; BR22 = 19; BR23 = 5; BR24 = 47; BR25 = 37; BR26 = 11; BR27 = 0; BR28 = 0; BR29 = 0;
                BR31 = 25; BR32 = 18; BR33 = 5; BR34 = 48; BR35 = 37; BR36 = 11; BR37 = 1; BR38 = 1; BR39 = 0;
                BR41 = 21; BR42 = 16; BR43 = 5; BR44 = 12; BR45 = 9; BR46 = 3; BR47 = 0; BR48 = 0; BR49 = 0;
                PixX2 = 228; PixY2 = 23;
            }
            if ((pictureBox2.Height == 1080) && (pictureBox2.Width == 1920))
            {
                BR11 = 16; BR12 = 12; BR13 = 3; BR14 = 9; BR15 = 7; BR16 = 2; BR17 = 0; BR18 = 0; BR19 = 0;
                BR21 = 21; BR22 = 16; BR23 = 5; BR24 = 11; BR25 = 8; BR26 = 2; BR27 = 0; BR28 = 0; BR29 = 0;
                BR31 = 16; BR32 = 13; BR33 = 4; BR34 = 8; BR35 = 6; BR36 = 2; BR37 = 0; BR38 = 0; BR39 = 0;
                BR41 = 21; BR42 = 16; BR43 = 5; BR44 = 12; BR45 = 9; BR46 = 3; BR47 = 0; BR48 = 0; BR49 = 0;
                PixX2 = 228; PixY2 = 23;
            }

            if ((pictureBox2.Height == 1050) && (pictureBox2.Width == 1680))
            {

                BR11 = 15; BR12 = 11; BR13 = 3; BR14 = 22; BR15 = 17; BR16 = 5; BR17 = 0; BR18 = 0; BR19 = 0;
                BR21 = 18; BR22 = 14; BR23 = 4; BR24 = 27; BR25 = 21; BR26 = 6; BR27 = 0; BR28 = 0; BR29 = 0;
                BR31 = 18; BR32 = 13; BR33 = 4; BR34 = 29; BR35 = 22; BR36 = 6; BR37 = 0; BR38 = 0; BR39 = 0;
                BR41 = 14; BR42 = 11; BR43 = 3; BR44 = 23; BR45 = 18; BR46 = 5; BR47 = 0; BR48 = 0; BR49 = 0;
                PixX2 = 221; PixY2 = 41;
            }

            if ((pictureBox2.Height == 1024) && (pictureBox2.Width == 1280))
            {

                BR11 = 25; BR12 = 18; BR13 = 7; BR14 = 39; BR15 = 30; BR16 = 9; BR17 = 0; BR18 = 0; BR19 = 0;
                BR21 = 21; BR22 = 15; BR23 = 6; BR24 = 32; BR25 = 25; BR26 = 7; BR27 = 0; BR28 = 0; BR29 = 0;
                BR31 = 21; BR32 = 16; BR33 = 6; BR34 = 32; BR35 = 25; BR36 = 7; BR37 = 0; BR38 = 0; BR39 = 0;
                BR41 = 25; BR42 = 19; BR43 = 7; BR44 = 38; BR45 = 29; BR46 = 8; BR47 = 0; BR48 = 0; BR49 = 0;
                PixX2 = 239; PixY2 = 41;
            }

            if ((pictureBox2.Height == 768) && (pictureBox2.Width == 1366))
            {

                BR11 = 39; BR12 = 29; BR13 = 15; BR14 = 41; BR15 = 31; BR16 = 9; BR17 = 0; BR18 = 0; BR19 = 0;
                BR21 = 40; BR22 = 29; BR23 = 15; BR24 = 39; BR25 = 30; BR26 = 9; BR27 = 0; BR28 = 0; BR29 = 0;
                BR31 = 32; BR32 = 23; BR33 = 12; BR34 = 33; BR35 = 25; BR36 = 7; BR37 = 0; BR38 = 0; BR39 = 0;
                BR41 = 32; BR42 = 24; BR43 = 12; BR44 = 32; BR45 = 25; BR46 = 7; BR47 = 0; BR48 = 0; BR49 = 0;
                PixX2 = 170; PixY2 = 31;
            }

            if ((pictureBox2.Height == 900) && (pictureBox2.Width == 1440))
            {

                BR11 = 57; BR12 = 43; BR13 = 12; BR14 = 26; BR15 = 20; BR16 = 6; BR17 = 0; BR18 = 0; BR19 = 0;
                BR21 = 49; BR22 = 37; BR23 = 11; BR24 = 24; BR25 = 19; BR26 = 5; BR27 = 0; BR28 = 0; BR29 = 0;
                BR31 = 49; BR32 = 37; BR33 = 11; BR34 = 26; BR35 = 20; BR36 = 6; BR37 = 0; BR38 = 0; BR39 = 0;
                BR41 = 49; BR42 = 37; BR43 = 11; BR44 = 26; BR45 = 20; BR46 = 6; BR47 = 0; BR48 = 0; BR49 = 0;
                PixX2 = 199; PixY2 = 36;
            }

            // find item box bottom right pixels



            BottomRight = FindAllPixelLocations(DataContainer.img1, BR11, BR12, BR13, BR14, BR15, BR16, BR17, BR18, BR19);
            foreach (Point test in BottomRight)
            {


                if (X2 > test.X)
                {
                    X2 = test.X; Y2 = test.Y;

                    //check for EQUIPPED using pixel color orange in the Q
                    Color pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 - PixY2));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                    pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 - (PixY2 + 1)));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }

                    /*
                    pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 + PixY1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                    pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2 +1 ), Math.Abs(Y1 + PixY1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                    */

                    //if (X2 > (X1 + 500)) { X2 = 20000; }


                }


            }
            if (X2.Equals(20000))
            {

                BottomRight = FindAllPixelLocations(DataContainer.img1, BR21, BR22, BR23, BR24, BR25, BR26, BR27, BR28, BR29);
                foreach (Point test in BottomRight)
                {
                    if (X2 > test.X)
                    {
                        X2 = test.X; Y2 = test.Y;

                        Color pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 - PixY2));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 - (PixY2 + 1)));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }

                        /*
                    pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 + PixY1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                    pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2 + 1), Math.Abs(Y1 + PixY1));
                    if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }

                    //if (X2 > (X1 + 500)) { X2 = 20000; }
                    //if (X2 < 100) { X2 = 20000; }
                         * 
                         */
                    }
                }
            }

            if (X2.Equals(20000))
            {
                BottomRight = FindAllPixelLocations(DataContainer.img1, BR31, BR32, BR33, BR34, BR35, BR36, BR37, BR38, BR39);
                foreach (Point test in BottomRight)
                {
                    if (X2 > test.X)
                    {
                        X2 = test.X; Y2 = test.Y;
                        Color pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 - PixY2));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 - (PixY2 + 1)));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }


                        /*
                        pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2), Math.Abs(Y1 + PixY1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                        pixelColorz = DataContainer.img1.GetPixel(Math.Abs(test.X - PixX2 + 1), Math.Abs(Y1 + PixY1));
                        if ((pixelColorz.R == 255) && (pixelColorz.B == 0)) { X2 = 20000; }
                        //if (X2 > (X1 + 500)) { X2 = 20000; }
                        //if (X2 < 100) { X2 = 20000; }
                         * */
                    }
                }
            }

            if (X2.Equals(20000))
            {
                BottomRight = FindAllPixelLocations(DataContainer.img1, BR41, BR42, BR43, BR44, BR45, BR46, BR47, BR48, BR49);
                foreach (Point test in BottomRight)
                {
                    if (X2 > test.X)
                    {
                        X2 = test.X; Y2 = test.Y;
                    }
                }
            }



            //richTextBox3.AppendText(X1 + ","+ Y1 + ";"+X2 + "," + Y2+"\n");


            X1 = X1 + 1;
            //if ((pictureBox2.Height == 1200) && (pictureBox2.Width == 1920)){ Y1 = Y1 - 37; }
            X2 = X2 + 1;


            //richTextBox3.AppendText("X2:"+X2.ToString() + " . Y2:" + Y2.ToString());

            sizeX = X2 - X1;
            sizeY = Y2 - Y1;


            imgrectal.X = X1;
            imgrectal.Y = Y1;
            imgrectal.Width = sizeX;
            imgrectal.Height = sizeY;


            DataContainer.imgcropped = cropImage(DataContainer.img1, imgrectal);
            pictureBox2.Image = DataContainer.imgcropped;




            string zeronumb = "\\0";
            if (picturenumber < 100)
            {
                zeronumb = zeronumb + "0";
            }
            if (picturenumber < 10)
            {
                zeronumb = zeronumb + "0";
            }


            string imagesave = DataContainer.imgdir + zeronumb + picturenumber.ToString() + ".jpg";
            //richTextBox3.AppendText("Picture saved as : " + imagesave + "\n");
            //richTextBox3.ScrollToCaret();

            pictureBox2.Image.Save(imagesave, ImageFormat.Jpeg);

            //string imagesave = imgname + zeronumb + picturenumber.ToString()+".bmp"; 
            //richTextBox1.AppendText("Picture saved as : "+ imagesave + "\n");
            //pictureBox2.Image.Save(imagesave,ImageFormat.Bmp);


            RefreshList();
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            picturenumber = picturenumber + 1;

            pictureBox2.Refresh();


            //richTextBox1.AppendText("finished\n");
        }

        public static List<Point> FindAllPixelLocations(Bitmap img, byte ColorR1, byte ColorG1, byte ColorB1, byte ColorR2, byte ColorG2, byte ColorB2, byte ColorR3, byte ColorG3, byte ColorB3)
        {
            List<Point> points = new List<Point>();

            //int c = color.ToArgb();

            BitmapData bmData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - img.Width * 3;

                //byte red, green, blue;
                //string grobl;

                for (int y = 0; y < img.Height; ++y)
                {
                    for (int x = 0; x < (img.Width); ++x)
                    {
                        //blue = p[0];
                        //green = p[1];
                        //red = p[2];

                        if (p[0].Equals(ColorB1))
                        {
                            if (p[1].Equals(ColorG1))
                            {
                                if (p[2].Equals(ColorR1))
                                {
                                    if (p[3].Equals(ColorB2))
                                    {
                                        if (p[4].Equals(ColorG2))
                                        {
                                            if (p[5].Equals(ColorR2))
                                            {
                                                if (p[6].Equals(ColorB3))
                                                {
                                                    if (p[7].Equals(ColorG3))
                                                    {
                                                        if (p[8].Equals(ColorR3))
                                                        {
                                                            points.Add(new Point(x, y));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //pixel suivant

                        p += 3; //3
                    }
                    p += nOffset;
                }
            }

            img.UnlockBits(bmData);





            /*

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (c.Equals(img.GetPixel(x, y).ToArgb()))
                    {
                        points.Add(new Point(x, y));
                        //this.richTextBox1.AppendText(c.ToString());
                    }
                
                }
            }
             
            */

            return points;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string listbfile = listBox1.SelectedItem.ToString();
                button4.Enabled = true;
                button3.Enabled = true;
                //string listbtext = "OCR not ready yet";
                pictureBox2.ImageLocation = DataContainer.imgdir + "\\" + listbfile;

                _originalImage = pictureBox2.Image;
                //DataContainer.img1 = new Bitmap(pictureBox2.Image);  TODO click reset image after loading from list empties imagebox2

            }
        }


        /* check for stats after OCR.. currently non functional
        private string Stats(string strlook)
        {
            string boxstr = richTextBox2.Text;
            //int boxlong = boxstr.Length;
            int resultstr = boxstr.IndexOf(strlook, 0, StringComparison.CurrentCultureIgnoreCase);
            string statvalue = "0";
            if (resultstr > 0)
            {
                int resultstr2 = resultstr - 7;
                int resultplus = boxstr.IndexOf("+", resultstr2);
                int resultlong = resultstr - resultplus;
                statvalue = boxstr.Substring(resultplus + 1, resultlong - 2);
                
            }
            
            return statvalue;
        }*/

        private void itemType()
        {
            string boxstr = comboBox1.SelectedItem.ToString();
            //richTextBox3.AppendText(boxstr);
            ItemTypeFinal = boxstr;
            /*
            string[] itemtypes = new string[] { "Helm", "Shoulders", "Amulet", "Chest Armor","Cloak", "Gloves", "Mighty Belt", "Belt", "Bracers", "Ring", "Pants", "Boots", "Spirit Stone", "Voodoo Mask", "Wizard Hat",   //armor
                "Quiver", "Shield", "Mojo", " Source", //offhand
                //"Two-Handed Axe", "Two-Handed Mace", "Two-Handed Mighty Weapon", //2H 1st part
                "Hand Crossbow", "Axe", "Sword", "Spear", "Mace", "Dagger", "Mighty Weapon", "Fist Weapon", "Ceremonial Knife", "Wand",  //1Handed
                "Crossbow", "Bow", "Daibo", "Staff", "Polearm", //2H 2nd part
                "Enchantress Focus", "Scoundrel Token", "Templar Relic" //follower
            
            };

            foreach (string itemlook in itemtypes)
            {

                //richTextBox3.AppendText(itemlook);

                int resultstr = boxstr.IndexOf(itemlook, 0, StringComparison.CurrentCultureIgnoreCase);
                
                if (resultstr > 0)
                {
                    int twohanded = boxstr.IndexOf("Two-Hand", 0, StringComparison.CurrentCultureIgnoreCase);
                    if (twohanded > 0)
                    {
                        richTextBox3.AppendText("Item Type : Two-Handed " + itemlook + "\n");
                        ItemTypeFinal = "Two-handed " + itemlook;
                    }
                    else { richTextBox3.AppendText("Item Type : " + itemlook + "\n");
                    ItemTypeFinal = itemlook;
                    }
                    break;
                }
                else { ItemTypeFinal = "Not Found"; }
                
            }
             */

        }

        private void clickcombo()
        {

            //string searchvalue = "0", tmpstat = "0", tmpdataa = "0";

            richTextBox3.Clear();
            string boxstr = comboBox1.SelectedItem.ToString();
            boxstr = boxstr.Replace("- ", "");
            ItemTypeFinal = boxstr;



            /*
                        if ((ItemTypeFinal != "Armor") && (ItemTypeFinal != "----------") && (checkBox1.Checked == true))
                        {

                            System.Drawing.Font currentFont = this.richTextBox3.SelectionFont;
                            this.richTextBox3.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold);

                            richTextBox3.SelectionColor = System.Drawing.Color.FromName("Red");
                            richTextBox3.AppendText("Maximum stats for : ");
                            richTextBox3.SelectionColor = System.Drawing.Color.FromName("Black");
                            richTextBox3.AppendText(ItemTypeFinal + "\n\n");

                            DisplayItemValue("Dexterity"); DisplayItemValue("Intelligence"); DisplayItemValue("Strength"); DisplayItemValue("Vitality");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Critical Hit Chance"); DisplayItemValue("Critical Hit Damage"); DisplayItemValue("Resistance to All Elements");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Attack Speed"); DisplayItemValue("Movement Speed");
                            DisplayItemValue("Magic Find"); DisplayItemValue("Extra Gold from Monsters");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Life On Hit"); DisplayItemValue("Damage Dealt Is Converted to Life");
                            DisplayItemValue("% Life");
                            DisplayItemValue("Life per Second");
                            DisplayItemValue("Life after Each Kill");
                            DisplayItemValue("Life per Spirit Spent");
                            DisplayItemValue("Health Globes Grant +X Life");
                            richTextBox3.AppendText("\n");

                            DisplayItemValue("Monster kills grant extra Experience");
                            DisplayItemValue("Increase Gold and Health Pickup Radius");
                            DisplayItemValue("Level Requirement Reduced");
                            DisplayItemValue("Armor Bonus");
                            DisplayItemValue("Chance to Block");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Reduces damage from melee attacks");
                            DisplayItemValue("Reduces damage from ranged attacks");
                            DisplayItemValue("Reduces damage from elites");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Increases Spirit Regeneration");
                            DisplayItemValue("Increases Hatred Regeneration");
                            DisplayItemValue("Increases Mana Regeneration");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Melee attackers take X Damage");
                            DisplayItemValue("Reduce duration of control impairing effects");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Physical Resistance");
                            DisplayItemValue("Cold Resistance");
                            DisplayItemValue("Fire Resistance");
                            DisplayItemValue("Lightning Resistance");
                            DisplayItemValue("Poison Resistance");
                            DisplayItemValue("Arcane Resistance");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Chance to Stun on Hit");
                            DisplayItemValue("Chance to Knockback on Hit");
                            DisplayItemValue("Chance to Slow on Hit");
                            DisplayItemValue("Chance to Immobilize on Hit");
                            DisplayItemValue("Chance to Freeze on Hit");
                            DisplayItemValue("Chance to Fear on Hit");
                            DisplayItemValue("Chance to Chill on Hit");
                            richTextBox3.AppendText("\n");
                            DisplayItemValue("Maximum Arcane Power");
                            DisplayItemValue("Maximum Discipline");
                            DisplayItemValue("Maximum Mana");
                            DisplayItemValue("Critical Hits grant Arcane Power");


                            DisplayItemValue("Bonus Maximum Damage");
                            DisplayItemValue("Bonus Minimum Damage");
                            DisplayItemValue("Bonus Elemental Damage");
                            DisplayItemValue("Bonus Cold Damage");




                        }*/

            if ((ItemTypeFinal != "Armor") && (ItemTypeFinal != "----------")) //&& (checkBox1.Checked == false))
            {

                System.Drawing.Font currentFont = this.richTextBox3.SelectionFont;
                this.richTextBox3.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Bold);

                richTextBox3.SelectionColor = System.Drawing.Color.FromName("Red");
                richTextBox3.AppendText("Maximum stats for : ");
                richTextBox3.SelectionColor = System.Drawing.Color.FromName("Black");
                richTextBox3.AppendText(ItemTypeFinal + "\n\n");

                DisplayItemValue("Dexterity"); DisplayItemValue("Intelligence"); DisplayItemValue("Strength"); DisplayItemValue("Vitality");
                richTextBox3.AppendText("\n");
                DisplayItemValue("Health Globes Grant +X Life"); DisplayItemValue("Life per Second"); DisplayItemValue("Life after Each Kill"); DisplayItemValue("Life On Hit"); DisplayItemValue("Life per Spirit Spent"); DisplayItemValue("Damage Dealt Is Converted to Life"); DisplayItemValue("% Life");
                richTextBox3.AppendText("\n");
                DisplayItemValue("Magic Find"); DisplayItemValue("Extra Gold from Monsters"); DisplayItemValue("Monster kills grant extra Experience"); DisplayItemValue("Movement Speed"); DisplayItemValue("Increase Gold and Health Pickup Radius"); DisplayItemValue("Level Requirement Reduced");
                richTextBox3.AppendText("\n");
                DisplayItemValue("Critical Hit Chance"); DisplayItemValue("Critical Hit Damage"); DisplayItemValue("Attack Speed"); DisplayItemValue("Sockets");
                richTextBox3.AppendText("\n");
                DisplayItemValue("Armor Bonus"); DisplayItemValue("Chance to Block"); DisplayItemValue("Reduces damage from melee attacks"); DisplayItemValue("Reduces damage from ranged attacks"); DisplayItemValue("Reduces damage from elites"); DisplayItemValue("Melee attackers take X Damage"); DisplayItemValue("Reduce duration of control impairing effects");
                richTextBox3.AppendText("\n");
                DisplayItemValue("Resistance to All Elements"); DisplayItemValue("Physical Resistance"); DisplayItemValue("Cold Resistance"); DisplayItemValue("Fire Resistance"); DisplayItemValue("Lightning Resistance"); DisplayItemValue("Poison Resistance"); DisplayItemValue("Arcane Resistance");
                richTextBox3.AppendText("\n");
                DisplayItemValue("Chance to Stun on Hit"); DisplayItemValue("Chance to Knockback on Hit"); DisplayItemValue("Chance to Slow on Hit"); DisplayItemValue("Chance to Immobilize on Hit"); DisplayItemValue("Chance to Freeze on Hit"); DisplayItemValue("Chance to Fear on Hit"); DisplayItemValue("Chance to Chill on Hit"); DisplayItemValue("Increases Spirit Regeneration"); DisplayItemValue("Increases Hatred Regeneration"); DisplayItemValue("Increases Mana Regeneration"); DisplayItemValue("Maximum Arcane Power"); DisplayItemValue("Maximum Discipline"); DisplayItemValue("Maximum Mana"); DisplayItemValue("Maximum Fury"); DisplayItemValue("Critical Hits grant Arcane Power"); DisplayItemValue("Bonus Maximum Damage"); DisplayItemValue("Bonus Minimum Damage"); DisplayItemValue("Bonus Elemental Damage"); DisplayItemValue("Bonus Cold Damage"); DisplayItemValue("Barbarian Bash Damage"); DisplayItemValue("Barbarian Cleave Damage"); DisplayItemValue("Barbarian Frenzy Damage"); DisplayItemValue("Barbarian Rend Resource Cost"); DisplayItemValue("Barbarian Revenge Critical"); DisplayItemValue("Barbarian Weapon Throw Resource Cost"); DisplayItemValue("DH Chakram Resource Cost"); DisplayItemValue("DH Evasive Fire Damage"); DisplayItemValue("DH Grenades Damage"); DisplayItemValue("DH Impale Resource Cost"); DisplayItemValue("DH Spike Trap Damage"); DisplayItemValue("Monk Crippling Wave Damage"); DisplayItemValue("Monk Cyclone Strike Resource Cost"); DisplayItemValue("Monk Deadly Reach Damage"); DisplayItemValue("Monk Exploding Palm Damage"); DisplayItemValue("Monk Fists of Thunder Damage"); DisplayItemValue("Monk Sweeping Wind Damage"); DisplayItemValue("Monk Way of the Hundred Fists Damage"); DisplayItemValue("Wizard Arcane Torrent Resource Cost"); DisplayItemValue("Wizard Disintegrate Resource Cost"); DisplayItemValue("Wizard Electrocute Damage"); DisplayItemValue("Wizard Explosive Bat Critical"); DisplayItemValue("Wizard Hydra Resource Cost"); DisplayItemValue("Wizard Ray of Frost Critical"); DisplayItemValue("WD Acid Cloud Critical"); DisplayItemValue("WD Firebats Resource Cost"); DisplayItemValue("WD Locust Swarm Damage"); DisplayItemValue("WD Summon Zombie Dogs Reduce Cooldown"); DisplayItemValue("Barbarian Hammer of the Ancients Resource Cost"); DisplayItemValue("Barbarian Overpower Critical"); DisplayItemValue("Barbarian Seismic Slam Critical"); DisplayItemValue("Barbarian Whirlwind Critical"); DisplayItemValue("DH Bola Shot Damage"); DisplayItemValue("DH Elemental Arrow Damage"); DisplayItemValue("DH Entangling Shot Damage"); DisplayItemValue("DH Hungering Arrow Damage"); DisplayItemValue("DH Multishot Critical"); DisplayItemValue("DH Rapid Fire Critical"); DisplayItemValue("Monk Lashing Tail Kick Resource Cost"); DisplayItemValue("Monk Tempest Rush Critical"); DisplayItemValue("Monk Wave of Light Critical"); DisplayItemValue("WD Firebomb Resource Cost"); DisplayItemValue("WD Haunt Damage"); DisplayItemValue("WD Plague of Toads"); DisplayItemValue("WD Poison Darts Damage"); DisplayItemValue("WD Spirit Barrage Damage"); DisplayItemValue("WD Wall of Zombies Reduce Cooldown"); DisplayItemValue("WD Zombie Charger Resource Cost"); DisplayItemValue("Wizard Energy Twister Critical"); DisplayItemValue("Wizard Magic Missile Damage"); DisplayItemValue("Wizard Arcane Orb Critical"); DisplayItemValue("Wizard Blizzard Increase Duration"); DisplayItemValue("Wizard Meteor Resource Cost"); DisplayItemValue("Wizard Shock Pulse Damage"); DisplayItemValue("Wizard Spectral Blade Damage");



            }


            //itemType();  //sets ItemTypeFinal
            //richTextBox3.AppendText(ItemTypeFinal);
            /*
            if ((ItemTypeFinal == "Two-Handed Axe") || (ItemTypeFinal == "Two-Handed Mace") || (ItemTypeFinal == "Two-Handed Mighty Weapon") || (ItemTypeFinal == "Hand Crossbow")
            || (ItemTypeFinal == "Axe") || (ItemTypeFinal == "Sword") || (ItemTypeFinal == "Spear") || (ItemTypeFinal == "Mace") || (ItemTypeFinal == "Dagger")
            || (ItemTypeFinal == "Mighty Weapon") || (ItemTypeFinal == "Fist Weapon") || (ItemTypeFinal == "Ceremonial Knife") || (ItemTypeFinal == "Wand")
            || (ItemTypeFinal == "Crossbow") || (ItemTypeFinal == "Bow") || (ItemTypeFinal == "Daibo") || (ItemTypeFinal == "Staff") || (ItemTypeFinal == "Polearm"))
            {
                ItemTypeFinal = "Weapon";
            }
            if ((ItemTypeFinal == "Quiver") || (ItemTypeFinal == "Shield") || (ItemTypeFinal == "Mojo") || (ItemTypeFinal == " Source")) { ItemTypeFinal = "Offhand"; }

            searchvalue = "Dexterity";


            //tmpstat = Stats(searchvalue);  //value of desired stats
            //richTextBox3.AppendText(searchvalue + " : " + tmpstat + " ");
            if (ItemTypeFinal != "Not Found") { tmpdataa = GetItemData(ItemTypeFinal, searchvalue); } //value of maximum stat for item

            */



            //Stats("Intelligence");
            //Stats("Strength");
            //Stats("Vitality");




            /*Stats("Resistance to All");
            Stats("Fire Resistance");
            Stats("Physical Resistance");
         
            Stats("Extra Gold");
            Stats("Movement Speed");
            Stats("% Life");
            
            Stats("Minimum Damage");
            Stats("Life after Each Kill"); */


        }
    }
}
