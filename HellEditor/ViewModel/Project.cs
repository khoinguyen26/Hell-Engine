using System.Collections.ObjectModel;
using System.Runtime.Serialization;

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
        public ReadOnlyObservableCollection<Scene> Scenes { get; }

        public Project(string name, string path)
        {
            Name = name;
            Path = path;

            _scenes.Add(new Scene(this, "Default Scene"));
        }
    }
}
