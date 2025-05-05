namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Base class for video conversion tests.
/// </summary>
public abstract class VideoConversionTests
{
    /// <summary>
    /// Initializes the test environment by copying test files from the base directory to a temporary directory.
    /// This method is called before each test is executed.
    /// </summary>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [TestInitialize]
    public void Setup()
    {
        string testDataDirectory = TestsConfig.TestsDataDirectory;
        if (!Directory.Exists(testDataDirectory))
        {
            throw new DirectoryNotFoundException($"Test data directory not found: {testDataDirectory}");
        }
        string baseFilesPath = TestsConfig.TestsDataBaseDirectory;
        if (!Directory.Exists(baseFilesPath))
        {
            throw new DirectoryNotFoundException($"Base test files directory not found: {baseFilesPath}");
        }

        string tempFilesPath = TestsConfig.TestsDataTempDirectory;
        if (!Directory.Exists(tempFilesPath))
        {
            Directory.CreateDirectory(tempFilesPath);
        }
        
        foreach (var file in Directory.GetFiles(baseFilesPath))
        {
            string fileName = Path.GetFileName(file);
            string destinationPath = Path.Combine(tempFilesPath, fileName);
            File.Copy(file, destinationPath, true);
        }
    }

    /// <summary>
    /// Cleans up the test environment by deleting copied test files from the temporary directory.
    /// This method is called after each test is executed.
    /// </summary>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [TestCleanup]
    public void Cleanup()
    {
        Directory.Delete(TestsConfig.TestsDataTempDirectory, true);
    }

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