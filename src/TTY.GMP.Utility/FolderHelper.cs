using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 文件目录帮助类
    /// </summary>
    public class FolderHelper
    {
        /// <summary>
        /// 清空目录
        /// 如果目录不存在则创建目录
        /// </summary>
        /// <param name="dir"></param>
        public static void ClearFolder(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var fileinfo = new DirectoryInfo(dir).GetFileSystemInfos();
            foreach (var file in fileinfo)
            {
                if (file is DirectoryInfo)
                {
                    new DirectoryInfo(file.FullName).Delete(true);
                }
                else
                {
                    File.Delete(file.FullName);
                }
            }
        }
    }
}
