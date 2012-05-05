using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ContourAnalysis.Core
{
    [Serializable]
    public class ContourInformationCollection : List<ContourInformation>
    {
        public ContourInformationCollection()
        {
        }
        public ContourInformationCollection(IEnumerable<ContourInformation> contourInformations)
            : base(contourInformations)
        {
        }

        public void Save(Stream output)
        {
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(output, this);
        }

        public static ContourInformationCollection Load(Stream input)
        {
            BinaryFormatter serializer = new BinaryFormatter();

            return (ContourInformationCollection)serializer.Deserialize(input);
        }
    }
}
