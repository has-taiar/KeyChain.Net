using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Security.Credentials;

namespace KeyChain.Net.XamarinWinRT
{
    public class KeyChainHelper : IKeyChainHelper
    {
        private PasswordVault _passValut = null;
        private const string _RESOURCE = "KeyChain.NET";

        public KeyChainHelper()
        {
            _passValut = new PasswordVault();
        }

        public bool SetKey(string name, string value)
        {
            PasswordCredential cred = new PasswordCredential(_RESOURCE, name, value);
            _passValut.Add(cred);
            return true;
        }

        public bool SaveKey(string name, string value)
        {
            PasswordCredential cred = new PasswordCredential(_RESOURCE, name, value);
            _passValut.Add(cred);
            return true;
        }

        public string GetKey(string name)
        {
            try
            {
                PasswordCredential cred = _passValut.Retrieve(_RESOURCE, name);
                cred.RetrievePassword();
                return cred.Password;
            } catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteKey(string name)
        {
            PasswordCredential cred = null;
            try
            {
                cred = _passValut.Retrieve(_RESOURCE, name);
            } catch (Exception)
            {
                return false;
            }
            _passValut.Remove(cred);
            return true;
        }
    }
}
