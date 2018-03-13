using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Facturas expedidas.
    /// </summary>
    [Serializable]
    [XmlRoot("SuministroLRFacturasEmitidas")]
    public class SuministroLRFacturasEmitidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLRFacturasEmitidas")]
        public List<RegistroLRFacturasEmitidas> RegistroLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRFacturasEmitidas()
        {
            Cabecera = new Cabecera();
            RegistroLRFacturasEmitidas = new List<RegistroLRFacturasEmitidas>();
        }
    }
}
