﻿namespace oroc
{
    using System.Windows.Forms;

    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            SetupDataBindings();

            HandleCreated += OnSettingsDialogHandleCreated;
        }

        private void OnSettingsDialogHandleCreated(object sender, System.EventArgs e)
        {
            if (!(Owner is MainDialog))
            {
                Log.e("Owner of Settings Window is not the right type.");
                return;
            }

            MainDialog main_dialog = Owner as MainDialog;

            main_dialog.SetStatusBarText(SingleInstanceCheckBox, "Allow only one instance of OpenRoC to run on this computer.");
            main_dialog.SetStatusBarText(StartMinimizedCheckBox, "Start OpenRoC minimized, in task-bar next time it launches.");
        }

        private void SetupDataBindings()
        {
            StartMinimizedCheckBox.DataBindings.Add(new Binding(nameof(StartMinimizedCheckBox.Checked), Settings.Instance, nameof(Settings.Instance.IsStartMinimizedEnabled)));
            SingleInstanceCheckBox.DataBindings.Add(new Binding(nameof(SingleInstanceCheckBox.Checked), Settings.Instance, nameof(Settings.Instance.IsSingleInsntaceEnabled)));
            HttpInterfaceEnabledCheckBox.DataBindings.Add(new Binding(nameof(HttpInterfaceEnabledCheckBox.Checked), Settings.Instance, nameof(Settings.Instance.IsWebInterfaceEnabled)));
            HttpUrlTextBox.DataBindings.Add(new Binding(nameof(HttpUrlTextBox.Text), Settings.Instance, nameof(Settings.Instance.WebInterfaceAddress)));
        }

        private void OnHttpInterfaceEnabledCheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            if (HttpUrlTextBox.Enabled != checkbox.Checked)
                HttpUrlTextBox.Enabled = checkbox.Checked;
        }
    }
}
