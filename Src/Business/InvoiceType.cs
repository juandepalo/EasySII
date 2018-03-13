namespace EasySII.Business
{
    /// <summary>
    /// Tipo factura
    /// </summary>
    public enum InvoiceType
    {
        /// <summary>
        /// Factura
        /// </summary>
        F1,
        /// <summary>
        /// Factura Simplificada (ticket)
        /// </summary>
        F2,
        /// <summary>
        /// Factura Rectificativa (Art 80.1 y 80.2 y error fundado en derecho)
        /// </summary>
        R1,
		/// <summary>
		/// Factura Rectificativa (Art. 80.3): Cuando el destinatario de las operaciones sujetas al 
		/// Impuesto no haya hecho efectivo el pago de las cuotas repercutidas y siempre que, con 
		/// posterioridad al devengo de la operación, se dicte auto de declaración de concurso.
		/// </summary>
		R2,
		/// <summary>
		/// Factura Rectificativa (Art. 80.4): Cuando los créditos correspondientes a 
		/// las cuotas repercutidas por las operaciones gravadas sean total o parcialmente incobrables.
		/// </summary>
		R3,
        /// <summary>
        /// Factura Rectificativa (Resto)
        /// </summary>
        R4,
        /// <summary>
        /// Factura Rectificativa en facturas simplificadas
        /// </summary>
        R5,
        /// <summary>
        /// Factura emitida en sustitución de facturas simplificadas facturadas y declaradas
        /// </summary>
        F3,
        /// <summary>
        /// Asiento resumen de facturas
        /// </summary>
        F4,
        /// <summary>
        /// Sólo para recibidas. Importación DUA.
        /// </summary>
        F5,
        /// <summary>
        /// Sólo para recibidas. Otros justificantes contables.
        /// </summary>
        F6,
        /// <summary>
        /// Aduanas - Liquidación complementaria
        /// </summary>
        LC
    };
}
