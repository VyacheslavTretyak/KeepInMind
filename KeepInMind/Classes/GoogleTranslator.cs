using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using Google.Apis.Translate.v3beta1;
using Google.Apis.Util.Store;
using Google.Cloud.Translation.V2;

namespace KeepInMind.Classes
{
	class GoogleTranslator
	{
		private string fromLanguage;
		private string toLanguage;
		public GoogleTranslator()
		{
			fromLanguage = WordsManager.Instance.GetConfig().FromLanguage;
			toLanguage = WordsManager.Instance.GetConfig().ToLanguage;
		}

		public string Translate(string text)
		{
			return Task<string>.Run(() => TranslateTask(text)).Result;
		}

		private string TranslateTask(string text)
		{			
			TranslationClient client = TranslationClient.Create();
			var response = client.TranslateText(text, toLanguage, fromLanguage);
			return response.TranslatedText;
		}
	}
}
