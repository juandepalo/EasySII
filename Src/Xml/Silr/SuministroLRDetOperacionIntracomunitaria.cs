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
    [XmlRoot("SuministroLRDetOperacionIntracomunitaria")]
    public class SuministroLRDetOperacionIntracomunitaria
    {
        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLRDetOperacionIntracomunitaria")]
        public List<RegistroLRDetOperacionIntracomunitaria> RegistroLRDetOperacionIntracomunitaria { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRDetOperacionIntracomunitaria()
        {
            Cabecera = new Cabecera();
            RegistroLRDetOperacionIntracomunitaria = new List<RegistroLRDetOperacionIntracomunitaria>();
        }
    }
}