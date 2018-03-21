using EasySII.Business.Batches;
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
        /// <param name="invoicesBatch"> Lote de facturas recibidas.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de facturas emitidas recibidas.</returns>
        public static string SendSiiLote(Batch invoicesBatch)
        {

            if (invoicesBatch.BatchItems.Count == 0)
                throw new ArgumentException("Couldnt't send APInvoicesBatch without invoices.");

            string response = Call(invoicesBatch.GetSiiEndpoint(),
                invoicesBatch.GetActionUrl(),
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }
    }
}
