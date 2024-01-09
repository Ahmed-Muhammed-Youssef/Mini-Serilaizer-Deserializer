# Mini Serializer/ Deserializer

A lightweight library for serializing and deserializing objects to XML and JSON formats. Currently supports XML serialization with JSON support in progress.

## XML Serializer 
### Features

- Serialize any .NET object to XML
- Support for primitive types, collections, complex object graphs
- Configure DateTime format output
- Ignore null valued properties
- Handle circular references

### Usage

```csharp
var obj = new MyClass { ... }; 

var xmlSerializer = new MiniXmlSerializer();
var xml = xmlSerializer.Serialize(obj);
```

### Configuration

The serializer can be configured through MiniXmlSerializerConfigurations class:

```csharp
var config = new MiniXmlSerializerConfigurations()
{
  DateTimeFormat = "yyyy-MM-dd",
  MapEnumsNumericValues = true
};
var xmlSerializer = new MiniXmlSerializer(config);
```

## XML Deserializer 

**Underdevelopment**

## Json Serializer 

**Underdevelopment**

## Json Deserializer 

**Underdevelopment**

## Roadmap

- Complete XML deserialization implementation
- Complete JSON (de)serialization implementation
- Documentation and examples

The goal is to have a simple yet full-featured serialization library that can be used across different .NET projects and domains. Feedback welcome!

Let me know if you have any other questions!
