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

using EasySII.Xml.SiiR;
using EasySII.Xml.Silr;
using EasySII.Xml.VNifV2Ent;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Soap
{
    /// <summary>
    /// SOAP body.
    /// </summary>
    [Serializable]
    [XmlRoot("Body")]
    public class Body
    {

        /*--------------------------- Validación de NIF --------------------------------------*/

        /// <summary>
        /// Información de contribuyente a validar.
        /// </summary>
        [XmlElement("VNifV2Ent", Namespace = Settings.NamespaceVNifV2Ent)]
        public VNifVEnt VNifV2Ent { get; set; }

        /// <summary>
        /// NIF del contribuyente.
        /// Numérico(4).
        /// </summary>
        [XmlArray("VNifV2Sal", Namespace = Settings.NamespaceVNifV2Sal)]
        [XmlArrayItem("Contribuyente", Namespace = Settings.NamespaceVNifV2Sal)]
        public List<ContribuyenteSal> Contribuyentes { get; set; }


        /*--------------------------- Tratamiento de los suministros --------------------------------------*/
        /// <summary>
        /// Suministro de información al SII de bienes inversión.
        /// </summary>
        [XmlElement("SuministroLRBienesInversion", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRBienesInversion SuministroLRBienesInversion { get; set; }

        /// <summary>
        /// Suministro de información al SII de Cobros de Facturas Emitidas
        /// </summary>
        [XmlElement("SuministroLRCobrosEmitidas", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRCobrosEmitidas SuministroLRCobrosEmitidas { get; set; }

        /// <summary>
        /// Suministro de información al SII de facturas emitidas.
        /// </summary>
        [XmlElement("SuministroLRFacturasEmitidas", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRFacturasEmitidas SuministroLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Suministro de información al SII de facturas recibidas.
        /// </summary>
        [XmlElement("SuministroLRFacturasRecibidas", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRFacturasRecibidas SuministroLRFacturasRecibidas { get; set; }

        /// <summary>
        /// Suministro de información al SII de cobros en metálico.
        /// </summary>
        [XmlElement("SuministroLRCobrosMetalico", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRCobrosMetalico SuministroLRCobrosMetalico { get; set; }

        /// <summary>
        /// Suministro de información al SII de Operaciones de Seguros.
        /// </summary>
        [XmlElement("SuministroLROperacionesSeguros", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLROperacionesSeguros SuministroLROperacionesSeguros { get; set; }

        /// <summary>
        /// Suministro de información al SII de Agencias de Viajes.
        /// </summary>
        [XmlElement("SuministroLRAgenciasViajes", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRAgenciasViajes SuministroLRAgenciasViajes { get; set; }

        /// <summary>
        /// Suministro de información al SII de Operaciones Intracomunitarias.
        /// </summary>
        [XmlElement("SuministroLRDetOperacionIntracomunitaria", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRDetOperacionIntracomunitaria SuministroLRDetOperacionIntracomunitaria { get; set; }

        /// <summary>
        /// Suministro de información al SII de pago facturas recibidas.
        /// </summary>
        [XmlElement("SuministroLRPagosRecibidas", Namespace = Settings.NamespaceSiiLR)]
        public SuministroLRPagosRecibidas SuministroLRPagosRecibidas { get; set; }

        /*--------------------------- Tratamiento de las consultas --------------------------------------*/
        /// <summary>
        /// Consulta de información al SII de Bienes de Inversión.
        /// </summary>
        [XmlElement("ConsultaLRBienesInversion", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaLRBienesInversion ConsultaLRBienesInversion { get; set; }

        /// <summary>
        /// Consulta de envíos al SII de cobros de facturas emitidas.
        /// </summary>
        [XmlElement("ConsultaCobros", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaCobros ConsultaCobros { get; set; }

        /// <summary>
        /// Consulta de información al SII de facturas emitidas.
        /// </summary>
        [XmlElement("ConsultaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaLRFacturasEmitidas ConsultaLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Consulta de información al SII de facturas recibidas.
        /// </summary>
        [XmlElement("ConsultaLRFacturasRecibidas", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaLRFacturasRecibidas ConsultaLRFacturasRecibidas { get; set; }

        /// <summary>
        /// Consulta de envíos al SII de pagos de facturas recibidas.
        /// </summary>
        [XmlElement("ConsultaPagos", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaPagos ConsultaPagos { get; set; }

        /// <summary>
        /// Consulta de envíos al SII de Operaciones Intracomunitarias.
        /// </summary>
        [XmlElement("ConsultaLRDetOperIntracomunitarias", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaLRDetOperIntracomunitarias ConsultaLRDetOperIntracomunitarias { get; set; }

        /// <summary>
        /// Consulta de envíos al SII de Operaciones de Seguros.
        /// </summary>
        [XmlElement("ConsultaLROperacionesSeguros", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaLROperacionesSeguros ConsultaLROperacionesSeguros { get; set; }

        /// <summary>
        /// Consulta de envíos al SII de Cobros en Metalico.
        /// </summary>
        [XmlElement("ConsultaLRCobrosMetalico", Namespace = Settings.NamespaceSiiLRQ)]
        public ConsultaLRCobrosMetalico ConsultaLRCobrosMetalico { get; set; }

        /*--------------------------- Tratamiento de las bajas --------------------------------------*/
        /// <summary>
        /// Baja de Bienes de Inversión enviados al SII.
        /// </summary>
        [XmlElement("BajaLRBienesInversion", Namespace = Settings.NamespaceSiiLR)]
        public BajaLRBienesInversion BajaLRBienesInversion { get; set; }

        /// <summary>
        /// Baja de facturas emitidas enviadas al SII.
        /// </summary>
        [XmlElement("BajaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiLR)]
        public BajaLRFacturasEmitidas BajaLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Baja de facturas recibidas enviadas al SII.
        /// </summary>
        [XmlElement("BajaLRFacturasRecibidas", Namespace = Settings.NamespaceSiiLR)]
        public BajaLRFacturasRecibidas BajaLRFacturasRecibidas { get; set; }

        /// <summary>
        /// Baja de Operaciones Intracomunitarias enviadas al SII.
        /// </summary>
        [XmlElement("BajaLRDetOperacionIntracomunitaria", Namespace = Settings.NamespaceSiiLR)]
        public BajaLRDetOperacionIntracomunitaria BajaLRDetOperacionIntracomunitaria { get; set; }

        /// <summary>
        /// Baja de Operaciones de Seguros enviadas al SII.
        /// </summary>
        [XmlElement("BajaLROperacionesSeguros", Namespace = Settings.NamespaceSiiLR)]
        public BajaLROperacionesSeguros BajaLROperacionesSeguros { get; set; }

        /// <summary>
        /// Baja de Agencias Viajes enviadas al SII.
        /// </summary>
        [XmlElement("BajaLRAgenciasViajes", Namespace = Settings.NamespaceSiiLR)]
        public BajaLRAgenciasViajes BajaLRAgenciasViajes { get; set; }

        /// <summary>
        /// Baja de Cobros en Metálico enviados al SII.
        /// </summary>
        [XmlElement("BajaLRCobrosMetalico", Namespace = Settings.NamespaceSiiLR)]
        public BajaLRCobrosMetalico BajaLRCobrosMetalico { get; set; }
        
        /*---------------------------------- Respuestas de altas/bajas --------------------------------------*/
        /// <summary>
        /// Respuesta AEAT LRFacturasEmitidas.
        /// </summary>
        [XmlElement("RespuestaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBajaFacturasEmitidas.
        /// </summary>
        [XmlElement("RespuestaLRBajaFacturasEmitidas", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBajaFacturasEmitidas { get; set; }

        /// <summary>
        /// Respuesta AEAT LRFacturasRecibidas.
        /// </summary>
        [XmlElement("RespuestaLRFacturasRecibidas", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRFacturasRecibidas { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBajaFacturasRecibidas.
        /// </summary>
        [XmlElement("RespuestaLRBajaFacturasRecibidas", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBajaFacturasRecibidas { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBienesInversion.
        /// </summary>
        [XmlElement("RespuestaLRBienesInversion", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBienesInversion { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBajaBienesInversion.
        /// </summary>
        [XmlElement("RespuestaLRBajaBienesInversion", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBajaBienesInversion { get; set; }

        /// <summary>
        /// Respuesta AEAT LROperacionIntracomunitaria.
        /// </summary>
        [XmlElement("RespuestaLRDetOperacionesIntracomunitarias", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRDetOperacionesIntracomunitarias { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBajaDetOperacionesIntracomunitarias.
        /// </summary>
        [XmlElement("RespuestaLRBajaDetOperacionesIntracomunitarias", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBajaDetOperacionesIntracomunitarias { get; set; }

        /// <summary>
        /// Respuesta AEAT LRCobrosMetalico.
        /// </summary>
        [XmlElement("RespuestaLRCobrosMetalico", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRCobrosMetalico { get; set; }

        /// <summary>
        /// Respuesta AEAT LROperacionesSeguros.
        /// </summary>
        [XmlElement("RespuestaLROperacionesSeguros", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLROperacionesSeguros { get; set; }

        /// <summary>
        /// Respuesta AEAT LRAgenciasViajes.
        /// </summary>
        [XmlElement("RespuestaLRAgenciasViajes", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRAgenciasViajes { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBajaCobrosMetalico.
        /// </summary>
        [XmlElement("RespuestaLRBajaCobrosMetalico", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBajaCobrosMetalico { get; set; }

        /// <summary>
        /// Respuesta AEAT LRBajaOperacionesSeguros.
        /// </summary>
        [XmlElement("RespuestaLRBajaOperacionesSeguros", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRBajaOperacionesSeguros { get; set; }

        /// <summary>
        /// Respuesta AEAT LRCobrosEmitidas.
        /// </summary>
        [XmlElement("RespuestaLRCobrosEmitidas", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRCobrosEmitidas { get; set; }

        /// <summary>
        /// Respuesta AEAT LRPagosRecibidas.
        /// </summary>
        [XmlElement("RespuestaLRPagosRecibidas", Namespace = Settings.NamespaceSiiR)]
        public RespuestaLRF RespuestaLRPagosRecibidas { get; set; }

        /*---------------------------------- Respuestas de consultas --------------------------------------*/

        /// <summary>
        /// Respuesta AEAT ConsultaLRFacturasRecibidas.
        /// </summary>
        [XmlElement("RespuestaConsultaLRFacturasRecibidas", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaLRFacturasRecibidas RespuestaConsultaLRFacturasRecibidas { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRFacturasRecibidas.
        /// </summary>
        [XmlElement("RespuestaConsultaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaLRFacturasEmitidas RespuestaConsultaLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRBienesInversion.
        /// </summary>
        [XmlElement("RespuestaConsultaLRBienesInversion", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaLRBienesInversion RespuestaConsultaLRBienesInversion { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRDetOperIntracomunitarias.
        /// </summary>
        [XmlElement("RespuestaConsultaLRDetOperIntracomunitarias", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaLRDetOperIntracomunitarias RespuestaConsultaLRDetOperIntracomunitarias { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRDetOperIntracomunitarias.
        /// </summary>
        [XmlElement("RespuestaConsultaLRCobrosMetalico", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaLRCobrosMetalico RespuestaConsultaLRCobrosMetalico { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRDetOperIntracomunitarias.
        /// </summary>
        [XmlElement("RespuestaConsultaLROperacionesSeguros", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaLROperacionesSeguros RespuestaConsultaLROperacionesSeguros { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRDetOperIntracomunitarias.
        /// </summary>
        [XmlElement("RespuestaConsultaCobros", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaCobros RespuestaConsultaCobros { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRDetOperIntracomunitarias.
        /// </summary>
        [XmlElement("RespuestaConsultaPagos", Namespace = Settings.NamespaceSiiRQ)]
        public RespuestaConsultaPagos RespuestaConsultaPagos { get; set; }

        /// <summary>
        /// Respuesta AEAT ConsultaLRDetOperIntracomunitarias.
        /// </summary>
        [XmlElement("Fault", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapFault RespuestaError { get; set; }

        /// <summary>
        /// Body del envelope.
        /// </summary>
        public Body()
        {
        }

        /// <summary>
        /// Devuelve el número de hijos.
        /// </summary>
        public int GetChildNumber()
        {
            return
                ((VNifV2Ent == null) ? 0 : 1) +
                ((Contribuyentes == null) ? 0 : 1) +

                ((SuministroLRBienesInversion == null) ? 0 : 1) +
                ((SuministroLRCobrosEmitidas == null) ? 0 : 1) +
                ((SuministroLRFacturasEmitidas == null) ? 0 : 1) +
                ((SuministroLRFacturasRecibidas == null) ? 0 : 1) +
                ((SuministroLRCobrosMetalico == null) ? 0 : 1) +
                ((SuministroLROperacionesSeguros == null) ? 0 : 1) +
                ((SuministroLRAgenciasViajes == null) ? 0 : 1) +
                ((SuministroLRDetOperacionIntracomunitaria == null) ? 0 : 1) +
                ((SuministroLRPagosRecibidas == null) ? 0 : 1) +

                ((ConsultaLRBienesInversion == null) ? 0 : 1) +
                ((ConsultaCobros == null) ? 0 : 1) +
                ((ConsultaLRFacturasEmitidas == null) ? 0 : 1) +
                ((ConsultaLRFacturasRecibidas == null) ? 0 : 1) +
                ((ConsultaLRCobrosMetalico == null) ? 0 : 1) +
                ((ConsultaLROperacionesSeguros == null) ? 0 : 1) +
                ((ConsultaLRDetOperIntracomunitarias == null) ? 0 : 1) +
                ((ConsultaPagos == null) ? 0 : 1) +

                ((BajaLRBienesInversion == null) ? 0 : 1) +
                ((BajaLRFacturasEmitidas == null) ? 0 : 1) +
                ((BajaLRFacturasRecibidas == null) ? 0 : 1) +
                ((BajaLRCobrosMetalico == null) ? 0 : 1) +
                ((BajaLROperacionesSeguros == null) ? 0 : 1) +
                ((BajaLRAgenciasViajes == null) ? 0 : 1) +
                ((BajaLRDetOperacionIntracomunitaria == null) ? 0 : 1);
        }

        /// <summary>
        /// Si el body es de una respuesta a un envío de alta/baja
        /// de libro registro, devuelve el cuerpo de la respuesta. 
        /// En caso contrario devuelve null.
        /// </summary>
        /// <returns>Respuesta en su caso o null.</returns>
        public RespuestaLRF GetRespuestaLRF()
        {

            var respuesta = RespuestaLRFacturasEmitidas;

            if (respuesta == null)
                respuesta = RespuestaLRBajaFacturasEmitidas;

            if (respuesta == null)
                respuesta = RespuestaLRFacturasRecibidas;

            if (respuesta == null)
                respuesta = RespuestaLRBajaFacturasRecibidas;

            if (respuesta == null)
                respuesta = RespuestaLRBienesInversion;

            if (respuesta == null)
                respuesta = RespuestaLRBajaBienesInversion;

            if (respuesta == null)
                respuesta = RespuestaLRDetOperacionesIntracomunitarias;

            if (respuesta == null)
                respuesta = RespuestaLRBajaDetOperacionesIntracomunitarias;

            if (respuesta == null)
                respuesta = RespuestaLRCobrosMetalico;

            if (respuesta == null)
                respuesta = RespuestaLRCobrosMetalico;

            if (respuesta == null)
                respuesta = RespuestaLRCobrosMetalico;

            if (respuesta == null)
                respuesta = RespuestaLROperacionesSeguros;

            if (respuesta == null)
                respuesta = RespuestaLRAgenciasViajes;

            if (respuesta == null)
                respuesta = RespuestaLRBajaCobrosMetalico;

            if (respuesta == null)
                respuesta = RespuestaLRBajaOperacionesSeguros;

            if (respuesta == null)
                respuesta = RespuestaLRCobrosEmitidas;

            if (respuesta == null)
                respuesta = RespuestaLRPagosRecibidas;

            return respuesta;

        }
    }
}
