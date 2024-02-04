using System.Web;

namespace ODataUriHelper.Application
{
    /// <summary>
    /// Provides a custom constructor for uniform resource identifiers (URIs) that represent
    /// an OData v4 query and modifies URIs for the Uri class.
    /// </summary>
    public sealed class ODataUriBuilder
    {
        /// <summary>
        /// The $compute clause of the OData query string.
        /// </summary>
        public string Compute => Query.Compute.ToString();

        /// <summary>
        /// The $expand clause of the OData query string.
        /// </summary>
        public string Expand => Query.Expand.ToString();

        /// <summary>
        /// The $filter clause of the OData query string.
        /// </summary>
        public string Filter => Query.Filter.ToString();

        /// <summary>
        /// The $orderby clause of the OData query string.
        /// </summary>
        public string OrderBy => Query.OrderBy.ToString();

        /// <summary>
        /// The OData query string.
        /// </summary>
        public string QueryString => Query.ToString();

        /// <summary>
        /// The $select clause of the OData query string.
        /// </summary>
        public string Select => Query.Select.ToString();

        /// <summary>
        /// The $skip clause of the OData query string.
        /// </summary>
        public int Skip => Query.Skip;

        /// <summary>
        /// The $top clause of the OData query string.
        /// </summary>
        public int Top => Query.Top;

        /// <summary>
        /// The OData v4 query string builder.
        /// </summary>
        internal ODataQueryBuilder Query { get; private set; } = new();

        /// <summary>
        /// The uniform resource identifier (URI) builder.
        /// </summary>
        internal UriBuilder Uri { get; private set; } = new UriBuilder();

        /// <summary>
        /// Initializes a new instance of the ODataUriBuilder class.
        /// </summary>
        public ODataUriBuilder() {}

        /// <summary>
        /// Initializes a new instance of the ODataUriBuilder class.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="UriFormatException" />
        public ODataUriBuilder(string uri) => Uri = new UriBuilder(uri);

        /// <summary>
        /// Initializes a new instance of the ODataUriBuilder class.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        public ODataUriBuilder(Uri uri) => Uri = new UriBuilder(uri);

        /// <summary>
        /// Gets the System.Uri instance constructed by the specified ODataUriBuilder instance.
        /// </summary>
        /// <returns>The Uri constructed by the ODataUriBuilder.</returns>
        /// <exception cref="UriFormatException" />
        public Uri GetODataUri() => GetUriBuilder().Uri;

        /// <summary>
        /// Returns the display string for the specified ODataUriBuilder instance.
        /// </summary>
        /// <exception cref="UriFormatException" />
        public override string ToString() => GetUriBuilder().ToString();

        private UriBuilder GetUriBuilder()
        {
            var queryBuilder = HttpUtility.ParseQueryString(Query.ToString());

            Uri.Query = queryBuilder.ToString();

            return Uri;
        }
    }

    public static class ODataUriBuilderExtensions
    {
        public static ODataUriBuilder AndExpand(this ODataUriBuilder builder, string value) => builder.Expand(value);

        public static ODataUriBuilder AndFilter(this ODataUriBuilder builder, string value) => builder.Filter(value);

        public static ODataUriBuilder AndSelect(this ODataUriBuilder builder, string value) => builder.Select(value);

        public static ODataUriBuilder Compute(this ODataUriBuilder builder, string value)
        {
            builder.Query.Compute(value);

            return builder;
        }

        public static ODataUriBuilder Expand(this ODataUriBuilder builder, string value)
        {
            builder.Query.Expand(value);

            return builder;
        }

        public static ODataUriBuilder Filter(this ODataUriBuilder builder, string value)
        {
            builder.Query.Filter(value);

            return builder;
        }

        public static ODataUriBuilder OrderBy(this ODataUriBuilder builder, string value)
        {
            builder.Query.OrderBy(value);

            return builder;
        }

        public static ODataUriBuilder Select(this ODataUriBuilder builder, string value)
        {
            builder.Query.Select(value);

            return builder;
        }

        public static ODataUriBuilder Skip(this ODataUriBuilder builder, int value)
        {
            builder.Query.Skip(value);

            return builder;
        }

        public static ODataUriBuilder Top(this ODataUriBuilder builder, int value)
        {
            builder.Query.Top(value);

            return builder;
        }
    }
}
