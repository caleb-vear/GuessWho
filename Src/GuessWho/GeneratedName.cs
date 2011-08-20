using System.Collections.Generic;
using System.Linq;

namespace GuessWho
{
    public class GeneratedName
    {
        readonly IEnumerable<string> _names;

        public IEnumerable<string> AllNames { get { return _names; } }

        public IEnumerable<string> GivenNames
        {
            get { return _names.Take(_names.Count() - 1); }
        }

        public IEnumerable<string> MiddleNames
        {
            get { return GivenNames.Skip(1); }
        }

        public string Surname
        {
            get { return _names.Last(); }
        }

        public string GivenName
        {
            get { return _names.First(); }
        }

        public Gender Gender { get; private set; }

        public GeneratedName(IEnumerable<string> names, Gender gender)
        {
            _names = names.Select(Capitalize);
            Gender = gender;
        }

        string Capitalize(string name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        }

        public override string ToString()
        {
            return string.Join(" ", _names);
        }
    }
}