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
    [XmlRoot("ConsultaCobros")]
    public class ConsultaCobros
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("FiltroConsultaCobros")]
        public FiltroConsultaCobrosPagos FiltroConsultaCobros { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public ConsultaCobros()
        {
            Cabecera = new Cabecera();
            FiltroConsultaCobros = new FiltroConsultaCobrosPagos();
        }
    }
}
