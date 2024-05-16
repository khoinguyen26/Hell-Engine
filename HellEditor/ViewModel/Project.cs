using HellEditor.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;

namespace HellEditor.ViewModel
{
    [DataContract(Name = "Game")]
    public class Project : BaseViewModel
    {
        public static string Extension { get; } = ".hell";
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Path { get; private set; }
        public string FullPath => $"{Path}{Name}{Extension}";

        [DataMember(Name = "Scenes")]
        private ObservableCollection<Scene> _scenes = new();
        public ReadOnlyObservableCollection<Scene> Scenes { get; private set; }

        private Scene _activeScene;
        public Scene ActiveScene
        {
            get => _activeScene;
            set
            {
                if (_activeScene != value)
                {
                    _activeScene = value;
                    OnPropertyChanged(nameof(ActiveScene));
                }
            }
        }

        public static Project Current => Application.Current.MainWindow.DataContext as Project;

        public static Project Load(string file)
        {
            Debug.Assert(File.Exists(file));
            return Serializer.FromFile<Project>(file);
        }

        public void Unload()
        {

        }

        public static void Save(Project project)
        {
            Serializer.ToFile(project, project.FullPath);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_scenes != null)
            {
                Scenes = new ReadOnlyObservableCollection<Scene>(_scenes);
                OnPropertyChanged(nameof(Scenes));
            }
            ActiveScene = Scenes.FirstOrDefault(x => x.IsActive);
        }

        public Project(string name, string path)
        {
            Name = name;
            Path = path;

            _scenes.Add(new Scene(this, "Default Scene"));
            OnDeserialized(new StreamingContext());
        }
    }
}
