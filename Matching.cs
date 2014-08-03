using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using SourceAFIS.Simple;
using System.IO;

namespace TestAPIFingerprint
{
    class Matching
    {
        static AfisEngine AFIS = new AfisEngine();

        static void Main(string[] args)
        {
            Person queryPerson = null;
            //  String path = "E:/College/Semester 7/Tentang TA/Oprek FVC/Template/FVC2000/Db1_a/FVC2000-Db1_a_1_1.template";
            String path = "E:/College/Semester 7/Tentang TA/Oprek FVC/Template/Summary/FVC2000-Db1_a_1_1.template";
            queryPerson = SetupQueryPerson(queryPerson,path);

            Fingerprint fp = new Fingerprint();
            Person dataPerson = new Person(fp);
            float verify;
            

            String fvc = "FVC2000";
            String db = "Db1_a";
            String prefix = fvc + "-" + db;
            //String pathTemplate = "E:/College/Semester 7/Tentang TA/Oprek FVC/Template/" + fvc + "/" + db;
            String pathTemplate = "E:/College/Semester 7/Tentang TA/Oprek FVC/Template/Summary";
            String[] files = Directory.GetFiles(pathTemplate);

            int counter = 0;
            foreach(String uriTmp in files){
                if (!Path.GetFileNameWithoutExtension(uriTmp).Equals("properties"))
                {
                    fp.AsIsoTemplate = File.ReadAllBytes(uriTmp);
                    verify = AFIS.Verify(queryPerson, dataPerson);
                    if(verify!=0)
                        File.AppendAllText("c:/infoPenting",Path.GetFileName(uriTmp) + "\t" + verify+"\n");
                }
                Console.WriteLine("Progress = "+((float) ++counter/files.Length)*100 +" %");
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static Person SetupQueryPerson(Person queryPerson,String path)
        {
            Fingerprint fp = new Fingerprint();
            fp.AsIsoTemplate = File.ReadAllBytes(path);
            return new Person(fp);
        }
    }
}