using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.UnityLinker;
using UnityEditor.Build.Reporting;

namespace OpenAI
{
    public class LinkXmlTransferer : IUnityLinkerProcessor
    {
        int IOrderedCallback.callbackOrder => 0;

        public string GenerateAdditionalLinkXmlFile(BuildReport report, UnityLinkerBuildPipelineData data)
        {
            string linkXmlGuid = AssetDatabase.FindAssets("link.xml")[0];
            string linkXmlPath = AssetDatabase.GUIDToAssetPath(linkXmlGuid);
            return Path.GetFullPath(linkXmlPath);
        }

        public void OnBeforeRun(BuildReport report, UnityLinkerBuildPipelineData data) { }
        
        public void OnAfterRun(BuildReport report, UnityLinkerBuildPipelineData data) { }
    }
}
