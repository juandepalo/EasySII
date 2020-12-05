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

using EasySII.Xml.Sii;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{

    /// <summary>
    /// Venta de bienes en consigna. El objetivo de este Libro registro es reflejar la 
    /// situación de los bienes a que se refieren determinadas operaciones 
    /// intracomunitarias en tanto no tenga lugar el devengo de las entregas 
    /// o de las adquisiciones.
    /// </summary>
    public class OperacionIntracomunitariaVentasEnConsigna
    {

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("InfoExpedicionRecepcion", Namespace = Settings.NamespaceSii)]
        public InfoExpedicionRecepcion InfoExpedicionRecepcion { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("IdRegistroExpInicial", Namespace = Settings.NamespaceSii)]
        public IdRegistroExpInicial IdRegistroExpInicial { get; set; }

        /// <summary>
        /// DestinoFinalExpedRecep.
        /// </summary>
        [XmlElement("DestinoFinalExpedRecep", Namespace = Settings.NamespaceSii)]
        public DestinoFinalExpedRecep DestinoFinalExpedRecep { get; set; }

        /// <summary>
        /// A partir de la versión 1.1.
        /// Referencia Externa. Dato adicional de contenido 
        /// libre enviado por algunas aplicaciones clientes 
        /// (asiento contable, etc). Alfanumérico(60).
        /// Se añade en los libros de emitidas, recibidas, 
        /// bienes de inversión y determinadas operaciones 
        /// intracomunitarias una etiqueta adicional de contenido 
        /// libre denominada RefExterna con el objetivo de que se 
        /// pueda añadir información interna de la empresa asociada 
        /// al registro de la factura.
        /// </summary>
        [XmlElement("RefExterna", Namespace = Settings.NamespaceSii)]
        public string RefExterna { get; set; }

        /// <summary>
        /// Versión 1.1
        /// Número de registro obtenido al enviar la 
        /// autorización en materia de facturación o de 
        /// libros registro. Alfanumérico(15)
        /// </summary>
        [XmlElement("NumRegistroAcuerdoFacturacion", Namespace = Settings.NamespaceSii)]
        public string NumRegistroAcuerdoFacturacion { get; set; }

        /// <summary>
        /// Versión 1.1
        /// NombreRazon + NIF de la entidad sucedida como 
        /// consecuencia de una operación de reestructuración.
        /// </summary>
        [XmlElement("EntidadSucedida", Namespace = Settings.NamespaceSii)]
        public Parte EntidadSucedida { get; set; }

        /// <summary>
        /// Versión 1.1
        /// Identificador que especifica aquellos registros de 
        /// facturación con dificultades para enviarse en plazo 
        /// por no tener constancia del cambio de condición a
        /// GGEE, de la inclusión en REDEME o de un cambio 
        /// en la competencia inspectora. L29 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("RegPrevioGGEEoREDEMEoCompetencia", Namespace = Settings.NamespaceSii)]
        public string RegPrevioGGEEoREDEMEoCompetencia { get; set; }

    }
}
