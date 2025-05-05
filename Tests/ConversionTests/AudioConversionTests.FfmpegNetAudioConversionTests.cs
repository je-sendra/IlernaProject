using IlernaProject.ClassLib.Converters;

namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Test class for FfmpegNet audio conversion.
/// This class inherits from AudioConversionTests and implements the conversion tests using the FfmpegNet library.
/// </summary>
[TestClass]
public class FfmpegNetAudioConversionTests : AudioConversionTests
{
    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_ValidInputFile_CreatesOutputFile()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp3");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.wav");
        IAudioConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "nonExistentFile.mp3");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.wav");
        IAudioConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp3");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.wav");
        File.Copy(inputFilePath, outputFilePath, true);
        IAudioConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp3");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.nonExistentFormat");
        IAudioConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.mp3");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.wav");
        IVideoConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

        // Act
        using var cancellationTokenSource = new CancellationTokenSource();
        var resultTask = ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, cancellationTokenSource.Token);
        await cancellationTokenSource.CancelAsync();
        var result = await resultTask;

        // Assert
        Assert.IsTrue(result.HasFailed);
    }
}