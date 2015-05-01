namespace AudioDivider
{
    partial class FormAutoControl
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
            this.radio_ProcessPath = new System.Windows.Forms.RadioButton();
            this.group_SelectBy = new System.Windows.Forms.GroupBox();
            this.radio_WindowName = new System.Windows.Forms.RadioButton();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.combo_Devices = new System.Windows.Forms.ComboBox();
            this.lbl_SwitchTo = new System.Windows.Forms.Label();
            this.group_ControlWhen = new System.Windows.Forms.GroupBox();
            this.radio_ProgramPlayAudio = new System.Windows.Forms.RadioButton();
            this.radio_ProgramStarts = new System.Windows.Forms.RadioButton();
            this.group_SelectBy.SuspendLayout();
            this.group_ControlWhen.SuspendLayout();
            this.SuspendLayout();
            // 
            // radio_ProcessPath
            // 
            this.radio_ProcessPath.AutoSize = true;
            this.radio_ProcessPath.Location = new System.Drawing.Point(6, 19);
            this.radio_ProcessPath.Name = "radio_ProcessPath";
            this.radio_ProcessPath.Size = new System.Drawing.Size(85, 17);
            this.radio_ProcessPath.TabIndex = 0;
            this.radio_ProcessPath.TabStop = true;
            this.radio_ProcessPath.Text = "Programpath";
            this.radio_ProcessPath.UseVisualStyleBackColor = true;
            this.radio_ProcessPath.CheckedChanged += new System.EventHandler(this.radio_ProcessPath_CheckedChanged);
            // 
            // group_SelectBy
            // 
            this.group_SelectBy.Controls.Add(this.radio_WindowName);
            this.group_SelectBy.Controls.Add(this.radio_ProcessPath);
            this.group_SelectBy.Location = new System.Drawing.Point(99, 19);
            this.group_SelectBy.Name = "group_SelectBy";
            this.group_SelectBy.Size = new System.Drawing.Size(200, 72);
            this.group_SelectBy.TabIndex = 1;
            this.group_SelectBy.TabStop = false;
            this.group_SelectBy.Text = "Identify by";
            // 
            // radio_WindowName
            // 
            this.radio_WindowName.AutoSize = true;
            this.radio_WindowName.Location = new System.Drawing.Point(6, 42);
            this.radio_WindowName.Name = "radio_WindowName";
            this.radio_WindowName.Size = new System.Drawing.Size(90, 17);
            this.radio_WindowName.TabIndex = 1;
            this.radio_WindowName.TabStop = true;
            this.radio_WindowName.Text = "Windowname";
            this.radio_WindowName.UseVisualStyleBackColor = true;
            this.radio_WindowName.CheckedChanged += new System.EventHandler(this.radio_WindowName_CheckedChanged);
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Enabled = false;
            this.btn_Ok.Location = new System.Drawing.Point(12, 153);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(78, 23);
            this.btn_Ok.TabIndex = 2;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(486, 149);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(78, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // combo_Devices
            // 
            this.combo_Devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Devices.FormattingEnabled = true;
            this.combo_Devices.Location = new System.Drawing.Point(99, 97);
            this.combo_Devices.Name = "combo_Devices";
            this.combo_Devices.Size = new System.Drawing.Size(391, 21);
            this.combo_Devices.TabIndex = 6;
            this.combo_Devices.SelectedIndexChanged += new System.EventHandler(this.combo_Devices_SelectedIndexChanged);
            // 
            // lbl_SwitchTo
            // 
            this.lbl_SwitchTo.AutoSize = true;
            this.lbl_SwitchTo.Location = new System.Drawing.Point(36, 100);
            this.lbl_SwitchTo.Name = "lbl_SwitchTo";
            this.lbl_SwitchTo.Size = new System.Drawing.Size(54, 13);
            this.lbl_SwitchTo.TabIndex = 7;
            this.lbl_SwitchTo.Text = "Switch to:";
            // 
            // group_ControlWhen
            // 
            this.group_ControlWhen.Controls.Add(this.radio_ProgramPlayAudio);
            this.group_ControlWhen.Controls.Add(this.radio_ProgramStarts);
            this.group_ControlWhen.Location = new System.Drawing.Point(319, 19);
            this.group_ControlWhen.Name = "group_ControlWhen";
            this.group_ControlWhen.Size = new System.Drawing.Size(200, 72);
            this.group_ControlWhen.TabIndex = 2;
            this.group_ControlWhen.TabStop = false;
            this.group_ControlWhen.Text = "Control when";
            // 
            // radio_ProgramPlayAudio
            // 
            this.radio_ProgramPlayAudio.AutoSize = true;
            this.radio_ProgramPlayAudio.Location = new System.Drawing.Point(6, 42);
            this.radio_ProgramPlayAudio.Name = "radio_ProgramPlayAudio";
            this.radio_ProgramPlayAudio.Size = new System.Drawing.Size(157, 17);
            this.radio_ProgramPlayAudio.TabIndex = 1;
            this.radio_ProgramPlayAudio.TabStop = true;
            this.radio_ProgramPlayAudio.Text = "Program starts playing audio";
            this.radio_ProgramPlayAudio.UseVisualStyleBackColor = true;
            // 
            // radio_ProgramStarts
            // 
            this.radio_ProgramStarts.AutoSize = true;
            this.radio_ProgramStarts.Location = new System.Drawing.Point(6, 19);
            this.radio_ProgramStarts.Name = "radio_ProgramStarts";
            this.radio_ProgramStarts.Size = new System.Drawing.Size(92, 17);
            this.radio_ProgramStarts.TabIndex = 0;
            this.radio_ProgramStarts.TabStop = true;
            this.radio_ProgramStarts.Text = "Program starts";
            this.radio_ProgramStarts.UseVisualStyleBackColor = true;
            // 
            // FormAutoControl
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(576, 188);
            this.Controls.Add(this.group_ControlWhen);
            this.Controls.Add(this.lbl_SwitchTo);
            this.Controls.Add(this.combo_Devices);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.group_SelectBy);
            this.Name = "FormAutoControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AutoControl";
            this.group_SelectBy.ResumeLayout(false);
            this.group_SelectBy.PerformLayout();
            this.group_ControlWhen.ResumeLayout(false);
            this.group_ControlWhen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radio_ProcessPath;
        private System.Windows.Forms.GroupBox group_SelectBy;
        private System.Windows.Forms.RadioButton radio_WindowName;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox combo_Devices;
        private System.Windows.Forms.Label lbl_SwitchTo;
        private System.Windows.Forms.GroupBox group_ControlWhen;
        private System.Windows.Forms.RadioButton radio_ProgramPlayAudio;
        private System.Windows.Forms.RadioButton radio_ProgramStarts;
    }
}