namespace EasySII.Business
{
    /// <summary>
    /// Calificación del tipo de localización del inmueble según
    /// la lista L6: Situación del Inmueble.
    /// </summary>
    public enum BuildingState
    {
        /// <summary>
        /// Valor usado cuando no se trata de una factura
        /// emitida de alquiler.
        /// </summary>
        SinValor,
        /// <summary>
        /// Inmueble con referencia catastral situado en cualquier 
        /// punto del territorio español, excepto País Vasco y Navarra.
        /// </summary>
        EsExceptoPaisVascoYNavarra,
        /// <summary>
        /// Inmueble situado en la Comunidad Autónoma del País Vasco 
        /// o en la Comunidad Foral de Navarra.
        /// </summary>
        PaisVascoYNavarra,
        /// <summary>
        /// Inmueble en cualquiera de las situaciones anteriores pero sin referencia catastral.
        /// </summary>
        SinRefCatastral,
        /// <summary>
        /// Inmueble situado en el extranjero.
        /// </summary>
        EnElExtranjero
    }
}
