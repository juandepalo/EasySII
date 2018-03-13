using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Respuesta error en cualquiera de las operacines que podamos realizar.
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", ElementName = "Fault")]
    public class SoapFault
    {
        /// <summary>
        /// Indica el código del error.
        /// </summary>
        [XmlElement(Form = XmlSchemaForm.Unqualified, ElementName = "faultcode")]
        public String FaultCode { get; set; }

        /// <summary>
        /// Descripción del error.
        /// </summary>
        [XmlElement(Form = XmlSchemaForm.Unqualified, ElementName = "faultstring")]
        public String FaultDescription { get; set; }

        /// <summary>
        /// Detalle del error.
        /// </summary>
        [XmlElement(Form = XmlSchemaForm.Unqualified, ElementName = "detail")]
        public InnerException[] Detail { get; set; }
    }

    /// <summary>
    /// Descripción de la Excepción.
    /// </summary>
    [XmlType(Namespace = "http://my.namespace.com", TypeName = "InnerException")]
    public partial class InnerException
    {
        /// <summary>
        /// Mensaje de la Excepción.
        /// </summary>
        [XmlElement(Form = XmlSchemaForm.Unqualified, ElementName = "message")]
        public String Message { get; set; }
    }
}
