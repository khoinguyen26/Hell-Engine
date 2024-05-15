using System.Runtime.Serialization;

namespace HellEditor.Models
{
    [DataContract]
    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }
        public byte[] Icon { get; set; }
        public string IconFilePath { get; set; }
        public string ProjectFilePath { get; set; }
    }
}
