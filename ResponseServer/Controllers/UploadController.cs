using LogEngines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
//using System.Windows.Media.Imaging;
using Tesseract;
//using static System.Net.Mime.MediaTypeNames;
using LogEngines;
using System.Data.Entity.Validation;

namespace ResponseServer.Controllers
{
    public class UploadController : ApiController
    {
        [ResponseType(typeof(FileUpload))]
        public async Task<IHttpActionResult> PostFileUploadAsync(string name)
        {

            LogEngine.Write("Request Recieved", name);
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                LogEngine.Write("File Recieved");
                // Get the uploaded image from the Files collection  
                var httpPostedFile = HttpContext.Current.Request.Files[0];
                if (httpPostedFile != null)
                {
                    LogEngine.Write("File Not NULL");
                    FileUpload imgupload = new FileUpload();
                    int length = httpPostedFile.ContentLength;
                    //get imagedata  
                    LogEngine.Write("Stream Reading...");
                    httpPostedFile.InputStream.Read(imgupload.FileBytes, 0, imgupload.FileBytes.Length);
                    //imgupload.FileName = Path.GetFileName(httpPostedFile.FileName);
                    // db.FileUploads.Add(imgupload);
                    // db.SaveChanges();
                    // Make sure you provide Write permissions to destination folder
                    LogEngine.Write("Stream Readed");
                    string sPath = @"C:\Users\brati\source\repos\Server Business Cards\ResponseServer\FileUploads\Images\" + name;
                    if (!Directory.Exists(sPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        catch (Exception e)
                        {
                            LogEngine.Write("Directory Create " + e, name);
                        }

                    }
                    var fileSavePath = sPath + "\\" + Path.GetFileName(httpPostedFile.FileName);
                    LogEngine.Write("File SAVING to " + fileSavePath);
                    // Save the uploaded file to "UploadedFiles" folder  
                    httpPostedFile.SaveAs(fileSavePath);
                    //string toReadFile = @"C:\Users\brati\source\repos\Server Business Cards\ResponseServer\FileUploads\Images\brt94\toread2.jpg";
                    //Do next no await//increased timeout is not a solution
                    ReadImageOCR(fileSavePath, name);
                    LogEngine.Write("File SAVED to " + fileSavePath);
                    return Ok("Image Uploaded");
                }
            }
            return Ok("Image is not Uploaded");
        }

        private Task<string> ReadImageOCR(string path, string name)
        {
            LogEngine.Write("Reading File", name);
            Bitmap b = new Bitmap(path);
            using (var engine = new TesseractEngine(@"C:\Users\brati\source\repos\Server Business Cards\ResponseServer\tessdata", "ron", EngineMode.TesseractAndLstm, @"C:\Users\brati\source\repos\Server Business Cards\ResponseServer\config.ini"))
            {

                using (var page = engine.Process(b, PageSegMode.Auto))
                {
                    string text = page.GetText();
                    tagTransform(text, path, name, true);
                }
            }
            LogEngine.Write("Reading File Completed", name);
            return null;
        }

        private void tagTransform(string text, string filePATH, string user, bool saveToFile = false)
        {
            LogEngine.Write("Reading Tags", user);
            //string sb = "";
            //if (saveToFile)
            File.WriteAllText(filePATH + ".xml", text);
            var str_array = File.ReadAllLines(filePATH + ".xml");
            using (var db = new bcservEntities())
            {
                var card = new bizCard();
                var acc = (from x in db.users where x.username == user select x).FirstOrDefault();

                foreach (var str in str_array)
                {
                    //NUME PRENUME
                    if (str.ContainsName())
                    {
                        if (str.Contains(" "))
                        {
                            var numePrenume = str.Split(' ');
                            card.Nume = numePrenume[0];
                            if (numePrenume.Length > 1)
                                card.Prenume = numePrenume[1];
                        }
                        else
                        {
                            card.Nume = str;
                        }
                        continue;
                    }
                    if (str.ToLower().isPhoneNumber() != "error")
                    {
                        card.phone1 = str.ToLower().isPhoneNumber();
                        continue;
                    }
                    if (str.Contains("@") && str.Contains("."))
                    {
                        var s = str.Split(' ');
                        foreach (var s1 in s)
                        {
                            if (s1.Contains(" "))
                            {
                                card.email = s1;
                                continue;
                            }
                        }
                    }
                    if (str.Contains("www") || str.Contains("."))
                    {
                        card.website = str;
                        continue;
                    }

                }
               
                try
                {
                    acc.bizCards.Add(card);
                    LogEngine.Write("Saving BizCard...", user);
                     db.SaveChanges();
                    
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        LogEngine.Write(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State),user);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            LogEngine.Write(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage),user);
                        }
                    }
                    throw;
                }
            }
            LogEngine.Write("Reading Tags Completed", user);
        }
    }
}
