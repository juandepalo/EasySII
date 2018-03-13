namespace EasySII.Business
{
    /// <summary>
    /// Lista 11 de las especificaciones donde se 
    /// detallan los medios de pago.
    /// </summary>
    public enum PaymentTerms
    {
        /// <summary>
        /// Transferencia bancaria.
        /// </summary>
        Transferencia = 1,
        /// <summary>
        /// Cheque.
        /// </summary>
        Cheque,
        /// <summary>
        /// No se cobra / paga (fecha límite de devengo / devengo 
        /// forzoso en concurso de acreedores)
        /// </summary>
        NoCobroPago,
        /// <summary>
        /// Otros medios de cobro / pago
        /// </summary>
        Otros,
        /// <summary>
        /// Domiciliación bancaria
        /// </summary>
        DomiciliacionBancaria
    }
}
