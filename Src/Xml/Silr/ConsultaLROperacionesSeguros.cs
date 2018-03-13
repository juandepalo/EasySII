﻿using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Facturas recibidas.
    /// </summary>
    [Serializable]
    [XmlRoot("ConsultaLROperacionesSeguros")]
    public class ConsultaLROperacionesSeguros
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Filtro consulta.
        /// </summary>
        [XmlElement("FiltroConsulta")]
        public FiltroConsultaOTT FiltroConsulta { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public ConsultaLROperacionesSeguros()
        {
            Cabecera = new Cabecera();
            FiltroConsulta = new FiltroConsultaOTT();
        }
    }
}