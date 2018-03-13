using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Identificador distinto al NIF.
    /// </summary>
    [Serializable]
    [XmlRoot("IDOtro", Namespace = Settings.NamespaceSii)]
    public class IDOtro
    {
        /// <summary>
        /// Código del país asociado al
        /// emisor de la factura.
        /// Alfanumérico(2) (ISO 3166-1 alpha-2 codes) L17.
        /// </summary>
        public string CodigoPais { get; set; }

        /// <summary>
        /// Clave para establecer el tipo de
        /// identificación en el pais de
        /// residencia.Alfanumérico(2) L4.
        /// </summary>
        public string IDType { get; set; }

        /// <summary>
        /// Número de identificación en el país de residencia.
        /// Alfanumérico(20).
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Representación textual de esta instancia de Parte.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (CodigoPais ?? "") + (IDType ?? "") + 
                ", " + (ID ?? "") ;
        }
    }
}
