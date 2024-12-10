using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}
// 6259790636215 is too high
// 6259790636215
// 6259790630969
// 6259790630969
void P1()
{
    Int64 result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    Int64[] disk;
    List<int> files = new List<int>();
    List<int> free = new List<int>();
    int disksize = 0;
    int i;
    for ( i=0; i<input[0].Length; i++)
    {
        if ((i & 1)==0)
        {
            files.Add(input[0][i] - 48);
        }
        else free.Add(input[0][i] - 48);
        disksize += input[0][i] - 48;
    }
    disk = new Int64[disksize];
    int diskpos = 0;
    for ( i = 0; i < files.Count; i++)
    {
        for (int j = 0; j < files[i]; j++)
        {
            disk[diskpos++] = i;
        }
        if (i < free.Count)
        {
            for (int j = 0; j < free[i]; j++)
            {
                disk[diskpos++] = -1;
            }
        }
    }
    int firstfree = 0;
    for ( i=disksize-1; i>firstfree; i--)
    {
        if (disk[i] == 5246)
            Console.WriteLine("Break here");
        while ((firstfree < disk.Length) && (disk[firstfree] != -1)) firstfree++;
        while (disk[i] == -1) i--;
        if (i > firstfree)
        {
            disk[firstfree++] = disk[i];
            disk[i] = -1;
        }
    }
    Console.WriteLine(i + "," + firstfree);
    for ( i=0; i<disksize; i++)
    {
        if (disk[i]>=0)
        {
            result += disk[i] * (Int64)i;
        }
    }
    //for ( i=0; i<disksize; i++)
    //{
    //    if (disk[i] >= 0) Console.Write(disk[i]);
    //    else Console.Write(".");
    //}
    Console.WriteLine();
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    Int64 result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    Int64[] disk;
    List<int> files = new List<int>();
    List<int> free = new List<int>();
    int disksize = 0;
    int i,j;
    int[] filepos;
    int[] freepos;
    for (i = 0; i < input[0].Length; i++)
    {
        if ((i & 1) == 0)
        {
            files.Add(input[0][i] - 48);
        }
        else free.Add(input[0][i] - 48);
        disksize += input[0][i] - 48;
    }
    disk = new Int64[disksize];
    filepos = new int[files.Count];
    freepos = new int[files.Count];
    int diskpos = 0;
    int lastpos = 0;
    for (i = 0; i < files.Count; i++)
    {
        filepos[i] = diskpos;
        for ( j = 0; j < files[i]; j++)
        {
            disk[diskpos++] = i;
            lastpos = diskpos;
        }
        if (i < free.Count)
        {
            freepos[i] = diskpos;
            for ( j = 0; j < free[i]; j++)
            {
                disk[diskpos++] = -1;
            }
        }
    }
    //freepos[i] = diskpos;
    int freep = 0;
    int k;
    for (i=files.Count-1; i>=0; i--)
    {
        int length = files[i];
        int curpos = filepos[i];
        bool foundfree = false;
        j = 0;
        while ((!foundfree) && (j < disksize))
        {
            for ( ; j < disksize; j++)
            {
                if (disk[j] == -1) break;
            }
            if ((j < disksize) && (j + length < disksize))
            {
                for (k = j; k < j + length; k++)
                {
                    if (disk[k] != -1)
                    {
                        foundfree = false;
                        break;
                    }
                }
                if ((k == j + length) && (j < curpos))
                {
                    foundfree = true;
                    for (int l = 0; l < length; l++)
                    {
                        disk[l + j] = i;
                        disk[l + curpos] = -1;
                    }
                }
                else j = k;
            }
            else j++;

        }
    }
    for (i = 0; i < disksize; i++)
    {
        if (disk[i] >= 0)
        {
            result += disk[i] * (Int64)i;
        }
    }
    for (i = 0; i < disksize; i++)
    {
        if (disk[i] >= 0) Console.Write(disk[i]);
        else Console.Write(".");
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
