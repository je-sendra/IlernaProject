using dotenv.net;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace IlernaProject.Tests;

/// <summary>
/// Configuration class for tests.
/// </summary>
public static class TestsConfig
{
    /// <summary>
    /// Static constructor to load environment variables from the .env file.
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    static TestsConfig()
    {
        Initialize();
    }

    private static void Initialize()
    {
        string envFilePath = Path.Combine(AppContext.BaseDirectory, "../../../../", ".env");
        if (!File.Exists(envFilePath))
        {
            throw new FileNotFoundException($"The .env file was not found at the expected location: {envFilePath}");
        }
        DotEnvOptions dotEnvOptions = new
        (
            envFilePaths: [envFilePath]
        );
        DotEnv.Load(dotEnvOptions);
    }

    /// <summary>
    /// Directory where test data files are located.
    /// This directory is set using the environment variable "TestsDataDirectory".
    /// If the environment variable is not set, an exception is thrown.
    /// </summary>
    public static string TestsDataDirectory => Environment.GetEnvironmentVariable("TestsDataDirectory") ?? throw new SettingsException("TestsDataDirectory not set in .env file");

    /// <summary>
    /// Temporary directory for test data files.
    /// This directory is used to store temporary files during tests.
    /// It is created as a subdirectory of the TestsDataDirectory.
    /// </summary>
    public static string TestsDataTempDirectory => Path.Combine(TestsDataDirectory, "temp");

    /// <summary>
    /// Directory where base test data files are located.
    /// This directory is used to store the original test files that are copied to the temporary directory before each test.
    /// It is a subdirectory of the TestsDataDirectory.
    /// </summary>
    public static string TestsDataBaseDirectory => Path.Combine(TestsDataDirectory, "base");

    /// <summary>
    /// Path to the FFMpeg executable.
    /// This path is set using the environment variable "FfmpegPath".
    /// If the environment variable is not set, an exception is thrown.
    /// </summary>
    public static string FfmpegPath => Environment.GetEnvironmentVariable("FfmpegPath") ?? throw new SettingsException("FfmpegPath not set in .env file");
}