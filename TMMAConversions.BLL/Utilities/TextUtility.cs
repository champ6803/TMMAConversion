using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using TMMAConversions.DAL.Models;
using Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace TMMAConversions.BLL.Utilities
{
    public class TextUtility
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ReadValidateErrorBOMFile(string fileName, string excelExtension, string excelName, string validatePath)
        {
            try
            {
                Application xlApp = new Application();
                Workbook xlWorkbook = xlApp.Workbooks.Open(excelName);
                _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
                // init line 5
                for (int i = 5; i < lines.Count(); i++)
                {
                    string[] MessageCNMaterial = { "00300", "00055" };
                    string[] MessageCNAlternative = { "29003" };
                    string[] MessageCNUnit = { "29175" };

                    string[] text = lines[i].Split('\t');

                    string errorCode = string.Format("{0}{1}", text[14].Trim(), text[15].Trim()); // Message Class + Message Number

                    if (text[13] == "E") // Message Type Error
                    {
                        int headerIndex = Int32.Parse(text[9].Trim()) + 3; // index header
                        if (MessageCNMaterial.Contains(errorCode))
                        {
                            // fix position at 1 is Header Material
                            Range cell = xlRange.Cells[1, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNAlternative.Contains(errorCode))
                        {
                            // fix position at 4 is BOMAlternative
                            Range cell = xlRange.Cells[4, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNUnit.Contains(errorCode))
                        {
                            // fix position at 5,3 is Unit
                            Range cell = xlRange.Cells[5, 3];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                    }
                }
                xlWorkbook.SaveAs(validatePath, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorkbook.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ReadValidateErrorWorkCenterFile(string fileName, string excelExtension, string excelName, string validatePath)
        {
            try
            {
                Application xlApp = new Application();
                Workbook xlWorkbook = xlApp.Workbooks.Open(excelName);
                _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
                // init line 5
                for (int i = 5; i < lines.Count(); i++)
                {
                    string[] MessageCNMaterial = { "00300", "00055" };
                    string[] MessageCNAlternative = { "29003" };
                    string[] MessageCNUnit = { "29175" };

                    string[] text = lines[i].Split('\t');

                    string errorCode = string.Format("{0}{1}", text[14].Trim(), text[15].Trim()); // Message Class + Message Number

                    if (text[13] == "E") // Message Type Error
                    {
                        int headerIndex = Int32.Parse(text[9].Trim()) + 3; // index header
                        if (MessageCNMaterial.Contains(errorCode))
                        {
                            // fix position at 1 is Header Material
                            Range cell = xlRange.Cells[1, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNAlternative.Contains(errorCode))
                        {
                            // fix position at 4 is BOMAlternative
                            Range cell = xlRange.Cells[4, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNUnit.Contains(errorCode))
                        {
                            // fix position at 5,3 is Unit
                            Range cell = xlRange.Cells[5, 3];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                    }
                }
                xlWorkbook.SaveAs(validatePath, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorkbook.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ReadValidateErrorWorkCenterRoutingFile(string fileName, string excelExtension, string excelName, string validatePath)
        {
            try
            {
                Application xlApp = new Application();
                Workbook xlWorkbook = xlApp.Workbooks.Open(excelName);
                _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
                // init line 5
                for (int i = 5; i < lines.Count(); i++)
                {
                    string[] MessageCNMaterial = { "00300", "00055" };
                    string[] MessageCNAlternative = { "29003" };
                    string[] MessageCNUnit = { "29175" };

                    string[] MessageDelateOperationRouting = { };
                    
                    string[] text = lines[i].Split('\t');

                    string errorCode = string.Format("{0}{1}", text[14].Trim(), text[15].Trim()); // Message Class + Message Number

                    if (text[13] == "E") // Message Type Error
                    {
                        // Create a new file
                        using (StreamWriter fs = new StreamWriter(fileName + ".txt", false, Encoding.Unicode))
                        {
                            //fs.WriteLine("\t0000\tM\tZCONV_{0}\t{1}          {2}  {3}", Name, user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time
                            //fs.WriteLine("\t0000\tM\tZCONV_{0}\t{1}          {2}  {3}", Name, user, HeaderDate, HeaderTime); // Header BOM {0} User {1} Date {2} Time

                        }

                        int headerIndex = Int32.Parse(text[9].Trim()) + 3; // index header
                        if (MessageCNMaterial.Contains(errorCode))
                        {
                            // fix position at 1 is Header Material
                            Range cell = xlRange.Cells[1, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNAlternative.Contains(errorCode))
                        {
                            // fix position at 4 is BOMAlternative
                            Range cell = xlRange.Cells[4, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNUnit.Contains(errorCode))
                        {
                            // fix position at 5,3 is Unit
                            Range cell = xlRange.Cells[5, 3];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                    }
                }
                xlWorkbook.SaveAs(validatePath, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorkbook.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ReadValidateErrorProductionVersionFile(string fileName, string excelExtension, string excelName, string validatePath)
        {
            try
            {
                Application xlApp = new Application();
                Workbook xlWorkbook = xlApp.Workbooks.Open(excelName);
                _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Range xlRange = xlWorksheet.UsedRange;

                string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
                // init line 5
                for (int i = 5; i < lines.Count(); i++)
                {
                    string[] MessageCNMaterial = { "00300", "00055" };
                    string[] MessageCNAlternative = { "29003" };
                    string[] MessageCNUnit = { "29175" };

                    string[] text = lines[i].Split('\t');

                    string errorCode = string.Format("{0}{1}", text[14].Trim(), text[15].Trim()); // Message Class + Message Number

                    if (text[13] == "E") // Message Type Error
                    {
                        int headerIndex = Int32.Parse(text[9].Trim()) + 3; // index header
                        if (MessageCNMaterial.Contains(errorCode))
                        {
                            // fix position at 1 is Header Material
                            Range cell = xlRange.Cells[1, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNAlternative.Contains(errorCode))
                        {
                            // fix position at 4 is BOMAlternative
                            Range cell = xlRange.Cells[4, headerIndex];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                        if (MessageCNUnit.Contains(errorCode))
                        {
                            // fix position at 5,3 is Unit
                            Range cell = xlRange.Cells[5, 3];
                            // hilight fill
                            cell.Font.Color = ColorTranslator.ToOle(Color.Red);
                            cell.Borders.LineStyle = XlLineStyle.xlContinuous;
                            cell.Borders.Weight = 2d;
                            cell.Borders.Color = ColorTranslator.ToOle(Color.Red);
                            // add message comments
                            cell.AddComment(text[2]);
                        }

                    }
                }
                xlWorkbook.SaveAs(validatePath, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlWorkbook.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}