using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Suministro de información de pagos de facturas recibidas.
    /// </summary>
    [Serializable]
    [XmlRoot("SuministroLRPagosRecibidas")]
    public class SuministroLRPagosRecibidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de cobros hasta 10.000.
        /// </summary>
        [XmlElement("RegistroLRPagos")]
        public List<RegistroLRPagos> RegistroLRPagos { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRPagosRecibidas()
        {
            Cabecera = new Cabecera();
            RegistroLRPagos = new List<RegistroLRPagos>();
        }
    }
}
