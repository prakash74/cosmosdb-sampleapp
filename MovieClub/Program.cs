using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieDocumentsClub
{
    public static class Program
    { 
        public static void Main()
        {
            Demo demo = new Demo();
            for (int i = 0; i < 2000; i++)
            {
                demo.RunDemo(i);
                Thread.Sleep(15000);
            }
        }
    }
}
