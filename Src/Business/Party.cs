namespace EasySII.Business
{
    /// <summary>
    /// Interlocutor de negocio.
    /// </summary>
    public class Party
    {
        /// <summary>
        /// Número de identificación fiscal.
        /// </summary>
        public string TaxIdentificationNumber { get; set; }

        /// <summary>
        /// Nombre o razón social del interlocutor de negocio.
        /// </summary>
        public string PartyName { get; set; }

        /// <summary>
        /// Representacion textual de la instancia.
        /// </summary>
        /// <returns>Representacion textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{TaxIdentificationNumber??""}, {PartyName??""}";
        }
    }
}
