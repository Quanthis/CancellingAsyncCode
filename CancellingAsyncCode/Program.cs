using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Numerics;

namespace CancellingAsyncCode
{
    class Program
    {
        static async Task<int> CancellableAsyncMethod(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    WriteLine("Still working... ");
                }
                return 0;
            });
        }

        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            CancellableAsyncMethod(token);

            cts.CancelAfter(TimeSpan.FromSeconds(5));           //now its way more accurate

            /*for(ushort i = 0; i <= 10; i++)
            {
                WriteLine("i == " + i);
                Thread.Sleep(1000);
                if(i == 5)
                {
                    cts.Cancel();          //method with this token actually stops, but 2 seceonds after it is cancelled
                }
            }*/
            
            ReadKey();
        }
    }
}
