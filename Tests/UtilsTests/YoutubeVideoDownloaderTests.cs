using IlernaProject.ClassLib.Models;
using IlernaProject.ClassLib.Utils;
using IlernaProject.Tests.ConversionTests;

namespace IlernaProject.Tests.UtilsTests;

/// <summary>
/// Test class for YoutubeVideoDownloader.
/// </summary>
[TestClass]
public class YoutubeVideoDownloaderTests
{
    /// <summary>
    /// Executes before each test method in the class.
    /// This method is responsible for setting up the test environment.
    /// It creates a temporary directory for test data if it does not already exist.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        if (!Directory.Exists(TestsConfig.TestsDataTempDirectory))
        {
            Directory.CreateDirectory(TestsConfig.TestsDataTempDirectory);
        }
    }


    /// <summary>
    /// Executes after each test method in the class.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
    {
        TestsUtils.Cleanup();
    }

    /// <summary>
    /// Ensures that when a valid URL is provided, the output file is created successfully.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestDownloadVideoAsync_ValidUrl_CreatesOutputFile()
    {
        // Arrange
        string videoUrl = TestsConfig.YoutubeVideoUrl;
        string outputPath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.mp4");
        YoutubeVideoDownloader downloader = new();

        // Act
        OperationResult result = await downloader.DownloadVideoAsync(videoUrl, outputPath, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.HasFailed);
        Assert.IsTrue(File.Exists(outputPath));
    }

    /// <summary>
    /// Ensures that when the output file already exists, an error result is returned.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestDownloadVideoAsync_VideoAlreadyExists_ReturnsErrorResult()
    {
        // Arrange
        string videoUrl = TestsConfig.YoutubeVideoUrl;
        string outputPath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.mp4");
        YoutubeVideoDownloader downloader = new();
        File.Create(outputPath).Close();

        // Act
        var result = await downloader.DownloadVideoAsync(videoUrl, outputPath, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.HasFailed);
    }

    /// <summary>
    /// Ensures that when cancellation is requested, an error result is returned.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestDownloadVideoAsync_CancellationRequested_ReturnsErrorResult()
    {
        // Arrange
        string videoUrl = TestsConfig.YoutubeVideoUrl;
        string outputPath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.mp4");
        YoutubeVideoDownloader downloader = new();
        using CancellationTokenSource cts = new();

        // Act
        Task<OperationResult> resultTask = downloader.DownloadVideoAsync(videoUrl, outputPath, cts.Token);
        await cts.CancelAsync();

        OperationResult result = await resultTask;

        // Assert
        Assert.IsTrue(result.HasFailed);
        Assert.IsFalse(File.Exists(outputPath));
    }

    /// <summary>
    /// Ensures that when an invalid URL is provided, an error result is returned.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task TestDownloadVideoAsync_InvalidUrl_ReturnsErrorResult()
    {
        // Arrange
        string videoUrl = "invalid_url";
        string outputPath = Path.Combine(TestsConfig.TestsDataTempDirectory, "output.mp4");
        YoutubeVideoDownloader downloader = new();

        // Act
        OperationResult result = await downloader.DownloadVideoAsync(videoUrl, outputPath, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.HasFailed);
        Assert.IsFalse(File.Exists(outputPath));
    }
}