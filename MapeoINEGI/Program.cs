using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MapeoINEGI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int reemplazos = 0;

			string metadatos;
			string geojson;
			StringBuilder sb = new StringBuilder();
			string patron;

			string geojsonPath = @"/Users/eherrador/Desktop/GFK/GeoJSON/Originales/";
			string geojsonModiPath = @"/Users/eherrador/Desktop/GFK/GeoJSON/Modificados V2/";
			string geojsonReportePath = @"/Users/eherrador/Desktop/GFK/GeoJSON/Modificados V2/Reportes/";

			//string geojsonFileName = @"AGSUrbAgeb.json";
			//string geojsonFileName = @"BCUrbAgeb.json";
			//string geojsonFileName = @"DFUrbAgeb.json";
			//string geojsonFileName = @"GTOUrbAgeb.json";
			//string geojsonFileName = @"JLUrbAgeb.json";
			//string geojsonFileName = @"MEXUrbAgeb.json";
			//string geojsonFileName = @"MORUrbAgeb.json";
			//string geojsonFileName = @"NLUrbAgeb.json";
			//string geojsonFileName = @"QROUrbAgeb.json";
			//string geojsonFileName = @"SINUrbAgeb.json";
			//string geojsonFileName = @"SLPUrbAgeb.json";
			string geojsonFileName = @"SONUrbAgeb.json";

			string metadatosFile = @"/Users/eherrador/Desktop/GFK/GeoJSON/Metadatos/TodasDescripciones.txt";

			try {
				using (StreamReader streamGeoJSON = new StreamReader(geojsonPath + geojsonFileName))
				{
					geojson = streamGeoJSON.ReadToEnd();
					Console.WriteLine("GeoJSON leído...");
					Console.WriteLine(geojson);
				
					using (StreamReader streamMetadatos = new StreamReader(metadatosFile))
					{
						while((metadatos = streamMetadatos.ReadLine()) != null)
						{
							string[] datos = metadatos.Split(':'); //LLave -> datos[0]       Valor -> datos[1]
							if (geojson.Contains(datos[0])) {
								Console.WriteLine ("Sustituyendo: " + datos[0] + " -> " + datos[1]);
								sb.AppendLine("Sustituyendo: " + datos[0] + " -> " + datos[1]);
								//geojson = geojson.Replace(datos[0], datos[1]);
								patron = "\\b" + datos[0] + "\\b";
								geojson = Regex.Replace(geojson, patron,  datos[1]);
								reemplazos++;

							}
							else {
								Console.WriteLine("El geojson no contiene a " + datos[0]);
								sb.AppendLine("El geojson no contiene a " + datos[0] + ". No hay información sobre " + datos[1]);
							}
						}
					}
				}

				Console.WriteLine("=============================");
				Console.WriteLine(geojson);

				using (StreamWriter outfile = new StreamWriter(geojsonModiPath + geojsonFileName))
				{
					outfile.Write(geojson);
				}
				using (StreamWriter outfile = new StreamWriter(geojsonReportePath + geojsonFileName + ".txt"))
				{
					outfile.Write(sb.ToString());
					outfile.WriteLine();
					outfile.Write("Se realizaron " + reemplazos + " reemplazos");
				}

				Console.WriteLine("Proceso terminado...");
				Console.ReadLine();
			}
			catch (Exception e) {

				Console.WriteLine (e.Message);
			}
		}
	}
}
