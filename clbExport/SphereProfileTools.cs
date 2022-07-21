using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace clbExport
{
    static class SphereProfileTools
    {

        public static void Export(int longitude, string fileName)
        {
            ResetIndex();
            StringBuilder stringBuilder = new StringBuilder();
            string profileName = "SJSPHERE";
            stringBuilder.AppendLine("library_id \"1Gen\"");
            stringBuilder.AppendLine("section_type");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"name \"{profileName}\"");
            stringBuilder.AppendLine($"profitab \"{profileName}\" | USER | 0 |   | 1 | 1 | 1Gen.{profileName} | d |\"");
            stringBuilder.AppendLine("base_attribute");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("name \"d\"");
            stringBuilder.AppendLine("description \"albl_Diameter\"");
            stringBuilder.AppendLine("type dimension");
            stringBuilder.AppendLine("default 500");
            stringBuilder.AppendLine("}");
            StringBuilder geometryStringBuilder = new StringBuilder();
            StringBuilder parameterStringBuilder = new StringBuilder();
            geometryStringBuilder.AppendLine("geometry");
            geometryStringBuilder.AppendLine("{");
            geometryStringBuilder.AppendLine("name \"default\"");
            AppendFirstFace(geometryStringBuilder, longitude);
            var values = GetValues();
            for (int vIndex = 0; vIndex < values.Length; vIndex++)
            {
                double value = values[vIndex];
                AppendFace(geometryStringBuilder, parameterStringBuilder, longitude, vIndex + 1, value);
            }

            AppendLastFace(geometryStringBuilder, longitude, values.Length + 1);
            geometryStringBuilder.AppendLine("}");
            stringBuilder.Append(parameterStringBuilder.ToString());
            stringBuilder.Append(geometryStringBuilder.ToString());
            stringBuilder.AppendLine("}");
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(stringBuilder.ToString());
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }


        private static Dictionary<string,string> _parameterDict;

        private static string GetParameterName(StringBuilder parameterStringBuilder, string formula)
        {
            if (_parameterDict.ContainsKey(formula))
            {
                return _parameterDict[formula];
            }
            else
            {
                string name = $"p{_parameterDict.Count + 1}";
                parameterStringBuilder.AppendLine("expression");
                parameterStringBuilder.AppendLine("{");
                parameterStringBuilder.AppendLine($"name \"{name}\"");
                parameterStringBuilder.AppendLine("type dimension");
                parameterStringBuilder.AppendLine($"formula {formula}");
                parameterStringBuilder.AppendLine("}");
                _parameterDict.Add(formula, name);
                return name;
            }
        }


        private static void ResetIndex()
        {
            _parameterDict = new Dictionary<string, string>();
        }

        private static double[] GetValues()
        {
            return new double[]
            {
                0.01,
                0.02,
                0.05,
                0.1,
                0.15,
                0.2,
                0.25,
                0.3,
                0.35,
                0.4,
                0.45,
                0.5,
                0.55,
                0.6,
                0.65,
                0.7,
                0.75,
                0.8,
                0.85,
                0.9,
                0.95,
                0.98,
                0.99,
            };
        }

        private static void AppendFirstFace(StringBuilder geometryStringBuilder,int longitude)
        {
            geometryStringBuilder.AppendLine("face");
            geometryStringBuilder.AppendLine("{");
            geometryStringBuilder.AppendLine($"index 0");
            for (int longitudeIndex = 0; longitudeIndex < longitude; longitudeIndex++)
            {
                double x = 2 * Math.Cos(Math.PI * 2 * longitudeIndex / longitude);
                double y = -2 * Math.Sin(Math.PI * 2 * longitudeIndex / longitude);
                geometryStringBuilder.AppendLine($"point 0 {x:F3} {y:F3}");
            }
            geometryStringBuilder.AppendLine("}");
        }
        private static void AppendLastFace(StringBuilder geometryStringBuilder, int longitude,int valueCount)
        {
            geometryStringBuilder.AppendLine("face");
            geometryStringBuilder.AppendLine("{");
            geometryStringBuilder.AppendLine($"index {valueCount}");
            for (int longitudeIndex = 0; longitudeIndex < longitude; longitudeIndex++)
            {
                double x = 2 * Math.Cos(Math.PI * 2 * longitudeIndex / longitude);
                double y = -2 * Math.Sin(Math.PI * 2 * longitudeIndex / longitude);
                geometryStringBuilder.AppendLine($"point 1 {x:F2} {y:F2}");
            }
            geometryStringBuilder.AppendLine("}");
        }

        private static void AppendFace(StringBuilder geometryStringBuilder,StringBuilder parameterStringBuilder, int longitude,int vIndex,double value)
        {
            geometryStringBuilder.AppendLine("face");
            geometryStringBuilder.AppendLine("{");
            geometryStringBuilder.AppendLine($"index {vIndex}");
            double gap = (value - 0.5);
            double rRate = Math.Sqrt(0.5 * 0.5 - gap * gap);
            for (int longitudeIndex = 0; longitudeIndex < longitude; longitudeIndex++)
            {
                double xRate = Math.Cos(Math.PI * 2 * longitudeIndex / longitude) * rRate;
                string xParameterName = GetParameterName(parameterStringBuilder, $"{xRate:F3}*d");
                double yRate = -Math.Sin(Math.PI * 2 * longitudeIndex / longitude) * rRate;
                string yParameterName = GetParameterName(parameterStringBuilder, $"{yRate:F3}*d");
                geometryStringBuilder.AppendLine($"point {value:F2} {xParameterName} {yParameterName}");
            }
            geometryStringBuilder.AppendLine("}");
        }

        #region parameter



        #endregion

        #region const

        public static void ExportConst(string fileName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("library_id \"Misc\"");
            foreach (var d in GetDiameters())
            {
                AppendSectionType(stringBuilder, d);
            }
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(stringBuilder.ToString());
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        private static int GetLongitude(double diameter)
        {
            double v = Math.Sqrt(diameter * Math.PI);
            int ten = Convert.ToInt32(v - (v % 10.0)) + 40;
            return ten;
        }

        private static double[] GetDiameters()
        {
            return new[]
            {
                250.0,
                300.0,
                350.0,
                400.0,
                450.0,
                500.0,
                550.0,
                600.0,
                650.0,
                700.0,
                750.0,
                800.0,
                850.0,
                900.0,
                950.0,
                1000.0,
                1050.0,
                1100.0,
                1150.0,
                1200.0,
            };
        }

        private static void AppendSectionType(StringBuilder stringBuilder, double diameter)
        {
            var longitude = GetLongitude(diameter);
            string profileName = $"SPHERE{diameter:F0}";
            stringBuilder.AppendLine("section_type");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"name \"{profileName}\"");
            StringBuilder geometryStringBuilder = new StringBuilder();
            geometryStringBuilder.AppendLine("geometry");
            geometryStringBuilder.AppendLine("{");
            geometryStringBuilder.AppendLine("name \"default\"");
            AppendFirstFace(geometryStringBuilder, longitude);
            var values = GetValues();
            for (int vIndex = 0; vIndex < values.Length; vIndex++)
            {
                double value = values[vIndex];
                geometryStringBuilder.AppendLine("face");
                geometryStringBuilder.AppendLine("{");
                geometryStringBuilder.AppendLine($"index {vIndex+1}");
                double gap = (value - 0.5);
                double rRate = Math.Sqrt(0.5 * 0.5 - gap * gap);
                for (int longitudeIndex = 0; longitudeIndex < longitude; longitudeIndex++)
                {
                    double xRate = diameter * Math.Cos(Math.PI * 2 * longitudeIndex / longitude) * rRate;
                    double yRate = -diameter * Math.Sin(Math.PI * 2 * longitudeIndex / longitude) * rRate;
                    geometryStringBuilder.AppendLine($"point {value:F2} {xRate:F3} {yRate:F3}");
                }
                geometryStringBuilder.AppendLine("}");
            }

            AppendLastFace(geometryStringBuilder, longitude, values.Length + 1);
            geometryStringBuilder.AppendLine("}");
            stringBuilder.Append(geometryStringBuilder.ToString());
            stringBuilder.AppendLine("}");
        }

        #endregion
    }
}
