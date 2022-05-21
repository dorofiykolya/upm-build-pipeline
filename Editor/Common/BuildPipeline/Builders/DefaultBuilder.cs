using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Common.BuildPipeline.Builders
{
    public class DefaultBuilder : IBuilder
    {
        public virtual void PreBuild(BuildConfiguration config, ILogger logger)
        {
        }

        public virtual BuildReport Build(BuildConfiguration config, ILogger logger)
        {
            var result = UnityEditor.BuildPipeline.BuildPlayer(config.BuildPlayerOptions);
            return result;
        }

        public virtual void PostBuild(BuildReport report, BuildConfiguration config, ILogger logger)
        {
        }

        public virtual string Help
        {
            get { return ""; }
        }
    }
}