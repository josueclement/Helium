using System;
using System.Collections.Generic;
using System.Text;
using FastMember;

namespace Helium.Core.Serialization
{
    public abstract class Serializer
    {
        #region Constructor

        static Serializer()
        {
            TypeAccessors = new Dictionary<Type, TypeAccessor>();
        }

        #endregion

        #region Static members

        private static Dictionary<Type, TypeAccessor> TypeAccessors { get; set; }

        /// <summary>
        /// Register an object for serialization
        /// </summary>
        /// <param name="type"></param>
        public static void RegisterType(Type type)
        {
            TypeAccessors.Add(type, TypeAccessor.Create(type));

            //get properties and their attributes...
        }

        #endregion
    }
}
