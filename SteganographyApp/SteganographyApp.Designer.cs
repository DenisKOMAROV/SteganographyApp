namespace SteganographyApp
{
    partial class SteganographyApp
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
            this.btnOpenBaseImage = new System.Windows.Forms.Button();
            this.btnRecoverText = new System.Windows.Forms.Button();
            this.btnOpenTextFile = new System.Windows.Forms.Button();
            this.btnHideText = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.Label();
            this.pbBaseImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBaseImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenBaseImage
            // 
            this.btnOpenBaseImage.Location = new System.Drawing.Point(369, 37);
            this.btnOpenBaseImage.Name = "btnOpenBaseImage";
            this.btnOpenBaseImage.Size = new System.Drawing.Size(127, 39);
            this.btnOpenBaseImage.TabIndex = 0;
            this.btnOpenBaseImage.Text = "Открыть изображение";
            this.btnOpenBaseImage.UseVisualStyleBackColor = true;
            this.btnOpenBaseImage.Click += new System.EventHandler(this.btnOpenBaseImage_Click);
            // 
            // btnRecoverText
            // 
            this.btnRecoverText.Location = new System.Drawing.Point(369, 82);
            this.btnRecoverText.Name = "btnRecoverText";
            this.btnRecoverText.Size = new System.Drawing.Size(127, 40);
            this.btnRecoverText.TabIndex = 1;
            this.btnRecoverText.Text = "Раскрыть скрытое сообщениие";
            this.btnRecoverText.UseVisualStyleBackColor = true;
            this.btnRecoverText.Click += new System.EventHandler(this.btnRecoverText_Click);
            // 
            // btnOpenTextFile
            // 
            this.btnOpenTextFile.Location = new System.Drawing.Point(369, 318);
            this.btnOpenTextFile.Name = "btnOpenTextFile";
            this.btnOpenTextFile.Size = new System.Drawing.Size(127, 45);
            this.btnOpenTextFile.TabIndex = 2;
            this.btnOpenTextFile.Text = "Открыть текстовый файл";
            this.btnOpenTextFile.UseVisualStyleBackColor = true;
            this.btnOpenTextFile.Click += new System.EventHandler(this.btnOpenTextFile_Click);
            // 
            // btnHideText
            // 
            this.btnHideText.Location = new System.Drawing.Point(369, 369);
            this.btnHideText.Name = "btnHideText";
            this.btnHideText.Size = new System.Drawing.Size(127, 43);
            this.btnHideText.TabIndex = 3;
            this.btnHideText.Text = "Спрятать текст";
            this.btnHideText.UseVisualStyleBackColor = true;
            this.btnHideText.Click += new System.EventHandler(this.btnHideText_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Исходное изображение";
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.BackColor = System.Drawing.SystemColors.Control;
            this.fileName.Location = new System.Drawing.Point(369, 302);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(64, 13);
            this.fileName.TabIndex = 5;
            this.fileName.Text = "Имя файла";
            // 
            // pbBaseImage
            // 
            this.pbBaseImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbBaseImage.Location = new System.Drawing.Point(13, 37);
            this.pbBaseImage.Name = "pbBaseImage";
            this.pbBaseImage.Size = new System.Drawing.Size(350, 375);
            this.pbBaseImage.TabIndex = 6;
            this.pbBaseImage.TabStop = false;
            // 
            // SteganographyApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 438);
            this.Controls.Add(this.pbBaseImage);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHideText);
            this.Controls.Add(this.btnOpenTextFile);
            this.Controls.Add(this.btnRecoverText);
            this.Controls.Add(this.btnOpenBaseImage);
            this.Name = "SteganographyApp";
            this.Text = "SteganographyApp";
            ((System.ComponentModel.ISupportInitialize)(this.pbBaseImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenBaseImage;
        private System.Windows.Forms.Button btnRecoverText;
        private System.Windows.Forms.Button btnOpenTextFile;
        private System.Windows.Forms.Button btnHideText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileName;
        private System.Windows.Forms.PictureBox pbBaseImage;
    }
}

