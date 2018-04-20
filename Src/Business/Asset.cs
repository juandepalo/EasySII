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
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;

namespace EasySII.Business
{
    /// <summary>
    /// Factura de bienes de inversión (Activo).
    /// </summary>
    public class Asset : Invoice, IBatchItem
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
                siiInvoice.PeriodoImpositivo.Periodo = "0A"; // anual
            }
            else
            {
                siiInvoice.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiInvoice.PeriodoLiquidacion.Periodo = "0A"; // anual
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
                siiDelete.PeriodoImpositivo.Periodo = "0A"; // anual
            }
            else
            {
                siiDelete.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiDelete.PeriodoImpositivo.Periodo = "0A"; // anual
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

            // Campos especificos para los bienes de inversión.

            siiDelete.IdentificacionBien = PropertyId;       

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
                    return ToSII();
                case BatchActionKeys.DR:
                    return ToRegistroLRBajaBienesInversionSII();
            }

            throw new Exception($"Unknown BatchActionKey: {batchActionKey}");
        }

    }
}
