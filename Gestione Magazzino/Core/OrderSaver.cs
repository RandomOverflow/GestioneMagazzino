using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gestione_Magazzino.Core
{
    public class OrderSaver
    {
        public OrderSaver(IEnumerable<Order> orders, string outputPath)
        {
            FileOutputPath = outputPath;

            SavingOrders = orders;
        }

        public IEnumerable<Order> SavingOrders { get; }
        public string FileOutputPath { get; }

        public void Save()
        {
            SpreadsheetDocument xl = SpreadsheetDocument.Create(FileOutputPath, SpreadsheetDocumentType.Workbook);

            WorkbookPart wbp = xl.AddWorkbookPart();
            WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
            Workbook wb = new Workbook();
            FileVersion fv = new FileVersion { ApplicationName = "Microsoft Office Excel" };
            Worksheet ws = new Worksheet();

            SheetData sd = new SheetData();
            uint rowIndex = 1;
            Row rowHeaders = new Row { RowIndex = rowIndex++ };

            string[] headers = { "Codice Ordine", "Totale (€)", "Data & Ora" };

            foreach (string header in headers)
            {
                Cell cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(header)
                };
                rowHeaders.AppendChild(cell);
            }
            sd.AppendChild(rowHeaders);

            foreach (Order order in SavingOrders)
            {
                Row row = new Row { RowIndex = rowIndex };

                Cell cell = new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(order.Id.ToString())
                };

                Cell cell2 = new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(order.TotalPrice.ToString("##.##")),
                    StyleIndex = 4
                };
                Cell cell3 = new Cell
                {
                    DataType = CellValues.Date,
                    CellValue = new CellValue(order.DateTime.ToString("dd-MM-yyyy HH:mm:ss"))
                };
                row.Append(cell, cell2, cell3);

                sd.AppendChild(row);
                rowIndex++;
            }

            Row rowFine = new Row { RowIndex = rowIndex };
            //Cell cellTextSum = new Cell
            //{
            //    DataType = CellValues.String,
            //    CellValue = new CellValue("Totale (€):"),

            //};
            Cell cellSum = new Cell
            {
                DataType = CellValues.Number,
                CellValue = new CellValue(SavingOrders.Sum(x => x.TotalPrice).ToString("##.##")),
                CellReference = "B" + rowIndex
            };

            rowFine.AppendChild(cellSum);
            sd.AppendChild(rowFine);

            ws.AppendChild(sd);
            wsp.Worksheet = ws;
            wsp.Worksheet.Save();

            Sheets sheets = new Sheets();
            Sheet sheet = new Sheet
            {
                Name = Path.GetFileNameWithoutExtension(FileOutputPath),
                SheetId = 1,
                Id = wbp.GetIdOfPart(wsp)
            };
            sheets.AppendChild(sheet);
            wb.Append(fv, sheets);

            xl.WorkbookPart.Workbook = wb;
            xl.WorkbookPart.Workbook.Save();
            xl.Close();
        }
    }
}