using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Cabecera de un envío.
    /// </summary>
    [Serializable]
    [XmlRoot("Cabecera", Namespace = Settings.NamespaceSii)]
    public class Cabecera
    {
        /// <summary>
        /// Identificación de la versión del esquema utilizado para
        ///el intercambio de información.
        /// </summary>
        [XmlElement("IDVersionSii")]
        public string IDVersionSii { get; set; }

        /// <summary>
        /// Titular del libro de registro. Alfanumérico(3).
        /// </summary>
        [XmlElement("Titular")]
        public Titular Titular { get; set; }

        /// <summary>
        /// Tipo de operación (alta, modificación). Lista L0: A0, A1, A4.
        /// <para>A0: Alta de facturas/registro</para>
        /// <para>A1: Modificación de facturas/registros (errores registrales)</para>
        /// <para>A4: Modificación Factura Régimen de Viajeros</para>
        /// </summary>
        [XmlElement("TipoComunicacion")]
        public string TipoComunicacion { get; set; }

        /// <summary>
        /// Cabecera.
        /// </summary>
        public Cabecera()
        {
            IDVersionSii = Settings.Current.IDVersionSii;
            Titular = new Titular();
        }

        /// <summary>
        /// Representación textual de esta instancia de Cabecera.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (IDVersionSii ?? "") + ", " + Titular.ToString() + ", " +
                (TipoComunicacion??"");
        }

    }
}
