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
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;

namespace EasySII.Business
{
    /// <summary>
    /// Factura emitida. Añade funcionalidad para las facturas
    /// no sujetas por reglas de localización (Canarias, Ceuta y Melilla,
    /// por ejemplo que no están sujetas a IVA ya que están sujetas a
    /// IGIC o IPSI).
    /// </summary>
    public class ARInvoiceAV : ARInvoice
    {    

        /// <summary>
        /// Indica si la factura es no sujeta en el TAI por reglas de localización.
        /// </summary>
        public bool IsTAIRules { get; set; }

        /// <summary>
        /// Constructor de ARInvoice.
        /// </summary>
        public ARInvoiceAV() : base()
        {
        }

        /// <summary>
        /// Constructor de ARInvoice.
        /// </summary>
        /// <param name="registroLRFacturasEmitidas">Objeto serialización xml facturas emitidas.</param>
        public ARInvoiceAV(RegistroLRFacturasEmitidas registroLRFacturasEmitidas) : 
            base(registroLRFacturasEmitidas)
        {

            RegistroLRFacturasEmitidas siiInvoice = registroLRFacturasEmitidas;

            // Gestión del tipo desglose           
            NoSujeta noSujeta = siiInvoice?.FacturaExpedida?.TipoDesglose?.DesgloseFactura?.NoSujeta; 
            if (noSujeta != null)
            {
                if (noSujeta.ImporteTAIReglasLocalizacion != null)
                {
                    IsTAIRules = true;
                    GrossAmount = SIIParser.ToDecimal(noSujeta.ImporteTAIReglasLocalizacion);
                }
            }

        } 

		/// <summary>
		/// Para NIF nacionales no comenzados en 'N'.
		/// </summary>
		/// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
		/// <returns>Desglose de factura.</returns>
		internal override DesgloseFactura GetDesgloseFactura(bool skipErrors = false)
        {

            DesgloseFactura desgloseFactura = new DesgloseFactura();

            if (ClaveRegimenEspecialOTrascendencia == ClaveRegimenEspecialOTrascendencia.IpsiIgic ||
				IsTaxExcluded || IsTAIRules)
                desgloseFactura.NoSujeta = GetNoSujeta(skipErrors);
            else
                desgloseFactura.Sujeta = GetSujeta(skipErrors);

            return desgloseFactura;

        }  

	}
}
