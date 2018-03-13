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
    [XmlRoot("BajaLRFacturasRecibidas")]
    public class BajaLRFacturasRecibidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("RegistroLRBajaRecibidas")]
        public List<RegistroLRBajaRecibidas> RegistroLRBajaRecibidas { get; set; }

        /// <summary>
        /// Constructor de la clase BajaLRFacturasRecibidas.
        /// </summary>
        public BajaLRFacturasRecibidas()
        {
            Cabecera = new Cabecera();
            RegistroLRBajaRecibidas = new List<RegistroLRBajaRecibidas>();
        }
    }
}
