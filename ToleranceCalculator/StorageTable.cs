using Newtonsoft.Json;
using System;
using System.IO;

namespace ToleranceCalculator
{
    public static class StorageTable
    {
        public static object LoadTable(string filePath, Type dataType)
        {
            object obj = null;

            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();

                obj = serializer.Deserialize(file, dataType);
            }

            return obj;
        }

        public static void SaveTable(string filePath)
        {
            filePath += ".ttb";

            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(file, DimensionTable.DimensionModels);
            }
        }
    }
}
