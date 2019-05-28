# yy.ProtoInspector
A Fiddler Extension for inspecting ProtoBuf in HTTP requests and responses

# Dependencies
1. Fiddler v2.2.8 or above 
2. .Net Framework 4.6.1 or above
3. [protobuf-net](https://github.com/mgravell/protobuf-net)
4. [JSON.Net](https://www.newtonsoft.com/json)

# Basic Usage
1. Build the project. The build proces will automatically copy these three files to the directory ```%userprofile%\AppData\Local\Programs\Fiddler\Inspectors``` (if not, please manually copy them to this folder):  
    * ```Newtonsoft.Json.dll```
    * ```protobuf-net.dll```
    * ```yy.ProtoInspector.dll```
2. Launch Fiddler, select any request, click the "Inspectors" tab page, you can find "ProtoBuf" subtab. 
3. Click "ProtoBuf" subtab page, it should show something like this: ![protoinspector not initalized](/screenshots/protobuf-tab-not-initialized.png)
4. Click "Load Types" button to load the assembly containing the ProtoContracts defined using [protobuf-net](https://github.com/mgravell/protobuf-net)
5. Select the on-wire message type for the request (or the on-wire message type for the response if it's in the context of Response Insepector)
6. Now you can inspect the protobuf message in Fiddler: ![JSON text or JSON object tree](/screenshots/protobuf-tab.png)
