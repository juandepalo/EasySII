using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos del bien inversión.
    /// </summary>
    [Serializable]
    public class BienesInversion
    {         

        /// <summary>
        /// Clave que identificará el bien inversión.
        /// </summary>
        [XmlElement("IdentificacionBien", Namespace = Settings.NamespaceSii)]
        public string IdentificacionBien { get; set; }


        /// <summary>
        /// Fecha inicio utilización. Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaInicioUtilizacion", Namespace = Settings.NamespaceSii)]
        public string FechaInicioUtilizacion { get; set; }

        /// <summary>
        /// Prorrata anual definitiva.
        /// </summary>
        [XmlElement("ProrrataAnualDefinitiva", Namespace = Settings.NamespaceSii)]
        public string ProrrataAnualDefinitiva { get; set; }

        /// <summary>
        /// Regularizacion Anual Deducción.
        /// </summary>
        [XmlElement("RegularizacionAnualDeduccion", Namespace = Settings.NamespaceSii)]
        public string RegularizacionAnualDeduccion { get; set; }


        /// <summary>
        /// Indentificación Entrega.
        /// </summary>
        [XmlElement("IndentificacionEntrega", Namespace = Settings.NamespaceSii)]
        public string IndentificacionEntrega { get; set; }

        /// <summary>
        /// Decimal(12,2).
        /// </summary>
        [XmlElement("RegularizacionDeduccionEfectuada", Namespace = Settings.NamespaceSii)]
        public string RegularizacionDeduccionEfectuada { get; set; }

        /// <summary>
        /// Constructor clase BienesInversion.
        /// </summary>
        public BienesInversion()
        {
        }
    }
}
