using ApiCommon;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManyThread
{
    #region Thread
    public class NopiExportData
    {
        //定义线程， 目前开启10个
        static internal Thread[] threads = new Thread[10];

        public DataSet GetData()
        {
            try
            {
                DataSet dsNew = new DataSet();
                for (int i = 1; i < 11; i++)
                {
                    int PageIndex = i;
                    //拼接查询条件
                    string strWhere = "";
                    //参数集合
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@TableName",SqlDbType.NVarChar,50){Value = "T_UserInfo"},
                    new SqlParameter("@ReturnFields",SqlDbType.NVarChar,2000){Value = "UserID,Name,IDCard,Phone,Password"},
                    new SqlParameter("@PageSize",SqlDbType.Int){Value = 30000},
                    new SqlParameter("@PageIndex",SqlDbType.Int){Value = PageIndex},
                    new SqlParameter("@Where",SqlDbType.NVarChar,2000){Value = strWhere},
                    new SqlParameter("@Orderfld",SqlDbType.NVarChar,2000){Value = "IDCard"},
                    new SqlParameter("@OrderType",SqlDbType.Int){Value = 1}
                    };
                    //执行存储过程
                    var ds = DbHelper_WebRep.GetTableByStoredProcedure("SupesoftPage", parameters);
                    var dt = ds.Tables[0].Copy();
                    dt.TableName = (PageIndex - 1).ToString();
                    dsNew.Tables.Add(dt);
                }
                return dsNew;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RunThread()
        {
            try
            {
                string excelFolder = Environment.CurrentDirectory + "/excelPage";//excel文件保存地址
                string zipFolder = Environment.CurrentDirectory + "/excelPage.zip";//zip压缩文件存放地址
                var ds = GetData();//获取数据
                Console.WriteLine("数据获取成功：" + DateTime.Now.ToString("HH:mm:ss ffff"));

                //实列化并调用方法
                ExportExcel dd = new ExportExcel(ds, excelFolder);
                for (int i = 0; i < threads.Length; i++)//初始化线程
                {
                    Thread t = new Thread(new ThreadStart(dd.RunThread));
                    threads[i] = t;
                }
                for (int i = 0; i < threads.Length; i++)//赋值线程名称
                    threads[i].Name = i.ToString();
                for (int i = 0; i < threads.Length; i++)//开启线程
                    threads[i].Start();
                for (int i = 0; i < threads.Length; i++)//等待线程执行完毕
                    threads[i].Join();

                //导出的excel文件夹，压缩zip
                ZipFloClass zp = new ZipFloClass();
                zp.CompressDirectory(excelFolder, zipFolder, 5, false);

                Console.WriteLine("导出文件地址为：" + zipFolder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class ExportExcel
    {
        DataSet dsContent;
        string excelPath;
        public ExportExcel(DataSet ds, string path)
        {
            dsContent = ds;
            excelPath = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void RunThread()
        {
            int index = Convert.ToInt32(Thread.CurrentThread.Name);
            string excelName = excelPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + index + ".xlsx";
            DataTableToExcel(excelName, dsContent.Tables[index], index.ToString(), true);
        }

        /// <summary>
        /// 把DataTable的数据写入到指定的excel文件中
        /// </summary>
        /// <param name="TargetFileNamePath">目标文件excel的路径</param>
        /// <param name="sourceData">要写入的数据</param>
        /// <param name="sheetName">excel表中的sheet的名称，可以根据情况自己起</param>
        /// <param name="IsWriteColumnName">是否写入DataTable的列名称</param>
        /// <returns>返回写入的行数</returns>
        public void DataTableToExcel(string TargetFileNamePath, DataTable sourceData, string sheetName, bool IsWriteColumnName)
        {
            Console.WriteLine(Thread.CurrentThread.Name + "线程进入方法，Sheet" + sheetName);

            using (FileStream fs = new FileStream(TargetFileNamePath, FileMode.OpenOrCreate))//读取流
            {
                //根据Excel文件的后缀名创建对应的workbook
                IWorkbook workbook = null;
                if (TargetFileNamePath.IndexOf(".xlsx") > 0)
                {  //2007版本的excel
                    workbook = new XSSFWorkbook();
                }
                else if (TargetFileNamePath.IndexOf(".xls") > 0) //2003版本的excel
                {
                    workbook = new HSSFWorkbook();
                }

                //excel表的sheet名
                ISheet sheet = workbook.CreateSheet("Sheet" + sheetName);

                //写入Excel的行数
                int WriteRowCount = 0;

                //指明需要写入列名，则写入DataTable的列名,第一行写入列名
                if (IsWriteColumnName)
                {
                    //sheet表创建新的一行,即第一行
                    IRow ColumnNameRow = sheet.CreateRow(0); //0下标代表第一行
                                                             //进行写入DataTable的列名
                    for (int colunmNameIndex = 0; colunmNameIndex < sourceData.Columns.Count; colunmNameIndex++)
                    {
                        ColumnNameRow.CreateCell(colunmNameIndex).SetCellValue(sourceData.Columns[colunmNameIndex].ColumnName);
                    }
                    WriteRowCount++;
                }

                //写入数据
                for (int row = 0; row < sourceData.Rows.Count; row++)
                {
                    //sheet表创建新的一行
                    IRow newRow = sheet.CreateRow(WriteRowCount);
                    for (int column = 0; column < sourceData.Columns.Count; column++)
                    {

                        newRow.CreateCell(column).SetCellValue(sourceData.Rows[row][column].ToString());

                    }

                    WriteRowCount++;  //写入下一行
                }

                workbook.Write(fs);
                fs.Close();
                workbook.Close();
                Console.WriteLine(TargetFileNamePath + "文件，Sheet" + sheetName + "保存成功。" + DateTime.Now.ToString("HH:mm:ss ffff"));
            }
        }
    }

    public class ZipFloClass
    {
        /// <summary>    
        /// 压缩文件夹    
        /// </summary>    
        ///<param name="dirPath">要打包的文件夹    
        ///<param name="GzipFileName">目标文件名    
        ///<param name="CompressionLevel">压缩品质级别（0~9）    
        ///<param name="deleteDir">是否删除原文件夹  
        public void CompressDirectory(string dirPath, string GzipFileName, int CompressionLevel, bool deleteDir)
        {
            using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(GzipFileName)))
            {
                zipoutputstream.SetLevel(CompressionLevel);
                Crc32 crc = new Crc32();
                Dictionary<string, DateTime> fileList = GetAllFies(dirPath);
                foreach (KeyValuePair<string, DateTime> item in fileList)
                {
                    FileStream fs = File.OpenRead(item.Key.ToString());
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(item.Key.Substring(dirPath.Length));
                    entry.DateTime = item.Value;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipoutputstream.PutNextEntry(entry);
                    zipoutputstream.Write(buffer, 0, buffer.Length);
                }
            }
            if (deleteDir)
            {
                Directory.Delete(dirPath, true);
            }
        }

        /// <summary>    
        /// 获取所有文件    
        /// </summary>    
        /// <returns></returns>
        private Dictionary<string, DateTime> GetAllFies(string dir)
        {
            Dictionary<string, DateTime> FilesList = new Dictionary<string, DateTime>();
            DirectoryInfo fileDire = new DirectoryInfo(dir);
            if (!fileDire.Exists)
            {
                throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");
            }
            GetAllDirFiles(fileDire, FilesList);
            GetAllDirsFiles(fileDire.GetDirectories(), FilesList);
            return FilesList;
        }

        /// <summary>    
        /// 获取一个文件夹下的所有文件夹里的文件    
        /// </summary>    
        ///<param name="dirs">    
        ///<param name="filesList">    
        private void GetAllDirsFiles(DirectoryInfo[] dirs, Dictionary<string, DateTime> filesList)
        {
            foreach (DirectoryInfo dir in dirs)
            {
                foreach (FileInfo file in dir.GetFiles("*.*"))
                {
                    filesList.Add(file.FullName, file.LastWriteTime);
                }
                GetAllDirsFiles(dir.GetDirectories(), filesList);
            }
        }
        /// <summary>    
        /// 获取一个文件夹下的文件    
        /// </summary>    
        ///<param name="dir">目录名称    
        ///<param name="filesList">文件列表HastTable    
        private void GetAllDirFiles(DirectoryInfo dir, Dictionary<string, DateTime> filesList)
        {
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                filesList.Add(file.FullName, file.LastWriteTime);
            }
        }
    }

    #endregion

    #region ThreadPool

    public class NopiExportDataThreadPool
    {
        public void RunThread()
        {
            try
            {
                string excelFolder = Environment.CurrentDirectory + "/excelPage";//excel文件保存地址
                string zipFolder = Environment.CurrentDirectory + "/excelPage.zip";//zip压缩文件存放地址

                if (!Directory.Exists(excelFolder))
                {
                    Directory.CreateDirectory(excelFolder);
                }

                int MaxCount = 10;//允许线程池中运行最多10个线程

                //新建ManualResetEvent对象并且初始化为无信号状态
                ManualResetEvent eventX = new ManualResetEvent(false);
                Console.WriteLine("允许线程池运行最多{0}个线程", MaxCount);
                ExportExcelThreadPool pool = new ExportExcelThreadPool();
                //创建工作项
                //注意初始化oAlpha对象的eventX属性
                pool.eventX = eventX;

                for (int iItem = 1; iItem <= MaxCount; iItem++)
                {
                    var path = excelFolder + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + iItem + ".xlsx";
                    ThreadPool.QueueUserWorkItem(new WaitCallback(pool.RunThread), new OptMsg(path, iItem));
                }
                //等待事件的完成，即线程调用ManualResetEvent.Set()方法
                eventX.WaitOne(Timeout.Infinite, true);
                //WaitOne()方法使调用它的线程等待直到eventX.Set()方法被调用


                //导出的excel文件夹，压缩zip
                //ZipFloClass zp = new ZipFloClass();
                //zp.CompressDirectory(excelFolder, zipFolder, 5, false);

                Console.WriteLine("导出文件地址为：" + zipFolder);
                Console.WriteLine("线程最终结束：" + DateTime.Now.ToString("HH:mm:ss ffff"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class OptMsg
    {
        public string excelPath;
        public int pageIndex;

        public OptMsg(string path, int index)
        {
            excelPath = path;
            pageIndex = index;
        }
    }

    public class ExportExcelThreadPool
    {
        public Hashtable HashCount;
        public ManualResetEvent eventX; //信号灯

        public void RunThread(object data)
        {
            //输出当前线程的hash编码值和Cookie的值

            OptMsg msg = data as OptMsg;
            DataTable dt;
            lock (this)
            {
                dt = GetData(msg.pageIndex);
            }
            eventX.Set();
            DataTableToExcel(msg.excelPath, dt, msg.pageIndex.ToString(), true);
        }

        public DataTable GetData(int PageIndex)
        {
            try
            {
                Console.WriteLine("获取{0}数据", PageIndex);
                //拼接查询条件
                string strWhere = "";
                //参数集合
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TableName",SqlDbType.NVarChar,50){Value = "T_UserInfo"},
                    new SqlParameter("@ReturnFields",SqlDbType.NVarChar,2000){Value = "UserID,Name,IDCard,Phone,Password"},
                    new SqlParameter("@PageSize",SqlDbType.Int){Value = 30000},
                    new SqlParameter("@PageIndex",SqlDbType.Int){Value = PageIndex},
                    new SqlParameter("@Where",SqlDbType.NVarChar,2000){Value = strWhere},
                    new SqlParameter("@Orderfld",SqlDbType.NVarChar,2000){Value = "IDCard"},
                    new SqlParameter("@OrderType",SqlDbType.Int){Value = 1}
                };
                //执行存储过程
                var ds = DbHelper_WebRep.GetTableByStoredProcedure("SupesoftPage", parameters);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 把DataTable的数据写入到指定的excel文件中
        /// </summary>
        /// <param name="TargetFileNamePath">目标文件excel的路径</param>
        /// <param name="sourceData">要写入的数据</param>
        /// <param name="sheetName">excel表中的sheet的名称，可以根据情况自己起</param>
        /// <param name="IsWriteColumnName">是否写入DataTable的列名称</param>
        /// <returns>返回写入的行数</returns>
        public void DataTableToExcel(string TargetFileNamePath, DataTable sourceData, string sheetName, bool IsWriteColumnName)
        {
            Console.WriteLine("导出excel，Sheet" + sheetName);

            using (FileStream fs = new FileStream(TargetFileNamePath, FileMode.OpenOrCreate))//读取流
            {
                //根据Excel文件的后缀名创建对应的workbook
                IWorkbook workbook = null;
                if (TargetFileNamePath.IndexOf(".xlsx") > 0)
                {  //2007版本的excel
                    workbook = new XSSFWorkbook();
                }
                else if (TargetFileNamePath.IndexOf(".xls") > 0) //2003版本的excel
                {
                    workbook = new HSSFWorkbook();
                }

                //excel表的sheet名
                ISheet sheet = workbook.CreateSheet("Sheet" + sheetName);

                //写入Excel的行数
                int WriteRowCount = 0;

                //指明需要写入列名，则写入DataTable的列名,第一行写入列名
                if (IsWriteColumnName)
                {
                    //sheet表创建新的一行,即第一行
                    IRow ColumnNameRow = sheet.CreateRow(0); //0下标代表第一行
                                                             //进行写入DataTable的列名
                    for (int colunmNameIndex = 0; colunmNameIndex < sourceData.Columns.Count; colunmNameIndex++)
                    {
                        ColumnNameRow.CreateCell(colunmNameIndex).SetCellValue(sourceData.Columns[colunmNameIndex].ColumnName);
                    }
                    WriteRowCount++;
                }

                //写入数据
                for (int row = 0; row < sourceData.Rows.Count; row++)
                {
                    //sheet表创建新的一行
                    IRow newRow = sheet.CreateRow(WriteRowCount);
                    for (int column = 0; column < sourceData.Columns.Count; column++)
                    {

                        newRow.CreateCell(column).SetCellValue(sourceData.Rows[row][column].ToString());

                    }

                    WriteRowCount++;  //写入下一行
                }

                workbook.Write(fs);
                fs.Close();
                workbook.Close();
            }
            Console.WriteLine("Sheet" + sheetName + "保存成功。" + DateTime.Now.ToString("HH:mm:ss ffff"));
        }
    }

    #endregion

    #region Task

    public class NpoiExportDataTask
    {
        public void RunThread()
        {
            string excelFolder = Environment.CurrentDirectory + "/excelPage";//excel文件保存地址
            string zipFolder = Environment.CurrentDirectory + "/excelPage.zip";//zip压缩文件存放地址

            if (!Directory.Exists(excelFolder))
            {
                Directory.CreateDirectory(excelFolder);
            }

            Action<object> action = (object model) =>
             {
                 var _actionToExcel = model as actionToExcel;
                 DataTableToExcel(_actionToExcel.TargetFileNamePath, _actionToExcel.sourceData, _actionToExcel.sheetName, _actionToExcel.IsWriteColumnName);
             };

            for (int i = 0; i < 10; i++)
            {
                var t = Task<DataTable>.Factory.StartNew(new Func<object, DataTable>(GetData), (i + 1));
                t.Wait();
                var path = excelFolder + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + (i + 1) + ".xlsx";
                actionToExcel model = new actionToExcel() { TargetFileNamePath = path, sourceData = t.Result, sheetName = (i+1).ToString(), IsWriteColumnName = true };
                Task.Factory.StartNew(action, model);
            }
            Task.WaitAll();
        }

        public DataTable GetData(object PageIndex)
        {
            try
            {
                Console.WriteLine("获取{0}数据", PageIndex);
                //拼接查询条件
                string strWhere = "";
                //参数集合
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TableName",SqlDbType.NVarChar,50){Value = "T_UserInfo"},
                    new SqlParameter("@ReturnFields",SqlDbType.NVarChar,2000){Value = "UserID,Name,IDCard,Phone,Password"},
                    new SqlParameter("@PageSize",SqlDbType.Int){Value = 30000},
                    new SqlParameter("@PageIndex",SqlDbType.Int){Value = PageIndex},
                    new SqlParameter("@Where",SqlDbType.NVarChar,2000){Value = strWhere},
                    new SqlParameter("@Orderfld",SqlDbType.NVarChar,2000){Value = "IDCard"},
                    new SqlParameter("@OrderType",SqlDbType.Int){Value = 1}
                };
                //执行存储过程
                var ds = DbHelper_WebRep.GetTableByStoredProcedure("SupesoftPage", parameters);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 把DataTable的数据写入到指定的excel文件中
        /// </summary>
        /// <param name="TargetFileNamePath">目标文件excel的路径</param>
        /// <param name="sourceData">要写入的数据</param>
        /// <param name="sheetName">excel表中的sheet的名称，可以根据情况自己起</param>
        /// <param name="IsWriteColumnName">是否写入DataTable的列名称</param>
        /// <returns>返回写入的行数</returns>
        public void DataTableToExcel(string TargetFileNamePath, DataTable sourceData, string sheetName, bool IsWriteColumnName)
        {
            Console.WriteLine("导出excel，Sheet" + sheetName);

            using (FileStream fs = new FileStream(TargetFileNamePath, FileMode.OpenOrCreate))//读取流
            {
                //根据Excel文件的后缀名创建对应的workbook
                IWorkbook workbook = null;
                if (TargetFileNamePath.IndexOf(".xlsx") > 0)
                {  //2007版本的excel
                    workbook = new XSSFWorkbook();
                }
                else if (TargetFileNamePath.IndexOf(".xls") > 0) //2003版本的excel
                {
                    workbook = new HSSFWorkbook();
                }

                //excel表的sheet名
                ISheet sheet = workbook.CreateSheet("Sheet" + sheetName);

                //写入Excel的行数
                int WriteRowCount = 0;

                //指明需要写入列名，则写入DataTable的列名,第一行写入列名
                if (IsWriteColumnName)
                {
                    //sheet表创建新的一行,即第一行
                    IRow ColumnNameRow = sheet.CreateRow(0); //0下标代表第一行
                                                             //进行写入DataTable的列名
                    for (int colunmNameIndex = 0; colunmNameIndex < sourceData.Columns.Count; colunmNameIndex++)
                    {
                        ColumnNameRow.CreateCell(colunmNameIndex).SetCellValue(sourceData.Columns[colunmNameIndex].ColumnName);
                    }
                    WriteRowCount++;
                }

                //写入数据
                for (int row = 0; row < sourceData.Rows.Count; row++)
                {
                    //sheet表创建新的一行
                    IRow newRow = sheet.CreateRow(WriteRowCount);
                    for (int column = 0; column < sourceData.Columns.Count; column++)
                    {

                        newRow.CreateCell(column).SetCellValue(sourceData.Rows[row][column].ToString());

                    }

                    WriteRowCount++;  //写入下一行
                }

                workbook.Write(fs);
                fs.Close();
                workbook.Close();
            }
            Console.WriteLine("Sheet" + sheetName + "保存成功。" + DateTime.Now.ToString("HH:mm:ss ffff"));
        }
    }

    public class actionToExcel
    {
        public string TargetFileNamePath { get; set; }
        public DataTable sourceData { get; set; }
        public string sheetName { get; set; }
        public bool IsWriteColumnName { get; set; }
    }
    #endregion
}
