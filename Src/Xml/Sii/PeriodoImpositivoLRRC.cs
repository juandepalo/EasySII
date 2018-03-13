using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Perido impositivo
    /// </summary>
    [Serializable]
    [XmlRoot("PeriodoImpositivo", Namespace = Settings.NamespaceSiiRQ)]
    public class PeriodoImpositivoLRRC
    {
        /// <summary>
        /// Ejercicio impositivo de la factura.
        /// Numérico(4).
        /// </summary>
        public string Ejercicio { get; set; }

        /// <summary>
        /// Periodo impositivo de la factura. Alfanumérico(2). L1:
        /// <para>01: Enero</para>
        /// <para>02: Febrero</para>
        /// <para>03: Marzo</para>
        /// <para>04: Abril</para>
        /// <para>05: Mayo</para>
        /// <para>06: Junio</para>
        /// <para>07: Julio</para>
        /// <para>08: Agosto</para>
        /// <para>09: Septiembre</para>
        /// <para>10: Octubre</para>
        /// <para>11: Noviembre</para>
        /// <para>12: Diciembre</para>
        /// <para>0A: Anual</para>
        /// </summary>
        public string Periodo { get; set; }
    }
}
