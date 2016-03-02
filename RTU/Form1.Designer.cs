namespace RTU
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxD1 = new System.Windows.Forms.TextBox();
            this.textBoxD2 = new System.Windows.Forms.TextBox();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridViewRls = new System.Windows.Forms.DataGridView();
            this.dataGridViewOp = new System.Windows.Forms.DataGridView();
            this.dataGridViewDTr = new System.Windows.Forms.DataGridView();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDTr)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование снаряда";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(147, 10);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.Text = "РС 9М215 без ТК";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Угол возвышения";
            // 
            // textBoxTet
            // 
            this.textBoxTet.Location = new System.Drawing.Point(147, 39);
            this.textBoxTet.Name = "textBoxTet";
            this.textBoxTet.Size = new System.Drawing.Size(27, 20);
            this.textBoxTet.TabIndex = 1;
            this.textBoxTet.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Диапазон направлений";
            // 
            // textBoxD1
            // 
            this.textBoxD1.Location = new System.Drawing.Point(290, 39);
            this.textBoxD1.Name = "textBoxD1";
            this.textBoxD1.Size = new System.Drawing.Size(33, 20);
            this.textBoxD1.TabIndex = 1;
            this.textBoxD1.Text = "230";
            this.textBoxD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxD2
            // 
            this.textBoxD2.Location = new System.Drawing.Point(354, 39);
            this.textBoxD2.Name = "textBoxD2";
            this.textBoxD2.Size = new System.Drawing.Size(33, 20);
            this.textBoxD2.TabIndex = 1;
            this.textBoxD2.Text = "238";
            this.textBoxD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxStep
            // 
            this.textBoxStep.Location = new System.Drawing.Point(449, 39);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(28, 20);
            this.textBoxStep.TabIndex = 1;
            this.textBoxStep.Text = "2";
            this.textBoxStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "от";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "до";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(394, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "с шагом";
            // 
            // dataGridViewRls
            // 
            this.dataGridViewRls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRls.Location = new System.Drawing.Point(12, 83);
            this.dataGridViewRls.Name = "dataGridViewRls";
            this.dataGridViewRls.Size = new System.Drawing.Size(323, 223);
            this.dataGridViewRls.TabIndex = 0;
            // 
            // dataGridViewOp
            // 
            this.dataGridViewOp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOp.Location = new System.Drawing.Point(341, 83);
            this.dataGridViewOp.Name = "dataGridViewOp";
            this.dataGridViewOp.Size = new System.Drawing.Size(323, 223);
            this.dataGridViewOp.TabIndex = 0;
            // 
            // dataGridViewDTr
            // 
            this.dataGridViewDTr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDTr.Location = new System.Drawing.Point(670, 83);
            this.dataGridViewDTr.Name = "dataGridViewDTr";
            this.dataGridViewDTr.Size = new System.Drawing.Size(206, 223);
            this.dataGridViewDTr.TabIndex = 0;
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(540, 12);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(323, 41);
            this.buttonRun.TabIndex = 4;
            this.buttonRun.Text = "Рассчетать";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Координаты РЛС";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(456, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Координаты ОП";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(716, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Данные траектории";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 320);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridViewDTr);
            this.Controls.Add(this.dataGridViewRls);
            this.Controls.Add(this.dataGridViewOp);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.textBoxStep);
            this.Controls.Add(this.textBoxD2);
            this.Controls.Add(this.textBoxD1);
            this.Controls.Add(this.textBoxTet);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расчет точек упреждения";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDTr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxTet;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBoxD1;
        public System.Windows.Forms.TextBox textBoxD2;
        public System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DataGridView dataGridViewRls;
        public System.Windows.Forms.DataGridView dataGridViewOp;
        public System.Windows.Forms.DataGridView dataGridViewDTr;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

