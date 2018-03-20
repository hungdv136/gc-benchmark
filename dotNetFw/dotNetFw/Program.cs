using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace dotNetFw
{
    class Program
    {
        private const int WindowSize = 200000;
        private const int MsgCount = 10000000;
        private const int MsgSize = 1024;

        private static byte[] CreateMessage(int n)
        {
            return Enumerable.Repeat((byte)n, MsgSize).ToArray();
        }

        private static void PushMessage(Dictionary<int, byte[]> map, int id)
        {
            var lowId = id - WindowSize;
            map.Add(id, CreateMessage(id));
            if (lowId >= 0)
            {
                map.Remove(lowId);
            }
        }

        static void Main(string[] args)
        {
            var worst = new TimeSpan();
            var map = new Dictionary<int, byte[]>();
            for (var i = 0; i < MsgCount; i++)
            {
                var sw = Stopwatch.StartNew();
                PushMessage(map, i);
                sw.Stop();
                if (sw.Elapsed > worst)
                {
                    worst = sw.Elapsed;
                }
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine($"Worst push time : {worst.TotalMilliseconds} ms");
            Console.ReadKey();
        }

    }
}
