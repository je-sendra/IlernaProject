using IlernaProject.ClassLib.Converters;

namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Test class for FfmpegNet video conversion.
/// This class inherits from VideoConversionTests and implements the conversion tests using the FfmpegNet library.
/// </summary>
[TestClass]
public class FfmpegNetVideoConversionTests : VideoConversionTests
{
    private readonly string _ffmpegPath = "/usr/bin/ffmpeg";

    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_ValidInputFile_CreatesOutputFile()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp4");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.avi");
        IVideoConverter ffmpegNetConverter = new FfmpegNetConverter(_ffmpegPath);

        // Act
        var result = await ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.HasFailed);
        Assert.IsTrue(File.Exists(outputFilePath));
    }

    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_InputFileDoesNotExist_ReturnsErrorResult()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "nonExistentFile.mp4");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.avi");
        IVideoConverter ffmpegNetConverter = new FfmpegNetConverter(_ffmpegPath);

        // Act
        var result = await ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.HasFailed);
    }

    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_OutputFileAlreadyExists_ReturnsErrorResult()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp4");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.avi");
        File.Copy(inputFilePath, outputFilePath, true);
        IVideoConverter ffmpegNetConverter = new FfmpegNetConverter(_ffmpegPath);

        // Act
        var result = await ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.HasFailed);
    }

    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_ConversionFails_ReturnsErrorResult()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp4");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.nonExistentFormat");
        IVideoConverter ffmpegNetConverter = new FfmpegNetConverter(_ffmpegPath);

        // Act
        var result = await ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.HasFailed);
    }

    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_CancellationRequested_ReturnsErrorResult()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp4");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.avi");
        IVideoConverter ffmpegNetConverter = new FfmpegNetConverter(_ffmpegPath);

        // Act
        using var cancellationTokenSource = new CancellationTokenSource();
        var resultTask = ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, cancellationTokenSource.Token);
        await cancellationTokenSource.CancelAsync();
        var result = await resultTask;

        // Assert
        Assert.IsTrue(result.HasFailed);
    }

}