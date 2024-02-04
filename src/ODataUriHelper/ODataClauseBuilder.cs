using System.Text;

namespace ODataUriHelper.Application
{
    internal class ODataFilterClauseBuilder : ODataClauseBuilder
    {
        internal ODataFilterClauseBuilder() : base(ODataClause.Filter) { }

        internal ODataFilterClauseBuilder AndBy(string value)
        {
            Append($" and {value}");

            return this;
        }
    }

    internal class ODataOrderByClauseBuilder : ODataClauseBuilder
    {
        internal ODataOrderByClauseBuilder() : base(ODataClause.OrderBy) { }

        internal ODataOrderByClauseBuilder ThenBy(string value)
        {
            Append($",{value}");

            return this;
        }
    }

    internal class ODataExpandClauseBuilder : ODataClauseBuilder
    {
        internal ODataExpandClauseBuilder() : base(ODataClause.Expand) { }

        internal ODataExpandClauseBuilder And(string value)
        {
            Append($",{value}");

            return this;
        }
    }

    internal class ODataSelectClauseBuilder : ODataClauseBuilder
    {
        internal ODataSelectClauseBuilder() : base(ODataClause.Select) { }

        internal ODataSelectClauseBuilder And(string value)
        {
            Append($",{value}");

            return this;
        }
    }

    internal class ODataComputeClauseBuilder : ODataClauseBuilder
    {
        internal ODataComputeClauseBuilder() : base(ODataClause.Compute) { }
    }

    internal abstract class ODataClauseBuilder(ODataClause clause)
    {
        private StringBuilder _builder = new();

        internal ODataClause Clause { get; set; } = clause;
        internal int Length => ToString().Length;

        internal ODataClauseBuilder Append(string value)
        {
            _builder.Append(value);

            return this;
        }

        internal ODataClauseBuilder Clear()
        {
            _builder.Clear();

            return this;
        }

        internal ODataClauseBuilder Replace(char oldChar, char newChar)
        {
            _builder.Replace(oldChar, newChar);

            return this;
        }

        internal ODataClauseBuilder Replace(string oldString, string newString)
        {
            _builder.Replace(oldString, newString);

            return this;
        }

        public override string ToString() => (_builder.Length == 0) switch
        {
            true => string.Empty,
            false => $"${Clause.ToString().ToLowerInvariant()}={_builder}"
        };
    }
}
