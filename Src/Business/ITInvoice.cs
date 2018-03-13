using EasySII.Tax;
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EasySII.Business
{
    /// <summary>
    /// Operacion Intracomunitaria (Intra-Community Transaction)
    /// </summary>
    public class ITInvoice : Invoice
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
        /// Tipo de Operación Intracomunitaria.
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Clave de declarado intracomunitario.
        /// </summary>
        public ClaveDeclarado ClaveDeclarado { get; set; }

        /// <summary>
        /// Estado Miembro. Dependerá de la lista L18.
        /// </summary>
        public string EstadoMiembro { get; set; }

        /// <summary>
        /// Descripción de Bienes
        /// </summary>
        public string DescripcionBienes { get; set; }

        /// <summary>
        /// Dirección Operador.
        /// </summary>
        public string DireccionOperador { get; set; }

        /// <summary>
        /// Constructor de ITInvoice.
        /// </summary>
        public ITInvoice()
        {
            IDOtroType = IDOtroType.OtroDocProbatorio;
        }

        /// <summary>
        /// Constructor de ITInvoice.
        /// </summary>
        /// <param name="registroLRDetOperacionIntracomunitaria">Objeto serialización xml operaciones intracomunitarias.</param>
        public ITInvoice(RegistroLRDetOperacionIntracomunitaria registroLRDetOperacionIntracomunitaria)
        {

            RegistroLRDetOperacionIntracomunitaria siiInvoice = registroLRDetOperacionIntracomunitaria;

            InvoiceNumber = siiInvoice.IDFactura.NumSerieFacturaEmisor;
            IssueDate = Convert.ToDateTime(siiInvoice.IDFactura.FechaExpedicionFacturaEmisor);

            //
            // Tratamiento del BuyerParty
            //
            BuyerParty = new Party()
            {
                TaxIdentificationNumber = siiInvoice.IDFactura.IDEmisorFactura.NIF,
                PartyName = siiInvoice.IDFactura.IDEmisorFactura.NombreRazon
            };

            if (siiInvoice.IDFactura.IDEmisorFactura.IDOtro != null)
            {

                // Si no es un nif español

                IDOtroType =
                (IDOtroType)Convert.ToInt32(
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.IDType);

                CountryCode = siiInvoice.IDFactura.IDEmisorFactura.IDOtro.CodigoPais;

            }

            //
            // Tratamiento del SellerParty
            //
            SellerParty = new Party()
            {
                TaxIdentificationNumber = (siiInvoice.Contraparte.IDOtro == null) ? null : siiInvoice.Contraparte.IDOtro.ID,
                PartyName = siiInvoice.Contraparte.NombreRazon
            };

            if (siiInvoice.Contraparte.IDOtro != null)
            {

                // Si no es un nif español

                IDOtroType =
                (IDOtroType)Convert.ToInt32(
                    siiInvoice.Contraparte.IDOtro.IDType);

                CountryCode = siiInvoice.Contraparte.IDOtro.CodigoPais;

            }

            if (BuyerParty == null)
                throw new ArgumentNullException("BuyerParty is null.");

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

            OperationType operationType;

            if (!Enum.TryParse<OperationType>(siiInvoice.OperacionIntracomunitaria.TipoOperacion, out operationType))
                throw new InvalidOperationException($"Unknown operation type {siiInvoice.OperacionIntracomunitaria.TipoOperacion}");

            OperationType = operationType;


            ClaveDeclarado claveDeclarado;

            if (!Enum.TryParse<ClaveDeclarado>(siiInvoice.OperacionIntracomunitaria.ClaveDeclarado, out claveDeclarado))
                throw new InvalidOperationException($"Unknown clave declarado {siiInvoice.OperacionIntracomunitaria.ClaveDeclarado}");

            ClaveDeclarado = claveDeclarado;

            EstadoMiembro = siiInvoice.OperacionIntracomunitaria.EstadoMiembro;
            DescripcionBienes = siiInvoice.OperacionIntracomunitaria.DescripcionBienes;
            DireccionOperador = siiInvoice.OperacionIntracomunitaria.DireccionOperador;
        }

        /// <summary>
        /// Obtiene un objeto RegistroLRDetOperacionIntracomunitaria, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLRDetOperacionIntracomunitaria.</returns>
        internal RegistroLRDetOperacionIntracomunitaria ToSII()
        {

            RegistroLRDetOperacionIntracomunitaria siiInvoice = new RegistroLRDetOperacionIntracomunitaria();

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

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (BuyerParty == null)
                throw new ArgumentNullException("BuyerParty is null.");

            // Se puede tratar de un Buyer extranjero, de manera que tendremos que poner el tratamiento correspondiente
            try
            {
                taxIdEs = new TaxIdEs(BuyerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;

            if (BuyerParty != null)
            {

                siiInvoice.IDFactura.IDEmisorFactura.NombreRazon = BuyerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    // Si no es un nif español
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiInvoice.IDFactura.IDEmisorFactura.IDOtro.ID = BuyerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiInvoice.IDFactura.IDEmisorFactura.NIF = BuyerParty.TaxIdentificationNumber;
                }
            }

            siiInvoice.IDFactura.NumSerieFacturaEmisor = InvoiceNumber;
            siiInvoice.IDFactura.FechaExpedicionFacturaEmisor = (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");

            // Se procede a tratar el Seller, el cual puede ser extranjero.
            taxIdEs = null;
            IsNotNifES = false;

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

                siiInvoice.Contraparte.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    // Si no es un nif español
                    siiInvoice.Contraparte.IDOtro = new IDOtro();
                    siiInvoice.Contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiInvoice.Contraparte.IDOtro.CodigoPais = CountryCode;
                    siiInvoice.Contraparte.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiInvoice.Contraparte.NIF = SellerParty.TaxIdentificationNumber;
                }
            }

            //
            // Tratamos el resto de información de la factura intracomunitaria.
            //
            siiInvoice.OperacionIntracomunitaria.TipoOperacion = OperationType.ToString();
            siiInvoice.OperacionIntracomunitaria.ClaveDeclarado = ClaveDeclarado.ToString();
            siiInvoice.OperacionIntracomunitaria.EstadoMiembro = EstadoMiembro;
            siiInvoice.OperacionIntracomunitaria.DescripcionBienes = DescripcionBienes;
            siiInvoice.OperacionIntracomunitaria.DireccionOperador = DireccionOperador;

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
                taxIdEs = new TaxIdEs(BuyerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;

            if (BuyerParty != null)
            {

                siiFilter.ClavePaginacion = new ClavePaginacion();
                siiFilter.ClavePaginacion.IDEmisorFactura.NombreRazon = BuyerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro = new IDOtro();
                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiFilter.ClavePaginacion.IDEmisorFactura.IDOtro.ID = BuyerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiFilter.ClavePaginacion.IDEmisorFactura.NIF = BuyerParty.TaxIdentificationNumber;
                }

                if (InvoiceNumber != null)
                    siiFilter.ClavePaginacion.NumSerieFacturaEmisor = InvoiceNumber;

                if (IssueDate != null)
                    siiFilter.ClavePaginacion.FechaExpedicionFacturaEmisor =
                        (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");

            }

            return siiFilter;

        }

        internal RegistroLRBajaDetOperacionIntracomunitaria ToRegistroLRBajaOperIntracomSII()
        {

            RegistroLRBajaDetOperacionIntracomunitaria siiDelete = new RegistroLRBajaDetOperacionIntracomunitaria();

            if (BuyerParty == null)
                throw new ArgumentNullException("BuyerParty is null.");

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
                taxIdEs = new TaxIdEs(BuyerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;


            if (BuyerParty != null)
            {

                siiDelete.IDFactura = new IDFactura();
                siiDelete.IDFactura.IDEmisorFactura.NombreRazon =BuyerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiDelete.IDFactura.IDEmisorFactura.IDOtro = new IDOtro();
                    siiDelete.IDFactura.IDEmisorFactura.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiDelete.IDFactura.IDEmisorFactura.IDOtro.CodigoPais = CountryCode;
                    siiDelete.IDFactura.IDEmisorFactura.IDOtro.ID = BuyerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiDelete.IDFactura.IDEmisorFactura.NIF = BuyerParty.TaxIdentificationNumber;
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
