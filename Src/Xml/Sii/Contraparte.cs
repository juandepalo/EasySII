using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Titular del libro de registro.
    /// </summary>
    [Serializable]
    [XmlRoot("Contraparte", Namespace = Settings.NamespaceSii)]
    public class Contraparte : Parte
    {   
    }
}
