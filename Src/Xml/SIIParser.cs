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

using EasySII.Xml.Soap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EasySII.Xml
{

    /// <summary>
    /// Espacios de nombres SII.
    /// </summary>
    public enum SIINamespaces
    {
        /// <summary>
        /// Suministro informacion.
        /// </summary>
        sii,
        /// <summary>
        /// SuministroLR
        /// </summary>
        siiLR,
        /// <summary>
        /// ConsultaLR
        /// </summary>
        con,
        /// <summary>
        /// SuministroInformacion
        /// </summary>
        sum
    }

    /// <summary>
    /// Transforma los objetos SII en archivos xml.
    /// </summary>
    public class SIIParser
    {

        /// <summary>
        /// Espacios de nombres utilizados.
        /// </summary>
        public static Dictionary<SIINamespaces, string> Namespaces = new Dictionary<SIINamespaces, string>() {
            {SIINamespaces.sii, Settings.NamespaceSii },
            {SIINamespaces.sum, Settings.NamespaceSii },
            {SIINamespaces.con, Settings.NamespaceSiiLRQ },
            {SIINamespaces.siiLR, Settings.NamespaceSiiLR },
        };

		/// <summary>
		/// Serializa un objeto Envelop en un xml.
		/// </summary>
		/// <param name="envelope">Sobre a enviar.</param>
		/// <param name="ns">Espacio de nombres.</param> 
		/// <param name="nsSum">Espacio de nombres para suministro de 
		/// información (a veces prefijo sii, otras prefijo sum).</param> 
		/// <returns>XML resultado de la serialización.</returns>
		public static string GetXmlText(Envelope envelope, SIINamespaces ns = SIINamespaces.siiLR, SIINamespaces nsSum = SIINamespaces.sii)
		{

			ClearNulls(envelope); // Limpia nulos

			XmlDocument xmlDocument = new XmlDocument();

			XmlSerializer xmlSerializer = new XmlSerializer(envelope.GetType());
			XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

			namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
			namespaces.Add(ns.ToString(), Namespaces[ns]);
			namespaces.Add(nsSum.ToString(), Namespaces[nsSum]);

			byte[] buff = null;

			using (var ms = new MemoryStream())
			{
				using (StreamWriter sw = new StreamWriter(ms, Encoding.GetEncoding("UTF-8")))
				{
					xmlSerializer.Serialize(sw, envelope, namespaces);
					buff = ms.ToArray();
				}
			}

			return Encoding.UTF8.GetString(buff);

		}

		/// <summary>
		/// Serializa un objeto Envelop en un xml, y guarda el
		/// resultado en la ruta especificada.
		/// </summary>
		/// <param name="envelope">Sobre a enviar.</param>
		/// <param name="xmlPath">Ruta del xml.</param>
		/// <param name="ns">Espacio de nombres.</param> 
		/// <param name="nsSum">Espacio de nombres para suministro de 
		/// información (a veces prefijo sii, otras prefijo sum).</param> 
		/// <returns>XmlDocument resultado de la serialización.</returns>
		public static XmlDocument GetXml(Envelope envelope, string xmlPath, SIINamespaces ns = SIINamespaces.siiLR, SIINamespaces nsSum = SIINamespaces.sii)
        {

			ClearNulls(envelope); // Limpia nulos

			XmlDocument xmlDocument = new XmlDocument();

            XmlSerializer xmlSerializer = new XmlSerializer(envelope.GetType());
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

            namespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.Add(ns.ToString(), Namespaces[ns]);
            namespaces.Add(nsSum.ToString(), Namespaces[nsSum]);

            using (StreamWriter sw = new StreamWriter(xmlPath, false, Encoding.GetEncoding("UTF-8")))
            {
                xmlSerializer.Serialize(sw, envelope, namespaces);
            }

            xmlDocument.Load(xmlPath);

            return xmlDocument;

        }

		/// <summary>
		/// Si tiene todas las propiedades hijas nulas o empty, establece
		/// el valor del objeto en null. Esta función se utiliza para poner
		/// a nulo elementos erróneamente inicializados, o que por alguna
		/// cirsunstancia están vacíos y provocan errores de serialización.
		/// </summary>
		/// <param name="instance">Instancia a tratar.</param>
		public static void ClearNulls(object instance)
		{
			GetCleared(instance);
	
		}

		/// <summary>
		/// Devuelve un importe formateado para un campo decimal
		/// de la especificación del SII.
		/// </summary>
		/// <param name="amount">Impote a formatear</param>
		/// <returns>Importe formateado.</returns>
		public static string FromDecimal(decimal amount)
		{
			return amount.ToString(Settings.DefaultNumberFormatInfo);
		}

		/// <summary>
		/// Devuelve una fecha formateada para un campo decimal
		/// de la especificación del SII.
		/// </summary>
		/// <param name="date">fecha a formatear</param>
		/// <returns>Fecha formateada.</returns>
		public static string FromDate(DateTime? date)
		{
			return (date??new DateTime(1, 1, 1)).ToString("dd-MM-yyyy");
		}

		/// <summary>
		/// Convierte en decimal un importe representado por un texto en 
		/// un tag de un xml del SII.
		/// </summary>
		/// <param name="amount">Texto de importe a convertir.</param>
		/// <returns>Importe decimal representado.</returns>
		public static decimal ToDecimal(string amount)
		{
			return Convert.ToDecimal(amount, Settings.DefaultNumberFormatInfo);
		}
	

		/// <summary>
		/// Devuelve el valor de un objeto 'limpio'. Es decir, si el objeto está
		/// vacío devuelve nulo, si no devuelve el valor del objeto. Esta función 
		/// recursiva se utiliza para recorrer un objeto y eliminar tags vacíos en
		/// la serialización xml.
		/// </summary>
		/// <param name="instance">Instancia del objeto a limpiar</param>
		/// <param name="parent">Objeto padre si la llamada es recursiva.</param>
		/// <returns></returns>
		private static object GetCleared(object instance, object parent = null)
		{

			if (instance == null)
				return null;

			// Para strings
			if (instance.GetType() == typeof(string))
			{
				if (string.IsNullOrEmpty(instance.ToString()))
					return null;
				else
					return instance;
			}



			Type instanceType = instance.GetType();

			bool isList = typeof(IList).IsAssignableFrom(instanceType);

			if (isList)
			{
				// Para objetos colecciones
				IList list = instance as IList;
				IList newList = (IList)Activator.CreateInstance(instanceType);

				if (list.Count == 0)
				{
					return null;
				}
				else
				{
					for (int i = 0; i < list.Count; i++)
					{

						object pLisItemValue = GetCleared(list[i]);

						if (pLisItemValue != null)
							newList.Add(pLisItemValue);
					}

					if (newList.Count > 0)
						return newList;
					else
						return null;
				}

			}
			else
			{

				// Para objetos complejos

				bool complexNotNull = false;

				foreach (var pInf in instanceType.GetProperties())
				{
					if (!(pInf.PropertyType.BaseType == typeof(Array)))
					{
						object propValue = GetCleared(pInf.GetValue(instance), instance);// Recursión

						pInf.SetValue(instance, propValue); // Establece valor limpio

						if (propValue != null)
							complexNotNull = true;
					}
				}

				return (complexNotNull) ? instance : null;
			}

		}

	}
}
