using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Selfhost_Course_ppedv
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:6666";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}
