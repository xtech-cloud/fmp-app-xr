﻿using UnityEditor;
using UnityEngine;
using System.IO;

public static class BuildTools
{
    //--------------------------------
    // Settings
    //--------------------------------

    static string outpath = System.IO.Path.Combine(Application.dataPath, "../../_dist_");

    [MenuItem("BuildTools/AssetBundle/WebGL")]
    public static void BuildAssetBundleForWebGL()
    {
        buildAssetBundle(BuildTarget.WebGL);
    }

    [MenuItem("BuildTools/AssetBundle/Win32")]
    public static void BuildAssetBundleForWin32()
    {
        buildAssetBundle(BuildTarget.StandaloneWindows);
    }

    [MenuItem("BuildTools/AssetBundle/Win64")]
    public static void BuildAssetBundleForWin64()
    {
        buildAssetBundle(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("BuildTools/AssetBundle/Android")]
    public static void BuildAssetBundleForAndroid()
    {
        buildAssetBundle(BuildTarget.Android);
    }

    private static void buildAssetBundle(BuildTarget _target)
    {
        Directory.CreateDirectory(outpath);
        string path = System.IO.Path.Combine(outpath, convertToPlatform(_target));
        Directory.CreateDirectory(path);
        Debug.Log(string.Format("build assetbundle at {0}", path));
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ForceRebuildAssetBundle, _target);

        //remove unused files
        File.Delete(Path.Combine(path, convertToPlatform(_target)));
        File.Delete(Path.Combine(path, convertToPlatform(_target)) + ".manifest");
        foreach (string file in Directory.GetFiles(path))
        {
            if (!file.EndsWith(".manifest"))
                continue;
            string target = Path.Combine(path, Path.GetFileName(file));
            File.Delete(target);
        }
    }

    private static string convertToPlatform(BuildTarget _target)
    {
        if (BuildTarget.StandaloneWindows == _target)
            return "win32";
        if (BuildTarget.StandaloneWindows64 == _target)
            return "win64";
        if (BuildTarget.WebGL == _target)
            return "webgl";
        if (BuildTarget.Android == _target)
            return "android";
        return "unknow";
    }


}
