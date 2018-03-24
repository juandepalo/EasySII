using System;
using System.Xml.Serialization;


namespace EasySII.Xml.VNifV2Ent
{
	/// <summary>
	/// Perido impositivo
	/// </summary>
	[Serializable]
	[XmlRoot("Contribuyente", Namespace = Settings.NamespaceVNifV2Ent)]
	public class Contribuyente
	{
		/// <summary>
		/// NIF del contribuyente.
		/// Numérico(4).
		/// </summary>
		public string Nif { get; set; }

		/// <summary>
		/// Nombre del contribuyente.	
		/// </summary>
		public string Nombre { get; set; }

		/// <summary>
		/// Nombre del contribuyente.	
		/// </summary>
		public string Resultado { get; set; }
	}
}
