using HalconDotNet;
using Microsoft.Win32;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class MainFrm : Form
    {
        private string[] ImgPathes = new string[] { };
        private int index = 0;
        private bool canShowBtn = false;
        private bool isFrist = false;
        public MainFrm()
        {
            InitializeComponent();
            isFrist = true;
            imgBox1.ContextMenuStrip = contextMenuStrip2;
        }
        public MainFrm(string imgPath)
        {
            InitializeComponent();
            if (ReadImage(imgPath))
            {
                GetFolderImages(imgPath);
            }
            imgBox1.ContextMenuStrip = contextMenuStrip1;
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {

        }

        private void imgBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        private void imgBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                string str = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (ReadImage(str))
                {
                    GetFolderImages(str);
                }
            }
        }

        private void GetFolderImages(string str)
        {
            string folder = Path.GetDirectoryName(str);

            ImgPathes = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => s.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase) || s.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
                        s.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) || s.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase) || 
                        s.EndsWith(".tif", StringComparison.CurrentCultureIgnoreCase)).ToArray();

            //Array.Sort(ImgPathes, new FileNameSort());
            index = Array.IndexOf(ImgPathes, str);
        }

        private bool ReadImage(string path)
        {
            if (isFrist)
            {
                imgBox1.ContextMenuStrip = contextMenuStrip1;
                isFrist = false;
            }
            var imgPath = path.ToLower();
            if (imgPath.EndsWith(".bmp") || imgPath.EndsWith(".jpg") || imgPath.EndsWith(".png") || imgPath.EndsWith(".jpeg") || imgPath.EndsWith(".tif"))
            {
                try
                {
                    HOperatorSet.ReadImage(out HObject Img, path);
                    imgBox1.HImage = Img;
                    imgBox1.DispObj();
                    toolStripStatusLabel3.Text = $"W:{imgBox1.ImgWidth} H:{imgBox1.ImgHeight}";
                    FileInfo fileInfo = new FileInfo(path);
                    this.Text = $"{path} - {fileInfo.Length / 1024 / 1024f:#0.00}MB";
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void 上一张ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LastImg();
        }

        private void 下一张ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextImg();
        }

        private void imgBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= 5 + 32)
            {
                canShowBtn = true;
                this.Cursor = Cursors.PanWest;
            }
            else if (e.X >= imgBox1.Width - 5 - 32)
            {
                canShowBtn = true;
                this.Cursor = Cursors.PanEast;
            }
            else
            {
                if (canShowBtn)
                {
                    canShowBtn = false;
                    this.Cursor = Cursors.Default;
                }
            }
            try
            {
                imgBox1.HalconWindow.GetMposition(out int mouseY, out int mouseX, out int mbutton);
                if (imgBox1.HImage != null)
                {
                    if (mouseY > 0 && mouseX > 0 && mouseX < imgBox1.ImgWidth && mouseY < imgBox1.ImgHeight)
                    {
                        HOperatorSet.GetGrayval(imgBox1.HImage, mouseY, mouseX, out HTuple grayval);
                        toolStripStatusLabel1.Text = "X:" + mouseX + " Y:" + mouseY + " _ " + grayval.ToString();
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "X:" + mouseX + " Y:" + mouseY;
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "X:" + mouseX + " Y:" + mouseY;
                }
            }
            catch
            {
            }
        }

        private void LastImg()
        {
            if (ImgPathes.Length <= 1)
            {
                Vibration();
                return;
            }
            if (index == 0)
            {
                Vibration();
                return;
            }
            index--;
            if (index >= 0)
            {
                ReadImage(ImgPathes[index]);
            }
        }
        private void NextImg()
        {
            if (ImgPathes.Length <= 1)
            {
                Vibration();
                return;
            }
            if (index == ImgPathes.Length - 1)
            {
                Vibration();
                return;
            }
            index++;
            if (index < ImgPathes.Length)
            {
                ReadImage(ImgPathes[index]);
            }
        }
        private void Vibration()
        {
            Point pOld = this.Location;//原来的位置
            int radius = 3;//半径
            for (int n = 0; n < 2; n++) //旋转圈数
            {
                //右半圆逆时针
                for (int i = -radius; i <= radius; i++)
                {
                    int x = Convert.ToInt32(Math.Sqrt(radius * radius - i * i));
                    int y = -i;

                    this.Location = new Point(pOld.X + x, pOld.Y + y);
                    Thread.Sleep(5);
                }
                //左半圆逆时针
                for (int j = radius; j >= -radius; j--)
                {
                    int x = -Convert.ToInt32(Math.Sqrt(radius * radius - j * j));
                    int y = -j;

                    this.Location = new Point(pOld.X + x, pOld.Y + y);
                    Thread.Sleep(5);
                }
            }
            //抖动完成，恢复原来位置
            this.Location = pOld;
        }

        /// <summary>
        /// Create an associaten for a file extension in the windows registry
        /// CreateAssociation(@"vendor.application",".tmf","Tool file",@"C:\Windows\SYSWOW64\notepad.exe",@"%SystemRoot%\SYSWOW64\notepad.exe,0");
        /// </summary>
        /// <param name="ProgID">e.g. vendor.application</param>
        /// <param name="extension">e.g. .tmf</param>
        /// <param name="description">e.g. Tool file</param>
        /// <param name="application">e.g.  @"C:\Windows\SYSWOW64\notepad.exe"</param>
        /// <param name="icon">@"%SystemRoot%\SYSWOW64\notepad.exe,0"</param>
        /// <param name="hive">e.g. The user-specific settings have priority over the computer settings. KeyHive.LocalMachine  need admin rights</param>
        private void CreateAssociation(string ProgID, string extension, string description, string application, string icon, KeyHiveSmall hive = KeyHiveSmall.CurrentUser)
        {
            RegistryKey selectedKey = null;

            switch (hive)
            {
                case KeyHiveSmall.ClassesRoot:
                    Registry.ClassesRoot.CreateSubKey(extension).SetValue("", ProgID);
                    selectedKey = Registry.ClassesRoot.CreateSubKey(ProgID);
                    break;

                case KeyHiveSmall.CurrentUser:
                    Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + extension).SetValue("", ProgID);
                    selectedKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + ProgID);
                    break;

                case KeyHiveSmall.LocalMachine:
                    Registry.LocalMachine.CreateSubKey(@"Software\Classes\" + extension).SetValue("", ProgID);
                    selectedKey = Registry.LocalMachine.CreateSubKey(@"Software\Classes\" + ProgID);
                    break;
            }

            if (selectedKey != null)
            {
                if (description != null)
                {
                    selectedKey.SetValue("", description);
                }
                if (icon != null)
                {
                    selectedKey.CreateSubKey("DefaultIcon").SetValue("", icon, RegistryValueKind.ExpandString);
                    selectedKey.CreateSubKey(@"Shell\Open").SetValue("icon", icon, RegistryValueKind.ExpandString);
                }
                if (application != null)
                {
                    selectedKey.CreateSubKey(@"Shell\Open\command").SetValue("", "\"" + application + "\"" + " \"%1\"", RegistryValueKind.ExpandString);
                }
            }
            selectedKey.Flush();
            selectedKey.Close();
        }
        /// <summary>
        /// Creates a association for current running executable
        /// </summary>
        /// <param name="extension">e.g. .tmf</param>
        /// <param name="hive">e.g. KeyHive.LocalMachine need admin rights</param>
        /// <param name="description">e.g. Tool file. Displayed in explorer</param>
        private void SelfCreateAssociation(string extension, KeyHiveSmall hive = KeyHiveSmall.CurrentUser, string description = "")
        {
            string ProgID = Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.FullName;
            string FileLocation = Assembly.GetExecutingAssembly().Location;
            CreateAssociation(ProgID, extension, description, FileLocation, FileLocation + ",0", hive);
        }

        private void 添加文件右键菜单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //设置右键菜单
                RegistryKey shell = Registry.ClassesRoot.OpenSubKey("*", true).OpenSubKey("shell", true);
                if (shell == null) shell = Registry.ClassesRoot.OpenSubKey("*", true).CreateSubKey("shell");
                RegistryKey custome = shell.CreateSubKey("图像查看器");
                custome.SetValue("Icon", Application.ExecutablePath + ",0");
                RegistryKey cmd = custome.CreateSubKey("command");
                cmd.SetValue("", Application.ExecutablePath + " %1");
                cmd.Close();
                custome.Close();
                shell.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void 设置文件打开关联ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelfCreateAssociation(".bmp", KeyHiveSmall.ClassesRoot);
                SelfCreateAssociation(".jpg", KeyHiveSmall.ClassesRoot);
                SelfCreateAssociation(".png", KeyHiveSmall.ClassesRoot);
                SelfCreateAssociation(".jpeg", KeyHiveSmall.ClassesRoot);
                SelfCreateAssociation(".tif", KeyHiveSmall.ClassesRoot);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void imgBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Escape)
            {
                Close();
            }
            else if (e.KeyValue == (int)Keys.Right || e.KeyValue == (int)Keys.D)
            {
                NextImg();
            }
            else if (e.KeyValue == (int)Keys.Left || e.KeyValue == (int)Keys.A)
            {
                LastImg();
            }
        }

        private void imgBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (canShowBtn)
            {
                if (e.X <= 5 + 32)
                {
                    LastImg();
                }
                else if (e.X >= imgBox1.Width - 5 - 32)
                {
                    NextImg();
                }
            }
        }

        private void 锁定区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (锁定区域ToolStripMenuItem.Checked)
            {
                锁定区域ToolStripMenuItem.Checked = false;
                imgBox1.AutoClearRegion = true;
            }
            else
            {
                锁定区域ToolStripMenuItem.Checked = true;
                imgBox1.AutoClearRegion = false;
            }
        }

        private void 读取RegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Region文件|*.hobj";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    HRegion hRegion = new HRegion();
                    hRegion.ReadRegion(ofd.FileName);
                    int regCount = hRegion.CountObj();
                    for (int i = 1; i <= regCount; i++)
                    {
                        imgBox1.HRegions.Add(new HRegionAtt() { Region = hRegion.SelectObj(i), Name = "ROI", DrawColor = HColors.Blue, DrawMode = "margin", LineWidth = 2 });
                    }
                    imgBox1.DispObj();
                }
            }
        }

        private void 保存RegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Region文件|*.hobj";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    imgBox1.SaveRegion(sfd.FileName);
                }
            }
        }

        private void 合并Region并保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Region文件|*.hobj";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    imgBox1.SaveUnionRegion(sfd.FileName);
                }
            }
        }
    }

    internal enum KeyHiveSmall
    {
        ClassesRoot,
        CurrentUser,
        LocalMachine
    }
}
