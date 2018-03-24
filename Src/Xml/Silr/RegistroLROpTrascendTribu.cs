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
    serving extract PDFs data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

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
        [XmlElement("ImporteTotal", Namespace = Settings.NamespaceSiiLR)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Versión 1.1
        /// NombreRazon + NIF de la entidad sucedida como 
        /// consecuencia de una operación de reestructuración.
        /// </summary>
        [XmlElement("EntidadSucedida", Namespace = Settings.NamespaceSii)]
        public Parte EntidadSucedida { get; set; }

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