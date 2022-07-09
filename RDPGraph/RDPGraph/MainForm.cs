﻿using System;
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

        private uint AxisData_ID_Index = 0;

        private List<Class_Mainform_ListScatterLine> ScatterLineList = new List<Class_Mainform_ListScatterLine>();

        private ScottPlot.Plottable.MarkerPlot HighlightedPoint;
        private ScottPlot.Plottable.Annotation HighlightedPoint_Label;
        private int LastHighlightedIndex = -1;

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
            LastHighlightedIndex = -1;

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

            HighlightedPoint_Label = Main_Graph.Plot.AddAnnotation("", 0, 0);
            HighlightedPoint_Label.Font.Size = 20;
            HighlightedPoint_Label.Font.Name = "Impact";
            HighlightedPoint_Label.Font.Color = Color.White;
            HighlightedPoint_Label.Shadow = false;
            HighlightedPoint_Label.BackgroundColor = Color.FromArgb(25, Color.Blue);
            HighlightedPoint_Label.BorderWidth = 2;
            HighlightedPoint_Label.BorderColor = Color.DarkGray;

            HighlightedPoint_Label.X = 0;
            HighlightedPoint_Label.Y = 0;

            HighlightedPoint_Label.IsVisible = false;
        }

        private void Timer_GraphCrosshair_Tick(object sender, EventArgs e)
        {
            //임시로 ID는 0인 ScatterLine만 체크함
            ScatterLineList.ForEach(delegate (Class_Mainform_ListScatterLine Scatterline){
                if (Scatterline.id == AxisData_ID_Index - 1) 
                {
                    (double mouseCoordX, double mouseCoordY) = Main_Graph.GetMouseCoordinates();
                    double xyRatio = Main_Graph.Plot.XAxis.Dims.PxPerUnit / Main_Graph.Plot.YAxis.Dims.PxPerUnit;
                    (double pointX, double pointY, int pointIndex) = Scatterline.Data.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

                    HighlightedPoint.X = pointX;
                    HighlightedPoint.Y = pointY;
                    HighlightedPoint.IsVisible = true;

                    HighlightedPoint_Label.IsVisible = true;

                    if (LastHighlightedIndex != pointIndex)
                    {
                        HighlightedPoint_Label.Label = "(" + Convert.ToString(pointX) + ", " + Convert.ToString(pointY) + ")";

                        LastHighlightedIndex = pointIndex;
                        Main_Graph.Render();
                        Main_Graph.Refresh();
                    }
                }
            });
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
