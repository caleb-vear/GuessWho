namespace GuessWho
{
    public class DefaultNameFileTypeConvention : INameFileTypeConvention
    {
        public bool ContainsMaleNames(CensusDataFile file)
        {
            return file.Filename.ToLower().Contains(".male.");
        }

        public bool ContainsFemaleNames(CensusDataFile file)
        {
            return file.Filename.ToLower().Contains(".female.");
        }

        public bool ContainsSurnames(CensusDataFile file)
        {
            return file.Filename.ToLower().Contains(".last.");
        }
    }
}