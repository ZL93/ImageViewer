namespace ImageViewer
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上一张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.锁定区域ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加文件右键菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置文件打开关联ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgBox1 = new ImageViewer.ImgBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.读取RegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存RegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.合并Region并保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上一张ToolStripMenuItem,
            this.下一张ToolStripMenuItem,
            this.toolStripSeparator1,
            this.锁定区域ToolStripMenuItem,
            this.读取RegionToolStripMenuItem,
            this.保存RegionToolStripMenuItem,
            this.合并Region并保存ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 164);
            // 
            // 上一张ToolStripMenuItem
            // 
            this.上一张ToolStripMenuItem.Name = "上一张ToolStripMenuItem";
            this.上一张ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.上一张ToolStripMenuItem.Text = "上一张";
            this.上一张ToolStripMenuItem.Click += new System.EventHandler(this.上一张ToolStripMenuItem_Click);
            // 
            // 下一张ToolStripMenuItem
            // 
            this.下一张ToolStripMenuItem.Name = "下一张ToolStripMenuItem";
            this.下一张ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下一张ToolStripMenuItem.Text = "下一张";
            this.下一张ToolStripMenuItem.Click += new System.EventHandler(this.下一张ToolStripMenuItem_Click);
            // 
            // 锁定区域ToolStripMenuItem
            // 
            this.锁定区域ToolStripMenuItem.Name = "锁定区域ToolStripMenuItem";
            this.锁定区域ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.锁定区域ToolStripMenuItem.Text = "锁定区域";
            this.锁定区域ToolStripMenuItem.Click += new System.EventHandler(this.锁定区域ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 17);
            this.toolStripStatusLabel1.Text = "XY";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(717, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel3.Text = "WH";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加文件右键菜单ToolStripMenuItem,
            this.设置文件打开关联ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(173, 48);
            // 
            // 添加文件右键菜单ToolStripMenuItem
            // 
            this.添加文件右键菜单ToolStripMenuItem.Name = "添加文件右键菜单ToolStripMenuItem";
            this.添加文件右键菜单ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.添加文件右键菜单ToolStripMenuItem.Text = "添加文件右键菜单";
            this.添加文件右键菜单ToolStripMenuItem.Click += new System.EventHandler(this.添加文件右键菜单ToolStripMenuItem_Click);
            // 
            // 设置文件打开关联ToolStripMenuItem
            // 
            this.设置文件打开关联ToolStripMenuItem.Name = "设置文件打开关联ToolStripMenuItem";
            this.设置文件打开关联ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.设置文件打开关联ToolStripMenuItem.Text = "设置文件打开关联";
            this.设置文件打开关联ToolStripMenuItem.Click += new System.EventHandler(this.设置文件打开关联ToolStripMenuItem_Click);
            // 
            // imgBox1
            // 
            this.imgBox1.AllowDrop = true;
            this.imgBox1.AutoClearRegion = true;
            this.imgBox1.AutoDispose = true;
            this.imgBox1.CanDrawRegion = true;
            this.imgBox1.CanMoveImg = true;
            this.imgBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.imgBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgBox1.HBackColor = "light steel blue";
            this.imgBox1.HImage = null;
            this.imgBox1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.imgBox1.Location = new System.Drawing.Point(0, 0);
            this.imgBox1.Name = "imgBox1";
            this.imgBox1.Size = new System.Drawing.Size(784, 739);
            this.imgBox1.TabIndex = 0;
            this.imgBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.imgBox1_DragDrop);
            this.imgBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.imgBox1_DragEnter);
            this.imgBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imgBox1_KeyDown);
            this.imgBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBox1_MouseDown);
            this.imgBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgBox1_MouseMove);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // 读取RegionToolStripMenuItem
            // 
            this.读取RegionToolStripMenuItem.Name = "读取RegionToolStripMenuItem";
            this.读取RegionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.读取RegionToolStripMenuItem.Text = "读取Region";
            this.读取RegionToolStripMenuItem.Click += new System.EventHandler(this.读取RegionToolStripMenuItem_Click);
            // 
            // 保存RegionToolStripMenuItem
            // 
            this.保存RegionToolStripMenuItem.Name = "保存RegionToolStripMenuItem";
            this.保存RegionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.保存RegionToolStripMenuItem.Text = "保存Region";
            this.保存RegionToolStripMenuItem.Click += new System.EventHandler(this.保存RegionToolStripMenuItem_Click);
            // 
            // 合并Region并保存ToolStripMenuItem
            // 
            this.合并Region并保存ToolStripMenuItem.Name = "合并Region并保存ToolStripMenuItem";
            this.合并Region并保存ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.合并Region并保存ToolStripMenuItem.Text = "合并Region并保存";
            this.合并Region并保存ToolStripMenuItem.Click += new System.EventHandler(this.合并Region并保存ToolStripMenuItem_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.imgBox1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageViewer - Bulid by ZL";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImgBox imgBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 上一张ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下一张ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 添加文件右键菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置文件打开关联ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 锁定区域ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 读取RegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存RegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 合并Region并保存ToolStripMenuItem;
    }
}

