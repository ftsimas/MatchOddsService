using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MatchOddsService.Models.DTOs
{
    [DisplayName("Error")]
    public class ErrorDTO
    {
        /// <summary>
        /// A unique identifier for the error
        /// </summary>
        /// <example>E0000</example>
        [JsonPropertyName("error")]
        public string Code { get; set; }

        /// <summary>
        /// A brief human-readable message
        /// </summary>
        /// <example>Example Error</example>
        public string Message { get; set; }

        /// <summary>
        /// A lengthier explanation of the error
        /// </summary>
        /// <example>This is an example description of the error.</example>
        public string Detail { get; set; }
    }
}
