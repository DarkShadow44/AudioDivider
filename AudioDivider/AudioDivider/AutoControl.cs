using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AudioDivider
{
    public partial class FormAutoControl : Form
    {

        ProgramAutoInfo programAutoInfo;
        ProgramInfo programInfo;

        public FormAutoControl(ProgramInfo programInfo)
        {
            this.programInfo = programInfo;
            InitializeComponent();

            List<SoundInfoDevice> devices = SoundHandler.getSoundInfo();
            foreach (var device in devices)
            {
                combo_Devices.Items.Add(device.name);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            List<SoundInfoDevice> devices = SoundHandler.getSoundInfo();
            ProgramAutoInfo.SelectBy selectby = (ProgramAutoInfo.SelectBy)(radio_ProcessPath.Checked ? 0 : 1);
            programAutoInfo = new ProgramAutoInfo(selectby, programInfo.Path, programInfo.name, devices[combo_Devices.SelectedIndex].ID, radio_ProgramStarts.Checked);
            this.Hide();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void combo_Devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }

        void RefreshButton()
        {
            btn_Ok.Enabled = combo_Devices.SelectedIndex != -1 && (radio_ProcessPath.Checked || radio_WindowName.Checked);
        }

        private void radio_WindowName_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }

        private void radio_ProcessPath_CheckedChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }

        public ProgramAutoInfo GetProgramAutoInfo()
        {
            return programAutoInfo;
        }

    }
}
