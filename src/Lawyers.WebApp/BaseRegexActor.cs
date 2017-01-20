using System.Text.RegularExpressions;

namespace Lawyers.WebApp
{
    public abstract class BaseRegexActor : IActor
    {
        private readonly string _pattern;

        protected BaseRegexActor(string pattern)
        {
            _pattern = pattern;
        }

        public ResultData Handle(string param, int page)
        {
            if (param == null) return null;
            var match = Regex.Match(param, _pattern);

            if (!match.Success) return null;
            return InternalHandle(param, page, match.Groups);
        }

        protected abstract ResultData InternalHandle(string s, int page, GroupCollection matchGroups);
    }
}