using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageViewer2
{
    public partial class WImgBox : PictureBox
    {
        public WImgBox()
        {
            InitializeComponent();
        }
        private Image img;
        private Point p = new Point();
        private float offsetX = 0;
        private float offsetY = 0;
        private float imgScale = 0;

        [Description("是否自动释放上张图片")]
        public bool IsAutoDispose { get; set; } = false;

        public new Image Image
        {
            get { return img; }
            set
            {
                if (img != value)
                {
                    if (img != null && IsAutoDispose)
                    {
                        img.Dispose();
                    }
                    img = value;
                    if (img != null)
                    {
                        DispFit();
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => { Invalidate(); }));
                        }
                        else
                        {
                            Invalidate();
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (img != null)
            {
                Graphics g = pe.Graphics;
                if (imgScale > 7)
                {
                    pe.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                }
                else
                {
                    pe.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                    pe.Graphics.InterpolationMode = InterpolationMode.Default;
                }

                g.DrawImage(img, offsetX, offsetY, img.Width * imgScale, img.Height * imgScale);
            }
            base.OnPaint(pe);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                p = new Point(e.X, e.Y);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offsetX += e.X - p.X;
                offsetY += e.Y - p.Y;
                p = new Point(e.X, e.Y);
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            float imgScaleOffset = e.Delta > 0 ? 1.2f : 0.8f;
            offsetX += (e.X - offsetX) * (1 - imgScaleOffset);
            offsetY += (e.Y - offsetY) * (1 - imgScaleOffset);
            imgScale *= imgScaleOffset;
            Invalidate();
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            DispFit();
            Invalidate();
            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// 填充显示图像
        /// </summary>
        private void DispFit()
        {
            if (img != null)
            {
                //小图显示原图大小
                if (img.Width < Width && img.Height < Height)
                {
                    imgScale = 1;
                }
                else
                {
                    //图像缩放至中间
                    if (img.Width * Height >= img.Height * Width)
                    {
                        imgScale = Width / (float)img.Width;
                    }
                    else
                    {
                        imgScale = Height / (float)img.Height;
                    }
                }
                offsetX = (Width - img.Width * imgScale) / 2;
                offsetY = (Height - img.Height * imgScale) / 2;
            }
        }
        /// <summary>
        /// 获取鼠标所指的图像实际位置
        /// </summary>
        /// <param name="boxP">鼠标位置</param>
        /// <returns>图像实际位置</returns>
        internal Point GetImgPoint(Point boxP)
        {
            Point result = new Point();
            result.X = (int)((boxP.X - offsetX) / imgScale);
            result.Y = (int)((boxP.Y - offsetY) / imgScale);
            return result;
        }
    }
}
