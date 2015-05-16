rmdir /s /q "KeyChain.Net"
rmdir /s /q "KeyChain.Net.XamarinAndroid"
rmdir /s /q "KeyChain.Net.XamarinIOS"
rmdir /s /q "KeyChain.Net.XamarinWinPhone"
rmdir /s /q "output" 

mkdir KeyChain.Net
copy /y ..\src\KeyChain.Net\bin\Release\KeyChain.Net.dll KeyChain.Net

mkdir KeyChain.Net.XamarinAndroid
copy /y ..\src\KeyChain.Net.XamarinAndroid\bin\Release\KeyChain.Net.XamarinAndroid.dll KeyChain.Net.XamarinAndroid
copy /y ..\src\KeyChain.Net\bin\Release\KeyChain.Net.dll KeyChain.Net.XamarinAndroid

mkdir KeyChain.Net.XamarinIOS
copy /y ..\src\KeyChain.Net.XamarinIOS\bin\Release\KeyChain.Net.XamarinIOS.dll KeyChain.Net.XamarinIOS
copy /y ..\src\KeyChain.Net\bin\Release\KeyChain.Net.dll KeyChain.Net.XamarinIOS

mkdir KeyChain.Net.XamarinWinPhone
copy /y ..\src\KeyChain.Net.XamarinWinPhone\bin\Release\KeyChain.Net.XamarinWinPhone.dll KeyChain.Net.XamarinWinPhone
copy /y ..\src\KeyChain.Net\bin\Release\KeyChain.Net.dll KeyChain.Net.XamarinWinPhone


pause