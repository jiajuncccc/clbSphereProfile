using System.IO;

namespace clbExport
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && File.Exists(args[0]))
            {
                SphereProfileTools.ExportConst(args[0]);
            }
        }
    }
}
