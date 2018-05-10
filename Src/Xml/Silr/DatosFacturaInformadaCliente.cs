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
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de facturas informadas por clientes.
    /// </summary>
    [Serializable]
    public class DatosFacturaInformadaCliente
    {

        /// <summary>
        /// Tipo factura según lista L2:
        /// <para>F1: Factura</para>
        /// <para>F2: Factura Simplificada (ticket)</para>
        /// <para>R1: Factura Rectificativa (Art 80.1 y 80.2 y error fundado en derecho)</para>
        /// <para>R2: Factura Rectificativa (Art. 80.3)</para>
        /// <para>R3: Factura Rectificativa (Art. 80.4)</para>
        /// <para>R4: Factura Rectificativa (Resto)</para>
        /// <para>R5: Factura Rectificativa en facturas simplificadas</para>
        /// <para>F3: Factura emitida en sustitución de facturas simplificadas facturadas y declaradas</para>
        /// <para>F4: Asiento resumen de facturas</para>
        /// </summary>
        [XmlElement("TipoFactura", Namespace = Settings.NamespaceSiiRQ)]
        public string TipoFactura { get; set; }

        /// <summary>
        /// Clave que identificará el tipo de operación o el régimen 
        /// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendencia", Namespace = Settings.NamespaceSiiRQ)]
        public string ClaveRegimenEspecialOTrascendencia { get; set; }

        /// <summary>
        /// Importe total de la factura. Decimal(12,2).
        /// </summary>
        [XmlElement("ImporteTotal", Namespace = Settings.NamespaceSiiRQ)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Texto breve de la operación. Alfanumérico(500).
        /// </summary>
        [XmlElement("DescripcionOperacion", Namespace = Settings.NamespaceSiiRQ)]
        public string DescripcionOperacion { get; set; }

        /// <summary>
        /// Desglose Factura
        /// </summary>
        [XmlElement("DesgloseFactura", Namespace = Settings.NamespaceSiiRQ)]
        public DesgloseFacturaInfCliente DesgloseFactura { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaRegContable", Namespace = Settings.NamespaceSiiRQ)]
        public string FechaRegContable { get; set; }

        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public DatosFacturaInformadaCliente()
        {       
        }
    }
}
