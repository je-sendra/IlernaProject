using IlernaProject.Tests.ConversionTests;

namespace IlernaProject.Tests;

/// <summary>
/// Represents a test template for tests that require copying files from a base directory to a temporary directory.
/// This class provides methods to set up and clean up the test environment.
/// </summary>
public abstract class FolderCopyTest
{
    /// <summary>
    /// Initializes the test environment by copying test files from the base directory to a temporary directory.
    /// This method is called before each test is executed.
    /// </summary>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [TestInitialize]
    public void Setup()
    {
        TestsUtils.Setup();
    }

    /// <summary>
    /// Cleans up the test environment by deleting copied test files from the temporary directory.
    /// This method is called after each test is executed.
    /// </summary>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [TestCleanup]
    public void Cleanup()
    {
        TestsUtils.Cleanup();
    }
}