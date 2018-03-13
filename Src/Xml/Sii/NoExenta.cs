namespace EasySII.Xml.Sii
{
    /// <summary>
    /// No Exenta.
    /// </summary>
    public class NoExenta
    {

        /// <summary>
        /// Calificación del tipo de operación Sujeta/ No Exenta. Lista L7:
        /// <para>S1: Sujeta – No Exenta</para>
        /// <para>S2: Sujeta – No Exenta - Inv. Suj. Pasivo</para>
        /// </summary>
        public string TipoNoExenta { get; set;}

        /// <summary>
        /// Desglose IVA
        /// </summary>
        public DesgloseIVA DesgloseIVA { get; set; }

        /// <summary>
        /// Constructor clase NoExenta.
        /// </summary>
        public NoExenta()
        {
            DesgloseIVA = new DesgloseIVA();
        }
    }
}
