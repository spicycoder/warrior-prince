#tool nuget:?package=Pickles.CommandLine&version=2.20.1
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target = Argument("target", "BuildAndPublish");
var configuration = Argument("configuration", "Release");
///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});
Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});
///////////////////////////////////////////////////////////////////////////////
// Variables
///////////////////////////////////////////////////////////////////////////////
const string PicklesExecutable = "./tools/Pickles.CommandLine.2.20.1/tools/pickles.exe";
const string SolutionName = "./WarriorPrince.sln";
const string ArtifactsDirectory = "./artifacts";
const string ProjectToPublish = "./src/Api.Http/Api.Http.csproj";
const string TestProject = "./tests/Api.Tests/Api.Tests.csproj";
const string FeaturesDirectory = "./tests/Api.Tests/Features";
const string ResultsDirectory = "./tests/Api.Tests/TestResults";
const string TestResult = "results.trx";
///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
   .Does(() =>
{
   if (DirectoryExists(ArtifactsDirectory))
   {
      DeleteDirectory(
         ArtifactsDirectory,
         new DeleteDirectorySettings
         {
            Force = true,
            Recursive = true
         });
   }
   if (DirectoryExists(ResultsDirectory))
   {
       DeleteDirectory(
          ResultsDirectory,
          new DeleteDirectorySettings
          {
             Force = true,
             Recursive = true
          });
   }
   DotNetCoreClean(
      SolutionName,
      new DotNetCoreCleanSettings
      {
         Configuration = configuration
      });
});
Task("Restore")
   .Does(() =>
{
   DotNetCoreRestore(SolutionName);
});
Task("Build")
   .Does(() =>
{
   DotNetCoreBuild(
      SolutionName,
      new DotNetCoreBuildSettings
      {
         Configuration = configuration,
         NoRestore = true
      });
});
Task("Publish")
   .Does(() =>
{
   DotNetCorePublish(
      ProjectToPublish,
      new DotNetCorePublishSettings
      {
         Configuration = configuration,
         OutputDirectory = ArtifactsDirectory,
         NoRestore = true,
         NoBuild = true
      });
});
Task("ApiTest")
   .Does(() =>
{
   DotNetCoreTest(
      TestProject,
      new DotNetCoreTestSettings
      {
         Configuration = configuration,
         NoBuild = true,
         NoRestore = true,
         Logger = $"trx;LogFileName={TestResult}"
      });
});
Task("LivingDocumentation")
   .Does(() =>
{
   StartProcess(
      PicklesExecutable,
      $"--feature-directory={FeaturesDirectory} " +
      $"--output-directory={ResultsDirectory} " +
      $"--link-results-file={ResultsDirectory}/{TestResult} " +
      "--test-results-format=vstest " +
      "--documentation-format=dhtml");
});
Task("BuildAndPublish")
   .IsDependentOn("Clean")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("Publish");
Task("TestAndDocument")
   .IsDependentOn("Clean")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("ApiTest")
   .IsDependentOn("LivingDocumentation");
RunTarget(target);
