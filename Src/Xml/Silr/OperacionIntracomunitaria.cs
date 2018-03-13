using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos de la Operación Intracomunitaria.
    /// </summary>
    [Serializable]
    public class OperacionIntracomunitaria
    {
        /// <summary>
        /// Tipo Operación según lista L12:
        /// <para>A: El envío o recepción de bienes para la realización de los informes parciales o trabajos mencionados
        ///          en el artículo 70, apartado uno, número 7º, de la Ley del Impuesto (Ley 37/1992)</para>
        /// <para>B: Las transferencias de bienes y las adquisiciones intracomunitarias de bienes comprendidas en los artículos
        ///          9, apartado 3º, y 16, apartado 2º, de la Ley del Impuesto (Ley 37/1992)</para>
        /// </summary>
        [XmlElement("TipoOperacion", Namespace = Settings.NamespaceSii)]
        public string TipoOperacion { get; set; }

        /// <summary>
        /// Clave de declarado intracomunitario según lista L13:
        /// <para>D: Declarante</para>
        /// <para>R: Remitente</para>
        /// </summary>
        [XmlElement("ClaveDeclarado", Namespace = Settings.NamespaceSii)]
        public string ClaveDeclarado { get; set; }

        /// <summary>
        /// Código del Estado miembro de origen o de envío según lista L18:
        /// <para>DE: Alemania</para>
        /// <para>AT: Austria</para>
        /// </summary>
        [XmlElement("EstadoMiembro", Namespace = Settings.NamespaceSii)]
        public string EstadoMiembro { get; set; }

        /// <summary>
        /// Plazo de Operación
        /// </summary>
        [XmlElement("PlazoOperacion", Namespace = Settings.NamespaceSii)]
        public string PlazoOperacion { get; set; }

        /// <summary>
        /// Descripción de los biene adquiridos.
        /// </summary>
        [XmlElement("DescripcionBienes", Namespace = Settings.NamespaceSii)]
        public string DescripcionBienes { get; set; }

        /// <summary>
        /// Dirección del operador intracomunitario.
        /// </summary>
        [XmlElement("DireccionOperador", Namespace = Settings.NamespaceSii)]
        public string DireccionOperador { get; set; }

        /// <summary>
        /// Otras facturas o documentación relativas a las operaciones de que se trate
        /// </summary>
        [XmlElement("FacturasODocumentacion", Namespace = Settings.NamespaceSii)]
        public string FacturasODocumentacion { get; set; }

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public OperacionIntracomunitaria()
        {
        }
    }
}
