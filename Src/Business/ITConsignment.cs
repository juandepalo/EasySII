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

namespace EasySII.Business
{
    /// <summary>
    /// Venta bien en consigna.
    /// </summary>
    public class ITConsignment : Invoice, IBatchItem
    {

        /// <summary>
        /// Representación calculada del objeto de negocio factura recibida (ITConsignment)
        /// como un objeto definido según las especificaciones del SII.
        /// </summary>
        public RegistroLRDetOperacionIntracomunitariaVentasEnConsigna InnerSII { get; private set; }


        /// <summary>
        /// Fecha recepción.
        /// </summary>
        public DateTime? ReceptionDate { get; set; }


        /// <summary>
        /// Cuando el identificador es distinto del NIF establece el tipo de identificador utilizado.
        /// </summary>
        public IDOtroType IDOtroType { get; set; }

        /// <summary>
        /// Código ISO del pais cuando el NIF no es español.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Clave de declarado venta consigna.
        /// </summary>
        public ClaveDeclarante ClaveDeclarante { get; set; }

        /// <summary>
        /// Descripción de Bienes
        /// </summary>
        public string WarehouseAddress { get; set; }

        /// <summary>
        /// Estado Miembro. Dependerá de la lista L18.
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Estado Miembro. Dependerá de la lista L18.
        /// </summary>
        public string EstadoMiembroPartida { get; set; }

        /// <summary>
        /// Estado Miembro. Dependerá de la lista L18.
        /// </summary>
        public string EstadoMiembroLlegada { get; set; }


        /// <summary>
        /// Descripción de Bienes
        /// </summary>
        public string DescripcionBienes { get; set; }

        /// <summary>
        /// Cantidad.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Constructor de ITInvoice.
        /// </summary>
        public ITConsignment()
        {
            IDOtroType = IDOtroType.OtroDocProbatorio;
        }

        /// <summary>
        /// Constructor de ITInvoice.
        /// </summary>
        /// <param name="registroLRDetOperacionIntracomunitariaVentasEnConsigna">Objeto serialización xml venta consigna.</param>
        public ITConsignment(RegistroLRDetOperacionIntracomunitariaVentasEnConsigna registroLRDetOperacionIntracomunitariaVentasEnConsigna)
        {

            RegistroLRDetOperacionIntracomunitariaVentasEnConsigna siiInvoice = registroLRDetOperacionIntracomunitariaVentasEnConsigna;

            InvoiceNumber = siiInvoice.IdRegistroDeclarado.IdRegistro;
            OperationType = siiInvoice.TipoOperacion;
            IssueDate = Convert.ToDateTime(siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.FechaExpedicion);
            ReceptionDate = Convert.ToDateTime(siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.FechaLlegada);
            Quantity = Convert.ToInt32(siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.Cantidad);
            GrossAmount = SIIParser.ToDecimal(siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.ValorBienes);
            WarehouseAddress = siiInvoice.Deposito.DireccionAlmacen;

            // Tratamiento del BuyerParty

            BuyerParty = new Party()
            {
                TaxIdentificationNumber = siiInvoice.Contraparte.NIF,
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


            // Tratamiento del BuyerParty

            BuyerParty = new Party()
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

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");


            ClaveDeclarante claveDeclarante;

            if (!Enum.TryParse<ClaveDeclarante>(siiInvoice.ClaveDeclarante, out claveDeclarante))
                throw new InvalidOperationException($"Unknown clave declarado {siiInvoice.ClaveDeclarante}");

            ClaveDeclarante = claveDeclarante;

            EstadoMiembroPartida = siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.EmPartida;
            EstadoMiembroLlegada = siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.EmLlegada;
            DescripcionBienes = siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.DescripBienes;
            
        }

        /// <summary>
        /// Obtiene un objeto RegistroLRDetOperacionIntracomunitaria, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
		/// <param name="updateInnerSII">Si es true, actualiza el objeto SII subyacente
		/// con el valor calculado.</param>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLRDetOperacionIntracomunitaria.</returns>
        internal RegistroLRDetOperacionIntracomunitariaVentasEnConsigna ToSII(bool updateInnerSII = false)
        {

            if (InnerSII != null)
                return InnerSII;


            RegistroLRDetOperacionIntracomunitariaVentasEnConsigna siiInvoice = new RegistroLRDetOperacionIntracomunitariaVentasEnConsigna();

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

            if (!string.IsNullOrEmpty(ExternalReference) &&
                !(Settings.Current.IDVersionSii.CompareTo("1.1") < 0))
                siiInvoice.OperacionIntracomunitaria.RefExterna = ExternalReference;

            siiInvoice.IdRegistroDeclarado = new IdRegistroDeclarado() 
            {
                Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy"),
                Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM")
            };

            siiInvoice.TipoOperacion = OperationType;

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

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

                siiInvoice.Contraparte = new Contraparte() 
                { 
                    NombreRazon = BuyerParty.PartyName 
                };

                if (IsNotNifES)
                {

                    if (CountryCode == null && IDOtroType != IDOtroType.NifIva)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    // Si no es un nif español
                    siiInvoice.Contraparte.IDOtro = new IDOtro();
                    siiInvoice.Contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiInvoice.Contraparte.IDOtro.CodigoPais = CountryCode;
                    siiInvoice.Contraparte.IDOtro.ID = BuyerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiInvoice.Contraparte.NIF = BuyerParty.TaxIdentificationNumber;
                }
            }

            siiInvoice.IdRegistroDeclarado.IdRegistro = InvoiceNumber;

            siiInvoice.OperacionIntracomunitaria= new OperacionIntracomunitariaVentasEnConsigna()
            {
                InfoExpedicionRecepcion = new InfoExpedicionRecepcion()
                {
                    FechaExpedicion = (IssueDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy"),
                    FechaLlegada = (ReceptionDate ?? new DateTime(1, 1, 1)).ToString("dd-MM-yyyy")
                }
            };
            
            // Tratamos el resto de información de la factura intracomunitaria.
            
            siiInvoice.ClaveDeclarante = ClaveDeclarante.ToString();
            siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.EmPartida = EstadoMiembroPartida;
            siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.EmLlegada = EstadoMiembroLlegada;
            siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.DescripBienes = DescripcionBienes;
            siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.Cantidad = $"{Quantity}";
            siiInvoice.OperacionIntracomunitaria.InfoExpedicionRecepcion.ValorBienes = SIIParser.FromDecimal(GrossAmount);
            siiInvoice.Deposito = new Deposito()
            {
                DireccionAlmacen = WarehouseAddress
            };

            if (updateInnerSII)
                InnerSII = siiInvoice;

            return siiInvoice;

        }


        internal FiltroConsulta ToFilterSII()
        {

            throw new NotImplementedException("No implementado ToFilterSII para ITConsignment.");

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
                    return ToSII(updateInnerSII);
                //case BatchActionKeys.DR:
                    //return ToRegistroLRBajaRecibidasSII();
            }

            throw new Exception($"Unknown BatchActionKey: {batchActionKey}");
        }

    }
}
