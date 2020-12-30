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
    /// Factura emitida.
    /// </summary>
    public class ARInvoice : Invoice, IBatchItem
    {

        /// <summary>
        /// Número de factura de la última factura simplificada
        /// incluida en un asiento resumen de factura simplificadas.
        /// Este dato sólo debe suministrarse para facturas con el
        /// tipo de factura F4 (asiento resumen).
        /// </summary>
        public string InvoiceNumberLastItem { get; set; }

        /// <summary>
        /// Parte compradora que figura en la factura.
        /// </summary>
        public override Party BuyerParty { get; set; }

        /// <summary>
        /// Indica si la factura es de servicios. Se utiliza para el
        /// desglose por tipo de operación. Valor por defecto true.
        /// Trabaja a nivel de cabecera.
        /// </summary>
        public bool IsService { get; set; }

        /// <summary>
        /// Indica si la factura es no sujeta por artículo 7, 14 u otros.
        /// </summary>
        public bool IsTaxExcluded { get; set; }

        /// <summary>
        /// Cuando el identificador es distinto del NIF establece el tipo de identificador utilizado.
        /// </summary>
        public IDOtroType IDOtroType { get; set; }

        /// <summary>
        /// Código ISO del pais cuando el NIF no es español.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Cobros recibidos de la factura.
        /// </summary>
        public List<ARInvoicePayment> ARInvoicePayments { get; set; }

        /// <summary>
        /// Desde Fecha de presentación de las facturas en la AEAT.
        /// </summary>
        public DateTime? SinceDate { get; set; }

        /// <summary>
        /// Hasta Fecha de presentación de las facturas en la AEAT.
        /// </summary>
        public DateTime? UntilDate { get; set; }

        /// <summary>
        /// Representación calculada del objeto de negocio factura emitida (ARInvoice)
        /// como un objeto definido según las especificaciones del SII.
        /// </summary>
        public RegistroLRFacturasEmitidas InnerSII { get; private set; }

        /// <summary>
        /// En el caso de alquileres con clave de operación 12 o 13,
        /// recoge el estado de localización del inmueble.
        /// </summary>
        public BuildingState BuildingState { get; set; }

        /// <summary>
        /// Referencia catastral del inmueble.
        /// </summary>
        public string BuildingReference { get; set; }

        /// <summary>
        /// Constructor de ARInvoice.
        /// </summary>
        public ARInvoice()
        {
            ARInvoicePayments = new List<ARInvoicePayment>();
            IDOtroType = IDOtroType.OtroDocProbatorio;
        }

        /// <summary>
        /// Constructor de ARInvoice.
        /// </summary>
        /// <param name="registroLRFacturasEmitidas">Objeto serialización xml facturas emitidas.</param>
        public ARInvoice(RegistroLRFacturasEmitidas registroLRFacturasEmitidas)
        {
            ARInvoicePayments = new List<ARInvoicePayment>();
            InvoicesRectified = new List<InvoiceRectified>();

            RegistroLRFacturasEmitidas siiInvoice = registroLRFacturasEmitidas;

            SellerParty = new Party()
            {
                TaxIdentificationNumber = siiInvoice.IDFactura.IDEmisorFactura.NIF
            };

            InvoiceNumber = siiInvoice.IDFactura.NumSerieFacturaEmisor;

            if (siiInvoice.FacturaExpedida.TipoFactura == $"{InvoiceType.F4}")
                InvoiceNumberLastItem = siiInvoice.IDFactura.NumSerieFacturaEmisorResumenFin;


            IssueDate = Convert.ToDateTime(siiInvoice.IDFactura.FechaExpedicionFacturaEmisor);

            InvoiceType invoiceType;

            if (!Enum.TryParse<InvoiceType>(siiInvoice.FacturaExpedida.TipoFactura, out invoiceType))
                throw new InvalidOperationException($"Unknown invoice type {siiInvoice.FacturaExpedida.TipoFactura}");

            InvoiceType = invoiceType;

            ClaveRegimenEspecialOTrascendencia =
                (ClaveRegimenEspecialOTrascendencia)Convert.ToInt32(
                    siiInvoice.FacturaExpedida.ClaveRegimenEspecialOTrascendencia);

            GrossAmount = SIIParser.ToDecimal(siiInvoice.FacturaExpedida.ImporteTotal);
            InvoiceText = siiInvoice.FacturaExpedida.DescripcionOperacion;

            if(siiInvoice.FacturaExpedida.FechaOperacion != null)
                OperationIssueDate = Convert.ToDateTime(siiInvoice.FacturaExpedida.FechaOperacion);
                
            BuyerParty = new Party()
            {
                PartyName = siiInvoice.FacturaExpedida.Contraparte.NombreRazon,
                TaxIdentificationNumber = siiInvoice.FacturaExpedida.Contraparte.NIF
            };

            if (siiInvoice.FacturaExpedida.Contraparte.IDOtro != null)
            {

                // Si no es un nif español

                IDOtroType =
                (IDOtroType)Convert.ToInt32(
                    siiInvoice.FacturaExpedida.Contraparte.IDOtro.IDType);

                CountryCode = siiInvoice.FacturaExpedida.Contraparte.IDOtro.CodigoPais;
                BuyerParty.TaxIdentificationNumber = siiInvoice.FacturaExpedida.Contraparte.IDOtro.ID;


            }

            // Gestion rectificativa

            if (siiInvoice.FacturaExpedida.TipoRectificativa != null)
            {

                if (!siiInvoice.FacturaExpedida.TipoFactura.StartsWith("R"))
                    throw new ArgumentException("For TipoRectificativa not null TipoFactura must begin with 'R'.");

                FacturaExpedida facturaExpedidaRectificativa = siiInvoice.FacturaExpedida;

                if (facturaExpedidaRectificativa.ImporteRectificacion != null)
                {
                    RectifiedBase = SIIParser.ToDecimal(facturaExpedidaRectificativa.ImporteRectificacion.BaseRectificada);
                    RectifiedAmount = SIIParser.ToDecimal(facturaExpedidaRectificativa.ImporteRectificacion.CuotaRectificada);
                }

                RectifiedType rectifiedType;

                if (Enum.TryParse<RectifiedType>(facturaExpedidaRectificativa.TipoRectificativa, out rectifiedType))
                    RectifiedType = rectifiedType;

                if (facturaExpedidaRectificativa.FacturasRectificadas.Count == 0)
                    throw new ArgumentException("FacturasRectificadas without elements.");


                foreach (var regRect in facturaExpedidaRectificativa.FacturasRectificadas)
                {
                    InvoiceRectified invoiceRectified = new InvoiceRectified();

                    invoiceRectified.RectifiedInvoiceNumber = regRect.NumSerieFacturaEmisor;
                    invoiceRectified.RectifiedIssueDate = Convert.ToDateTime(regRect.FechaExpedicionFacturaEmisor);

                    InvoicesRectified.Add(invoiceRectified);
                }

            }

            // Gestión del tipo desglose

            Sujeta sujeta;

            if (siiInvoice.FacturaExpedida.TipoDesglose.DesgloseFactura != null)
            {
                sujeta = siiInvoice.FacturaExpedida.TipoDesglose.DesgloseFactura.Sujeta;
            }
            else
            {
                if (siiInvoice.FacturaExpedida.TipoDesglose.DesgloseTipoOperacion == null)
                {
                    throw new ArgumentNullException("Ni DesgloseFactura ni DesgloseTipoOperacion encontrado.");
                }
                else
                {
                    DesgloseTipoOperacion desgloseTipoOperacion =
                        siiInvoice.FacturaExpedida.TipoDesglose.DesgloseTipoOperacion;

                    if (desgloseTipoOperacion.PrestacionServicios == null)
                    {
                        sujeta = desgloseTipoOperacion.Entrega.Sujeta;
                    }
                    else
                    {
                        IsService = true;
                        sujeta = desgloseTipoOperacion.PrestacionServicios.Sujeta;
                    }
                }
            }

            if (sujeta != null)
            {

                if (sujeta.NoExenta != null)
                {

                    SujetaType sujetaType;

                    if (sujeta.NoExenta.TipoNoExenta != null && !Enum.TryParse<SujetaType>(
                        sujeta.NoExenta.TipoNoExenta,
                        out sujetaType))
                        throw new InvalidCastException(
                            $"Unknown sujeta type {sujeta.NoExenta.TipoNoExenta}");


                    foreach (var iva in sujeta.NoExenta.DesgloseIVA.DetalleIVA)
                    {
                        decimal taxRate, taxBase, taxAmount, taxRateRE, taxAmountRE;
                        taxRate = SIIParser.ToDecimal(iva.TipoImpositivo);
                        taxBase = SIIParser.ToDecimal(iva.BaseImponible);
                        taxAmount = SIIParser.ToDecimal(iva.CuotaRepercutida);
                        taxRateRE = SIIParser.ToDecimal(iva.TipoRecargoEquivalencia);
                        taxAmountRE = SIIParser.ToDecimal(iva.CuotaRecargoEquivalencia);

                        TaxesOutputs.Add(taxRate, new decimal[] { taxBase, taxAmount, taxRateRE, taxAmountRE });

                    }
                }

                if (sujeta.Exenta != null)
                {

                    CausaExencion causaExencion;

                    if (!Enum.TryParse<CausaExencion>(sujeta.Exenta.CausaExencion, out causaExencion))
                        throw new InvalidCastException(
                            $"Unknown CausaExencion {sujeta.Exenta.CausaExencion}");

                    CausaExencion = causaExencion;
                    GrossAmount = Convert.ToDecimal(sujeta.Exenta.BaseImponible, Settings.DefaultNumberFormatInfo);

                }
            }

        }

        /// <summary>
        /// Elimina el objeto SII subyacente.
        /// </summary>
        public void ClearInnerSII()
        {
            InnerSII = null;
        }

        /// <summary>
        /// Obtiene un objeto RegistroLRFacturasEmitidas, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
        /// <param name="updateInnerSII">Si es true, actualiza el objeto SII subyacente
        /// con el valor calculado.</param>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLRFacturasEmitidas.</returns>
        public RegistroLRFacturasEmitidas ToSII(bool updateInnerSII = false, bool skipErrors = false)
        {

            if (InnerSII != null)
                return InnerSII;

            RegistroLRFacturasEmitidas siiInvoice = new RegistroLRFacturasEmitidas();

            if (IssueDate == null && !skipErrors)
                throw new ArgumentNullException("IssueDate is null.");

            if (!string.IsNullOrEmpty(ExternalReference) &&
                 !(Settings.Current.IDVersionSii.CompareTo("1.1") < 0))
                siiInvoice.FacturaExpedida.RefExterna = ExternalReference;

            DateTime? periodDate = OperationIssueDate ?? IssueDate; 

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiInvoice.PeriodoImpositivo.Ejercicio = (periodDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiInvoice.PeriodoImpositivo.Periodo = (periodDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiInvoice.PeriodoLiquidacion.Ejercicio = (periodDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiInvoice.PeriodoLiquidacion.Periodo = (periodDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            if (SellerParty == null && !skipErrors)
                throw new ArgumentNullException("SellerParty is null.");

            siiInvoice.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
            siiInvoice.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;

            if (InvoiceType == InvoiceType.F4)
                siiInvoice.IDFactura.NumSerieFacturaEmisorResumenFin = InvoiceNumberLastItem;


            siiInvoice.IDFactura.FechaExpedicionFacturaEmisor = SIIParser.FromDate(IssueDate);

            siiInvoice.FacturaExpedida.TipoFactura = InvoiceType.ToString();
            siiInvoice.FacturaExpedida.ClaveRegimenEspecialOTrascendencia =
                ((int)ClaveRegimenEspecialOTrascendencia).ToString().PadLeft(2, '0');

            siiInvoice.FacturaExpedida.ImporteTotal = SIIParser.FromDecimal(GrossAmount);

            if (!(Settings.Current.IDVersionSii.CompareTo("1.1") < 0)) 
                if (GrossAmount > UpperLimit)
                    siiInvoice.FacturaExpedida.Macrodato = "S";

            siiInvoice.FacturaExpedida.DescripcionOperacion = InvoiceText;

            if (InvoiceType == InvoiceType.R1 ||
                InvoiceType == InvoiceType.R2 ||
                InvoiceType == InvoiceType.R3 ||
                InvoiceType == InvoiceType.R4)
            {
                if (InvoicesRectified.Count == 1)
                {
                    OperationIssueDate = InvoicesRectified[0].RectifiedIssueDate;
                }
                else if (InvoicesRectified.Count > 1)
                {
                    /* FAQ_version_10.pdf
					2.14. ¿Qué fecha de operación debe constar en una factura rectificativa si se rectifican varias 
					facturas mediante un único documento de rectificación?
					Se consignará el último día del mes natural en el que se registró la última factura rectificada 
					(la de fecha más antigua).	 
					 */
                    DateTime maxDate = new DateTime(1, 1, 1);
                    foreach (var invoiceRectified in InvoicesRectified)
                        if (invoiceRectified.RectifiedIssueDate > maxDate)
                            maxDate = invoiceRectified.RectifiedIssueDate ?? new DateTime(1, 1, 1);

                    int curMonth, nextMonth;
                    curMonth = nextMonth = maxDate.Month;

                    DateTime nextDate = maxDate;

                    while (curMonth == nextMonth)
                    {
                        maxDate = nextDate;
                        nextDate = nextDate.AddDays(1);
                        nextMonth = nextDate.Month;
                    }

                    OperationIssueDate = maxDate;

                }
            }


            if (OperationIssueDate != null)
                siiInvoice.FacturaExpedida.FechaOperacion = SIIParser.FromDate(OperationIssueDate);


            siiInvoice.FacturaExpedida.Contraparte = GetContraparte(skipErrors);

            //// Gestionar los asuntos relacionados con el NIF del comprador
            //// Si no es nacional o comienza con 'N' en lugar de DesgloseFactura 
            //// hay que informar DesgloseTipoOperacion

            TaxIdEs taxIdEs = null;

            // Bandera que indica si el NIF es o no es español

            bool IsNotNifES = false;


            try
            {
                taxIdEs =
                          new TaxIdEs(BuyerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;

            // Tratamiento de las facturas rectificativas.

            if (InvoicesRectified.Count != 0)
            {

                siiInvoice.FacturaExpedida.TipoRectificativa = RectifiedType.ToString();

                if (RectifiedBase != 0)
                {
                    ImporteRectificacion importeRectifica = new ImporteRectificacion();
                    importeRectifica.BaseRectificada = SIIParser.FromDecimal(RectifiedBase);
                    importeRectifica.CuotaRectificada = SIIParser.FromDecimal(RectifiedAmount);
                    importeRectifica.CuotaRecargoRectificado = SIIParser.FromDecimal(RectifiedEquivCharge);
                    siiInvoice.FacturaExpedida.ImporteRectificacion = importeRectifica;
                }

                siiInvoice.FacturaExpedida.FacturasRectificadas = new List<IDFactura>();

                foreach (var regRect in InvoicesRectified)
                {
                    IDFactura FactRectificada = new IDFactura();

                    FactRectificada.NumSerieFacturaEmisor = regRect.RectifiedInvoiceNumber;
                    FactRectificada.FechaExpedicionFacturaEmisor = SIIParser.FromDate(regRect.RectifiedIssueDate);
                    // En este caso pongo a null IDEmisorFactura para que no serialice una etiqueta vacía.
                    FactRectificada.IDEmisorFactura = null;

                    siiInvoice.FacturaExpedida.FacturasRectificadas.Add(FactRectificada);
                }

            }
            else
            {
                if (InvoiceType.ToString().StartsWith("R"))
                    siiInvoice.FacturaExpedida.TipoRectificativa = RectifiedType.ToString();
            }

            // Gestión desglose factura
            if (InvoiceType == InvoiceType.F4 ||
                InvoiceType == InvoiceType.F2 ||
                InvoiceType == InvoiceType.R5)
                siiInvoice.FacturaExpedida.TipoDesglose.DesgloseFactura = GetDesgloseFactura();
            else if (taxIdEs == null ||
                    !taxIdEs.IsDCOK ||
                    BuyerParty.TaxIdentificationNumber.StartsWith("N"))
                siiInvoice.FacturaExpedida.TipoDesglose.DesgloseTipoOperacion = GetDesgloseTipoOperacion();
            else
                siiInvoice.FacturaExpedida.TipoDesglose.DesgloseFactura = GetDesgloseFactura();


            // Alquileres que requieren información adicional
            if (ClaveRegimenEspecialOTrascendencia == ClaveRegimenEspecialOTrascendencia.ArrendamientoLocalNegocioNoRetencion ||
                ClaveRegimenEspecialOTrascendencia == ClaveRegimenEspecialOTrascendencia.ArrendamientoLocalNegocioAmbosImportacionesNoDua)
            {
                // Obligatorios los datos de inmueble en este caso.
                if (BuildingState == BuildingState.SinValor)
                    throw new ArgumentNullException($"For ClaveRegimenEspecialOTrascendencia={ClaveRegimenEspecialOTrascendencia} BuildingState is required.");

                if (string.IsNullOrEmpty(BuildingReference))
                    throw new ArgumentNullException($"For ClaveRegimenEspecialOTrascendencia={ClaveRegimenEspecialOTrascendencia} BuildingReference is required.");

                siiInvoice.FacturaExpedida.DatosInmueble = new DatInmueble();

                siiInvoice.FacturaExpedida.DatosInmueble.DetalleInmueble.SituacionInmueble = ((int)BuildingState).ToString();
                siiInvoice.FacturaExpedida.DatosInmueble.DetalleInmueble.ReferenciaCatastral = BuildingReference;

            }



            if (updateInnerSII)
                InnerSII = siiInvoice;

            return siiInvoice;

        }


        /// <summary>
        /// Devuelve un objeto Contraparte del SII mediante los datos de la
        /// factura actual.
        /// </summary>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns> Contraparte para el SII</returns>
        internal Contraparte GetContraparte(bool skipErrors = false)
        {

            Contraparte contraparte = new Contraparte();
            // Gestionar los asuntos relacionados con el NIF del comprador
            // Si no es nacional o comienza con 'N' en lugar de DesgloseFactura 
            // hay que informar DesgloseTipoOperacion

            TaxIdEs taxIdEs = null;

            // Solo funciona si no se ha cambiado la causa por defecto
            if (CausaExencion == CausaExencion.E1)
                if (IDOtroType == IDOtroType.NifIva) // Establece la causa de excepción para operaciones UE.
                    CausaExencion = CausaExencion.E5;

            // Si no es una factura simplificada o asiento resumen
            if (InvoiceType != InvoiceType.F4 &&
                InvoiceType != InvoiceType.F2 &&
                InvoiceType != InvoiceType.R5)
            {

                // Bandera que indica si el NIF es o no es español

                bool IsNotNifES = false;


                try
                {
                    taxIdEs =
                              new TaxIdEs(BuyerParty.TaxIdentificationNumber);
                }
                catch
                {
                    IsNotNifES = true;
                }

                if (taxIdEs != null)
                    IsNotNifES = !taxIdEs.IsDCOK;


                if (BuyerParty != null)
                {

                    contraparte.NombreRazon = BuyerParty.PartyName;

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

                        if (!string.IsNullOrEmpty(BuyerParty.TaxIdentificationNumber))
                            contraparte.IDOtro.ID = BuyerParty.TaxIdentificationNumber;

                        /*http://www.agenciatributaria.es/static_files/AEAT/Contenidos_Comunes/La_Agencia_Tributaria/Informacion_institucional/Campanias/Suministro_Inmediato_Informacion/FAQ_version_10.pdf
						
						! OJO DOC ANTIGUO !
						3.7. ¿Cómo se registra una Exportación?
						La operación se anota en el Libro Registro de Facturas Expedidas.
						En el campo “Clave Régimen especial o Trascendencia” se consignará el valor 15.
						Deberá identificarse al cliente – en caso de ser extranjero- mediante el “Código país” y
						las claves 3 “Pasaporte”, 4 “Documento oficial de identificación expedido por el país o
						territorio de residencia”, 5 “Certificado de residencia” ó 6 “Otro documento probatorio”
						del campo “IDType”.
						Por otra parte, la base imponible de la factura se incluirá en el campo de tipo de
						operación “Exenta” dentro del bloque “Entrega”. Como causa de exención se
						consignará la clave E2 “Exenta por el artículo 21”.	 
						*/

                        // Solo funciona si no se ha modificado la asignación de Exención por defecto
                        if (CausaExencion == CausaExencion.E1)
                            if (IDOtroType != IDOtroType.NifIva) // ni español ni CE (Exportación)
                                CausaExencion = CausaExencion.E2; // Establece la causa excepción

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

                            if (CountryCode == null || CountryCode != "ES" && !skipErrors)
                                throw new ArgumentNullException(
                                $"Invoice {InvoiceNumber}: For IDOtroType.NoCensado Country Code can't be null. Must be 'ES'");

                            contraparte.IDOtro.CodigoPais = CountryCode;

                            if (!string.IsNullOrEmpty(BuyerParty.TaxIdentificationNumber))
                                contraparte.IDOtro.ID = BuyerParty.TaxIdentificationNumber;

                        }
                        else
                        {
                            contraparte.NIF = BuyerParty.TaxIdentificationNumber;
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
        /// Para NIF nacioanales no comenzados en 'N'.
        /// </summary>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Desglose de factura.</returns>
        internal virtual DesgloseFactura GetDesgloseFactura(bool skipErrors = false)
        {

            DesgloseFactura desgloseFactura = new DesgloseFactura();

            if (ClaveRegimenEspecialOTrascendencia == ClaveRegimenEspecialOTrascendencia.IpsiIgic ||
                IsTaxExcluded)
                desgloseFactura.NoSujeta = GetNoSujeta(skipErrors);
            else
                desgloseFactura.Sujeta = GetSujeta(skipErrors);

            return desgloseFactura;

        }

        /// <summary>
        /// Devuelve el cuerpo de una operación sujeta. Falta añadir funcionalidad
        /// para casos en los que convivan en una misma factura líneas exentas de 
        /// por causas distintas, que convivan parte exenta con parte no exenta.
        /// </summary>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Operación sujeta correspondiente a la factura.</returns>
        internal Sujeta GetSujeta(bool skipErrors = false)
        {

            Sujeta sujeta = new Sujeta();

            if (TaxesOutputs.Count > 0)
            {

                /* IMPLEMENTACIÓN PARA LÍNEAS CON MÚLTIPLES CASOS:
				 * Utilizamos tipos negativos para indicar que se trata de líneas exentas,
				 * el tipo negativo será el valor del que obtendremos la CausaExencion
				 *  */

                // Anulo inicialización por defecto.
                sujeta.NoExenta = null;

                foreach (KeyValuePair<decimal, decimal[]> taxOut in TaxesOutputs)
                {
                    // Primero debe ir siempre el bloque de exenta
                    if (taxOut.Key < 0)
                    {

                        // Operación exenta
                        Exenta exenta = new Exenta();

                        int causaIn = -Convert.ToInt32(taxOut.Key);
                        CausaExencion causaExencion = (CausaExencion)causaIn;

                        exenta.CausaExencion = causaExencion.ToString();
                        exenta.BaseImponible = SIIParser.FromDecimal(taxOut.Value[0]);

                        if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                        {
                            sujeta.Exenta = exenta;
                        }
                        else
                        {

                            if(sujeta.Exenta == null)
                                sujeta.Exenta = new Exenta()
                                {
                                    DetalleExenta = new List<Exenta>()
                                };

                            sujeta.Exenta.DetalleExenta.Add(exenta);                          

                        }

                    }
                    else if (taxOut.Key == 0)
                    {
                        // Operación exenta (exención por defecto E1)
                        Exenta exenta = new Exenta();

                        exenta.CausaExencion = CausaExencion.E1.ToString();
                        exenta.BaseImponible = SIIParser.FromDecimal(taxOut.Value[0]);

                        if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                            sujeta.Exenta = exenta;
                        else
                            sujeta.Exenta = new Exenta()
                            {
                                DetalleExenta = new List<Exenta>()
                                {
                                    exenta
                                }
                            };
                    }
                }

                foreach (KeyValuePair<decimal, decimal[]> taxOut in TaxesOutputs)
                {
                    if (taxOut.Key > 0)
                    {

                        // Operación no exenta (TIPO IVA > 0)
                        if (sujeta.NoExenta == null)
                        {
                            sujeta.NoExenta = new NoExenta();
                            sujeta.NoExenta.TipoNoExenta = Sujeta.ToString();
                        }

                        DetalleIVA detalleIVA = new DetalleIVA()
                        {
                            TipoImpositivo = (taxOut.Value[2] == 0) ? SIIParser.FromDecimal(taxOut.Key) : "0",
                            BaseImponible = SIIParser.FromDecimal(taxOut.Value[0]),
                            CuotaRepercutida = SIIParser.FromDecimal(taxOut.Value[1])
                        };

                        if ((taxOut.Value[2] != 0))
                        {
                            detalleIVA.TipoRecargoEquivalencia = SIIParser.FromDecimal(taxOut.Key);
                            detalleIVA.CuotaRecargoEquivalencia = SIIParser.FromDecimal(taxOut.Value[2]);
                        }

                        sujeta.NoExenta.DesgloseIVA.DetalleIVA.Add(detalleIVA);

                    }
                }


            }
            else
            {

                // Anulo inicialización por defecto.
                sujeta.NoExenta = null;

                Exenta exenta = new Exenta();

                exenta.CausaExencion = CausaExencion.ToString();
                exenta.BaseImponible = SIIParser.FromDecimal(GrossAmount);

                if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                    sujeta.Exenta = exenta;
                else
                    sujeta.Exenta = new Exenta()
                    {
                        DetalleExenta = new List<Exenta>()
                        {
                            exenta
                        }
                    };

            }

            return sujeta;
        }

        /// <summary>
        /// Devuelve el cuerpo de una operación no sujeta.
        /// </summary>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Operación no sujeta correspondiente a la factura.</returns>
        internal NoSujeta GetNoSujeta(bool skipErrors = false)
        {

            NoSujeta noSujeta = new NoSujeta();

            if (IsTaxExcluded)
                noSujeta.ImportePorArticulos7_14_Otros = SIIParser.FromDecimal(GrossAmount);
            else
                noSujeta.ImporteTAIReglasLocalizacion = SIIParser.FromDecimal(GrossAmount);

            return noSujeta;

        }

        /// <summary>
        /// Devuelve objeto XML desglose tipo operación.
        /// </summary>
        /// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
        /// <returns>Objeto XML desglose tipo operación.</returns>
        internal DesgloseTipoOperacion GetDesgloseTipoOperacion(bool skipErrors = false)
        {
            DesgloseTipoOperacion desgloseTipoOperacion = new DesgloseTipoOperacion();

            Sujeta sujeta = GetSujeta();

            if (IsService)
            {

                desgloseTipoOperacion.PrestacionServicios = new PrestacionServicios();
                desgloseTipoOperacion.PrestacionServicios.Sujeta = sujeta;

                if (IDOtroType == IDOtroType.NifIva)
                    if (desgloseTipoOperacion.PrestacionServicios.Sujeta.Exenta != null)
                        if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                            desgloseTipoOperacion.PrestacionServicios.Sujeta.Exenta.CausaExencion = CausaExencion.ToString();
                        else
                            desgloseTipoOperacion.PrestacionServicios.Sujeta.Exenta.DetalleExenta[0].CausaExencion = CausaExencion.ToString();
            }
            else
            {

                desgloseTipoOperacion.Entrega = new Entrega();
                desgloseTipoOperacion.Entrega.Sujeta = sujeta;

                if (IDOtroType == IDOtroType.NifIva)
                    if (desgloseTipoOperacion.Entrega.Sujeta.Exenta != null)
                        if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                            desgloseTipoOperacion.Entrega.Sujeta.Exenta.CausaExencion = CausaExencion.ToString();
                        else
                            desgloseTipoOperacion.Entrega.Sujeta.Exenta.DetalleExenta[0].CausaExencion = CausaExencion.ToString();

            }

            return desgloseTipoOperacion;
        }


        /// <summary>
        /// Devuelve un objeto XML con un filtro consulta de facturas emitidas.
        /// </summary>
        /// <returns>Objeto XML con un filtro consulta de facturas emitidas.</returns>
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

            if (SellerParty != null)
            {

                if (siiFilter.IDFactura == null)
                    siiFilter.IDFactura = new IDFactura();

                siiFilter.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;

            }

            if (InvoiceNumber != null && IssueDate != null)
            {

                if (siiFilter.IDFactura == null)
                    siiFilter.IDFactura = new IDFactura();

                siiFilter.IDFactura.IDEmisorFactura = null;

                siiFilter.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;

                siiFilter.IDFactura.FechaExpedicionFacturaEmisor =
                SIIParser.FromDate(IssueDate);

            }

            if (BuyerParty != null)
                siiFilter.Contraparte = GetContraparte();

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
        internal ConsultaFactInformadasCliente ToFilterExternSII()
        {

            ConsultaFactInformadasCliente siiFilter = new ConsultaFactInformadasCliente();

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");           

            siiFilter.FiltroConsulta.PeriodoImputacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
            siiFilter.FiltroConsulta.PeriodoImputacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");  

            if (BuyerParty != null)
                siiFilter.FiltroConsulta.Cliente = GetContraparte();

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
        internal RegistroLRCobros ToPaymentsSII()
        {

            RegistroLRCobros registroLRCobros = new RegistroLRCobros();

            registroLRCobros.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
            registroLRCobros.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;
            registroLRCobros.IDFactura.FechaExpedicionFacturaEmisor = SIIParser.FromDate(IssueDate);

            foreach (var payment in ARInvoicePayments)
            {

                if (payment.PaymentDate == null)
                    throw new ArgumentNullException("PaymentDate is null.");

                Cobro cobro = new Cobro()
                {
                    Fecha = SIIParser.FromDate(payment.PaymentDate),
                    Medio = ((int)payment.PaymentTerm).ToString().PadLeft(2, '0'),
                    Importe = SIIParser.FromDecimal(payment.PaymentAmount),
                    Cuenta_O_Medio = payment.AccountOrTermsText
                };

                registroLRCobros.Cobros.Add(cobro);
            }

            return registroLRCobros;
        }

        /// <summary>
        /// Crea un filtro de cobros relacionados con la factura
        /// utilizando como filtro los datos de la misma.
        /// </summary>
        /// <returns>Objeto XML de filtro para consulta de cobros de factura.</returns>
        internal FiltroConsultaCobrosPagos ToFilterCobrosSII()
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
                        SIIParser.FromDate(IssueDate);
            }

            return siiFilter;

        }

        /// <summary>
        /// Genero un objeto XML de baja de factura.
        /// </summary>
        /// <returns>Objeto XML de baja de factura.</returns>
        internal RegistroLRBajaExpedidas ToRegistroLRBajaExpedidasSII()
        {

            RegistroLRBajaExpedidas siiDelete = new RegistroLRBajaExpedidas();

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (InvoiceNumber == null)
                throw new ArgumentNullException("InvoiceNumber is null.");

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

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
                siiDelete.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;

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
                    return ToRegistroLRBajaExpedidasSII();
                case BatchActionKeys.PG:
                    return ToPaymentsSII();
            }

            throw new Exception($"Unknown BatchActionKey: {batchActionKey}");
        }

        /// <summary>
        /// Id. de la instancia.
        /// </summary>
        /// <returns>Id. de la instancia.</returns>
        public override string GetItemKey()
        {
            if(string.IsNullOrEmpty(InvoiceNumberLastItem))
                return base.GetItemKey();

            return base.GetItemKey() + "," + InvoiceNumberLastItem;
        }



    }
}