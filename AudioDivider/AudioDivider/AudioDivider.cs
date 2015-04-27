using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AudioControl
{
    public partial class AudioDivider : Form
    {
        public AudioDivider()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Injector.Initialize();
            Communication.ServerStart();
            timerRefresh.Start();         
        }

        class ControlledProgram
        {
            public string name;
            public ControlledProgram(string name)
            {
                this.name = name;
            }
        }
        List<ControlledProgram> controlledPrograms = new List<ControlledProgram>();

        void RefreshSoundInfo()
        {
            treeSound.BeginUpdate();

            string selectedNodeName = null;
            if (treeSound.SelectedNode != null)
            {
                selectedNodeName = treeSound.SelectedNode.Text;
            }
            string selectedDevice = (string)combo_Devices.SelectedItem;

           
            treeSound.Nodes.Clear();
            combo_Devices.Items.Clear();

            List<SoundInfoDevice> devices = SoundHandler.getSoundInfo();
            foreach (var device in devices)
            {
                TreeNode node = treeSound.Nodes.Add(device.name);
                combo_Devices.Items.Add(device.name);
                foreach (var session in device.sessions)
                {
                    TreeNode nodeSession = node.Nodes.Add(session.windowName + " (" + session.pid + ")");
                    nodeSession.Tag = session.pid;
                }
            }

            treeSound.ExpandAll();

          
            foreach(TreeNode nodeDevice in treeSound.Nodes)
            {
                if (nodeDevice.Text == selectedNodeName)
                    treeSound.SelectedNode = nodeDevice;
                foreach (TreeNode nodeProgram in nodeDevice.Nodes)
                {
                    if (nodeProgram.Text == selectedNodeName)
                        treeSound.SelectedNode = nodeProgram;
                }
            }
            combo_Devices.SelectedItem = selectedDevice;
            treeSound.EndUpdate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Communication.ServerStop();
        }


        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshSoundInfo();
        }
        bool chk_Controlled_ChangedbyUser = true;
        private void chk_Controlled_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk_Controlled_ChangedbyUser)
                return;
           
            combo_Devices.Enabled = true;
            chk_Controlled_ChangedbyUser = false;
            chk_Controlled.Enabled = false;
            chk_Controlled_ChangedbyUser = true;

            int pid = (int)treeSound.SelectedNode.Tag;
            controlledPrograms.Add(new ControlledProgram(treeSound.SelectedNode.Text));

            Injector.Inject(pid);
        }

        private void treeSound_AfterSelect(object sender, TreeViewEventArgs e)
        {
            chk_Controlled_ChangedbyUser = false;
            if (treeSound.Nodes.Contains(treeSound.SelectedNode)) // Is a Device
            {
                chk_Controlled.Enabled = false;
                chk_Controlled.Checked = false;
                combo_Devices.Enabled = false;
                btn_Set.Enabled = false;
                chk_Controlled_ChangedbyUser = true;
                return;
            }
           

            bool alreadyControlled = false;
            foreach(var controlledProgram in controlledPrograms)
            {
                if (controlledProgram.name == treeSound.SelectedNode.Text)
                    alreadyControlled = true;
            }

            if (alreadyControlled)
            {
                chk_Controlled.Checked = true;
                chk_Controlled.Enabled = false;
                combo_Devices.Enabled = true;
                btn_Set.Enabled = true;
            }
            else
            {
                chk_Controlled.Checked = false;
                chk_Controlled.Enabled = true;
                combo_Devices.Enabled = false;
                btn_Set.Enabled = false;
            }
            chk_Controlled_ChangedbyUser = true;
           
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            if (treeSound.SelectedNode != null && combo_Devices.SelectedItem != null)
            {
                List<SoundInfoDevice> devices = SoundHandler.getSoundInfo();

                int pid = (int)treeSound.SelectedNode.Tag;
                string deviceId = devices[combo_Devices.SelectedIndex].ID;

                Communication.ServerSend(pid, 1, deviceId);
            }

            SoundHandler.switchDefaultDevice();
            Thread.Sleep(100);

            RefreshSoundInfo();
        }
    }
}
