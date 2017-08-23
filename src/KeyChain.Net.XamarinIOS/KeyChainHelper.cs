using System;
using Security;
using Foundation;

namespace KeyChain.Net.XamarinIOS
{
	public class KeyChainHelper : IKeyChainHelper
	{
		public KeyChainHelper () : this(_myServiceId, _defaultSyncSetting, _accessiblityPolicy)
		{			
		}
	
		public KeyChainHelper (string serviceId, bool syncWithICloud, SecAccessible accessibilityPolicy)
		{	
			if (string.IsNullOrEmpty(serviceId)) throw new ArgumentNullException("ServiceId cannot be null");		

			_myServiceId = serviceId;
			_defaultSyncSetting = syncWithICloud;
			_accessiblityPolicy = accessibilityPolicy;
		}

		private static string _myServiceId = "keyChainHelperService";
		private static bool _defaultSyncSetting = false;
		private static SecAccessible _accessiblityPolicy = SecAccessible.Always;

		/// <summary>
		/// Save a Key (or a Password) to the KeyChain
		/// </summary>
		/// <returns><c>true</c>, if key was saved, <c>false</c> otherwise.</returns>
		/// <param name="keyName">Key name or username.</param>
		/// <param name="keyValue">Key value or password.</param>
		public bool SetKey (string keyName, string keyValue)
		{
			var result = SetPassword (keyName, keyValue, _myServiceId, _accessiblityPolicy, _defaultSyncSetting);
			return result == SecStatusCode.Success;
		}

		/// <summary>
		/// Same as SetKey(name, value), but it deletes any old key before attempting to save
		/// </summary>
		/// <returns><c>true</c>, if key was saved, <c>false</c> otherwise.</returns>
		/// <param name="keyName">Key name.</param>
		/// <param name="keyValue">Key value.</param>
		public bool SaveKey(string keyName, string keyValue)
		{
			DeleteKey(keyName);

			return SetKey(keyName, keyValue);
		}

		/// <summary>
		/// Gets the key/password value from the keyChain.
		/// </summary>
		/// <returns>The key/password (or null if the password was not found in the KeyChain).</returns>
		/// <param name="keyName">Keyname/username.</param>
		public string GetKey (string keyName)
		{
			return GetPassword(keyName, _myServiceId, _defaultSyncSetting);
		}

		/// <summary>
		/// Deletes a key (or a password) from the KeyChain.
		/// </summary>
		/// <returns><c>true</c>, if key was deleted, <c>false</c> otherwise.</returns>
		/// <param name="keyName">Key name (or username).</param>
		public bool DeleteKey(string keyName)
		{
			var result = DeletePassword(keyName, _myServiceId, _defaultSyncSetting);
			return result == SecStatusCode.Success;
		}

		/// <summary>
		/// Deletes a username/password record.
		/// </summary>
		/// <param name="username">the username to query. Not case sensitive. May not be NULL.</param>
		/// <param name="serviceId">the service description to query. Not case sensitive.  May not be NULL.</param>
		/// <param name="synchronizable">
		/// Defines if the record you want to delete is syncable via iCloud keychain or not. Note that using the same username and service ID
		/// but different synchronization settings will result in two keychain entries.
		/// </param>
		/// <returns>Status code</returns>
		private static SecStatusCode DeletePassword (string username, string serviceId, bool synchronizable)
		{
			if ( username == null )
			{
				throw new ArgumentNullException ( "userName" );
			}
			
			if ( serviceId == null )
			{
				throw new ArgumentNullException ( "serviceId" );
			}
			
			// Querying is case sesitive - we don't want that.
			username = username.ToLowerInvariant (  );
			serviceId = serviceId.ToLowerInvariant (  );
			
			// Query and remove.
			SecRecord queryRec = new SecRecord ( SecKind.GenericPassword ) { Service = serviceId, Label = serviceId, Account = username, Synchronizable = synchronizable };
			SecStatusCode code = SecKeyChain.Remove ( queryRec );
			return code;
		}

		/// <summary>
		/// Sets a password for a specific username.
		/// </summary>
		/// <param name="username">the username to add the password for. Not case sensitive.  May not be NULL.</param>
		/// <param name="password">the password to associate with the record. May not be NULL.</param>
		/// <param name="serviceId">the service description to use. Not case sensitive.  May not be NULL.</param>
		/// <param name="secAccessible">defines how the keychain record is protected</param>
		/// <param name="synchronizable">
		/// Defines if keychain record can by synced via iCloud keychain.
		/// Note that using the same username and service ID but different synchronization settings will result in two keychain entries.
		/// </param>
		/// <returns>SecStatusCode.Success if everything went fine, otherwise some other status</returns>
		private static SecStatusCode SetPassword ( string username, string password, string serviceId, SecAccessible secAccessible, bool synchronizable )
		{
			if ( username == null ) {
				throw new ArgumentNullException ( "userName" );
			}
			
			if ( serviceId == null ) {
				throw new ArgumentNullException ( "serviceId" );
			}
			
			if ( password == null ) {
				throw new ArgumentNullException ( "password" );
			}
			
			// Querying is case sesitive - we don't want that.
			username = username.ToLowerInvariant (  );
			serviceId = serviceId.ToLowerInvariant (  );
			
			// Don't bother updating. Delete existing record and create a new one.
			DeletePassword ( username, serviceId, synchronizable );
			
			// Create a new record.
			// Store password UTF8 encoded.
			SecStatusCode code = SecKeyChain.Add ( new SecRecord ( SecKind.GenericPassword ) {
				Service = serviceId,
				Label = serviceId,
				Account = username,
				Generic = NSData.FromString ( password, NSStringEncoding.UTF8 ),
				Accessible = secAccessible,
				Synchronizable = synchronizable
			} );
			
			return code;
		}

		/// <summary>
		/// Gets a password for a specific username.
		/// </summary>
		/// <param name="username">the username to query. Not case sensitive.  May not be NULL.</param>
		/// <param name="serviceId">the service description to use. Not case sensitive.  May not be NULL.</param>
		/// <param name="synchronizable">
		/// Defines if the record you want to get is syncable via iCloud keychain or not. Note that using the same username and service ID
		/// but different synchronization settings will result in two keychain entries.
		/// </param>
		/// <returns>
		/// The password or NULL if no matching record was found.
		/// </returns>
		private static string GetPassword ( string username, string serviceId, bool synchronizable )
		{
			if ( username == null )
			{
				throw new ArgumentNullException ( "userName" );
			}
			
			if ( serviceId == null )
			{
				throw new ArgumentNullException ( "serviceId" );
			}
			
			// Querying is case sesitive - we don't want that.
			username = username.ToLowerInvariant (  );
			serviceId = serviceId.ToLowerInvariant (  );
			
			SecStatusCode code;
			// Query the record.
			SecRecord queryRec = new SecRecord ( SecKind.GenericPassword ) { Service = serviceId, Label = serviceId, Account = username, Synchronizable = synchronizable };
			queryRec = SecKeyChain.QueryAsRecord ( queryRec, out code );
				
			// If found, try to get password.
			if ( code == SecStatusCode.Success && queryRec != null && queryRec.Generic != null )
			{
				// Decode from UTF8.
				return NSString.FromData ( queryRec.Generic, NSStringEncoding.UTF8 );
			}
			
			// Something went wrong.
			return null;
		}
	}
}

