// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Packaging.Ionic.Zip;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
int x, y;
void Searcher(string path, ExcelWorksheet ws,int depth, int max_depth, List<FileInfo> listaPlikow)
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
        Searcher(path + "\\" + subDir.Name, ws, depth + 1, max_depth,listaPlikow);
        ws.Rows[tempy, y-1].Group();
    }
    //ws.Row(tempy).OutlineLevel = y-tempy;
    //ws.Row(tempy).OutlineLevel.

}

void CreateExcel(string search_Path,string outputFileName,int maxDepth)
{
    x = 1;y = 1;
    int y2 = 1, x2 = 1;
    List<FileInfo> listaPlikow = new List<FileInfo>();
    Dictionary<string, long> dictOfSize = new Dictionary<string, long>();
    Dictionary<string, int> dictOfCount = new Dictionary<string, int>();
    if (!Directory.Exists(search_Path))
    {
        Console.WriteLine("base directory is wrong try again!!");
        return;
    }
    string p_strPath = search_Path +"\\" + outputFileName +".xlsx";
    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    var ep = new ExcelPackage();
    var workSheet = ep.Workbook.Worksheets.Add("Struktura katalogu");
    ExcelWorksheet ws = ep.Workbook.Worksheets["Struktura katalogu"];

    ws.Column(1).Width = 100;
    Searcher(search_Path, ws, 0, maxDepth,listaPlikow);
    List<FileInfo> sortowanaLista = listaPlikow.OrderBy(o => o.Length).Reverse().ToList();

    var workSheet2 = ep.Workbook.Worksheets.Add("Statystyki");
    ExcelWorksheet ws2 = ep.Workbook.Worksheets["Statystyki"];
    ws2.Column(1).Width = 100;

    foreach (FileInfo file in sortowanaLista)
    {
        ws2.Cells[y2, x2].Value = file.FullName;
        ws2.Cells[y2, x2 + 1].Value = file.Extension;
        ws2.Cells[y2, x2 + 2].Value = file.Length;
        if (!dictOfSize.ContainsKey(file.Extension))
        {
            dictOfSize[file.Extension] = file.Length;
            dictOfCount[file.Extension] = 1;
        }
        else
        {
            dictOfSize[file.Extension] += file.Length;
            dictOfCount[file.Extension] += 1;
        }
        y2++;
        if (y2 > 11)
        {
            break;
        }
    }

    int startY = y2;
    foreach (var info in dictOfSize)
    {
        ws2.Cells[y2, x2].Value = info.Key;
        ws2.Cells[y2, x2 + 1].Value = info.Value;
        ws2.Cells[y2, x2 + 2].Value = dictOfCount[info.Key];
        y2++;
    }

    var chart = (ws2.Drawings.AddChart("PieChart", eChartType.Pie3D) as ExcelPieChart);
    chart.Title.Text = "% roszerzeń wg rozmiaru";
    chart.SetPosition(1, 5, 5, 5);
    chart.SetSize(600, 300);
    ExcelAddress valAdd = new ExcelAddress(startY, 2, y2 - 1, 2);
    var ser = (chart.Series.Add(valAdd.Address, "A" + startY + ":A" + (y2 - 1)) as ExcelPieChartSerie);

    chart.DataLabel.ShowCategory = true;
    chart.DataLabel.ShowPercent = true;
    chart.Legend.Border.LineStyle = eLineStyle.Solid;
    chart.Legend.Border.Fill.Style = eFillStyle.SolidFill;
    chart.Legend.Border.Fill.Color = Color.DarkBlue;

    var chart2 = (ws2.Drawings.AddChart("PieChartDos", eChartType.Pie3D) as ExcelPieChart);
    chart2.Title.Text = "% roszerzeń ilosciowo";
    chart2.SetPosition(17, 5, 5, 5);
    chart2.SetSize(600, 300);
    ExcelAddress valAdd2 = new ExcelAddress(startY, 3, y2 - 1, 3);
    var ser2 = (chart2.Series.Add(valAdd2.Address, "A" + startY + ":A" + (y2 - 1)) as ExcelPieChartSerie);

    chart2.DataLabel.ShowCategory = true;
    chart2.DataLabel.ShowPercent = true;
    chart2.Legend.Border.LineStyle = eLineStyle.Solid;
    chart2.Legend.Border.Fill.Style = eFillStyle.SolidFill;
    chart2.Legend.Border.Fill.Color = Color.AliceBlue;


    if (File.Exists(p_strPath))
        File.Delete(p_strPath);
 
    FileStream objFileStrm = File.Create(p_strPath);
    objFileStrm.Close();

    File.WriteAllBytes(p_strPath, ep.GetAsByteArray());
    Console.WriteLine("Finished Saving file to:\n"+ p_strPath);
}

string outputFileName = "lab3";
string search_Path = @"c:\Users\chudy\source\repos\DOT_NET";

CreateExcel(search_Path, outputFileName,3);

while(true)
{
    Console.WriteLine("type the path to folder to search");
    string path = Console.ReadLine();
    Console.WriteLine("type the depth of search");
    string depthString = Console.ReadLine();
    int depth=0;
    try
    {
        depth = int.Parse(depthString);
    }
    catch
    {
        Console.WriteLine("error reading depth try again");
        continue;
    }
    Console.WriteLine("type name of the output excel file (no extension only name)");
    string outputName = Console.ReadLine();
    CreateExcel(path, outputName, depth);
}
