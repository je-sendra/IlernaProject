using IlernaProject.ClassLib.Converters;

namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Test class for FfmpegNet image conversion.
/// This class inherits from ImageConversionTests and implements the conversion tests using the FfmpegNet library.
/// </summary>
[TestClass]
public class FfmpegNetImageConversionTests : ImageConversionTests
{
    /// <inheritdoc/>
    [TestMethod]
    public override async Task TestConvertAsync_ValidInputFile_CreatesOutputFile()
    {
        // Arrange
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.jpg");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.png");
        IImageConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "nonExistentFile.jpg");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.png");
        IImageConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.jpg");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.png");
        File.Copy(inputFilePath, outputFilePath, true);
        IImageConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.jpg");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.nonExistentFormat");
        IImageConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

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
        string inputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "test.jpg");
        string outputFilePath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.png");
        IImageConverter ffmpegNetConverter = new FfmpegNetConverter(TestsConfig.FfmpegPath);

        // Act
        using var cancellationTokenSource = new CancellationTokenSource();
        var resultTask = ffmpegNetConverter.ConvertAsync(inputFilePath, outputFilePath, cancellationTokenSource.Token);
        await cancellationTokenSource.CancelAsync();
        var result = await resultTask;

        // Assert
        Assert.IsTrue(result.HasFailed);
    }
}