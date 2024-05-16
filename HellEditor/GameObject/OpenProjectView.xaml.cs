using HellEditor.Models;
using HellEditor.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace HellEditor.GameObject
{
    /// <summary>
    /// Interaction logic for OpenProjectView.xaml
    /// </summary>
    public partial class OpenProjectView : UserControl
    {
        public OpenProjectView()
        {
            InitializeComponent();
        }

        private void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedProject();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenSelectedProject();
        }

        private void OpenSelectedProject()
        {
            var project = OpenProject.Open(projectListBox.SelectedItem as ProjectData);

            bool dialogResult = false;

            var window = Window.GetWindow(this);

            if (project != null)
            {
                dialogResult = true;
                window.DataContext = project;
            }

            window.DialogResult = dialogResult;
            window.Close();
        }

    }
}
