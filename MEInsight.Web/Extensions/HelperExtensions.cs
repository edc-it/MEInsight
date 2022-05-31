using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MEInsight.Web.Extensions
{
    public static class HelperExtensions
    {
        public static DataTable ReadExcelSheet(IFormFile file, bool firstRowIsHeader = true)
        {
            List<string> Headers = new();
            DataTable dt = new();
            var readstream = file.OpenReadStream();

            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(readstream, false))
            {
                //Read First WorkSheets
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                Worksheet worksheet = ((WorksheetPart)doc.WorkbookPart.GetPartById(sheet.Id.Value)).Worksheet;
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                
                int counter = 0;
                
                foreach (Row row in rows)
                {
                    counter++;
                    //Read the first row as header
                    if (counter == 1)
                    {
                        var j = 1;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            var columnName = firstRowIsHeader ? GetCellValue(doc, cell) : "Field" + j++;

                            Headers.Add(columnName);
                            dt.Columns.Add(columnName);
                        }
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            dt.Rows[^1][i] = GetCellValue(doc, cell);
                            i++;
                        }
                    }
                }

            }
            return dt;
        }

        public static void CreateExcelFile(DataTable table, string destination)
        {
            var ds = new DataSet();
            ds.Tables.Add(table);
            ExportDSToExcel(ds, destination);
        }

        private static string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }

        public static void ExportDSToExcel(DataSet ds, string destination)
        {
            using var workbook = SpreadsheetDocument.Create(destination, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
            var workbookPart = workbook.AddWorkbookPart();
            workbook.WorkbookPart.Workbook = new Workbook
            {
                Sheets = new Sheets()
            };

            uint sheetId = 1;

            foreach (DataTable table in ds.Tables)
            {
                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                sheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                if (sheets.Elements<Sheet>().Any())
                {
                    sheetId =
                        sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                Sheet sheet = new() { Id = relationshipId, SheetId = sheetId, Name = table.TableName };
                sheets.Append(sheet);

                Row headerRow = new();

                List<String> columns = new();
                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    Cell cell = new()
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.ColumnName)
                    };
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new();
                    foreach (String col in columns)
                    {
                        Cell cell = new()
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(dsrow[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
            }
        }
    }
}
