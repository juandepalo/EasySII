using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Facturas expedidas.
    /// </summary>
    [Serializable]
    [XmlRoot("ConsultaLRFacturasEmitidas")]
    public class ConsultaLRFacturasEmitidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("FiltroConsulta")]
        public FiltroConsulta FiltroConsulta { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public ConsultaLRFacturasEmitidas()
        {
            Cabecera = new Cabecera();
            FiltroConsulta = new FiltroConsulta();
        }
    }
}
