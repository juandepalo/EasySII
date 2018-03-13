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
    [XmlRoot("ConsultaLRFacturasEmitidas")]
    public class BajaLRFacturasEmitidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("RegistroLRBajaExpedidas")]
        public List<RegistroLRBajaExpedidas> RegistroLRBajaExpedidas { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public BajaLRFacturasEmitidas()
        {
            Cabecera = new Cabecera();
            RegistroLRBajaExpedidas = new List<RegistroLRBajaExpedidas>();
        }
    }
}
