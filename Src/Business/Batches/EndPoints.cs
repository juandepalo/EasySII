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

using System.Collections.Generic;

namespace EasySII.Business.Batches
{

    /// <summary>
    /// EndPoints por tipo de documentos.
    /// </summary>
    public class EndPoints
    {

        /// <summary>
        /// Prefijo de todos los endpoints del SII.
        /// </summary>
        public static string EndPointPrefix = Settings.Current.SiiEndPointPrefix;

        /// <summary>
        /// Mapeo entre tipos de lote del SII y objetos de negocio.
        /// </summary>
        public static Dictionary<BatchTypes, string> BusinessTypesOfSii = new Dictionary<BatchTypes, string>() {
            {BatchTypes.FacturasRecibidas,                                      $"{EndPointPrefix}/fr/SiiFactFRV1SOAP" },
            {BatchTypes.PagosRecibidas,                                         $"{EndPointPrefix}/fr/SiiFactPAGV1SOAP" },
            {BatchTypes.FacturasEmitidas,                                       $"{EndPointPrefix}/fe/SiiFactFEV1SOAP" },
            {BatchTypes.CobrosEmitidas,                                         $"{EndPointPrefix}/fe/SiiFactCOBV1SOAP" },
            {BatchTypes.BienesInversion,                                        $"{EndPointPrefix}/bi/SiiFactBIV1SOAP" },
            {BatchTypes.CobrosMetalico,                                         $"{EndPointPrefix}/pm/SiiFactCMV1SOAP" },
            {BatchTypes.OperacionesSeguros,                                     $"{EndPointPrefix}/pm/SiiFactCMV1SOAP" },
            {BatchTypes.DetOperacionIntracomunitaria,                           $"{EndPointPrefix}/oi/SiiFactOIV1SOAP" },
            {BatchTypes.AgenciasViajes,                                         $"{EndPointPrefix}/pm/SiiFactCMV1SOAP" },
        };
    }
}
