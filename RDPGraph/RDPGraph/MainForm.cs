using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;

using ScottPlot;
using System.Collections;

namespace RDPGraph
{

    public partial class MainForm : MaterialForm
    {
        private Class_ScottPlotGraph GraphGraph = new Class_ScottPlotGraph();
        private string str_FilePath;


        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo900, Primary.Indigo500, Accent.Red200, TextShade.WHITE);

            Main_Graph.Plot.Style(Style.Gray1);
        }

        private void BT_FileLoad_Click(object sender, EventArgs e)
        {
            try
            {
                this.str_FilePath = TF_FilePath.Text;

                //데이터 뽑기 1. Header, Data 뽑아오기
                var reader = new System.IO.StreamReader(str_FilePath,Encoding.Default);

                ArrayList HeaderList = new ArrayList();
                ArrayList DataList = new ArrayList();

                var line = reader.ReadLine();
                string[] parts = line.Split(',');
                
                foreach(string part in parts)
                {
                    HeaderList.Add(part);
                }

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    parts = line.Split(',');

                    foreach (string part in parts)
                    {
                        DataList.Add(part);
                    }
                }

                //데이터 뽑기 2. 데이터 정리해서 Graph Class에 넣기

                for(var i = 0; i < HeaderList.Count; i++)
                {
                    Class_ScottPlotGraph_AxisData temp = new Class_ScottPlotGraph_AxisData();

                    temp.Axis_Header = (string)HeaderList[i]; //Header 데이터 넣기
                    
                    ArrayList tempAxisDataList = new ArrayList();
                    for (var j = 0 ; j< DataList.Count; j++)
                    {    
                        if(j % HeaderList.Count == i) //i번째 Header의 데이터인경우
                        {
                            tempAxisDataList.Add(DataList[j]);
                        }
                    }
                    temp.SetAxisData(tempAxisDataList);

                    GraphGraph.AxisList.Add(temp);
                }

                GUI_RefreshComboBox();

                string filename_temp = str_FilePath.Split('\\').Last();
                MessageBox.Show(filename_temp + " Load Complete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void GUI_RefreshComboBox()
        {
            CB_XAxis.Items.Clear();
            CB_YAxis.Items.Clear();

            foreach (Class_ScottPlotGraph_AxisData data in this.GraphGraph.AxisList)
            {
                CB_XAxis.Items.Add(data.Axis_Header);
                CB_YAxis.Items.Add(data.Axis_Header);
            }

            CB_XAxis.SelectedIndex = 0;
            CB_YAxis.SelectedIndex = 1;
        }

        private void BT_FindFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TF_FilePath.Text = openFileDialog.FileName;
                    this.str_FilePath = TF_FilePath.Text;

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BT_AddGraph_Click(object sender, EventArgs e)
        {

            int HeaderIndex_X = CB_XAxis.SelectedIndex;
            int HeaderIndex_Y = CB_YAxis.SelectedIndex;

            Class_ScottPlotGraph_AxisData AxisX = GraphGraph.AxisList.ElementAt(HeaderIndex_X);
            Class_ScottPlotGraph_AxisData AxisY = GraphGraph.AxisList.ElementAt(HeaderIndex_Y);

            Main_Graph.Plot.Title(str_FilePath.Split('\\').Last());
            Main_Graph.Plot.XLabel(AxisX.Axis_Header);

            Main_Graph.Plot.AddScatterLines(AxisX.Axis_Data, AxisY.Axis_Data, label: AxisY.Axis_Header);
            Main_Graph.Plot.Legend();

            Main_Graph.Refresh();
        }

        private void BT_ClearGraph_Click(object sender, EventArgs e)
        {
            Main_Graph.Plot.XLabel("");
            Main_Graph.Plot.YLabel("");
            Main_Graph.Plot.Clear();
            Main_Graph.Refresh();
        }
    }
}
