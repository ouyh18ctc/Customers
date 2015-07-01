using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.ViewHelper;
using System.Web.Mvc;

namespace Customers.UnitTests.ViewHelper
{
    [TestClass]
    public class PagingHelpersTest
    {
        [TestMethod]
        public void TestPagingHelpers_Can_Generate_Page_Links()
        {

            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                + "&nbsp;|&nbsp;"
                + @"<a class=""selected"" href=""Page2"">2</a>"
                + "&nbsp;|&nbsp;"
                + @"<a href=""Page3"">3</a>");
        }
    }
}
