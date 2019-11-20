namespace GuessWho
{
    public class CensusNameData
    {
        public string Name { get; set; }
        public short Frequency { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CensusNameData;

            if (other == null) 
                return false;

            return other.Name == Name && other.Frequency == Frequency;
        }

        public override string ToString()
        {
            return (Name ?? string.Empty) + " | " + Frequency;
        }

        public override int GetHashCode()
        {
            return 29 * Name.GetHashCode() ^ Frequency.GetHashCode();
        }
    }
}