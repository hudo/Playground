//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;

//namespace CookieConsent.Service
//{
//    public class FileAssetsProvider : IAssetsProvider
//    {
//        private readonly string _fallbackCulture;
//        private readonly IFileShim _fileShim;
//        private static readonly ConcurrentDictionary<string, string> LocalozedHtmlTemplates = new ConcurrentDictionary<string, string>();
//        private static string _jsContent;

//        public FileAssetsProvider(Dictionary<string, string> htmlFileLocation, string jsFileLocation, string fallbackCulture, IFileShim fileShim)
//        {
//            if(string.IsNullOrWhiteSpace(fallbackCulture)) 
//                throw new ArgumentNullException("fallbackCulture", "Fallback culture not specified");

//            if(htmlFileLocation == null)
//                throw new ArgumentNullException("htmlFileLocation");

//            if(!htmlFileLocation.ContainsKey(fallbackCulture))
//                throw new ArgumentException("Fallback culture file location should be also specified in location list");

//            LoadJsAsset(jsFileLocation);

//            LoadHtmlAssets(htmlFileLocation);

//            _fallbackCulture = fallbackCulture;
//            _fileShim = fileShim;
//        }

//        private void LoadHtmlAssets(Dictionary<string, string> htmlFileLocation)
//        {
//            foreach (var culturePair in htmlFileLocation)
//            {
//                if(LocalozedHtmlTemplates.ContainsKey(culturePair.Key)) continue;
                
//                if (_fileShim.Exists(culturePair.Value))
//                    LocalozedHtmlTemplates[culturePair.Key] = _fileShim.ReadAllText(culturePair.Value);
//                else
//                {
//                    Debug.WriteLine(string.Format("Can't load localized html template from [{0}], fallback culture will be used.", culturePair.Value));
//                    LocalozedHtmlTemplates[culturePair.Key] = "";
//                }
//            }
//        }

//        private void LoadJsAsset(string jsFileLocation)
//        {
//            if (_fileShim.Exists(jsFileLocation))
//                _jsContent = _fileShim.ReadAllText(jsFileLocation);
//            else
//                throw new FileNotFoundException("Can't load JS", jsFileLocation);
//        }

//        public string GetHtml(string culture)
//        {
//            if(!LocalozedHtmlTemplates.ContainsKey(culture))    
//                throw new ArgumentException(string.Format("Culture {0} was not registered", culture));

//            var template = LocalozedHtmlTemplates[culture];

//            return string.IsNullOrEmpty(template) ? LocalozedHtmlTemplates[_fallbackCulture] : template;
//        }

//        public string Javascript
//        {
//            get { return _jsContent; }
//        }
//    }
//}