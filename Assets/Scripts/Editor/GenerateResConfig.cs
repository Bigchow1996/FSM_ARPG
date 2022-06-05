using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
/***
 * Unity 编辑器扩展
 * 只在Unity编辑器中执行，为了增加Unity新功能的代码
 * 扩展菜单
 * 美化Inspector
 * 增加窗口
 * 注意：
 * 代码只能放在Editor目录中
 * 代码不会发布到平台中
 * 
 * */
public class GenerateResConfig
{
    [MenuItem("Tools/Resources/Generate Resource Config File")]
    public static void Generate()
    {
        //1 获取Resources目录中指定的资源路径
        string[] resFilePaths = AssetDatabase.FindAssets("t:Prefab t:Sprite", new string[] { "Assets/Resources"});
        for (int i = 0; i < resFilePaths.Length; i++)
        {
            //Assets/Resources/Prefabs/Skills/Static/Effect19.prefab
            string assetPath = AssetDatabase.GUIDToAssetPath(resFilePaths[i]);
            //2生成对应关系 资源名 = 路径
            string fileName = Path.GetFileNameWithoutExtension(assetPath);//Effect19
            string path = assetPath.Replace("Assets/Resources/", "");//Prefabs/Skills/Static/Effect19.prefab
            path = path.Substring(0, path.IndexOf('.')); //Prefabs/Skills/Static/Effect19
            resFilePaths[i] = fileName + "=" + path;
        }
        //写入配置文件
        File.WriteAllLines("Assets/StreamingAssets/Config/ResConfig.txt", resFilePaths);
        //刷新目录
        AssetDatabase.Refresh();
    }
}
