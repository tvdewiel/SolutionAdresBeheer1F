using StreetNamesBL.Exceptions;
using StreetNamesBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StreetNamesBL.Managers
{
    public class FileManager
    {
        private IFileProcessor processor;

        public FileManager(IFileProcessor processor)
        {
            this.processor = processor;
        }

        public void CheckZipFile(string zipFileName, List<string> fileNames)
        {
            try
            {
                Dictionary<string, string> errors = new();
                Dictionary<string, string> map = new();
                List<string> configEntries = new List<string>()
                {
                    "streetNames",
                    "municipalityNames",
                    "link_StreetName_Municipality",
                    "link_Province_MunicipalityNames",
                    "provinces"
                };
                if (!fileNames.Contains("FileNamesConfig.txt".ToLower()))
                    throw new ZipFileManagerException("FileNamesConfig.txt is missing");
                List<string> data = processor.GetFileNamesConfigInfoFromZip(zipFileName,"FileNamesConfig.txt");
                foreach(var line in data)
                {
                    string[] parts = line.Split(':');
                    map.Add(parts[0].Trim(), parts[1].Trim().Replace('\"'.ToString(),string.Empty).ToLower());
                }
                foreach(string entry in configEntries)
                {
                    if (!map.ContainsKey(entry)) errors.Add(entry, "missing in config");
                }
                foreach(string file in map.Values)
                {
                    if (!fileNames.Contains(file)) errors.Add(file, "missing in zip");
                }
                if (errors.Count > 0)
                {
                    ZipFileManagerException ex = new ZipFileManagerException("Files Missing");
                    foreach(var entry in errors)
                    {
                        ex.Data.Add(entry.Key,entry.Value);
                    }
                    throw ex;
                }
            }
            catch(ZipFileManagerException) { throw; }
            catch(Exception ex) { throw new FileManagerException("CheckZipFile", ex); }
        }

        public List<string> GetFilesFromZip(string fileName)
        {
            try
            {
                List<string> names = processor.GetFileNamesFromZip(fileName);
                return names;
            }
            catch(Exception ex) { throw new FileManagerException($"GetFilesFromZip - {ex.Message}", ex); }
        }
    }
}
