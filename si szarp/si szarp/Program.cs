using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
class MyComparator : IComparer<string>
{
    public int Compare(string x, string y)
    {
        if (x.Length > y.Length)
            return 0;
        else if(y.Length>x.Length)
            return 1;
        else
        {
            return x.CompareTo(y);
        }
    }
}
class program
{
    static void SerialajzDeserjalaize(DirectoryInfo rootFolder)
    {
        SortedDictionary<string, int> dictionary = new SortedDictionary<string, int>(new MyComparator());
        foreach (var file in rootFolder.GetFiles())
        {
            dictionary.Add(file.Name, (int)file.Length);
        }
        foreach (var subdir in rootFolder.GetDirectories())
        {
            dictionary.Add(subdir.Name, (subdir.GetFiles().Length + subdir.GetDirectories().Length));
        }


        FileStream fs = new FileStream("FileToSerialize", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, dictionary);
        fs.Flush();
        fs.Close();
        fs.Dispose();

        BinaryFormatter formatter2 = new BinaryFormatter(); 
        FileStream fs2 = File.Open("FileToSerialize", FileMode.Open);

        SortedDictionary<string, int> deSerializedDictionary = new SortedDictionary<string, int>(new MyComparator());

        object obj = formatter2.Deserialize(fs2);
        deSerializedDictionary = (SortedDictionary<string, int>)obj;
        fs2.Flush();
        fs2.Close();
        fs2.Dispose();
        foreach (var element in deSerializedDictionary.Reverse())
        {
            Console.WriteLine("{0} -> {1}", element.Key, element.Value);
        }
    }
    static String Rahs(FileAttributes att2)
    {
        String rahs2 = "";
        if ((att2 & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            rahs2 += "r";
        else
            rahs2 += "-";
        if ((att2 & FileAttributes.Archive) == FileAttributes.Archive)
            rahs2 += "a";
        else
            rahs2 += "-";
        
        if ((att2 & FileAttributes.Hidden) == FileAttributes.Hidden)
            rahs2 += "h";
        else
            rahs2 += "-";
        if ((att2 & FileAttributes.System) == FileAttributes.System)
            rahs2 += "s";
        else
            rahs2 += "-";
        return rahs2;
    }
    static void Wypisywacz(DirectoryInfo folder,int tabs,ref System.DateTime najstarszyPlik)
    {
        DirectoryInfo[] dir = folder.GetDirectories();
        FileInfo[] files = folder.GetFiles();
        
        for (int i = 0; i < tabs ; i++)
        {
            Console.Write("\t");
        }
        int counter = folder.EnumerateDirectories().Count();
        counter += folder.EnumerateFiles().Count();
        FileAttributes att2 = folder.Attributes;
        string rahs2 = Rahs(att2);
        
        Console.WriteLine(folder.Name+" ("+counter+") "+rahs2);
        foreach (DirectoryInfo di in dir)
        {
            if( najstarszyPlik > di.CreationTime)
                najstarszyPlik=di.CreationTime;
            Wypisywacz(di,tabs+1,ref najstarszyPlik);
        }
        foreach (FileInfo file in files)
        {
            
            if (najstarszyPlik > file.CreationTime)
                najstarszyPlik = file.CreationTime;
            for (int i = 0; i < tabs+1; i++)
            {
                Console.Write("\t");
            }
            long size = file.Length;
            FileAttributes att = file.Attributes;
            string rahs=Rahs(att);
            Console.WriteLine(file.Name + " " + file.Length + " bajtow "+rahs);
        }
    }
    static void Main(string[] args)
    {
        DirectoryInfo rootFolder = new DirectoryInfo(@"C:\" + args[0]);
        System.DateTime najstarszyPlik = DateTime.MaxValue;
        Wypisywacz(rootFolder, 0, ref najstarszyPlik);
        Console.WriteLine(najstarszyPlik);
        SerialajzDeserjalaize(rootFolder);
    }
}

