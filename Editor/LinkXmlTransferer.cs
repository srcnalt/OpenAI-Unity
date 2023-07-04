using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.UnityLinker;
using UnityEditor.Build.Reporting;

namespace OpenAI
{
    public class LinkXmlTransferer : IUnityLinkerProcessor
    {
        private const string SearchFolder = "Packages/com.srcnalt.openai-unity/Resources";
        
        int IOrderedCallback.callbackOrder => 0;

        public string GenerateAdditionalLinkXmlFile(BuildReport report, UnityLinkerBuildPipelineData data)
        {
            string[] linkXmlGuid = AssetDatabase.FindAssets("link", new string[] { SearchFolder });
            string linkXmlPath = AssetDatabase.GUIDToAssetPath(linkXmlGuid[0]);
            return Path.GetFullPath(linkXmlPath);
        }

        public void OnBeforeRun(BuildReport report, UnityLinkerBuildPipelineData data) { }
        
        public void OnAfterRun(BuildReport report, UnityLinkerBuildPipelineData data) { }
    }
}
