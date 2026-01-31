using DocumentsAPI.Core.Documents.Interfaces;
using Quartz;

namespace DocumentsAPI.Core.Jobs
{
    public class CleanUpOldDocumentsJob : IJob
    {
        private readonly ICleanUpOldDocumentsService _cleanUpOldDocumentsService;
        public CleanUpOldDocumentsJob(ICleanUpOldDocumentsService cleanUpOldDocumentsService)
        {
            _cleanUpOldDocumentsService = cleanUpOldDocumentsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _cleanUpOldDocumentsService.CleanUpOldDocumentsAsync();
        }
    }
}
