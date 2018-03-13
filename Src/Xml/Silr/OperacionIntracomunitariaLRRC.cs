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
    public class OperacionIntracomunitariaLRRC
    {
        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSiiRQ)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("DetOperIntracomunitarias", Namespace = Settings.NamespaceSiiRQ)]
        public OperacionIntracomunitaria OperacionIntracom { get; set; }


        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public OperacionIntracomunitariaLRRC()
        {
            Contraparte = new Contraparte();
            OperacionIntracom = new OperacionIntracomunitaria();
        }
    }
}
