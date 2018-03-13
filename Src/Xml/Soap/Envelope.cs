using System;
using System.IO;
using System.Xml.Serialization;

namespace EasySII.Xml.Soap
{
    /// <summary>
    /// Representacion de envelope (sobre) para SOAP. 
    /// Sobre: el cual define qué hay en el mensaje y cómo procesarlo.
    /// </summary>
    [Serializable]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        /// <summary>
        /// SOAP Header.
        /// </summary>
        [XmlElement]
        public Header Header { get; set; }


        /// <summary>
        /// Body del envelope SOAP.
        /// </summary>
        [XmlElement]
        public Body Body { get; set; }

        /// <summary>
        /// Constructor clase Envelope.
        /// </summary>
        public Envelope()
        {
            Header = new Header();
            Body = new Body();
        }

        /// <summary>
        /// Constructor clase Envelope.
        /// </summary>
        /// <param name="xmlPath">Ruta al archivo xml que contiene el mensaje SOAP.</param>
        public Envelope(string xmlPath)
        {        

            Envelope envelope = null;

            XmlSerializer serializer = new XmlSerializer(this.GetType());
            if (File.Exists(xmlPath))
            {
                using (StreamReader r = new StreamReader(xmlPath))
                {
                    envelope = serializer.Deserialize(r) as Envelope;
                }
            }

            if (envelope == null)
                throw new Exception("XML SOAP selerailization error");

            Header = envelope.Header;
            Body = envelope.Body;

			SIIParser.ClearNulls(Body);

		}

    }
}
