var target = string.IsNullOrEmpty(Argument("target", "Default")) ? "Default" : Argument("target", "Default");

Task("Version").Does(() =>
{
    Information("Hello");
});

Task("Build")
    .IsDependentOn("Version")
    .Does(() =>
{
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);