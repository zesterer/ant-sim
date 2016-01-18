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
				this.drawPanel = new DoubleBufferedPanel();
	            this.pauseButton = new System.Windows.Forms.Button();
				this.clearAntsButton = new System.Windows.Forms.Button();
				this.clearFoodButton = new System.Windows.Forms.Button();
				this.clearNestsButton = new System.Windows.Forms.Button();
	            this.simulationTimer = new System.Windows.Forms.Timer(this.components);
	            this.SuspendLayout();

				this.drawPanel.Location = new System.Drawing.Point(13, 49);
				this.drawPanel.Name = "drawPanel";
				this.drawPanel.Size = new System.Drawing.Size(438, 554);
				this.drawPanel.TabIndex = 0;
				this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanelPaint);
				this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanelClick);

	            this.pauseButton.BackColor = System.Drawing.SystemColors.ButtonFace;
	            this.pauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
	            this.pauseButton.Location = new System.Drawing.Point(13, 13);
	            this.pauseButton.Name = "pauseButton";
				this.pauseButton.Text = "Pause";
	            this.pauseButton.Size = new System.Drawing.Size(65, 30);
	            this.pauseButton.TabIndex = 1;
	            //this.pauseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
	            this.pauseButton.UseVisualStyleBackColor = false;
	            this.pauseButton.Click += new System.EventHandler(this.pauseButtonClick);

				this.clearAntsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
				this.clearAntsButton.Location = new System.Drawing.Point(91, 13);
				this.clearAntsButton.Name = "clearAntsButton";
				this.clearAntsButton.Size = new System.Drawing.Size(65, 30);
				this.clearAntsButton.TabIndex = 2;
				this.clearAntsButton.Text = "Clear Ants";
				this.clearAntsButton.UseVisualStyleBackColor = false;
				this.clearAntsButton.Click += new System.EventHandler(this.clearAntsButtonClick);

				this.clearFoodButton.BackColor = System.Drawing.SystemColors.ButtonFace;
				this.clearFoodButton.Location = new System.Drawing.Point(169, 13);
				this.clearFoodButton.Name = "clearFoodButton";
				this.clearFoodButton.Size = new System.Drawing.Size(65, 30);
				this.clearFoodButton.TabIndex = 3;
				this.clearFoodButton.Text = "Clear Food";
				this.clearFoodButton.UseVisualStyleBackColor = false;
				this.clearFoodButton.Click += new System.EventHandler(this.clearFoodButtonClick);

				this.clearNestsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
				this.clearNestsButton.Location = new System.Drawing.Point(247, 13);
				this.clearNestsButton.Name = "clearNestsButton";
				this.clearNestsButton.Size = new System.Drawing.Size(65, 30);
				this.clearNestsButton.TabIndex = 3;
				this.clearNestsButton.Text = "Clear Nests";
				this.clearNestsButton.UseVisualStyleBackColor = false;
				this.clearNestsButton.Click += new System.EventHandler(this.clearNestsButtonClick);

	            this.simulationTimer.Enabled = true;
	            this.simulationTimer.Interval = 25;
	            this.simulationTimer.Tick += new System.EventHandler(this.simulationTimer_Tick);

	            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	            this.ClientSize = new System.Drawing.Size(466, 620);
				this.Controls.Add(this.clearNestsButton);
				this.Controls.Add(this.clearFoodButton);
				this.Controls.Add(this.clearAntsButton);
	            this.Controls.Add(this.pauseButton);
	            this.Controls.Add(this.drawPanel);
	            this.Name = "anti-sim";
	            this.Text = "Ant Simulation";
	            this.ResumeLayout(false);
            }

            #endregion

			private DoubleBufferedPanel drawPanel;
            private System.Windows.Forms.Button pauseButton;
            private System.Windows.Forms.Button clearAntsButton;
            private System.Windows.Forms.Button clearFoodButton;
			private System.Windows.Forms.Button clearNestsButton;
            private System.Windows.Forms.Timer simulationTimer;
        }
    }
}