namespace Firesplash.UnityAssets.SocketIO.MIT
{
    public class Parser {
        internal static SIOEventStructure Parse(string json) {
            string[] data = json.Split(new char[] { ',' }, 2);
            string eventName = data[0].Substring(2, data[0].Length - 3);

            //No Payload
            if(data.Length == 1) {
                return new SIOEventStructure()
                {
                    eventName = (eventName.Length > 1 && eventName[eventName.Length - 1] == '"' ? eventName.Remove(eventName.Length - 1) : eventName),
                    data = null
                };
            }

            //Plain String
            if (data[1].StartsWith("\""))
            {
                return new SIOEventStructure()
                {
                    eventName = (eventName.Length > 1 && eventName[eventName.Length - 1] == '"' ? eventName.Remove(eventName.Length - 1) : eventName),
                    data = data[1].Substring(1, data[1].Length - 3)
                };
            }

            //Json data
            return new SIOEventStructure()
            {
                eventName = (eventName.Length > 1 && eventName[eventName.Length - 1] == '"' ? eventName.Remove(eventName.Length - 1) : eventName),
                data = (data[1].Length > 1 && data[1][data[1].Length - 1] == ']' ? data[1].Remove(data[1].Length - 1) : data[1])
            };
        }

        public string ParseData(string json) {
            return json.Substring(1, json.Length - 2);
        }

    }
}