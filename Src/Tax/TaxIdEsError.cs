namespace EasySII.Tax
{
    /// <summary>
    /// Definición de errores en número fiscal.
    /// </summary>
    public enum TaxIdEsError
    {
        /// <summary>
        /// Ningún error.
        /// </summary>
        NoError,
        /// <summary>
        /// Longitud de la cadena errónea, o carácteres no permitidos.
        /// </summary>
        InvalidString,
        /// <summary>
        /// Carácter no permitido en la primera posición.
        /// </summary>
        InvalidFirstChar,
        /// <summary>
        /// Longitud no permitida.
        /// </summary>
        InvalidLength,
        /// <summary>
        /// El dígito de control no es válido.
        /// </summary>
        InvalidControlNumber
    }
}
