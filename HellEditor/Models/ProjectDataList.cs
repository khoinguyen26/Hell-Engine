using System.Runtime.Serialization;

namespace HellEditor.Models
{
    [DataContract]
    public class ProjectDataList
    {
        [DataMember]
        public List<ProjectData> Projects { get; set; }
    }
}
