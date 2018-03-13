using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySII.Business
{
    /// <summary>
    /// Datos y métodos generales de la clase business.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Prefijos de nombre de archivo de cada uno de los tipos de envíos.
        /// </summary>
        public static Dictionary<string, string> BatchFilePrefixes = new Dictionary<string, string>()
        {
            {"LRFE", "Lote facturas emitidas" },                            //   1
            {"LRFR", "Lote facturas recibidas" },                           //   2
            {"LROI", "Lote operaciones intracomunitarias" },                //   3
            {"LCFE", "Lote pagos facturas emitidas" },                      //   4
            {"LPFR", "Lote pagos facturas recibidas" },                     //   5
            {"DRFE", "Lote borrado facturas emitidas" },                    //   6
            {"DROI", "Lote borrado operaciones intracomunitarias" },        //   7
            {"DRFR", "Lote borrado facturas recibidas" },                   //   8
            {"QRFE", "Consulta facturas emitidas" },                        //   9
            {"QRFR", "Consulta facturas recibidas" },                       //  10
            {"QROI", "Consulta operaciones intracomunitarias" },            //  11
            {"LRBI", "Lote bienes inversion" },                             //  12
            {"DRBI", "Lote borrado bienes inversion" },                     //  13
            {"QRBI", "Consulta bienes inversion" },                         //  14
            {"LRCM", "Lote cobros en metalico" },                           //  15
            {"DRCM", "Lote borrado cobros en metalico" },                   //  16
            {"QRCM", "Consulta cobros en metalico" },                       //  17
        };
    }
}
