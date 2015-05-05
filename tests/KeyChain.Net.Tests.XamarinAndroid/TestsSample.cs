using System;
using NUnit.Framework;
using KeyChain.Net.XamarinAndroid;

namespace KeyChain.Net.Tests.XamarinAndroid
{
	[TestFixture]
	public class TestsSample
	{
		IKeyChainHelper _keyChainHelper;

		[SetUp]
		public void Setup ()
		{
			_keyChainHelper = new KeyChainHelper(TestApplication.Context, "myKeyProtectionPassword");
		}

		[Test]
		public void SetKey_ShouldStoreKeyInKeyChain_ReturnsTrue ()
		{
			var isSaved = _keyChainHelper.SetKey("myKey", "myKeyValue");
			Assert.IsTrue(isSaved);
			Assert.AreEqual("myKeyValue", _keyChainHelper.GetKey("myKey"));
		}

		[Test]
		public void GetKey_WhenKeyExists_ShouldReturnKey ()
		{
			var isSaved = _keyChainHelper.SaveKey("myKey", "myKeyValue");
			Assert.IsTrue(isSaved);

			var keyValue = _keyChainHelper.GetKey("myKey");
			Assert.AreEqual("myKeyValue", keyValue);
		}

		[Test]
		public void GetKey_WhenKeyDoesNotExists_ShouldReturnNull ()
		{
			var keyValue = _keyChainHelper.GetKey("myKey111");
			Assert.IsTrue(string.IsNullOrEmpty(keyValue));
		}

		[Test]
		public void DeleteKey_ShouldReturnTrue ()
		{
			var isSaved = _keyChainHelper.SaveKey("myKey", "myKeyValue");
			Assert.IsTrue(isSaved);

			var isDeleted = _keyChainHelper.DeleteKey("myKey");
			Assert.IsTrue(isDeleted);

			Assert.IsTrue(string.IsNullOrEmpty(_keyChainHelper.GetKey("myKey")));
		}

		[TearDown]
		public void Tear ()
		{
			_keyChainHelper = null;
		}

	}
}

