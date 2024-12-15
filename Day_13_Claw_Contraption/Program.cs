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
