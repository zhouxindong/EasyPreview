using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPreview.PreviewHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyPreview.PreviewHandler.Tests
{
    [TestClass()]
    public class PreviewHandlerTests
    {
        [TestMethod()]
        public void GetCLSIDFromExtensionShellExTest()
        {
            var clsid = Kernel.PreviewHandler.GetCLSIDFromExtensionShellEx(Kernel.PreviewHandler.PreviewHandlerGuid,
                ".pdf");
            Console.WriteLine(clsid);
        }
    }
}