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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos de la factura.
    /// </summary>
    [Serializable]
    public class FacturaRecibidaLRRC
    {

        /// <summary>
        /// Especificación del tipo de factura a dar de alta: factura  normal, 
        /// factura rectificativa, tickets, factura emitida en sustitución de facturas.
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
        public string TipoFactura {get; set;}

        /// <summary>
        /// Campo que identifica si el tipo de factura rectificativa es por sustitución o por diferencia.
        /// Tipo rectificativa según lista L5:
        /// <para>S: Por sustitución.</para>
        /// <para>I: Por diferencias.</para>
        /// </summary>
        [XmlElement("TipoRectificativa", Namespace = Settings.NamespaceSiiRQ)]
        public string TipoRectificativa { get; set; }

        /// <summary>
        /// Facturas Agrupadas.
        /// </summary>
        [XmlArray("FacturasAgrupadas", Namespace = Settings.NamespaceSiiRQ)]
        [XmlArrayItem("IDFacturaAgrupada", Namespace = Settings.NamespaceSiiRQ)]
        public List<IDFactura> FacturasAgrupadas { get; set; }

        /// <summary>
        /// Facturas Rectificadas.
        /// </summary>
        [XmlArray("FacturasRectificadas", Namespace = Settings.NamespaceSiiRQ)]
        [XmlArrayItem("IDFacturaRectificada", Namespace = Settings.NamespaceSiiRQ)]
        public List<IDFactura> FacturasRectificadas { get; set; }

        /// <summary>
        /// Importe Rectificacion.
        /// </summary>
        [XmlElement("ImporteRectificacion", Namespace = Settings.NamespaceSiiRQ)]
        public ImporteRectificacion ImporteRectificacion { get; set; }

        /// <summary>
        /// Fecha en la que se ha realizado la operación siempre que sea  
        /// diferente a la fecha de expedición.
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaOperación", Namespace = Settings.NamespaceSiiRQ)]
        public string FechaOperación { get; set; }

        /// <summary>
        /// Clave que identificará el tipo de operación o el régimen 
        /// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendencia", Namespace = Settings.NamespaceSiiRQ)]
        public string ClaveRegimenEspecialOTrascendencia { get; set; }


        /// <summary>
        /// Importe total de la factura.
        /// </summary>
        [XmlElement("ImporteTotal", Namespace = Settings.NamespaceSiiRQ)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Base imponible a coste de la factura.
        /// </summary>
        [XmlElement("BaseImponibleACoste", Namespace = Settings.NamespaceSiiRQ)]
        public string BaseImponibleACoste { get; set; }

        /// <summary>
        /// Texto breve de la operación.
        /// </summary>
        [XmlElement("DescripcionOperacion", Namespace = Settings.NamespaceSiiRQ)]
        public string DescripcionOperacion { get; set; }

        /// <summary>
        /// Datos del DUA.
        /// </summary>
        [XmlElement("Aduanas", Namespace = Settings.NamespaceSiiRQ)]
        public Aduanas Aduanas { get; set; }

        /// <summary>
        /// Desglose Factura.
        /// </summary>
        [XmlElement("DesgloseFactura", Namespace = Settings.NamespaceSiiRQ)]
        public DesgloseFacturaR DesgloseFactura { get; set; }

 
        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSiiRQ)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaRegContable", Namespace = Settings.NamespaceSii)]
        public string FechaRegContable { get; set; }

        /// <summary>
        /// Cuota Deducible
        /// </summary>
        [XmlElement("CuotaDeducible", Namespace = Settings.NamespaceSiiRQ)]
        public string CuotaDeducible { get; set; }

        /// <summary>
        /// Indica si la factura tiene pagos o no
        /// </summary>
        [XmlElement("Pagos", Namespace = Settings.NamespaceSiiRQ)]
        public string Pagos { get; set; }

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public FacturaRecibidaLRRC()
        {
            Contraparte = new Contraparte();
            DesgloseFactura = new DesgloseFacturaR();
        }
    }
}
