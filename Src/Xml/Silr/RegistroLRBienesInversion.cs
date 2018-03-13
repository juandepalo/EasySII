﻿using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de bienes inversión.
    /// </summary>
    [Serializable]
    public class RegistroLRBienesInversion
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
        /// Identicador unívoco de la factura. Número+serie 
        /// que identifica a la ultima factura.
        /// </summary>
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Detalle de datos del bien inversión.
        /// </summary>
        [XmlElement("BienesInversion", Namespace = Settings.NamespaceSiiLR)]
        public BienesInversion BienesInversion { get; set; }

        /// <summary>
        /// Constructor clase RegistroLRBienesInversion.
        /// </summary>
        public RegistroLRBienesInversion()
        {
            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            IDFactura = new IDFactura();
            BienesInversion = new BienesInversion();
        }
    }
}