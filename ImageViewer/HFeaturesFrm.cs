using HalconDotNet;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class HFeaturesFrm : Form
    {
        private ImgBox imgBox;
        private HObject Img;
        private HObject Hregion;
        public HFeaturesFrm(ImgBox parent)
        {
            InitializeComponent();
            imgBox = parent;
            Hregion = parent.selectRegion;
            Img = parent.HImage;
            TopMost = true;
        }

        private void HRegionFeaturesFrm_Load(object sender, EventArgs e)
        {
            imgBox.ShowFeature = new ShowFeatureHandler((img, reg) =>
            {
                Img = img;
                Hregion = reg;
                UpdateListView();
            });
            foreach (TreeNode item in treeView1.Nodes)
            {
                item.Expand();
            }
        }

        private HTuple GetFeature(string featureName)
        {
            if (Hregion == null)
            {
                return new HTuple();
            }
            HOperatorSet.RegionFeatures(Hregion, featureName, out HTuple feature);
            return feature;
        }
        private HTuple GetGrayFeature(string featureName)
        {
            if (Hregion == null)
            {
                return new HTuple();
            }
            HOperatorSet.GrayFeatures(Hregion, Img, featureName, out HTuple feature);
            return feature;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                UpdateListView();
            }
            else
            {
                treeView1.AfterCheck -= treeView1_AfterCheck;
                foreach (TreeNode item in e.Node.Nodes)
                {
                    item.Checked = e.Node.Checked;
                    if (item.Nodes.Count > 0)
                    {
                        foreach (TreeNode node in item.Nodes)
                        {
                            node.Checked = e.Node.Checked;
                        }
                    }
                }
                treeView1.AfterCheck += treeView1_AfterCheck;
                UpdateListView();
            }
        }

        private void UpdateListView()
        {
            listView1.Items.Clear();
            AddRegionListView(treeView1.Nodes[0]);
            AddGrayListView(treeView1.Nodes[1]);
        }

        private void AddRegionListView(TreeNode nodes)
        {
            foreach (TreeNode item in nodes.Nodes)
            {
                if (item.Tag != null && item.Checked)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.ForeColor = Color.Black;
                    lvitem.SubItems[0].Text = item.Text;
                    HTuple t = GetFeature(item.Tag.ToString());
                    lvitem.SubItems.Add(t.ToString());
                    listView1.Items.Add(lvitem);

                }
                if (item.Nodes.Count > 0)
                {
                    AddRegionListView(item);
                }
            }
        }

        private void AddGrayListView(TreeNode nodes)
        {
            foreach (TreeNode item in nodes.Nodes)
            {
                if (item.Tag != null && item.Checked)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.ForeColor = Color.DimGray;
                    lvitem.SubItems[0].Text = item.Text;
                    HTuple t = GetGrayFeature(item.Tag.ToString());
                    lvitem.SubItems.Add(t.ToString());
                    listView1.Items.Add(lvitem);

                }
                if (item.Nodes.Count > 0)
                {
                    AddGrayListView(item);
                }
            }
        }
        private void HRegionFeaturesFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            imgBox.ShowFeature = null;
        }
    }
}
