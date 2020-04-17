# Lightest Night

## System &rightarrow; Utilities

Utility and helpers that underpin the LightestNight ecosystem

### Extensions
#### Assembly Extensions
* `Assembly.GetInstancesOfInterface(Type interfaceType)`
  * Gets all instances of the interfaceType
  
* `GetInstancesOfInterface<T>()`
  * Gets all instances of the interface type `T`
  
#### Enumerable Extensions
* `Enumerable.IsNullOrEmpty<T>()`
  * Returns a boolean denoting whether the enumerable is either `null` or empty
  
### Generators
#### Guid
* `GuidGenerator.GenerateTimeBasedGuid()`
  * Generates a new Guid based on the current date &amp; time
  
* `GuidGenerator.GenerateTimeBasedGuid(DateTime dateTime)`
  * Generates a new Guid based on the given date &amp; time
  
* `GuidGenerator.GenerateTimeBasedGuid(DateTimeOffset dateTime)`
  * Generates a new Guid based on the given Date Time Offset
  
* `DateTimeOffset GuidGenerator.GetDateTimeOffset(Guid guid)`
  * Gets the Date Time Offset that was used to generate the Guid
  
* `DateTime GuidGenerator.GetDateTime(Guid guid)`
  * Gets the Date &amp; Time that was used to generate the Guid
  
* `DateTime GuidGenerator.GetLocalDateTime(Guid guid)`
  * Gets the Date &amp; Time that was used to generate the Guid in local time
  
* `DateTime GuidGenerator.GetUtcDateTime(Guid guid)`
  * Gets the Date &amp; Time that was used to generate the Guid in UTC
  
### Attributes
* `GetCustomAttributeValue<TAttributeType, TValue>(MemberInfo type, Func<TAttributeType, TValue> predicate, TValue defaultValue)`
  * Allows the retrieval of a value from an attribute property using the given predicate