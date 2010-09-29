using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Microsoft.ClojureExtension.Configuration
{
    public partial class FrameworkOptions : UserControl
    {
        private readonly SettingsStore _settingsStore;
        private List<Framework> _currentFrameworkList;

        public FrameworkOptions(SettingsStore settingsStore)
        {
            _settingsStore = settingsStore;
            _currentFrameworkList = new List<Framework>();
            InitializeComponent();
        }

        private void FrameworkOptions_Load(object sender, EventArgs e)
        {
            if (!_settingsStore.Exists("Frameworks")) return;
            _currentFrameworkList = _settingsStore.Get<List<Framework>>("Frameworks");
            frameworkList.Items.Clear();
            frameworkList.Items.AddRange(_currentFrameworkList.ToArray());
        }

        public List<Framework> GetFrameworkList()
        {
            return new List<Framework>(_currentFrameworkList);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(frameworkNameTextBox.Text))
            {
                MessageBox.Show("You must supply a framework name.", "Cannot add framework.");
                return;
            }

            if (string.IsNullOrEmpty(frameworkLocationTextBox.Text))
            {
                MessageBox.Show("You must supply a framework location.", "Cannot add framework.");
                return;
            }

            Framework existingFramework = _currentFrameworkList.Find(f => f.Name == frameworkNameTextBox.Text);
            if (existingFramework != null) _currentFrameworkList.Remove(existingFramework);
            _currentFrameworkList.Add(new Framework(frameworkNameTextBox.Text, frameworkLocationTextBox.Text));
            frameworkList.Items.Clear();
            frameworkList.Items.AddRange(_currentFrameworkList.ToArray());
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (frameworkList.SelectedItem == null) return;
            _currentFrameworkList.Remove(_currentFrameworkList.Find(f => f == frameworkList.SelectedItem));
            frameworkList.Items.Remove(frameworkList.SelectedItem);
        }

        private void frameworkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Framework selectedFramework = (Framework) frameworkList.SelectedItem;
            frameworkNameTextBox.Text = selectedFramework.Name;
            frameworkLocationTextBox.Text = selectedFramework.Location;
        }
    }
}