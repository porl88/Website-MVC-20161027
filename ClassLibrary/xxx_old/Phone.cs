using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
	public class Phone
	{

		public Phone() {
			Country = CountryCode.UnitedKingdom;
		}

		public Phone(string number) {
			Country = CountryCode.UnitedKingdom;
			Number = number;
		}

		public Phone(CountryCode country, string number) {
			Country = country;
			Number = number;
		}

		public CountryCode Country { get; set; }
		public string Number { get; set; }

		public enum CountryCode {
			Afghanistan = 93,
			ÅlandIslands = 358,
			Albania = 355,
			Algeria = 213,
			AmericanSamoa = 1-684,
			Andorra = 376,
			Angola = 244,
			Anguilla = 1-264,
			AntiguaBarbuda = 1-268,
			Argentina = 54,
			Armenia = 374,
			Aruba = 297,
			Australia = 61,
			Austria = 43,
			Azerbaijan = 994,
			Bahamas = 1-242,
			Bahrain = 973,
			Bangladesh = 880,
			Barbados = 1-246,
			Belarus = 375,
			Belgium = 32,
			Belize = 501,
			Benin = 229,
			Bermuda = 1-441,
			Bhutan = 975,
			Bolivia = 591,
			BosniaHerzegovina = 387,
			Botswana = 267,
			Brazil = 55,
			BritishVirginIslands = 1-284,
			Brunei = 673,
			Bulgaria = 359,
			BurkinaFaso = 226,
			Burma = 95,
			Burundi = 257,
			Cambodia = 855,
			Cameroon = 237,
			Canada = 1,
			CapeVerde = 238,
			CaymanIslands = 1-345,
			CentralAfricanRepublic = 237,
			Chad = 235,
			Chile = 56,
			China = 86,
			Columbia = 57,
			Comoros = 269,
			Congo = 242,
			CookIslands = 682,
			CostaRica = 506,
			CôteIvoire = 225,
			Croatia = 385,
			Cuba = 53,
			Cyprus = 357,
			CzechRepublic = 420,
			DemocraticRepublicCongo = 243,
			Denmark = 45,
			Djibouti = 253,
			Dominica = 1-767,
			DominicanRepublic = 1-809,
			EastTimor = 670,
			Ecuador = 593,
			Egypt = 20,
			ElSalvador = 503,
			EquatorialGuinea = 240,
			Eritrea = 291,
			Estonia = 372,
			Ethiopia = 251,
			FalklandIslands = 500,
			FaroeIslands = 298,
			Fiji = 679,
			Finland = 358,
			France = 33,
			FrenchGuiana = 594,
			FrenchPolynesia = 689,
			Gabon = 241,
			Gambia = 220,
			Georgia = 995,
			Germany = 49,
			Ghana = 233,
			Gibraltar = 350,
			Greece = 30,
			Greenland = 299,
			Grenada = 1-473,
			Guadeloupe = 590,
			Guam = 1-671,
			Guatemala = 502,
			Guernsey = 44,
			Guinea = 224,
			GuineaBissau = 245,
			Guyana = 592,
			Haiti = 509,
			Honduras = 504,
			HongKong = 852,
			Hungary = 36,
			Iceland = 354,
			India = 91,
			Indonesia = 62,
			Iran = 98,
			Iraq = 964,
			Ireland = 353,
			IsleOfMan = 44,
			Israel = 972,
			Italy = 39,
			Jamaica = 1-876,
			Japan = 81,
			Jersey = 44,
			Jordan = 962,
			Kazakhstan = 7,
			Kenya = 254,
			Kiribati = 686,
			Kuwait = 965,
			Kyrgyzstan = 996,
			Laos = 856,
			Latvia = 371,
			Lebanon = 961,
			Lesotho = 266,
			Liberia = 231,
			Libya = 218,
			Liechtenstein = 423,
			Lithuania = 370,
			Luxembourg = 352,
			Macau = 853,
			Macedonia = 389,
			Madagascar = 261,
			Malawi = 265,
			Malaysia = 60,
			Maldives = 960,
			Mali = 220,
			Malta = 356,
			MarshallIslands = 692,
			Martinique = 596,
			Mauritania = 222,
			Mauritius = 230,
			Mayotte = 262,
			Mexico = 52,
			Micronesia = 691,
			Moldovia = 373,
			Monaco = 377,
			Mongolia = 976,
			Montenegro = 382,
			Montserrat = 1-664,
			Morocco = 212,
			Mozambique = 258,
			Namibia = 264,
			Nauru = 674,
			Nepal = 977,
			Netherlands = 31,
			NetherlandsAntilles = 1-869,
			NewCaledonia = 687,
			NewZealand = 64,
			Nicaragua = 505,
			Niger = 227,
			Nigeria = 234,
			NiueIsland = 676,
			NorfolkIsland = 6723,
			NorthKorea = 850,
			Norway = 47,
			Oman = 968,
			Pakistan = 92,
			Palau = 680,
			Panama = 507,
			PapuaNewGuinea = 675,
			Paraguay = 595,
			Peru = 51,
			Philippines = 63,
			Pitairn = 64,
			Poland = 48,
			Portugal = 351,
			PuertoRico = 1-787,
			Qatar = 974,
			Réunion = 262,
			Romania = 40,
			Russia = 7,
			Rwanda = 250,
			SaintBarthélemy = 590,
			SaintHelena = 247,
			SaintKitts = 1-869,
			SaintLucia = 1-758,
			SaintMartin = 590,
			SaintPierre = 508,
			SaintVincent = 1-784,
			Samoa = 685,
			SanMarino = 378,
			SãoTomé = 239,
			SaudiArabia = 966,
			Senegal = 221,
			Serbia = 381,
			Seychelles = 248,
			SierraLeone = 232,
			Singapore = 65,
			Slovakia = 421,
			Slovenia = 386,
			SolomonIslands = 677,
			Somalia = 252,
			SouthAfrica = 27,
			SouthKorea = 82,
			Spain = 34,
			SriLanka = 94,
			Sudan = 249,
			Svalbard = 47,
			Swaziland = 268,
			Sweden = 46,
			Switzerland = 41,
			Syria = 963,
			Taiwan = 886,
			Tajikistan = 992,
			Tanzania = 255,
			Thailand = 66,
			Togo = 228,
			Tokelau = 690,
			Tonga = 676,
			TrinidadTobago = 1-868,
			Tunisia = 216,
			Turkey = 90,
			Turkmenistan = 993,
			TurksCaicosIslands = 1-649,
			Tuvalu = 688,
			USVirginIslands = 1-340,
			Uganda = 256,
			Ukraine = 380,
			UnitedArabEmirates = 971,
			UnitedKingdom = 44,
			UnitedStates = 1,
			Uruguay = 598,
			Uzbekistan = 998,
			Vanuatu = 678,
			VaticanCity = 39,
			Venezuela = 58,
			Vietnam = 84,
			WallisFutuna = 681,
			Yemen = 967,
			Zambia = 260,
			Zimbabwe = 263,	
		}



	
		//**********************************************************************************************************************
		//FUNCTION:	correctly formats phone numbers - removes any non-digit characters and inserts a space at the correct position - mainly for saving a phone number into a database
		//**********************************************************************************************************************
		public virtual bool Format() {

			if (!String.IsNullOrWhiteSpace(Number)) {
				
				//remove anything that is not a number
				Number = Regex.Replace(Number, @"\D", "");
				
				//remove the initial 0, if there is one
				Number = Number.TrimStart(new char[] { '0' });

				//check if it is a valid phone number
				if (Regex.IsMatch(Number, @"^\d{10,30}$")) {

					switch (Country) {
						case CountryCode.UnitedKingdom:
							Number = FormatUKNumber(Number);
							break;
					}

					return true;
				}
			}

			return false;
		}






		//**********************************************************************************************************************
		//FUNCTION:	formats UK phone numbers - adds spaces in the correct places
		//**********************************************************************************************************************
		private string FormatUKNumber(string number) {

			if (!String.IsNullOrWhiteSpace(number)) {

				//if the number begins with '2', then insert a space after the second digit
				if (number.StartsWith("2")) {
					number = number.Insert(2, " ");
				}
				else if (number.StartsWith("1")) {

					//if the number begins with a short code
					if (number.StartsWith("11")) {
						//insert a space after the third digit
						number = number.Insert(3, " ");
					}
					else {
						//if the number begins with a short code
						switch (number.Substring(0, 3)) {
							case "121":
							case "131":
							case "141":
							case "151":
							case "161":
							case "171":
							case "181":
							case "191":
								//insert a space after the third digit
								number = number.Insert(3, " ");
								break;
							default:
								//insert a space after the fourth digit
								number = number.Insert(4, " ");
								break;
						}
					}
				}
				else {
					//insert a space after the fourth digit
					number = number.Insert(4, " ");
				}

				//add second space
				string actualNumber = number.Split()[1];
				if (actualNumber.Length >= 8) {
					number = number.Insert(number.Length - 4, " ");
				}
				else if (actualNumber.Length > 5) {
					number = number.Insert(number.Length - 3, " ");
				}

			}

			return number;
		}



	}
}