namespace ImageRotation3D
{
    partial class Base
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.newBackImage = new System.Windows.Forms.Button();
            this.newOverlayImage = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 52);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1088, 552);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // newBackImage
            // 
            this.newBackImage.Location = new System.Drawing.Point(12, 12);
            this.newBackImage.Name = "newBackImage";
            this.newBackImage.Size = new System.Drawing.Size(111, 23);
            this.newBackImage.TabIndex = 1;
            this.newBackImage.Text = "New back image";
            this.newBackImage.UseVisualStyleBackColor = true;
            this.newBackImage.Click += new System.EventHandler(this.NewBackImage_Click);
            // 
            // newOverlayImage
            // 
            this.newOverlayImage.Location = new System.Drawing.Point(137, 12);
            this.newOverlayImage.Name = "newOverlayImage";
            this.newOverlayImage.Size = new System.Drawing.Size(111, 23);
            this.newOverlayImage.TabIndex = 2;
            this.newOverlayImage.Text = "New overlay image";
            this.newOverlayImage.UseVisualStyleBackColor = true;
            this.newOverlayImage.Click += new System.EventHandler(this.NewOverlayImage_Click);
            // 
            // Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 604);
            this.Controls.Add(this.newOverlayImage);
            this.Controls.Add(this.newBackImage);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Base";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image 3D Rotation";
            this.Load += new System.EventHandler(this.Base_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button newBackImage;
        private System.Windows.Forms.Button newOverlayImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.PictureBox pictureBox1;
    }
}

