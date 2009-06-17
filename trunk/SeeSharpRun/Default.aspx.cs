using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeeSharp
{
	public partial class _Default : System.Web.UI.Page
	{
		private string codeTemplate = 
@"using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class Program
{
	public static void Main(String[] args)
	{
	}
}
";

		

		protected void Page_Load( object sender, EventArgs e )
		{
			if (!IsPostBack)
				txtCode.Text = codeTemplate;
		}

		protected void btnTestCode_Click( object sender, EventArgs e )
		{
			txtRunResults.Text = String.Empty;
			string code = txtCode.Text;
			CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
			string[] referencedAssemblies = new string[] { "System.dll" };//, "System.Configuration", "System.Core", "System.Data", "System.Data.DataSetExtensions", "System.Xml", "System.Xml.Linq" };
			CompilerParameters compileParams = new CompilerParameters(referencedAssemblies);
			compileParams.GenerateInMemory = true;
			compileParams.MainClass = "Program";
			
			CompilerResults results = codeProvider.CompileAssemblyFromSource(compileParams, new string[] { code });
			if (results.Output.Count > 0)
			{
				foreach (string output in results.Output)
					txtRunResults.Text = txtRunResults.Text + output;
				return;
			}

			Stream stream = new MemoryStream();
			TextWriter outputWriter = new StreamWriter(stream);
			Console.SetOut(outputWriter);
			Assembly assembly = results.CompiledAssembly;
			
			Type type = assembly.GetExportedTypes().First(t=>t.Name=="Program");
			if (type != null)
			{
				MethodInfo[] methods = type.GetMethods();

				MethodInfo method = methods.First(m => m.Name == "Main");
				method.Invoke(null, new object[]{new string[]{}});
			}
//			MethodInfo entryPoint = assembly.EntryPoint;
//			entryPoint.Invoke(null, null);

			outputWriter.Flush();
			stream.Seek(0, SeekOrigin.Begin);
			TextReader reader = new StreamReader(stream);
			txtRunResults.Text = reader.ReadToEnd();
		}
	}
}
