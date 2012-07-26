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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diablo
{
    public partial class Helpbox : Form
    {
        public Helpbox()
        {
            InitializeComponent();
            richTextBox1.AppendText("Use F12 while in game to capture items (hovering the item with the mouse cursor)\n\nUse delete key to delete from image list\n\nSet your video options to WINDOWED FULLSCREEN or WINDOWED !\n\n");
            richTextBox1.AppendText("Autocropped items are automatically saved. If you're cropping manually you need to save the item with the save picture button\nReset image button resets image to the full screenshot in case autocrop didn't work\n");
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:"+linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.Text);
        }



 
    }
}
