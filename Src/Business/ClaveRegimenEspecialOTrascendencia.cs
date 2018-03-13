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
