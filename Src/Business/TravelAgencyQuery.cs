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

using EasySII.Xml;
using EasySII.Xml.Silr;
using EasySII.Xml.Soap;
using System;
using System.Text;
using System.Xml;

namespace EasySII.Business
{
    /// <summary>
    /// Consulta de agencias de viajes.
    /// </summary>
    public class TravelAgencyQuery
    {
 

        /// <summary>
        /// Titular del lote de agencias de viajes.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Operación agencia viajes para recoger información de filtro.
        /// </summary>
        public OPTributaria TravelAgencyOperation { get; private set; }

        /// <summary>
        /// Constructor clase InsurancesQuery.
        /// </summary>
        public TravelAgencyQuery()
        {
            TravelAgencyOperation = new OPTributaria();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de agencias de viajes.
        /// </summary>
        /// <returns> El sobre soap de consulta de agencias de viajes.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaLROperacionesSeguros = new ConsultaLROperacionesSeguros();

            envelope.Body.ConsultaLROperacionesSeguros.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaLROperacionesSeguros.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaLROperacionesSeguros.FiltroConsulta = TravelAgencyOperation.ToFilterSII();        

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de agencias de viajes como un archivo xml para soap según las
        /// especificaciones de la aeat.
        /// </summary>
        /// <param name="xmlPath">Ruta donde se guardará el archivo generado.</param>
        /// <returns>Xaml generado.</returns>
        public XmlDocument GetXml(string xmlPath)
        {
            return SIIParser.GetXml(GetEnvelope(), xmlPath, SIINamespaces.con);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de agencias de viajes.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de agencias de viajes.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QRAV.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de agencias de viajes.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de agencias de viajes.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QRAV.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de agencias de viajes.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (TravelAgencyOperation.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
