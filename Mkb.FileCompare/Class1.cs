using System;
using System.IO;

namespace Mkb
{
    public static class FileCompare
    {
        private const int BytesToRead = sizeof(long);

        public static bool FilesAreEqual(FileInfo first, FileInfo second, bool fileNameMatch = true)
        {
            if ((fileNameMatch && first.Name != second.Name) || first.Length != second.Length)
            {
                return false;
            }

            if (string.Equals(first.FullName, second.FullName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }


            var iterations = (int) Math.Ceiling((double) first.Length / BytesToRead);

            using (var fs1 = first.OpenRead())
            {
                using (var fs2 = second.OpenRead())
                {
                    var one = new byte[BytesToRead];
                    var two = new byte[BytesToRead];

                    for (var i = 0; i < iterations; i++)
                    {
                        fs1.Read(one, 0, BytesToRead);
                        fs2.Read(two, 0, BytesToRead);

                        if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                            return false;
                    }
                }
            }

            return true;
        }
    }
}