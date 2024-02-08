
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;
using UnityEditor;
using UnityEngine.Device;


namespace nicorueda

{

    public static class Floders
    {
        public static void CreateDirectories(string root, params string[] dir)
        {
            foreach (var newDirectory in dir)
            {
                CreateDirectory(Combine(dataPath, root, newDirectory));
            }
        }
    }


    public static class Packages
    {
        public static async Task ReplacePackagesFromGist(string id, string user = "NicoRuedaA")
        {
            var url = GetGistUrl(id, user);
            var contents = await GetContents(url);
            ReplacePackageFile(contents);
        }

        public static string GetGistUrl(string id, string user = "NicoRuedaA") => $"https://gist.githubusercontent.com/{user}/{id}/raw";

        public static async Task<string> GetContents(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public static void ReplacePackageFile(string contents)
        {
            var existing = Path.Combine(Application.dataPath, "../Packages.manifest.json");
            File.WriteAllText(existing, contents);
            UnityEditor.PackageManager.Client.Resolve();
        }
        
        
        public static void InstallUnityPackage(string packageName) =>
            UnityEditor.PackageManager.Client.Add($"com.unity.{packageName}");
        
    }
    





    public static class ToolsMenu
    {

        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Floders.CreateDirectories("_Project", "Scripts", "Art", "Scenes");
            Refresh();
        }

        [MenuItem("Tools/Setup/Load New Manifest")]
        static async void LoadNewManifest() =>
            await Packages.ReplacePackagesFromGist("acc5bc0017ddb5ad5d13d44dbd07f964");


        [MenuItem("Tools/Setup/Packages/New Input System")]
        static void AddNewInputSystem() => Packages.InstallUnityPackage("inputsystem");
        
        [MenuItem("Tools/Setup/Packages/Post Processing")]
        static void AddPostProessing() => Packages.InstallUnityPackage("postprocessing");
        
        [MenuItem("Tools/Setup/Packages/Cinemachine")]
        static void AddPostCinemachine() => Packages.InstallUnityPackage("cinemachine");


        /* {
             var url = GetGistUrl("acc5bc0017ddb5ad5d13d44dbd07f964");
             var contents = await GetContents(url);
             ReplacePackageFile(contents);
         }

         public static void Dir(string root, params string[] dir)
         {
             var fullPath = Combine(dataPath, root);
             foreach (var newDirectory in dir)
             {
                 CreateDirectory(Combine(fullPath, newDirectory));
             }
         }

         static string GetGistUrl(string id, string user = "NicoRuedaA") =>
             $

         static async Task<string> GetContents(string url)
         {
             using var client = new HttpClient();
             var response = await client.GetAsync(url);
             var content = await response.Content.ReadAsStringAsync();
             return content;
         }

         static void ReplacePackageFile(string contents)
         {
             var existing = Path.Combine(Application.dataPath, "../Packages/manifest.json");
             File.WriteAllText(existing, contents);
             UnityEditor.PackageManager.Client.Resolve();
         }
 */
    }
}
