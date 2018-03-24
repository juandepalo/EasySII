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

using EasySII.Xml;
using EasySII.Xml.Soap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EasySII.Business.Batches
{

    /// <summary>
    /// Representa un lote de un tipo determinado
    /// a enviar al SII.
    /// </summary>
    public class Batch
    {

        static string _Ns = "EasySII.Xml.Silr";

        /// <summary>
        /// Clave de la acción que realiza el lote.
        /// </summary>
        public BatchActionKeys BatchActionKey { get; private set; }

        /// <summary>
        /// Prefijo que se le da en el SII a este tipo
        /// de lote.
        /// </summary>
        public BatchActionPrefixes BatchActionPrefix { get; private set; }

        /// <summary>
        /// Tipo que se le da en el SII a este tipo
        /// de lote.
        /// </summary>
        public BatchTypes BatchType { get; private set; }

        /// <summary>
        /// Tipo de comunicación.
        /// </summary>
        public CommunicationType CommunicationType { get; set; }

        /// <summary>
        /// Titular del lote de facturas recibidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de elementos incluidos en el lote.
        /// </summary>
        public List<IBatchItem> BatchItems { get; private set; }

        /// <summary>
        /// Construye una nueva instancia de un lote.
        /// </summary>
        /// <param name="batchActionKey">Clave de la acción que realiza el lote.</param>
        /// <param name="batchActionPrefix">Prefijo que se le da en el SII a 
        /// este tipo de lote.</param>
        /// <param name="batchType"></param>
        public Batch(BatchActionKeys batchActionKey,
            BatchActionPrefixes batchActionPrefix, BatchTypes batchType)
        {
            BatchActionKey = batchActionKey;
            BatchActionPrefix = batchActionPrefix;
            BatchType = batchType;

            BatchItems = new List<IBatchItem>();

        }

        /// <summary>
        /// Devuelve el nombre que recibe el lote en
        /// las especificaciones del SII.
        /// </summary>
        /// <returns>Nombre del tipo que representa
        /// el lote en las especificaciones del SII.</returns>
        private string GetSiiTypeName()
        {
            return $"{BatchActionPrefix}{BatchType}";
        }

        /// <summary>
        /// Devuelve el nombre que recibe el lote en
        /// las especificaciones del SII.
        /// </summary>
        /// <returns>Nombre del tipo que representa
        /// el lote en las especificaciones del SII.</returns>
        private string GetSiiTypeFullName()
        {
            return $"{_Ns}.{GetSiiTypeName()}";
        }

        /// <summary>
        /// Devuelve el nombre que recibe el lote en
        /// las especificaciones del SII.
        /// </summary>
        /// <returns>Nombre del tipo que representa
        /// el lote en las especificaciones del SII.</returns>
        private string GetSiiItemsTypeName()
        {
            string siiType = $"{BatchActionPrefix}";
            string batchType = $"{BatchType}";

            if (siiType.StartsWith("Suministro"))
            {
                siiType = siiType.Replace("Suministro", "Registro");

                if(batchType.StartsWith("Pagos"))
                    batchType = "Pagos";
                else if (!batchType.StartsWith("CobrosMetalico") && 
                    batchType.StartsWith("Cobros"))
                    batchType = "Cobros";

                return $"{siiType}{batchType}";
            }
            else if (siiType.StartsWith("Baja"))
            {
                siiType = siiType.Replace("Baja", "Registro");

                // En emitidas no sigue un orden :(

                if (batchType.EndsWith("Emitidas"))
                    batchType = batchType.Replace("Emitidas", "Expedidas");
                else if (batchType == "CobrosMetalico")
                    batchType = $"Baja{batchType}";

                siiType = $"{siiType}{batchType}";
                return siiType.Replace("Facturas", "Baja");
            }

            return null;
        }

        /// <summary>
        /// Tipo que representa el lote de negocio
        /// en el SII.
        /// </summary>
        /// <returns></returns>
        private Type GetSiiType()
        {
            return Type.GetType(GetSiiTypeFullName());
        }

        /// <summary>
        /// Devuelve una nueva instancia de lote del SII
        /// para este lote de negocio.
        /// </summary>
        /// <returns></returns>
        private ISiiLote GetSiiInstance()
        {
            return Activator.CreateInstance(GetSiiType()) as ISiiLote;
        }

        /// <summary>
        /// Items de un lote del SII.
        /// </summary>
        /// <param name="siiInstance"></param>
        /// <returns></returns>
        private IList GetSiiInstanceItems(ISiiLote siiInstance)
        {
            return siiInstance.GetType().GetProperty(GetSiiItemsTypeName()).GetValue(siiInstance) as IList;
        }
     
        /// <summary>
        /// Devuelve el sobre soap del lote de facturas emitidas.
        /// </summary>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Sobre SOAP con lote de facturas recibidas.</returns>
        public Envelope GetEnvelope(bool skipErrors = false)
        {
            Envelope envelope = new Envelope();

            ISiiLote siiInstance = GetSiiInstance();

            if(BatchActionPrefix == BatchActionPrefixes.SuministroLR && 
                BatchType != BatchTypes.PagosRecibidas &&
                BatchType != BatchTypes.CobrosEmitidas)
                siiInstance.Cabecera.TipoComunicacion = $"{CommunicationType}";

            siiInstance.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            siiInstance.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.GetType().GetProperty(GetSiiTypeName()).SetValue(envelope.Body, siiInstance); 

            IList siiInstanceItems = GetSiiInstanceItems(siiInstance);

            foreach (var item in BatchItems)
                siiInstanceItems.Add(item.ToSIIBatchItem(BatchActionKey, false, skipErrors));

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de facturas como un archivo xml para soap según las
        /// especificaciones de la aeat.
        /// </summary>
        /// <param name="xmlPath">Ruta donde se guardará el archivo generado.</param>
        /// <returns>Xaml generado.</returns>
        public XmlDocument GetXml(string xmlPath)
        {
            return SIIParser.GetXml(GetEnvelope(), xmlPath);
        }

        /// <summary>
        /// Devuelve el tag para actión del web service.
        /// </summary>
        /// <returns>Tag para actión del web service.</returns>
        private string GetSiiActionTag()
        {
            return $"?op={GetSiiTypeName()}";
        }

        /// <summary>
        /// Devuelve la url para el envío del lote.
        /// </summary>
        /// <returns></returns>
        public string GetSiiEndpoint()
        {
            return EndPoints.BusinessTypesOfSii[BatchType];
        }

        /// <summary>
        /// Devuelve la url para el envío del lote.
        /// </summary>
        /// <returns></returns>
        public string GetActionUrl()
        {
            return $"{GetSiiEndpoint()}{GetSiiActionTag()}";
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de facturas emitidas.</returns>
        public string GetSentFileName()
        {

            return GetFileName($"{BatchActionKey}.{BatchType}" +
                ".SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName($"{BatchActionKey}.{BatchType}" +
                ".RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = $"{BatchActionKey}.{BatchType}" + 
                ".SENT.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>        
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = $"{BatchActionKey}.{BatchType}" + 
                ".RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        private string GetFileName(string template)
        {

            return GetName(template, BatchItems[0].GetItemKey(),
                BatchItems[BatchItems.Count - 1].GetItemKey(),
                Titular.TaxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="template"> Plantilla para el nombre.</param>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>        
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        private static string GetName(string template, string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {
            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                numFirstInvoiceNumber)).Replace("-", "");

            numLast = BitConverter.ToString(Encoding.UTF8.GetBytes(
                numLastInvoiceNumber)).Replace("-", "");

            return string.Format(template, taxIdentificationNumber, numFirst, numLast);
        }


    }

}
