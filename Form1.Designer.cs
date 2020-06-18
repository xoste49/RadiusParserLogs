namespace RadiusParserLogs
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
         this.openbtn = new System.Windows.Forms.Button();
         this.lv = new System.Windows.Forms.ListView();
         this.ofd = new System.Windows.Forms.OpenFileDialog();
         this.SuspendLayout();
         // 
         // openbtn
         // 
         this.openbtn.Location = new System.Drawing.Point(12, 12);
         this.openbtn.Name = "openbtn";
         this.openbtn.Size = new System.Drawing.Size(75, 23);
         this.openbtn.TabIndex = 0;
         this.openbtn.Text = "Открыть";
         this.openbtn.UseVisualStyleBackColor = true;
         this.openbtn.Click += new System.EventHandler(this.openbtn_Click);
         // 
         // lv
         // 
         this.lv.HideSelection = false;
         this.lv.Location = new System.Drawing.Point(13, 42);
         this.lv.Name = "lv";
         this.lv.Size = new System.Drawing.Size(775, 396);
         this.lv.TabIndex = 1;
         this.lv.UseCompatibleStateImageBehavior = false;
         this.lv.View = System.Windows.Forms.View.Details;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.lv);
         this.Controls.Add(this.openbtn);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button openbtn;
      private System.Windows.Forms.ListView lv;
      private System.Windows.Forms.OpenFileDialog ofd;
   }
}

