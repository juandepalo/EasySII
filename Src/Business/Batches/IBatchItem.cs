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

namespace EasySII.Business.Batches
{

    /// <summary>
    /// Representa un elemento de un lote.
    /// </summary>
    public interface IBatchItem
    {

        /// <summary>
        /// Código CSV asginado por la AEAT.
        /// </summary>
        string CSV { get; set; }

        /// <summary>
        /// Estado de la factura en el SII de la AEAT.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Código de error de la factura en el SII de la AEAT.
        /// </summary>
        string ErrorCode { get; set; }

        /// <summary>
        /// Mensaje de error de de la factura en el SII de la AEAT.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Obtiene un objeto RegistroLRFacturasRecibidas, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
        /// <param name="batchActionKey">Tipo de lote.</param>
        /// <param name="updateInnerSII">Si es true, actualiza el objeto SII subyacente
        /// con el valor calculado.</param>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLRFacturasEmitidas.</returns>
        object ToSIIBatchItem(BatchActionKeys batchActionKey, 
            bool updateInnerSII = false, bool skipErrors = false);


        /// <summary>
        /// Devuelve un identificador para la instancia de item: InvoiceNumber...
        /// </summary>
        string GetItemKey();

        /// <summary>
        /// Devuelve fecha del item: IssueDate...
        /// con formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        string GetItemDate();

        /// <summary>
        /// Devuelve el identificador del interlocutor de negocio
        /// que actúa como emisor.
        /// </summary>
        /// <returns>Id. del emisor.</returns>
        string GetPartyKey();

    }
}
