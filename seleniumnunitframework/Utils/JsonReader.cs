using Newtonsoft.Json.Linq;

namespace SeleniumNUnitFramework.Utils
{
    internal class JsonReader
    {
        private string _jsonFilePath = "TestData/SearchTermsTestData.json";


        public JsonReader() { }

        public JsonReader(string jsonFileName) { 
            this._jsonFilePath = $"TestData/{jsonFileName}";
        }

        public string GetData(string jsonKey) {
            string jsonString = File.ReadAllText(this._jsonFilePath);
            JToken jsonObj = JToken.Parse(jsonString);
            string jsonValue = jsonObj.SelectToken(jsonKey).Value<string>();
            Console.WriteLine(jsonValue);
            return jsonValue;
        }

        public string[] GetDataArray(string jsonKey) {
            string jsonString = File.ReadAllText(this._jsonFilePath);
            JToken jsonObj = JToken.Parse(jsonString);
            string[] jsonValues = jsonObj.SelectToken(jsonKey).Values<string>().ToArray();
            foreach(string value in jsonValues) {
                Console.WriteLine(value);
            }
            return jsonValues;
        }
    }
}
