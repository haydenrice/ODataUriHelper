using System.Web;

namespace ODataUriHelper.Tests
{
    public class ODataUriBuilderTests
    {
        [Fact]
        public void ODataUriBuilder_ToStringShouldReturnExpectedOutput()
        {
            // Arrange
            string baseUri = "http://localhost/test";
            string filter = "startswith(Name, 'J') and CreatedDate gt '1/1/2004'";
            string orderby = "ID asc,Name desc";
            string select = "ID,Name,CreatedDate";
            string expand = "Address1,Address2";
            var uriBuilder = new ODataUriBuilder(baseUri);
            var expectedUriBuilder = new UriBuilder(baseUri);
            var expectedQuery = HttpUtility.ParseQueryString(string.Empty);
            
            // Act
            uriBuilder
                .Filter(filter)
                .OrderBy(orderby)
                .Select(select)
                .Expand(expand);

            expectedQuery["$filter"] = filter;
            expectedQuery["$orderby"] = orderby;
            expectedQuery["$select"] = select;
            expectedQuery["$expand"] = expand;
            expectedUriBuilder.Query = expectedQuery.ToString();

            // Assert
            string expected = expectedUriBuilder.ToString();
            string actual = uriBuilder.ToString();
            Assert.Equal(expected, actual);
        }
    }
}