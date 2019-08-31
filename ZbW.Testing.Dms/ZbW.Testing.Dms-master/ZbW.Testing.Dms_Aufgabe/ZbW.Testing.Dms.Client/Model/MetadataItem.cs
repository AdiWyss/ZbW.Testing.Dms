using System;
using Prism.Mvvm;

namespace ZbW.Testing.Dms.Client.Model
{
    internal class MetadataItem : BindableBase
    {
        public string _bezeichnung;

        public DateTime? _valutaDatum;

        public string _typ;

        public string _stichwoerter;

        public DateTime _erfassungsdatum;

        public string _benutzer;

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


        public DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                SetProperty(ref _erfassungsdatum, value);
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

        public DateTime? ValutaDatum
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

        public MetadataItem(String bezeichnung, DateTime valutaDatum, String typ, String stichwoerter, DateTime erfassungsdatum, String benutzer)
        {
            Bezeichnung = bezeichnung;
            ValutaDatum = valutaDatum;
            Typ = typ;
            Stichwoerter = stichwoerter;
            Erfassungsdatum = erfassungsdatum;
            Benutzer = benutzer;
        }
    }
}