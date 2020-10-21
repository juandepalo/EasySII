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

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Detalle IVA.
    /// </summary>
    public class DetalleIVA
    {
        /// <summary>
        /// Tipo Impositivo. Opcional a partir de la versión 0.7.
        /// </summary>
        public string TipoImpositivo { get; set; }

        /// <summary>
        /// Base Imponible. Decimal(12,2).
        /// </summary>
        public string BaseImponible { get; set; }

        /// <summary>
        /// Cuota Repercutida. Decimal(12,2). Opcional a partir de la versión 0.7.
        /// </summary>
        public string CuotaRepercutida { get; set; }

        /// <summary>
        /// Cuota Soportada. Decimal(12,2).
        /// </summary>
        public string CuotaSoportada { get; set; }

        /// <summary>
        /// Tipo Recargo Equivalencia. Decimal(3,2).
        /// </summary>
        public string TipoRecargoEquivalencia { get; set; }

        /// <summary>
        /// Cuota Recargo Equivalencia. Decimal(12,2).
        /// </summary>
        public string CuotaRecargoEquivalencia { get; set; }

        /// <summary>
        /// Porcentaje compensación REAGYP. Decimal(3,2).
        /// </summary>
        public string PorcentCompensacionREAGYP { get; set; }

        /// <summary>
        /// Compensación REAGYP. Decimal(12,2).
        /// </summary>
        public string ImporteCompensacionREAGYP { get; set; }

        /// <summary>
        /// Identificador que especifica bien de inversión. 
        /// Si no se informa este campo se entenderá que tiene valor “N”.
        /// Valores permitidos 'S' o 'N':
        /// <para>'S': Si</para>
        /// <para>'N': No</para> 
        /// </summary>
        public string BienInversion { get; set; }

    }
}
