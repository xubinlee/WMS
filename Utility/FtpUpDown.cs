﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class FtpUpDown
    {
        string ftpServerIP;//服务器ip地址
        string ftpUserID;//用户名
        string ftpPassword;//密码
        FtpWebRequest reqFTP;

        private void Connect(String path)//连接ftp
        {

            // 根据uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        }

        public FtpUpDown(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            this.ftpServerIP = ftpServerIP;
            this.ftpUserID = ftpUserID;
            this.ftpPassword = ftpPassword;
        }

        private string[] GetFileList(string path, string WRMethods)//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            try
            {
                Connect(path);
                reqFTP.Method = WRMethods;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public string[] GetFileList(string path)//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            //return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
            return GetFileList(ftpServerIP + path, WebRequestMethods.Ftp.ListDirectory);
        }
        public string[] GetFileList()//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            //return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
            return GetFileList(ftpServerIP, WebRequestMethods.Ftp.ListDirectory);
        }
        public bool Upload(string filename, out string error) //上面的代码实现了从ftp服务器上载文件的功能
        {
            //FileInfo fileInfo = new FileInfo(filename);
            ////string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            //string url = ftpServerIP + fileInfo;
            //Connect(url);//连接         
            //// 默认为true，连接不会被关闭
            //// 在一个命令之后被执行
            //reqFTP.KeepAlive = false;
            //// 指定执行什么命令
            //reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            //// 上传文件时通知服务器文件的大小
            //reqFTP.ContentLength = fileInfo.Length;
            //// 缓冲大小设置为kb 
            //int buffLength = 2048;
            //byte[] buff = new byte[buffLength];
            //int contentLen;
            //// 打开一个文件流(System.IO.FileStream) 去读上传的文件
            //FileStream fs = fileInfo.OpenRead();
            //try
            //{
            //    // 把上传的文件写入流
            //    Stream strm = reqFTP.GetRequestStream();
            //    // 每次读文件流的kb
            //    contentLen = fs.Read(buff, 0, buffLength);
            //    // 流内容没有结束
            //    while (contentLen != 0)
            //    {
            //        // 把内容从file stream 写入upload stream 
            //        strm.Write(buff, 0, contentLen);
            //        contentLen = fs.Read(buff, 0, buffLength);
            //    }
            //    // 关闭两个流
            //    strm.Close();
            //    fs.Close();
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "Upload Error");
            //}
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpServerIP + fileInf.Name;
            FtpWebRequest reqFTP;
            // Create FtpWebRequest object from the Uri provided 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerIP + fileInf.Name));
            try
            {
                // Provide the WebPermission Credintials 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                // By default KeepAlive is true, where the control connection is not closed 
                // after a command is executed. 
                reqFTP.KeepAlive = false;

                // Specify the command to be executed. 
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type. 
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file 
                reqFTP.ContentLength = fileInf.Length;

                // The buffer size is set to 2kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded 
                //FileStream fs = fileInf.OpenRead(); 
                FileStream fs = fileInf.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Stream to which the file to be upload is written 
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time 
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends 
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream 
                strm.Close();
                fs.Close();
                error = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                reqFTP.Abort();
                //  Logging.WriteError(ex.Message + ex.StackTrace);
                error = ex.Message + ex.StackTrace;
                return false;
            }
        }
        public bool Download(string filePath, string fileName, out string errorinfo)////上面的代码实现了从ftp服务器下载文件的功能
        {
            try
            {
                String onlyFileName = Path.GetFileName(fileName);
                string newFileName = filePath + onlyFileName;
                if (File.Exists(newFileName))
                {
                    errorinfo = string.Format("本地文件{0}已存在,无法下载", newFileName);
                    return false;
                }
                //string url = "ftp://" + ftpServerIP + "/" + fileName;
                string url = ftpServerIP + fileName;
                Connect(url);//连接 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                FileStream outputStream = new FileStream(newFileName, FileMode.Create);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                errorinfo = "";
                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("因{0},无法下载", ex.Message);
                return false;
            }
        }
        //删除文件
        public void DeleteFileName(string fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                //string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                string url = ftpServerIP + fileInfo.Name;
                Connect(url);//连接         
                // 默认为true，连接不会被关闭
                // 在一个命令之后被执行
                reqFTP.KeepAlive = false;
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch 
            {
                //MessageBox.Show(ex.Message, "删除错误");
            }
        }
        //创建目录
        public void MakeDir(string dirName)
        {
            try
            {
                //string uri = "ftp://" + ftpServerIP + "/" + dirName;
                string url = ftpServerIP + dirName;
                Connect(url);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
        }
        //删除目录
        public void delDir(string dirName)
        {
            try
            {
                //string uri = "ftp://" + ftpServerIP + "/" + dirName;
                string url = ftpServerIP + dirName;
                Connect(url);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
        }
        //获得文件大小
        public long GetFileSize(string filename)
        {
            long fileSize = 0;
            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                //string uri = "ftp://" + ftpServerIP + "/" + fileInfo.Name;
                string url = ftpServerIP + fileInfo.Name;
                Connect(url);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                fileSize = response.ContentLength;
                response.Close();
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
            return fileSize;
        }
        //文件改名
        public void Rename(string currentFilename, string newFilename)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(currentFilename);
                //string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                string url = ftpServerIP + fileInfo.Name;
                Connect(url);//连接
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //Stream ftpStream = response.GetResponseStream();
                //ftpStream.Close();
                response.Close();
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }
        }

        //获得文件明晰
        public string[] GetFilesDetailList()
        {
            //return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);
            return GetFileList(ftpServerIP, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
        //获得文件明晰
        public string[] GetFilesDetailList(string path)
        {
            //return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
            return GetFileList(ftpServerIP + path, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
    
    }
}
