# LucidUnits 

Provides simplified storage and arithmetitic of unit bound data with varied common units. If you are looking
for a package with broad built in Unit support you may want to look into Units.Net instead.

The purpose of this package is to provide a convenient way to store and use mixed units in 
wide ranging scenarious. Additional unit support is a great way to contribute.

#### Highlights:
- Numerous C# operator overloads 
- Mix diverse units in equations
- Data structure to save units with values `UnitValue`
- Validation

#### Built in Units:

###### Distance
- Inch - Foot - Yard - Mile - Meter - Centimeter - Millimeter - Kilometer 

###### Angle 
- Degree - Radian - Bearing - Rotation

### UnitValue

All values in LucidUnits derive from `UnitValue` which has two properties:
```c#
  public string Unit { get; }
  public double Value { get; set; }
```

This is a simple way to store numeric values with proper unit information. 
For example, here is a UnitValue as json:

```json
{
  "name": "John Doe",
  "height": {
      "unit": "FOOT",
      "value": 5.8
  }
}
```

By storing the unit with the value you can ensure consistence across disparate data sets.

### Automatic unit coercion

Any object derived from UnitValue will have full arithmetic functionality:


```c#
  var unitA = new UnitFoot(2);
  var unitB = new UnitInch(12);

  // Will be true
  Assert.IsTrue(unitA == (unitB + new UnitCentimeter(30.48)));

  // When combining units, the unit on the left of
  // the operator wins
  var unitC = unitA + unitB;

  // unitC is of unit Foot but can be compared for 
  // equality with any unit value
  Assert.IsTrue(new UnitMeter(.9144) == unitC);

  // Square units
  var squareFeet = unitA * unitA; 
  var alsoSquareFeet = unitA * unitB

  Assert.IsTrue(squareFeet > alsoSquareFeet);
```

### Custom units

One can create a custom unit and convert it to any other
'convertable' unit so long as a conversion between your custom unit
and any existing unit in the system, including existing custom ones, exists.

##### Defining and Registering:
To create a custom unit, derive from `UnitValue`:
```c#
  public UnitFootballField : UnitValue
  {
     // Defines the unit name and registers the unit with the system
     public static new string Unit { get; } = UnitValue.Register<UnitFootballField(
         "FootballField",              // The new unit's name
         UnitFoot.Unit,                // The base unit of conversion 
         fields => fields * 300,       // From the unit to the base unit conversion
         feet => feet / 300             // Too the unit from the base unit conversion
     );

     public UnitFootballField(double value) : base (Unit, value) {}
  }
```
---
##### Using: 
Now, simply convert to and from your custom unit and any other compatible unit in the system with ease.

```c#
  var field1 = new UnitFootballField(1);
  var 2FieldsLength = field1 * 2;

  Assert.AreEqual(600, 2FieldsLength.ConvertTo<UnitFoot>());
  Assert.AreEqual(182.88, 2FieldsLength.ConvertTo<UnitMeter>());

```
