using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchExcelReader.Data
{
    class DataReader
    {
        private string directoryToScan = "";
        private List<Instructions.Instruction> instructions;
        public List<DataPiece> Data = new List<DataPiece>();
        public DataTable DataAsTable = new DataTable();
        public DataReader(string mDirectoryToScan, List<Instructions.Instruction> mInstructions)
        {
            directoryToScan = mDirectoryToScan;
            instructions = mInstructions;
            setupDataTable();
            readFolder();
        }
        private void readFolder()
        {
            int fileCount = 0;
            DirectoryInfo d = new DirectoryInfo(@directoryToScan);
            FileInfo[] files = d.GetFiles("*.xlsx");
            foreach (FileInfo f in files)
            {
                fileCount++;
                // For each XLSX file in directory
                string fileName = f.FullName;
                Console.WriteLine("Processing file " + fileName);
                // Process XLSX file
                using (ExcelPackage package = new ExcelPackage(new FileInfo(@fileName)))
                {
                    DataRow dr = DataAsTable.NewRow();
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
                    int rowCount = 0;
                    foreach (Instructions.Instruction i in instructions)
                    {
                        workSheet.Cells[i.Cell].Calculate();
                        string val = Convert.ToString(workSheet.Cells[i.Cell].Value);
                        Data.Add(new DataPiece(i, val));
                        dr[rowCount] = val;
                        rowCount++;
                        Console.WriteLine("Cell " + i.Cell + ": " + val);
                    }
                    DataAsTable.Rows.Add(dr);
                }
            }
            Console.WriteLine("Processed " + fileCount + " file(s) successfully!");
        }
        private void setupDataTable()
        {
            foreach (Instructions.Instruction i in instructions)
            {
                DataAsTable.Columns.Add(i.Alias, typeof(string));
            }
        }
    }
}
