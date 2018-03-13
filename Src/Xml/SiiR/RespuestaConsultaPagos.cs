﻿using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Respuesta de la consulta de pagos sobre facturas recibidas.
    /// </summary>
    [Serializable]
    [XmlRoot("RespuestaConsultaPagos")]
    public class RespuestaConsultaPagos
    {

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSiiRQ)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Datos de Periodo Impositivo.
        /// </summary>
        [XmlElement("PeriodoImpositivo", Namespace = Settings.NamespaceSiiRQ)]
        public PeriodoImpositivo PeriodoImpositivo { get; set; }


        /// <summary>
        /// Datos del perkiodo impositivo. 
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("PeriodoLiquidacion", Namespace = Settings.NamespaceSiiRQ)]
        public PeriodoImpositivo PeriodoLiquidacion { get; set; }

        /// <summary>
        /// Indicador de paginación.
        /// </summary>
        [XmlElement("IndicadorPaginacion", Namespace = Settings.NamespaceSiiRQ)]
        public string IndicadorPaginacion { get; set; }

        /// <summary>
        /// Resultado de la consulta.
        /// </summary>
        [XmlElement("ResultadoConsulta", Namespace = Settings.NamespaceSiiRQ)]
        public string ResultadoConsulta { get; set; }

        /// <summary>
        /// Lista de facturas recibidas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroRespuestaConsultaPagos", Namespace = Settings.NamespaceSiiRQ)]
        public List<RegistroRespuestaConsultaPagos> RegistroRespuestaConsultaPagos { get; set; }

        /// <summary>
        /// Constructor de la clase RespuestaConsultaCobros.
        /// </summary>
        public RespuestaConsultaPagos()
        {
            Cabecera = new Cabecera();

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            RegistroRespuestaConsultaPagos = new List<RegistroRespuestaConsultaPagos>();
        }
    }
}