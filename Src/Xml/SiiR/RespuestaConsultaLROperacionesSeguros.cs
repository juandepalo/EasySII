/*
    This file is part of the EasySII (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
        http://www.irenesolutions.com/terms-of-use.pdf
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the EasySII software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving sii XML data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Libro de registro de Operaciones Seguros
    /// </summary>
    [Serializable]
    [XmlRoot("RespuestaConsultaLROperacionesSeguros")]
    public class RespuestaConsultaLROperacionesSeguros
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSiiRQ)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
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
        [XmlElement("RegistroRespuestaConsultaOperacionesSeguros", Namespace = Settings.NamespaceSiiRQ)]
        public List<RegistroRCLROperacionesSeguros> RegistroRCLROperacionesSeguros { get; set; }

        /// <summary>
        /// Constructor de la clase RespuestaConsultaLRCobrosMetalico.
        /// </summary>
        public RespuestaConsultaLROperacionesSeguros()
        {
            Cabecera = new Cabecera();

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            RegistroRCLROperacionesSeguros = new List<RegistroRCLROperacionesSeguros>();
        }
    }
}
