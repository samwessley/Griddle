using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor;
 
public sealed class IOSBitcodePostprocessor
{
   public static bool enableBitcode = false;
  
   [PostProcessBuildAttribute]
   public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
       switch(target) {
           case BuildTarget.iOS:
               setupBitcode(pathToBuiltProject);
               break;
           default: break;
       }
   }
 
   private static void setupBitcode(string pathToBuiltProject) {
       var project = new PBXProject();
       var pbxPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
       project.ReadFromFile(pbxPath);
       setupBitcodeFramework(project);
       setupBitcodeMain(project);
       project.WriteToFile(pbxPath);
   }
 
   private static void setupBitcodeFramework(PBXProject project) {
       setupBitcode(project, project.GetUnityFrameworkTargetGuid());
   }
 
   private static void setupBitcodeMain(PBXProject project) {
       setupBitcode(project, project.GetUnityMainTargetGuid());
   }
 
   private static void setupBitcode(PBXProject project, string targetGUID) {
       project.SetBuildProperty(targetGUID, "ENABLE_BITCODE", enableBitcode ? "YES" : "NO");
   }
}