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

using EasySII.Business.Batches;
using EasySII.Xml.SiiR;
using EasySII.Xml.Silr;
using EasySII.Xml.Soap;
using System;
using System.IO;

namespace EasySII.Net
{

    /// <summary>
    /// Gestiona envío al sii.
    /// </summary>
    public class BatchDispatcher : Wsd
    {

        /// <summary>
        /// Envía un lote de facturas recibidas al SII.
        /// </summary>
        /// <param name="batch"> Lote de elmentos a enviar al SII.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT.</returns>
        public static string SendSiiLote(Batch batch)
        {

            if (batch.BatchItems.Count == 0)
                throw new ArgumentException("Couldnt't send Batch without BatchItems.");

            string response = Call(batch.GetSiiEndpoint(),
                batch.GetActionUrl(),
                batch.GetXml(Settings.Current.OutboxPath + batch.GetSentFileName()));

            string responsePath = Settings.Current.InboxPath + batch.GetReceivedFileName();
            File.WriteAllText(responsePath, response);

            // Aquí tengo que volcar los resultados de la respuesta en el lote

            //RespuestaLRF
            Envelope envelopeRespuesta = new Envelope(responsePath);

            var respuesta = envelopeRespuesta.Body.GetRespuestaLRF();

            if (respuesta == null && envelopeRespuesta.Body.RespuestaError != null)
                throw new Exception(envelopeRespuesta.Body.RespuestaError.FaultDescription);

            foreach (var lin in respuesta.RespuestaLinea)
            {
                if (lin.IDFactura != null)
                {
                    IBatchItem it = GetBatchItem(batch, lin.IDFactura);

                    it.Status = lin.EstadoRegistro;
                    it.CSV = respuesta.CSV;

                    if (it.Status == "Incorrecto" || it.Status == "AceptadoConErrores")
                    {
                        it.ErrorCode = lin.CodigoErrorRegistro;
                        it.ErrorMessage = lin.DescripcionErrorRegistro;
                    }
                   
                }

            }

            return response;

        }

        /// <summary>
        /// Devuelve el item del lote que se corresponde
        /// con los datos de identificación facilitados.
        /// </summary>
        /// <param name="batch">Lote en el que buscar.</param>
        /// <param name="idFactura">Datos de identificación.</param>
        /// <returns>Item coicidente o null.</returns>
        private static IBatchItem GetBatchItem(Batch batch, IDFactura idFactura)
        {
            foreach (var it in batch.BatchItems)
                if ((it as IBatchItem)?.GetPartyKey() == idFactura.GetIDEmisorFactura() &&
                    (it as IBatchItem)?.GetItemKey() == idFactura.GetNumSerieFacturaEmisor() &&
                    (it as IBatchItem)?.GetItemDate() == idFactura.FechaExpedicionFacturaEmisor)
                    return (it as IBatchItem);

            return null;
        }
    }
}
