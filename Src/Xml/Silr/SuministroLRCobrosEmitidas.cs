using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Suministro de información de cobros de Facturas expedidas.
    /// </summary>
    [Serializable]
    [XmlRoot("SuministroLRCobrosEmitidas")]
    public class SuministroLRCobrosEmitidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de cobros hasta 10.000.
        /// </summary>
        [XmlElement("RegistroLRCobros")]
        public List<RegistroLRCobros> RegistroLRCobros { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRCobrosEmitidas()
        {
            Cabecera = new Cabecera();
            RegistroLRCobros = new List<RegistroLRCobros>();
        }
    }
}
