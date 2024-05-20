using HellEditor.ViewModel;
using System.Windows.Controls;

namespace HellEditor.Editors
{
    /// <summary>
    /// Interaction logic for ProjectLayoutView.xaml
    /// </summary>
    public partial class ProjectLayoutView : UserControl
    {
        public ProjectLayoutView()
        {
            InitializeComponent();
        }

        private void OnAddScene_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = DataContext as Project;
            vm.AddScene("New Scene" + vm.Scenes.Count);
        }
    }
}
