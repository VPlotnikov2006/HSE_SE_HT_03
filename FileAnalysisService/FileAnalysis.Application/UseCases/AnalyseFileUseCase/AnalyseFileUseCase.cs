using FileAnalysis.Application.Interfaces;
using FileAnalysis.Domain.Entities;
using FileAnalysis.Domain.Services;

namespace FileAnalysis.Application.UseCases.AnalyseFileUseCase;

public class AnalyseFileUseCase(
    IFileStorageClient fileStorage,
    ISimilarityAlgorithm algorithm,
    IPlagiarismReportRepository repo)
{
    private readonly IFileStorageClient _fileStorage = fileStorage;
    private readonly ISimilarityAlgorithm _algorithm = algorithm;
    private readonly IPlagiarismReportRepository _repo = repo;

    public AnalyseFileResponse Execute(AnalyseFileRequest request)
    {
        var files = _fileStorage.GetFilesIdByWorkId(request.WorkId);
        var target = _fileStorage.GetFileContent(request.FileId);

        var report = new PlagiarismReport(request.FileId, request.WorkId, target.Metadata.Owner);

        foreach (var otherId in files)
        {
            if (otherId == target.Metadata.FileId)
            {
                continue;
            }

            var other = _fileStorage.GetFileContent(otherId);

            if (other is null)
            {
                continue;
            }

            var score = _algorithm.Calculate(target.Content, other.Content);

            if (_algorithm.IsPlagiarism(score))
            {
                report.AddMatch(otherId, other.Metadata.Owner, score);
            }
        }

        _repo.Add(report);
        _repo.SaveChanges();

        return new AnalyseFileResponse(report.ReportId, report.HighestSimilarity);
    }
}
