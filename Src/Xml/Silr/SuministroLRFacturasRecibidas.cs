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
    [XmlRoot("SuministroLRFacturasRecibidas")]
    public class SuministroLRFacturasRecibidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLRFacturasRecibidas")]
        public List<RegistroLRFacturasRecibidas> RegistroLRFacturasRecibidas { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRFacturasRecibidas()
        {
            Cabecera = new Cabecera();
            RegistroLRFacturasRecibidas = new List<RegistroLRFacturasRecibidas>();
        }
    }
}
