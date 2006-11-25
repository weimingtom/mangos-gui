namespace ConfigurationEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mhostedBox = new System.Windows.Forms.ComboBox();
            this.rhostedBox = new System.Windows.Forms.ComboBox();
            this.mnameBox = new System.Windows.Forms.TextBox();
            this.rnameBox = new System.Windows.Forms.TextBox();
            this.timerBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.appBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rpathBox = new System.Windows.Forms.TextBox();
            this.userBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.passBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.mangosData = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.realmData = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Restarter Timer Interval:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Realm Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mangos Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Is Mangos hosted on this computer?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Is Realmd hosted on this computer?";
            // 
            // mhostedBox
            // 
            this.mhostedBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.mhostedBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.mhostedBox.FormattingEnabled = true;
            this.mhostedBox.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.mhostedBox.Location = new System.Drawing.Point(215, 113);
            this.mhostedBox.Name = "mhostedBox";
            this.mhostedBox.Size = new System.Drawing.Size(43, 21);
            this.mhostedBox.TabIndex = 5;
            // 
            // rhostedBox
            // 
            this.rhostedBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.rhostedBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.rhostedBox.FormattingEnabled = true;
            this.rhostedBox.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.rhostedBox.Location = new System.Drawing.Point(215, 140);
            this.rhostedBox.Name = "rhostedBox";
            this.rhostedBox.Size = new System.Drawing.Size(43, 21);
            this.rhostedBox.TabIndex = 6;
            // 
            // mnameBox
            // 
            this.mnameBox.Location = new System.Drawing.Point(158, 88);
            this.mnameBox.Name = "mnameBox";
            this.mnameBox.Size = new System.Drawing.Size(100, 20);
            this.mnameBox.TabIndex = 7;
            // 
            // rnameBox
            // 
            this.rnameBox.Location = new System.Drawing.Point(158, 63);
            this.rnameBox.Name = "rnameBox";
            this.rnameBox.Size = new System.Drawing.Size(100, 20);
            this.rnameBox.TabIndex = 8;
            // 
            // timerBox
            // 
            this.timerBox.Location = new System.Drawing.Point(158, 38);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(100, 20);
            this.timerBox.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(290, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Save and Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(439, 186);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "MaNGOS GUI Configuration Editor";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(353, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Reset to Defaults";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Path to Mangos:";
            // 
            // appBox
            // 
            this.appBox.Location = new System.Drawing.Point(121, 169);
            this.appBox.Name = "appBox";
            this.appBox.Size = new System.Drawing.Size(137, 20);
            this.appBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Path to Realm:";
            // 
            // rpathBox
            // 
            this.rpathBox.Location = new System.Drawing.Point(121, 197);
            this.rpathBox.Name = "rpathBox";
            this.rpathBox.Size = new System.Drawing.Size(137, 20);
            this.rpathBox.TabIndex = 17;
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(141, 52);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(100, 20);
            this.userBox.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Database Username:";
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(104, 24);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(137, 20);
            this.hostBox.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Database host:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Database Password";
            // 
            // passBox
            // 
            this.passBox.Location = new System.Drawing.Point(141, 78);
            this.passBox.Name = "passBox";
            this.passBox.Size = new System.Drawing.Size(100, 20);
            this.passBox.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Mangos Database:";
            // 
            // mangosData
            // 
            this.mangosData.Location = new System.Drawing.Point(141, 104);
            this.mangosData.Name = "mangosData";
            this.mangosData.Size = new System.Drawing.Size(100, 20);
            this.mangosData.TabIndex = 21;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 133);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Realm Database:";
            // 
            // realmData
            // 
            this.realmData.Location = new System.Drawing.Point(141, 130);
            this.realmData.Name = "realmData";
            this.realmData.Size = new System.Drawing.Size(100, 20);
            this.realmData.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.realmData);
            this.groupBox1.Controls.Add(this.passBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.mangosData);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.userBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.hostBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(268, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 163);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "mySQL Database Settings";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(536, 246);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rpathBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.appBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.timerBox);
            this.Controls.Add(this.rnameBox);
            this.Controls.Add(this.mnameBox);
            this.Controls.Add(this.rhostedBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.mhostedBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Configuration Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mhostedBox;
        private System.Windows.Forms.ComboBox rhostedBox;
        private System.Windows.Forms.TextBox mnameBox;
        private System.Windows.Forms.TextBox rnameBox;
        private System.Windows.Forms.TextBox timerBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox appBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rpathBox;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox mangosData;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox realmData;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

