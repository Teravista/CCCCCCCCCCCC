// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Packaging.Ionic.Zip;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
int x = 1, y = 1;
List< FileInfo > listaPlikow= new List< FileInfo >();
Dictionary<string, long> dict = new Dictionary<string, long>();
void Searcher(string path, ExcelWorksheet ws,int depth, int max_depth)
{
    if (depth == 0)
    {
        ws.Cells[y, x].Value = path;
        y++;
    }
    int tempy = y;
    if (depth >= max_depth)
    {
        return;
    }

    var dir = new DirectoryInfo(path);

    foreach (FileInfo file in dir.GetFiles())
    {
        listaPlikow.Add(file);
        ws.Cells[y, x].Value = path + "\\" + file.Name;
        ws.Cells[y, x + 1].Value = file.Extension;
        ws.Cells[y, x + 2].Value = file.Attributes;
        ws.Cells[y, x + 3].Value = file.Length;
        y++;
    }

    foreach (DirectoryInfo subDir in dir.GetDirectories()) {
        ws.Cells[y, x].Value = path + "\\" + subDir.Name;
        //ws.Cells[y, x+1].Value = subDir.Name; nie ma rozszerzenia
        //ws.Cells[y, x+2].Value = subDir.Attributes;
        //ws.Cells[y, x+3].Value = "";
        y++;
        tempy = y;
        Searcher(path + "\\" + subDir.Name, ws, depth + 1, max_depth);
        ws.Rows[tempy, y-1].Group();
    }
    //ws.Row(tempy).OutlineLevel = y-tempy;
    //ws.Row(tempy).OutlineLevel.
    


}

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var ep = new ExcelPackage();
var workSheet = ep.Workbook.Worksheets.Add("Struktura katalogu");
ExcelWorksheet ws = ep.Workbook.Worksheets["Struktura katalogu"];

string p_strPath = @"c:\Users\chudy\source\repos\DOT_NET\lab3.xlsx";
string search_Path = @"c:\Users\chudy\source\repos\DOT_NET";
ws.Column(1).Width = 100;
Searcher(search_Path, ws, 0, 3);
List<FileInfo> sortowanaLista = listaPlikow.OrderBy(o => o.Length).Reverse().ToList();

var workSheet2 = ep.Workbook.Worksheets.Add("Statystyki");
ExcelWorksheet ws2 = ep.Workbook.Worksheets["Statystyki"];
ws2.Column(1).Width = 100;
int y2=1, x2=1;
foreach (FileInfo file in sortowanaLista)
{
    ws2.Cells[y2, x2].Value = file.FullName;
    ws2.Cells[y2, x2 + 1].Value = file.Extension;
    ws2.Cells[y2, x2+2].Value = file.Length;
    if(!dict.ContainsKey(file.Extension))
    {
        dict[file.Extension] = file.Length;
    }
    else
    {
        dict[file.Extension] += file.Length;
    }
    y2++;
    if(y2>11)
    {
        break;
    }
}

int startY = y2;
foreach (var info in dict)
{
    ws2.Cells[y2, x2].Value = info.Key;
    ws2.Cells[y2, x2 + 1].Value = info.Value;
    y2++;
}

var chart = (ws2.Drawings.AddChart("PieChart", eChartType.Pie3D) as ExcelPieChart);
chart.Title.Text = "Title Text";
chart.SetPosition(5, 5, 5, 5);
chart.SetSize(600, 300);
ExcelAddress valAdd = new ExcelAddress(startY, 2, y2-1, 2);
var ser = (chart.Series.Add(valAdd.Address, "A"+ startY + ":A"+(y2-1)) as ExcelPieChartSerie);

chart.DataLabel.ShowCategory = true;
chart.DataLabel.ShowPercent = true;
chart.Legend.Border.LineStyle = eLineStyle.Solid;
chart.Legend.Border.Fill.Style = eFillStyle.SolidFill;
chart.Legend.Border.Fill.Color = Color.DarkBlue;


if (File.Exists(p_strPath))
    File.Delete(p_strPath);





// Create excel file on physical disk  
FileStream objFileStrm = File.Create(p_strPath);
objFileStrm.Close();

// Write content to excel file  
File.WriteAllBytes(p_strPath, ep.GetAsByteArray());
Console.WriteLine("Hello, World!");
