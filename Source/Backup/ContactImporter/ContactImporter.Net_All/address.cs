using System;
using System.Collections;

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for address.
	/// </summary>
	class addresses:IEnumerable
	{
		public ArrayList m_addresstitles;

		public addresses()
		{
			m_addresstitles = new ArrayList();
		}

		public void Add(string addresstitle)
		{
			m_addresstitles.Add(addresstitle);
		}

		public void Remove(string addresstitle)
		{
			m_addresstitles.Remove(addresstitle);
		}

		public IEnumerator GetEnumerator()
		{
			return new addressesEnumerator(this);
		}
	}
}
