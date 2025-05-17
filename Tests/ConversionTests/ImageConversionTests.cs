namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Base class for image conversion tests.
/// This class provides a template for image conversion tests.
/// It inherits from FolderCopyTest, which handles the setup and cleanup of the test environment.
/// </summary>
public abstract class ImageConversionTests : FolderCopyTest
{
    /// <summary>
    /// Tests the conversion of an image file from one format to another.
    /// This test should check if an output file at the specified path has been created.
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
    