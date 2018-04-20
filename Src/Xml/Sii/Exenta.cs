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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Exenta.
    /// </summary>
    public class Exenta
    {

        /// <summary>
        /// Exenta a partir de la versión 1.1
        /// </summary>
        [XmlElement("DetalleExenta", Namespace = Settings.NamespaceSii)]
        public List<Exenta> DetalleExenta { get; set; }

        /// <summary>
        /// Causa de exención de operaciones sujetas y exentas. Lista L9:
        /// <para>E1: Exenta por el artículo 20</para>
        /// <para>E2: Exenta por el artículo 21</para>
        /// <para>E3: Exenta por el artículo 22</para>
        /// <para>E4: Exenta por el artículo 24</para>
        /// <para>E5: Exenta por el artículo 25</para>
        /// <para>E6: Exenta por Otros</para>
        /// </summary>
        public string CausaExencion { get; set;}

        /// <summary>
        /// Base imponible exenta.
        /// </summary>
        public string BaseImponible { get; set; }

        /// <summary>
        /// Constructor clase Exenta.
        /// </summary>
        public Exenta()
        {    
        }
    }
}
