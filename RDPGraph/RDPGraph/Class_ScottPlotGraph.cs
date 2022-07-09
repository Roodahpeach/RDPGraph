using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RDPGraph
{
    internal class Class_ScottPlotGraph
    {
        public List<Class_ScottPlotGraph_AxisData> AxisList = new List<Class_ScottPlotGraph_AxisData>(); // ArrayList로 작성하려했으나 특정 자료형을 명시해주는게 더 좋을 것 같아서 List 자료형 사용함.

        public Class_ScottPlotGraph()
        {
            return; //데이터 Set은 나중에 한다고 가정?
        }

    }

    internal class Class_ScottPlotGraph_AxisData
    {
        public string Axis_Header;
        public double[] Axis_Data;

        public Class_ScottPlotGraph_AxisData()
        {

        }

        public void SetAxisData(ArrayList Axis_Data)
        {
            this.Axis_Data = new double[Axis_Data.Count];

            for (var i = 0; i < Axis_Data.Count; i++)
            {
                this.Axis_Data[i] = Convert.ToDouble((string)Axis_Data[i]);
            }

        }
    }
}