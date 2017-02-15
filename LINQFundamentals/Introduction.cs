using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQFundamentals
{
    class Introduction
    {
        static void Main(string[] args)
        {

            string path = @"c:\windows";
            SampleWithoutLinQ(path);
            Console.WriteLine("******************************");
            SampleWithLinQ(path);

            Console.ReadLine();

        }

        private static void SampleWithLinQ(string path)
        {
            //Query syntax
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;
            foreach (FileInfo file in query.Take(5))
            {
                //c#6.0 string interoplation
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
            //Method syntax
            Console.WriteLine("******Method Syntax*********");
            var methodQuery = new DirectoryInfo(path).GetFiles()
                                .OrderByDescending(f => f.Length)
                                .Take(5);
            foreach (FileInfo file in methodQuery)
            {
                //c#6.0 string interoplation
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
        }

        private static void SampleWithoutLinQ(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            Array.Sort(files, new FileInfoComparer());
            /*
            foreach (FileInfo file in files)
            {
                //c#6.0 string interoplation
                Console.WriteLine($"{file.Name} : {file.Length}");
            }
            */
            for (var i = 0; i < 5; i++)
            {
                FileInfo fileInfo = files[i];
                //left justify name to 20 places and right justfy lenght and format it as number with 0 after decimal
                Console.WriteLine($"{fileInfo.Name, -20} : {fileInfo.Length, 10:N0}");

            }
        }
    }
    class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
    