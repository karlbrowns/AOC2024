using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using System.Globalization;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<List<int>> map = new List<List<int>>();
    List<List<int>> antinodes = new List<List<int>>();
    Hashtable antennas = new Hashtable();
    for (int i=0; i<input.Count; i++)
    {
        map.Add(new List<int>());
        antinodes.Add(new List<int>());
        for (int j=0; j < input[i].Length; j++)
        {
            if (input[i][j]=='.')
            {
                map[i].Add(0);
            }
            else
            {
                List<int> val = new List<int>();
                val.Add(i); val.Add(j);
                if (antennas.Contains(input[i][j]))
                {
                    List<List<int>> vals;
                    vals = (List<List<int>>)antennas[input[i][j]];
                    vals.Add(val);
                    antennas[input[i][j]] = vals;
                }
                else
                {
                    List<List<int>> vals = new List<List<int>>();
                    vals.Add(val);
                    antennas.Add(input[i][j], vals);
                }
                map[i].Add(input[i][j]);
            }
            antinodes[i].Add(0);
        }
    }
    int x1, y1, x2, y2;
    foreach(DictionaryEntry ant in antennas)
    {
        List<List<int>> antenna = (List<List<int>>)ant.Value;
        for (int i=0; i<antenna.Count-1; i++)
        {
            for (int j=i+1; j<antenna.Count; j++)
            {
                y1 = antenna[i][0];
                x1 = antenna[i][1];
                y2 = antenna[j][0];
                x2 = antenna[j][1];
                int diffx = x2 - x1;
                int diffy = y2 - y1;
                int newx = x1 - diffx;
                int newy = y1 - diffy;
                if ((newx >=0 ) && (newx < antinodes[i].Count) && (newy >=0) && (newy < antinodes.Count))
                    antinodes[newy][newx] = 1;
                newx = x2 + diffx;
                newy = y2 + diffy;
                if ((newx >= 0) && (newx < antinodes[i].Count) && (newy >= 0) && (newy < antinodes.Count))
                    antinodes[newy][newx] = 1;
            }
        }
    }
    for (int i=0; i < antinodes.Count; i++)
    {
        for (int j=0; j < antinodes[i].Count; j++)
        {
            if (antinodes[i][j] == 1) result++;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
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
