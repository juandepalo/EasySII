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

using EasySII.Net;
using EasySII.Xml.Sii;
using EasySII.Xml.SiiR;
using EasySII.Xml.Soap;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace EasySII.Business.Queries
{

    /// <summary>
    /// Consulta de facturas clientes y proveedores.
    /// </summary>
    public class QueryAeatExtern
    {     


        /// <summary>
        /// Devuelve las facturas emitidas registradas
        /// por los clientes del titular
        /// del lote en el periodo indicado.
        /// </summary>
        /// <param name="period">Periodo en formato yyyy.MM.</param>
        /// <param name="titular">Titular de las facturas a recuperar.</param>
        /// <param name="cliente">Cliente del que recuperar las facturas.</param>
        /// <returns>Lista de facturas emitidas en el periodo.</returns>
        private static RespuestaConsultaLRFactInformadasCliente GetByPeriodFE(string period, Party titular, Party cliente)
        {

            ARInvoicesQuery batchInvoiceQuery = new ARInvoicesQuery()
            {
                Titular = titular
            };

            ARInvoice templateInvoiceQuery = new ARInvoice();

            templateInvoiceQuery.BuyerParty = cliente;

            string syear = period.Substring(0, 4);
            string smonth = period.Substring(5, 2);

            int year = Convert.ToInt32(syear);
            int month = Convert.ToInt32(smonth);

            DateTime date = new DateTime(year, month, 1);

            // Necesitamos indicar una fecha de factura, para que se pueda calcular el ejercicio y periodo
            // que son necesarios y obligatorios para realizar esta peticiones.
            templateInvoiceQuery.IssueDate = date;
            batchInvoiceQuery.ARInvoice = templateInvoiceQuery;

            string response = Wsd.GetFacturasEmitidasCliente(batchInvoiceQuery);

            try
            {
                // Obtengo la respuesta de la consulta de facturas recibidas del archivo de respuesta de la AEAT.
                Envelope envelope = Envelope.FromXml(response);

                if (envelope.Body.RespuestaConsultaLRFactInformadasCliente != null)
                    return envelope.Body.RespuestaConsultaLRFactInformadasCliente;

                SoapFault msgError = envelope.Body.RespuestaError;

                if (msgError != null)
                    throw new Exception($"{msgError.FaultDescription}");

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;

        }

        /// <summary>
        /// Devuelve las facturas emitidas registradas
        /// por los clientes del titular
        /// del lote en el periodo indicado.
        /// </summary>
        /// <param name="period">Periodo en formato yyyy.MM.</param>
        /// <param name="titular">Titular de las facturas a recuperar.</param>
        /// <param name="proveedor">Proveedor del que recuperar las facturas.</param>
        /// <returns>Lista de facturas emitidas en el periodo.</returns>
        private static RespuestaConsultaLRFactInformadasProveedor GetByPeriodFR(string period, Party titular, Party proveedor)
        {

            APInvoicesQuery batchInvoiceQuery = new APInvoicesQuery()
            {
                Titular = titular
            };

            APInvoice templateInvoiceQuery = new APInvoice();

            templateInvoiceQuery.SellerParty = proveedor;

            string syear = period.Substring(0, 4);
            string smonth = period.Substring(5, 2);

            int year = Convert.ToInt32(syear);
            int month = Convert.ToInt32(smonth);

            DateTime date = new DateTime(year, month, 1);

            // Necesitamos indicar una fecha de factura, para que se pueda calcular el ejercicio y periodo
            // que son necesarios y obligatorios para realizar esta peticiones.
            templateInvoiceQuery.IssueDate = date;
            batchInvoiceQuery.APInvoice = templateInvoiceQuery;

            string response = Wsd.GetFacturasRecibidasProveedor(batchInvoiceQuery);

            try
            {
                // Obtengo la respuesta de la consulta de facturas recibidas del archivo de respuesta de la AEAT.
                Envelope envelope = Envelope.FromXml(response);

                if (envelope.Body.RegistroRespuestaConsultaLRFactInformadasProveedor != null)
                    return envelope.Body.RegistroRespuestaConsultaLRFactInformadasProveedor;

                SoapFault msgError = envelope.Body.RespuestaError;

                if (msgError != null)
                    throw new Exception($"{msgError.FaultDescription}");

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;

        }


        /// <summary>
        /// Recupera el libro registro del IVA para los
        /// periodos indicados entre el inicial y el final.
        /// </summary>
        /// <param name="periodStart">Periodo inicial</param>
        /// <param name="periodEnd">Periodo final.</param>
        /// <param name="titular">Titular del lote.</param>
        /// <param name="cliente">Cliente.</param> 
        public static DataTable GetCustomerInvoices(string periodStart, string periodEnd, 
            Party titular, Party cliente)
        {
            

            string syearStart = periodStart.Substring(0, 4);
            string smonthStart = periodStart.Substring(5, 2);

            string syearEnd = periodEnd.Substring(0, 4);
            string smonthEnd = periodEnd.Substring(5, 2);

            int yearStart = Convert.ToInt32(syearStart);
            int monthStart = Convert.ToInt32(smonthStart);

            int yearEnd = Convert.ToInt32(syearEnd);
            int monthEnd = Convert.ToInt32(smonthEnd);

            DateTime start = new DateTime(yearStart, monthStart, 1);
            DateTime end = new DateTime(yearEnd, monthEnd, 2);

            if (start > end)
                throw new Exception($"El periodo inicial '{periodStart}'" +
                    $" no puede ser mayor que el final '{periodEnd}'.");

            var tableResult = GetTableLR();

            while (start < end)
            {
                var respuestasFE = GetByPeriodFE(periodStart, titular, cliente);
                DataTable tableFR = GetTableLR();

                if (respuestasFE.RegistroRespuestaConsultaFactInformadasCliente != null)
                {
                    foreach (var invoice in respuestasFE.RegistroRespuestaConsultaFactInformadasCliente)
                    {
                        foreach (var iva in invoice.DatosFacturaInformadaCliente.DesgloseFactura.DesgloseIVA.DetalleIVA)
                        {
                            tableFR.Rows.Add("FE",                                                                      // 0
                            invoice.IDFactura.NumSerieFacturaEmisor,                                                    // 1
                            invoice.IDFactura.FechaExpedicionFacturaEmisor,                                             // 2
                            $"{invoice.Cliente.NIF}{invoice.Cliente?.IDOtro?.ID}",                                      // 3
                            invoice.Cliente.NombreRazon,                                                                // 4
                            invoice.PeriodoLiquidacion,                                                                 // 5
                            invoice.DatosFacturaInformadaCliente.TipoFactura,                                           // 6
                            invoice.DatosFacturaInformadaCliente.ClaveRegimenEspecialOTrascendencia,                    // 7
                            "DesgloseFactura",                                                                          // 8
                            null,                                                                                       // 9
                            iva.BaseImponible,                                                                          // 10
                            iva.TipoImpositivo,                                                                         // 11
                            iva.CuotaSoportada,                                                                         // 12
                            iva.TipoRecargoEquivalencia,                                                                // 13
                            iva.CuotaRecargoEquivalencia,                                                               // 14
                            iva.PorcentCompensacionREAGYP,                                                              // 15
                            iva.ImporteCompensacionREAGYP,                                                              // 16
                            0,                                                                                          // 17
                            0,                                                                                          // 18
                            null,                                                                                       // 19
                            invoice.EstadoFactura.EstadoRegistro,                                                       // 20
                            $"{(ExternStatus)Convert.ToInt32(invoice.EstadoFactura.EstadoCuadre)}"                      // 21
                            );
                        }
                    }
                }

                tableResult.Merge(tableFR);
                start = start.AddMonths(1);

            }

            return tableResult;
        }

        /// <summary>
        /// Recupera el libro registro del IVA para los
        /// periodos indicados entre el inicial y el final.
        /// </summary>
        /// <param name="periodStart">Periodo inicial</param>
        /// <param name="periodEnd">Periodo final.</param>
        /// <param name="titular">Titular del lote.</param>
        /// <param name="proveedor">Cliente.</param> 
        public static DataTable GetSupplierInvoices(string periodStart, string periodEnd,
            Party titular, Party proveedor)
        {

            string syearStart = periodStart.Substring(0, 4);
            string smonthStart = periodStart.Substring(5, 2);

            string syearEnd = periodEnd.Substring(0, 4);
            string smonthEnd = periodEnd.Substring(5, 2);

            int yearStart = Convert.ToInt32(syearStart);
            int monthStart = Convert.ToInt32(smonthStart);

            int yearEnd = Convert.ToInt32(syearEnd);
            int monthEnd = Convert.ToInt32(smonthEnd);

            DateTime start = new DateTime(yearStart, monthStart, 1);
            DateTime end = new DateTime(yearEnd, monthEnd, 2);

            if (start > end)
                throw new Exception($"El periodo inicial '{periodStart}'" +
                    $" no puede ser mayor que el final '{periodEnd}'.");

            var tableResult = GetTableLR();

            while (start < end)
            {

                var respuestaFR = GetByPeriodFR(start.ToString("yyyy.MM"), titular, proveedor);
                DataTable tableFR = GetTableLR();

                if (respuestaFR.RegistroRespuestaConsultaLRFactInformadasProveedor != null)
                {

                    foreach (var invoice in respuestaFR.RegistroRespuestaConsultaLRFactInformadasProveedor)
                    {

                        string tipoDesglose = (invoice.DatosFacturaInformadaProveedor.TipoDesglose.DesgloseFactura != null) ?
                            "DesgloseFactura" : null;

                        if (string.IsNullOrEmpty(tipoDesglose))
                            tipoDesglose = (invoice.DatosFacturaInformadaProveedor.TipoDesglose.DesgloseTipoOperacion != null) ?
                            "DesgloseTipoOperacion" : null;

                        DesgloseF desglose = invoice.DatosFacturaInformadaProveedor?.TipoDesglose?.DesgloseFactura;

                        if (desglose == null)
                            desglose = invoice.DatosFacturaInformadaProveedor?.TipoDesglose?.DesgloseTipoOperacion?.Entrega;

                        if (desglose == null)
                            desglose = invoice.DatosFacturaInformadaProveedor?.TipoDesglose?.DesgloseTipoOperacion?.PrestacionServicios;



                        decimal noSujArt7_14 = 0;
                        decimal noSujTAI = 0;

                        if (desglose.NoSujeta != null)
                        {
                            tableFR.Rows.Add("FR",                                                                  // 0
                            invoice.IDFactura.NumSerieFacturaEmisor,                                                // 1
                            invoice.IDFactura.FechaExpedicionFacturaEmisor,                                         // 2
                            invoice.IDFactura.GetIDEmisorFactura(),                                                 // 3
                            invoice.Proveedor.NombreRazon,                                                          // 4
                            invoice.PeriodoLiquidacion,                                                             // 5
                            invoice.DatosFacturaInformadaProveedor.TipoFactura,                                     // 6
                            invoice.DatosFacturaInformadaProveedor.ClaveRegimenEspecialOTrascendencia,              // 7
                            tipoDesglose,                                                                           // 8
                            0,                                                                                      // 9
                            0,                                                                                      // 10
                            0,                                                                                      // 11
                            0,                                                                                      // 12
                            0,                                                                                      // 13
                            0,                                                                                      // 14
                            0,                                                                                      // 15
                            0,                                                                                      // 16
                            noSujArt7_14,                                                                           // 17
                            noSujTAI,                                                                               // 18
                            null,                                                                                   // 19
                            invoice.EstadoFactura.EstadoRegistro,                                                   // 20
                            $"{(ExternStatus)Convert.ToInt32(invoice.EstadoFactura.EstadoCuadre)}"                  // 21 
                            );
                        }
                        else
                        {

                            if (desglose?.Sujeta.Exenta != null)
                            {
                                if (desglose.Sujeta.Exenta.DetalleExenta != null)
                                {
                                    foreach (var iva in desglose.Sujeta.Exenta.DetalleExenta)
                                    {
                                        tableFR.Rows.Add("FR",                                                                  // 0
                                        invoice.IDFactura.NumSerieFacturaEmisor,                                                // 1
                                        invoice.IDFactura.FechaExpedicionFacturaEmisor,                                         // 2
                                        invoice.IDFactura.GetIDEmisorFactura(),                                                 // 3
                                        invoice.Proveedor.NombreRazon,                                                          // 4
                                        invoice.PeriodoLiquidacion,                                                             // 5
                                        invoice.DatosFacturaInformadaProveedor.TipoFactura,                                     // 6
                                        invoice.DatosFacturaInformadaProveedor.ClaveRegimenEspecialOTrascendencia,              // 7
                                        tipoDesglose,                                                                           // 8
                                        iva.CausaExencion,                                                                      // 9
                                        iva.BaseImponible,                                                                      // 10
                                        0,                                                                                      // 11
                                        0,                                                                                      // 12
                                        0,                                                                                      // 13
                                        0,                                                                                      // 14
                                        0,                                                                                      // 15
                                        0,                                                                                      // 16
                                        0,                                                                                      // 17
                                        0,                                                                                      // 18
                                        null,                                                                                   // 19
                                        invoice.EstadoFactura.EstadoRegistro,                                                   // 20
                                        $"{(ExternStatus)Convert.ToInt32(invoice.EstadoFactura.EstadoCuadre)}"                  // 21
                                        );
                                    }
                                }
                                else
                                {
                                    tableFR.Rows.Add("FR",                                                                      // 0
                                     invoice.IDFactura.NumSerieFacturaEmisor,                                                   // 1
                                     invoice.IDFactura.FechaExpedicionFacturaEmisor,                                            // 2
                                     invoice.IDFactura.GetIDEmisorFactura(),                                                    // 3
                                     invoice.Proveedor.NombreRazon,                                                             // 4
                                     invoice.PeriodoLiquidacion,                                                                // 5
                                     invoice.DatosFacturaInformadaProveedor.TipoFactura,                                        // 6
                                     invoice.DatosFacturaInformadaProveedor.ClaveRegimenEspecialOTrascendencia,                 // 7
                                     tipoDesglose,                                                                              // 8
                                     desglose.Sujeta.Exenta.CausaExencion,                                                                                         // 9
                                     desglose.Sujeta.Exenta.BaseImponible,                                                      // 10
                                     0,                                                                                         // 11
                                     0,                                                                                         // 12
                                     0,                                                                                         // 13
                                     0,                                                                                         // 14
                                     0,                                                                                         // 15
                                     0,                                                                                         // 16
                                     0,                                                                                         // 17
                                     0,                                                                                         // 18
                                     null,                                                                                      // 19
                                     invoice.EstadoFactura.EstadoRegistro,                                                      // 20
                                     $"{(ExternStatus)Convert.ToInt32(invoice.EstadoFactura.EstadoCuadre)}"                     // 21 
                                     );
                                }

                            }
                            else
                            {
                                foreach (var iva in desglose.Sujeta.NoExenta.DesgloseIVA.DetalleIVA)
                                {
                                    tableFR.Rows.Add("FR",                                                                      // 0
                                    invoice.IDFactura.NumSerieFacturaEmisor,                                                    // 1
                                    invoice.IDFactura.FechaExpedicionFacturaEmisor,                                             // 2
                                    invoice.IDFactura.GetIDEmisorFactura(),                                                     // 3
                                    invoice.Proveedor.NombreRazon,                                                              // 4
                                    invoice.PeriodoLiquidacion,                                                                 // 5
                                    invoice.DatosFacturaInformadaProveedor.TipoFactura,                                         // 6
                                    invoice.DatosFacturaInformadaProveedor.ClaveRegimenEspecialOTrascendencia,                  // 7
                                    tipoDesglose,                                                                               // 8
                                    desglose.Sujeta.NoExenta.TipoNoExenta,                                                      // 9
                                    iva.BaseImponible,                                                                          // 10
                                    iva.TipoImpositivo,                                                                         // 11
                                    iva.CuotaSoportada,                                                                         // 12
                                    iva.TipoRecargoEquivalencia,                                                                // 13
                                    iva.CuotaRecargoEquivalencia,                                                               // 14
                                    iva.PorcentCompensacionREAGYP,                                                              // 15
                                    iva.ImporteCompensacionREAGYP,                                                              // 16
                                    noSujArt7_14,                                                                               // 17
                                    noSujTAI,                                                                                   // 18
                                    null,                                                                                       // 19
                                    invoice.EstadoFactura.EstadoRegistro,                                                       // 20
                                    $"{(ExternStatus)Convert.ToInt32(invoice.EstadoFactura.EstadoCuadre)}"                      // 21
                                    );
                                }
                            }

                        }
                    }

                }

                tableResult.Merge(tableFR);
                start = start.AddMonths(1);

            }


            return tableResult;

        }

        /// <summary>
        /// Recupera el libro registro del IVA para los
        /// periodos indicados entre el inicial y el final
        /// y lo almacena en formato CSV en la ruta indicada.
        /// </summary>
        /// <param name="periodStart">Periodo inicial</param>
        /// <param name="periodEnd">Periodo final.</param>
        /// <param name="titular">Titular del lote.</param>
        /// <param name="cliente">Titular del lote.</param> 
        /// <param name="csvPath">Ruta en la que se guardará el CSV de resultados.</param>
        public static void SaveCustomerInvoicesCSV(string periodStart, string periodEnd,
            Party titular, Party cliente, string csvPath)
        {

            var invoices = GetCustomerInvoices(periodStart, periodEnd, titular, cliente);
            SaveInvoicesToCSV(invoices, csvPath);

        }

        /// <summary>
        /// Recupera el libro registro del IVA para los
        /// periodos indicados entre el inicial y el final
        /// y lo almacena en formato CSV en la ruta indicada.
        /// </summary>
        /// <param name="periodStart">Periodo inicial</param>
        /// <param name="periodEnd">Periodo final.</param>
        /// <param name="titular">Titular del lote.</param>
        /// <param name="proveedor">Titular del lote.</param> 
        /// <param name="csvPath">Ruta en la que se guardará el CSV de resultados.</param>
        public static void SaveSupplierInvoicesCSV(string periodStart, string periodEnd,
            Party titular, Party proveedor, string csvPath)
        {

            var invoices = GetSupplierInvoices(periodStart, periodEnd, titular, proveedor);
            SaveInvoicesToCSV(invoices, csvPath);

        }

        /// <summary>
        /// Guarda una tabla de facturas como CSV.
        /// </summary>
        /// <param name="invoices">Tabla con facturas.</param>
        /// <param name="csvPath">Ruta del archivo.</param>
        private static void SaveInvoicesToCSV(DataTable invoices, string csvPath)
        {
            string line = "Libro;NumDoc;Fecha;NIF;Nombre;Periodo;TipoFactura;ClaveRegEspOTrasc;TipoDesglose;Tipo;BaseImponible;TipoImpositivo;Cuota;TipoRE;CuotaRE;TipoREAGYP;CuotaREAGYP;NoSujArt7_14;NoSujTAI;CSV;Estado;EstadoCuadre\n";
            StringBuilder sb = new StringBuilder();
            sb.Append(line);

            foreach (DataRow invoice in invoices.Rows)
            {

                line = $"{invoice["Libro"]};" +                                             // 0
                $"{invoice["NumDoc"]};" +                                                   // 1
                $"{invoice["Fecha"]};" +                                                    // 2
                $"{invoice["NIF"]};" +                                                      // 3
                $"{invoice["Nombre"]};" +                                                   // 4
                $"{invoice["Periodo"]};" +                                                  // 5
                $"{invoice["TipoFactura"]};" +                                              // 6
                $"{invoice["ClaveRegEspOTrasc"]};" +                                        // 7
                $"{invoice["TipoDesglose"]};" +                                             // 8
                $"{invoice["Tipo"]};" +                                                     // 9
                $"{invoice["BaseImponible"]};" +                                            // 10
                $"{invoice["TipoImpositivo"]};" +                                           // 11
                $"{invoice["Cuota"]};" +                                                    // 12
                $"{invoice["TipoRE"]};" +                                                   // 13
                $"{invoice["CuotaRE"]};" +                                                  // 14
                $"{invoice["TipoREAGYP"]};" +                                               // 15
                $"{invoice["CuotaREAGYP"]};" +                                              // 16
                $"{invoice["NoSujArt7_14"]};" +                                             // 17
                $"{invoice["NoSujTAI"]};" +                                                 // 18
                $"{invoice["CSV"]};" +                                                      // 19
                $"{invoice["Estado"]};" +                                                   // 20  
                $"{invoice["EstadoCuadre"]}\n";                                             // 21 

                sb.Append(line);

            }

            int WIN_1252_CP = 1252; // Windows ANSI codepage 1252
            File.WriteAllText(csvPath, sb.ToString(), Encoding.GetEncoding(WIN_1252_CP));
        }

        /// <summary>
        /// Tabla libro registros vacía con las
        /// columna generadas.
        /// </summary>
        /// <returns>Tabla LR vacía con columnas.</returns>
        private static DataTable GetTableLR()
        {

            DataTable result = new DataTable();
            result.Columns.Add("Libro", typeof(string));
            result.Columns.Add("NumDoc", typeof(string));
            result.Columns.Add("Fecha", typeof(DateTime));
            result.Columns.Add("NIF", typeof(string));
            result.Columns.Add("Nombre", typeof(string));
            result.Columns.Add("Periodo", typeof(string));
            result.Columns.Add("TipoFactura", typeof(string));
            result.Columns.Add("ClaveRegEspOTrasc", typeof(string));
            result.Columns.Add("TipoDesglose", typeof(string));
            result.Columns.Add("Tipo", typeof(string));
            result.Columns.Add("BaseImponible", typeof(decimal));
            result.Columns.Add("TipoImpositivo", typeof(decimal));
            result.Columns.Add("Cuota", typeof(decimal));
            result.Columns.Add("TipoRE", typeof(decimal));
            result.Columns.Add("CuotaRE", typeof(decimal));
            result.Columns.Add("TipoREAGYP", typeof(decimal));
            result.Columns.Add("CuotaREAGYP", typeof(decimal));
            result.Columns.Add("NoSujArt7_14", typeof(decimal));
            result.Columns.Add("NoSujTAI", typeof(decimal));
            result.Columns.Add("CSV", typeof(string));
            result.Columns.Add("Estado", typeof(string));
            result.Columns.Add("EstadoCuadre", typeof(string));

            return result;

        }

    }
}
