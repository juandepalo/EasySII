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
    [XmlRoot("BajaLROperacionesSeguros")]
    public class BajaLROperacionesSeguros
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("RegistroLROperacionesSeguros")]
        public List<RegistroLROpTrascendTribu> RegistroLROperacionesSeguros { get; set; }

        /// <summary>
        /// Constructor de la clase BajaLRFacturasRecibidas.
        /// </summary>
        public BajaLROperacionesSeguros()
        {
            Cabecera = new Cabecera();
            RegistroLROperacionesSeguros = new List<RegistroLROpTrascendTribu>();
        }
    }
}
