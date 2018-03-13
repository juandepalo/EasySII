using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de facturas emitidas.
    /// </summary>
    [Serializable]
    public class RegistroLROpTrascendTribu
    {
        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
        /// </summary>
        [XmlElement("PeriodoImpositivo", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoImpositivo { get; set; }

        /// <summary>
        /// Datos del perkiodo impositivo. 
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("PeriodoLiquidacion", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoLiquidacion { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSiiLR)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Clave que identificará el tipo de operación. Lista L21.
        /// <para>A: Indemnizaciones o prestaciones satisfechas superiores a 3005,06</para>
        /// <para>B: Primas o contraprestaciones percibidas superiores a 3005,06</para>
        /// </summary>
        [XmlElement("ClaveOperacion", Namespace = Settings.NamespaceSii)]
        public string ClaveOperacion { get; set; }


        /// <summary>
        /// Importe total del Seguro o Cobro en Metállico.
        /// </summary>
        [XmlElement("ImporteTotal", Namespace = Settings.NamespaceSii)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public RegistroLROpTrascendTribu()
        {
            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            Contraparte = new Contraparte();
        }
    }
}