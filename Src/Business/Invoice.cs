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

using EasySII.Business.Batches;
using System;
using System.Collections.Generic;

namespace EasySII.Business
{
    /// <summary>
    /// Representa una factura.
    /// </summary>
    public class Invoice
    {

        Dictionary<decimal, decimal[]> _TaxesOutputs;     

        /// <summary>
        /// Vendedor.
        /// </summary>
        public virtual Party SellerParty { get; set; }

        /// <summary>
        /// Parte compradora que figura en la factura.
        /// </summary>
        public virtual Party BuyerParty { get; set; }

        /// <summary>
        /// Fecha de emisión de la factura.
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Número de factura.
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Texto de cabecera de factura.
        /// </summary>
        public string InvoiceText { get; set; }

        /// <summary>
        /// Importe total.
        /// </summary>
        public decimal GrossAmount { get; set; }

        /// <summary>
        /// Indica si los impuestos incluidos son por inversión del sujeto pasivo
        /// o por el contrario son por operaciones interiores.
        /// </summary>
        public SujetaType Sujeta { get; set; }

        /// <summary>
        /// <para>Determina el tipo de factura del que se trata según 
        ///  la especificación de la AEAT en su lista de valores 
        ///  'L3.1  Clave de régimen especial o trascendencia en facturas recibidas':</para>
        ///  <para>01   Operación de régimen común.</para>
        ///  <para>02   Operaciones por las que los Empresarios satisfacen compensaciones REAGYP.</para>
        ///  <para>03   Operaciones a las que se aplique el régimen especial de bienes usados, 
        ///  objetos de arte, antigüedades y objetos de colección (135-139 DE LIVA)</para>
        ///  <para>04   Régimen especial oro de inversión</para>
        ///  <para>05   Régimen especial agencias de viajes</para>
        ///  <para>06   Régimen especial grupo de entidades en IVA</para>
        ///  <para>07   Régimen especial grupo de entidades en IVA (Nivel Avanzado)</para>
        ///  <para>08   Régimen especial criterio de caja</para>
        ///  <para>09   Operaciones sujetas al IPSI / IGIC</para>
        ///  <para>10   Adquisiciones intracomunitarias de bienes y prestaciones de servicios</para>
        ///  <para>11   Compra de agencias viajes: operaciones de mediación en nombre y por cuenta 
        ///  ajena en los servicios de transporte prestados al destinatario de los servicios de 
        ///  acuerdo con el apartado 3 de la disposición adicional cuarta del Reglamento de Facturación.</para>
        ///  <para>12   Facturación de las prestaciones de servicios de agencias de viaje que actúan como
        ///  mediadoras en nombre y por cuenta ajena (D.A.4ª RD1619/2012)</para>
        ///  <para>13   Cobros por cuenta de terceros de honorarios profesionales o de dº derivados de la
        ///  propiedad industrial, de autor u otros por cuenta de sus socios, asociados o colegiados
        ///  efectuados por sociedades, asociaciones, colegios profesionales u otras entidades que,
        ///  entre sus funciones, realicen las de cobro.</para>
        ///  <para>14   Operaciones de seguros</para>
        ///  <para>15   Operaciones de arrendamiento de local de negocio</para>
        /// </summary>
        public ClaveRegimenEspecialOTrascendencia ClaveRegimenEspecialOTrascendencia { get; set; }

        /// <summary>
        /// Tipos de factura.
        /// </summary>
        public InvoiceType InvoiceType { get; set; }

        /// <summary>
        /// Tipos de rectificativa.
        /// </summary>
        public RectifiedType RectifiedType { get; set; }

        /// <summary>
        /// Fecha de operación de la factura.
        /// </summary>
        public DateTime? OperationIssueDate { get; set; }

        /// <summary>
        /// Lista de facturas rectificadas por la rectificativa.
        /// </summary>
        public List<InvoiceRectified> InvoicesRectified { get; set; }

        /// <summary>
        /// Causa exención en su caso.
        /// </summary>
        public CausaExencion CausaExencion { get; set; }

        /// <summary>
        /// Total Base Impuesto Rectificado.
        /// </summary>
        public decimal RectifiedBase { get; set; }

        /// <summary>
        /// Total Cuota Impuesto Rectificado.
        /// </summary>
        public decimal RectifiedAmount { get; set; }

		/// <summary>
		/// Total Cuota recargo de equivalencia Rectificado.
		/// </summary>
		public decimal RectifiedEquivCharge { get; set; }

		/// <summary>
		/// Líneas de IVA.
		/// </summary>
		public Dictionary<decimal, decimal[]> TaxesOutputs
        {
            get
            {
                return _TaxesOutputs;
            }
        }

        /// <summary>
        /// Constructor Invoice.
        /// </summary>
        public Invoice()
        {
            _TaxesOutputs = new Dictionary<decimal, decimal[]>();
            RectifiedType = RectifiedType.I;
            InvoicesRectified = new List<InvoiceRectified>();
        }

		/// <summary>
		/// Añade una línea de impuestos a la factura, o si ya existe el tipo
		/// impositivo facilitado mediante el parámetro taxRate, se suma la base
		/// y la cuota indicada al registro ya existente para dicho tipo.
		/// </summary>
		/// <param name="taxRate">Tipo impositivo.</param>
		/// <param name="taxableBase">Base imponible.</param>
		/// <param name="taxAmount">Cuota impuesto</param>
		/// <param name="taxAmountRecargoEquivalencia">Cuota recargo equivalencia.</param>
		/// <param name="taxAmountCompensacionREAGYP">Cuota Compensación REAGYP.</param>
		public void AddTaxOtuput(decimal taxRate, decimal taxableBase, decimal taxAmount, 
			decimal taxAmountRecargoEquivalencia = 0, decimal taxAmountCompensacionREAGYP = 0)
        {

            decimal[] tax;

            if (!_TaxesOutputs.TryGetValue(taxRate, out tax))
            { 
                _TaxesOutputs.Add(taxRate, new decimal[] { taxableBase, taxAmount, taxAmountRecargoEquivalencia, taxAmountCompensacionREAGYP});
            }
            else
            {
                _TaxesOutputs[taxRate][0] += taxableBase;
                _TaxesOutputs[taxRate][1] += taxAmount;
                _TaxesOutputs[taxRate][2] += taxAmountRecargoEquivalencia;
				_TaxesOutputs[taxRate][3] += taxAmountCompensacionREAGYP;
			}
        }

        /// <summary>
        /// Devuelve un identificador para la instancia de item: InvoiceNumber.
        /// </summary>
        public virtual string GetItemKey()
        {
            return InvoiceNumber;
        }

    }
}
