//int loopCount = 100000000;
//List<int> listInt = new List<int>(loopCount);
//int[] arrayInt = new int[loopCount];


//var watch = System.Diagnostics.Stopwatch.StartNew();
//for (int i = 0; i < loopCount; i++)
//{
//    arrayInt[i] = i;
//}
//foreach (int n in arrayInt)
//{
//    int a = n * 2 * 3 * 4 * 5;
//}
//watch.Stop();
//Console.WriteLine(watch.ElapsedMilliseconds);

//var watch2 = System.Diagnostics.Stopwatch.StartNew();
//for (int i = 0; i < loopCount; i++)
//{
//    listInt.Add(i);
//}
//foreach (int n in listInt)
//{
//    int a = n * 2 * 3 * 4 * 5;
//}
//watch2.Stop();
//Console.WriteLine(watch2.ElapsedMilliseconds);
//Console.ReadKey();

int count = 0;
for (int i = 0; i < 1000; i++)
{
    Thread t = new Thread(() =>
    {
        count++;
        while (true)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
    });
    t.IsBackground = true;
    t.Start();
}

Console.WriteLine(count);
Console.ReadKey();