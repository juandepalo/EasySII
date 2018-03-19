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

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Datos Descuadre Contraparte.
    /// </summary>
    public class DatosDescuadreContraparte
    {

        /// <summary>
        /// Sumatorio Base Imponible declarada por la contraparte en la operación 
        /// con inversión de sujeto pasivo.Sólo se suministra si la factura es 
        /// parcialmente contrastada y hay discrepancia con la 
        /// información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string SumBaseImponibleISP{ get; set; }

        /// <summary>
        /// Sumatorio Base Imponible declarada por la contraparte en la operación sin 
        /// inversión de sujeto pasivo.Sólo se suministra si la factura es parcialmente 
        /// contrastada y hay discrepancia con la información declarada por 
        /// la contraparte. Decimal(14,2).
        /// </summary>
        public string SumBaseImponible { get; set; }

        /// <summary>
        ///Sumatorio de la cuota del impuesto declarada por la contraparte en la operacion
        ///sin inversión de sujeto pasivo.Sólo se suministra si la factura es parcialmente 
        ///contrastada y hay discrepancia con la información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string SumCuota{ get; set; }

        /// <summary>
        /// Sumatorio de la cuota del recargo de equivalencia declarada por la contraparte en la operacion 
        /// sin inversión de sujeto pasivo.Sólo se suministra si la factura es parcialmente contrastada y hay
        /// discrepancia con la información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string SumCuotaRecargoEquivalencia{ get; set; }

        /// <summary>
        /// Importe de la factura declarada por la contraparte. Sólo se suministra si la factura es parcialmente 
        /// contrastada y hay discrepancia con la información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string ImporteTotal{ get; set; }

        /// <summary>
        /// Constructor DatosDescuadreContraparte.
        /// </summary>
        public DatosDescuadreContraparte()
        {
        }
    }
}
