using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Bienes de Inversión.
    /// </summary>
    [Serializable]
    [XmlRoot("BajaLRBienesInversion")]
    public class BajaLRBienesInversion
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("RegistroLRBajaBienesInversion")]
        public List<RegistroLRBajaBienesInversion> RegistroLRBienesInversion { get; set; }

        /// <summary>
        /// Constructor de la clase BajaLRBienesInversion.
        /// </summary>
        public BajaLRBienesInversion()
        {
            Cabecera = new Cabecera();
            RegistroLRBienesInversion = new List<RegistroLRBajaBienesInversion>();
        }
    }
}
