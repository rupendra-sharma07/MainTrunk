using System;
using System.Collections;
namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for addressesEnumerator.
	/// </summary>
	class addressesEnumerator : IEnumerator
	{
		private int position = -1;
		private addresses addresses;

		public addressesEnumerator(addresses addresses)
		{
			this.addresses = addresses;
		}

		public bool MoveNext()
		{
			if (position < addresses.m_addresstitles.Count - 1)
			{
				position++;
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Reset()
		{
			position = -1;
		}
		public int Total()
		{
			return position;
		}

		public object Current
		{
			get
			{
				return addresses.m_addresstitles[position];
			}
		}
	}
}
