using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Representa una parte o interlocutor de negocio.
    /// </summary>
    public class Parte
    {
        /// <summary>
        /// Nombre-razón social del Titular del libro de registro de facturas expedidas.
        /// Alfanumérico(40).
        /// </summary>
        public string NombreRazon { get; set; }

        /// <summary>
        /// NIF del representante del titular del libro de registro. FormatoNIF(9).
        /// </summary>
        public string NIFRepresentate { get; set; }

        /// <summary>
        /// NIF asociado al titular del libro de registro. FormatoNIF(9).
        /// </summary>
        public string NIF { get; set; }

        /// <summary>
        /// Identificador de la parte distinto al NIF.
        /// </summary>
        [XmlElement("IDOtro", Namespace = Settings.NamespaceSii)]
        public IDOtro IDOtro { get; set; }

        /// <summary>
        /// Representación textual de esta instancia de Parte.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (NombreRazon ?? "") + ", " + (NIF ?? "") + ", " +
                ((IDOtro==null) ? "" : IDOtro.ToString());
        }
    }
}
