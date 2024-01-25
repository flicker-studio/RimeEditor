using System.Collections.Generic;
using System.IO;
namespace Moon.Kernel.Extension
{
    public static class File
    {
        public static bool Move(string source, string target)
        {
            if (!Directory.Exists(source))
            {
                return false;
            }
    
            if (Directory.Exists(target))
            {
                return false;
            }
    
            DirectoryInfo sourceInfo = Directory.CreateDirectory(source);
            DirectoryInfo targetInfo = Directory.CreateDirectory(target);
    
            if (sourceInfo.FullName == targetInfo.FullName)
            {
                return false;
            }
    
            Stack<DirectoryInfo> sourceDirectories = new Stack<DirectoryInfo>();
            sourceDirectories.Push(sourceInfo);
    
            Stack<DirectoryInfo> targetDirectories = new Stack<DirectoryInfo>();
            targetDirectories.Push(targetInfo);
    
            while (sourceDirectories.Count > 0)
            {
                DirectoryInfo sourceDirectory = sourceDirectories.Pop();
                DirectoryInfo targetDirectory = targetDirectories.Pop();
    
                foreach (FileInfo file in sourceDirectory.GetFiles())
                {
                    file.CopyTo(Path.Combine(targetDirectory.FullName, file.Name), overwrite: true);
                }
    
                foreach(DirectoryInfo subDirectory in sourceDirectory.GetDirectories())
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
