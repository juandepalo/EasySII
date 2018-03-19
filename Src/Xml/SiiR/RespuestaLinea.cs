/*
    This file is part of the EasySII (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
        http://www.irenesolutions.com/terms-of-use.pdf
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the EasySII software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving extract PDFs data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using EasySII.Xml.Silr;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Línea de respuesta.
    /// </summary>
    public class RespuestaLinea
    {
        /// <summary>
        /// Datos de cabecera. Identificador de la factura especificada 
        /// en la petición de suministro baja.
        /// </summary>
        [XmlElement("IDFactura", Namespace = Settings.NamespaceSiiR)]
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Estado Registro. Campo que especifica si la factura ha sido registrada correctamente, o
        /// ha sido rechazada, o se trata de un caso en el que la factura ha sido
        /// registrada pero con errores (lista L15).
        /// </summary>
        [XmlElement("EstadoRegistro", Namespace = Settings.NamespaceSiiR)]
        public string EstadoRegistro { get; set; }

        /// <summary>
        /// Código que identifica el tipo de error producido para una
        /// factura/registro específico (lista L16).
        /// </summary>
        [XmlElement("CodigoErrorRegistro", Namespace = Settings.NamespaceSiiR)]
        public string CodigoErrorRegistro { get; set; }

        /// <summary>
        /// Descripción del error asociado al código de error producido en una
        /// factura/registro. Alfanumérico(500).
        /// </summary>
        [XmlElement("DescripcionErrorRegistro", Namespace = Settings.NamespaceSiiR)]
        public string DescripcionErrorRegistro { get; set; }

		/// <summary>
		/// Código seguro de verificación del registro ya existente en el sistema.
		/// Solo se suministra este campo en dos casos:
		/// En la respuesta de la operación de alta: Si el registro enviado es
		/// rechazado por estar duplicado.
		/// En la respuesta de la operación de baja: Si el registro enviado es
		/// rechazado porque ya está dado de baja. Alfanumérico(16).
		/// </summary>
		[XmlElement("CSV", Namespace = Settings.NamespaceSiiR)]
		public string CSV { get; set; }

		/// <summary>
		/// Representación textual de esta instancia de RespuestaLinea.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
        {
            return ((IDFactura==null) ? "" : IDFactura.NumSerieFacturaEmisor) + ": " + 
                (EstadoRegistro??"");
        }
    }
}
