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
    serving sii XML data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Respuesta envío Libro de registro de Facturas expedidas.
    /// </summary>
    [Serializable]
    [XmlRoot("RespuestaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiR)]
    public class RespuestaLRF
    {
        /// <summary>
        /// Código CSV asignado al envío. 
        /// Código seguro de verificación asociado a la petición enviada.
        /// </summary>
        [XmlElement("CSV", Namespace = Settings.NamespaceSiiR)]
        public string CSV { get; set; }

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("DatosPresentacion", Namespace = Settings.NamespaceSiiR)]
        public DatosPresentacion DatosPresentacion { get; set; }


        /// <summary>
        /// Datos de cabecera.
        /// Cabecera equivalente a la enviada en la petición de suministro / baja
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSiiR)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Estado Envio.
        /// Campo que especifica si el conjunto de facturas ha sido registrada
        /// correctamente, o ha sido rechazada, o se ha aceptado de forma parcial (lista L14).
        /// </summary>
        [XmlElement("EstadoEnvio", Namespace = Settings.NamespaceSiiR)]
        public string EstadoEnvio { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RespuestaLinea", Namespace = Settings.NamespaceSiiR)]
        public List<RespuestaLinea> RespuestaLinea { get; set; }

        /// <summary>
        /// Representación textual de esta instancia de RespuestaLinea.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (CSV??"") +", "+ EstadoEnvio+ ", " +
                RespuestaLinea.Count.ToString();
        }
    }
}
