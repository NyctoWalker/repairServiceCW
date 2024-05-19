using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repairServiceCW.Models
{
    public class ReportGenerator
    {
        private readonly string projPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\";

        public void WordGenerateReport(List<OrderElement> elements)
        {
            string path = projPath + $"{elements[0].IdOrderNavigation.OrderCode} - {DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss.fff")}.docx";

            using (WordprocessingDocument doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                var mainPart = doc.AddMainDocumentPart();
                Body body = new Body();

                mainPart.Document = new Document(body);

                // Таблица с границами
                var table = new Table();

                TableProperties tblProperties = new TableProperties();
                TableBorders tblBorders = new TableBorders();

                TopBorder topBorder = new TopBorder();
                topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                topBorder.Color = "000000";
                tblBorders.AppendChild(topBorder);

                BottomBorder bottomBorder = new BottomBorder();
                bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                bottomBorder.Color = "000000";
                tblBorders.AppendChild(bottomBorder);

                RightBorder rightBorder = new RightBorder();
                rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                rightBorder.Color = "000000";
                tblBorders.AppendChild(rightBorder);

                LeftBorder leftBorder = new LeftBorder();
                leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                leftBorder.Color = "000000";
                tblBorders.AppendChild(leftBorder);

                InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();
                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                insideHBorder.Color = "000000";
                tblBorders.AppendChild(insideHBorder);

                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();
                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                insideVBorder.Color = "000000";
                tblBorders.AppendChild(insideVBorder);

                tblProperties.AppendChild(tblBorders);
                table.AppendChild(tblProperties);

                // Заголовки
                var headerRow = new TableRow(
                    new TableCell(new Paragraph(new Run(new Text("Название элемента")))),
                    new TableCell(new Paragraph(new Run(new Text("Цена")))),
                    new TableCell(new Paragraph(new Run(new Text("Дата окончания")))),
                    new TableCell(new Paragraph(new Run(new Text("Тип")))));

                table.Append(headerRow);

                // Заполнение данными из списка элементов
                foreach (var orderElement in elements)
                {
                    string add = "";
                    if (orderElement.CodeElementNavigation.ElementType1 != "Работа")
                    {
                        add = $"({orderElement.ElementQuantity} шт.)";
                    }

                    var row = new TableRow();
                    row.Append(
                    new TableCell(new Paragraph(new Run(new Text(orderElement.ElementName + add)))),
                    new TableCell(new Paragraph(new Run(new Text(orderElement.ElementPrice.ToString())))),
                    new TableCell(new Paragraph(new Run(new Text(orderElement.ElementEndDate.ToString("dd.MM.yyyy"))))),
                    new TableCell(new Paragraph(new Run(new Text(orderElement.CodeElementNavigation.ElementType1)))));
                    table.Append(row);
                }

                // Добавляем таблицу в документ
                body.Append(table);

                mainPart.Document.Save();
            }
        }
    }
}
