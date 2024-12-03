using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<int> left= new List<int>();
    List<int> right = new List<int>();

    foreach (string line in System.IO.File.ReadLines(data))
    {
        List<int> list = new List<int>();
        string[] slist = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        {
            left.Add(int.Parse(slist[0]));
            right.Add(int.Parse(slist[1]));
        }
    }
    left.Sort();
    right.Sort();
    for (int i = 0; i < left.Count; i++)
    {
        result += int.Abs(left[i] - right[i]);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
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
