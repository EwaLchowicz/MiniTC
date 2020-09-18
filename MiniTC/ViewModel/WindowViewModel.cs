using System;
using System.Linq;
using System.Windows.Input;
using System.IO;
using System.Windows;
using MiniTC.ViewModel.Base;


namespace MiniTC.ViewModel
{
    using Model;
    class WindowViewModel
    {
        public PanelViewModel LeftPanel { get; set; }
        public PanelViewModel RightPanel { get; set; } 

        public WindowViewModel()
        {
            LeftPanel = new PanelViewModel();
            RightPanel = new PanelViewModel();
        }

        private ICommand _copy = null;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(
                        x =>
                        {
                            string file = LeftPanel.SelectedFile.Substring(LeftPanel.SelectedFile.LastIndexOf(Path.DirectorySeparatorChar));
                            string start = LeftPanel.SelectedFile;
                            string end = "";

                            if (!string.IsNullOrEmpty(RightPanel.SelectedFile))
                            {
                                end = RightPanel.CurrentPath.Remove(RightPanel.CurrentPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                            }
                            else
                            {
                                end = RightPanel.CurrentPath;
                            }

                            string destination = end + file;

                            try
                            {
                                File.Copy(@start, @destination);
                                RightPanel.GetContent();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Error "+e.Message);
                            }
                        },
                        x => !string.IsNullOrEmpty(LeftPanel.SelectedFile) && !LeftPanel.SelectedFile.Contains("<D>") && !string.IsNullOrEmpty(RightPanel.CurrentDrive)
                        );
                }
                return _copy;
            }
        }
        

    }
}
