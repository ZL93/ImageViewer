using Microsoft.Win32;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ImageViewer2
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
            wImgBox1.ContextMenuStrip = contextMenuStrip2;
        }
        public MainFrm(string imgPath)
        {
            InitializeComponent();
            if (ReadImage(imgPath))
            {
                GetFolderImages(imgPath);
            }
            wImgBox1.ContextMenuStrip = contextMenuStrip1;
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
                        s.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) || s.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase)||
                        s.EndsWith(".tif", StringComparison.CurrentCultureIgnoreCase)).ToArray();

            //Array.Sort(ImgPathes, new FileNameSort());
            index = Array.IndexOf(ImgPathes, str);
        }

        private bool ReadImage(string path)
        {
            if (isFrist)
            {
                wImgBox1.ContextMenuStrip = contextMenuStrip1;
                isFrist = false;
            }
            path = path.ToLower();
            if (path.EndsWith(".bmp") || path.EndsWith(".jpg") || path.EndsWith(".png") || path.EndsWith(".jpeg") || path.EndsWith(".tif"))
            {
                FileInfo fileInfo = new FileInfo(path);
                this.Text = $"{path} - {fileInfo.Length / 1024 / 1024f:#0.00}MB";
                try
                {
                    using (Image img = Image.FromFile(path))
                    {
                        Bitmap bmp = new Bitmap(img);
                        wImgBox1.Image = bmp;
                        toolStripStatusLabel3.Text = $"W:{bmp.Width} H:{bmp.Height}";
                    }
                    return true;
                }
                catch
                {
                    MessageBox.Show(Path.GetFileName(path)+" 打开失败！！!");
                }
            }
            return false;
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void 上一张ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LastImg();
        }
        private void 下一张ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextImg();
        }

        private void wImgBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (wImgBox1.Image != null)
            {
                if (e.X <= 5 + 32 || e.X >= wImgBox1.Width - 5 - 32)
                {
                    if (!canShowBtn)
                    {
                        Cursor = Cursors.Hand;
                        canShowBtn = true;
                        wImgBox1.Refresh();
                    }
                }
                else
                {
                    if (canShowBtn)
                    {
                        Cursor = Cursors.Default;
                        canShowBtn = false;
                        wImgBox1.Refresh();
                    }
                }
                if (e.X >= 0 && e.Y >= 0)
                {
                    Point p = wImgBox1.GetImgPoint(new Point(e.X, e.Y));
                    if (p.X >= 0 && p.Y >= 0 && p.X < wImgBox1.Image.Width && p.Y < wImgBox1.Image.Height)
                    {
                        Bitmap bitmap = (Bitmap)(wImgBox1.Image);
                        Color color = bitmap.GetPixel(p.X, p.Y);
                        toolStripStatusLabel1.Text = $"X: {p.X} Y: {p.Y} [{color.R},{color.G},{color.B}]";
                    }
                }
            }
        }

        private void wImgBox1_Paint(object sender, PaintEventArgs e)
        {
            if (wImgBox1.Image != null)
            {
                if (canShowBtn)
                {
                    Graphics g = e.Graphics;
                    g.DrawImage(Properties.Resources.左箭头, new Point(5, (wImgBox1.Height - 32) / 2));
                    g.DrawImage(Properties.Resources.右箭头, new Point(wImgBox1.Width - 32 - 5, (wImgBox1.Height - 32) / 2));
                }
            }
        }

        private void wImgBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (wImgBox1.Image != null)
            {
                if (e.X <= 5 + 32)
                {
                    LastImg();
                }
                else if (e.X >= wImgBox1.Width - 5 - 32)
                {
                    NextImg();
                }
            }
        }

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
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

        private void wImgBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Activate();
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
                    Thread.Sleep(10);
                }
                //左半圆逆时针
                for (int j = radius; j >= -radius; j--)
                {
                    int x = -Convert.ToInt32(Math.Sqrt(radius * radius - j * j));
                    int y = -j;

                    this.Location = new Point(pOld.X + x, pOld.Y + y);
                    Thread.Sleep(10);
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

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = @"(*.bmp)|*.bmp|(*.jpg)|*.jpg|(*.png)|*.png|(*.jpeg)|*.jpeg|(*.tif)|*.tif",
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(sfd.FileName).Trim().ToLower();
                if (ext == ".bmp")
                {
                    wImgBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                else if (ext == ".jpg" || ext == ".jpeg")
                {
                    wImgBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else if (ext == ".png")
                {
                    wImgBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                else if (ext == ".tif")
                {
                    wImgBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
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
