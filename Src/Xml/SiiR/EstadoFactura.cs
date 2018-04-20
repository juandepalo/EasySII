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

using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Datos presentación lote asignado por la AEAT.
    /// </summary>
    [Serializable]
    [XmlRoot("EstadoFactura")]
    public class EstadoFactura
    {

        /// <summary>
        /// Estado de contraste de la factura.
        /// <para>Lista L23:</para> 
        /// <para>1: No contrastable. Estas facturas no permiten contrastarse.</para> 
        /// <para>2: En proceso de contraste. Estado "temporal" entre el alta/modificación 
        /// de la factura y su intento de cuadre.</para> 
        /// <para>3: No contrastada. El emisor o el receptor no han registrado la factura 
        /// (no hay coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición).</para>
        /// <para>4: Parcialmente contrastada. El emisor y el receptor han registrado la factura 
        /// (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) pero tiene 
        /// discrepancias en algunos datos de la factura.</para>
        /// <para> 5: Contrastada. El emisor y el receptor han registrado la factura (coincidencia en el NIF del 
        /// emisor, número de factura del emisor y fecha de expedición) con los mismos datos de la factura.</para>
        /// </summary>
        [XmlElement("EstadoCuadre", Namespace = Settings.NamespaceSiiRQ)]
        public string EstadoCuadre { get; set; }

        /// <summary>
        /// Timestamp del cuadre.
        /// </summary>
        [XmlElement("TimestampEstadoCuadre", Namespace = Settings.NamespaceSiiRQ)]
        public string TimestampEstadoCuadre { get; set; }

        /// <summary>
        /// Timestamp de la última modificación realizada a la factura.
        /// </summary>
        [XmlElement("TimestampUltimaModificacion", Namespace = Settings.NamespaceSiiRQ)]
        public string TimestampUltimaModificacion { get; set; }

        /// <summary>
        /// Estado de la factura en el registro.
        /// <para>Lista L24: </para>
        /// <para>Correcta</para>
        /// <para>AceptadaConErrores</para>
        /// <para>Anulada</para>
        /// </summary>
        [XmlElement("EstadoRegistro", Namespace = Settings.NamespaceSiiRQ)]
        public string EstadoRegistro { get; set; }


        /// <summary>
        /// Representación textual de esta instancia de DatosPresentacion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (TimestampUltimaModificacion ?? "") + ", " +
                EstadoRegistro;
        }

    }
}
