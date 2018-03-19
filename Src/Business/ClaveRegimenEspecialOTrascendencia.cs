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

namespace EasySII.Business
{
    /// <summary>
    /// L3.1  Clave de régimen especial o trascendencia en facturas expedidas
    /// </summary>
    public enum ClaveRegimenEspecialOTrascendencia
    {
        /// <summary>
        /// Operación de régimen común
        /// </summary>
        RegimenComun = 1,
        /// <summary>
        /// Emitidas: Exportación. Recibidas: Operaciones por las que los
        /// empresarios satisfacen compensaciones EAGYP.
        /// </summary>
        ExportacionREAGYP,
        /// <summary>
        /// Operaciones a las que se aplique el régimen especial de bienes usados, objetos de arte,
        /// antigüedades y objetos de colección (135-139 de LIVA),
        /// </summary>
        EspecialBienesUsados,
        /// <summary>
        /// Régimen especial oro de inversión
        /// </summary>
        EspecialOroInversion,
        /// <summary>
        /// Régimen especial agencias de viajes
        /// </summary>
        EspecialAgenciaViajes,
        /// <summary>
        /// Régimen especial grupo de entidades en IVA (Nivel Avanzado)
        /// </summary>
        EspecialGrupoEntidadesAvanzado,
        /// <summary>
        /// Régimen especial criterio de caja
        /// </summary>
        EspecialCriterioCaja,
        /// <summary>
        /// Operaciones sujetas al IPSI / IGIC
        /// </summary>
        IpsiIgic,
        /// <summary>
        /// Emitidas: Facturación de las prestaciones de servicios de agencias de viaje que actúan como 
        /// mediadoras en nombre y por cuenta ajena (D.A.4ª RD1619/2012). Recibidas: Adquisiones
        /// intracomunitarias bienes y servicios.
        /// </summary>
        AgenciasViajeCuentaAjenaAdqIntracom,
        /// <summary>
        /// Emitidas: Cobros por cuenta de terceros de honorarios profesionales o de Dº derivados 
        /// de la propiedad industrial, de autor u otros por cuenta de sus socios, 
        /// asociados o colegiados efectuados por sociedades, asociaciones, colegios 
        /// profesionales u otras entidades que, entre sus funciones, 
        /// realicen las de cobro. Recibidas: Viajes.
        /// </summary>
        CobrosCuentaTercerosCompraAgViajes,
        /// <summary>
        /// Emitidas: Operaciones de arrendamiento de local de negocio sujetas a retención.
        /// Recibidas: Viaje cta ajena.
        /// </summary>
        ArrendamientoLocalNegocioRetencionAgViajesCtaAjena,
        /// <summary>
        /// Operaciones de arrendamiento de local de negocio no sujetos a retención
        /// </summary>
        ArrendamientoLocalNegocioNoRetencion,
        /// <summary>
        /// Emitidas: Operaciones de arrendamiento de local de negocio sujetas y no sujetas a retención.
        /// Recibidas: Importaciones no DUA.
        /// </summary>
        ArrendamientoLocalNegocioAmbosImportacionesNoDua,
        /// <summary>
        /// Emitidas: Certificaciones obra destinatario admón. pública
        /// Recibidas: Envíos del primer semestre de 2017. Primer semestre 2017 y otras facturas anteriores a la inclusión en el SII
        /// </summary>
        FacturaIvaPteDevengoRecibidas2017Sem1,
        /// <summary>
        /// Emitidas: Tracto sucesivo.
        /// </summary>
        FacturaIvaPteDevengoTractoSucesivo,
        /// <summary>
        /// Emitidas: Primer semestre 2017 y otras facturas anteriores a la inclusión en el SII
        /// </summary>
        Emitidas2017Sem1
    };


}
