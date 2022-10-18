using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CToolkitCs.v1_2.Compress
{
    public class CtkCompressUtil
    {

        public static void ExtractEntries(ZipArchive archive, string zipPath, string destPath)
        {

            var query = from row in archive.Entries
                        where row.FullName.StartsWith(zipPath)
                        select row;

            foreach (var entry in query)
            {
                if (string.IsNullOrEmpty(entry.Name)) continue;
                var path = entry.FullName.Replace(zipPath, "");
                path = Path.Combine(destPath, path);
                var fi = new FileInfo(path);
                if (!fi.Directory.Exists) fi.Directory.Create();
                entry.ExtractToFile(fi.FullName, true);
            }
        }

        public static void ExtractEntry(ZipArchive archive, string zipFile, string destFolder)
        {

            var query = from row in archive.Entries
                        where row.FullName.StartsWith(zipFile)
                        select row;

            var entry = query.FirstOrDefault();
            if (entry == null) return;
            if (string.IsNullOrEmpty(entry.Name)) return;

            var path = Path.Combine(destFolder, entry.Name);
            var fi = new FileInfo(path);
            if (!fi.Directory.Exists) fi.Directory.Create();
            entry.ExtractToFile(fi.FullName, true);


        }
    }
}
