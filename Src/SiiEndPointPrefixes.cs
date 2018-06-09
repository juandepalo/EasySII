using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySII
{

    /// <summary>
    /// Prefijos de los endpoints para los web service del
    /// SII de la AEAT: Pruebas y producción.
    /// </summary>
    public class SiiEndPointPrefixes
    {

        /// <summary>
        /// Prefijo del endpoint de purebas.
        /// </summary>
        public const string Test = "https://www7.aeat.es/wlpl/SSII-FACT/ws";

        /// <summary>
        /// Prefijo del endpoint de producción.
        /// </summary>
        public const string Prod = "https://www1.agenciatributaria.gob.es/wlpl/SSII-FACT/ws";

        /// <summary>
        /// Prefijo del endpoint de purebas diputación Bizkaia.
        /// </summary>
        public const string TestBizkaia = "https://pruapps.bizkaia.eus/SSII-FACT/ws";

        /// <summary>
        /// Prefijo del endpoint de producción diputación Bizkaia.
        /// </summary>
        public const string ProdBizkaia = "https://sii.bizkaia.eus/SSII-FACT/ws";

    }
}
