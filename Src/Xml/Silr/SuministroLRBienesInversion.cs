using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Bienes inversión.
    /// </summary>
    [Serializable]
    [XmlRoot("SuministroLRBienesInversion")]
    public class SuministroLRBienesInversion
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLRBienesInversion")]
        public List<RegistroLRBienesInversion> RegistroLRBienesInversion { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRBienesInversion()
        {
            Cabecera = new Cabecera();
            RegistroLRBienesInversion = new List<RegistroLRBienesInversion>();
        }
    }
}
