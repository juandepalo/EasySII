using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Titular del libro de registro.
    /// </summary>
    [Serializable]
    [XmlRoot("Titular", Namespace = Settings.NamespaceSii)]
    public class Titular : Parte
    {
    }
}
