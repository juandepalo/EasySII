using EasySII.Xml.Soap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
		/// Serializa un objeto Envelop en un xml, y guarda el
		/// resultado en la ruta especificada.
		/// </summary>
		/// <param name="envelope">Sobre a enviar.</param>
		/// <param name="xmlPath">Ruta del xml.</param>
		/// <param name="ns">Espacio de nombres.</param> 
		/// <param name="nsSum">Espacio de nombres para suministro de 
		/// información (a veces prefijo sii, otras prefijo sum).</param> 
		/// <returns></returns>
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
