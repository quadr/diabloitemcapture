/*
 Copyright Afkbio © 2012 
 
 This file is part of Diablo Item Capture 
 
 Diablo Item Capture is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 
  */

using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Utilities;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections.Specialized;
using Diablo;
using Imageshack;
using Diablo.Properties;







namespace DiabloApp
{
    

    public partial class Form1 : Form
    {

       

        globalKeyboardHook gkh = new globalKeyboardHook();
        
        Rectangle imgrectal;
        
        
         //image cache path
        string maindir, settingsdir, settingsfile, lastfilename, lastfiletmp;
        string ItemTypeFinal = "Not Found";//, ItemData;
        string[][] Itemtemp = new string[1000][];
        string shackresp;

        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        int picturenumber;
                Point squarestart;
        
        string GetTypePos, GetStatPos;

        private Image _originalImage;
        private bool _selecting, _moving;
        private Rectangle _selection;
        private Point _pointmoveA, _pointmoveB; //moving image with right click

        

        public Form1()
        {
            InitializeComponent();
       

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            maindir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DiabloItemCapture";
            settingsdir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DiabloItemCapture\\Data";
            DataContainer.imgdir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DiabloItemCapture\\Cache";
            settingsfile = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DiabloItemCapture\\Data\\settings.xml";

            if (Directory.Exists(maindir)) { }
            else { Directory.CreateDirectory(maindir); }
            if (Directory.Exists(settingsdir)) {}
            else { Directory.CreateDirectory(settingsdir); }
            if (Directory.Exists(DataContainer.imgdir)) { }
            else { Directory.CreateDirectory(DataContainer.imgdir);  }
            if (Directory.Exists(DataContainer.imgdir+"\\bigpic")) { }
            else { Directory.CreateDirectory(DataContainer.imgdir+"\\bigpic"); }
            
            for (int ta = 0; ta < 1000; ta++) { Itemtemp[ta] = new string[1000]; }
            textBox1.Text = DataContainer.imgdir;
            lastfilename = "";

            picturenumber = 0;


            if (File.Exists(settingsfile)){


                using (StreamReader StreamReader = new StreamReader(settingsfile))
                {
                string linecoount = File.ReadAllText(settingsfile);
                String xmllString = linecoount;
                    
         
            // Create an XmlReader
                        using (XmlReader reader = XmlReader.Create(new StringReader(xmllString)))
                        {
                            reader.ReadToFollowing("Imagedir");
                            //richTextBox2.AppendText("Content of the title element: " + reader.ReadElementContentAsString());
                            DataContainer.imgdir = reader.ReadElementContentAsString();
                            textBox1.Text = DataContainer.imgdir;
                            reader.Close();
                
                        }
                }
            }
            else {
                
                // creates settings.xml with default path in it
                new System.Xml.Linq.XDocument(new System.Xml.Linq.XElement("root", new System.Xml.Linq.XElement("Imagedir", System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DiabloItemCapture\\Cache"))).Save(settingsfile);


                 }

                 
        }

       


        private void Form1_Load(object sender, EventArgs e)
        {

            
            
            gkh.HookedKeys.Add(Keys.F12);
            
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.hook();
           


            RefreshListFirst();
            if (picturenumber > 0)
            {
                lastfiletmp = lastfilename.Replace(".jpg", "");
                //richTextBox1.AppendText(lastfiletmp);
                if (lastfiletmp.Contains("0")) { picturenumber = int.Parse(lastfiletmp) + 1; }
            }

            //load item_max_value

            if (File.Exists(settingsdir + "\\item_max_value.csv"))
            {
                StreamReader StreamReader = new StreamReader(settingsdir + "\\item_max_value.csv");
                var linecount = File.ReadAllLines(settingsdir + "\\item_max_value.csv").Length;
                
                string tmptab;
                
                for (int a = 0; a < linecount; a++)
                {
                    tmptab = StreamReader.ReadLine();
                    int numbercomma = CountStringOccurrences(tmptab, ";");
                    string coltmp = "";
                    int poscomma = 0, poscommalast = 0, commalong = 0;

                    for (int z = 0; z < numbercomma; z++)
                    {
                        poscomma = tmptab.IndexOf(";", poscommalast);
                        commalong = poscomma - poscommalast;
                        coltmp = tmptab.Substring(poscommalast, commalong);

                        
                        Itemtemp[z][a] = coltmp;
                        poscommalast = poscomma + 1;
                    }

                }
            }
            else
            {
                //WebClient webClient = new WebClient();
                //webClient.DownloadFile("http://chuckie.free.fr/item_max_value.csv", settingsdir + "\\item_max_value.csv");

                string rsrc = Resources.item_max_value;

                using (FileStream fs = File.Create(settingsdir + "\\item_max_value.csv", rsrc.Length))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(rsrc);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                
                
                StreamReader StreamReader = new StreamReader(settingsdir + "\\item_max_value.csv");
                var linecount = File.ReadAllLines(settingsdir + "\\item_max_value.csv").Length;


                string tmptab;

                for (int a = 0; a < linecount; a++)
                {
                    tmptab = StreamReader.ReadLine();
                    
                    int numbercomma = CountStringOccurrences(tmptab, ";");
                    string coltmp = "";
                    int poscomma = 0, poscommalast = 0, commalong = 0;

                    for (int z = 0; z < numbercomma; z++)
                    {
                        poscomma = tmptab.IndexOf(";", poscommalast);
                        commalong = poscomma - poscommalast;
                        coltmp = tmptab.Substring(poscommalast, commalong);
                        Itemtemp[z][a] = coltmp;
                        poscommalast = poscomma + 1;
                    }
                }
            }
            FileInfo fInfo = new FileInfo(settingsdir + "\\item_max_value.csv");
            //fInfo.IsReadOnly = true;
          }




        


        public string GetItemData(string Idata, string Sdata)
        {
            string resultdata=""; 
            GetItemTypePos(Idata);
            GetItemStatPos(Sdata);
            resultdata = ItemMaxData(GetStatPos, GetTypePos);
            return resultdata;
        }

        public string ItemMaxData(string ItemPosX, string ItemPosY) {
            string ItemMaxValue="";
            ItemMaxValue = Itemtemp[Int64.Parse(ItemPosX)][Int64.Parse(ItemPosY)];
            return ItemMaxValue;
        }

        public void GetItemTypePos(string GetTypelolPos){
            int fu;
            foreach (string[] array in Itemtemp)
            {
                fu = Array.IndexOf(array, GetTypelolPos);
                if (fu != -1) { GetTypePos = fu.ToString(); }
            }
        }

        public void GetItemStatPos(string GetSPos){
            int fu,fucount = 0;
            foreach (string[] array in Itemtemp)
            {
                fu = Array.IndexOf(array, GetSPos);
                if (fu != -1){
                    GetStatPos = fucount.ToString();
                }
                fucount = fucount +1;
            }
        }
           
        
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;

            }
            return count;
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {

            //used to avoid double interception
            if ((milliseconds + 10) > (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond))
            {  

                
                //richTextBox3.AppendText(milliseconds.ToString()+ "\n");
                button3.Enabled = true;
                gkh.unhook();
                

                CaptureScreen();
                
                gkh.hook();
                e.Handled = true;
                
            }
            milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        }  


        public void CaptureScreen()
        {
            Point CropA = new Point(0, 0);
            Point CropB = new Point(0, 0);
            Rectangle firstcrop = new Rectangle(0, 0, 1, 1);

            Bitmap BMP = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Graphics GFX = System.Drawing.Graphics.FromImage(BMP);
            GFX.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size, System.Drawing.CopyPixelOperation.SourceCopy);
            DataContainer.img1 = BMP;
            DataContainer.imgwi = BMP.Width;
            Point MousePos = System.Windows.Forms.Control.MousePosition;
            
            double imagerapport = (Convert.ToDouble(BMP.Width) / BMP.Height);
            

            //int lolppp = Convert.ToInt32(Math.Floor(imagerapport));


            if (imagerapport < 1.6) { DataContainer.screensize = 1; } // 4/3
            else if (imagerapport == 1.6) { DataContainer.screensize = 2; }  //16/10
            else if (imagerapport > 1.6) { DataContainer.screensize = 3; }  //16/9
            //richTextBox3.AppendText("\ng :"+ imagerapport.ToString());

            if (MousePos.X < (DataContainer.img1.Width / 2)) {
                CropA.X = MousePos.X - 10;
                CropB.X = MousePos.X + Convert.ToInt32(DataContainer.img1.Width * 24.45 / 100) + 140;
                
            }
            else { 
                CropA.X = MousePos.X - Convert.ToInt32(DataContainer.img1.Width * 24.45 / 100) - 140;
                CropB.X = MousePos.X + 10;
            }
            //CropX.X = MousePos.X - Convert.ToInt32(DataContainer.img1.Width * 24.45 / 100);
            //richTextBox3.AppendText(CropA.X.ToString() +";"+CropB.X.ToString() +"\n");

            //if ((MousePos.Y + 500) < DataContainer.img1.Height) { firstcrop = new Rectangle(CropA.X, 0, Math.Abs(CropA.X - CropB.X), (MousePos.Y + 500)); }
            firstcrop = new Rectangle(CropA.X, 0, Math.Abs(CropA.X - CropB.X), DataContainer.img1.Height);
            Bitmap firstc = cropImage(DataContainer.img1, firstcrop);
            pictureBox2.Image = firstc;


            //richTextBox3.AppendText("ok capturescreen");
            firstc = FindHeight(firstc);
            pictureBox2.Image = firstc;
 

            firstc = FindWidth(firstc);

               // richTextBox3.AppendText("ok findwidth");
           pictureBox2.Image = firstc;
            //secondc.Dispose();

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
           //BMP.Dispose();
           
        }

        public Bitmap FindHeight(Bitmap image)
        {
            Bitmap returnMap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0,
                                     image.Width, image.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            int a = 0;
            unsafe
            {
                
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                
                // y position of bottom coin
                Int32 bottompos = 0;
                bool bottomposfound = false;

                // y position of top yellow text
                Int32 toppos = 10000;
                for (int i = 0; i < bitmapData1.Height; i++)
                {

                    
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // write the logic implementation here
                        a = (imagePointer1[0] + imagePointer1[1] + imagePointer1[2]) / 3;
                        
                        
                        //imagePointer2[0] = (byte)a;
                        //imagePointer2[1] = (byte)a;
                        //imagePointer2[2] = (byte)a;

                        //top pixel green (set)
                        if ((-1 < imagePointer1[0] && imagePointer1[0] < 1) && (237 < imagePointer1[1] && imagePointer1[1] < 241) && (0 < imagePointer1[2] && imagePointer1[2] < 4))
                        {
                            //get only first iteration of yellow pixel
                            if (toppos > i) { toppos = i; }
                        }

                        //top pixel (yellow title)
                        if ((-1 < imagePointer1[0] && imagePointer1[0] < 1) && (254 < imagePointer1[1] && imagePointer1[1] < 256) && (254 < imagePointer1[2] && imagePointer1[2] < 256))
                        {
                            //get only first iteration of yellow pixel
                            if (toppos > i) { toppos = i; }
                        }
                        //orange (legendary title)
                        if ((45 < imagePointer1[0] && imagePointer1[0] < 49) && (98 < imagePointer1[1] && imagePointer1[1] < 102) && (189 < imagePointer1[2] && imagePointer1[2] < 193))
                        {

                            //get only first iteration of pixel
                            if (toppos > i) { toppos = i; }
                        }
                        //blue (rare)
                        if ((250 < imagePointer1[0] && imagePointer1[0] < 256) && (100 < imagePointer1[1] && imagePointer1[1] < 110) && (100 < imagePointer1[2] && imagePointer1[2] < 110))
                        {

                            //get only first iteration of pixel
                            if (toppos > i) { toppos = i; }
                        }

                        // bottom pixel in coin on the item panel
                        if ((19 < imagePointer1[0] && imagePointer1[0] < 24) && (144 < imagePointer1[1] && imagePointer1[1] < 159) && (218 < imagePointer1[2] && imagePointer1[2] < 236))
                        {
                            
                            /*
                            imagePointer2[0] = 0;
                            imagePointer2[1] = 0;
                            imagePointer2[2] = 255;
                            imagePointer2[3] = imagePointer1[3];
                            richTextBox3.AppendText(j.ToString() + ";" + i.ToString() + "\n");
                            */


                            if ((bottomposfound == false) && (i > toppos + 200)) { bottompos = i; bottomposfound = true; }
                            
                        }
                        
                        /* else
                        {
                            imagePointer2[0] = (byte)a;
                            imagePointer2[1] = (byte)a;
                            imagePointer2[2] = (byte)a;
                            imagePointer2[3] = imagePointer1[3];
                        }
                        */

                        imagePointer2[0] = imagePointer1[0];
                        imagePointer2[1] = imagePointer1[1];
                        imagePointer2[2] = imagePointer1[2];
                        imagePointer2[3] = imagePointer1[3];

                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }//end for j

                    //if (pixcount > 452) { richTextBox3.AppendText(pixcount +";"+i.ToString() + "\n"); }
                    
                
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);

                    

                }
                
                //end for i

                
               

                double tpp = image.Height *13 / 1200;
                double btt = image.Height * 13 / 1200;

                if (toppos < 10000) { toppos = toppos - (Convert.ToInt32(Math.Floor(tpp))); }
                else { toppos = 0; }
                if (bottompos > 0) { bottompos = bottompos + (Convert.ToInt32(Math.Floor(btt))); }
                else { bottompos = image.Height; }
                //richTextBox3.AppendText(toppos+ ";" + bottompos + "\n");
                
                Rectangle crop2 = new Rectangle(0, toppos, returnMap.Width, Math.Abs(bottompos - toppos));
                Bitmap returnMap2 = cropImage(returnMap, crop2);
                

                image.UnlockBits(bitmapData1);
                returnMap.UnlockBits(bitmapData2);
                
               

                return returnMap2;
                

            }
            
            
            
            //returnMap.Dispose();

            
            
           


        }

        public Bitmap FindWidth(Bitmap imagez)
        {
            Bitmap returnMapz = new Bitmap(imagez.Width, imagez.Height, PixelFormat.Format32bppArgb);


            BitmapData bitmapData3 = imagez.LockBits(new Rectangle(0, 0,
                                     imagez.Width, imagez.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            BitmapData bitmapData4 = returnMapz.LockBits(new Rectangle(0, 0,
                                     returnMapz.Width, returnMapz.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            int a = 0;
            unsafe
            {

                byte* imagePointer1 = (byte*)bitmapData3.Scan0;
                byte* imagePointer2 = (byte*)bitmapData4.Scan0;
                

                // y position of bottom coin
                Int32 leftpos = 100000;

                // y position of top yellow text
               
                for (int i = 0; i < bitmapData3.Height; i++)
                {


                    for (int j = 0; j < bitmapData3.Width; j++)
                    {
                        // write the logic implementation here
                        a = (imagePointer1[0] + imagePointer1[1] + imagePointer1[2]) / 3;


                        //imagePointer2[0] = (byte)a;
                        //imagePointer2[1] = (byte)a;
                        //imagePointer2[2] = (byte)a;

                        //left pixel : almost black then white
                        if ((imagePointer1[0] < 140) && (imagePointer1[1] < 50) && (imagePointer1[2] < 50) &&
                            (254 < imagePointer1[4] && imagePointer1[4] < 256) && (103 < imagePointer1[5] && imagePointer1[5] < 107) && (103 < imagePointer1[6] && imagePointer1[6] < 107))
                        {

                            /*
                            imagePointer2[0] = 0;
                            imagePointer2[1] = 0;
                            imagePointer2[2] = 255;
                            imagePointer2[3] = imagePointer1[3];
                            richTextBox3.AppendText(j.ToString() + ";" + i.ToString() + "\n");
                         */



                            //get only first iteration of yellow pixel
                            if (leftpos > j) { leftpos = j; }
                            

                        }
                        

                        imagePointer2[0] = imagePointer1[0];
                        imagePointer2[1] = imagePointer1[1];
                        imagePointer2[2] = imagePointer1[2];
                        imagePointer2[3] = imagePointer1[3];

                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }//end for j

                    //if (pixcount > 452) { richTextBox3.AppendText(pixcount +";"+i.ToString() + "\n"); }


                    //4 bytes per pixel
                    imagePointer1 += bitmapData3.Stride - (bitmapData3.Width * 4);
                    imagePointer2 += bitmapData4.Stride - (bitmapData3.Width * 4);



                }

                //end for i

                double lpp = DataContainer.imgwi * 29 / 1920;
                double rpp = DataContainer.imgwi * 446 / 1920;

                if (DataContainer.screensize == 1) //4/3
                {
                    lpp = DataContainer.imgwi * 29 / 1600;
                    rpp = DataContainer.imgwi * 446 / 1600;
                }
                if (DataContainer.screensize == 3)  //16/9
                {
                    lpp = DataContainer.imgwi * 28 / 1920;
                    rpp = DataContainer.imgwi * 401 / 1920;
                }

                if (leftpos < 10000) { leftpos = leftpos - (Convert.ToInt32(Math.Floor(lpp))); }
                else { leftpos = 0; }
                if (leftpos < 0) { leftpos = 0; }
                Int32 rightpos = leftpos + Convert.ToInt32(Math.Floor(rpp));
                if (rightpos > returnMapz.Width) { rightpos = returnMapz.Width; }


                //richTextBox3.AppendText("left" + leftpos + ";" +rightpos+  "\n");
                Rectangle crop2 = new Rectangle(leftpos, 0, Math.Abs(rightpos - leftpos),returnMapz.Height);
                //Bitmap returnMap2 = cropImage(returnMapz, crop2);
                returnMapz = returnMapz.Clone(crop2, returnMapz.PixelFormat);

                
                imagez.UnlockBits(bitmapData3);
                //returnMapz.UnlockBits(bitmapData4);
                //richTextBox3.AppendText("\nok findwidth");
                return returnMapz;

            }



        

            




        }
        private Bitmap cropImage(Bitmap imga, Rectangle cropArea)
        {

            Bitmap bmpCrop = DataContainer.img1;
            //if ((cropArea.Width > 0) && (cropArea.Height > 0) ) { 
            bmpCrop = imga.Clone(cropArea, imga.PixelFormat); //}
            //
            //imga.Dispose();
            return (bmpCrop);
            
            
            
            
        }   
        
                                           
        
        private void RefreshList()
        {

            DirectoryInfo dinfo = new DirectoryInfo(DataContainer.imgdir);
            FileInfo[] Files = dinfo.GetFiles("*.jpg");
            listBox1.Items.Clear();
            foreach (FileInfo file in Files)
            {
                
                listBox1.Items.Add(file.Name);
                lastfilename = file.Name;
                
            }

        }

        private void RefreshListFirst()
        {

            DirectoryInfo dinfo = new DirectoryInfo(DataContainer.imgdir);
            FileInfo[] Files = dinfo.GetFiles("*.jpg");
            listBox1.Items.Clear();
            foreach (FileInfo file in Files)
            {

                listBox1.Items.Add(file.Name);
                lastfilename = file.Name;
                picturenumber = picturenumber + 1;
            }

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           this.folderBrowserDialog1.RootFolder=System.Environment.SpecialFolder.MyComputer;
           this.folderBrowserDialog1.ShowNewFolderButton=true;
            DialogResult result=this.folderBrowserDialog1.ShowDialog();
             if (result==DialogResult.OK)
             {
                 textBox1.Clear();

               // retrieve the name of the selected folder
                 string foldername=this.folderBrowserDialog1.SelectedPath;
                 
               // print the folder name on a label
                textBox1.Text=foldername;
                DataContainer.imgdir = foldername;

                File.Delete(settingsfile);
                new System.Xml.Linq.XDocument(new System.Xml.Linq.XElement("root", new System.Xml.Linq.XElement("Imagedir", DataContainer.imgdir))).Save(settingsfile);
                RefreshListFirst();
                pictureBox2.Image = null;
               
 
                               
              }
 

        }

        private void button2_Click(object sender, EventArgs e)
        {

            button3.Enabled = false;
            button4.Enabled = false;
            string directoryPath = DataContainer.imgdir;
            if (MessageBox.Show("Really delete from " + DataContainer.imgdir, "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Directory.GetFiles(directoryPath).ToList().ForEach(File.Delete);
                picturenumber = 0;
                RefreshList();
                //richTextBox1.AppendText("All files deleted");
                //richTextBox1.ScrollToCaret();
                pictureBox2.Image = null;
            }

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

    


        private void itemType()
        {
            string boxstr = comboBox1.SelectedItem.ToString();
            //richTextBox3.AppendText(boxstr);
            ItemTypeFinal = boxstr;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string listbfile = "";
            string IMGUR_ANONYMOUS_API_KEY = "f591d329875aa5dbe125d3fd42207d44";
                     
            
            listbfile = listBox1.SelectedItem.ToString();

            

            button4.Enabled = false;
            if (comboBox2.SelectedIndex == 0) 
            {
                PostToImgur(DataContainer.imgdir + "\\" + listbfile, IMGUR_ANONYMOUS_API_KEY);
            }

            if (comboBox2.SelectedIndex == 1)
            {
                linkLabel1.Text = Imageshack.ImageshackAPI.Upload(DataContainer.imgdir + "\\" + listbfile);
                //richTextBox3.AppendText(DataContainer.imgdir + "\\" + listbfile);
            }
           

            button4.Enabled = true;
            
        }

        public void PostToImgur(string imagFilePath, string apiKey)
        {
            byte[] imageData;
           
            FileStream fileStream = File.OpenRead(imagFilePath);
            imageData = new byte[fileStream.Length];
            fileStream.Read(imageData, 0, imageData.Length);
            fileStream.Close();

			const int MAX_URI_LENGTH = 32766;
			string base64img = System.Convert.ToBase64String(imageData);
			StringBuilder sb = new StringBuilder();

			for(int i = 0; i < base64img.Length; i += MAX_URI_LENGTH) {
				sb.Append(Uri.EscapeDataString(base64img.Substring(i, Math.Min(MAX_URI_LENGTH, base64img.Length - i))));
			}
            string imgurApiKey = "f591d329875aa5dbe125d3fd42207d44";
			string uploadRequestString = "image=" + sb.ToString() + "&key=" + imgurApiKey;
            
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://api.imgur.com/2/upload");
            webRequest.Timeout = 10000;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ServicePoint.Expect100Continue = false;

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());
            streamWriter.Write(uploadRequestString);
            streamWriter.Close();

            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream);

            string responseString = responseReader.ReadToEnd();

            StringBuilder output = new StringBuilder();

            String xmlString = responseString;
                    
            // Create an XmlReader
            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("original");
                //richTextBox2.AppendText("Content of the title element: " + reader.ReadElementContentAsString());
                
                linkLabel1.Text = reader.ReadElementContentAsString();
                
            }




            
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }


        private void DisplayItemValue(string inputvalue)
        {
            string tmpdataa = GetItemData(ItemTypeFinal, inputvalue); //value of maximum stat for item

            if (checkBox2.Checked == true)
            {
                

                richTextBox3.SelectionColor = System.Drawing.Color.FromName("Blue");
                richTextBox3.AppendText(inputvalue + " : ");
                richTextBox3.SelectionColor = System.Drawing.Color.FromName("Black");
                richTextBox3.AppendText(tmpdataa + "\n");
                
            }
            else {
          
               if (tmpdataa != "0") {

                richTextBox3.SelectionColor = System.Drawing.Color.FromName("Blue");
                richTextBox3.AppendText(inputvalue + " : ");
                richTextBox3.SelectionColor = System.Drawing.Color.FromName("Black");
                richTextBox3.AppendText(tmpdataa + "\n");
                 }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clickcombo();
        }

        private void clickcombo()
        {

            //string searchvalue = "0", tmpstat = "0", tmpdataa = "0";

            richTextBox3.Clear();
            string boxstr = comboBox1.SelectedItem.ToString();
            boxstr = boxstr.Replace("- ", "");
            ItemTypeFinal = boxstr;



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




        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            clickcombo();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            // Starting point of the selection:
            if (e.Button == MouseButtons.Left)
            {
                _selecting = true;
                _selection = new Rectangle(new Point(e.X, e.Y), new Size());
                squarestart.X = e.X;
                squarestart.Y = e.Y;

                //attempt to block mouse in picturebox while selecting
                //DataContainer.Prevclip = Cursor.Clip;
                //Cursor.Clip = RectangleToScreen(new Rectangle(new Point(panel1.Location.X +10 , panel1.Location.Y + 10), pictureBox2.Size));
            }

            if (e.Button == MouseButtons.Right)
            {
                _moving = true;
                pictureBox2.Cursor = Cursors.SizeAll;
                _pointmoveA = new Point(e.X, e.Y);
                //richTextBox3.AppendText(_pointmove.ToString());
            }
           
    
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            bool okcrop = true;
            //Cursor.Clip = DataContainer.Prevclip;
            if (e.Button == MouseButtons.Left && _selecting && (pictureBox2.Image != null))
            {
                // Create cropped image:
                //Image img = pictureBox2.Image.Crop(_selection);
                if ((_selection.X + _selection.Width) > (pictureBox2.Image.Width))
                {
                    okcrop = false;
                }
                if ((_selection.Y + _selection.Height) > (pictureBox2.Image.Height))
                {
                    okcrop = false;
                }
                if (_selection.Y < 0) { okcrop = false; }
                if (_selection.X < 0) { okcrop = false; }

                if ((_selection.Width > 0) && (_selection.Height > 0) && (pictureBox2.Image != null) && (okcrop == true))
                {
                    Bitmap tmpimg = new Bitmap(pictureBox2.Image);
                    Bitmap tmpimgcropped = DataContainer.img1;
                    tmpimgcropped = cropImage(tmpimg, _selection); 
                    pictureBox2.Image = tmpimgcropped;
                }


                // Fit image to the picturebox:
                //pictureBox2.Image = img.Fit2PictureBox(pictureBox2);

                _selecting = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                pictureBox2.Cursor = Cursors.Cross;
                _pointmoveB = new Point(e.X, e.Y);
                _moving = false;
                
                //richTextBox3.AppendText(_pointmoveB.ToString());
            }


        }


        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            
            // Update the actual size of the selection:
            if ((_selecting) && (pictureBox2.Image != null))
            {

                
                //_selection.Width = Math.Abs(e.X - _selection.X);
                //_selection.Height = Math.Abs(e.Y - _selection.Y);
                _selection.Width = Math.Abs(e.X - squarestart.X);
                _selection.Height = Math.Abs(e.Y - squarestart.Y);


                //change the uppperleft corner of the rectangle following the selecting direction
                if (e.X > _selection.X)
                {
                    if (e.Y > _selection.Y) {}
                    else
                    {
                        _selection.Y = e.Y;

                    }
                }
                else
                {
                    if (e.Y > _selection.Y)
                    {
                        _selection.X = e.X;
                    }
                    else
                    {
                        _selection.X = e.X;
                        _selection.Y = e.Y;
                    }

                }

                if ((_selection.X + _selection.Width) > (pictureBox2.Image.Width)) { //_selecting = false; 
                }
                if ((_selection.Y + _selection.Height) > (pictureBox2.Image.Height)) { //_selecting = false; 
                }


                // Redraw the picturebox:
                pictureBox2.Refresh();
            }

            if (_moving)
            {
                
                _pointmoveB.X = _pointmoveB.X + _pointmoveA.X - e.X;
                _pointmoveB.Y = _pointmoveB.Y + _pointmoveA.Y - e.Y;
                panel1.AutoScrollPosition = _pointmoveB;
                //pictureBox2.AutoScrollOffset = _pointmoveB;
                pictureBox2.Refresh();
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (_selecting)
            {
                // Draw a rectangle displaying the current selection

                
                Pen pen = Pens.GreenYellow;
                e.Graphics.DrawRectangle(pen, _selection);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
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
            //richTextBox1.AppendText("Picture saved as : " + imagesave + "\n");
            //richTextBox1.ScrollToCaret();

            pictureBox2.Image.Save(imagesave, ImageFormat.Jpeg);

            //string imagesave = imgname + zeronumb + picturenumber.ToString()+".bmp"; 
            //richTextBox1.AppendText("Picture saved as : "+ imagesave + "\n");
            //pictureBox2.Image.Save(imagesave,ImageFormat.Bmp);


            RefreshList();
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            picturenumber = picturenumber + 1;

            pictureBox2.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           pictureBox2.Image = DataContainer.img1; 
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            Helpbox frm = new Helpbox();
            frm.Show();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            //gkh.unhook();
            if (e.KeyCode == Keys.Delete) {

                //richTextBox3.AppendText(listBox1.SelectedIndex.ToString());
                string listcfile = listBox1.SelectedItem.ToString();
                File.Delete(DataContainer.imgdir + "\\" + listcfile);
                RefreshList();
                if (listBox1.Items.Count == 0) { button4.Enabled = false; }
                 }

            //gkh.hook();  
           
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            gkh.unhook(); gkh.hook();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Wizard wiz = new Wizard();
            wiz.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            var keystr = comboBox3.SelectedIndex;
            gkh.unhook();
            gkh.HookedKeys.Clear();

            
            switch (keystr)
            {
                case 0:
                    gkh.HookedKeys.Add(Keys.F12);
                    break;
                case 1:
                    gkh.HookedKeys.Add(Keys.F1);
                    break;
                case 2:
                    gkh.HookedKeys.Add(Keys.F2);
                    break;
                case 3:
                    gkh.HookedKeys.Add(Keys.F3);
                    break;
                case 4:
                    gkh.HookedKeys.Add(Keys.F4);
                    break;
                case 5:
                    gkh.HookedKeys.Add(Keys.F5);
                    break;
                case 6:
                    gkh.HookedKeys.Add(Keys.F6);
                    break;
                case 7:
                    gkh.HookedKeys.Add(Keys.F7);
                    break;
                case 8:
                    gkh.HookedKeys.Add(Keys.F8);
                    break;
                default:
                    gkh.HookedKeys.Add(Keys.F12);
                    break;
            }


            

            
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.hook();
            
            
        }

 


       
      

 



       
        

        
    }
    public static class DataContainer
    {
        public static string imgdir;
        public static string imageshackurl;
        public static string imgpathlol;
        public static Bitmap img1, imgcropped;
        public static int imgwi;
        public static int screensize = 1;
        public static Rectangle Prevclip;
    }

    

}

