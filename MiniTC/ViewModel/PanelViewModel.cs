using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using MiniTC.Properties;
using MiniTC.ViewModel.Base;

namespace MiniTC.ViewModel
{
    using Model;
    class PanelViewModel : ViewModelBase
    {
        private Panel panel = new Panel();
        private List<string> content;
        private List<string> alldrives;
        private string currentDrive;
        private string currentPath;
        private string selectedFile;

        public PanelViewModel()
        {
            this.content = new List<string>();
            this.alldrives = panel.drives;
            this.currentPath = "";
            this.currentDrive = "";
            this.selectedFile = null;
        }

        public List<string> Content
        {
            get => this.content;
            set
            {
                this.content = value;
                this.OnPropertyChanged();
            }
        }
        public List<string> Alldrives
        {
            get => this.alldrives;
            set
            {
                this.alldrives = value;
                this.OnPropertyChanged();
            }
        }

        public string CurrentPath
        {
            get => this.currentPath;
            set
            {
                this.currentPath = value;
                this.OnPropertyChanged();
            }
        }
        public string CurrentDrive
        {
            get => this.currentDrive;
            set
            {
                
                this.currentDrive = value;
                this.CurrentPath = value;
                this.GetContent();
                this.OnPropertyChanged();
            }
        }
        public string SelectedFile
        {
            get=> this.selectedFile;
            set
            {
                this.selectedFile = value;
                Search();
                this.OnPropertyChanged();
            }
        }

        public void GetContent()
        {
            bool root = false;
            try
            {
                panel.UpdateContent(CurrentPath);
            }
            catch (UnauthorizedAccessException)
            {
                this.SelectedFile = null;
            }

            foreach (var drv in Alldrives)
            {
                if (this.currentPath == drv)
                {
                    root = true;
                }
            }

            if (!root)
            {
                List<string> list = panel.cont;
                list.Insert(0, Resources.GoBack);
                this.Content = list;
                this.SelectedFile = null;
            }
            else
            {
                this.Content = panel.cont;
            }
        }

        public void Search()
        {
            if (string.IsNullOrEmpty(SelectedFile))
            {
                return;
            }
            else if (SelectedFile.Contains(Resources.DirectorySign))
            {
                this.CurrentPath = selectedFile.Substring(3);
                this.selectedFile=null;
                GetContent();
            }
            else if (SelectedFile == Resources.GoBack)
            {
                CurrentPath = Directory.GetParent(currentPath).ToString();
                this.selectedFile = null;
                GetContent();
            }
            else this.CurrentPath = SelectedFile;
            
        }

       


    }

}
