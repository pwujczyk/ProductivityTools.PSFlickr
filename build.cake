#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
	
// Define directories.
var buildDir = Directory("./out/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./ProductivityTools.PSFlickr.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      MSBuild("./ProductivityTools.PSFlickr.sln", settings =>
        settings.SetConfiguration(configuration));
});

Task("DeveloperBuild")
    .IsDependentOn("Build")
    .Does(() =>
{
	CopyFile("./src/ProductivityTools.PSFlickr.Configuration/ConfigurationLocal.xml", "./out/bin/Debug/Configuration.xml");
});

Task("Module")
    .IsDependentOn("Build")
    .Does(() =>
{
	CleanDirectory("./out/Module/");
	CopyDirectory("./out/bin/Debug/", "./out/Module/");
});




//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
