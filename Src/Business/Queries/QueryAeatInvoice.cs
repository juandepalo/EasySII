using EasySII.Net;
using EasySII.Xml;
using EasySII.Xml.Sii;
using EasySII.Xml.SiiR;
using EasySII.Xml.Soap;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace EasySII.Business.Queries
{

    /// <summary>
    /// Resumen de información a nivel de línea de libro registro facturas,
    /// recuperada de la AEAT mediante las consultas de facturas emitidas
    /// y recibidas.
    /// </summary>
    public class QueryAeatInvoice
    {

        /// <summary>
        /// Para almecenar el objeto en el wrapper.
        /// </summary>
        RegistroRCLRFacturasEmitidas _RegistroRCLRFacturasEmitidas;

        /// <summary>
        /// Para almecenar el objeto en el wrapper.
        /// </summary>
        RegistroRCLRFacturasRecibidas _RegistroRCLRFacturasRecibidas;

        /// <summary>
        /// Factura, TipOpEntrega, TiOpServicio
        /// </summary>
        private DesgloseF Desglose

        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida != null)
                    {
                        if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose != null)
                        {
                            if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseFactura != null)
                            {
                                return _RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseFactura;
                            }
                            else
                            {
                                if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion != null)
                                {
                                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion.Entrega != null)
                                        return _RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion.Entrega;
                                    else if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion.PrestacionServicios != null)
                                        return _RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion.PrestacionServicios;
                                }
                            }
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Factura, TipOpEntrega, TiOpServicio
        /// </summary>
        private DesgloseIVA DesgloseIVA

        {
            get
            {
                if (Libro == "FR")
                    if (_RegistroRCLRFacturasRecibidas.FacturaRecibida != null)
                        if (_RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura != null)
                            if (_RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo != null)
                                return _RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo;
                            else if (_RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura.DesgloseIVA != null)
                                return _RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura.DesgloseIVA;

                return null;
            }
        }

        /// <summary>
        /// Devuelve las facturas emitidas envíadas por el titular
        /// del lote en el periodo indicado.
        /// </summary>
        /// <param name="period">Periodo en formato yyyy.MM.</param>
        /// <param name="titular">Titular de las facturas a recuperar.</param>
        /// <returns>Lista de facturas emitidas en el periodo.</returns>
        private static RespuestaConsultaLRFacturasEmitidas GetByPeriodFE(string period, Party titular)
        {

            ARInvoicesQuery batchInvoiceQuery = new ARInvoicesQuery()
            {
                Titular = titular
            };
            ARInvoice templateInvoiceQuery = new ARInvoice();

            string syear = period.Substring(0, 4);
            string smonth = period.Substring(5, 2);

            int year = Convert.ToInt32(syear);
            int month = Convert.ToInt32(smonth);

            DateTime date = new DateTime(year, month, 1);

            // Necesitamos indicar una fecha de factura, para que se pueda calcular el ejercicio y periodo
            // que son necesarios y obligatorios para realizar esta peticiones.
            templateInvoiceQuery.IssueDate = date;
            batchInvoiceQuery.ARInvoice = templateInvoiceQuery;

            string response = Wsd.GetFacturasEmitidas(batchInvoiceQuery);

            var streamResponse = new MemoryStream();
            var writer = new StreamWriter(streamResponse);
            writer.Write(response);
            writer.Flush();
            streamResponse.Position = 0;

            try
            {
                // Obtengo la respuesta de la consulta de facturas recibidas del archivo de respuesta de la AEAT.
                Envelope envelope = new Envelope(streamResponse);

                if(envelope.Body.RespuestaConsultaLRFacturasEmitidas != null)
                    return envelope.Body.RespuestaConsultaLRFacturasEmitidas;
             
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
        /// Devuelve las facturas recibidas envíadas por el titular
        /// del lote en el periodo indicado.
        /// </summary>
        /// <param name="period">Periodo en formato yyyy.MM.</param>
        /// <param name="titular">Titular de las facturas a recuperar.</param>
        /// <returns>Lista de facturas recibidas en el periodo.</returns>
        private static RespuestaConsultaLRFacturasRecibidas GetByPeriodFR(string period, Party titular)
        {

            APInvoicesQuery batchInvoiceQuery = new APInvoicesQuery()
            {
                Titular = titular
            };
            APInvoice templateInvoiceQuery = new APInvoice();
            // Necesitamos indicar una fecha de factura, para que se pueda calcular el ejercicio y periodo
            // que son necesarios y obligatorios para realizar esta peticiones.
            string syear = period.Substring(0, 4);
            string smonth = period.Substring(5, 2);

            int year = Convert.ToInt32(syear);
            int month = Convert.ToInt32(smonth);

            DateTime date = new DateTime(year, month, 1);

            templateInvoiceQuery.IssueDate = date;

            batchInvoiceQuery.APInvoice = templateInvoiceQuery;

            string response = Wsd.GetFacturasRecibidas(batchInvoiceQuery);

            var streamResponse = new MemoryStream();
            var writer = new StreamWriter(streamResponse);
            writer.Write(response);
            writer.Flush();
            streamResponse.Position = 0;

            try
            {

                // Obtengo la respuesta de la consulta de facturas recibidas del archivo de respuesta de la AEAT.
                Envelope envelope = new Envelope(streamResponse);                

                if (envelope.Body.RespuestaConsultaLRFacturasRecibidas != null)
                    return envelope.Body.RespuestaConsultaLRFacturasRecibidas;
               
                SoapFault msgError = envelope.Body.RespuestaError;
                if (msgError != null)
                    throw new Exception(msgError.FaultDescription);

              
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
        public static List<QueryAeatInvoice> GetInvoices(string periodStart, string periodEnd, Party titular)
        {
            var respuestasEmitidas = new List<RespuestaConsultaLRFacturasEmitidas>();
            var respuestasRecibidas = new List<RespuestaConsultaLRFacturasRecibidas>();

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

            while (start < end)
            {
                respuestasEmitidas.Add(GetByPeriodFE(start.ToString("yyyy.MM"), titular));
                respuestasRecibidas.Add(GetByPeriodFR(start.ToString("yyyy.MM"), titular));
                start = start.AddMonths(1);
            }

            List<QueryAeatInvoice> libroLines = new List<QueryAeatInvoice>();

            foreach (var periodo in respuestasEmitidas)
            {

                string per = $"{periodo.PeriodoImpositivo.Ejercicio}.{periodo.PeriodoImpositivo.Periodo}";

                if (periodo.ResultadoConsulta == "ConDatos")
                {
                    foreach (var invoice in periodo.RegistroRCLRFacturasEmitidas)
                    {
                        libroLines.Add(new QueryAeatInvoice(invoice)
                        {
                            Periodo = per
                        });
                    }
                }
            }

            foreach (var periodo in respuestasRecibidas)
            {

                string per = $"{periodo.PeriodoImpositivo.Ejercicio}.{periodo.PeriodoImpositivo.Periodo}";

                if (periodo.ResultadoConsulta == "ConDatos")
                {
                    foreach (var invoice in periodo.RegistroRCLRFacturasRecibidas)
                    {
                        libroLines.Add(new QueryAeatInvoice(invoice)
                        {
                            Periodo = per
                        });
                    }
                }
            }

            return libroLines;

        }

        /// <summary>
        /// Recupera el libro registro del IVA para los
        /// periodos indicados entre el inicial y el final.
        /// </summary>
        /// <param name="periodStart">Periodo inicial</param>
        /// <param name="periodEnd">Periodo final.</param>
        /// <param name="titular">Titular del lote.</param>
        public static DataTable GetTableLR(string periodStart, string periodEnd, Party titular)
        {
            List<QueryAeatInvoice> invoices = GetInvoices(periodStart, periodEnd, titular);

            DataTable result = GetTableLR();

            foreach (var invoice in invoices)
            {
                foreach (var iva in invoice.Lines)
                {
                    result.Rows.Add(invoice.Libro,                      // 0
                    invoice.NumDoc,                                     // 1
                    invoice.Fecha,                                      // 2
                    invoice.NIF,                                        // 3
                    invoice.Nombre,                                     // 4
                    invoice.Periodo,                                    // 5
                    invoice.TipoFactura,                                // 6
                    invoice.ClaveRegimenEspecialOTrascendencia,         // 7
                    invoice.TipoDesglose,                               // 8
                    iva.Tipo,                                           // 9
                    iva.BaseImponible,                                  // 10
                    iva.TipoImpositivo,                                 // 11
                    iva.Cuota,                                          // 12
                    iva.TipoRE,                                         // 13
                    iva.CuotaRE,                                        // 14
                    iva.TipoREAGYP,                                     // 15
                    iva.CuotaREAGYP,                                    // 16
                    iva.NoSujArt7_14,                                   // 17
                    iva.NoSujTAI,                                       // 18
                    invoice.CSV,                                        // 19
                    invoice.Estado);                                    // 20           
                }
            }

            return result;

        }

        /// <summary>
        /// Recupera el libro registro del IVA para los
        /// periodos indicados entre el inicial y el final
        /// y lo almacena en formato CSV en la ruta indicada.
        /// </summary>
        /// <param name="periodStart">Periodo inicial</param>
        /// <param name="periodEnd">Periodo final.</param>
        /// <param name="titular">Titular del lote.</param>
        /// <param name="csvPath">Ruta en la que se guardará el CSV de resultados.</param>
        public static void SaveCSV(string periodStart, string periodEnd, 
            Party titular, string csvPath)
        {

            List<QueryAeatInvoice> invoices = GetInvoices(periodStart, periodEnd, titular);

            string line = "Libro;NumDoc;Fecha;NIF;Nombre;Periodo;TipoFactura;ClaveRegEspOTrasc;TipoDesglose;Tipo;BaseImponible;TipoImpositivo;Cuota;TipoRE;CuotaRE;TipoREAGYP;CuotaREAGYP;NoSujArt7_14;NoSujTAI;CSV;Estado\n";
            StringBuilder sb = new StringBuilder();
            sb.Append(line);

            foreach (var invoice in invoices)
            {
                foreach (var iva in invoice.Lines)
                {

                    line = $"{invoice.Libro};" +                                        // 0
                    $"{invoice.NumDoc};" +                                              // 1
                    $"{invoice.Fecha};" +                                               // 2
                    $"{invoice.NIF};" +                                                 // 3
                    $"{invoice.Nombre};" +                                              // 4
                    $"{invoice.Periodo};" +                                             // 5
                    $"{invoice.TipoFactura};" +                                         // 6
                    $"{invoice.ClaveRegimenEspecialOTrascendencia};" +                  // 7
                    $"{invoice.TipoDesglose};" +                                        // 8
                    $"{iva.Tipo};" +                                                    // 9
                    $"{iva.BaseImponible};" +                                           // 10
                    $"{iva.TipoImpositivo};" +                                          // 11
                    $"{iva.Cuota};" +                                                   // 12
                    $"{iva.TipoRE};" +                                                  // 13
                    $"{iva.CuotaRE};" +                                                 // 14
                    $"{iva.TipoREAGYP};" +                                              // 15
                    $"{iva.CuotaREAGYP};" +                                             // 16
                    $"{iva.NoSujArt7_14};" +                                            // 17
                    $"{iva.NoSujTAI};" +                                                // 18
                    $"{invoice.CSV};" +                                                 // 19
                    $"{invoice.Estado}\n";                                              // 20  

                    sb.Append(line);
                }
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

            return result;

        }

        /// <summary>
        /// FE: Emitidas / FR: Recibidas
        /// </summary>
        public string Libro { get; private set; }

        /// <summary>
        /// Periodo
        /// </summary>
        public string Periodo { get; set; }

        /// <summary>
        /// Estado en el SII de la AEAT.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// código CSV.
        /// </summary>
        public string CSV { get; set; }

        /// <summary>
        /// ClaveRegimenEspecialOTrascendencia
        /// </summary>
        public string ClaveRegimenEspecialOTrascendencia
        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.ClaveRegimenEspecialOTrascendencia))
                            return _RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.ClaveRegimenEspecialOTrascendencia;
                }
                if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas.FacturaRecibida != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasRecibidas.FacturaRecibida.ClaveRegimenEspecialOTrascendencia))
                            return _RegistroRCLRFacturasRecibidas.FacturaRecibida.ClaveRegimenEspecialOTrascendencia;
                }

                return null;
            }
        }

        /// <summary>
        /// NIF
        /// </summary>
        public string NIF
        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura != null)
                    {
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura.NIF))
                        {
                            return _RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura.NIF;
                        }
                        else
                        {
                            if (_RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura.IDOtro != null)
                                return _RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura.IDOtro.ID;
                        }
                    }
                }
                else if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura != null)
                    {
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura.NIF))
                        {
                            return _RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura.NIF;
                        }
                        else
                        {
                            if (_RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura.IDOtro != null)
                                return _RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura.IDOtro.ID;
                        }
                    }

                }

                return null;
            }
        }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre
        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida != null)
                        if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.Contraparte != null)
                            if (!string.IsNullOrEmpty(_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.Contraparte.NombreRazon))
                                return _RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.Contraparte.NombreRazon;

                }
                else if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas.FacturaRecibida != null)
                        if (_RegistroRCLRFacturasRecibidas.FacturaRecibida.Contraparte != null)
                            if (!string.IsNullOrEmpty(_RegistroRCLRFacturasRecibidas.FacturaRecibida.Contraparte.NombreRazon))
                                return _RegistroRCLRFacturasRecibidas.FacturaRecibida.Contraparte.NombreRazon;
                }

                return null;
            }
        }


        /// <summary>
        /// Número documento
        /// </summary>
        public string NumDoc
        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.IDFactura != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasEmitidas.IDFactura.NumSerieFacturaEmisor))
                            return _RegistroRCLRFacturasEmitidas.IDFactura.NumSerieFacturaEmisor;

                }
                else if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas.IDFactura != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasRecibidas.IDFactura.NumSerieFacturaEmisor))
                            return _RegistroRCLRFacturasRecibidas.IDFactura.NumSerieFacturaEmisor;

                }

                return null;
            }
        }

        /// <summary>
        /// Fecha documento
        /// </summary>
        public DateTime? Fecha
        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasEmitidas.IDFactura.IDEmisorFactura.NIF))
                            return Convert.ToDateTime(_RegistroRCLRFacturasEmitidas.IDFactura.FechaExpedicionFacturaEmisor);

                }
                else if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasRecibidas.IDFactura.IDEmisorFactura.NIF))
                            return Convert.ToDateTime(_RegistroRCLRFacturasRecibidas.IDFactura.FechaExpedicionFacturaEmisor);

                }

                return null;
            }
        }

        /// <summary>
        /// F1...
        /// </summary>
        public string TipoFactura
        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoFactura))
                            return _RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoFactura;

                }
                else if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas.FacturaRecibida != null)
                        if (!string.IsNullOrEmpty(_RegistroRCLRFacturasRecibidas.FacturaRecibida.TipoFactura))
                            return _RegistroRCLRFacturasRecibidas.FacturaRecibida.TipoFactura;

                }

                return null;
            }
        }

        /// <summary>
        /// Factura, TipOpEntrega, TiOpServicio
        /// </summary>
        public string TipoDesglose

        {
            get
            {
                if (Libro == "FE")
                {
                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida != null)
                    {
                        if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose != null)
                        {
                            if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseFactura != null)
                            {
                                return "FEFactura";
                            }
                            else
                            {
                                if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion != null)
                                {
                                    if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion.Entrega != null)
                                        return "FEOpEntrega";
                                    else if (_RegistroRCLRFacturasEmitidas.DatosFacturaEmitida.TipoDesglose.DesgloseTipoOperacion.PrestacionServicios != null)
                                        return "FEOpServicios";
                                }
                            }
                        }
                    }
                }
                else if (Libro == "FR")
                {
                    if (_RegistroRCLRFacturasRecibidas != null)
                    {
                        if (_RegistroRCLRFacturasRecibidas.FacturaRecibida != null)
                        {
                            if (_RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura.InversionSujetoPasivo != null)
                            {
                                return "FRInvSujPasivo";
                            }
                            else if (_RegistroRCLRFacturasRecibidas.FacturaRecibida.DesgloseFactura.DesgloseIVA != null)
                            {
                                return "FRDesgloseIVA";
                            }
                        }
                    }
                }

                return null;

            }
        }

        /// <summary>
        /// Lineas de IVA.
        /// </summary>
        public List<QueryAeatInvoiceLine> Lines { get; private set; }


        /// <summary>
        /// Construye una nueva instancia de la clase QueryAeatLine.
        /// </summary>
        /// <param name="respuesta"></param>
        public QueryAeatInvoice(dynamic respuesta)
        {

            if (respuesta.DatosPresentacion != null)
                CSV = respuesta.DatosPresentacion.CSV;

            Lines = new List<QueryAeatInvoiceLine>();

            switch (respuesta.GetType().Name)
            {
                case "RegistroRCLRFacturasEmitidas":
                    _RegistroRCLRFacturasEmitidas = (RegistroRCLRFacturasEmitidas)respuesta;
                    Libro = "FE";
                    Estado = _RegistroRCLRFacturasEmitidas.EstadoFactura.EstadoRegistro;
                    GetLinesFE();
                    break;
                case "RegistroRCLRFacturasRecibidas":
                    _RegistroRCLRFacturasRecibidas = (RegistroRCLRFacturasRecibidas)respuesta;
                    Libro = "FR";
                    Estado = _RegistroRCLRFacturasRecibidas.EstadoFactura.EstadoRegistro;
                    GetLinesFR();
                    break;
            }

        }


        private void GetLinesFE()
        {

            if (Desglose.NoSujeta != null)
            {
                Lines.Add(new QueryAeatInvoiceLine()
                {
                    NoSujArt7_14 = SIIParser.ToDecimal(Desglose.NoSujeta.ImportePorArticulos7_14_Otros),
                    NoSujTAI = SIIParser.ToDecimal(Desglose.NoSujeta.ImporteTAIReglasLocalizacion),
                    Tipo = "NS"
                });
            }

            if (Desglose.Sujeta != null)
            {

                if (Desglose.Sujeta.Exenta != null)
                {
                    Lines.Add(new QueryAeatInvoiceLine()
                    {
                        BaseImponible = SIIParser.ToDecimal(Desglose.Sujeta.Exenta.BaseImponible),
                        Tipo = Desglose.Sujeta.Exenta.CausaExencion
                    });
                }

                if (Desglose.Sujeta.NoExenta != null)
                {

                    if (Desglose.Sujeta.NoExenta.DesgloseIVA != null)
                    {
                        if (Desglose.Sujeta.NoExenta.DesgloseIVA.DetalleIVA != null)
                        {
                            foreach (var iva in Desglose.Sujeta.NoExenta.DesgloseIVA.DetalleIVA)
                            {
                                Lines.Add(new QueryAeatInvoiceLine()
                                {
                                    BaseImponible = SIIParser.ToDecimal(iva.BaseImponible),
                                    TipoImpositivo = SIIParser.ToDecimal(iva.TipoImpositivo),
                                    TipoRE = SIIParser.ToDecimal(iva.TipoRecargoEquivalencia),
                                    Cuota = SIIParser.ToDecimal(iva.CuotaRepercutida),
                                    CuotaRE = SIIParser.ToDecimal(iva.CuotaRecargoEquivalencia),
                                    TipoREAGYP = SIIParser.ToDecimal(iva.PorcentCompensacionREAGYP),
                                    CuotaREAGYP = SIIParser.ToDecimal(iva.ImporteCompensacionREAGYP),
                                    Tipo = Desglose.Sujeta.NoExenta.TipoNoExenta
                                });
                            }
                        }
                    }
                }

            }
        }


        private void GetLinesFR()
        {

            if (DesgloseIVA != null)
            {
                if (DesgloseIVA.DetalleIVA != null)
                {
                    foreach (var iva in DesgloseIVA.DetalleIVA)
                    {
                        Lines.Add(new QueryAeatInvoiceLine()
                        {
                            BaseImponible = SIIParser.ToDecimal(iva.BaseImponible),
                            TipoImpositivo = SIIParser.ToDecimal(iva.TipoImpositivo),
                            TipoRE = SIIParser.ToDecimal(iva.TipoRecargoEquivalencia),
                            Cuota = SIIParser.ToDecimal(iva.CuotaSoportada),
                            CuotaRE = SIIParser.ToDecimal(iva.CuotaRecargoEquivalencia),
                            TipoREAGYP = SIIParser.ToDecimal(iva.PorcentCompensacionREAGYP),
                            CuotaREAGYP = SIIParser.ToDecimal(iva.ImporteCompensacionREAGYP),
                            Tipo = ""
                        });
                    }
                }
            }

        }

    }
}
