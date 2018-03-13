using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos de un cobro/pago de factura.
    /// </summary>
    [Serializable]
    public class Cobro
    {

        /// <summary>
        /// Fecha cobro/pago dd-MM-yyyy.
        /// </summary>
        [XmlElement("Fecha", Namespace = Settings.NamespaceSii)]
        public string Fecha { get; set;}

        /// <summary>
        /// Importe total del cobro/pago.
        /// </summary>
        [XmlElement("Importe", Namespace = Settings.NamespaceSii)]
        public string Importe { get; set; }

        /// <summary>
        /// Medio de cobro/ pago según especificaciones (lista: L11 Medio de Pago/Cobro).
        /// <para>01: Transferencia</para>
        /// <para>02: Cheque</para>
        /// <para>03: No se cobra / paga (fecha límite de devengo / devengo 
        /// forzoso en concurso de acreedores)</para>
        /// <para>04: Otros medios de cobro / pago</para>   
        /// </summary>
        [XmlElement("Medio", Namespace = Settings.NamespaceSii)]
        public string Medio { get; set; }

        /// <summary>
        /// Indicador de si ha sido emitida por terceros.
        /// </summary>
        [XmlElement("Cuenta_O_Medio", Namespace = Settings.NamespaceSii)]
        public string Cuenta_O_Medio { get; set; }
     

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public Cobro()
        {
        }
    }
}
