// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
Item item = await ReadAsync<Item>(@".\data.txt");
StringBuilder builder = new();
builder.AppendLine("The following arguments are passed:");

// Display the command line arguments using the args variable.
foreach (var arg in args)
{
    builder.AppendLine($"Argument={arg}");
}

Console.WriteLine(item.item.Constains(new SubItem{RouteId=107}));
Console.WriteLine(builder.ToString());


// Return a success code.
return 0;

 async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
  };
public class SubItem : IEquatable<SubItem>
    {
        public int RouteId =0;
        public int agancyId =0;
        public string routeShortName="";
        public string routeLongName="";
        public string activationDate="";
        public string routeType="";
		public bool Equals(SubItem other)
    {
        if (other == null) return false;
        return (this.RouteId.Equals(other.RouteId));
    }

    };
	
	public class Item
    {
        public SubItem item;
    };


