namespace EasySII.Business
{
    /// <summary>
    /// Lista L4  Tipos de Identificación en el país de residencia
    /// </summary>
    public enum IDOtroType
    {
        /// <summary>
        /// NIF-IVA
        /// </summary>
        NifIva = 2,
        /// <summary>
        /// PASAPORTE
        /// </summary>
        Pasaporte,
        /// <summary>
        /// DOCUMENTO OFICIAL DE IDENTIFICACIÓN EXPEDIDO POR EL PAIS O TERRITORIO DE RESIDENCIA.
        /// </summary>
        DocOficialPaisResidencia,
        /// <summary>
        /// CERTIFICADO DE RESIDENCIA
        /// </summary>
        CertificadoResidencia,
        /// <summary>
        /// OTRO DOCUMENTO PROBATORIO
        /// </summary>
        OtroDocProbatorio,
        /// <summary>
        /// NO CENSADO: Número Id: NIF no censado del receptor de la factura Apellidos y nombre: 
        /// Nombre del no censado receptor de la factura. Siempre que se utilice esta clave de 
        /// identificación 07 el registro quedará aceptado con errores
        /// </summary>
        NoCensado
    }
}
