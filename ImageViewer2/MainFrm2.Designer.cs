namespace ImageViewer2
{
    partial class MainFrm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm2));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上一张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加文件右键菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置文件打开关联ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxEx1 = new ImageViewer2.PictureBoxEx();
            this.右旋转ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.左旋转ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上一张ToolStripMenuItem,
            this.下一张ToolStripMenuItem,
            this.右旋转ToolStripMenuItem,
            this.左旋转ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 136);
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
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 789);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 1;
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
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(767, 17);
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
            // pictureBoxEx1
            // 
            this.pictureBoxEx1.AllowDrop = true;
            this.pictureBoxEx1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxEx1.BackgroundImage")));
            this.pictureBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxEx1.Image = null;
            this.pictureBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxEx1.Name = "pictureBoxEx1";
            this.pictureBoxEx1.Size = new System.Drawing.Size(834, 789);
            this.pictureBoxEx1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxEx1.TabIndex = 2;
            this.pictureBoxEx1.TabStop = false;
            this.pictureBoxEx1.DragDrop += new System.Windows.Forms.DragEventHandler(this.imgBox1_DragDrop);
            this.pictureBoxEx1.DragEnter += new System.Windows.Forms.DragEventHandler(this.imgBox1_DragEnter);
            this.pictureBoxEx1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxEx1_Paint);
            this.pictureBoxEx1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEx1_MouseClick);
            this.pictureBoxEx1.MouseEnter += new System.EventHandler(this.pictureBoxEx1_MouseEnter);
            this.pictureBoxEx1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEx1_MouseMove);
            // 
            // 右旋转ToolStripMenuItem
            // 
            this.右旋转ToolStripMenuItem.Image = global::ImageViewer2.Properties.Resources.右旋转;
            this.右旋转ToolStripMenuItem.Name = "右旋转ToolStripMenuItem";
            this.右旋转ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.右旋转ToolStripMenuItem.Text = "右旋转";
            this.右旋转ToolStripMenuItem.Click += new System.EventHandler(this.右旋转ToolStripMenuItem_Click);
            // 
            // 左旋转ToolStripMenuItem
            // 
            this.左旋转ToolStripMenuItem.Image = global::ImageViewer2.Properties.Resources.左旋转;
            this.左旋转ToolStripMenuItem.Name = "左旋转ToolStripMenuItem";
            this.左旋转ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.左旋转ToolStripMenuItem.Text = "左旋转";
            this.左旋转ToolStripMenuItem.Click += new System.EventHandler(this.左旋转ToolStripMenuItem_Click);
            // 
            // MainFrm2
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 811);
            this.Controls.Add(this.pictureBoxEx1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainFrm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像查看器 - Bulid by ZL";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrm_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private PictureBoxEx pictureBoxEx1;
        private System.Windows.Forms.ToolStripMenuItem 右旋转ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 左旋转ToolStripMenuItem;
    }
}

