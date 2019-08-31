using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace ZbW.Testing.Dms.Client.Model 
{
    public class FileHandler : BindableBase
    {

        //Declaration
        private string _repositoryPath;
        private string _stichwoerter;
        private string _bezeichnung;
        private string _fullFilePath;
        private string _fileName;
        private string _typ;
        private DateTime _erfassungsDatum;
        private DateTime _valutaDatum;
        private string _benutzer;
        private List<MetadataItem> _foundDocuments;
        XmlDocument doc;
        private readonly IGuid _guid;

        //Properties
        public string RepositoryPath
        {
            get
            {
                return _repositoryPath;
            }

            set
            {
                SetProperty(ref _repositoryPath, value);
            }
        }
        public string Stichwoerter
        {
            get
            {
                return _stichwoerter;
            }

            set
            {
                SetProperty(ref _stichwoerter, value);
            }
        }
        public string Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }

            set
            {
                SetProperty(ref _bezeichnung, value);
            }
        }
        public string FullFilePath
        {
            get
            {
                return _fullFilePath;
            }

            set
            {
                SetProperty(ref _fullFilePath, value);
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                SetProperty(ref _fileName, value);
            }
        }
        public string Typ
        {
            get
            {
                return _typ;
            }

            set
            {
                SetProperty(ref _typ, value);
            }
        }
        public string Benutzer
        {
            get
            {
                return _benutzer;
            }

            set
            {
                SetProperty(ref _benutzer, value);
            }
        }
        public DateTime ErfassungsDatum
        {
            get
            {
                return _erfassungsDatum;
            }

            set
            {
                SetProperty(ref _erfassungsDatum, value);
            }
        }
        public DateTime ValutaDatum
        {
            get
            {
                return _valutaDatum;
            }

            set
            {
                SetProperty(ref _valutaDatum, value);
            }
        }
        public List<MetadataItem> FoundDocuments
        {
            get
            {
                return _foundDocuments;
            }

            set
            {
                SetProperty(ref _foundDocuments, value);
            }
        }

        public FileHandler(String repositoryPath, IGuid guid)
        {
            RepositoryPath = repositoryPath;
            _guid = guid;
        }

        public string CreateGUID()
        {
            return _guid.CreateGUID();
        }

        public string GetDestinationPath(DateTime? valutaDatum)
        {
            String destinationPath;
            destinationPath = Path.Combine(_repositoryPath, valutaDatum.Value.Year.ToString());

            return destinationPath;
        }

        public string GetFileDestinationPath(String destinationPath, String guid, String fileName)
        {
            String fileDestinationPath;
            fileDestinationPath = Path.Combine(destinationPath, guid) + "_Content" + Path.GetExtension(fileName);

            return fileDestinationPath;
        }

        public void CreateDir(String dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

        }

        public void CreateRepository(String repository)
        {
            CreateDir(repository);
        }

        public void MoveFile(String filePath, String fullFilePathDestination, String xml, Boolean isRemoveFileEnabled)
        {
            if (File.Exists(xml))
            {
                if (isRemoveFileEnabled)
                {
                    System.IO.File.Move(filePath, fullFilePathDestination);
                }
                else
                {
                    System.IO.File.Copy(filePath, fullFilePathDestination);
                }
            }
 
        }

        public List<MetadataItem> GetDocuments(string selectedTypItem, string suchbegriff)
        {
            //try
            //{
                FoundDocuments = new List<MetadataItem>();
                foreach (string directories in Directory.GetDirectories(RepositoryPath))
                {
                    foreach (string file in Directory.GetFiles(directories, "*.xml"))
                    {
                        doc = new XmlDocument();
                        doc.Load(file);
                        XmlNodeList elemList = doc.GetElementsByTagName("Typ");
                        Typ = elemList[0].InnerXml;
                        elemList = doc.GetElementsByTagName("Stichwoerter");
                        Stichwoerter = elemList[0].InnerXml;
                        elemList = doc.GetElementsByTagName("Bezeichnung");
                        Bezeichnung = elemList[0].InnerXml;

                        if (selectedTypItem != null && suchbegriff.Equals(""))
                        {
                            if (Typ == selectedTypItem)
                            {
                                LoadDocumentProperties(elemList, doc, FoundDocuments);
                            }
                        }
                        else if (selectedTypItem == null && !suchbegriff.Equals(""))
                        {
                            if (Stichwoerter.Contains(suchbegriff) || Bezeichnung.Contains(suchbegriff))
                            {
                                LoadDocumentProperties(elemList, doc, FoundDocuments);
                            }
                        }
                        else if (selectedTypItem != null && !suchbegriff.Equals(""))
                        {
                            if (Typ == selectedTypItem && (Stichwoerter.Contains(suchbegriff) || Bezeichnung.Contains(suchbegriff)))
                            {
                                LoadDocumentProperties(elemList, doc, FoundDocuments);
                            }
                        }
                        else
                        {
                            throw new System.ArgumentException("Type AND searchterm can't be empty");
                        }
                    }
                }
                return FoundDocuments;
            //}
            //catch (ArgumentException e)
            //{
            //    MessageBox.Show(e.Message);
            //    return null;
            //}
            
        }

        private void LoadDocumentProperties(XmlNodeList elemList, XmlDocument doc, List<MetadataItem> foundDocuments)
        {
            elemList = doc.GetElementsByTagName("ValutaDatum");
            ValutaDatum = Convert.ToDateTime(elemList[0].InnerXml);
            elemList = doc.GetElementsByTagName("ErfassungsDatum");
            ErfassungsDatum = Convert.ToDateTime(elemList[0].InnerXml);
            elemList = doc.GetElementsByTagName("Benutzer");
            Benutzer = elemList[0].InnerXml;
            elemList = doc.GetElementsByTagName("Filename");
            FileName = elemList[0].InnerXml;
            elemList = doc.GetElementsByTagName("GUIDFilePath");
            FullFilePath = elemList[0].InnerXml;
            foundDocuments.Add(new MetadataItem(Bezeichnung, ValutaDatum, Typ, Stichwoerter, ErfassungsDatum, Benutzer, FullFilePath, FileName));
        }

    }
}
