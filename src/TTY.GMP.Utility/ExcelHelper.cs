using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 创建一个Excel
        /// </summary>
        /// <returns>返回一个空表格</returns>
        public static HSSFWorkbook InitializeWorkBook()
        {
            var workBook = new HSSFWorkbook();
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            var si = PropertySetFactory.CreateSummaryInformation();
            dsi.Company = "深圳田田云网络科技有限公司";
            si.Author = "www.ttyun.com";
            workBook.DocumentSummaryInformation = dsi;
            workBook.SummaryInformation = si;
            return workBook;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="excelDatas"></param>
        /// <param name="savePath"></param>
        public static void ExportExcel(List<ExcelData> excelDatas, string savePath)
        {
            var workbook = InitializeWorkBook();
            foreach (var excelData in excelDatas)
            {
                FillSheet(workbook, excelData);
            }
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }

        /// <summary>
        /// 填充工作表
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="excelData"></param>
        private static void FillSheet(HSSFWorkbook workbook, ExcelData excelData)
        {
            var title = excelData.Dt.TableName;
            var dt = excelData.Dt;
            var titleStyle = workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            var font = workbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = (short)16;
            titleStyle.SetFont(font);
            var sheet1 = workbook.CreateSheet(title);
            var titleRow = sheet1.CreateRow(0);
            titleRow.Height = (short)20 * 25;
            var region = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count);
            sheet1.AddMergedRegion(region);
            var titleCell = titleRow.CreateCell(0);
            titleCell.CellStyle = titleStyle;
            titleCell.SetCellValue(title);
            var headerRow = sheet1.CreateRow(1);
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            var titleFont = workbook.CreateFont();
            titleFont.FontHeightInPoints = (short)11;
            titleFont.FontName = "宋体";
            headerStyle.SetFont(titleFont);
            var dataformat = workbook.CreateDataFormat();
            headerStyle.DataFormat = dataformat.GetFormat("text");

            headerRow.CreateCell(0).SetCellValue("排名");
            headerRow.GetCell(0).CellStyle = headerStyle;

            for (int i = 0; i < excelData.ColumnHeader.Length; i++)
            {
                headerRow.CreateCell(i + 1).SetCellValue(excelData.ColumnHeader[i]);
                headerRow.GetCell(i + 1).CellStyle = headerStyle;
                sheet1.SetColumnWidth(i, 256 * 18);
            }

            var bodyStyle = workbook.CreateCellStyle();
            bodyStyle.BorderBottom = BorderStyle.Thin;
            bodyStyle.BorderLeft = BorderStyle.Thin;
            bodyStyle.BorderRight = BorderStyle.Thin;
            bodyStyle.BorderTop = BorderStyle.Thin;
            bodyStyle.DataFormat = dataformat.GetFormat("text");
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                var bodyRow = sheet1.CreateRow(r + 2);
                bodyRow.CreateCell(0).SetCellValue(r + 1);
                bodyRow.GetCell(0).CellStyle = bodyStyle;
                bodyRow.GetCell(0).CellStyle.Alignment = HorizontalAlignment.Center;

                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    bodyRow.CreateCell(c + 1).SetCellValue(dt.Rows[r][c].ToString());
                    bodyRow.GetCell(c + 1).CellStyle = bodyStyle;
                }
            }
            sheet1.CreateFreezePane(1, 2);
        }
    }

    /// <summary>
    /// Excel数据描述
    /// </summary>
    public class ExcelData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public DataTable Dt { get; set; }

        /// <summary>
        /// 表头
        /// </summary>
        public string[] ColumnHeader { get; set; }
    }
}
