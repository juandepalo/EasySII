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
using EasySII.Tax;
using EasySII.Xml;
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Collections.Generic;

namespace EasySII.Business
{
    /// <summary>
    /// Factura recibida.
    /// </summary>
    public class APInvoice : Invoice,  IBatchItem
    {

        /// <summary>
        /// Pagos emitidos por la factura recibida.
        /// </summary>
        public List<APInvoicePayment> APInvoicePayments { get; set; }

        /// <summary>
        /// Fecha asiento contable de la factura recibida.
        /// Se corresponde con el campo los datos de periodo imnpositivo:
        /// ejercicio y periodo.
        /// </summary>
        public DateTime? PostingDate { get; set; }

		/// <summary>
		/// Fecha registro contable de la factura recibida.
		/// Se corresponde con el campo FechaRegContable de la
		/// especificación de la aeat para el SII.
		/// </summary>
		public DateTime? RegisterDate { get; set; }

		/// <summary>
		/// Cuando el identificador es distinto del NIF establece el tipo de identificador utilizado.
		/// </summary>
		public IDOtroType IDOtroType { get; set; }

        /// <summary>
        /// Código ISO del pais cuando el NIF no es español.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Indica si se trata de una operación de inversión del sujeto pasivo.
        /// </summary>
        public bool IsInversionSujetoPasivo { get; set; }

        /// <summary>
        /// Desde Fecha de presentación de las facturas en la AEAT.
        /// </summary>
        public DateTime? SinceDate { get; set; }

        /// <summary>
        /// Hasta Fecha de presentación de las facturas en la AEAT.
        /// </summary>
        public DateTime? UntilDate { get; set; }

        /// <summary>
        /// Indica si se trata de una operación de inversión del sujeto pasivo.
        /// </summary>
        public bool IsFiltroClavePag { get; set; }

		/// <summary>
		/// Representación calculada del objeto de negocio factura recibida (APInvoice)
		/// como un objeto definido según las especificaciones del SII.
		/// </summary>
		public RegistroLRFacturasRecibidas InnerSII { get; private set; }

		/// <summary>
		/// Constructor de APInvoice.
		/// </summary>
		public APInvoice()
        {
            APInvoicePayments = new List<APInvoicePayment>();
            IDOtroType = IDOtroType.OtroDocProbatorio;
        }

        /// <summary>
        /// Constructor de ARInvoice.
        /// </summary>
        /// <param name="registroLRFacturasRecibidas">Objeto serialización xml facturas emitidas.</param>
        public APInvoice(RegistroLRFacturasRecibidas registroLRFacturasRecibidas)
        {

            APInvoicePayments = new List<APInvoicePayment>();
            InvoicesRectified = new List<InvoiceRectified>();

            RegistroLRFacturasRecibidas siiInvoice = registroLRFacturasRecibidas;

            InvoiceNumber = siiInvoice.IDFactura.NumSerieFacturaEmisor;
            IssueDate = Convert.ToDateTime(siiInvoice.IDFactura.FechaExpedicionFacturaEmisor);
            PostingDate = Convert.ToDateTime(siiInvoice.FacturaRecibida.FechaRegContable);

            if(siiInvoice.FacturaRecibida.FechaOperacion!=null)
                OperationIssueDate = Convert.ToDateTime(siiInvoice.FacturaRecibida.FechaOperacion);

            SellerParty = new Party()
            {
                TaxIdentificationNumber = siiInvoice.IDFactura.IDEmisorFactura.NIF,
                PartyName = siiInvoice.FacturaRecibida.Contraparte.NombreRazon
            };

            if (siiInvoice.FacturaRecibida.Contraparte.IDOtro != null)
            {

                // Si no es un nif español

                IDOtroType =
                (IDOtroType)Convert.ToInt32(
                    siiInvoice.FacturaRecibida.Contraparte.IDOtro.IDType);

                CountryCode = siiInvoice.FacturaRecibida.Contraparte.IDOtro.CodigoPais;
                SellerParty.TaxIdentificationNumber = siiInvoice.FacturaRecibida.Contraparte.IDOtro.ID;

            }


            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

            if (PostingDate == null)
                throw new ArgumentNullException("PostingDate is null.");

            InvoiceType invoiceType;

            if (!Enum.TryParse<InvoiceType>(siiInvoice.FacturaRecibida.TipoFactura, out invoiceType))
                throw new InvalidOperationException($"Unknown invoice type {siiInvoice.FacturaRecibida.TipoFactura}");

            InvoiceType = invoiceType;


            ClaveRegimenEspecialOTrascendencia =
                (ClaveRegimenEspecialOTrascendencia)Convert.ToInt32(
                    siiInvoice.FacturaRecibida.ClaveRegimenEspecialOTrascendencia);

            // Gestion rectificativa

            if (siiInvoice.FacturaRecibida.TipoRectificativa != null)
            {

                if (!siiInvoice.FacturaRecibida.TipoFactura.StartsWith("R"))
                    throw new ArgumentException("For TipoRectificativa not null TipoFactura must begin with 'R'.");

                FacturaRecibida facturaRecibidaRectificativa = siiInvoice.FacturaRecibida;

                if (facturaRecibidaRectificativa.ImporteRectificacion != null)
                { 
                    RectifiedBase = SIIParser.ToDecimal(facturaRecibidaRectificativa.ImporteRectificacion.BaseRectificada);
                    RectifiedAmount = SIIParser.ToDecimal(facturaRecibidaRectificativa.ImporteRectificacion.CuotaRectificada);
                }

                RectifiedType rectifiedType;

                if (Enum.TryParse<RectifiedType>(facturaRecibidaRectificativa.TipoRectificativa, out rectifiedType))
                    RectifiedType = rectifiedType;

                if (facturaRecibidaRectificativa.FacturasRectificadas.Count == 0)
                    throw new ArgumentException("FacturasRectificadas without elements.");


                foreach (var regRect in facturaRecibidaRectificativa.FacturasRectificadas)
                {
                    InvoiceRectified invoiceRectified = new InvoiceRectified();

                    invoiceRectified.RectifiedInvoiceNumber = regRect.NumSerieFacturaEmisor;
                    invoiceRectified.RectifiedIssueDate = Convert.ToDateTime(regRect.FechaExpedicionFacturaEmisor);

                    InvoicesRectified.Add(invoiceRectified);
                }

            }

            // Gestion desgloses

            decimal taxRate, taxBase, taxAmount, taxRateRE, taxAmountRE;

            if (siiInvoice.FacturaRecibida.DesgloseFactura != null)
            {
                DesgloseIVA desgloseIVA = null;
                if (siiInvoice.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo != null &&
                    siiInvoice.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo.DetalleIVA.Count > 0)
                {
                    desgloseIVA = siiInvoice.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo;
                }
                else
                {

                    if (siiInvoice.FacturaRecibida.DesgloseFactura.DesgloseIVA == null)
                        throw new ArgumentException("For InversionSujetoPasivo DesgloseIVA must be present on DesgloseFactura.");

                    desgloseIVA = siiInvoice.FacturaRecibida.DesgloseFactura.DesgloseIVA;

                }

                foreach (var iva in desgloseIVA.DetalleIVA)
                {

                    taxRate = SIIParser.ToDecimal(iva.TipoImpositivo);
                    taxBase = SIIParser.ToDecimal(iva.BaseImponible);
                    taxAmount = SIIParser.ToDecimal(iva.CuotaSoportada); 
                    taxRateRE = SIIParser.ToDecimal(iva.TipoRecargoEquivalencia);
                    taxAmountRE = SIIParser.ToDecimal(iva.CuotaRecargoEquivalencia);

                    TaxesOutputs.Add(taxRate, new decimal[] { taxBase, taxAmount, taxRateRE, taxAmountRE });

                }

            }
            else
            {
                throw new ArgumentException("DesgloseFactura no present in FacturaRecibida.");
            }

            // Gestión cuota deducible

            GrossAmount = SIIParser.ToDecimal(siiInvoice.FacturaRecibida.ImporteTotal);
            InvoiceText = siiInvoice.FacturaRecibida.DescripcionOperacion;

        }

		/// <summary>
		/// Elimina el objeto SII subyacente.
		/// </summary>
		public void ClearInnerSII()
		{
			InnerSII = null;
		}

		/// <summary>
		/// Obtiene un objeto RegistroLRFacturasRecibidas, este objeto se utiliza
		/// para la serialización xml.
		/// </summary>
		/// <param name="updateInnerSII">Si es true, actualiza el objeto SII subyacente
		/// con el valor calculado.</param>
		/// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
		/// <returns>Nueva instancia del objeto para serialización 
		/// xml RegistroLRFacturasEmitidas.</returns>
		public RegistroLRFacturasRecibidas ToSII(bool updateInnerSII = false, bool skipErrors = false)
        {

			if (InnerSII != null)
				return InnerSII;

            RegistroLRFacturasRecibidas siiInvoice = new RegistroLRFacturasRecibidas();

            if (IssueDate == null && !skipErrors)
                throw new ArgumentNullException("IssueDate is null.");

            if (PostingDate == null && !skipErrors)
                throw new ArgumentNullException("PostingDate is null.");

			if (RegisterDate == null && !skipErrors)
				throw new ArgumentNullException("RegisterDate is null.");


            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiInvoice.PeriodoImpositivo.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiInvoice.PeriodoImpositivo.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiInvoice.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiInvoice.PeriodoLiquidacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            if (SellerParty == null && !skipErrors)
                throw new ArgumentNullException("SellerParty is null.");

			if(GrossAmount != 0)
				siiInvoice.FacturaRecibida.ImporteTotal = SIIParser.FromDecimal(GrossAmount);


            if (!(Settings.Current.IDVersionSii.CompareTo("1.1") < 0))
                if (GrossAmount > 100000)
                    siiInvoice.FacturaRecibida.Macrodato = "S";

            siiInvoice.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;
			siiInvoice.IDFactura.FechaExpedicionFacturaEmisor = SIIParser.FromDate(IssueDate);

			/* B) Facturas Recibidas
			 
			En un plazo de cuatro días naturales desde la fecha en que se produzca el registro 
			contable de la factura y, en todo caso, antes del día 16 del mes siguiente al periodo 
			de liquidación en que se hayan incluido las operaciones correspondientes 
			(período en que se deduce el IVA soportado).
			Se entiende que el registro contable de la factura se produce en la fecha de entrada 
			en el sistema contable con independencia de la fecha reflejada en el asiento contable. */

			siiInvoice.FacturaRecibida.FechaRegContable = SIIParser.FromDate(RegisterDate);

			if (OperationIssueDate != null)
				siiInvoice.FacturaRecibida.FechaOperacion = SIIParser.FromDate(OperationIssueDate);

			siiInvoice.FacturaRecibida.TipoFactura = InvoiceType.ToString();
            siiInvoice.FacturaRecibida.ClaveRegimenEspecialOTrascendencia = 
                ((int)ClaveRegimenEspecialOTrascendencia).ToString().PadLeft(2, '0');

            siiInvoice.FacturaRecibida.DescripcionOperacion = InvoiceText;

			if (InvoiceType == InvoiceType.F5)
			{
				/* SII_Descripcion_ServicioWeb_v0.7.pdf (pag. 203)
				  8.1.2.2.Ejemplo mensaje XML de alta de importación
				En los datos identificativos correspondientes al proveedor se consignaran los del importador y titular del libro registro
				Deberán consignarse, como número de factura y fecha de expedición, el número de referencia que figura en el propio DUA y la fecha de su
				admisión por la Administración Aduanera respectivamente*/

				SellerParty = BuyerParty;

			}

			siiInvoice.FacturaRecibida.Contraparte = GetContraparte(siiInvoice, skipErrors);

            // Tratamiento de las facturas rectificativas.

            if (InvoicesRectified.Count != 0)
            {                

                siiInvoice.FacturaRecibida.TipoRectificativa =  RectifiedType.ToString();


				if (RectifiedBase != 0)
				{
					// Si consta el datos de importe rectificacion (tipo rectif 's'), lo ponemos
					ImporteRectificacion importeRectifica = new ImporteRectificacion();

					importeRectifica.BaseRectificada = SIIParser.FromDecimal(RectifiedBase);
					importeRectifica.CuotaRectificada = SIIParser.FromDecimal(RectifiedAmount);
					importeRectifica.CuotaRecargoRectificado = SIIParser.FromDecimal(RectifiedEquivCharge);
					siiInvoice.FacturaRecibida.ImporteRectificacion = importeRectifica;
				}

                siiInvoice.FacturaRecibida.FacturasRectificadas = new List<IDFactura>();

                foreach (var regRect in InvoicesRectified)
                {
                    IDFactura FactRectificada = new IDFactura();

                    FactRectificada.NumSerieFacturaEmisor = regRect.RectifiedInvoiceNumber;
                    FactRectificada.FechaExpedicionFacturaEmisor = SIIParser.FromDate(regRect.RectifiedIssueDate);
                    // En este caso pongo a null IDEmisorFactura para que no serialice una etiqueta vacía.
                    FactRectificada.IDEmisorFactura = null;

                    siiInvoice.FacturaRecibida.FacturasRectificadas.Add(FactRectificada);
                }

            }
            else
            {
                if (InvoiceType.ToString().StartsWith("R") && !skipErrors)
                    throw new Exception("RectifiedInvoiceNumber for InvoiceType of kind 'R' must be not null.");
            }


			// Desgloses		

			DesgloseIVA desgloseIVA = GetDesgloseIVA();

		
			if (IsInversionSujetoPasivo)
				siiInvoice.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo = desgloseIVA;
			else
				siiInvoice.FacturaRecibida.DesgloseFactura.DesgloseIVA = desgloseIVA;
			

            decimal cuotaDeducible = 0;

            foreach (KeyValuePair<decimal, decimal[]> kvp in TaxesOutputs)
                cuotaDeducible += kvp.Value[1];

            siiInvoice.FacturaRecibida.CuotaDeducible = SIIParser.FromDecimal(cuotaDeducible);

			if (updateInnerSII)
				InnerSII = siiInvoice;

			return siiInvoice;

        }

		/// <summary>
		/// Devuelve un objeto Contraparte del SII mediante los datos de la
		/// factura actual.
		/// </summary>
		/// <param name="siiInvoice">Factura a la que pertenece la contraparte.</param>
		/// <param name="skipErrors">Indica si hay que omitir las excepciones.</param> 
		/// <returns> Contraparte para el SII.</returns>
		internal Contraparte GetContraparte(RegistroLRFacturasRecibidas siiInvoice, bool skipErrors = false)
		{

			Contraparte contraparte = new Contraparte();
			// Gestionar los asuntos relacionados con el NIF del comprador
			// Si no es nacional o comienza con 'N' en lugar de DesgloseFactura 
			// hay que informar DesgloseTipoOperacion

			TaxIdEs taxIdEs = null;	

			// Si no es una factura simplificada o asiento resumen
			if (InvoiceType != InvoiceType.F4 && InvoiceType != InvoiceType.F2)
			{

				// Bandera que indica si el NIF es o no es español

				bool IsNotNifES = false;


				try
				{
					taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
				}
				catch
				{
					IsNotNifES = true;
				}

				if (taxIdEs != null)
					IsNotNifES = !taxIdEs.IsDCOK;


				if (SellerParty != null)
				{

					contraparte.NombreRazon = SellerParty.PartyName;

					if (IsNotNifES)
					{

						if (CountryCode == null && IDOtroType != IDOtroType.NifIva && !skipErrors)
							throw new ArgumentNullException(
								$"Invoice {InvoiceNumber}: For foreign tax identificator number Country Code can't be null");

						// Si no es un nif español
						contraparte.IDOtro = new IDOtro();
						contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');

						if (CountryCode != null)
							contraparte.IDOtro.CodigoPais = CountryCode;

						if (!string.IsNullOrEmpty(SellerParty.TaxIdentificationNumber))
							contraparte.IDOtro.ID = SellerParty.TaxIdentificationNumber;

						siiInvoice.IDFactura.IDEmisorFactura.IDOtro = contraparte.IDOtro;

					}
					else
					{
						if (IDOtroType == IDOtroType.NoCensado)
						{

							/* SII_Descripcion_ServicioWeb_v0.7.pdf
							 * 8.1.1.4.Ejemplo mensaje XML de alta cuando la contraparte no está censada
							Para los casos en que se haya rechazado una factura emitida debido a que la contraparte (NIF y nombre) no está censada en la AEAT, podrá
							enviar dicha factura, en un segundo intento, suministrando el NIF en el bloque <IdOtro> con los siguientes contenidos:
							Código país: ES
							Clave ID: 07. No censado*/

							contraparte.IDOtro = new IDOtro();
							contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');

							if ((CountryCode == null || CountryCode != "ES") && !skipErrors)
								throw new ArgumentNullException(
								$"Invoice {InvoiceNumber}: For IDOtroType.NoCensado Country Code can't be null. Must be 'ES'");

							contraparte.IDOtro.CodigoPais = CountryCode;

							if (!string.IsNullOrEmpty(SellerParty.TaxIdentificationNumber))
								contraparte.IDOtro.ID = SellerParty.TaxIdentificationNumber;

							siiInvoice.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;

						}
						else
						{
							siiInvoice.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
							contraparte.NIF = SellerParty.TaxIdentificationNumber;
						}

					}
				}
			}
			else
			{
				// Para las simplificadas null en contraparte para evitar tag vacio
				contraparte = null;
			}

			return contraparte;

		}


		/// <summary>
		/// Devuelve el cuerpo de una operación sujeta.
		/// </summary>
		/// <param name = "skipErrors" > Indica si hay que omitir las excepciones.</param> 
		/// <returns>Operación sujeta correspondiente a la factura.</returns>
		internal DesgloseIVA GetDesgloseIVA(bool skipErrors = false)
        {

            DesgloseIVA desgloseIVA = new DesgloseIVA();

            if (TaxesOutputs.Count > 0)
            {                               

                foreach (KeyValuePair<decimal, decimal[]> taxOut in TaxesOutputs)
                {

					DetalleIVA detalleIVA = new DetalleIVA()
					{
						TipoImpositivo = (taxOut.Value[2] == 0 && taxOut.Value[3] == 0) ? SIIParser.FromDecimal(taxOut.Key) : "0",
						BaseImponible = SIIParser.FromDecimal(taxOut.Value[0]),
						CuotaSoportada = SIIParser.FromDecimal(taxOut.Value[1])
					};

					if (taxOut.Value[2] != 0)
					{
						detalleIVA.TipoRecargoEquivalencia = SIIParser.FromDecimal(taxOut.Key);
						detalleIVA.CuotaRecargoEquivalencia = SIIParser.FromDecimal(taxOut.Value[2]);
					}

					if (taxOut.Value[3] != 0)
					{
						if (taxOut.Value[2] != 0 && !skipErrors)
							throw new Exception("Only one value can be non zero: taxAmountCompensacionREAGYP != 0 and taxAmountRecargoEquivalencia != 0.");
					
						if(ClaveRegimenEspecialOTrascendencia != ClaveRegimenEspecialOTrascendencia.ExportacionREAGYP && !skipErrors)
							throw new Exception("ClaveRegimenEspecialOTrascendencia must be ClaveRegimenEspecialOTrascendencia.ExportacionREAGYP.");

						detalleIVA.TipoImpositivo = detalleIVA.CuotaSoportada = null;
						detalleIVA.PorcentCompensacionREAGYP = SIIParser.FromDecimal(taxOut.Key);
						detalleIVA.ImporteCompensacionREAGYP = SIIParser.FromDecimal(taxOut.Value[3]);						

					}


					desgloseIVA.DetalleIVA.Add(detalleIVA);
                }
            }
            else
            {
				desgloseIVA.DetalleIVA.Add(new DetalleIVA()
				{
					BaseImponible = SIIParser.FromDecimal(GrossAmount)
				});
			}

            return desgloseIVA;
        }


        internal FiltroConsulta ToFilterSII()
        {

            FiltroConsulta siiFilter = new FiltroConsulta();

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiFilter.PeriodoImpositivo.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiFilter.PeriodoImpositivo.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiFilter.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiFilter.PeriodoLiquidacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            TaxIdEs taxIdEs = null;

            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;

            siiFilter.ClavePaginacion = new ClavePaginacion();
            siiFilter.IDFactura = new IDFactura();

            if (SellerParty != null)
            {

                if (IsFiltroClavePag == true)
                {
                    siiFilter.ClavePaginacion.IDEmisorFactura.NombreRazon = SellerParty.PartyName;
                }
                else
                {
                    siiFilter.IDFactura.IDEmisorFactura.NombreRazon = SellerParty.PartyName;
                }

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    if (IsFiltroClavePag == true)
                    {
                        siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro = new IDOtro();
                        siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                        siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                        siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;
                    }
                    else
                    {
                        siiFilter.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                        siiFilter.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                        siiFilter.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                        siiFilter.IDFactura.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;
                    }
                }
                else
                {
                    if (IsFiltroClavePag == true)
                    {
                        siiFilter.ClavePaginacion.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
                    }
                    else
                    {
                        siiFilter.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
                    }
                }

                if (InvoiceNumber != null)
                {
                    if (IsFiltroClavePag == true)
                    {
                        siiFilter.ClavePaginacion.NumSerieFacturaEmisor = InvoiceNumber;
                    }
                    else
                    {
                        siiFilter.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;
                    }
                }

                if (IssueDate != null)
                {
                    if (IsFiltroClavePag == true)
                    {
                        siiFilter.ClavePaginacion.FechaExpedicionFacturaEmisor =
								SIIParser.FromDate(IssueDate);
                    }
                    else
                    {
                        siiFilter.IDFactura.FechaExpedicionFacturaEmisor =
								SIIParser.FromDate(IssueDate);
                    }
                }

            }


            // Tratamiento del Desde/Hasta Fecha Presentación.
            if (SinceDate != null && UntilDate != null)
            {
                if (siiFilter.FechaPresentacion == null)
                    siiFilter.FechaPresentacion = new RangoFechaPresentacion();

                siiFilter.FechaPresentacion.Desde = SIIParser.FromDate(SinceDate);
                siiFilter.FechaPresentacion.Hasta = SIIParser.FromDate(UntilDate);
            }

            return siiFilter;

        }

        /// <summary>
        /// Devuelve un objeto XML con un filtro consulta de facturas emitidas.
        /// </summary>
        /// <returns>Objeto XML con un filtro consulta de facturas emitidas.</returns>
        internal ConsultaFactInformadasProveedor ToFilterExternSII()
        {

            PostingDate = RegisterDate = IssueDate;

            RegistroLRFacturasRecibidas siiInvoice = ToSII();

            ConsultaFactInformadasProveedor siiFilter = new ConsultaFactInformadasProveedor();

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

            siiFilter.FiltroConsulta.PeriodoImputacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
            siiFilter.FiltroConsulta.PeriodoImputacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");

            if (SellerParty != null)
                siiFilter.FiltroConsulta.Proveedor = siiInvoice.FacturaRecibida.Contraparte;

            // Tratamiento del Desde/Hasta Fecha Presentación.
            if (SinceDate != null && UntilDate != null)
            {
                if (siiFilter.FiltroConsulta.FechaExpedicion == null)
                    siiFilter.FiltroConsulta.FechaExpedicion = new RangoFechaPresentacion();

                siiFilter.FiltroConsulta.FechaExpedicion.Desde = SIIParser.FromDate(SinceDate);
                siiFilter.FiltroConsulta.FechaExpedicion.Hasta = SIIParser.FromDate(UntilDate);
            }

            return siiFilter;

        }

        /// <summary>
        /// Devuelve el registro de cobros relacionados con la factura
        /// en un objeto XML.
        /// </summary>
        /// <returns>Objeto XML de registro de cobros relacionados con la factura.</returns>
        internal RegistroLRPagos ToPaymentsSII()
        {

            RegistroLRPagos registroLRPagos = new RegistroLRPagos();

            registroLRPagos.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;
            registroLRPagos.IDFactura.FechaExpedicionFacturaEmisor = SIIParser.FromDate(IssueDate);

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;


            if (SellerParty != null)
            {
                registroLRPagos.IDFactura.IDEmisorFactura.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    registroLRPagos.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                    registroLRPagos.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    registroLRPagos.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    registroLRPagos.IDFactura.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;
                }
                else
                {
                    registroLRPagos.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
                }
            }

            foreach (var payment in APInvoicePayments)
            {

                if (payment.PaymentDate == null)
                    throw new ArgumentNullException("PaymentDate is null.");

                Cobro cobro = new Cobro()
                {
                    Fecha = (payment.PaymentDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy"),
                    Medio = ((int)payment.PaymentTerm).ToString().PadLeft(2, '0'),
                    Importe = SIIParser.FromDecimal(payment.PaymentAmount),
                    Cuenta_O_Medio = payment.AccountOrTermsText
                };

                registroLRPagos.Pagos.Add(cobro);
            }

            return registroLRPagos;
        }

        /// <summary>
        /// Crea un filtro de cobros relacionados con la factura
        /// utilizando como filtro los datos de la misma.
        /// </summary>
        /// <returns>Objeto XML de filtro para consulta de  de factura.</returns>
        internal FiltroConsultaCobrosPagos ToFilterPagosSII()
        {

            FiltroConsultaCobrosPagos siiFilter = new FiltroConsultaCobrosPagos();

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (InvoiceNumber == null)
                throw new ArgumentNullException("InvoiceNumber is null.");

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;


            if (SellerParty != null)
            {

                siiFilter.IDFactura = new IDFactura();
                siiFilter.IDFactura.IDEmisorFactura.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiFilter.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                    siiFilter.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiFilter.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiFilter.IDFactura.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;
                }
                else
                {
                    siiFilter.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
                }

                if (InvoiceNumber != null)
                    siiFilter.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;

                if (IssueDate != null)
                    siiFilter.IDFactura.FechaExpedicionFacturaEmisor =
                        (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");

            }

            return siiFilter;

        }


        internal RegistroLRBajaRecibidas ToRegistroLRBajaRecibidasSII()
        {

            RegistroLRBajaRecibidas siiDelete = new RegistroLRBajaRecibidas();

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (InvoiceNumber == null)
                throw new ArgumentNullException("InvoiceNumber is null.");

            if (IssueDate == null)
                throw new ArgumentNullException("PostingDate is null.");
 

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiDelete.PeriodoImpositivo.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiDelete.PeriodoImpositivo.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiDelete.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiDelete.PeriodoLiquidacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;


            if (SellerParty != null)
            {

                siiDelete.IDFactura = new IDFactura();
                siiDelete.IDFactura.IDEmisorFactura.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiDelete.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                    siiDelete.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiDelete.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiDelete.IDFactura.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiDelete.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
                }

                if (InvoiceNumber != null)
                    siiDelete.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;

                if (IssueDate != null)
                    siiDelete.IDFactura.FechaExpedicionFacturaEmisor =
						SIIParser.FromDate(IssueDate);

            }

            return siiDelete;

        }

        /// <summary>
        /// Obtiene un objeto RegistroLRFacturasRecibidas, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
        /// <param name="batchActionKey">Tipo de lote.</param>
        /// <param name="updateInnerSII">Si es true, actualiza el objeto SII subyacente
        /// con el valor calculado.</param>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLRFacturasEmitidas.</returns>
        public object ToSIIBatchItem(BatchActionKeys batchActionKey,
            bool updateInnerSII = false, bool skipErrors = false)
        {
            switch (batchActionKey)
            {
                case BatchActionKeys.LR:
                    return ToSII(updateInnerSII, skipErrors);
                case BatchActionKeys.DR:
                    return ToRegistroLRBajaRecibidasSII();
                case BatchActionKeys.PG:
                    return ToPaymentsSII();
            }

            throw new Exception($"Unknown BatchActionKey: {batchActionKey}");
        }

    }
}
