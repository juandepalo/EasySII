namespace EasySII.Business
{
    /// <summary>
    /// Causa de exención de operaciones sujetas y exentas. Lista L9.
    /// </summary>
    public enum CausaExencion
    {
        /// <summary>
        /// Exenta por el artículo 20: Exenciones en operaciones interiores.
        /// </summary>
        E1,
        /// <summary>
        /// Exenta por el artículo 21 (Exportaciones)
        /// </summary>
        E2,
        /// <summary>
        /// Exenta por el artículo 22: Operaciones asimiladas a las exportaciones.
		/// Navegación marítima internacional, aeronaves...
        /// </summary>
        E3,
        /// <summary>
        ///  Exenta por el artículo 24: Exenciones ralativas a regímenes aduaneros y fiscales.
		///  Depositos aduaneros...
        /// </summary>
        E4,
        /// <summary>
        /// Exenta por el artículo 25 (Operaciones UE)
        /// </summary>
        E5,
        /// <summary>
        /// Exenta por Otros
        /// </summary>
        E6
    };
}
