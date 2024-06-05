using HellEditor.Models;
using HellEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace HellEditor.ViewModel
{
    /// <summary>
    /// Handle open a project
    /// </summary>
    public class OpenProject
    {
        private static readonly string _applicationDataPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\HellEditor\";
        private static readonly string _projectDataPath;
        private static readonly ObservableCollection<ProjectData> _projects = new ObservableCollection<ProjectData>();
        public static ReadOnlyObservableCollection<ProjectData> Projects { get; }

        private static void ReadProjectData()
        {
            if (File.Exists(_projectDataPath))
            {
                var projects = Serializer.FromFile<ProjectDataList>(_projectDataPath).Projects.OrderByDescending(x => x.Date);
                _projects.Clear();
                foreach (var project in projects)
                {
                    if (File.Exists(project.FullPath))
                    {
                        project.Icon = File.ReadAllBytes($@"{project.ProjectPath}\.Hell\Icon.png");
                        _projects.Add(project);
                    }
                }
            }
        }
        private static void WriteProjectData()
        {
            var project = _projects.OrderBy(x => x.Date).ToList();
            Serializer.ToFile(new ProjectDataList() { Projects = project }, _projectDataPath);
        }

        public static Project Open(ProjectData projectData)
        {
            ReadProjectData();

            // try to get existed project
            var project = _projects.FirstOrDefault(x => x.FullPath == projectData.FullPath);

            if (project != null)
            {
                // project found and we open it to edit so update the date
                project.Date = DateTime.Now;
            }
            else
            {
                project = projectData;
                project.Date = DateTime.Now;
                _projects.Add(project);
            }
            WriteProjectData();

            return Project.Load(project.FullPath);
        }


        static OpenProject()
        {
            try
            {
                if (!Directory.Exists(_applicationDataPath)) Directory.CreateDirectory(_applicationDataPath);
                _projectDataPath = $@"{_applicationDataPath}ProjectData.xml";
                Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
                ReadProjectData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
