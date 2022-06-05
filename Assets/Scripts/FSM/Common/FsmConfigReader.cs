using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace AI.FSM
{
    public class FsmConfigReader
    {
        //外层字典：key 状态名  value 内层字典
        //内层字典：key 条件名  value 状态名
        public Dictionary<string, Dictionary<string, string>> map;
        string mainKey;//外层字典：key 状态名
        public FsmConfigReader(string fileName)
        {
            map = new Dictionary<string, Dictionary<string, string>>();
            string content = ConfigurationReader.GetConfigFile(fileName);//先获取文件
            ConfigurationReader.ReadConfigFile(content, LineHandler);//然后再解析文件
        }
        /// <summary>
        /// 解析单行文本
        /// </summary>
        /// <param name="line"></param>
        private void LineHandler(string line)
        {
            
            line = line.Trim();
            if (line == "") return;
            if (line.StartsWith("["))
            {
                //如果该行以"["开始，表示状态
                mainKey = line.Substring(1, line.Length - 2);
                map.Add(mainKey, new Dictionary<string, string>());
            }
            else
            {
                string[] keyValue = line.Split('>');
                map[mainKey].Add(keyValue[0], keyValue[1]);
            }
        }
    }

}
