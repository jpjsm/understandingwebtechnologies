# Programmer Group
**A programming skills sharing group**

## Under Windows, complete the gRPC Java example step by step
Keywords: Maven Java Apache snapshot

This paper gives a Java example of using Google RPC (grpc) to complete rpc step by step under Windows.~

This article will explain from the following parts.

- Generate code automatically according to proto - Write proto file, and automatically generate code required by gRPC under window s according to tools
- Code Composition - Gives the modular structure of Maven Engineering and implements the code step by step in each module.
- Testing - Testing the code written, including Server starting and binding services, Client connecting and invoking services
- Summary - A brief introduction to the content of this article~

Next, step by step, complete each part.~

>**Note**:
>> Instead of using an unkown executable from an unkown source !!  
>> Use the plugin available in  
https://mvnrepository.com/artifact/io.grpc/protoc-gen-grpc-java

>> - Select the latest version
>> - From `Files` click on _View All_
>> - Download the `protoc-gen-grpc-java-<latest-version>-windows-x86_64.exe`
>> - Generate the rpc communications code  
`protoc.exe --plugin=protoc-gen-grpc-java="<path-to-downloaded-plugin>\protoc-gen-grpc-java-<latest-version>-windows-x86_64.exe" --grpc-java_out=./ *.proto`

See: [https://programmer.group/under-windows-complete-the-grpc-java-example-step-by-step.html](https://programmer.group/under-windows-complete-the-grpc-java-example-step-by-step.html)
