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
const string SolutionName = "./WarriorPrince.sln";
const string ArtifactsDirectory = "./artifacts";
const string ProjectToPublish = "./src/Api.Http/Api.Http.csproj";
///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean")
   .Does(() =>
{
   if (DirectoryExists(ArtifactsDirectory))
   {
      DeleteDirectory(ArtifactsDirectory);
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
Task("BuildAndPublish")
   .IsDependentOn("Clean")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("Publish");
RunTarget(target);