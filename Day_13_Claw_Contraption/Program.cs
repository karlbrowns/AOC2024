using System.Text.RegularExpressions;
using System.Diagnostics;

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
    char[] split = { ',', '+' };
    char[] split2 = {'=', ','};
    List<List<int>> abtn = new List<List<int>>();
    List<List<int>> bbtn = new List<List<int>>();
    List<List<int>> results = new List<List<int>>();
    List<string> input = read_input(data);
    for (int i=0; i<input.Count; i++)
    {
        if ((i%4)==0)
        {
            abtn.Add(new List<int>());
            string[] nums = input[i].Split(split,StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(nums[1]);
            int y = int.Parse(nums[3]);
            abtn[index].Add(x);
            abtn[index].Add(y);
        }
        if ((i % 4) == 1)
        {
            bbtn.Add(new List<int>());
            string[] nums = input[i].Split(split, StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(nums[1]);
            int y = int.Parse(nums[3]);
            bbtn[index].Add(x);
            bbtn[index].Add(y);
        }
        if ((i % 4) == 2)
        {
            results.Add(new List<int>());
            string[] nums = input[i].Split(split2, StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(nums[1]);
            int y = int.Parse(nums[3]);
            results[index].Add(x);
            results[index].Add(y);
        }
        if ((i % 4) == 3) index++;
    }
    int xend, yend, diffx, diffy;
    List<List<int>> possibles= new List<List<int>>();
    index = 0;
    for (int i=0; i<results.Count; i++)
    {
        possibles.Clear();
        index = 0;
        int apress = 0; int bpress = 0;
        xend = results[i][0];
        yend = results[i][1];
        apress = xend / abtn[i][0];
        while ((apress>0) && (apress * abtn[i][1] > yend)) apress--;
        while (apress >= 0) {
            diffx = xend - apress * abtn[i][0];
            diffy = yend - apress * abtn[i][1];
            if ((diffx % bbtn[i][0] == 0))
            {
                bpress = diffx / bbtn[i][0];
                if (bpress * bbtn[i][1] == diffy)
                {
                    possibles.Add(new List<int>());
                    possibles[index].Add(apress);
                    possibles[index].Add(bpress);
                    index++;
                    apress--;
                }
                else apress--;
            }
            else apress--;
        }
        if (possibles.Count > 0) {
            int test = possibles[0][0] * 3 + possibles[0][1];
            for (int j = 1; j < possibles.Count; j++)
            {
                if (possibles[j][0] * 3 + possibles[j][1] < test)
                {
                    test = possibles[j][0] * 3 + possibles[j][1];
                }
            }
            result += test;
        }
        if (possibles.Count > 1) Console.WriteLine("Possibles: " + possibles.Count);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    Int128 result = 0;
    int index = 0;
    String data = "input.txt";
    char[] split = { ',', '+' };
    char[] split2 = { '=', ',' };
    List<List<Int128>> abtn = new List<List<Int128>>();
    List<List<Int128>> bbtn = new List<List<Int128>>();
    List<List<Int128>> results = new List<List<Int128>>();
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
        if ((i % 4) == 0)
        {
            abtn.Add(new List<Int128>());
            string[] nums = input[i].Split(split, StringSplitOptions.RemoveEmptyEntries);
            Int64 x = Int64.Parse(nums[1]);
            Int64 y = Int64.Parse(nums[3]);
            abtn[index].Add(x);
            abtn[index].Add(y);
        }
        if ((i % 4) == 1)
        {
            bbtn.Add(new List<Int128>());
            string[] nums = input[i].Split(split, StringSplitOptions.RemoveEmptyEntries);
            Int64 x = Int64.Parse(nums[1]);
            Int64 y = Int64.Parse(nums[3]);
            bbtn[index].Add(x);
            bbtn[index].Add(y);
        }
        if ((i % 4) == 2)
        {
            results.Add(new List<Int128>());
            string[] nums = input[i].Split(split2, StringSplitOptions.RemoveEmptyEntries);
            Int64 x = Int64.Parse(nums[1]);
            Int64 y = Int64.Parse(nums[3]);
            results[index].Add(10000000000000 + x);
            results[index].Add(10000000000000 + y);
        }
        if ((i % 4) == 3) index++;
    }
    Int128 rx, ry, diffx, diffy, ajump=1, bjump=1;
    List<List<Int128>> possibles = new List<List<Int128>>();
    index = 0;
    for (int i = 0; i < results.Count; i++)
    {
        possibles.Clear();
        index = 0;
        Console.WriteLine(i);
        Int128 apress = 0; Int128 bpress = 0;
        
        rx = results[i][0];
        ry = results[i][1];
        Int128 x1, y1, x2, y2;
        x1 = abtn[i][0];
        x2 = bbtn[i][0];
        y1 = abtn[i][1];
        y2 = bbtn[i][1];
        Int128 dividend = x1 * ry - y1 * rx;
        Int128 divisor = y2 * x1 - x2 * y1;
        if ((dividend % divisor)==0)
        {
            bpress = dividend / divisor;
            if (bpress > 0)
            {
                apress = (rx - bpress * x2);
                if ((apress % x1) == 0)
                {
                    apress = apress / x1;
                    if (apress > 0)
                    {
                        possibles.Add(new List<Int128> { apress, bpress });
                    }
                }
            }
        }
        if (possibles.Count > 0)
        {
            Int128 test = possibles[0][0] * 3 + possibles[0][1];
            for (int j = 1; j < possibles.Count; j++)
            {
                if (possibles[j][0] * 3 + possibles[j][1] < test)
                {
                    test = possibles[j][0] * 3 + possibles[j][1];
                }
            }
            result += test;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}
//134667349543534 too high
Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
