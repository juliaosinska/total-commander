namespace TotalCommander
{
    partial class frmCommander
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lvOkno1 = new ListView();
            nazwa1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            data1 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            lvOkno2 = new ListView();
            nazwa2 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            data2 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            Dysk1 = new ComboBox();
            Dysk2 = new ComboBox();
            tbAktywnaSciezka = new TextBox();
            tbNieAktywnaSciezka = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // lvOkno1
            // 
            lvOkno1.AllowDrop = true;
            lvOkno1.Columns.AddRange(new ColumnHeader[] { nazwa1, columnHeader2, data1, columnHeader4 });
            lvOkno1.FullRowSelect = true;
            lvOkno1.LabelEdit = true;
            lvOkno1.Location = new Point(14, 65);
            lvOkno1.Margin = new Padding(3, 4, 3, 4);
            lvOkno1.Name = "lvOkno1";
            lvOkno1.Size = new Size(514, 659);
            lvOkno1.TabIndex = 0;
            lvOkno1.UseCompatibleStateImageBehavior = false;
            lvOkno1.View = View.Details;
            // 
            // nazwa1
            // 
            nazwa1.Text = "Nazwa";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Typ";
            // 
            // data1
            // 
            data1.Text = "Data";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Rozmiar";
            // 
            // lvOkno2
            // 
            lvOkno2.AllowDrop = true;
            lvOkno2.Columns.AddRange(new ColumnHeader[] { nazwa2, columnHeader6, data2, columnHeader8 });
            lvOkno2.FullRowSelect = true;
            lvOkno2.LabelEdit = true;
            lvOkno2.Location = new Point(558, 65);
            lvOkno2.Margin = new Padding(3, 4, 3, 4);
            lvOkno2.Name = "lvOkno2";
            lvOkno2.Size = new Size(514, 659);
            lvOkno2.TabIndex = 1;
            lvOkno2.UseCompatibleStateImageBehavior = false;
            lvOkno2.View = View.Details;
            // 
            // nazwa2
            // 
            nazwa2.Text = "Nazwa";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Typ";
            // 
            // data2
            // 
            data2.Text = "Data";
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Rozmiar";
            // 
            // Dysk1
            // 
            Dysk1.FormattingEnabled = true;
            Dysk1.Location = new Point(14, 16);
            Dysk1.Margin = new Padding(3, 4, 3, 4);
            Dysk1.Name = "Dysk1";
            Dysk1.Size = new Size(87, 28);
            Dysk1.TabIndex = 2;
            // 
            // Dysk2
            // 
            Dysk2.FormattingEnabled = true;
            Dysk2.Location = new Point(558, 17);
            Dysk2.Margin = new Padding(3, 4, 3, 4);
            Dysk2.Name = "Dysk2";
            Dysk2.Size = new Size(87, 28);
            Dysk2.TabIndex = 3;
            // 
            // tbAktywnaSciezka
            // 
            tbAktywnaSciezka.Location = new Point(107, 16);
            tbAktywnaSciezka.Name = "tbAktywnaSciezka";
            tbAktywnaSciezka.ReadOnly = true;
            tbAktywnaSciezka.Size = new Size(421, 27);
            tbAktywnaSciezka.TabIndex = 8;
            // 
            // tbNieAktywnaSciezka
            // 
            tbNieAktywnaSciezka.Location = new Point(651, 17);
            tbNieAktywnaSciezka.Name = "tbNieAktywnaSciezka";
            tbNieAktywnaSciezka.ReadOnly = true;
            tbNieAktywnaSciezka.Size = new Size(421, 27);
            tbNieAktywnaSciezka.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 728);
            label1.Name = "label1";
            label1.Size = new Size(194, 20);
            label1.TabIndex = 10;
            label1.Text = "F7 - Stwórz Folder F8 - Usuń";
            label1.Click += label1_Click;
            // 
            // frmCommander
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 755);
            Controls.Add(label1);
            Controls.Add(tbNieAktywnaSciezka);
            Controls.Add(tbAktywnaSciezka);
            Controls.Add(Dysk2);
            Controls.Add(Dysk1);
            Controls.Add(lvOkno2);
            Controls.Add(lvOkno1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmCommander";
            Text = "TotalCommander";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvOkno1;
        private ColumnHeader nazwa1;
        private ColumnHeader columnHeader2;
        private ColumnHeader data1;
        private ColumnHeader columnHeader4;
        private ListView lvOkno2;
        private ColumnHeader nazwa2;
        private ColumnHeader columnHeader6;
        private ColumnHeader data2;
        private ColumnHeader columnHeader8;
        private ComboBox Dysk1;
        private ComboBox Dysk2;
        private TextBox tbAktywnaSciezka;
        private TextBox tbNieAktywnaSciezka;
        private Label label1;
    }
}