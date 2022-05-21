using UnityEditor.Build.Reporting;

namespace Common.BuildPipeline
{
    public interface IBuilderProcessor
    {
        void OnConfiguration(BuildConfiguration configuration);
        void OnBeforeBuild(BuildConfiguration configuration);
        void OnAfterBuild(BuildConfiguration configuration, BuildReport report);
        void OnFinalize(BuildConfiguration configuration, BuildReport report);
        string Help { get; }
    }
}