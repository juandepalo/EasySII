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

using EasySII.Net;
using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace EasySII
{
    /// <summary>
    /// Configuración.
    /// </summary>
    [Serializable]
    [XmlRoot("EasySIISettings")]
    public class Settings
    {
        /// <summary>
        /// Configuración actual.
        /// </summary>
        static Settings _Current;

        /// <summary>
        /// Formato de importes para los xml del sii.
        /// </summary>
        internal static NumberFormatInfo DefaultNumberFormatInfo = new NumberFormatInfo();

        internal static string DefaultNumberDecimalSeparator = ".";

        /// <summary>
        /// Ruta al directorio de configuración.
        /// </summary>
        internal static string Path = System.Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData) + "\\EasySII\\";

        /// <summary>
        /// Espacio de nombres servicio web
        /// para validación NIF (Entrada).
        /// </summary>
        internal const string NamespaceVNifV2Ent = "http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Ent.xsd";

        /// <summary>
        /// Espacio de nombres servicio web
        /// para validación NIF (Salida).
        /// </summary>
        internal const string NamespaceVNifV2Sal = "http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Sal.xsd";


        /// <summary>
        /// Prefijo de espacios de nombres estatal (AEAT).
        /// </summary>
        internal const string NamespacePrefix = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/";

        /// <summary>
        /// Prefijo de espacios de nombres pais Vasco Bizkaia.
        /// </summary>
        internal const string NamespacePrefixBizkaia = "http://www.bizkaia.eus/ogasuna/sii/documentos/";

        #if BIZKAIA

        /// <summary>
        /// Espacio de nombres SIILR
        /// </summary>
        internal const string NamespaceSiiLR = NamespacePrefixBizkaia + "SuministroLR.xsd"; 

        /// <summary>
        /// Espacio de nombres SIILR
        /// </summary>
        internal const string NamespaceSiiLRQ = NamespacePrefixBizkaia + "ConsultaLR.xsd"; 

        /// <summary>
        /// Espacio de nombres SII
        /// </summary>
        internal const string NamespaceSii = NamespacePrefixBizkaia + "SuministroInformacion.xsd"; 


        /// <summary>
        /// Espacio de nombres SII
        /// </summary>
        internal const string NamespaceSiiR = NamespacePrefixBizkaia + "RespuestaSuministro.xsd";

        /// <summary>
        /// Espacio de nombres SII
        /// </summary>
        internal const string NamespaceSiiRQ = NamespacePrefixBizkaia + "RespuestaConsultaLR.xsd";

        #else

        /// <summary>
        /// Espacio de nombres SIILR
        /// </summary>
        internal const string NamespaceSiiLR = NamespacePrefix + "SuministroLR.xsd"; 

        /// <summary>
        /// Espacio de nombres SIILR
        /// </summary>
        internal const string NamespaceSiiLRQ = NamespacePrefix + "ConsultaLR.xsd"; 

        /// <summary>
        /// Espacio de nombres SII
        /// </summary>
        internal const string NamespaceSii = NamespacePrefix + "SuministroInformacion.xsd"; 


        /// <summary>
        /// Espacio de nombres SII
        /// </summary>
        internal const string NamespaceSiiR = NamespacePrefix + "RespuestaSuministro.xsd";

        /// <summary>
        /// Espacio de nombres SII
        /// </summary>
        internal const string NamespaceSiiRQ = NamespacePrefix + "RespuestaConsultaLR.xsd";


        #endif

        /// <summary>
        /// Nombre del fichero de configuración.
        /// </summary>
        internal static string FileName = "EasySII.xml";
 

        /// <summary>
        /// Configuración en curso.
        /// </summary>
        public static Settings Current
        {
            get
            {
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }

        /// <summary>
        /// Identificación de la versión del esquema utilizado para
        ///el intercambio de información con la AEAT mediante el SII.
        /// </summary>
        [XmlElement("IDVersionSii")]
        public string IDVersionSii { get; set; }

        /// <summary>
        /// Ruta al directorio que actuará como bandeja de entrada.
        /// En este directorio se almacenarán todos los mensajes
        /// recibidos de la AEAT mediante el SII.
        /// </summary>
        [XmlElement("InboxPath")]
        public string InboxPath { get; set; }

        /// <summary>
        /// Ruta al directorio que actuará como bandeja de salida.
        /// En este directorio se almacenará una copia de cualquier
        /// envío realizado a la AEAT mediante el SII.
        /// </summary>
        [XmlElement("OutboxPath")]
        public string OutboxPath { get; set; }

        /// <summary>
        /// Número de serie del certificado a utilizar. Mediante este número
        /// de serie se selecciona del almacén de certificados de windows
        /// el certificado con el que realizar las comunicaciones.
        /// </summary>
        [XmlElement("CertificateSerial")]
        public string CertificateSerial { get; set; }

        /// <summary>
        /// EndPoint del web service de la AEAT.
        /// </summary>
        [XmlElement("SiiEndPointPrefix")]
        public string SiiEndPointPrefix { get; set; }

        /// <summary>
        /// EndPoint del web service de la AEAT para
        /// validación del NIF.
        /// </summary>
        [XmlElement("VNifV2EndPointPrefix")]
        public string VNifV2EndPointPrefix { get; set; }


        /// <summary>
        /// Constructor estático de la clase Settings.
        /// </summary>
        static Settings()
        {
            DefaultNumberFormatInfo.NumberDecimalSeparator = 
                DefaultNumberDecimalSeparator;

            Get();
        }

        /// <summary>
        /// Guarda la configuración en curso actual.
        /// </summary>
        public static void Save()
        {

            CheckDirectories();

            string FullPath = Path + "\\" + FileName;

            XmlSerializer serializer = new XmlSerializer(Current.GetType());

            using (StreamWriter w = new StreamWriter(FullPath))
            {
                serializer.Serialize(w, Current);
            }

        }

		/// <summary>
		/// Esteblece el archivo de configuración con el cual trabajar.
		/// </summary>
		/// <param name="fileName">Nombre del archivo de configuración a utilizar.</param>
		public static void SetConfigFileName(string fileName)
		{
			FileName = fileName;
			Get();
		}

        /// <summary>
        /// Inicia estaticos.
        /// </summary>
        /// <returns>La configuración cargada.</returns>
        internal static Settings Get()
        {

            _Current = new Settings();
      

            string FullPath = Path + "\\" + FileName;

            XmlSerializer serializer = new XmlSerializer(_Current.GetType());
            if (File.Exists(FullPath))
            {
                using (StreamReader r = new StreamReader(FullPath))
                {
                    _Current = serializer.Deserialize(r) as Settings;
                }
            }
            else
            {
                _Current.IDVersionSii = "1.0"; 
                _Current.InboxPath = Path + "Inbox\\";
				_Current.OutboxPath = Path + "Outbox\\";
				_Current.CertificateSerial = "3D327B0B";
				_Current.SiiEndPointPrefix = "https://www1.agenciatributaria.gob.es/wlpl/SSII-FACT/ws";
                _Current.VNifV2EndPointPrefix = "https://www1.agenciatributaria.gob.es/wlpl/BURT-JDIT/ws";
            }

            CheckDirectories();

            return _Current;
        }

        /// <summary>
        /// Aseguro existencia de directorios de trabajo.
        /// </summary>
        private static void CheckDirectories()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            if (!Directory.Exists(_Current.InboxPath))
                Directory.CreateDirectory(_Current.InboxPath);

            if (!Directory.Exists(_Current.OutboxPath))
                Directory.CreateDirectory(_Current.OutboxPath);
        }
 
    }
}
