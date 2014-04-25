using System;
using System.Collections.Generic;
using NServiceKit.Razor;
using NServiceKit.Razor.Managers;
using NServiceKit.VirtualPath;

namespace NServiceKit.ServiceHost.Tests.Formats_Razor
{
    public static class RazorFormatExtensions
    {
        public static RazorPage AddFileAndPage(this RazorFormat razorFormat, string filePath, string contents)
        {
            var pathProvider = (IWriteableVirtualPathProvider)razorFormat.VirtualPathProvider;
            pathProvider.AddFile(filePath, contents);
            return razorFormat.AddPage(filePath);
        }
    }
    
    public class RazorTestBase
	{
		public const string TemplateName = "Template";
		protected const string PageName = "Page";
        protected RazorFormat RazorFormat;
	}

}