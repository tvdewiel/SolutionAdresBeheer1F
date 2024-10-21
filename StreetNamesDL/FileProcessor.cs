using StreetNamesBL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetNamesDL
{
    public class FileProcessor : IFileProcessor
    {
        public List<string> GetFileNamesConfigInfoFromZip(string zipFileName, string configName)
        {
            using(ZipArchive archive=ZipFile.OpenRead(zipFileName)) 
            { 
                var entry=archive.GetEntry(configName);
                if (entry != null)
                {
                    List<string> data = new();
                    using (Stream entryStream = entry.Open())
                    using (StreamReader reader = new StreamReader(entryStream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            data.Add(line);
                        return data;
                    }
                }
                else throw new FileNotFoundException($"{configName} not found");
            }
        }

        public List<string> GetFileNamesFromZip(string fileName)
        {
            if (!File.Exists(fileName)) { throw new FileNotFoundException($"{fileName} not found"); }
            using(var zipFile=ZipFile.OpenRead(fileName))
            {
                return zipFile.Entries.Select(x=>x.FullName.ToLower()).ToList();
            }
        }
    }
}
