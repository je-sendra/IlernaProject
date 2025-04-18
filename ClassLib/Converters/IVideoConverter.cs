using IlernaProject.ClassLib.Models;

namespace IlernaProject.ClassLib.Converters;

/// <summary>
/// Base interface for video converters.
/// This interface defines a method for converting video files.
/// Implementations of this interface should provide the actual conversion logic.
/// </summary>
public interface IVideoConverter
{
    /// <summary>
    /// Converts a video file from one format to another. Saves the converted file to the specified output path.
    /// </summary>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<OperationResult> ConvertAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken);
}