using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MEInsight.Web.Extensions
{
    public static class HelperExtensions
    {
		public static DataTable ReadExcelSheet(IFormFile file)
		{
			DataTable dt = new();
			var readStream = file.OpenReadStream();

			using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(readStream, false))
			{

				WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
				IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
				string relationshipId = sheets.First().Id.Value;
				WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
				Worksheet workSheet = worksheetPart.Worksheet;
				SheetData sheetData = workSheet.GetFirstChild<SheetData>();
				IEnumerable<Row> rows = sheetData.Descendants<Row>();

				foreach (Cell cell in rows.ElementAt(0))
				{
					dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
				}

				foreach (Row row in rows)
				{
					DataRow tempRow = dt.NewRow();

					for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
					{
						Cell cell = row.Descendants<Cell>().ElementAt(i);

						//Gets Cell Index
						int actualCellIndex = CellReferenceToIndex(cell);

						//Format Cells
						if (cell?.StyleIndex?.Value == null)
						{
							tempRow[actualCellIndex] = GetCellValue(spreadSheetDocument, cell);
						}
						else
						{
							tempRow[actualCellIndex] = GetFormattedCellValue(workbookPart, cell);
						}
					}
					dt.Rows.Add(tempRow);
				}
			}
			dt.Rows.RemoveAt(0);
			return dt;
		}

		public static string GetCellValue(SpreadsheetDocument document, Cell cell)
		{
			SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
			string value = cell.CellValue.InnerXml;

			if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
			{
				return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
			}
			else
			{
				return value;
			}
		}

		//Format Cell Value based on Formats()
		private static string? GetFormattedCellValue(WorkbookPart workbookPart, Cell cell)
		{
			if (cell == null)
			{
				return null;
			}

			string value = "";

			if (cell.DataType == null) // number & dates
			{
				int styleIndex = (int)cell.StyleIndex.Value;
				CellFormat cellFormat = (CellFormat)workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ElementAt(styleIndex);
				uint formatId = cellFormat.NumberFormatId.Value;

				if (formatId == (uint)Formats.DateShort || formatId == (uint)Formats.DateLong)
				{
					double oaDate;
					if (double.TryParse(cell.InnerText, out oaDate))
					{
						value = DateTime.FromOADate(oaDate).ToShortDateString();
					}
				}
				else
				{
					value = cell.InnerText;
				}
			}
			else // Shared string or boolean
			{
				switch (cell.DataType.Value)
				{
					case CellValues.SharedString:
						SharedStringItem ssi = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(cell.CellValue.InnerText));
						value = ssi.Text.Text;
						break;
					case CellValues.Boolean:
						value = cell.CellValue.InnerText == "0" ? "false" : "true";
						break;
					default:
						value = cell.CellValue.InnerText;
						break;
				}
			}

			return value;
		}

		private enum Formats
		{
			General = 0,
			Number = 1,
			Decimal = 2,
			Currency = 164,
			Accounting = 44,
			DateShort = 14,
			DateLong = 165,
			Time = 166,
			Percentage = 10,
			Fraction = 12,
			Scientific = 11,
			Text = 49
		}

		private static int CellReferenceToIndex(Cell cell)
		{
			int index = 0;
			string reference = cell.CellReference.ToString().ToUpper();
			foreach (char ch in reference)
			{
				if (Char.IsLetter(ch))
				{
					int value = (int)ch - (int)'A';
					index = (index == 0) ? value : ((index + 1) * 26) + value;
				}
				else
				{
					return index;
				}
			}
			return index;
		}

		// TODO
		public static void CreateExcelFile(DataTable table, string destination)
        {
            var ds = new DataSet();
            ds.Tables.Add(table);
            ExportDSToExcel(ds, destination);
        }

		// TODO
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
