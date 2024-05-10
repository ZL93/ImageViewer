using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageViewer2
{
      public class PictureBoxEx : PictureBox
    {
        private readonly TransformService service = new TransformService();
        private bool canMove = true;
        public bool CanMove { get => canMove; set => canMove = value; }
        private Point prevPoint;
        private Image img;
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
                    service.ImgSize = img.Size;
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
        [Description("是否自动释放上张图片")]
        public bool IsAutoDispose { get; set; } = false;

        public PictureBoxEx()
            : base()
        {
            //DoubleBuffered = true;
        }

        public Point TranslatePoint(Point actualPoint)
        {
            return service.TranslatePoint(actualPoint);
        }

        public PointF TranslatePoint(PointF actualPoint)
        {
            return service.TranslatePoint(actualPoint);
        }

        public void TranslatePoints(Point[] actualPoints)
        {
            service.TranslatePoints(actualPoints);
        }

        public void TranslatePoints(PointF[] actualPoints)
        {
            service.TranslatePoints(actualPoints);
        }

        public Point GetImagePoint(Point actualPoint)
        {
            var tempPoint = TranslatePoint(new PointF(actualPoint.X, actualPoint.Y));
            return new Point((int)tempPoint.X, (int)tempPoint.Y);
        }

        public void Pan(float offsetX, float offsetY)
        {
            service.Pan(offsetX, offsetY);
            Invalidate();
        }

        public void Zoom(float scale, PointF zoomCenter)
        {
            service.Zoom(scale, zoomCenter);
            Invalidate();
        }

        public void Rotate(float angle)
        {
            service.Rotate(angle);
            Invalidate();
        }

        public void DispFit()
        {
            service.DispFit();
            Invalidate();
        }

        public void ShowImage(Image image, bool fit = true)
        {
            if (img != null && IsAutoDispose)
            {
                img.Dispose();
            }
            img = image;
            service.ImgSize = img.Size;
            if (fit)
            {
                service.RotateAngle = 0;
                DispFit();
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { Invalidate(); }));
            }
            else
            {
                Invalidate();
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Image != null)
            {
                prevPoint = e.Location;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canMove)
            {
                if (Image != null)
                {
                    var newPoint = e.Location;
                    var offsetX = newPoint.X - prevPoint.X;
                    var offsetY = newPoint.Y - prevPoint.Y;
                    prevPoint = newPoint;
                    service.Pan(offsetX, offsetY);
                    Invalidate();
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (canMove)
            {
                DispFit();
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Image != null && canMove)
            {
                if (e.Delta > 0)
                {
                    service.Zoom(1.2f, e.Location);
                }
                else
                {
                    service.Zoom(0.8f, e.Location);
                }
                Invalidate();
            }
            base.OnMouseWheel(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Image != null)
            {
                var gs = pe.Graphics.Save();
                if (service.Scale > 7)
                {
                    pe.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                }
                else
                {
                    pe.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
                    pe.Graphics.InterpolationMode = InterpolationMode.Default;
                }
                service.ApplyTransform(pe.Graphics);
                pe.Graphics.DrawImage(Image, 0, 0);
                pe.Graphics.Restore(gs);
            }
            base.OnPaint(pe);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            service.WindowSize = Size;
            base.OnSizeChanged(e);
        }
    }


    internal class TransformService
    {
        private float scale;
        private float scaleOffsetX;
        private float scaleOffsetY;
        private float translateX;
        private float translateY;
        private float rotateAngle;

        private bool matrixChanged = false;

        private Matrix matrixInvert = new Matrix();
        private readonly Matrix matrix = new Matrix();

        public float Scale { get => scale; }

        public float RotateAngle { get => rotateAngle; set => rotateAngle = value; }

        public Size ImgSize { get; set; }

        public Size WindowSize { get; set; }

        public TransformService()
        {
            scale = 1f;
            rotateAngle = scaleOffsetX = scaleOffsetY = translateX = translateY = 0f;
        }

        public void Pan(float offsetX, float offsetY)
        {
            matrixChanged = true;
            translateX += offsetX;
            translateY += offsetY;
        }

        public void Zoom(float zoomFactor, PointF zoomCenter)
        {
            matrixChanged = true;
            scaleOffsetX += (zoomCenter.X - scaleOffsetX - translateX) * (1 - zoomFactor);
            scaleOffsetY += (zoomCenter.Y - scaleOffsetY - translateY) * (1 - zoomFactor);
            scale *= zoomFactor;
        }
        public void Rotate(float angle)
        {
            matrixChanged = true;
            rotateAngle += angle;
        }
        public void Restore()
        {
            scale = 1;
            rotateAngle = scaleOffsetX = scaleOffsetY = translateX = translateY = 0f;
            matrixChanged = true;
        }

        public void DispFit()
        {
            if (ImgSize != null)
            {
                //小图显示原图大小
                if (ImgSize.Width < WindowSize.Width && ImgSize.Height < WindowSize.Height)
                {
                    scale = 1;
                }
                else
                {
                    //图像缩放至中间
                    if (ImgSize.Width * WindowSize.Height >= ImgSize.Height * WindowSize.Width)
                    {
                        scale = WindowSize.Width / (float)ImgSize.Width;
                    }
                    else
                    {
                        scale = WindowSize.Height / (float)ImgSize.Height;
                    }
                }
                scaleOffsetX = (WindowSize.Width - ImgSize.Width * scale) / 2;
                scaleOffsetY = (WindowSize.Height - ImgSize.Height * scale) / 2;
                translateX = translateY = 0f;
                matrixChanged = true;
            }
        }

        public void ApplyTransform(Graphics gs)
        {
            EnsureMatrix();
            gs.Transform = matrix;
        }

        private void EnsureMatrix()
        {
            if (matrixChanged)
            {
                matrix.Reset();
                matrix.RotateAt(rotateAngle, new PointF(ImgSize.Width / 2, ImgSize.Height / 2), MatrixOrder.Append);
                matrix.Scale(scale, scale, MatrixOrder.Append);
                matrix.Translate(scaleOffsetX, scaleOffsetY, MatrixOrder.Append);
                matrix.Translate(translateX, translateY, MatrixOrder.Append);

                matrixInvert = matrix.Clone();
                matrixInvert.Invert();
                matrixChanged = false;
            }
        }

        #region Translate
        /// <summary>
        /// 图像框位置 -> 图像位置
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point TranslatePoint(Point point)
        {
            Point[] singlePoint = new Point[1] { point };
            TranslatePoints(singlePoint);
            return singlePoint[0];
        }
        /// <summary>
        /// 图像框位置 -> 图像位置
        /// </summary>
        /// <param name="points"></param>
        public void TranslatePoints(Point[] points)
        {
            EnsureMatrix();
            matrixInvert.TransformPoints(points);
        }
        /// <summary>
        /// 图像框位置 -> 图像位置
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointF TranslatePoint(PointF point)
        {
            PointF[] singlePointF = new PointF[1] { point };
            TranslatePoints(singlePointF);
            return singlePointF[0];
        }
        /// <summary>
        /// 图像框位置 -> 图像位置
        /// </summary>
        /// <param name="points"></param>
        public void TranslatePoints(PointF[] points)
        {
            EnsureMatrix();
            matrixInvert.TransformPoints(points);
        }


        /// <summary>
        /// 图像位置 -> 图像框位置
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point TranslatePoint2(Point point)
        {
            Point[] singlePoint = new Point[1] { point };
            TranslatePoints(singlePoint);
            return singlePoint[0];
        }
        /// <summary>
        /// 图像位置 -> 图像框位置
        /// </summary>
        /// <param name="points"></param>
        public void TranslatePoints2(Point[] points)
        {
            EnsureMatrix();
            matrix.TransformPoints(points);
        }
        /// <summary>
        /// 图像位置 -> 图像框位置
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointF TranslatePoint2(PointF point)
        {
            PointF[] singlePointF = new PointF[1] { point };
            TranslatePoints(singlePointF);
            return singlePointF[0];
        }
        /// <summary>
        /// 图像位置 -> 图像框位置
        /// </summary>
        /// <param name="points"></param>
        public void TranslatePoints2(PointF[] points)
        {
            EnsureMatrix();
            matrix.TransformPoints(points);
        }

        #endregion

    }
}
