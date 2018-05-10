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
    /// Elemento de detalle de facturas emitidas.
    /// </summary>
    [Serializable]
    public class FiltroConsulta
    {

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
        /// </summary>
        [XmlElement("PeriodoImpositivo", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoImpositivo { get; set; }

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("PeriodoLiquidacion", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoLiquidacion { get; set; } 

        /// <summary>
        /// ID Factura.
        /// </summary>
        [XmlElement("IDFactura", Namespace = Settings.NamespaceSiiLRQ)]
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Contraparte.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSiiLRQ)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Rango Fecha Presentacion.
        /// </summary>
        [XmlElement("FechaPresentacion")]
        public RangoFechaPresentacion FechaPresentacion { get; set; }

        /// <summary>
        /// Rango inicio fin fecha cuadre. 
        /// </summary>
        [XmlElement("FechaCuadre", Namespace = Settings.NamespaceSii)]
        public RangoFechaPresentacion FechaCuadre { get; set; }

        /// <summary>
        /// Indica si la factura ha sido modificada mediante una
        /// operación de modificación(A1, A4) o baja.
        /// Alfanumérico(1).
        /// Valores posibles: “S” o “N”.
        /// </summary>
        [XmlElement("FacturaModificada")]
        public string FacturaModificada { get; set; }

        /// <summary>
        /// <para> Lista L23: Estado de cuadre de la factura</para>
        /// <para> 1: No contrastable. Estas facturas no permiten contrastarse.</para>
        /// <para> 2: En proceso de contraste. Estado "temporal" entre 
        /// el alta/modificación de la factura y su intento de cuadrea</para> 
        /// <para> 3: No contrastada. El emisor o el receptor no han registrado 
        /// la factura (no hay coincidencia en el NIF del emisor, número de factura 
        /// del emisor y fecha de expedición).</para> 
        /// <para> 4: Parcialmente contrastada. El emisor y el receptor han registrado la factura (coincidencia 
        /// en el NIF del emisor, número de factura del emisor y fecha de expedición) pero tiene
        /// discrepancias en algunos datos de la factura.</para> 
        /// <para> 5: Contrastada. El emisor y el receptor han registrado la factura 
        /// (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) 
        /// con los mismos datos de la factura.</para>
        /// </summary>
        [XmlElement("EstadoCuadre", Namespace = Settings.NamespaceSii)]
        public string EstadoCuadre { get; set; }

        /// <summary>
        /// Identicador unívoco de la factura. Número+serie que identifica a la ultima factura 
        /// cuando el Tipo de Factura es un asiento resumen de facturas.
        /// </summary>
        public ClavePaginacion ClavePaginacion { get; set; }
   

        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public FiltroConsulta()
        {

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            PeriodoImpositivo = new PeriodoImpositivo();
        }
    }
}
