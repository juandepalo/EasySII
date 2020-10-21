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
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos de la factura.
    /// </summary>
    [Serializable]
    public class FacturaRecibida
    {

        /// <summary>
        /// Especificación del tipo de factura a dar de alta: factura  normal, 
        /// factura rectificativa, tickets, factura emitida en sustitución de facturas.
        /// Tipo factura según lista L2:
        /// <para>F1: Factura (art. 6, 7.2 y 7.3 del RD 1619/2012)</para>
        /// <para>F2: Factura Simplificada (ticket).Factura Simplificada y y Facturas sin identificación del
        /// destinatario art. 6.1.d) RD 1619/2012</para>
        /// <para>R1: Factura Rectificativa (Art 80.1 y 80.2 y error fundado en derecho)</para>
        /// <para>R2: Factura Rectificativa (Art. 80.3)</para>
        /// <para>R3: Factura Rectificativa (Art. 80.4)</para>
        /// <para>R4: Factura Rectificativa (Resto)</para>
        /// <para>R5: Factura Rectificativa en facturas simplificadas</para>
        /// <para>F3: Factura emitida en sustitución de facturas simplificadas facturadas y declaradas</para>
        /// <para>F4: Asiento resumen de facturas</para>
        /// <para>LC: Aduanas - Liquidación complementaria</para> 
        /// </summary>
        [XmlElement("TipoFactura", Namespace = Settings.NamespaceSii)]
        public string TipoFactura {get; set;}

        /// <summary>
        /// Campo que identifica si el tipo de factura rectificativa es por sustitución o por diferencia.
        /// Tipo rectificativa según lista L5:
        /// <para>S: Por sustitución.</para>
        /// <para>I: Por diferencias.</para>
        /// </summary>
        [XmlElement("TipoRectificativa", Namespace = Settings.NamespaceSii)]
        public string TipoRectificativa { get; set; }

        /// <summary>
        /// Facturas Agrupadas.
        /// </summary>
        [XmlArray("FacturasAgrupadas", Namespace = Settings.NamespaceSii)]
        [XmlArrayItem("IDFacturaAgrupada", Namespace = Settings.NamespaceSii)]
        public List<IDFactura> FacturasAgrupadas { get; set; }

        /// <summary>
        /// Facturas Rectificadas.
        /// </summary>
        [XmlArray("FacturasRectificadas", Namespace = Settings.NamespaceSii)]
        [XmlArrayItem("IDFacturaRectificada", Namespace = Settings.NamespaceSii)]
        public List<IDFactura> FacturasRectificadas { get; set; }

        /// <summary>
        /// Importe Rectificacion.
        /// </summary>
        [XmlElement("ImporteRectificacion", Namespace = Settings.NamespaceSii)]
        public ImporteRectificacion ImporteRectificacion { get; set; }

        /// <summary>
        /// Fecha en la que se ha realizado la operación siempre que sea  
        /// diferente a la fecha de expedición.
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaOperacion", Namespace = Settings.NamespaceSii)]
        public string FechaOperacion { get; set; }

        /// <summary>
        /// Clave que identificará el tipo de operación o el régimen 
        /// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendencia", Namespace = Settings.NamespaceSii)]
        public string ClaveRegimenEspecialOTrascendencia { get; set; }

		/// <summary>
		/// Clave que identificará el tipo de operación o el régimen 
		/// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
		/// </summary>
		[XmlElement("ClaveRegimenEspecialOTrascendencia1", Namespace = Settings.NamespaceSii)]
		public string ClaveRegimenEspecialOTrascendencia1 { get; set; }

		/// <summary>
		/// Clave que identificará el tipo de operación o el régimen 
		/// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
		/// </summary>
		[XmlElement("ClaveRegimenEspecialOTrascendencia2", Namespace = Settings.NamespaceSii)]
		public string ClaveRegimenEspecialOTrascendencia2 { get; set; }

		/// <summary>
		/// Número de registro obtenido al enviar el 
		/// acuerdo de facturación correspondiente.
		/// </summary>
		[XmlElement("NumRegistroAcuerdoFacturacion", Namespace = Settings.NamespaceSii)]
		public string NumRegistroAcuerdoFacturacion { get; set; }

		/// <summary>
		/// Importe total de la factura.
		/// </summary>
		[XmlElement("ImporteTotal", Namespace = Settings.NamespaceSii)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Base imponible a coste de la factura.
        /// </summary>
        [XmlElement("BaseImponibleACoste", Namespace = Settings.NamespaceSii)]
        public string BaseImponibleACoste { get; set; }

        /// <summary>
        /// Texto breve de la operación.
        /// </summary>
        [XmlElement("DescripcionOperacion", Namespace = Settings.NamespaceSii)]
        public string DescripcionOperacion { get; set; }

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
        /// Versión 1.1.
        /// Factura simplificada Articulo 7,2 Y 7,3 RD 1619/2012. 
        /// Si no se informa este campo se entenderá que tiene valor “N". 
        /// L27 ('S', 'N').
        /// </summary>
        [XmlElement("FacturaSimplificadaArticulos7.2_7.3", Namespace = Settings.NamespaceSii)]
        public string FacturaSimplificadaArticulos72_73 { get; set; }

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
        /// Versión 1.1
        /// Identificador que especifica aquellas facturas con importe 
        /// de la factura superior a un umbral de 100.000 euros. Si no se informa este
        /// campo se entenderá que tiene valor “N”. L30 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("Macrodato", Namespace = Settings.NamespaceSii)]
        public string Macrodato { get; set; }

        /// <summary>
        /// Datos del DUA.
        /// </summary>
        [XmlElement("Aduanas", Namespace = Settings.NamespaceSii)]
        [Obsolete("Bloque Eliminado en la versión 0.7 del SII.", false)]
        public Aduanas Aduanas { get; set; }

        /// <summary>
        /// Desglose Factura.
        /// </summary>
        [XmlElement("DesgloseFactura", Namespace = Settings.NamespaceSii)]
        public DesgloseFacturaR DesgloseFactura { get; set; }

 
        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSii)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaRegContable", Namespace = Settings.NamespaceSii)]
        public string FechaRegContable { get; set; }

        /// <summary>
        /// Cuota Deducible
        /// </summary>
        [XmlElement("CuotaDeducible", Namespace = Settings.NamespaceSii)]
        public string CuotaDeducible { get; set; }

        /// <summary>
        /// Identificador que especifica si la factura se deduce en un periodo posterior. 
        /// Si no se informa este campo se entenderá que tiene valor “N”.
        /// Valores permitidos 'S' o 'N':
        /// <para>'S': Si</para>
        /// <para>'N': No</para> 
        /// </summary>
        [XmlElement("ADeducirEnPeriodoPosterior", Namespace = Settings.NamespaceSii)]
        public string ADeducirEnPeriodoPosterior { get; set; }

        /// <summary>
        /// Ejercicio de deducción.
        /// </summary>
        [XmlElement("EjercicioDeduccion", Namespace = Settings.NamespaceSii)]
        public string EjercicioDeduccion { get; set; }

        /// <summary>
        /// Periodo de deducción.
        /// Valores permitidos:
        /// <para>'01': Enero</para>
        /// <para>'02': Febrero</para> 
        /// <para>'03': Marzo</para> 
        /// <para>'04': Abril</para>
        /// <para>'05': Mayo</para>
        /// <para>'06': Junio</para>
        /// <para>'07': Julio</para>
        /// <para>'08': Agosto</para>
        /// <para>'09': Septiembre</para>
        /// <para>'10': Octubre</para>
        /// <para>'11': Noviembre</para>
        /// <para>'12': Diciembre</para>
        /// <para>'0A': Anual</para>
        /// <para>'1T': 1º Trimestre</para>
        /// <para>'2T': 2º Trimestre</para>
        /// <para>'3T': 3º Trimestre</para>
        /// <para>'4T': 4º Trimestre</para>
        /// 
        /// </summary>
        [XmlElement("PeriodoDeduccion", Namespace = Settings.NamespaceSii)]
        public string PeriodoDeduccion { get; set; }


        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public FacturaRecibida()
        {
            Contraparte = new Contraparte();
            DesgloseFactura = new DesgloseFacturaR();
        }
    }
}
