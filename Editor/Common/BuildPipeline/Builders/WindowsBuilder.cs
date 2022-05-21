using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Common.BuildPipeline.Builders
{
    public class WindowsBuilder : DefaultBuilder
    {
        private BuildTarget _buildTarget;

        public WindowsBuilder(BuildTarget buildTarget)
        {
            _buildTarget = buildTarget;
        }

        public override void PreBuild(BuildConfiguration config, ILogger logger)
        {
            var args = config.Arguments;

            BuilderUtils.AssertRequiredArguments<BuilderArguments.Standalone>(args, true);

            if (args.Contains(BuilderArguments.Standalone.FullScreenMode))
            {
                PlayerSettings.fullScreenMode =
                    args.GetValueByEnum<FullScreenMode>(BuilderArguments.Standalone.FullScreenMode);
            }

            if (args.Contains(BuilderArguments.Standalone.DefaultScreenWidth))
            {
                PlayerSettings.defaultScreenWidth = args.GetAsInt(BuilderArguments.Standalone.DefaultScreenWidth);
            }

            if (args.Contains(BuilderArguments.Standalone.DefaultScreenHeight))
            {
                PlayerSettings.defaultScreenHeight = args.GetAsInt(BuilderArguments.Standalone.DefaultScreenHeight);
            }
        }

        public override BuildReport Build(BuildConfiguration config, ILogger logger)
        {
            var result = UnityEditor.BuildPipeline.BuildPlayer(config.BuildPlayerOptions);
            return result;
        }

        public override string Help
        {
            get
            {
                return "Windows" + Environment.NewLine +
                       BuilderUtils.GetArgumentsDescription(typeof(BuilderArguments.Standalone), 2);
            }
        }
    }
}