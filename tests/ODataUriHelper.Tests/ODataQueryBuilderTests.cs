namespace ODataUriHelper.Tests
{
    public class ODataQueryBuilderTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("123-1281-GLT")]
        [InlineData("name eq 'Jane'")]
        public void FilterShouldAddValueToQueryStringWithFilterPrefix(string value)
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder.Filter(value);

            // Assert
            string expected = $"$filter={value}";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ID desc")]
        [InlineData("Name asc, OrderNumber desc")]
        [InlineData("ID, Name")]
        public void OrderByShouldAddValueToQueryStringWithOrderByPrefix(string value)
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder.OrderBy(value);

            // Assert
            string expected = $"$orderby={value}";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ID,Name")]
        [InlineData("DataCenter,OrderNumber")]
        [InlineData("FirstName,LastName")]
        public void SelectShouldAddValueToQueryStringWithSelectPrefix(string value)
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder.Select(value);

            // Assert
            string expected = $"$select={value}";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ID,Name")]
        [InlineData("DataCenter,OrderNumber")]
        [InlineData("FirstName,LastName")]
        public void ExpandShouldAddValueToQueryStringWithExpandPrefix(string value)
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder.Expand(value);

            // Assert
            string expected = $"$expand={value}";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ID eq 12", "contains(Name, 'John')")]
        [InlineData("Sales gt 12000", "LineItems le 5")]
        [InlineData("ID gt 300", "startswith(LastName, 'T')")]
        public void FilterExtensionShouldAddValueToQueryStringInCorrectOrderWithFilterPrefix(string filterBy, string andBy)
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder.Filter(filterBy).Filter(andBy);

            // Assert
            string expected = $"$filter={filterBy} and {andBy}";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ID,FirstName,LastName", "DateOfBirth,SSN")]
        [InlineData("FirstName", "LastName")]
        [InlineData("ID", "LastName")]
        public void SelectExtensionShouldAddValueToQueryStringInCorrectOrderWithSelectPrefix(string select, string and)
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder.Select(select).Select(and);

            // Assert
            string expected = $"$select={select},{and}";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringShouldReturnExpectedOutput()
        {
            // Arrange
            var builder = new ODataQueryBuilder();

            // Act
            builder
                .Filter("startswith(Name, 'J')").Filter("CreatedDate gt '1/1/2004'")
                .OrderBy("ID asc,Name desc")
                .Select("ID,Name").Select("CreatedDate")
                .Expand("Address1").Expand("Address2");

            // Assert
            string expected = "$filter=startswith(Name, 'J') and CreatedDate gt '1/1/2004'&$orderby=ID asc,Name desc&$select=ID,Name,CreatedDate&$expand=Address1,Address2";
            string actual = builder.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringShouldReturnExpectedOutputRegardlessOfOrder()
        {
            // Arrange
            var firstBuilder = new ODataQueryBuilder();
            var secondBuilder = new ODataQueryBuilder();
            var thirdBuilder = new ODataQueryBuilder();

            // Act
            firstBuilder
                .Filter("startswith(Name, 'J')")
                .OrderBy("ID asc,Name desc")
                .Select("ID,Name")
                .Expand("Address1")
                .AndSelect("CreatedDate")
                .AndFilter("CreatedDate gt '1/1/2004'")
                .AndExpand("Address2");

            secondBuilder
                .Filter("startswith(Name, 'J')").Filter("CreatedDate gt '1/1/2004'")
                .OrderBy("ID asc,Name desc")
                .Select("ID,Name").Select("CreatedDate")
                .Expand("Address1").Expand("Address2");

            thirdBuilder
                .Filter("startswith(Name, 'J') and CreatedDate gt '1/1/2004'")
                .OrderBy("ID asc,Name desc")
                .Select("ID,Name,CreatedDate")
                .Expand("Address1,Address2");

            // Assert
            string expected = "$filter=startswith(Name, 'J') and CreatedDate gt '1/1/2004'&$orderby=ID asc,Name desc&$select=ID,Name,CreatedDate&$expand=Address1,Address2";
            ODataQueryBuilder[] builders = [firstBuilder, secondBuilder, thirdBuilder];
            Assert.True(builders.All(actual => expected == actual.ToString()));
        }
    }
}
