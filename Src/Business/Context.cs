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

namespace EasySII.Business
{
    /// <summary>
    /// Datos y métodos generales de la clase business.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Prefijos de nombre de archivo de cada uno de los tipos de envíos.
        /// </summary>
        public static Dictionary<string, string> BatchFilePrefixes = new Dictionary<string, string>()
        {
            {"LRFE", "Lote facturas emitidas" },                            //   1
            {"LRFR", "Lote facturas recibidas" },                           //   2
            {"LROI", "Lote operaciones intracomunitarias" },                //   3
            {"LCFE", "Lote pagos facturas emitidas" },                      //   4
            {"LPFR", "Lote pagos facturas recibidas" },                     //   5
            {"DRFE", "Lote borrado facturas emitidas" },                    //   6
            {"DROI", "Lote borrado operaciones intracomunitarias" },        //   7
            {"DRFR", "Lote borrado facturas recibidas" },                   //   8
            {"QRFE", "Consulta facturas emitidas" },                        //   9
            {"QRFR", "Consulta facturas recibidas" },                       //  10
            {"QROI", "Consulta operaciones intracomunitarias" },            //  11
            {"LRBI", "Lote bienes inversion" },                             //  12
            {"DRBI", "Lote borrado bienes inversion" },                     //  13
            {"QRBI", "Consulta bienes inversion" },                         //  14
            {"LRCM", "Lote cobros en metalico" },                           //  15
            {"DRCM", "Lote borrado cobros en metalico" },                   //  16
            {"QRCM", "Consulta cobros en metalico" },                       //  17
        };
    }
}
