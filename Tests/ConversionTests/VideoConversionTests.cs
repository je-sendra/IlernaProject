namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Base class for video conversion tests.
/// </summary>
public abstract class VideoConversionTests : FolderCopyTest
{
    /// <summary>
    /// Tests the conversion of a video file from one format to another.
    /// This test should fheck if an output file at the specified path has been created.
    /// </summary>
    public abstract Task TestConvertAsync_ValidInputFile_CreatesOutputFile();

    /// <summary>
    /// This test should check if an error result is returned when the input file does not exist.
    /// </summary>
    public abstract Task TestConvertAsync_InputFileDoesNotExist_ReturnsErrorResult();

    /// <summary>
    /// This test should check if an error result is returned when the output file already exists.
    /// </summary>
    public abstract Task TestConvertAsync_OutputFileAlreadyExists_ReturnsErrorResult();

    /// <summary>
    /// This test should check if an error result is returned when the conversion fails.
    /// </summary>
    public abstract Task TestConvertAsync_ConversionFails_ReturnsErrorResult();

    /// <summary>
    /// This test should check if an error result is returned when the cancellation is requested.
    /// </summary>
    public abstract Task TestConvertAsync_CancellationRequested_ReturnsErrorResult();
}