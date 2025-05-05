namespace IlernaProject.Tests.ConversionTests;

/// <summary>
/// Utility class for setting up the test environment.
/// This class provides methods to initialize and clean up the test environment.
/// It is used to copy test files from the base directory to a temporary directory before each test.
/// </summary>
public static class TestsUtils
{
    /// <summary>
    /// Initializes the test environment by copying test files from the base directory to a temporary directory.
    /// This method should be called before each test is executed.
    /// </summary>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static void Setup()
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
    /// This method should be called after each test is executed.
    /// </summary>
    public static void Cleanup()
    {
        string tempFilesPath = TestsConfig.TestsDataTempDirectory;
        if (Directory.Exists(tempFilesPath))
        {
            Directory.Delete(tempFilesPath, true);
        }
    }
}