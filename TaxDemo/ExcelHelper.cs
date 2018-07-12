using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing;


namespace TaxDemo
{
    /// <summary>
    /// 工具类，EXCEL操作。
    /// </summary>
    class ExcelHelper
    {
        private Excel.Application app = null;
        private Excel.Workbook workbook = null;
        //private Excel.Workbooks wookbooks = null;
        private Excel.Worksheet worksheet = null;
        private Excel.Range workSheet_range = null;

        public ExcelHelper()
        {

        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filedir"></param>
        /// <param name="sheetindex"></param>
        /// <param name="readonlyprotect"></param>
        public void OpenFile(string filedir, int sheetindex, bool readonlyprotect = false)
        {
            app = new Excel.Application();
            app.Visible = false;  // Makes Excel visible to the user.
            app.DisplayAlerts = false;

            workbook = app.Workbooks.Open(filedir, ReadOnly: readonlyprotect); // //Never use 2 dots with com objects. however...
            worksheet = workbook.Worksheets[sheetindex];
        }


        public void ChangeSheet(int sheetindex)
        {

            worksheet = workbook.Worksheets[sheetindex];
        }



        /// <summary>
        /// 获取单个单元格的值，Notice CELLS the index starting at [1,1] (row,column)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string GetValue(int row, int col)
        {
            workSheet_range = worksheet.Cells[row, col];

            return workSheet_range.Value2.ToString();
        }

        /// <summary>
        /// 获取区域单元格值
        /// </summary>
        /// <param name="rowfrom"></param>
        /// <param name="colfrom"></param>
        /// <param name="rowto"></param>
        /// <param name="colto"></param>
        /// <returns></returns>
        public List<string> GetValue(int rowfrom, int colfrom, int rowto, int colto)
        {
            List<string> result = new List<string>();
            Excel.Range start = worksheet.Cells[rowfrom, colfrom];
            Excel.Range end = worksheet.Cells[rowto, colto];
            workSheet_range = worksheet.Range[start, end];

            object[,] cellValues = (object[,])workSheet_range.Value2;
            result = cellValues.Cast<object>().ToList().ConvertAll(x => Convert.ToString(x).Replace(" ", string.Empty));//cellValues.Cast<object>.Select(o => o.ToString()).ToList();
            return result;
        }

        /// <summary>
        /// 取一行区域单元格值
        /// </summary>
        /// <param name="rowno"></param>
        /// <returns></returns>
        public List<string> GetRow(int rowno)
        {
            List<string> result = new List<string>();
            Excel.Range start = worksheet.Cells[rowno, 1];
            Excel.Range end = worksheet.Cells[rowno, worksheet.UsedRange.Columns.Count];
            workSheet_range = worksheet.Range[start, end];

            object[,] cellValues = (object[,])workSheet_range.Value2;
            result = cellValues.Cast<object>().ToList().ConvertAll(x => Convert.ToString(x));//cellValues.Cast<object>.Select(o => o.ToString()).ToList();
            return result;
        }

        /// <summary>
        /// 取多行区域单元格值
        /// </summary>
        /// <param name="rowfrom"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<List<string>> GetRows(int rowfrom, int count)
        {
            List<List<string>> result = new List<List<string>>();
            for (int i = rowfrom; i < (rowfrom + count); i++)
            {
                result.Add(GetRow(i));
            }
            return result;
        }

        /// <summary>
        /// 取多行区域单元格值，以姓名列进行保存
        /// </summary>
        /// <param name="rowfrom"></param>
        /// <param name="count"></param>
        /// <param name="namcol"></param>
        /// <returns></returns>
        public Dictionary<string, List<List<string>>> GetRows(int rowfrom, int count, int namcol)
        {
            Dictionary<string, List<List<string>>> result = new Dictionary<string, List<List<string>>>();
            for (int i = rowfrom; i <= count; i++)
            {
                List<string> d = GetRow(i);
                string key = GetValue(i, namcol).Replace(" ", string.Empty).Trim();
                if (result.ContainsKey(key))
                {
                    result[key].Add(d);
                }
                else
                {
                    result.Add(key, new List<List<string>>());
                    result[key].Add(d);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取表头行
        /// </summary>
        /// <param name="headerrow"></param>
        /// <returns></returns>
        public List<string> GetHeader(int headerrow)
        {
            List<string> result = new List<string>();
            Excel.Range start = worksheet.Cells[headerrow, 1];
            Excel.Range end = worksheet.Cells[headerrow, worksheet.UsedRange.Columns.Count];
            workSheet_range = worksheet.Range[start, end];

            object[,] cellValues = (object[,])workSheet_range.Value2;
            result = cellValues.Cast<object>().ToList().ConvertAll(x => Convert.ToString(x));//cellValues.Cast<object>.Select(o => o.ToString()).ToList();
            return result;
        }

        /// <summary>
        /// 新建文件
        /// </summary>
        public void NewFile()
        {
            app = new Excel.Application();
            app.Visible = false;  // Makes Excel visible to the user.
            app.DisplayAlerts = false;

            workbook = app.Workbooks.Add();
            worksheet = app.ActiveSheet;

        }

        /// <summary>
        /// 新增行数据
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="sl"></param>
        public void AddRow(int rowindex, List<string> sl)
        {
            int columnindex = 0;

            foreach (string col in sl)
            {
                columnindex++;
                worksheet.Cells[rowindex, columnindex] = col;

            }
            DrawBorder();
        }

        /// <summary>
        /// 设置单元格数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        public void SetValue(int row, int col, string value)
        {
            worksheet.Cells[row, col] = value;
        }

        /// <summary>
        /// 设置单元格数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        public void SetValue(int row, int col, string[] value)
        {
            for (int i = col; i < col + value.Length; i++)
            {
                worksheet.Cells[row, i] = value[i - col];
            }
        }

        /// <summary>
        /// 文件保存
        /// </summary>
        /// <param name="dir"></param>
        public void SaveAs(string dir)
        {
            workbook.SaveAs(dir);
        }


        /// <summary>
        /// 画边框
        /// </summary>
        /// <param name="rowfrom"></param>
        /// <param name="colfrom"></param>
        /// <param name="rowto"></param>
        /// <param name="colto"></param>
        public void DrawBorder(int rowfrom, int colfrom, int rowto, int colto)
        {
            Excel.Range start = worksheet.Cells[rowfrom, colfrom];
            Excel.Range end = worksheet.Cells[rowto, colto];
            workSheet_range = worksheet.UsedRange;//worksheet.Range[start, end]; allused range or range 

            Excel.Borders borders = workSheet_range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;

            borders = null;
        }

        /// <summary>
        /// 已用区域画边框
        /// </summary>
        public void DrawBorder()
        {

            workSheet_range = worksheet.UsedRange;//worksheet.Range[start, end]; allused range or range 

            Excel.Borders borders = workSheet_range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;

            borders = null;
        }

        /// <summary>
        /// 对象关闭
        /// </summary>
        public void Close()
        {
            //cleaning up COM objects from .NET
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (workSheet_range != null)
            {
                Marshal.ReleaseComObject(workSheet_range);
            }
            if (worksheet != null)
            {
                Marshal.ReleaseComObject(worksheet);
            }
            if (workbook != null)
            {
                workbook.Save();
                workbook.Close(true);
                Marshal.ReleaseComObject(workbook);
            }
            if (app != null)
            {
                app.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(app);
            }
        }

        public void CloseAllProcess()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 获取有多少行数据
        /// </summary>
        /// <returns></returns>
        public int GetRowsCount()
        {

            //return worksheet.UsedRange.Rows.Count;
            int count = 0;
            for (int i = 1; i <= worksheet.UsedRange.Rows.Count; i++)
            {
                workSheet_range = worksheet.Cells[i, 1];

                if (workSheet_range.Value2 == null)
                {
                    count = i - 1;
                    break;
                }
                else if (workSheet_range.Value2.ToString() == string.Empty)
                {
                    count = i - 1;
                    break;
                }
                count = i;
            }
            return count;
        }

        /// <summary>
        /// 获取有多少列
        /// </summary>
        /// <returns></returns>
        public int GetColumnsCount()
        {
            return worksheet.UsedRange.Columns.Count;
        }

        /// <summary>
        /// 获取多少行数据
        /// </summary>
        /// <param name="datarowfrom"></param>
        /// <returns></returns>
        internal int GetDataRowsCount(int datarowfrom)
        {
            int count = 0;
            for (int i = 1; i <= worksheet.UsedRange.Rows.Count; i++)
            {
                workSheet_range = worksheet.Cells[i, 1];

                if (workSheet_range.Value2 == null)
                {
                    count = i - 1;
                    break;
                }
                else if (workSheet_range.Value2.ToString() == string.Empty)
                {
                    count = i - 1;
                    break;
                }
                count = i;
            }
            return count;
        }

        /* Modified by cyq 20160331 */
        /************  Begin  *********/
        public void SetFontNameSize(int row, int col, string fontName, int fontSize)
        {
            worksheet.Cells[row, col].Font.Name = fontName;
            worksheet.Cells[row, col].Font.Size = fontSize;
        }


        /************  End  *********/
    }
}
