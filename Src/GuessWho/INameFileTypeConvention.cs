namespace GuessWho
{
    public interface INameFileTypeConvention
    {
        bool ContainsMaleNames(CensusDataFile file);
        bool ContainsFemaleNames(CensusDataFile file);
        bool ContainsSurnames(CensusDataFile file);
    }
}