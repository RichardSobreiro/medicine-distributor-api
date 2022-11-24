db.createUser(
  {
    user: "sobreiro",
    pwd: "123456",
    roles: [
      {
        role: "readWrite",
        db: "shoppingcart"
      }
    ]
  }
);
db.createCollection('Products');
db.createCollection('MeasurementUnits');
db.MeasurementUnits.insertMany(
	[
		{_id: new ObjectId("6346d76fc2a561172dbc67d8"),MeasurementUnitDescription:"mg/ml"},
		{_id: new ObjectId("6346d96431dd9b738aa27555"),MeasurementUnitDescription:"mcg/ml"}
	]
);