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
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Utilities;
using System.Xml;
using WindowsFormsApplication1;
using Imageshack;

namespace Diablo
{
    public partial class Wizard : Form
    {

        private bool _selecting, _moving;
        private Point _pointmoveA, _pointmoveB;
        private Point _pointmoveBtmp;
        
        Bitmap combinedbitmapfinal;

        public Wizard()
        {
            InitializeComponent();

            refreshlist();
            comboBox1.SelectedIndex = 0;
            domainUpDown1.SelectedIndex = 0;
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
            }
        }

        private void refreshlist()
        {
            DirectoryInfo dinfo = new DirectoryInfo(DataContainer.imgdir);
            FileInfo[] Files = dinfo.GetFiles("*.jpg");
            listBox1.Items.Clear();
            foreach (FileInfo file in Files) { listBox1.Items.Add(file.Name);  }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            pictureBox2.Image = null;
            button8.Enabled = true;
            button7.Enabled = true;
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                
                listBox2.Items.Remove(listBox2.SelectedItem);
                
                
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {

                listBox2.Items.Add(listBox1.SelectedItem);


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
            {
                object selected = listBox2.SelectedItem;
                int indx = listBox2.Items.IndexOf(selected);
                int totl = listBox2.Items.Count;

                if (indx == 0)
                {
                    listBox2.Items.Remove(selected);
                    listBox2.Items.Insert(totl - 1, selected);
                    listBox2.SetSelected(totl - 1, true);
                }
                else
                {
                    listBox2.Items.Remove(selected);
                    listBox2.Items.Insert(indx - 1, selected);
                    listBox2.SetSelected(indx - 1, true);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
            {
                object selected = listBox2.SelectedItem;
                int indx = listBox2.Items.IndexOf(selected);
                int totl = listBox2.Items.Count;

                if (indx == totl - 1)
                {
                    listBox2.Items.Remove(selected);
                    listBox2.Items.Insert(0, selected);
                    listBox2.SetSelected(0, true);
                }
                else
                {
                    listBox2.Items.Remove(selected);
                    listBox2.Items.Insert(indx + 1, selected);
                    listBox2.SetSelected(indx + 1, true);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string listbfile = listBox1.SelectedItem.ToString();
                pictureBox1.ImageLocation = DataContainer.imgdir + "\\" + listbfile;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _moving = true;
                pictureBox1.Cursor = Cursors.SizeAll;
                _pointmoveA = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (_moving)
            {
                _pointmoveB.X = _pointmoveB.X + _pointmoveA.X - e.X;
                _pointmoveB.Y = _pointmoveB.Y + _pointmoveA.Y - e.Y;
                //richTextBox1.Clear();
                //richTextBox1.AppendText("A : " + _pointmoveA + ";\nB : " + _pointmoveB +";\ne :" + e.X +","+ e.Y + "\n");
               
                panel1.AutoScrollPosition = _pointmoveB;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pictureBox1.Cursor = Cursors.Default;
                _pointmoveB = new Point(e.X, e.Y);
                _moving = false;
            }

        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string listbfile = listBox2.SelectedItem.ToString();
                pictureBox2.ImageLocation = DataContainer.imgdir + "\\" + listbfile;
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _moving = true;
                pictureBox2.Cursor = Cursors.SizeAll;
                _pointmoveA = new Point(e.X, e.Y);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moving)
            {
                _pointmoveB.X = _pointmoveB.X + _pointmoveA.X - e.X;
                _pointmoveB.Y = _pointmoveB.Y + _pointmoveA.Y - e.Y;
                panel2.AutoScrollPosition = _pointmoveB;
                pictureBox2.Refresh();
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pictureBox2.Cursor = Cursors.Default;
                _pointmoveB = new Point(e.X, e.Y);
                _moving = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                ImageCombine();
                button8.Enabled = true;
                button7.Enabled = true;
                button10.Enabled = true;






            }

        }

        private void ImageCombine()
        {
            
            int pic_count = listBox2.Items.Count;
            string boxvalue = domainUpDown1.SelectedItem.ToString();
            int boxvalueint = 3;
            if (boxvalue == "Auto") {
                if (pic_count < 10)  {boxvalueint = 3;}
                else if (pic_count < 15) { boxvalueint = 4; }
                else { boxvalueint = 5; }
                }
            else { boxvalueint = Convert.ToInt32(boxvalue);}

            //richTextBox1.AppendText(boxvalueint.ToString());

           
            
            int rowcount = 1;
            double rowcounttmp = 0; double rowcounttmp2 = 0;

            int FinalPicWidth = 0;
            
            Int32 TotalHeight = 1;
            Int32 TallestHeight = 1;
            Bitmap[] images = new Bitmap[pic_count];
            
            
            rowcount = 1;

            rowcounttmp2 = (Convert.ToDouble(pic_count)) / boxvalueint;
            rowcounttmp = Math.Ceiling(rowcounttmp2);
            rowcount = Convert.ToInt32(rowcounttmp);
            //if (rowcounttmp != rowcounttmp2) { rowcount = Convert.ToInt32(rowcounttmp2) + 1; }
            //else { rowcount = Convert.ToInt32(rowcounttmp); }
            //richTextBox1.AppendText(pic_count.ToString()+ ","+boxvalueint+"\n");
            
            //richTextBox1.AppendText(rowcount.ToString() + "," +rowcounttmp.ToString() + "," +rowcounttmp2.ToString()+ "\n");

            Bitmap[] imagearr = new Bitmap[rowcount];
            
            
            int j = 0; int k = 0;
            int fromtop = 0;
            int imgh= 0;

            for (int r = 0; r < rowcount; r++){

                int fromleft = 0;
                Int32 TotalWidth = 0;
                TallestHeight = 0;
                for (int z = k; z < k + boxvalueint; z++)
                {
                    
                    if (z < pic_count)
                    {
                        
                        images[z] = new Bitmap(DataContainer.imgdir + "\\" + listBox2.Items[z].ToString());
                        imgh = images[z].Height;
                        TotalWidth = TotalWidth + images[z].Width;
                        if (TallestHeight < images[z].Height) { TallestHeight = images[z].Height; }
                    }

                //richTextBox1.AppendText(listBox2.Items[z].ToString());
                 }

                Bitmap combinedbitmap = new Bitmap(TotalWidth, TallestHeight);
                using (Graphics combinedgraphics = Graphics.FromImage(combinedbitmap))
                {

                    for (int w = k; w < k + boxvalueint; w++)
                    {
                        if (w < pic_count)
                        {

                            combinedgraphics.DrawImage(images[w], new Point(fromleft, 0));
                            fromleft = fromleft + images[w].Width;
                            //richTextBox1.AppendText(images[w].Width.ToString());
                        }
                    }
                }
                //Bitmap myBitmap = new Bitmap(TotalWidth, TallestHeight, combinedgraphics);
                
                imagearr[j] = combinedbitmap;
                
                
                

                j = j + 1;

            k = k + boxvalueint;
            
            }

            //imagearr[0].Save(DataContainer.imgdir + "\\test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //imagearr[1].Save(DataContainer.imgdir + "\\test2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //imagearr[2].Save(DataContainer.imgdir + "\\test3.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            
            
            int arrwidth = 0; int arrheight = 0;

            for (int z = 0; z < rowcount ; z++)
            {


                arrheight = arrheight + imagearr[z].Height;

                if (arrwidth < imagearr[z].Width) { arrwidth = imagearr[z].Width; }


                //richTextBox1.AppendText(listBox2.Items[z].ToString());
            }

            combinedbitmapfinal = new Bitmap(arrwidth, arrheight);
            using (Graphics combinedgraphicsfinal = Graphics.FromImage(combinedbitmapfinal))
            {

                for (int w = 0; w < rowcount ; w++)
                {


                    combinedgraphicsfinal.DrawImage(imagearr[w], new Point(0, fromtop));
                    fromtop = fromtop + imagearr[w].Height;
                    //richTextBox1.AppendText(images[w].Width.ToString());
                }
            }

            DataContainer.imgpathlol = DataContainer.imgdir + "\\bigpic\\AcualyIsDolan.jpg";
            combinedbitmapfinal.Save(DataContainer.imgpathlol, System.Drawing.Imaging.ImageFormat.Jpeg);


            //images[].Dispose();
            //combinedbitmap.Dispose();
            combinedbitmapfinal.Dispose();

            
            images[0].Dispose();
            imagearr[0].Dispose();

        
         }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
           
            string IMGUR_ANONYMOUS_API_KEY = "f591d329875aa5dbe125d3fd42207d44";
            
           
           

            if (comboBox1.SelectedIndex == 0)
            {
                PostToImgur(DataContainer.imgpathlol, IMGUR_ANONYMOUS_API_KEY);
            }

            if (comboBox1.SelectedIndex == 1)
            {
                string tmptext = Imageshack.ImageshackAPI.Upload(DataContainer.imgpathlol);
                linkLabel1.Text = tmptext;
                richTextBox1.AppendText(DataContainer.imgpathlol+ "\n" + tmptext);
                
            }


            button8.Enabled = true;


            



            
           


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

            for (int i = 0; i < base64img.Length; i += MAX_URI_LENGTH)
            {
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
            Process.Start(linkLabel1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.

                combinedbitmapfinal.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                     

                    
                

                fs.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                foreach (var item in listBox1.Items)
                {
                    listBox2.Items.Add(item);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (File.Exists(DataContainer.imgpathlol))
            {
                Process.Start(DataContainer.imgpathlol);
            }
        }
    }
}
