using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using SourceAFIS.Simple;
using System.IO;
using System.Diagnostics;

namespace TestAPIFingerprint
{
    class Extractor
    {
        static AfisEngine AFIS = new AfisEngine();

        static void Main(string[] args)
        {
            String fvc = "FVC2006";
            String db = "Db1_a"; // 1
            String prefix = fvc + "-" + db;
            String pathImage = "E:/College/Semester 7/Tentang TA/Oprek FVC/DVD/"+fvc+"/Dbs/"+db;
            String pathIso = "E:/College/Semester 7/Tentang TA/Oprek FVC/DVD/"+fvc+"/Dbs/Temp-"+db;
            Directory.CreateDirectory(pathIso);
            String[] files = Directory.GetFiles(pathImage);
            int i = 0;
            Stopwatch watch = Stopwatch.StartNew();
            foreach(String uriFp in files){
                try
                {
                    Fingerprint fp = new Fingerprint();
                    fp.AsBitmapSource = new BitmapImage(new Uri(uriFp, UriKind.RelativeOrAbsolute));
                    Person ps = new Person();
                    ps.Fingerprints.Add(fp);
                    AFIS.Extract(ps);
                    File.WriteAllBytes(pathIso + "/" + prefix + "_"+ Path.GetFileNameWithoutExtension(uriFp)+".template", fp.AsIsoTemplate);
                    Console.WriteLine("Progress : "+ (((float) (++i)/files.Length) * 100)+" %");
                }catch(Exception ex){
                    Console.WriteLine(" Skip!!"+ex.Message);
                }
            }
            watch.Stop();
            String [] lines = new String[1];
            lines[0] = "Time : " + watch.ElapsedMilliseconds + " ms";
            File.AppendAllLines(pathIso + "/properties.txt", lines);
            Console.WriteLine("Time : "+ watch.ElapsedMilliseconds + " ms");
            Console.WriteLine("Done");
        }
    }
}
