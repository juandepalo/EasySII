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

using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{

    /// Libro de registro de Facturas emitidas.
    /// </summary>
    [Serializable]
    [XmlRoot("RespuestaConsultaFactInformadasProveedor")]
    public class RespuestaConsultaFactInformadasProveedor
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Order = 1, Namespace = Settings.NamespaceSiiRQ)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Indica si hay más facturas en la consulta realizada
        /// Si hay más datos pendientes, este campo tendrá valor
        /// “S” y se podrán realizar nuevas consultas indicando
        /// la identificación de la última factura a partir de la
        /// cual se devolverán los siguientes registros.
        /// Alfanumérico(1).
        /// Valores posibles: “S” o “N”
        /// </summary>
        [XmlElement("IndicadorPaginacion", Namespace = Settings.NamespaceSiiRQ)]
        public string IndicadorPaginacion { get; set; }

        /// <summary>
        /// Indica si hay facturas para la consulta realizada.
        /// Valores posibles: “ConDatos” o “SinDatos”.
        /// </summary>
        [XmlElement("ResultadoConsulta", Namespace = Settings.NamespaceSiiRQ)]
        public string ResultadoConsulta { get; set; }

        /// <summary>
        /// Bloque que contiene campos de la factura
        /// informados por el proveedor. Se obtendrán como
        /// máximo 10.000 facturas, es decir, este bloque 
        /// puede repetirse 10.000 veces como máximo.
        /// </summary>
        public RegistroRespuestaConsultaFactInformadasProveedor RegistroRespuestaConsultaFactInformadasProveedor { get; set; }

    }
}
