using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetNamesBL.Interfaces
{
    public interface IFileProcessor
    {
        List<string> GetFileNamesConfigInfoFromZip(string zipFileName, string v);
        List<string> GetFileNamesFromZip(string fileName);
    }
}
