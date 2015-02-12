namespace ocr_pdf
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
            this.button1 = new System.Windows.Forms.Button();
            this.btn_edit_conf = new System.Windows.Forms.Button();
            this.chkbx_delete = new System.Windows.Forms.CheckBox();
            this.prgbar_tot = new System.Windows.Forms.ProgressBar();
            this.lbl_progrtot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(72, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Scegli file PDF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_edit_conf
            // 
            this.btn_edit_conf.Location = new System.Drawing.Point(14, 179);
            this.btn_edit_conf.Name = "btn_edit_conf";
            this.btn_edit_conf.Size = new System.Drawing.Size(267, 24);
            this.btn_edit_conf.TabIndex = 1;
            this.btn_edit_conf.Text = "Visualizza / modifica configurazione";
            this.btn_edit_conf.UseVisualStyleBackColor = true;
            this.btn_edit_conf.Click += new System.EventHandler(this.btn_edit_conf_Click);
            // 
            // chkbx_delete
            // 
            this.chkbx_delete.AutoSize = true;
            this.chkbx_delete.Location = new System.Drawing.Point(90, 102);
            this.chkbx_delete.Name = "chkbx_delete";
            this.chkbx_delete.Size = new System.Drawing.Size(120, 17);
            this.chkbx_delete.TabIndex = 2;
            this.chkbx_delete.Text = "Elimina file di origine";
            this.chkbx_delete.UseVisualStyleBackColor = true;
            // 
            // prgbar_tot
            // 
            this.prgbar_tot.Location = new System.Drawing.Point(14, 141);
            this.prgbar_tot.Name = "prgbar_tot";
            this.prgbar_tot.Size = new System.Drawing.Size(267, 22);
            this.prgbar_tot.TabIndex = 3;
            // 
            // lbl_progrtot
            // 
            this.lbl_progrtot.AutoSize = true;
            this.lbl_progrtot.Location = new System.Drawing.Point(11, 125);
            this.lbl_progrtot.Name = "lbl_progrtot";
            this.lbl_progrtot.Size = new System.Drawing.Size(23, 13);
            this.lbl_progrtot.TabIndex = 5;
            this.lbl_progrtot.Text = "File";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 216);
            this.Controls.Add(this.lbl_progrtot);
            this.Controls.Add(this.prgbar_tot);
            this.Controls.Add(this.chkbx_delete);
            this.Controls.Add(this.btn_edit_conf);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "OCR file PDF";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_edit_conf;
        private System.Windows.Forms.CheckBox chkbx_delete;
        private System.Windows.Forms.ProgressBar prgbar_tot;
        private System.Windows.Forms.Label lbl_progrtot;
    }
}

