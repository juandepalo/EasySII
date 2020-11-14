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
using System;
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
    /// </summary>
    public class RegistroLRDetOperacionIntracomunitariaVentasEnConsigna
    {

        /// <summary>
        /// Clave del declarante.
        /// Numérico(4).
        /// L31 => Clave Declarante
        /// <para>'V': Vendedor.</para>
        /// <para>'A': Adquirente</para>
        /// </summary>
        [XmlElement("ClaveDeclarante", Order = 1, Namespace = Settings.NamespaceSiiLR)]
        public string ClaveDeclarante { get; set; }

        /// <summary>
        /// IdRegistroDeclarado.
        /// </summary>
        [XmlElement("IdRegistroDeclarado", Order = 2, Namespace = Settings.NamespaceSiiLR)]
        public IdRegistroDeclarado IdRegistroDeclarado { get; set; }

        /// <summary>
        /// Tipo de operación
        /// Alfanumérico(2).
        /// L32 => Clave Declarante
        /// <para>'01': Expedición.</para>
        /// <para>'02': Sustitución del destinatario inicial </para>
        /// <para>'03': Entrega al destinatario inicial o al sustituto</para>
        /// <para>'04': Entrega al destinatario inicial o al sustituto</para>
        /// <para>'05': Expedición a otro País </para>
        /// <para>'06': Destrucción, perdida, robo </para>
        /// <para>'07': Devolución a TAI </para>
        /// <para>'08': Transcurso del plazo de 12 meses sin adquisición por el destinatario inicial o sustituto</para>
        /// <para>'09': Recepción</para>
        /// <para>'10': Adquisición</para>
        /// <para>'11': Retirada de bienes por parte del vendedor </para>
        /// <para>'12': Destrucción o desaparición </para>
        /// </summary>
        [XmlElement("TipoOperacion", Order = 3, Namespace = Settings.NamespaceSiiLR)]
        public string TipoOperacion { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Order = 4, Namespace = Settings.NamespaceSiiLR)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("SustitutoDestinatarioInicial", Order = 5, Namespace = Settings.NamespaceSiiLR)]
        public Contraparte SustitutoDestinatarioInicial { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Deposito", Order = 6, Namespace = Settings.NamespaceSiiLR)]
        public Deposito Deposito { get; set; }


        /// <summary>
        /// OperacionIntracomunitaria.
        /// </summary>
        [XmlElement("OperacionIntracomunitaria", Order = 7, Namespace = Settings.NamespaceSiiLR)]
        public OperacionIntracomunitariaVentasEnConsigna OperacionIntracomunitaria { get; set; }

    }
}
