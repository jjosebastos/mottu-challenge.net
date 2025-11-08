    using Microsoft.ML.Data;

namespace mottu_challenge.DataModels
{
    public class SentimentInput
    {
        [LoadColumn(0)]
        public string Text { get; set; }

        [LoadColumn(1)]
        public bool Label { get; set; }
    }
}
