namespace FileAnalysis.Application.Interfaces;

public interface IWordCloudClient
{
    byte[] BuildWordCloud(string text);
}
