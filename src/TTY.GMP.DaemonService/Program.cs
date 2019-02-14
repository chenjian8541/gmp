using System;
using TTY.GMP.LOG;

namespace TTY.GMP.DaemonService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("正在启动消费守护程序...");
                ServiceProvider.Process();
                Console.WriteLine("启动成功,...");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, typeof(Program));
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
