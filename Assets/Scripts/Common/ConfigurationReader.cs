using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    public class ConfigurationReader
    {
        /// <summary>
        /// 在移动端获取配置文件
        /// </summary>
        /// <param name="fileName">配置文件的名称</param>
        /// <returns>返回字符串文本</returns>
        public static string GetConfigFile(string fileName)
        {
            string path;
            //如果使用IO读取，路径必须明确（从盘符开始的具体路径）,所以不用 System.IO.FileStream
            //C#写法，语法简单，但是执行效率低
            //if(Application.platform == RuntimePlatform.Android)
            //宏标签写法，语法复杂，但是执行效率高，在编译是时候就可以生效

#if UNITY_EDITOR || UNITY_STANDALONE
            //如果是unity编辑器或者是windows平台
            path = "file://" + Application.dataPath + "/StreamingAssets/Config/" + fileName;
#elif UNITY_ANDROID
            //如果是安卓平台
            path = "jar:file://" + Application.dataPath + "!/assets/Config/" + fileName;            
#elif UNITY_IPHONE
            //如果是苹果平台
            path = "file://" + Application.dataPath + "/Raw/Config/" + fileName;
#endif
            //WWW www = new WWW(path);
            UnityWebRequest request = UnityWebRequest.Get(path);
            request.SendWebRequest();//读取数据
            while (true)
            {
                if (request.downloadHandler.isDone)//是否读取完数据
                {
                    return request.downloadHandler.text;
                }
            }
        }
        /// <summary>
        /// 解析多行文本
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="handler"></param>
        public static void ReadConfigFile(string fileContent, Action<string> handler)
        {
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    handler(line);
                }
            }
        }
    }
}

