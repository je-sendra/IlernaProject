using FFmpeg.NET;
using IlernaProject.ClassLib.Models;

namespace IlernaProject.ClassLib.Converters;

/// <summary>
/// Implementation of IVideoConverter using FFmpeg.NET library.
/// Repository: https://github.com/cmxl/FFmpeg.NET
/// </summary>
public class FfmpegNetConverter : IVideoConverter, IAudioConverter
{
    private readonly Engine _ffmpegEngine;

    /// <summary>
    /// Constructor for FfmpegNetConverter.
    /// Initializes the FFmpeg engine with the specified path.
    /// The path should point to the FFmpeg executable.
    /// </summary>
    /// <param name="enginePath"></param>
    public FfmpegNetConverter(string enginePath)
    {
        // Initialize the FFmpeg engine
        _ffmpegEngine = new Engine(enginePath);
    }

    /// <inheritdoc/>
    async Task<OperationResult> IVideoConverter.ConvertAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken)
    {
        try
        {
            // Check if the input file exists
            if (!File.Exists(inputFilePath))
            {
                string message = $"Input file does not exist: {inputFilePath}";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = message,
                    UserMessage = message
                };
            }

            // Check if the output file already exists
            if (File.Exists(outputFilePath))
            {
                string message = $"Output file already exists: {outputFilePath}";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = message,
                    UserMessage = message
                };
            }

            InputFile inputFile = new(inputFilePath);
            OutputFile? outputFile = new(outputFilePath);

            await _ffmpegEngine.ConvertAsync(inputFile, outputFile, cancellationToken);

            // Check if the output file was created successfully
            if (!File.Exists(outputFilePath))
            {
                string message = $"Output file was not created: {outputFilePath}";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = message,
                    UserMessage = message
                };
            }

            // Return success result
            return new OperationResult
            {
                HasFailed = false,
                InternalMessage = "Conversion successful",
                UserMessage = "Conversion completed successfully."
            };
        }
        catch (Exception ex)
        {
            // Return failure result
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = ex.Message,
                UserMessage = "An error occurred during conversion. Please try again."
            };
        }
    }

    async Task<OperationResult> IAudioConverter.ConvertAsync(string inputFilePath, string outputFilePath, CancellationToken cancellationToken)
    {
        // Reuse the same conversion logic as in IVideoConverter
        return await ((IVideoConverter)this).ConvertAsync(inputFilePath, outputFilePath, cancellationToken);
    }
}