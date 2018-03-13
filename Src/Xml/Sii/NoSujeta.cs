using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// No Sujeta.
    /// </summary>
    public class NoSujeta
    {

        /// <summary>
        /// Importe en euros si la sujeción es por el art. 7,14, otros. Decimal(12,2).
        /// </summary>
        public string ImportePorArticulos7_14_Otros { get; set; }

        /// <summary>
        /// Importe en euros si la sujeción es por operaciones 
        /// no sujetas en el TAI por reglas de localización. Decimal(12,2).
        /// </summary>
        public string ImporteTAIReglasLocalizacion { get; set; }

        /// <summary>
        /// Constructor clase NoSujeta.
        /// </summary>
        public NoSujeta()
        { 
        }
    }
}
