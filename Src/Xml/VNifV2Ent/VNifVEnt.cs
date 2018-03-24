using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace EasySII.Xml.VNifV2Ent
{
	/// <summary>
	/// Datos de contribuyentes
	/// </summary>
	[Serializable]
	[XmlRoot("VNifV2Ent", Namespace = Settings.NamespaceVNifV2Ent)]
	public class VNifVEnt
	{
		/// <summary>
		/// NIF del contribuyente.
		/// Numérico(4).
		/// </summary>
		public List<Contribuyente> Contribuyente { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public VNifVEnt()
		{
			Contribuyente = new List<Contribuyente>();
		}

	}
}
