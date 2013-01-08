using System;

namespace Improsys.ContactImporter.Util
{
	/// <summary>
	/// Summary description for Buffer.
	/// </summary>
	internal class Buffer
	{
		private String data  = "";
		public Buffer()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public String Data
		{
			set
			{
				data = value;
			}
			get
			{
				return data;
			}
		}

	}
}
