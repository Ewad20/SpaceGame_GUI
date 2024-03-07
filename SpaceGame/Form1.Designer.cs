namespace SpaceGame
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.WindowPanel = new System.Windows.Forms.Panel();
            this.pictureEnemy2 = new System.Windows.Forms.PictureBox();
            this.pictureEnemy = new System.Windows.Forms.PictureBox();
            this.PictureShip = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.WindowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEnemy2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEnemy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureShip)).BeginInit();
            this.SuspendLayout();
            // 
            // WindowPanel
            // 
            this.WindowPanel.BackColor = System.Drawing.SystemColors.Window;
            this.WindowPanel.Controls.Add(this.pictureEnemy2);
            this.WindowPanel.Controls.Add(this.pictureEnemy);
            this.WindowPanel.Controls.Add(this.PictureShip);
            this.WindowPanel.Location = new System.Drawing.Point(34, 27);
            this.WindowPanel.Name = "WindowPanel";
            this.WindowPanel.Size = new System.Drawing.Size(842, 520);
            this.WindowPanel.TabIndex = 1;
            this.WindowPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.WindowPanel_Paint);
            // 
            // pictureEnemy2
            // 
            this.pictureEnemy2.Image = global::SpaceGame.Properties.Resources.enemy2;
            this.pictureEnemy2.Location = new System.Drawing.Point(529, 141);
            this.pictureEnemy2.Name = "pictureEnemy2";
            this.pictureEnemy2.Size = new System.Drawing.Size(56, 52);
            this.pictureEnemy2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureEnemy2.TabIndex = 2;
            this.pictureEnemy2.TabStop = false;
            // 
            // pictureEnemy
            // 
            this.pictureEnemy.Image = global::SpaceGame.Properties.Resources.enemy;
            this.pictureEnemy.Location = new System.Drawing.Point(365, 91);
            this.pictureEnemy.Name = "pictureEnemy";
            this.pictureEnemy.Size = new System.Drawing.Size(56, 52);
            this.pictureEnemy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureEnemy.TabIndex = 1;
            this.pictureEnemy.TabStop = false;
            // 
            // PictureShip
            // 
            this.PictureShip.Image = global::SpaceGame.Properties.Resources.pobrane;
            this.PictureShip.Location = new System.Drawing.Point(410, 283);
            this.PictureShip.Name = "PictureShip";
            this.PictureShip.Size = new System.Drawing.Size(70, 71);
            this.PictureShip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureShip.TabIndex = 0;
            this.PictureShip.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(31, 568);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Health";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(83, 569);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(246, 15);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Value = 100;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(623, 568);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(246, 15);
            this.progressBar2.TabIndex = 5;
            this.progressBar2.Value = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(498, 563);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Health enemy1";
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(623, 597);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(246, 15);
            this.progressBar3.TabIndex = 7;
            this.progressBar3.Value = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label3.Location = new System.Drawing.Point(498, 596);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Health enemy2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(913, 631);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WindowPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEnemy2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEnemy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureShip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureShip;
        private System.Windows.Forms.Panel WindowPanel;
        private System.Windows.Forms.PictureBox pictureEnemy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureEnemy2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label3;
    }
}

