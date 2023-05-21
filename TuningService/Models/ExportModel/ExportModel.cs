
using TuningService.Models.ViewModels;

namespace TuningService.Models.ExportModel
{
    internal class ExportModel
    {
        public string FileName { get; set; }

        public DataForProcessing[] Data { get; set; }
    }
}
