using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class ImgBox : UserControl
    {
        [Description("是否自动释放上张图片")]
        public bool AutoDispose { get; set; } = false;

        [Description("是否清空区域")]
        public bool AutoClearRegion { get; set; } = true;

        private HObject _himg;
        public HObject HImage
        {
            get { return _himg; }
            set
            {
                if (_himg != value)
                {
                    HObject oldHObject = _himg;
                    _himg = value;
                    if (_himg != null)
                    {
                        HOperatorSet.GetImageSize(_himg, out HTuple w, out HTuple h);
                        //大小改变后就自适应图片框
                        if (ImgWidth != w || ImgHeight != h)
                        {
                            imgW = w;
                            imgH = h;
                            HalconWindow.DispObj(HImage);
                            AdaptiveImage();
                        }
                        if (AutoClearRegion) 
                        {
                            selectRegion?.Dispose();
                            selectRegion = null;
                            SelectRegionXLD?.Dispose();
                            SelectRegionXLD = null;
                            HRegions = new List<HRegionAtt>();
                            HTexts = new List<HTextAtt>();
                            HXLDs = new List<HXLDAtt>();
                        }
                        else if (selectRegion != null && SelectRegionXLD != null)
                        {
                            ShowFeature?.Invoke(_himg, selectRegion);
                        }
                    }
                    //释放原来的图像
                    if (oldHObject != null && AutoDispose)
                    {
                        oldHObject.Dispose();
                    }
                }
            }
        }
        public List<HRegionAtt> HRegions = new List<HRegionAtt>();
        public List<HTextAtt> HTexts = new List<HTextAtt>();
        public List<HXLDAtt> HXLDs = new List<HXLDAtt>();
        private List<HDrawingObject> D_objects = new List<HDrawingObject>();
        private HDrawingObject selected_drawing_object;

        private int imgW;
        public int ImgWidth { get => imgW; }
        private int imgH;
        public int ImgHeight { get => imgH; }

        private float imgscale = 1; //缩放比例
        private Point oldP = new Point();
        private int part_row1, part_col1, part_row2, part_col2;
        private bool _move = false;
        private bool isCtrl, isAlt;
        private HObject SelectRegionXLD;
        public HObject selectRegion;
        public ShowFeatureHandler ShowFeature;
        private static HFeaturesFrm regionFrm;
        private bool isDrawingRegion = false; //正在画Region
        private bool isDragObj = false;

        /// <summary>
        /// 能否控制图像平移缩放
        /// </summary>
        public bool CanMoveImg { get; set; } = true;
        public bool CanDrawRegion { get; set; } = true;
        public string HBackColor { get; set; } = "black";
        private System.ComponentModel.IContainer components = null;

        [Browsable(false)]
        public HWindow HalconWindow
        {
            get
            {
                if (this.window != null)
                {
                    return this.window;
                }
                return new HWindow();
            }
        }

        [Description("This rectangle specifies the image part to be displayed, which will automatically be zoomed to fill the window. To display a full image of size W x H, set this to 0;0;W;H")]
        [Category("Layout")]
        public Rectangle ImagePart
        {
            get
            {
                return this.imagePart;
            }
            set
            {
                if (value.IsEmpty)
                {
                    this.imagePart = new Rectangle(0, 0, base.Width - 2 * this.borderWidth, base.Height - 2 * this.borderWidth);
                }
                else
                {
                    this.imagePart = value;
                }
                this.UpdatePart();
            }
        }
        [Browsable(false)]
        public IntPtr HalconID
        {
            get
            {
                if (this.window != null)
                {
                    return this.window.Handle;
                }
                return IntPtr.Zero;
            }
        }

        private IntPtr hwnd = IntPtr.Zero;

        private HWindow window;

        private Rectangle imagePart = new Rectangle(0, 0, 640, 480);

        private Rectangle windowExtents = new Rectangle(0, 0, 320, 240);

        private int borderWidth = 0;
        public ImgBox()
        {
            InitializeComponent();

            base.VisibleChanged += this.HWindowControl_VisibleChanged;
            base.Resize += this.HWindowControl_Resize;
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ImgBox
            // 
            this.Name = "ImgBox";
            this.ResumeLayout(false);

        }
        protected override void OnLoad(EventArgs e)
        {
            this.createWindow(false);
            base.OnLoad(e);
        }
        private void HWindowControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.window != null && base.Visible && this.hwnd != base.Handle)
            {
                this.createWindow(true);
            }
        }
        private void HWindowControl_Resize(object sender, EventArgs e)
        {
            this.UpdateWindowExtents();
        }
        private void UpdateWindowExtents()
        {
            this.windowExtents = new Rectangle(this.borderWidth, this.borderWidth, base.ClientSize.Width - 2 * this.borderWidth, base.ClientSize.Height - 2 * this.borderWidth);
            if (this.window != null && this.windowExtents.Width > 0 && this.windowExtents.Height > 0)
            {
                this.window.GetWindowExtents(out int x, out int y, out int width, out int height);
                if (!this.windowExtents.Equals(new Rectangle(x, y, width, height)))
                {
                    this.window.SetWindowExtents(this.windowExtents.Left, this.windowExtents.Top, this.windowExtents.Width, this.windowExtents.Height);
                    if (HSystem.GetSystem(new HTuple("flush_graphic")).S == "true")
                    {
                        AdaptiveImage();
                        DispObj();
                        return;
                    }
                }
            }
        }
        private void UpdatePart()
        {
            if (this.window != null)
            {
                this.window.SetPart(this.imagePart.Top, this.imagePart.Left, this.imagePart.Top + this.imagePart.Height - 1, this.imagePart.Left + this.imagePart.Width - 1);
            }
        }
        private void createWindow(bool repair)
        {
            if ((this.window == null || repair) && DesignMode == false)
            {
                try
                {
                    HOperatorSet.SetCheck("~father");
                    if (this.window == null)
                    {
                        this.window = new HWindow();
                    }
                    this.hwnd = base.Handle;
                    this.window.OpenWindow(this.borderWidth, this.borderWidth, base.Width - 2 * this.borderWidth, base.Height - 2 * this.borderWidth, this.hwnd, "visible", "");
                    this.window.SetWindowParam("background_color", HBackColor);
                    this.window.SetWindowParam("flush", "false");
                    this.window.SetFont("微软雅黑-14");
                    this.UpdatePart();
                    if (HalconAPI.isWindows)
                    {
                        HOperatorSet.SetSystem("use_window_thread", "true");
                    }
                    DispObj();
                }
                catch (HOperatorException ex)
                {
                    int errorCode = ex.GetErrorCode();
                    if (errorCode >= 5100 && errorCode < 5200)
                    {
                        throw ex;
                    }
                }
                catch (DllNotFoundException)
                {
                }
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (isDrawingRegion || !CanMoveImg)
            {
                return;
            }
            HalconWindow.GetPart(out int x0, out int y0, out int x1, out int y1);
            imgscale = (x1 - x0) / (float)(Height);
            float px0 = y0 + e.X * imgscale;
            float py0 = x0 + e.Y * imgscale;
            imgscale = imgscale * (1 - e.Delta / 300f);
            imgscale = imgscale < 0.05f ? 0.05f : imgscale;
            imgscale = imgscale > 10f ? 10f : imgscale;

            float px1 = e.X * imgscale;
            float py1 = e.Y * imgscale;

            float offsetX = px0 - px1;
            float offsetY = py0 - py1;
            HalconWindow.SetPart((int)offsetY, (int)offsetX, (int)(Height * imgscale + offsetY), (int)(Width * imgscale + offsetX));
            DispObj();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Clicks == 2)
            {
                AdaptiveImage();
                DispObj();
                return;
            }
            if (!CanMoveImg)
            {
                return;
            }
            isDragObj = false;
            if (e.Button == MouseButtons.Left)
            {
                oldP = new Point(e.X, e.Y);
                HalconWindow.GetPart(out part_row1, out part_col1, out part_row2, out part_col2);
                imgscale = (part_row2 - part_row1) / (float)(Height);
                _move = true;

                HalconWindow.GetMposition(out int mouseY, out int mouseX, out int mbutton);
                SelectRegion(mouseX, mouseY);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            
            if (isDrawingRegion || !CanMoveImg || isDragObj)
            {
                return;
            }
            if (e.Button == MouseButtons.Left && _move)
            {
                int offsetX = (int)((oldP.X - e.X) * imgscale);
                int offsetY = (int)((oldP.Y - e.Y) * imgscale);

                HalconWindow.SetPart(part_row1 + offsetY, part_col1 + offsetX, part_row2 + offsetY, part_col2 + offsetX);
                DispObj();
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.Select();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _move = false;
            base.OnMouseUp(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (isDrawingRegion || !CanMoveImg)
            {
                return;
            }
            isCtrl = e.Control;
            isAlt = e.Alt;
            if (e.KeyCode == Keys.P)
            {
                if (regionFrm == null || regionFrm.IsDisposed)
                {
                    regionFrm = new HFeaturesFrm(this);
                }
                regionFrm.Location = this.PointToScreen(new Point(-regionFrm.Width, 0));
                regionFrm.Show();
                regionFrm.Focus();
            }
            if (CanDrawRegion)
            {
               
                HalconWindow.GetPart(out int x0, out int y0, out int x1, out int y1);
                if (e.KeyCode == Keys.C)
                {
                    HalconWindow.SetWindowParam("flush", "true");
                    HalconWindow.SetColor(HColors.OrangeRed);
                    HalconWindow.SetTposition(x0 + (int)(10 * imgscale), y0 + (int)(10 * imgscale));
                    HalconWindow.WriteString("开始绘制圆形区域，点击右键结束");
                    isDrawingRegion = true;
                    ContextMenuStrip cms = this.ContextMenuStrip;
                    ContextMenuStrip = null;
                    HalconWindow.SetColor(HColors.SpringGreen);
                    HalconWindow.SetLineWidth(2);
                    HalconWindow.DrawCircle(out double row, out double colum, out double radius);
                    HalconWindow.SetWindowParam("flush", "false");
                    isDrawingRegion = false;
                    ContextMenuStrip = cms;
                    if (radius != 0)
                    {
                        HOperatorSet.GenCircle(out HObject circle, row, colum, radius);
                        HRegions.Add(new HRegionAtt() { Region = circle, DrawColor = HColors.BlueViolet, DrawMode = "margin", LineWidth = 2 });
                        //HDrawingObject D_circle = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.CIRCLE, row, colum, radius);
                        //D_circle.SetDrawingObjectParams("color", HColors.Maroon);
                        //AttachDrawObj(D_circle);
                    }
                    DispObj();
                }
                else if (e.KeyCode == Keys.R && e.Control == true)
                {
                    HalconWindow.SetWindowParam("flush", "true");
                    HalconWindow.SetColor(HColors.OrangeRed);
                    HalconWindow.SetTposition(x0 + (int)(10 * imgscale), y0 + (int)(10 * imgscale));
                    HalconWindow.WriteString("开始绘制带角度矩形区域，点击右键结束");
                    isDrawingRegion = true;
                    ContextMenuStrip cms = this.ContextMenuStrip;
                    ContextMenuStrip = null;
                    HalconWindow.SetColor(HColors.SpringGreen);
                    HalconWindow.SetLineWidth(2);
                    HalconWindow.DrawRectangle2(out double row1, out double colum1, out double phi, out double row2, out double colum2);
                    HalconWindow.SetWindowParam("flush", "false");
                    isDrawingRegion = false;
                    ContextMenuStrip = cms;

                    if (row2 != 0 && colum2 != 0)
                    {
                        HOperatorSet.GenRectangle2(out HObject Rect, row1, colum1, phi, row2, colum2);
                        HRegions.Add(new HRegionAtt() { Region = Rect, DrawColor = HColors.BlueViolet, DrawMode = "margin", LineWidth = 2 });
                    }
                    DispObj();
                }
                else if (e.KeyCode == Keys.R)
                {
                    HalconWindow.SetWindowParam("flush", "true");
                    HalconWindow.SetColor(HColors.OrangeRed);
                    HalconWindow.SetTposition(x0 + (int)(10 * imgscale), y0 + (int)(10 * imgscale));
                    HalconWindow.WriteString("开始绘制不带角度矩形区域，点击右键结束");
                    isDrawingRegion = true;
                    ContextMenuStrip cms = this.ContextMenuStrip;
                    ContextMenuStrip = null;
                    HalconWindow.SetColor(HColors.SpringGreen);
                    HalconWindow.SetLineWidth(2);
                    HalconWindow.DrawRectangle1(out double row1, out double colum1, out double row2, out double colum2);
                    HalconWindow.SetWindowParam("flush", "false");
                    isDrawingRegion = false;
                    ContextMenuStrip = cms;

                    if (row2 != 0 && colum2 != 0)
                    {
                        HOperatorSet.GenRectangle1(out HObject Rect, row1, colum1, row2, colum2);
                        HRegions.Add(new HRegionAtt() { Region = Rect, DrawColor = HColors.BlueViolet, DrawMode = "margin", LineWidth = 2 });
                    }
                    DispObj();
                }
                else if (e.KeyCode == Keys.E)
                {
                    HalconWindow.SetWindowParam("flush", "true");
                    HalconWindow.SetColor(HColors.OrangeRed);
                    HalconWindow.SetTposition(x0 + (int)(10 * imgscale), y0 + (int)(10 * imgscale));
                    HalconWindow.WriteString("开始绘制椭圆形区域，点击右键结束");
                    isDrawingRegion = true;
                    ContextMenuStrip cms = this.ContextMenuStrip;
                    ContextMenuStrip = null;
                    HalconWindow.SetColor(HColors.SpringGreen);
                    HalconWindow.SetLineWidth(2);
                    HalconWindow.DrawEllipse(out double row1, out double colum1, out double phi, out double row2, out double colum2);
                    HalconWindow.SetWindowParam("flush", "false");
                    isDrawingRegion = false;
                    ContextMenuStrip = cms;

                    if (row2 != 0 && colum2 != 0)
                    {
                        HOperatorSet.GenEllipse(out HObject Ellipse, row1, colum1, phi, row2, colum2);
                        HRegions.Add(new HRegionAtt() { Region = Ellipse, DrawColor = HColors.BlueViolet, DrawMode = "margin", LineWidth = 2 });
                    }
                    DispObj();
                }
                else if (e.KeyCode == Keys.L)
                {
                    HalconWindow.SetWindowParam("flush", "true");
                    HalconWindow.SetColor(HColors.OrangeRed);
                    HalconWindow.SetTposition(x0 + (int)(10 * imgscale), y0 + (int)(10 * imgscale));
                    HalconWindow.WriteString("开始绘线区域，点击右键结束");
                    isDrawingRegion = true;
                    ContextMenuStrip cms = this.ContextMenuStrip;
                    ContextMenuStrip = null;
                    HalconWindow.SetColor(HColors.SpringGreen);
                    HalconWindow.SetLineWidth(2);
                    HalconWindow.DrawLine(out double row1, out double colum1, out double row2, out double colum2);
                    HalconWindow.SetWindowParam("flush", "false");
                    isDrawingRegion = false;
                    ContextMenuStrip = cms;

                    if (row2 != 0 && colum2 != 0)
                    {
                        HOperatorSet.GenRegionLine(out HObject Line, row1, colum1, row2, colum2);
                        HRegions.Add(new HRegionAtt() { Region = Line, DrawColor = HColors.BlueViolet, DrawMode = "margin", LineWidth = 2 });
                    }
                    DispObj();
                }
                else if (e.KeyCode == Keys.S)
                {
                    HalconWindow.SetWindowParam("flush", "true");
                    HalconWindow.SetColor(HColors.OrangeRed);
                    HalconWindow.SetTposition(x0 + (int)(10 * imgscale), y0 + (int)(10 * imgscale));
                    HalconWindow.WriteString("开始绘制任意区域，点击右键结束");
                    isDrawingRegion = true;
                    ContextMenuStrip cms = this.ContextMenuStrip;
                    ContextMenuStrip = null;
                    HalconWindow.SetColor(HColors.SpringGreen);
                    HalconWindow.SetLineWidth(2);
                    HRegion hRegion = HalconWindow.DrawRegion();
                    HalconWindow.SetWindowParam("flush", "false");
                    isDrawingRegion = false;
                    ContextMenuStrip = cms;

                    if (hRegion.Area > 0)
                    {
                        HRegions.Add(new HRegionAtt() { Region = hRegion, DrawColor = HColors.BlueViolet, DrawMode = "margin", LineWidth = 2 });
                        DispObj();
                    }

                }
               
            }


            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            isCtrl = isAlt = false;
            base.OnKeyUp(e);
        }
        private void AttachDrawObj(HDrawingObject obj)
        {
            D_objects.Add(obj);
            // The HALCON/C# interface offers convenience methods that
            // encapsulate the set_drawing_object_callback operator.
            obj.OnDrag(UpdateDrawObj);
            obj.OnAttach(UpdateDrawObj);
            obj.OnResize(UpdateDrawObj);
            obj.OnSelect(OnSelectDrawingObject);
            obj.OnDetach(UpdateDrawObj);
            if (selected_drawing_object == null)
            { selected_drawing_object = obj; }
            HalconWindow.AttachDrawingObjectToWindow(obj);
        }
        private void OnSelectDrawingObject(HDrawingObject dobj, HWindow hwin, string type)
        {
            selected_drawing_object = dobj;
            UpdateDrawObj(dobj, hwin, type);
        }
        private void UpdateDrawObj(HDrawingObject dobj, HWindow hwin, string type)
        {
            isDragObj = true;
            Console.WriteLine(type);
        }

        private void AdaptiveImage()
        {
            HalconWindow.SetPart(0, 0, -2, -2);
            HalconWindow.GetPart(out int x0, out int _, out int x1, out int _);
            imgscale = (x1 - x0) / (float)(Height);
        }


        public void DispObj()
        {
            //解决移动图像闪烁问题
            HalconWindow.ClearWindow();
            if (HImage != null && HImage.IsInitialized())
            {
                HalconWindow.DispObj(HImage);
            }

            for (int i = 0; i < HRegions.Count; i++)
            {
                if (HRegions[i].Region != null)
                {
                    HalconWindow.SetLineStyle(new HTuple());
                    HalconWindow.SetLineWidth(HRegions[i].LineWidth);
                    HalconWindow.SetDraw(HRegions[i].DrawMode);
                    HalconWindow.SetColor(HRegions[i].DrawColor);
                    HalconWindow.DispObj(HRegions[i].Region);

                }
            }

            for (int i = 0; i < HXLDs.Count; i++)
            {
                if (HXLDs[i].Xld != null)
                {
                    HalconWindow.SetColor(HXLDs[i].DrawColor);
                    HalconWindow.SetLineWidth(HXLDs[i].LineWidth);
                    HalconWindow.SetLineStyle(new HTuple());
                    HalconWindow.DispObj(HXLDs[i].Xld);
                }
            }

            for (int i = 0; i < HTexts.Count; i++)
            {
                HalconWindow.SetColor(HTexts[i].DrawColor);
                HalconWindow.GetPart(out int x0, out int y0, out int x1, out int y1);
                if (HTexts[i].IsFixed)
                {
                    HalconWindow.SetTposition(x0 + (int)(HTexts[i].DrawLocation.Y * imgscale), y0 + (int)(HTexts[i].DrawLocation.X * imgscale));
                }
                else
                {
                    HalconWindow.SetTposition(HTexts[i].DrawLocation.Y, HTexts[i].DrawLocation.X);
                }

                HalconWindow.WriteString(HTexts[i].Text);
            }

            if (SelectRegionXLD != null)
            {
                HalconWindow.SetColor("green");
                HalconWindow.SetLineWidth(2);
                HalconWindow.SetLineStyle(new HTuple(10, 5));
                HalconWindow.DispObj(SelectRegionXLD);
            }
            HalconWindow.FlushBuffer();
        }

        /// <summary>
        /// 保存窗口图像
        /// </summary>
        /// <param name="device">文件类型(png/bmp/jpeg/tiff)</param>
        /// <param name="path">路径</param>
        public void DumpWindowImg(string device, string path)
        {
            HOperatorSet.DumpWindow(this.HalconID, device, path);
        }
        private void SelectRegion(int position_X, int position_Y)
        {
            List<HTuple> selectIndexes = new List<HTuple>();
            List<HObject> selectRegions = new List<HObject>();
            foreach (var item in HRegions)
            {
                HOperatorSet.GetRegionIndex(item.Region, position_Y, position_X, out HTuple index);

                if (index > 0)
                {
                    selectIndexes.Add(index);
                    selectRegions.Add(item.Region);

                }
            }
            if (selectRegions.Count > 0)
            {
                if (regionFrm == null || regionFrm.IsDisposed)
                {
                    regionFrm = new HFeaturesFrm(this);
                    regionFrm.Location = this.PointToScreen(new Point(-regionFrm.Width, 0));
                    if (regionFrm.Left < 0)
                    {
                        regionFrm.Left = 0;
                    }
                }
                regionFrm.Show();
                //regionFrm.Focus();
                List<int> areas = new List<int>();
                foreach (HObject item in selectRegions)
                {
                    HOperatorSet.AreaCenter(item, out HTuple area, out HTuple x, out HTuple y);
                    areas.Add(area.I);
                }
                int min = areas.Min();
                int index = areas.IndexOf(min);

                HOperatorSet.SelectObj(selectRegions[index], out selectRegion, selectIndexes[index]);
                HOperatorSet.GenContourRegionXld(selectRegion, out SelectRegionXLD, "border");

                DispObj();
                ShowFeature?.Invoke(_himg, selectRegion);
            }
            else
            {
                SelectRegionXLD = null;
                DispObj();
            }
        }
        public void SaveRegion(string path)
        {
            HRegion region = new HRegion();
            region.GenEmptyObj();
            foreach (var item in HRegions)
            {
                region = region.ConcatObj(new HRegion(item.Region));
            }
            region.WriteRegion(path);
        }
        public void SaveUnionRegion(string path)
        {
            HRegion region = new HRegion();
            region.GenEmptyObj();
            foreach (var item in HRegions)
            {
                region = region.ConcatObj(new HRegion(item.Region));
            }
            region = region.Union1();
            region.WriteRegion(path);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.window != null)
            {
                this.window.Dispose();
                this.window = null;
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
    public delegate void ShowFeatureHandler(HObject img, HObject region);
}
