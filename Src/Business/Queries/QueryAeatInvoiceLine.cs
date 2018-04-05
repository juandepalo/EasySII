namespace EasySII.Business.Queries
{
    /// <summary>
    /// Lineas de impuestos
    /// </summary>
    public class QueryAeatInvoiceLine
    {
        /// <summary>
        /// E1, E2, E3...S1, S2...
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Tipo IVA
        /// </summary>
        public decimal TipoImpositivo { get; set; }

        /// <summary>
        /// Base IVA
        /// </summary>
        public decimal BaseImponible { get; set; }

        /// <summary>
        /// Cuota IVA
        /// </summary>
        public decimal Cuota { get; set; }

        /// <summary>
        /// Tipo IVA regcargo equivalencia.
        /// </summary>
        public decimal TipoRE { get; set; }

        /// <summary>
        /// cuota IVA recargo equivalencia
        /// </summary>
        public decimal CuotaRE { get; set; }

        /// <summary>
        /// Tipo IVA regimen especial agricultura ganaderia y pesca.
        /// Compensaciones.
        /// </summary>
        public decimal TipoREAGYP { get; set; }

        /// <summary>
        /// Cuota IVA regimen especial agricultura ganaderia y pesca.
        /// Compensaciones.
        /// </summary>
        public decimal CuotaREAGYP { get; set; }

        /// <summary>
        /// Importe no sujeto Art. 7, 14 y otros
        /// </summary>
        public decimal NoSujArt7_14 { get; set; }

        /// <summary>
        /// Importe no sujeto IPSI/IGIC
        /// </summary>
        public decimal NoSujTAI { get; set; }

        /// <summary>
        /// Correto, Anulada...
        /// </summary>
        public string Estado { get; set; }

    }
}
