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

using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos de la Operación Intracomunitaria.
    /// </summary>
    [Serializable]
    public class OperacionIntracomunitaria
    {
        /// <summary>
        /// Tipo Operación según lista L12:
        /// <para>A: El envío o recepción de bienes para la realización de los informes parciales o trabajos mencionados
        ///          en el artículo 70, apartado uno, número 7º, de la Ley del Impuesto (Ley 37/1992)</para>
        /// <para>B: Las transferencias de bienes y las adquisiciones intracomunitarias de bienes comprendidas en los artículos
        ///          9, apartado 3º, y 16, apartado 2º, de la Ley del Impuesto (Ley 37/1992)</para>
        /// </summary>
        [XmlElement("TipoOperacion", Namespace = Settings.NamespaceSii)]
        public string TipoOperacion { get; set; }

        /// <summary>
        /// Clave de declarado intracomunitario según lista L13:
        /// <para>D: Declarante</para>
        /// <para>R: Remitente</para>
        /// </summary>
        [XmlElement("ClaveDeclarado", Namespace = Settings.NamespaceSii)]
        public string ClaveDeclarado { get; set; }

        /// <summary>
        /// Código del Estado miembro de origen o de envío según lista L18:
        /// <para>DE: Alemania</para>
        /// <para>AT: Austria</para>
        /// </summary>
        [XmlElement("EstadoMiembro", Namespace = Settings.NamespaceSii)]
        public string EstadoMiembro { get; set; }

        /// <summary>
        /// Plazo de Operación
        /// </summary>
        [XmlElement("PlazoOperacion", Namespace = Settings.NamespaceSii)]
        public string PlazoOperacion { get; set; }

        /// <summary>
        /// Descripción de los biene adquiridos.
        /// </summary>
        [XmlElement("DescripcionBienes", Namespace = Settings.NamespaceSii)]
        public string DescripcionBienes { get; set; }

        /// <summary>
        /// Dirección del operador intracomunitario.
        /// </summary>
        [XmlElement("DireccionOperador", Namespace = Settings.NamespaceSii)]
        public string DireccionOperador { get; set; }

        /// <summary>
        /// Otras facturas o documentación relativas a las operaciones de que se trate
        /// </summary>
        [XmlElement("FacturasODocumentacion", Namespace = Settings.NamespaceSii)]
        public string FacturasODocumentacion { get; set; }

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

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public OperacionIntracomunitaria()
        {
        }
    }
}
