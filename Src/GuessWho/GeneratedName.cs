using System.Collections.Generic;
using System.Linq;

namespace GuessWho
{
    public class GeneratedName
    {
        readonly IEnumerable<string> _names;

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
            _names = names;            
            Gender = gender;
        }

        public override string ToString()
        {
            return string.Join(" ", _names);
        }
    }
}