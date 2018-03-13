namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Exenta.
    /// </summary>
    public class Exenta
    {

        /// <summary>
        /// Causa de exención de operaciones sujetas y exentas. Lista L9:
        /// <para>E1: Exenta por el artículo 20</para>
        /// <para>E2: Exenta por el artículo 21</para>
        /// <para>E3: Exenta por el artículo 22</para>
        /// <para>E4: Exenta por el artículo 24</para>
        /// <para>E5: Exenta por el artículo 25</para>
        /// <para>E6: Exenta por Otros</para>
        /// </summary>
        public string CausaExencion { get; set;}

        /// <summary>
        /// Base imponible exenta.
        /// </summary>
        public string BaseImponible { get; set; }

        /// <summary>
        /// Constructor clase Exenta.
        /// </summary>
        public Exenta()
        {    
        }
    }
}
