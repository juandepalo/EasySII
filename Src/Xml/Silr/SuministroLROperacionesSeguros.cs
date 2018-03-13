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
    [XmlRoot("SuministroLROperacionesSeguros")]
    public class SuministroLROperacionesSeguros
    {
        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Lista de Operaciones de Seguros con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroLROperacionesSeguros")]
        public List<RegistroLROpTrascendTribu> RegistroLROperacionesSeguros { get; set; }

        /// <summary>
        /// Constructor de la clase SuministroLRFacturasEmitidas.
        /// </summary>
        public SuministroLROperacionesSeguros()
        {
            Cabecera = new Cabecera();
            RegistroLROperacionesSeguros = new List<RegistroLROpTrascendTribu>();
        }
    }
}