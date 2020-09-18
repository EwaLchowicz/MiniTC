using System.Collections.Generic;
using System.Linq;
using System.IO;
using MiniTC.Properties;

namespace MiniTC.Model
{
    class Panel
    {
        public List<string> drives { get; set; }
        public List<string> cont { get; set; }

        public Panel()
        {
            UpdateDrives();
            cont = new List<string>();
        }

        public void UpdateContent(string path)
        {
 
            List<string> directories = new List<string>();
            List<string> files = new List<string>();

            directories = (Directory.GetDirectories(path).ToList<string>());
            for (int i = 0; i < directories.Count; i++)
            {
                directories[i] = directories[i].Insert(0, Resources.DirectorySign);
            }

            files = (Directory.GetFiles(path).ToList<string>());
            cont = directories;
            cont.AddRange(files);
        }

        public void UpdateDrives()
        {
            this.drives = Directory.GetLogicalDrives().ToList<string>();
        }


    }
}
