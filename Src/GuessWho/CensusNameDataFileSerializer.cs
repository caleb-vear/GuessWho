using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuessWho
{
    public class CensusNameDataFileSerializer
    {

        public void Serialize(MemoryStream stream, IEnumerable<CensusNameData> nameData)
        {
            var writer = new BinaryWriter(stream);

            foreach (var data in nameData)
            {
                var nameBytes = Encoding.UTF8.GetBytes(data.Name);
                writer.Write((byte)nameBytes.Length);
                writer.Write(nameBytes);
                writer.Write(data.Frequency);
            }
        }

        public IEnumerable<CensusNameData> Deserialize(MemoryStream stream)
        {
            var reader = new BinaryReader(stream);

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var nameLen = reader.ReadByte();
                var nameBytes = reader.ReadBytes(nameLen);

                yield return new CensusNameData
                                 {
                                     Name = Encoding.UTF8.GetString(nameBytes),
                                     Frequency = reader.ReadInt16(),
                                 };
            }
        }
    }
}