using System.Text;

namespace ODataUriHelper.Application
{
    /// <summary>
    /// Provides a custom constructor for an OData v4 query.
    /// </summary>
    internal class ODataQueryBuilder
    {
        public readonly ODataComputeClauseBuilder Compute = new();
        public readonly ODataExpandClauseBuilder Expand = new();
        public readonly ODataFilterClauseBuilder Filter = new();
        public readonly ODataOrderByClauseBuilder OrderBy = new();
        public readonly ODataSelectClauseBuilder Select = new();
        public int Skip;
        public int Top;

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Compute.Length > 0)
            {
                AppendClause(builder, Compute.ToString());
            }

            if (Filter.Length > 0)
            {
                AppendClause(builder, Filter.ToString());
            }

            if (OrderBy.Length > 0)
            {
                AppendClause(builder, OrderBy.ToString());
            }

            if (Select.Length > 0)
            {
                AppendClause(builder, Select.ToString());
            }

            if (Expand.Length > 0)
            {
                AppendClause(builder, Expand.ToString());
            }

            if (Top > 0)
            {
                AppendClause(builder, $"$top={Top}");
            }

            if (Skip > 0)
            {
                AppendClause(builder, $"$skip={Skip}");
            }

            return builder.ToString();
        }

        private static StringBuilder AppendClause(StringBuilder builder, string clause)
        {
            if (!string.IsNullOrEmpty(clause))
            {
                if (builder.Length > 0)
                {
                    builder.Append('&');
                }

                builder.Append(clause);
            }

            return builder;
        }
    }

    public static class ODataQueryBuilderExtensions
    {
        internal static ODataQueryBuilder AndExpand(this ODataQueryBuilder builder, string value) => builder.Expand(value);

        internal static ODataQueryBuilder AndFilter(this ODataQueryBuilder builder, string value) => builder.Filter(value);

        internal static ODataQueryBuilder AndSelect(this ODataQueryBuilder builder, string value) => builder.Select(value);

        internal static ODataQueryBuilder Compute(this ODataQueryBuilder builder, string value)
        {
            builder.Compute.Clear();
            builder.Compute.Append(value);

            return builder;
        }

        internal static ODataQueryBuilder Expand(this ODataQueryBuilder builder, string value)
        {
            _ = (builder.Expand.Length > 0) switch
            {
                true => builder.Expand.Append($",{value}"),
                false => builder.Expand.Append(value)
            };

            return builder;
        }

        internal static ODataQueryBuilder Filter(this ODataQueryBuilder builder, string value)
        {
            _ = (builder.Filter.Length > 0) switch
            {
                true => builder.Filter.Append($" and {value}"),
                false => builder.Filter.Append(value)
            };

            return builder;
        }

        internal static ODataQueryBuilder OrderBy(this ODataQueryBuilder builder, string value)
        {
            builder.OrderBy.Clear();
            builder.OrderBy.Append(value);

            return builder;
        }

        internal static ODataQueryBuilder Select(this ODataQueryBuilder builder, string value)
        {
            _ = (builder.Select.Length > 0) switch
            {
                true => builder.Select.Append($",{value}"),
                false => builder.Select.Append(value)
            };

            return builder;
        }

        internal static ODataQueryBuilder Skip(this ODataQueryBuilder builder, int value)
        {
            builder.Skip = value;

            return builder;
        }

        internal static ODataQueryBuilder Top(this ODataQueryBuilder builder, int value)
        {
            builder.Top = value;

            return builder;
        }
    }
}
