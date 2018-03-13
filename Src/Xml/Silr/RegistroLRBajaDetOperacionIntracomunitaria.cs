﻿using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de datos para baja de facturas recibidas.
    /// </summary>
    [Serializable]
    public class RegistroLRBajaDetOperacionIntracomunitaria
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
        /// Identicador unívoco de la factura. Número+serie que identifica a la ultima factura 
        /// cuando el Tipo de Factura es un asiento resumen de facturas.
        /// </summary>
        public IDFactura IDFactura { get; set; }


        /// <summary>
        /// Constructor clase RegistroLRBajaRecibidas.
        /// </summary>
        public RegistroLRBajaDetOperacionIntracomunitaria()
        {
            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            IDFactura = new IDFactura();
        }
    }
}