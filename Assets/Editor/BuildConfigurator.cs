using UnityEngine;
using UnityEditor;

public class BuildConfigurator
{
    [MenuItem("Build/Configure Settings")]
    static void Configure()
    {
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[] {
            new EditorBuildSettingsScene("Assets/Ali/Scenes/test 1.unity", true),
        };

        PlayerSettings.productName = "Glitch Quest";
        PlayerSettings.companyName = "Zerde Games";
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;

#if UNITY_ANDROID
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel22;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30;
#endif

#if UNITY_IOS
        PlayerSettings.iOS.targetOSVersionString = "11.0";
#endif
    }
}
