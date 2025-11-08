using Microsoft.ML.Data;

namespace mottu_challenge.DataModels
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public string Text { get; set; }
    }
}
