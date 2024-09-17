using ArweaveAO.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ArweaveAO.Responses
{
    public class MessageResult
    {
        [JsonPropertyName("Messages")]
        public List<Message> Messages { get; set; } = new();

        //[JsonPropertyName("Spawns")]
        //public List<object> Spawns { get; set; }

        //[JsonPropertyName("GasUsed")]
        //public int GasUsed { get; set; }
    }

    public class MessageResultArrayOutput
    {
        [JsonPropertyName("Messages")]
        public List<Message> Messages { get; set; } = new();

        //[JsonPropertyName("Spawns")]
        //public List<object> Spawns { get; set; }

        [JsonPropertyName("Output")]
        public List<OutputResult> Output { get; set; } = new();

        //[JsonPropertyName("GasUsed")]
        //public int GasUsed { get; set; }
    }

    public class MessageResultSingleOutput
    {
        [JsonPropertyName("Messages")]
        public List<Message> Messages { get; set; } = new();

        //[JsonPropertyName("Spawns")]
        //public List<object> Spawns { get; set; }

        [JsonPropertyName("Output")]
        public OutputResult? Output { get; set; }

        //[JsonPropertyName("GasUsed")]
        //public int GasUsed { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("Target")]
        public string? Target { get; set; }

        [JsonPropertyName("Anchor")]
        public string? Anchor { get; set; }

        [JsonPropertyName("Data")]
        public string? Data { get; set; }

        [JsonPropertyName("Tags")]
        public List<Tag> Tags { get; set; } = new();
    }

    public class OutputResult
    {
        [JsonPropertyName("data")]
        public OutputResultData? Data { get; set; }

        //[JsonPropertyName("print")]
        //public string? Print { get; set; }

        //[JsonPropertyName("prompt")]
        //public string? Prompt { get; set; }

    }

    public class OutputResultData
    {
        [JsonPropertyName("output")]
        public string? Output { get; set; }

        [JsonPropertyName("prompt")]
        public string? Prompt { get; set; }

        [JsonPropertyName("json")]
        public string? Json { get; set; }
    }
}
