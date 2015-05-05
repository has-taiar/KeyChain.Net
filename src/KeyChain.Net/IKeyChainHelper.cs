using System;

namespace KeyChain.Net
{
	public interface IKeyChainHelper
	{
		bool SetKey(string name, string value);
		bool SaveKey(string name, string value);
		string GetKey(string name);
		bool DeleteKey(string name);
	}
}

