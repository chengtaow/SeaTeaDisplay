using GlmNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace SeaTeaDisplay
{
    class DataModel
    {
        private List<vec3> pointCloud;

        public DataModel()
        {
            pointCloud = new List<vec3>();
        }

        public List<vec3> GetPointCloud()
        {
            if (pointCloud.Count > 0)
                return pointCloud;
            return null;
        }

        public void ImportPointCloudFile(string fileName)
        {
            vec3 tempPt;
            string lineStr;
            pointCloud.Clear();
            char[] delimiter = new char[] {',', ' '};
            using StreamReader sr = new StreamReader(fileName);
            while ((lineStr = sr.ReadLine()) != null)
            {
                string[] numStr = lineStr.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (numStr.Length >= 3)
                {
                    if (float.TryParse(numStr[0], out float x) &&
                        float.TryParse(numStr[1], out float y) &&
                        float.TryParse(numStr[2], out float z))
                    {
                        tempPt.x = x;
                        tempPt.y = y;
                        tempPt.z = z;
                        pointCloud.Add(tempPt);
                    }
                }
            }
        }
    }
}
