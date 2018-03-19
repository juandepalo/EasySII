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

using EasySII.Tax;
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;

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
