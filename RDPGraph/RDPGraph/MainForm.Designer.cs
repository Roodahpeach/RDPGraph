namespace RDPGraph
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TF_FilePath = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.BT_FileLoad = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BT_FindFile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BT_AddGraph = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CB_YAxis = new System.Windows.Forms.ComboBox();
            this.CB_XAxis = new System.Windows.Forms.ComboBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.Main_Graph = new ScottPlot.FormsPlot();
            this.BT_ClearGraph = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Timer_GraphCrosshair = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TF_FilePath
            // 
            this.TF_FilePath.Depth = 0;
            this.TF_FilePath.Hint = "";
            this.TF_FilePath.Location = new System.Drawing.Point(6, 20);
            this.TF_FilePath.MouseState = MaterialSkin.MouseState.HOVER;
            this.TF_FilePath.Name = "TF_FilePath";
            this.TF_FilePath.PasswordChar = '\0';
            this.TF_FilePath.SelectedText = "";
            this.TF_FilePath.SelectionLength = 0;
            this.TF_FilePath.SelectionStart = 0;
            this.TF_FilePath.Size = new System.Drawing.Size(751, 23);
            this.TF_FilePath.TabIndex = 0;
            this.TF_FilePath.Text = "File Path";
            this.TF_FilePath.UseSystemPasswordChar = false;
            // 
            // BT_FileLoad
            // 
            this.BT_FileLoad.Depth = 0;
            this.BT_FileLoad.Location = new System.Drawing.Point(857, 20);
            this.BT_FileLoad.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_FileLoad.Name = "BT_FileLoad";
            this.BT_FileLoad.Primary = true;
            this.BT_FileLoad.Size = new System.Drawing.Size(92, 23);
            this.BT_FileLoad.TabIndex = 1;
            this.BT_FileLoad.Text = "File Load";
            this.BT_FileLoad.UseVisualStyleBackColor = true;
            this.BT_FileLoad.Click += new System.EventHandler(this.BT_FileLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BT_FindFile);
            this.groupBox1.Controls.Add(this.TF_FilePath);
            this.groupBox1.Controls.Add(this.BT_FileLoad);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 636);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(955, 52);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CSV File Load";
            // 
            // BT_FindFile
            // 
            this.BT_FindFile.Depth = 0;
            this.BT_FindFile.Location = new System.Drawing.Point(763, 20);
            this.BT_FindFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_FindFile.Name = "BT_FindFile";
            this.BT_FindFile.Primary = true;
            this.BT_FindFile.Size = new System.Drawing.Size(88, 23);
            this.BT_FindFile.TabIndex = 2;
            this.BT_FindFile.Text = "Find File";
            this.BT_FindFile.UseVisualStyleBackColor = true;
            this.BT_FindFile.Click += new System.EventHandler(this.BT_FindFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BT_ClearGraph);
            this.groupBox2.Controls.Add(this.BT_AddGraph);
            this.groupBox2.Controls.Add(this.CB_YAxis);
            this.groupBox2.Controls.Add(this.CB_XAxis);
            this.groupBox2.Controls.Add(this.materialLabel2);
            this.groupBox2.Controls.Add(this.materialLabel1);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(973, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 619);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // BT_AddGraph
            // 
            this.BT_AddGraph.Depth = 0;
            this.BT_AddGraph.Location = new System.Drawing.Point(21, 162);
            this.BT_AddGraph.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_AddGraph.Name = "BT_AddGraph";
            this.BT_AddGraph.Primary = true;
            this.BT_AddGraph.Size = new System.Drawing.Size(79, 52);
            this.BT_AddGraph.TabIndex = 4;
            this.BT_AddGraph.Text = "Add Graph";
            this.BT_AddGraph.UseVisualStyleBackColor = true;
            this.BT_AddGraph.Click += new System.EventHandler(this.BT_AddGraph_Click);
            // 
            // CB_YAxis
            // 
            this.CB_YAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_YAxis.FormattingEnabled = true;
            this.CB_YAxis.Location = new System.Drawing.Point(21, 118);
            this.CB_YAxis.Name = "CB_YAxis";
            this.CB_YAxis.Size = new System.Drawing.Size(173, 20);
            this.CB_YAxis.TabIndex = 3;
            // 
            // CB_XAxis
            // 
            this.CB_XAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_XAxis.Location = new System.Drawing.Point(21, 52);
            this.CB_XAxis.Name = "CB_XAxis";
            this.CB_XAxis.Size = new System.Drawing.Size(173, 20);
            this.CB_XAxis.TabIndex = 2;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(18, 97);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(50, 18);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "Y Axis";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(18, 30);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(50, 18);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "X Axis";
            // 
            // Main_Graph
            // 
            this.Main_Graph.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Main_Graph.Location = new System.Drawing.Point(18, 69);
            this.Main_Graph.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Main_Graph.Name = "Main_Graph";
            this.Main_Graph.Size = new System.Drawing.Size(948, 562);
            this.Main_Graph.TabIndex = 4;
            // 
            // BT_ClearGraph
            // 
            this.BT_ClearGraph.Depth = 0;
            this.BT_ClearGraph.Location = new System.Drawing.Point(115, 162);
            this.BT_ClearGraph.MouseState = MaterialSkin.MouseState.HOVER;
            this.BT_ClearGraph.Name = "BT_ClearGraph";
            this.BT_ClearGraph.Primary = true;
            this.BT_ClearGraph.Size = new System.Drawing.Size(79, 52);
            this.BT_ClearGraph.TabIndex = 5;
            this.BT_ClearGraph.Text = "Clear Graph";
            this.BT_ClearGraph.UseVisualStyleBackColor = true;
            this.BT_ClearGraph.Click += new System.EventHandler(this.BT_ClearGraph_Click);
            // 
            // Timer_GraphCrosshair
            // 
            this.Timer_GraphCrosshair.Tick += new System.EventHandler(this.Timer_GraphCrosshair_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.Main_Graph);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Sizable = false;
            this.Text = "CSV File Graph";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField TF_FilePath;
        private MaterialSkin.Controls.MaterialRaisedButton BT_FileLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ScottPlot.FormsPlot Main_Graph;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton BT_AddGraph;
        private System.Windows.Forms.ComboBox CB_YAxis;
        private System.Windows.Forms.ComboBox CB_XAxis;
        private MaterialSkin.Controls.MaterialRaisedButton BT_FindFile;
        private MaterialSkin.Controls.MaterialRaisedButton BT_ClearGraph;
        private System.Windows.Forms.Timer Timer_GraphCrosshair;
    }
}

