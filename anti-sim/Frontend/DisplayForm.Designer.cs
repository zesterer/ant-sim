using System.Windows.Forms;

namespace AntSim
{
    namespace Frontend
    {
        partial class DisplayForm
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
	            this.components = new System.ComponentModel.Container();
	            this.drawPanel = new System.Windows.Forms.Panel();
	            this.pauseButton = new System.Windows.Forms.Button();
	            this.button2 = new System.Windows.Forms.Button();
	            this.button3 = new System.Windows.Forms.Button();
	            this.simulationTimer = new System.Windows.Forms.Timer(this.components);
	            this.SuspendLayout();
	            // 
				// drawPanel
	            // 
				this.drawPanel.Location = new System.Drawing.Point(13, 49);
				this.drawPanel.Name = "drawPanel";
				this.drawPanel.Size = new System.Drawing.Size(695, 432);
				this.drawPanel.TabIndex = 0;
				this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanelPaint);
				this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanelClick);
	            // 
	            // pauseButton
	            // 
	            this.pauseButton.BackColor = System.Drawing.SystemColors.ButtonFace;
	            this.pauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
	            this.pauseButton.Location = new System.Drawing.Point(13, 13);
	            this.pauseButton.Name = "pauseButton";
				this.pauseButton.Text = "Pause";
	            this.pauseButton.Size = new System.Drawing.Size(60, 30);
	            this.pauseButton.TabIndex = 1;
	            //this.pauseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
	            this.pauseButton.UseVisualStyleBackColor = false;
	            this.pauseButton.Click += new System.EventHandler(this.pauseButtonClick);
	            // 
	            // button2
	            // 
	            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
	            this.button2.Location = new System.Drawing.Point(86, 13);
	            this.button2.Name = "button2";
	            this.button2.Size = new System.Drawing.Size(60, 30);
	            this.button2.TabIndex = 2;
	            this.button2.Text = "a test";
	            this.button2.UseVisualStyleBackColor = false;
	            // 
	            // button3
	            // 
	            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
	            this.button3.Location = new System.Drawing.Point(159, 13);
	            this.button3.Name = "button3";
	            this.button3.Size = new System.Drawing.Size(60, 30);
	            this.button3.TabIndex = 3;
	            this.button3.Text = "button";
	            this.button3.UseVisualStyleBackColor = false;
	            // 
	            // simulationTimer
	            // 
	            this.simulationTimer.Enabled = true;
	            this.simulationTimer.Interval = 25;
	            this.simulationTimer.Tick += new System.EventHandler(this.simulationTimer_Tick);
	            // 
	            // DisplayForm
	            // 
	            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	            this.ClientSize = new System.Drawing.Size(720, 493);
	            this.Controls.Add(this.button3);
	            this.Controls.Add(this.button2);
	            this.Controls.Add(this.pauseButton);
	            this.Controls.Add(this.drawPanel);
	            this.Name = "DisplayForm";
	            this.Text = "DisplayForm";
	            this.ResumeLayout(false);
            }

            #endregion

            private System.Windows.Forms.Panel drawPanel;
            private System.Windows.Forms.Button pauseButton;
            private System.Windows.Forms.Button button2;
            private System.Windows.Forms.Button button3;
            private System.Windows.Forms.Timer simulationTimer;
        }
    }
}