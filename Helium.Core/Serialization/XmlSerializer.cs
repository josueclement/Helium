using Helium.Core.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Helium.Core.Serialization
{
    public class XmlSerializer : Serializer
    {
        #region Constructor

        public XmlSerializer(Type serializerType)
        {
            SerializerType = serializerType;
        }

        #endregion

        #region Properties

        public Type SerializerType { get; private set; }

        #endregion

        #region Public methods

        public void Serialize(object obj, Stream stream)
        {

        }

        public void Serialize(object obj, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                Serialize(obj, fs);
            }
        }

        #endregion

        #region Private methods

        protected bool IsObjectTreeNonRecursive(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
