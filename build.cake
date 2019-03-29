#tool "nuget:?package=GitVersion.CommandLine"

var target = string.IsNullOrEmpty(Argument("target", "Default")) ? "Default" : Argument("target", "Default");

public class BuildInfo
{
    public string GlobalAssemblyInfo { get; } = "./src/GlobalAssemblyInfo.cs";
    public string Sln { get; } = "./how-to-precache-an-xaf-winforms-application.sln";
}

void UpdateVersionInfo(Func<Version, Version> callback = null)
{
    var assemblyInfo = ParseAssemblyInfo(info.GlobalAssemblyInfo);
    var assemblyVersion = Version.Parse(assemblyInfo.AssemblyVersion);

    if(callback != null) assemblyVersion = callback(assemblyVersion);
    var gitVersion = GitVersion();
    var sha = gitVersion.Sha;
    var branch = gitVersion.BranchName;
    Information($"Version: {assemblyVersion}");
    Information($"Sha: {sha}");

    CreateAssemblyInfo(info.GlobalAssemblyInfo, new AssemblyInfoSettings
    {
        Configuration = assemblyInfo.Configuration,
        Company = assemblyInfo.Company,
        Description = assemblyInfo.Description,
        Product = assemblyInfo.Product,
        Copyright = assemblyInfo.Copyright,
        Trademark = assemblyInfo.Trademark,
        
        Version = assemblyVersion.ToString(),
        FileVersion = assemblyVersion.ToString(),
        InformationalVersion = $"{assemblyVersion}+{sha}+{branch}",
    }); 
}

var info = new BuildInfo();

Task("Version:Display").Does(() => UpdateVersionInfo());

Task("Version:Major").Does(() => UpdateVersionInfo(v => new Version(v.Major + 1, v.Minor, v.Build, v.Revision)));

Task("Version:Minor").Does(() => UpdateVersionInfo(v => new Version(v.Major, v.Minor + 1, v.Build, v.Revision)));

Task("Version:Build").Does(() => UpdateVersionInfo(v => new Version(v.Major, v.Minor, v.Build + 1, v.Revision)));

Task("Version:Rev").Does(() => UpdateVersionInfo(v => new Version(v.Major, v.Minor, v.Build, v.Revision + 1)));

Task("Restore")
    .Does(() =>
{
    NuGetRestore(info.Sln);   
});

Task("Build")
    .IsDependentOn("Version:Display")
    .IsDependentOn("Restore")
    .Does(() =>
{
    MSBuild(info.Sln);
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);