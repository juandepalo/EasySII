using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Libro de registro de Operaciones con Transcendencia Tributaria
    /// </summary>
    [Serializable]
    [XmlRoot("SuministroLRCobrosMetalico")]
    public class SuministroLRCobrosMetalico
    {
        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de Operaciones de Seguros con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLRCobrosMetalico")]
        public List<RegistroLROpTrascendTribu> RegistroLRCobrosMetalico { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLRCobrosMetalico()
        {
            Cabecera = new Cabecera();
            RegistroLRCobrosMetalico = new List<RegistroLROpTrascendTribu>();
        }
    }
}