namespace AudioDivider
{
    partial class FormAudioDivider
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
            this.treeSound = new System.Windows.Forms.TreeView();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.combo_Devices = new System.Windows.Forms.ComboBox();
            this.chk_Controlled = new System.Windows.Forms.CheckBox();
            this.btn_Set = new System.Windows.Forms.Button();
            this.chk_OnlyShowActive = new System.Windows.Forms.CheckBox();
            this.chk_AutoControl = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeSound
            // 
            this.treeSound.HideSelection = false;
            this.treeSound.Location = new System.Drawing.Point(12, 55);
            this.treeSound.Name = "treeSound";
            this.treeSound.Size = new System.Drawing.Size(532, 267);
            this.treeSound.TabIndex = 0;
            this.treeSound.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSound_AfterSelect);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // combo_Devices
            // 
            this.combo_Devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Devices.Enabled = false;
            this.combo_Devices.FormattingEnabled = true;
            this.combo_Devices.Location = new System.Drawing.Point(550, 137);
            this.combo_Devices.Name = "combo_Devices";
            this.combo_Devices.Size = new System.Drawing.Size(391, 21);
            this.combo_Devices.TabIndex = 5;
            // 
            // chk_Controlled
            // 
            this.chk_Controlled.AutoSize = true;
            this.chk_Controlled.Enabled = false;
            this.chk_Controlled.Location = new System.Drawing.Point(550, 104);
            this.chk_Controlled.Name = "chk_Controlled";
            this.chk_Controlled.Size = new System.Drawing.Size(73, 17);
            this.chk_Controlled.TabIndex = 6;
            this.chk_Controlled.Text = "Controlled";
            this.chk_Controlled.UseVisualStyleBackColor = true;
            this.chk_Controlled.CheckedChanged += new System.EventHandler(this.chk_Controlled_CheckedChanged);
            // 
            // btn_Set
            // 
            this.btn_Set.Enabled = false;
            this.btn_Set.Location = new System.Drawing.Point(947, 137);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(35, 23);
            this.btn_Set.TabIndex = 7;
            this.btn_Set.Text = "Set";
            this.btn_Set.UseVisualStyleBackColor = true;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // chk_OnlyShowActive
            // 
            this.chk_OnlyShowActive.AutoSize = true;
            this.chk_OnlyShowActive.Location = new System.Drawing.Point(12, 32);
            this.chk_OnlyShowActive.Name = "chk_OnlyShowActive";
            this.chk_OnlyShowActive.Size = new System.Drawing.Size(196, 17);
            this.chk_OnlyShowActive.TabIndex = 8;
            this.chk_OnlyShowActive.Text = "Show only programs currently active";
            this.chk_OnlyShowActive.UseVisualStyleBackColor = true;
            this.chk_OnlyShowActive.CheckedChanged += new System.EventHandler(this.chk_OnlyShowActive_CheckedChanged);
            // 
            // chk_AutoControl
            // 
            this.chk_AutoControl.AutoSize = true;
            this.chk_AutoControl.Enabled = false;
            this.chk_AutoControl.Location = new System.Drawing.Point(689, 104);
            this.chk_AutoControl.Name = "chk_AutoControl";
            this.chk_AutoControl.Size = new System.Drawing.Size(81, 17);
            this.chk_AutoControl.TabIndex = 10;
            this.chk_AutoControl.Text = "AutoControl";
            this.chk_AutoControl.UseVisualStyleBackColor = true;
            this.chk_AutoControl.CheckedChanged += new System.EventHandler(this.chk_AutoControl_CheckedChanged);
            // 
            // FormAudioDivider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 461);
            this.Controls.Add(this.chk_AutoControl);
            this.Controls.Add(this.chk_OnlyShowActive);
            this.Controls.Add(this.btn_Set);
            this.Controls.Add(this.chk_Controlled);
            this.Controls.Add(this.combo_Devices);
            this.Controls.Add(this.treeSound);
            this.Name = "FormAudioDivider";
            this.Text = "AudioDivider";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeSound;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ComboBox combo_Devices;
        private System.Windows.Forms.CheckBox chk_Controlled;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.CheckBox chk_OnlyShowActive;
        private System.Windows.Forms.CheckBox chk_AutoControl;
    }
}