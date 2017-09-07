
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace MySpace
{

    public class Tool
    {

        public static string LoadLuaFile(string fileName)
        {
            var fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath, fileName); //"LUA.txt
            FileInfo fInfo0 = new FileInfo(fileAddress);
            string s = "";
            if (fInfo0.Exists)
            {
                StreamReader r = new StreamReader(fileAddress);
                s = r.ReadToEnd();
                return s;
            }
            return null;
        }
        public static string PrintTool()
        {
            return "this is PrintTool in tool";
        }

        public static List<float> Tool_SmoothDamp(float current, float target, float smoothTime)
        {
            //float current, float target, ref float currentVelocity, float smoothTime
            List<float> result = new List<float>();
            float currentVelocity=0;
            float currentValue=0;
            currentValue= Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);
            result.Add(currentVelocity);
            result.Add(currentValue);
            return result ;
        }
    }
    
}
