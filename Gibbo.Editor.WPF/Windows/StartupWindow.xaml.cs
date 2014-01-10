﻿#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Net;
using System.Deployment.Application;
using Gibbo.Library;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for StartupForm.xaml
    /// </summary>
    public partial class StartupForm : Window
    {
        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        private const int GWL_STYLE = -16;

        private const uint WS_SYSMENU = 0x80000;

        protected override void OnSourceInitialized(EventArgs e)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE,
                GetWindowLong(hwnd, GWL_STYLE) & (0xFFFFFFFF ^ WS_SYSMENU));

            base.OnSourceInitialized(e);
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        } 

        #region constructors

        public StartupForm()
        {
            //new SplashWindow().ShowDialog();

            while (Gibbo.Editor.WPF.Properties.Settings.Default.UserEmail.Equals(string.Empty))
                new FirstLoginWindow().ShowDialog();

            // is this a deployed application?
            // we don't want it to send requests while debbuging.
            if (IsConnectedToInternet() && System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    // send login information
                    Task loginTask = new Task(() =>
                    {
                        string url = "http://dragon-scale-studios.com/gibbo/dataset/gibbo_dataset.php?opt=login&email=" + 
                            Gibbo.Editor.WPF.Properties.Settings.Default.UserEmail + "&version=" +
                            ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    });

                    loginTask.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error on login send information: " + ex.Message);
                }
            }

            InitializeComponent();

            this.ContentRendered += StartupForm_ContentRendered;

            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null)
            {
                string[] activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                if (activationData != null && activationData.Length > 0)
                {
                    Uri uri = new Uri(activationData[0]);
                    string path = uri.LocalPath.ToString();

                    if (File.Exists(path))
                    {
                        StartEditor(path);
                        return;
                    }
                }
            }

            if (Properties.Settings.Default.LastLoadedProjects.Trim() != string.Empty)
            {
                string[] splitted = Properties.Settings.Default.LastLoadedProjects.Split('|');
                foreach (string split in splitted)
                {
                    if (split.Trim() != string.Empty && File.Exists(split))
                    {
                        ListBoxItem item = new ListBoxItem();

                        item.Content = GibboHelper.SplitCamelCase( Path.GetDirectoryName(split).Split('\\').Last());
                        item.Tag = split;

                        ProjectsListBox.Items.Add(item);
                    }
                }
            }

            if (Properties.Settings.Default.LoadLastProject && ProjectsListBox.Items.Count > 0)
                StartEditor((string)((ProjectsListBox.Items[0] as ListBoxItem).Tag));
        }

        void StartupForm_ContentRendered(object sender, EventArgs e)
        {
            //this.DataContext = new ButtonVisibilityViewModel();
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectPath"></param>
        private void StartEditor(string projectPath)
        {
            this.Hide();

            new MainWindow(projectPath).ShowDialog();
            this.Close();
        }

        #endregion

        #region events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open project";
            ofd.Filter = @"(*.gibbo)|*.gibbo";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EditorCommands.AddToProjectHistory(ofd.FileName);
                StartEditor(ofd.FileName);
            }
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsListBox.SelectedItem != null)
            {
                string path = (string)((ProjectsListBox.SelectedItem as ListBoxItem).Tag);

                EditorCommands.AddToProjectHistory(path);
                StartEditor(path);
            }
            else
            {
                System.Windows.MessageBox.Show("No project selected!\nPlease select from the list, browser or create a new project..", "Ups", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProjectsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProjectsListBox.SelectedItem != null)
            {
                string path = (string)(ProjectsListBox.SelectedItem as ListBoxItem).Tag;

                EditorCommands.AddToProjectHistory(path);
                StartEditor(path);
            }
        }

        private void newProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            NewProjectWindow np = new NewProjectWindow();
            np.ShowDialog();

            // A project was created?
            if (np.DialogResult.Value)
            {
                EditorCommands.AddToProjectHistory(np.ProjectPath);
                StartEditor(np.ProjectPath);
            }
        }

        #endregion

    }
}
