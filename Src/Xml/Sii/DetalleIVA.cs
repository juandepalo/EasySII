namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Detalle IVA.
    /// </summary>
    public class DetalleIVA
    {
        /// <summary>
        /// Tipo Impositivo. Opcional a partir de la versión 0.7.
        /// </summary>
        public string TipoImpositivo { get; set; }

        /// <summary>
        /// Base Imponible. Decimal(12,2).
        /// </summary>
        public string BaseImponible { get; set; }

        /// <summary>
        /// Cuota Repercutida. Decimal(12,2). Opcional a partir de la versión 0.7.
        /// </summary>
        public string CuotaRepercutida { get; set; }

        /// <summary>
        /// Cuota Soportada. Decimal(12,2).
        /// </summary>
        public string CuotaSoportada { get; set; }

        /// <summary>
        /// Tipo Recargo Equivalencia. Decimal(3,2).
        /// </summary>
        public string TipoRecargoEquivalencia { get; set; }

        /// <summary>
        /// Cuota Recargo Equivalencia. Decimal(12,2).
        /// </summary>
        public string CuotaRecargoEquivalencia { get; set; }

        /// <summary>
        /// Porcentaje compensación REAGYP. Decimal(3,2).
        /// </summary>
        public string PorcentCompensacionREAGYP { get; set; }

        /// <summary>
        /// Compensación REAGYP. Decimal(12,2).
        /// </summary>
        public string ImporteCompensacionREAGYP { get; set; }

    }
}
