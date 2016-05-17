using System;
using System.IO;

namespace Koopakiller.ForumSamples.OpenXml.PowerPoint
{
    public class ZipFile
    {
        public static string Extract(string file)
        {
            var tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), new FileInfo(file).Name);
            var zip = new Ionic.Zip.ZipFile(file);
            zip.ExtractAll(tmp);
            return tmp;
        }

        public static void Zip(string path, string file)
        {
            using (var loanZip = new Ionic.Zip.ZipFile())
            {
                loanZip.AddDirectory(path);
                loanZip.Save(file);
            }
        }

    }
}