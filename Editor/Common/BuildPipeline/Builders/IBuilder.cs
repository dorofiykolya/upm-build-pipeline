using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Common.BuildPipeline.Builders
{
    public interface IBuilder
    {
        void PreBuild(BuildConfiguration config, ILogger logger);
        BuildReport Build(BuildConfiguration config, ILogger logger);
        void PostBuild(BuildReport report, BuildConfiguration config, ILogger logger);
        string Help { get; }
    }
}