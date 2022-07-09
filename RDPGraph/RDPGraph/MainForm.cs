using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
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

        private uint AxisData_ID_Index = 0;

        private List<Class_Mainform_ListScatterLine> ScatterLineList = new List<Class_Mainform_ListScatterLine>();

        private ScottPlot.Plottable.MarkerPlot HighlightedPoint;
        private int LastHighlightedIndex = -1;
        private uint LastHighlightedID = 0xFFFFFFFF;


        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo900, Primary.Indigo500, Accent.Red200, TextShade.WHITE);

            Main_Graph.Plot.Style(Style.Gray1);
            //GUI_InitHighlightPointer();
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
                
                GraphGraph.AxisList.Clear(); //기존 데이터 정리

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
                openFileDialog.Filter = "CSV File (*.csv) | *.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TF_FilePath.Text = openFileDialog.FileName;
                    this.str_FilePath = TF_FilePath.Text;

                    this.BT_FileLoad_Click(sender, new EventArgs());
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

            ScatterLineList.Add(new Class_Mainform_ListScatterLine(AxisData_ID_Index++, Main_Graph.Plot.AddScatter(AxisX.Axis_Data, AxisY.Axis_Data, label: AxisY.Axis_Header)));
            Main_Graph.Plot.Legend();
            
            Main_Graph.Plot.AxisAuto();
            Main_Graph.Refresh();

            GUI_InitHighlightPointer();
            Timer_GraphCrosshair.Start();
        }

        private void BT_ClearGraph_Click(object sender, EventArgs e)
        {
            ScatterLineList.Clear();
            GUI_ResetHighlightPointer();

            Main_Graph.Plot.XLabel("");
            Main_Graph.Plot.YLabel("");
            Main_Graph.Plot.Clear();
            Main_Graph.Refresh();

            Timer_GraphCrosshair.Stop();
        }

        private void GUI_InitHighlightPointer()
        {
            HighlightedPoint = Main_Graph.Plot.AddPoint(0, 0);
            HighlightedPoint.Color = Color.Red;
            HighlightedPoint.MarkerSize = 10;
            HighlightedPoint.MarkerShape = ScottPlot.MarkerShape.openCircle;
            HighlightedPoint.IsVisible = false;
        }
        private void GUI_ResetHighlightPointer()
        {
            LastHighlightedIndex = -1;
            LastHighlightedID = 0xFFFFFFFF;
        }

        private double Scottplot_GetDistance(double x1, double x2, double y1, double y2)
        {
            double distance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2))); ;

            return distance;
        }

        private void Timer_GraphCrosshair_Tick(object sender, EventArgs e)
        {
            (double mouseCoordX, double mouseCoordY) = Main_Graph.GetMouseCoordinates();
            (double min_pointX, double min_pointY, int min_pointIndex) = ScatterLineList.ElementAt(0).Data.GetPointNearest(mouseCoordX, mouseCoordY);
            double xyRatio = Main_Graph.Plot.XAxis.Dims.PxPerUnit / Main_Graph.Plot.YAxis.Dims.PxPerUnit;

            double min_dist = Scottplot_GetDistance(min_pointX,mouseCoordX,min_pointY,mouseCoordY);
            uint min_id = ScatterLineList.ElementAt(0).id;

            ScatterLineList.ForEach(delegate (Class_Mainform_ListScatterLine Scatterline){
                (double pointX, double pointY, int pointIndex) = Scatterline.Data.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);
                double temp_dist = Scottplot_GetDistance(pointX,mouseCoordX,pointY,mouseCoordY);
                if(temp_dist < min_dist)
                {
                    min_dist = temp_dist;
                    min_pointX = pointX;
                    min_pointY = pointY;
                    min_pointIndex = pointIndex;
                    min_id = Scatterline.id;
                }
            });

            HighlightedPoint.X = min_pointX;
            HighlightedPoint.Y = min_pointY;
            HighlightedPoint.IsVisible = true;

            TF_PosX.Text = Convert.ToString(min_pointX);
            TF_PosY.Text = Convert.ToString(min_pointY);
            
            if ((LastHighlightedID != min_id) || (LastHighlightedIndex != min_pointIndex))
            {
                LastHighlightedID = min_id;
                LastHighlightedIndex = min_pointIndex;
                Main_Graph.Render();
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    TF_FilePath.Text = file;
                    this.BT_FileLoad_Click(sender, new EventArgs());
                    return;
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }

    public class Class_Mainform_ListScatterLine
    {
        public uint id;
        public ScottPlot.Plottable.ScatterPlot Data;

        public Class_Mainform_ListScatterLine(uint id, ScottPlot.Plottable.ScatterPlot Data)
        {
            this.id = id;
            this.Data = Data;
        }
    }
}
