namespace Szyfr_RSA
{
    partial class generator_klucza
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LoadP = new System.Windows.Forms.Button();
            this.LoadQ = new System.Windows.Forms.Button();
            this.textBoxQ = new System.Windows.Forms.TextBox();
            this.openFileDialogP = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogQ = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generuj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Podaj 2 liczby pierwsze o podobnej liczbie bitów";
            // 
            // textBoxP
            // 
            this.textBoxP.Location = new System.Drawing.Point(32, 41);
            this.textBoxP.Name = "textBoxP";
            this.textBoxP.Size = new System.Drawing.Size(190, 20);
            this.textBoxP.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "p";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "q";
            // 
            // LoadP
            // 
            this.LoadP.Location = new System.Drawing.Point(228, 39);
            this.LoadP.Name = "LoadP";
            this.LoadP.Size = new System.Drawing.Size(167, 23);
            this.LoadP.TabIndex = 5;
            this.LoadP.Text = "Wczytaj losową liczbę z pliku";
            this.LoadP.UseVisualStyleBackColor = true;
            this.LoadP.Click += new System.EventHandler(this.LoadP_Click);
            // 
            // LoadQ
            // 
            this.LoadQ.Location = new System.Drawing.Point(228, 72);
            this.LoadQ.Name = "LoadQ";
            this.LoadQ.Size = new System.Drawing.Size(167, 23);
            this.LoadQ.TabIndex = 6;
            this.LoadQ.Text = "Wczytaj losową liczbę z pliku";
            this.LoadQ.UseVisualStyleBackColor = true;
            this.LoadQ.Click += new System.EventHandler(this.LoadQ_Click);
            // 
            // textBoxQ
            // 
            this.textBoxQ.Location = new System.Drawing.Point(32, 74);
            this.textBoxQ.Name = "textBoxQ";
            this.textBoxQ.Size = new System.Drawing.Size(190, 20);
            this.textBoxQ.TabIndex = 7;
            // 
            // openFileDialogP
            // 
            this.openFileDialogP.FileName = "openFileDialogP";
            this.openFileDialogP.Filter = "Pliki baz liczb pierwszych|*.prime";
            this.openFileDialogP.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogP_FileOk);
            // 
            // openFileDialogQ
            // 
            this.openFileDialogQ.FileName = "openFileDialogQ";
            this.openFileDialogQ.Filter = "Pliki baz liczb pierwszych|*.prime";
            this.openFileDialogQ.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogQ_FileOk);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(32, 127);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Zamaskuj dane";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // generator_klucza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 162);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBoxQ);
            this.Controls.Add(this.LoadQ);
            this.Controls.Add(this.LoadP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.MaximumSize = new System.Drawing.Size(423, 201);
            this.MinimumSize = new System.Drawing.Size(423, 201);
            this.Name = "generator_klucza";
            this.Text = "generator_klucza";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button LoadP;
        private System.Windows.Forms.Button LoadQ;
        private System.Windows.Forms.TextBox textBoxQ;
        private System.Windows.Forms.OpenFileDialog openFileDialogP;
        private System.Windows.Forms.OpenFileDialog openFileDialogQ;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}