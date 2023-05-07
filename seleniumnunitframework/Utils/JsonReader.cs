using Newtonsoft.Json.Linq;

namespace SeleniumNUnitFramework.Utils
{
    internal class JsonReader
    {
        private string _jsonFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\TestData\\SearchTermsData.json";


        public JsonReader() { }

        public JsonReader(string jsonFileName) { 
            this._jsonFilePath = AppDomain.CurrentDomain.BaseDirectory + $"\\TestData\\{jsonFileName}";
        }

        public string GetData(string jsonKey) {
            string jsonString = File.ReadAllText(this._jsonFilePath);
            JToken jsonObject = JToken.Parse(jsonString);
            return jsonObject.SelectToken(jsonKey).Value<string>();
        }

        public object[] GetDataObj(string jsonKey) {
            string jsonString = File.ReadAllText(this._jsonFilePath);
            JToken jsonObject = JToken.Parse(jsonString);
            return jsonObject.SelectToken(jsonKey).Values<object>().ToArray();
        }
    }
}
