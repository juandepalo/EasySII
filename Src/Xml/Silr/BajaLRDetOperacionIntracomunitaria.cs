using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Operaciones Intracomunitarias.
    /// </summary>
    [Serializable]
    [XmlRoot("BajaLRDetOperacionIntracomunitaria")]
    public class BajaLRDetOperacionIntracomunitaria
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("RegistroLRBajaDetOperacionIntracomunitaria")]
        public List<RegistroLRBajaDetOperacionIntracomunitaria> RegistroLRBajaDetOperacionIntracomunitaria { get; set; }

        /// <summary>
        /// Constructor de la clase BajaLRFacturasRecibidas.
        /// </summary>
        public BajaLRDetOperacionIntracomunitaria()
        {
            Cabecera = new Cabecera();
            RegistroLRBajaDetOperacionIntracomunitaria = new List<RegistroLRBajaDetOperacionIntracomunitaria>();
        }
    }
}
