
/*
 
 
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
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DiabloApp;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Imageshack
{
  public class ImageshackAPI
  {
    public static string Upload(string fileName)
    {

        

      //try
      //{
        string contentType = "image/jpeg"; ;
        CookieContainer cookie = new CookieContainer();
        NameValueCollection par = new NameValueCollection();
        par["MAX_FILE_SIZE"] = "3145728";
        par["refer"] = "";
        par["brand"] = "";
        par["key"] = "9DFGLPVY4638e6d7c21fb136ad031b6cb29dfc32";
        par["optimage"] = "1";
        par["rembar"] = "1";
        par["submit"] = "host it!";
        List<string> l = new List<string>();
        string resp;
        par["optsize"] = "resample";
        resp = UploadFileEx(fileName, "http://www.imageshack.us/upload_api.php", "fileupload", contentType, par, cookie);
        return resp;
      //}
      // catch (Exception ex)
     // {

     //   MessageBox.Show(ex.Message);
     //   return ex.Message;
        
      
    }

    public static string UploadFileEx(string uploadfile, string url, string fileFormName, string contenttype, NameValueCollection querystring, CookieContainer cookies)
    {
      if ((fileFormName == null) ||
        (fileFormName.Length == 0))
      {
        fileFormName = "fileupload";
      }

      if ((contenttype == null) ||
        (contenttype.Length == 0))
      {
        contenttype = "application/octet-stream";
      }


      string postdata;
      postdata = "?";
      if (querystring != null)
      {
        foreach (string key in querystring.Keys)
        {
          postdata += key + "=" + querystring.Get(key) + "&";
        }
      }
      Uri uri = new Uri(url + postdata);


      string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
      HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
      webrequest.CookieContainer = cookies;
      webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
      webrequest.Method = "POST";


      // Build up the post message header
      StringBuilder sb = new StringBuilder();
      sb.Append("--");
      sb.Append(boundary);
      sb.Append("\r\n");
      sb.Append("Content-Disposition: form-data; name=\"");
      sb.Append(fileFormName);
      sb.Append("\"; filename=\"");
      sb.Append(Path.GetFileName(uploadfile));
      sb.Append("\"");
      sb.Append("\r\n");
      sb.Append("Content-Type: ");
      sb.Append(contenttype);
      sb.Append("\r\n");
      sb.Append("\r\n");

      string postHeader = sb.ToString();
      byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

      // Build the trailing boundary string as a byte array
      // ensuring the boundary appears on a line by itself
      byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

      FileStream fileStream = new FileStream(uploadfile, FileMode.Open, FileAccess.Read);
      long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
      webrequest.ContentLength = length;

      Stream requestStream = webrequest.GetRequestStream();

      // Write out our post header
      requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

      // Write out the file contents
      byte[] buffer = new Byte[checked((UInt32)Math.Min(4096, (Int32)fileStream.Length))];
      Int32 bytesRead = 0;
      while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        requestStream.Write(buffer, 0, bytesRead);

        
      // Write out the trailing boundary
      requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
      WebResponse responce = webrequest.GetResponse();
      Stream s = responce.GetResponseStream();
      XmlSerializer xSerializer = new XmlSerializer(typeof(imginfo));
      imginfo ii = (imginfo)xSerializer.Deserialize(s);

      imginfoLinks ifl = ii.links;
      fileStream.Close();
      s.Close();
      return ifl.image_link;
      

    }
  }
}




  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ns.imageshack.us/imginfo/7/", IsNullable = false)]

  public partial class imginfo
  {

    private imginfoRating ratingField;

    private imginfoFiles filesField;

    private imginfoResolution resolutionField;

    private string classField;

    private string visibilityField;

    private imginfoUploader uploaderField;

    private imginfoLinks linksField;

    private byte versionField;

    private UInt32 timestampField;

    /// <remarks/>
    public imginfoRating rating
    {
      get
      {
        return this.ratingField;
      }
      set
      {
        this.ratingField = value;
      }
    }

    /// <remarks/>
    public imginfoFiles files
    {
      get
      {
        return this.filesField;
      }
      set
      {
        this.filesField = value;
      }
    }

    /// <remarks/>
    public imginfoResolution resolution
    {
      get
      {
        return this.resolutionField;
      }
      set
      {
        this.resolutionField = value;
      }
    }

    /// <remarks/>
    public string @class
    {
      get
      {
        return this.classField;
      }
      set
      {
        this.classField = value;
      }
    }

    /// <remarks/>
    public string visibility
    {
      get
      {
        return this.visibilityField;
      }
      set
      {
        this.visibilityField = value;
      }
    }

    /// <remarks/>
    public imginfoUploader uploader
    {
      get
      {
        return this.uploaderField;
      }
      set
      {
        this.uploaderField = value;
      }
    }

    /// <remarks/>
    public imginfoLinks links
    {
      get
      {
        return this.linksField;
      }
      set
      {
        this.linksField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte version
    {
      get
      {
        return this.versionField;
      }
      set
      {
        this.versionField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public UInt32 timestamp
    {
      get
      {
        return this.timestampField;
      }
      set
      {
        this.timestampField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoRating
  {

    private byte ratingsField;

    private decimal avgField;

    /// <remarks/>
    public byte ratings
    {
      get
      {
        return this.ratingsField;
      }
      set
      {
        this.ratingsField = value;
      }
    }

    /// <remarks/>
    public decimal avg
    {
      get
      {
        return this.avgField;
      }
      set
      {
        this.avgField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoFiles
  {

    private imginfoFilesImage imageField;

    private imginfoFilesThumb thumbField;

    private ushort serverField;

    private ushort bucketField;

    /// <remarks/>
    public imginfoFilesImage image
    {
      get
      {
        return this.imageField;
      }
      set
      {
        this.imageField = value;
      }
    }

    /// <remarks/>
    public imginfoFilesThumb thumb
    {
      get
      {
        return this.thumbField;
      }
      set
      {
        this.thumbField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort server
    {
      get
      {
        return this.serverField;
      }
      set
      {
        this.serverField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort bucket
    {
      get
      {
        return this.bucketField;
      }
      set
      {
        this.bucketField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoFilesImage
  {

    private UInt32 sizeField;

    private string contenttypeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public UInt32 size
    {
      get
      {
        return this.sizeField;
      }
      set
      {
        this.sizeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("content-type")]
    public string contenttype
    {
      get
      {
        return this.contenttypeField;
      }
      set
      {
        this.contenttypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
      get
      {
        return this.valueField;
      }
      set
      {
        this.valueField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoFilesThumb
  {

    private ushort sizeField;

    private string contenttypeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort size
    {
      get
      {
        return this.sizeField;
      }
      set
      {
        this.sizeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("content-type")]
    public string contenttype
    {
      get
      {
        return this.contenttypeField;
      }
      set
      {
        this.contenttypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
      get
      {
        return this.valueField;
      }
      set
      {
        this.valueField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoResolution
  {

    private ushort widthField;

    private ushort heightField;

    /// <remarks/>
    public ushort width
    {
      get
      {
        return this.widthField;
      }
      set
      {
        this.widthField = value;
      }
    }

    /// <remarks/>
    public ushort height
    {
      get
      {
        return this.heightField;
      }
      set
      {
        this.heightField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoUploader
  {

    private string ipField;

    /// <remarks/>
    public string ip
    {
      get
      {
        return this.ipField;
      }
      set
      {
        this.ipField = value;
      }
    }
  }

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ns.imageshack.us/imginfo/7/")]
  public partial class imginfoLinks
  {

    private string image_linkField;

    private string image_htmlField;

    private string image_bbField;

    private string image_bb2Field;

    private string thumb_linkField;

    private string thumb_htmlField;

    private string thumb_bbField;

    private string thumb_bb2Field;

    private string yfrog_linkField;

    private string yfrog_thumbField;

    private string ad_linkField;

    private string done_pageField;

    /// <remarks/>
    public string image_link
    {
      get
      {
        return this.image_linkField;
      }
      set
      {
        this.image_linkField = value;
      }
    }

    /// <remarks/>
    public string image_html
    {
      get
      {
        return this.image_htmlField;
      }
      set
      {
        this.image_htmlField = value;
      }
    }

    /// <remarks/>
    public string image_bb
    {
      get
      {
        return this.image_bbField;
      }
      set
      {
        this.image_bbField = value;
      }
    }

    /// <remarks/>
    public string image_bb2
    {
      get
      {
        return this.image_bb2Field;
      }
      set
      {
        this.image_bb2Field = value;
      }
    }

    /// <remarks/>
    public string thumb_link
    {
      get
      {
        return this.thumb_linkField;
      }
      set
      {
        this.thumb_linkField = value;
      }
    }

    /// <remarks/>
    public string thumb_html
    {
      get
      {
        return this.thumb_htmlField;
      }
      set
      {
        this.thumb_htmlField = value;
      }
    }

    /// <remarks/>
    public string thumb_bb
    {
      get
      {
        return this.thumb_bbField;
      }
      set
      {
        this.thumb_bbField = value;
      }
    }

    /// <remarks/>
    public string thumb_bb2
    {
      get
      {
        return this.thumb_bb2Field;
      }
      set
      {
        this.thumb_bb2Field = value;
      }
    }

    /// <remarks/>
    public string yfrog_link
    {
      get
      {
        return this.yfrog_linkField;
      }
      set
      {
        this.yfrog_linkField = value;
      }
    }

    /// <remarks/>
    public string yfrog_thumb
    {
      get
      {
        return this.yfrog_thumbField;
      }
      set
      {
        this.yfrog_thumbField = value;
      }
    }

    /// <remarks/>
    public string ad_link
    {
      get
      {
        return this.ad_linkField;
      }
      set
      {
        this.ad_linkField = value;
      }
    }

    /// <remarks/>
    public string done_page
    {
      get
      {
        return this.done_pageField;
      }
      set
      {
        this.done_pageField = value;
      }
    }
  }

