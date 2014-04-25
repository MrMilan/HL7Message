namespace HL7Message
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.btnRead = new System.Windows.Forms.Button();
            this.grafikVseho = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cLB = new System.Windows.Forms.CheckedListBox();
            this.lblCasi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grafikVseho)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(12, 384);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "Nacti";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // grafikVseho
            // 
            chartArea1.Name = "ChartArea1";
            this.grafikVseho.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.grafikVseho.Legends.Add(legend1);
            this.grafikVseho.Location = new System.Drawing.Point(12, 12);
            this.grafikVseho.Name = "grafikVseho";
            this.grafikVseho.Size = new System.Drawing.Size(1457, 366);
            this.grafikVseho.TabIndex = 1;
            this.grafikVseho.Text = "chart1";
            // 
            // cLB
            // 
            this.cLB.BackColor = System.Drawing.Color.Wheat;
            this.cLB.CheckOnClick = true;
            this.cLB.Enabled = false;
            this.cLB.FormattingEnabled = true;
            this.cLB.Location = new System.Drawing.Point(514, 384);
            this.cLB.Name = "cLB";
            this.cLB.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cLB.ScrollAlwaysVisible = true;
            this.cLB.Size = new System.Drawing.Size(122, 214);
            this.cLB.TabIndex = 2;
            this.cLB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cLB_MouseUp);
            // 
            // lblCasi
            // 
            this.lblCasi.AutoSize = true;
            this.lblCasi.Location = new System.Drawing.Point(104, 389);
            this.lblCasi.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblCasi.Name = "lblCasi";
            this.lblCasi.Size = new System.Drawing.Size(49, 13);
            this.lblCasi.TabIndex = 3;
            this.lblCasi.Text = "--------------";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1276, 500);
            this.ClientSize = new System.Drawing.Size(1174, 653);
            this.Controls.Add(this.lblCasi);
            this.Controls.Add(this.cLB);
            this.Controls.Add(this.grafikVseho);
            this.Controls.Add(this.btnRead);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grafikVseho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.DataVisualization.Charting.Chart grafikVseho;
        private System.Windows.Forms.CheckedListBox cLB;
        private System.Windows.Forms.Label lblCasi;
    }
}

