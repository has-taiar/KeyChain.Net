using Android.App;
using Android.Content;
using Android.Runtime;
using System;

namespace KeyChain.Net.Tests.XamarinAndroid
{
    [Application (Debuggable = true)]
    public class TestApplication : Application
    {            
        /// <summary>
        /// Base constructor which must be implemented if it is to successfully inherit from the Application
        /// class.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="transfer"></param>
        public TestApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }
    }
}