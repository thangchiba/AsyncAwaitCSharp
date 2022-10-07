using System.Text;
using AsyncAwait;

public class Program
{
    static int loopTime = 1000000;
    private static async Task Main(string[] args)
    {
        long TimeFinishSync = await TestSync();
        Console.WriteLine("Sync : " + TimeFinishSync);
        long TimeFinishASync = await TestASync();
        Console.WriteLine("ASync : " + TimeFinishASync);
        long TimeFinishASyncParallel = await TestASyncParallel();
        Console.WriteLine("ASyncParallel : " + TimeFinishASyncParallel);
        Console.ReadKey();
    }

    static async Task<long> TestSync()
    {
        TestAsyncAwait test = new TestAsyncAwait(loopTime);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        string res = test.DoSync();
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
    static async Task<long> TestASync()
    {
        TestAsyncAwait test = new TestAsyncAwait(loopTime);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        string res = await test.DoAsync();
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
    static async Task<long> TestASyncParallel()
    {
        TestAsyncAwait test = new TestAsyncAwait(loopTime);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        string res = await test.DoAsyncParallel();
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}