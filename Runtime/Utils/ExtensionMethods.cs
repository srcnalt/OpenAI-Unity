using System.IO;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace OpenAI
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Read a PNG file and add it to this form.
        /// </summary>
        /// <param name="form">List of multipart form sections.</param>
        /// <param name="path">Path of the file to read.</param>
        /// <param name="name">Name of the form field.</param>
        public static void AddImage(this List<IMultipartFormSection> form, string path, string name)
        {
            if (path != null)
            {
                var data = File.ReadAllBytes(path);
                var fileName = Path.GetFileName(path);
                form.Add(new MultipartFormFileSection(name, data, fileName, "image/png"));
            }
        }
        
        /// <summary>
        ///     Read a JSONL file and add it to this form.
        /// </summary>
        /// <param name="form">List of multipart form sections.</param>
        /// <param name="path">Path of the file to read.</param>
        /// <param name="name">Name of the form field.</param>
        public static void AddJsonl(this List<IMultipartFormSection> form, string path, string name)
        {
            if (path != null)
            {
                var data = File.ReadAllBytes(path);
                var fileName = Path.GetFileName(path);
                form.Add(new MultipartFormFileSection(name, data, fileName, "application/json"));
            }
        }
        
        /// <summary>
        ///     Add a primitive value to the form.
        /// </summary>
        /// <param name="form">List of multipart form sections.</param>
        /// <param name="value">Value of the form field.</param>
        /// <param name="name">Name of the form field.</param>
        public static void AddValue(this List<IMultipartFormSection> form, object value, string name)
        {
            if (value != null)
            {
                form.Add(new MultipartFormDataSection(name, value.ToString()));
            }
        }
        
        /// <summary>
        ///     Set headers of the HTTP request with user credentials.
        /// </summary>
        /// <param name="request">this UnityWebRequest</param>
        /// <param name="configuration">Configuration file that contains user credentials.</param>
        /// <param name="type">The value of the Accept header for an HTTP request.</param>
        public static void SetHeaders(this UnityWebRequest request, Configuration configuration, string type = null)
        {
            if (configuration.Auth.Organization != null)
            {
                request.SetRequestHeader("OpenAI-Organization", configuration.Auth.Organization);
            }
            if (type != null)
            {
                request.SetRequestHeader("Content-Type", type);
            }
            request.SetRequestHeader("Authorization", "Bearer " + configuration.Auth.ApiKey);
        }
    }
}
