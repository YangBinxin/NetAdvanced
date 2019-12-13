using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobWorkForm.Tools
{
    public class FileHelper
    {
        /// <summary>
        /// WebClient上传文件至服务器，默认不自动改名
        /// </summary>
        /// <param name="fileNamePath">文件名，全路径格式</param>
        /// <param name="uriString">服务器文件夹路径</param>
        public static void UpLoadFile(string fileNamePath, string uriString)
        {
            UpLoadFile(fileNamePath, uriString, false);
        }
        /// <summary>
        /// WebClient上传文件至服务器
        /// </summary>
        /// <param name="fileNamePath">文件名，全路径格式</param>
        /// <param name="uriString">服务器文件夹路径</param>
        /// <param name="IsAutoRename">是否自动按照时间重命名</param>
        public static void UpLoadFile(string fileNamePath, string uriString, bool IsAutoRename)
        {
            string fileName = fileNamePath.Substring(fileNamePath.LastIndexOf("\\") + 1);
            string NewFileName = fileName;
            if (IsAutoRename)
            {
                NewFileName = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + fileNamePath.Substring(fileNamePath.LastIndexOf("."));
            }
            string fileNameExt = fileName.Substring(fileName.LastIndexOf(".") + 1);
            if (uriString.EndsWith("/") == false) uriString = uriString + "/";

            uriString = uriString + NewFileName;
            //log.AddLog(uriString, "Log");
            //log.AddLog(fileNamePath, "Log");
            /**/
            /// 创建WebClient实例
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;
            // 要上传的文件
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            //FileStream fs = OpenFile();
            BinaryReader r = new BinaryReader(fs);
            byte[] postArray = r.ReadBytes((int)fs.Length);
            Stream postStream = myWebClient.OpenWrite(uriString, "PUT");
            try
            {
                //使用UploadFile方法可以用下面的格式
                //myWebClient.UploadFile(uriString,"PUT",fileNamePath);

                if (postStream.CanWrite)
                {
                    postStream.Write(postArray, 0, postArray.Length);
                    postStream.Close();
                    fs.Dispose();
                }
                else
                {
                    postStream.Close();
                    fs.Dispose();
                }

            }
            catch (Exception err)
            {
                postStream.Close();
                fs.Dispose();
                //Utility.LogWriter log = new Utility.LogWriter();
                throw err;
            }
            finally
            {
                postStream.Close();
                fs.Dispose();
            }

        }
    }
}
