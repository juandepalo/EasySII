﻿/*
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

namespace EasySII.Business
{
    /// <summary>
    /// Tipo factura
    /// </summary>
    public enum InvoiceType
    {
        /// <summary>
        /// Factura
        /// </summary>
        F1,
        /// <summary>
        /// Factura Simplificada (ticket)
        /// </summary>
        F2,
        /// <summary>
        /// Factura Rectificativa (Art 80.1 y 80.2 y error fundado en derecho)
        /// </summary>
        R1,
		/// <summary>
		/// Factura Rectificativa (Art. 80.3): Cuando el destinatario de las operaciones sujetas al 
		/// Impuesto no haya hecho efectivo el pago de las cuotas repercutidas y siempre que, con 
		/// posterioridad al devengo de la operación, se dicte auto de declaración de concurso.
		/// </summary>
		R2,
		/// <summary>
		/// Factura Rectificativa (Art. 80.4): Cuando los créditos correspondientes a 
		/// las cuotas repercutidas por las operaciones gravadas sean total o parcialmente incobrables.
		/// </summary>
		R3,
        /// <summary>
        /// Factura Rectificativa (Resto)
        /// </summary>
        R4,
        /// <summary>
        /// Factura Rectificativa en facturas simplificadas
        /// </summary>
        R5,
        /// <summary>
        /// Factura emitida en sustitución de facturas simplificadas facturadas y declaradas
        /// </summary>
        F3,
        /// <summary>
        /// Asiento resumen de facturas
        /// </summary>
        F4,
        /// <summary>
        /// Sólo para recibidas. Importación DUA.
        /// </summary>
        F5,
        /// <summary>
        /// Sólo para recibidas. Otros justificantes contables.
        /// </summary>
        F6,
        /// <summary>
        /// Aduanas - Liquidación complementaria
        /// </summary>
        LC
    };
}
