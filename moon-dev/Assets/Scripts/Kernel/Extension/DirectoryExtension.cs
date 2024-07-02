using System.Collections.Generic;
using System.IO;

namespace Moon.Kernel.Extension
{
    public static class DirectoryExtension
    {
        /// <summary>
        ///     Move files across disks
        /// </summary>
        /// <param name="source">The file that was moved </param>
        /// <param name="target">Destination address </param>
        /// <returns> Whether the operation was successful</returns>
        public static bool MoveSpanningDisk(string source, string target)
        {
            if (!Directory.Exists(source))
            {
                return false;
            }

            if (Directory.Exists(target))
            {
                return false;
            }

            var sourceInfo = Directory.CreateDirectory(source);
            var targetInfo = Directory.CreateDirectory(target);

            if (sourceInfo.FullName == targetInfo.FullName)
            {
                return false;
            }

            var sourceDirectories = new Stack<DirectoryInfo>();
            sourceDirectories.Push(sourceInfo);

            var targetDirectories = new Stack<DirectoryInfo>();
            targetDirectories.Push(targetInfo);

            while (sourceDirectories.Count > 0)
            {
                var sourceDirectory = sourceDirectories.Pop();
                var targetDirectory = targetDirectories.Pop();

                foreach (var file in sourceDirectory.GetFiles()) file.CopyTo(Path.Combine(targetDirectory.FullName, file.Name), true);

                foreach (var subDirectory in sourceDirectory.GetDirectories())
                {
                    sourceDirectories.Push(subDirectory);
                    targetDirectories.Push(targetDirectory.CreateSubdirectory(subDirectory.Name));
                }
            }

            sourceInfo.Delete(true);

            return true;
        }
    }
}