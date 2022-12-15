using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Common.BuildPipeline.Builders
{
    public class AndroidBuilder : DefaultBuilder
    {
        private const string KeystorePath = BuilderArguments.Android.KeystoreName;
        private const string KeystorePass = BuilderArguments.Android.KeystorePass;
        private const string KeyaliasName = BuilderArguments.Android.KeyaliasName;
        private const string KeyaliasPass = BuilderArguments.Android.KeyaliasPass;

        public override void PreBuild(BuildConfiguration config, ILogger logger)
        {
            var args = config.Arguments;

            BuilderUtils.AssertRequiredArguments<BuilderArguments.Android>(args, true);

            if (args.Contains(BuilderArguments.Android.UseCustomKeystore) && args.GetAsBool(BuilderArguments.Android.UseCustomKeystore))
            {
                args.AssertKeys(KeystorePath, KeystorePass, KeyaliasName, KeyaliasPass);
            
                PlayerSettings.Android.useCustomKeystore = true;
            
                PlayerSettings.Android.keystoreName = args[KeystorePath];
                PlayerSettings.Android.keystorePass = args[KeystorePass];

                PlayerSettings.Android.keyaliasName = args[KeyaliasName];
                PlayerSettings.Android.keyaliasPass = args[KeyaliasPass];
            }

            if (args.Contains(BuilderArguments.Android.PreferredInstallLocation))
            {
                PlayerSettings.Android.preferredInstallLocation =
                    args.GetValueByEnum<AndroidPreferredInstallLocation>(BuilderArguments.Android
                        .PreferredInstallLocation);
            }

            if (args.Contains(BuilderArguments.Android.AndroidBuildSubtarget))
            {
                EditorUserBuildSettings.androidBuildSubtarget =
                    args.GetValueByEnum<MobileTextureSubtarget>(BuilderArguments.Android.AndroidBuildSubtarget);
            }

            if (args.Contains(BuilderArguments.Android.BlitType))
            {
                PlayerSettings.Android.blitType =
                    args.GetValueByEnum<AndroidBlitType>(args[BuilderArguments.Android.BlitType]);
            }

            if (args.Contains(BuilderArguments.Android.TargetSdkVersion))
            {
                PlayerSettings.Android.targetSdkVersion =
                    args.GetValueByEnum<AndroidSdkVersions>(args[BuilderArguments.Android.TargetSdkVersion]);
            }

            if (args.Contains(BuilderArguments.Android.MinSdkVersion))
            {
                PlayerSettings.Android.minSdkVersion =
                    args.GetValueByEnum<AndroidSdkVersions>(args[BuilderArguments.Android.MinSdkVersion]);
            }

            if (args.Contains(BuilderArguments.Android.MaxAspectRatio))
            {
                PlayerSettings.Android.maxAspectRatio = args.GetAsFloat(BuilderArguments.Android.MaxAspectRatio);
            }

            PlayerSettings.Android.targetArchitectures = args.Contains(BuilderArguments.Android.TargetArchitectures)
                ? args.GetValueByEnum<AndroidArchitecture>(BuilderArguments.Android.TargetArchitectures)
                : AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;

            if (args.Contains(BuilderArguments.Android.BuildApkPerCpuArchitecture))
            {
                PlayerSettings.Android.buildApkPerCpuArchitecture =
                    args.GetAsBool(BuilderArguments.Android.BuildApkPerCpuArchitecture);
            }

            if (args.Contains(BuilderArguments.Android.DisableDepthAndStencilBuffers))
            {
                PlayerSettings.Android.disableDepthAndStencilBuffers =
                    args.GetAsBool(BuilderArguments.Android.DisableDepthAndStencilBuffers);
            }

            if (args.Contains(BuilderArguments.Android.ForceSDCardPermission))
            {
                PlayerSettings.Android.forceSDCardPermission =
                    args.GetAsBool(BuilderArguments.Android.ForceSDCardPermission);
            }

            if (args.Contains(BuilderArguments.Android.ForceInternetPermission))
            {
                PlayerSettings.Android.forceInternetPermission =
                    args.GetAsBool(BuilderArguments.Android.ForceInternetPermission);
            }

            if (args.Contains(BuilderArguments.Android.AndroidIsGame))
            {
                PlayerSettings.Android.androidIsGame = args.GetAsBool(BuilderArguments.Android.AndroidIsGame);
            }

            if (args.Contains(BuilderArguments.Android.UseAPKExpansionFiles))
            {
                PlayerSettings.Android.useAPKExpansionFiles =
                    args.GetAsBool(BuilderArguments.Android.UseAPKExpansionFiles);
            }

            PlayerSettings.Android.bundleVersionCode = config.BuildNumber;
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
                return "Android" + Environment.NewLine +
                       BuilderUtils.GetArgumentsDescription(typeof(BuilderArguments.Android), 2);
            }
        }
    }
}