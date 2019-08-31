using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZbW.Testing.Dms.Client.Model
{
    [ExcludeFromCodeCoverage]
    class XmlHandler : BindableBase
    {
        //Declatation
        string _documentFilePath;
        string _xmlFilePath;

        //Properties
        public string DocumentFilePath
        {
            get
            {
                return _documentFilePath;
            }

            set
            {
                SetProperty(ref _documentFilePath, value);
            }
        }
        public string XmlFilePath
        {
            get
            {
                return _xmlFilePath;
            }

            set
            {
                SetProperty(ref _xmlFilePath, value);
            }
        }

        public XmlHandler(String fileDestinationpath)
        {
            DocumentFilePath = fileDestinationpath;
        }

        public void CreateXML(String fileName, String bezeichnung, DateTime? valutaDatum, String selectedTypItem, String stichwoerter, String benutzer)
        {
            XmlWriter xml = XmlWriter.Create(XmlFilePath);

            xml.WriteStartElement("Document");
            xml.WriteElementString("Filename", fileName);
            xml.WriteElementString("GUIDFilePath", DocumentFilePath);
            xml.WriteElementString("Bezeichnung", bezeichnung);
            xml.WriteElementString("ValutaDatum", valutaDatum.ToString());
            xml.WriteElementString("Typ", selectedTypItem);
            xml.WriteElementString("Stichwoerter", stichwoerter);
            xml.WriteElementString("ErfassungsDatum", DateTime.Now.ToString());
            xml.WriteElementString("Benutzer", benutzer);
            xml.WriteEndElement();
            xml.Flush();          
        }

        public void GenerateXmlPath(String fullPathDestination, String guid)
        {
            XmlFilePath = Path.Combine(fullPathDestination, guid) + "_Metadata.xml";
        }
    }
}
