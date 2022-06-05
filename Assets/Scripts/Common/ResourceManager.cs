using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class ResourceManager
    {
        private static Dictionary<string, string> map;
        //静态构造函数：类被加载时执行一次，且只被执行一次
        /// <summary>
        /// 读取配置文件
        /// </summary>
        static ResourceManager()
        {
            map = new Dictionary<string, string>();
            string configFile =  ConfigurationReader.GetConfigFile("ResConfig.txt");
            ConfigurationReader.ReadConfigFile(configFile, BuildMap);
        }
        /// <summary>
        /// 构建数据字典
        /// </summary>
        /// <param name="line"></param>
        private static void BuildMap(string line)
        {
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0], keyValue[1]);
        }
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resName"></param>
        /// <returns></returns>
        public static T Load<T>(string resName) where T : UnityEngine.Object
        {
            if (!map.ContainsKey(resName)) return null;
            string path = map[resName];
            return Resources.Load<T>(path);
        }
    }
}

