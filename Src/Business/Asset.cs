using EasySII.Tax;
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EasySII.Business
{
    /// <summary>
    /// Factura de bienes de inversión (Activo).
    /// </summary>
    public class Asset : Invoice
    {

        /// <summary>
        /// Cuando el identificador es distinto del NIF establece el tipo de identificador utilizado.
        /// </summary>
        public IDOtroType IDOtroType { get; set; }

        /// <summary>
        /// Código ISO del pais cuando el NIF no es español.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Descripción de los bienes objeto de la operación.
        /// </summary>
        public string PropertyId { get; set; }

        /// <summary>
        /// Fecha de inicio de utilización del mismo.
        /// </summary>
        public DateTime? InitialDate { get; set; }

        /// <summary>
        /// Prorrata Anual Definitiva.
        /// </summary>
        public decimal ProrrataAnual { get; set; }

        /// <summary>
        /// Regularizacion Anual Deducción.
        /// </summary>
        public decimal RegAnualDeduc { get; set; }

        /// <summary>
        /// Identificación de la entrega.
        /// </summary>
        public string DeliveryId { get; set; }

        /// <summary>
        /// Regularizacion Deducción Efectuada.
        /// </summary>
        public decimal RegDeducEfec { get; set; }

        /// <summary>
        /// Constructor de APInvoice.
        /// </summary>
        public Asset()
        {
            IDOtroType = IDOtroType.OtroDocProbatorio;
        }

        /// <summary>
        /// Constructor de InvestmentProperty.
        /// </summary>
        /// <param name="registroLRBienesInversion">Objeto serialización xml Bienes Inversión.</param>
        public Asset(RegistroLRBienesInversion registroLRBienesInversion)
        {

            RegistroLRBienesInversion siiInvoice = registroLRBienesInversion;

            InvoiceNumber = siiInvoice.IDFactura.NumSerieFacturaEmisor;
            IssueDate = Convert.ToDateTime(siiInvoice.IDFactura.FechaExpedicionFacturaEmisor);

            SellerParty = new Party()
            {
                TaxIdentificationNumber = siiInvoice.IDFactura.IDEmisorFactura.NIF,
                PartyName = siiInvoice.IDFactura.IDEmisorFactura.NombreRazon
            };

            if (siiInvoice.IDFactura.IDEmisorFactura.IDOtro != null)
            {
                // Si no es un nif español
                IDOtroType =
                (IDOtroType)Convert.ToInt32(siiInvoice.IDFactura.IDEmisorFactura.IDOtro.IDType);

                CountryCode = siiInvoice.IDFactura.IDEmisorFactura.IDOtro.CodigoPais;

                SellerParty.TaxIdentificationNumber = siiInvoice.IDFactura.IDEmisorFactura.IDOtro.ID;
            }


            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");


            PropertyId = siiInvoice.BienesInversion.IdentificacionBien;
            InitialDate = Convert.ToDateTime(siiInvoice.BienesInversion.FechaInicioUtilizacion);
            ProrrataAnual = Convert.ToDecimal(siiInvoice.BienesInversion.ProrrataAnualDefinitiva, Settings.DefaultNumberFormatInfo);
            RegAnualDeduc = Convert.ToDecimal(siiInvoice.BienesInversion.RegularizacionAnualDeduccion, Settings.DefaultNumberFormatInfo);
            DeliveryId = siiInvoice.BienesInversion.IndentificacionEntrega;
            RegDeducEfec = Convert.ToDecimal(siiInvoice.BienesInversion.RegularizacionDeduccionEfectuada, Settings.DefaultNumberFormatInfo);

        }

        /// <summary>
        /// Obtiene un objeto RegistroLRBienesInversion, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLRBienesInversion.</returns>
        public RegistroLRBienesInversion ToSII()
        {

            RegistroLRBienesInversion siiInvoice = new RegistroLRBienesInversion();

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");


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

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            siiInvoice.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;
            siiInvoice.IDFactura.FechaExpedicionFacturaEmisor = (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");

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

                siiInvoice.IDFactura.IDEmisorFactura.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiInvoice.IDFactura.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;           
                }
            }

            // Campos especificos para los bienes de inversión.

            siiInvoice.BienesInversion.IdentificacionBien = PropertyId;
            siiInvoice.BienesInversion.FechaInicioUtilizacion = (InitialDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy"); ;
            siiInvoice.BienesInversion.ProrrataAnualDefinitiva = ProrrataAnual.ToString(Settings.DefaultNumberFormatInfo);
            siiInvoice.BienesInversion.RegularizacionAnualDeduccion = RegAnualDeduc.ToString(Settings.DefaultNumberFormatInfo);
            siiInvoice.BienesInversion.IndentificacionEntrega = DeliveryId;
            siiInvoice.BienesInversion.RegularizacionDeduccionEfectuada = RegDeducEfec.ToString(Settings.DefaultNumberFormatInfo);

            return siiInvoice;

        }

        internal FiltroConsulta ToFilterSII()
        {

            FiltroConsulta siiFilter = new FiltroConsulta();

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

            if (SellerParty != null)
            {

                siiFilter.ClavePaginacion = new ClavePaginacion();
                siiFilter.ClavePaginacion.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro = new IDOtro();
                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiFilter.ClavePaginacion.IDEmisorFactura.NIF = SellerParty.TaxIdentificationNumber;
                }

                if (InvoiceNumber != null)
                    siiFilter.ClavePaginacion.NumSerieFacturaEmisor = InvoiceNumber;

                if (IssueDate != null)
                    siiFilter.ClavePaginacion.FechaExpedicionFacturaEmisor =
                        (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");
            }

            return siiFilter;

        }


        internal RegistroLRBajaBienesInversion ToRegistroLRBajaBienesInversionSII()
        {

            RegistroLRBajaBienesInversion siiDelete = new RegistroLRBajaBienesInversion();

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (InvoiceNumber == null)
                throw new ArgumentNullException("InvoiceNumber is null.");

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
                        (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");

            }

            return siiDelete;

        }

    }
}
