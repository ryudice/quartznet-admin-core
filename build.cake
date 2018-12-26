
#addin "nuget:?package=Cake.Incubator&version=3.0.0"
#addin "nuget:?package=Markdown&version=2.2.1"
#tool "nuget:?package=GitVersion.CommandLine"
#addin "Cake.FileHelpers"
#addin "Cake.Npm"


var target = Argument("target", "Publish Docker");
var solution = "QuartzApi.Core.sln";
// var version =  GitVersion().SemVer;
var version =  "1.1.2";

var mark = new HeyRed.MarkdownSharp.Markdown();
var nugetSource = "https://api.nuget.org/v3/index.json";
var nugetApiKey = "oy2nsbzxi25t77vpnwzaabkfeukexbvupbxi3k4spwiue4";

var mainProjectDirectory = "src/QuartzApiCore.API/";

var assetsDirectory = $"{mainProjectDirectory}/wwwroot";

Information($"Using version {version}");

var nugetPublishDirectory = "artifacts";


Task("Npm Install")
.Does(()=>{
    var settings = new NpmInstallSettings{
        WorkingDirectory = assetsDirectory

    };
    NpmInstall(settings);
});

Task("Parcel")
    .IsDependentOn("Npm Install")
.Does(()=>{
    var settings = new NpmRunScriptSettings {
        WorkingDirectory = assetsDirectory,
        ScriptName = "build"
    };
    NpmRunScript(settings);
});

Task("Build").Does(()=>{
    DotNetCoreBuild(solution, new DotNetCoreBuildSettings(){
        MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(version)
    });
});

Task("Test").IsDependentOn("Build").Does(()=>{
      var testProjects = GetFiles("./tests/**/*.csproj");

         foreach(var testProject in testProjects) {
          DotNetCoreTest(testProject.FullPath);
        }
});

Task("Pack")
.IsDependentOn("Parcel")
.IsDependentOn("Test").Does(()=>{
    // var projectFile = GetFile("src/QuartzApiCore.API/*.csproj");
    var readMeMd = FileReadText("README.md");
    var readMe = mark.Transform(readMeMd);
    var summary = FileReadText("nuget-summary.txt");
    Information(summary);

    var nugetPackSettings = new NuGetPackSettings{
        Id = "Quartz.Admin.Core",
        Title = "Quartz Admin Core",
        Version = version,
        OutputDirectory = nugetPublishDirectory,
        Summary = summary,
        Authors = new[] {"Roberto Yudice"},
        Files = new [] { new NuSpecContent(){
            Exclude = "src/QuartzApiCore.API/wwwroot/**"
        } }
        // Description = summary
    };

      var settings = new DotNetCorePackSettings
     {
         Configuration = "Release",
         OutputDirectory = nugetPublishDirectory,
         NoBuild = false,
         MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(version)
         .WithProperty("PackageDescription",summary)
         .WithProperty("Authors","Roberto Yudice")
     };
    DotNetCorePack("src/QuartzApiCore.API/QuartzApiCore.API.csproj",settings);
    // NuGetPack("src/QuartzApiCore.API/QuartzApiCore.API.csproj", nugetPackSettings);
});

Task("Push")
.IsDependentOn("Pack")
.Does(()=>{
    var package = GetFiles(nugetPublishDirectory + "/*.nupkg");


    NuGetPush( package, new NuGetPushSettings{
        Source = nugetSource,
        ApiKey = nugetApiKey
    });

    Information("Package pushed");

});

RunTarget(target);