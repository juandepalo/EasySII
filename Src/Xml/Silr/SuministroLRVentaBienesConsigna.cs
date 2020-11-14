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

using EasySII.Business.Batches;
using EasySII.Xml.Sii;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{

    /// <summary>
    /// Libro registro de Determinadas Operaciones Intracomunitarias. Venta de
    /// bienes en consigna. El objetivo de este Libro registro es reflejar la 
    /// situación de los bienes a que se refieren determinadas operaciones 
    /// intracomunitarias en tanto no tenga lugar el devengo de las entregas 
    /// o de las adquisiciones.
    /// <para>El envío o recepción de bienes muebles corporales para la realización 
    /// de informes periciales, valoraciones y dictámenes sobre los mismos.</para>
    /// <para>Las transferencias que realice el sujeto pasivo de bienes corporales 
    /// de su empresa con destino a otro Estado miembro, para afectarlos a las 
    /// necesidades de aquélla en este último.</para>  
    /// <para>https://www.agenciatributaria.es/AEAT.internet/Inicio/_Segmentos_/Empresas_y_profesionales/Empresas/IVA/Obligaciones_contables_y_registrales_en_el_IVA/Libro_registro_de_determinadas_operaciones_intracomunitarias_.shtml</para>    
    /// </summary>
    public class SuministroLRVentaBienesConsigna : ISiiLote
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Order = 1, Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLRDetOperacionIntracomunitariaVentasEnConsigna", Order = 2)]
        public List<RegistroLRDetOperacionIntracomunitariaVentasEnConsigna> RegistroLRDetOperacionIntracomunitariaVentasEnConsigna { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRVentaBienesConsigna.
        /// </summary>
        public SuministroLRVentaBienesConsigna()
        {
            Cabecera = new Cabecera();
            RegistroLRDetOperacionIntracomunitariaVentasEnConsigna = new List<RegistroLRDetOperacionIntracomunitariaVentasEnConsigna>();
        }


    }
}
