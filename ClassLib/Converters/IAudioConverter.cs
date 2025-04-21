using IlernaProject.ClassLib.Models;

namespace IlernaProject.ClassLib.Converters;

/// <summary>
/// Base interface for audio converters.
/// This interface defines a method for converting audio files.
/// Implementations of this interface should provide the actual conversion logic.
/// </summary>
public interface IAudioConverter
{
    /// <summary>
    /// Converts an audio file from one format to another. Saves the converted file to the specified output path.
    /// </summary>
    /// <param name="inputFilePath"></param>
    /// <param name="outputFilePath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<OperationResult> ConvertAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken);
}