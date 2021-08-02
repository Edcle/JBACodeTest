# JBACodeTest

Test app to demonstration manipulation of data from a .pre file and insertion into a SQL database.

Notes:
1) This uses a local .mdf file as the database. I would have liked to have the app create this dynamically but struggled to get this working and decided that it probably wasn't the interesting part. In any case, it will create the "Precipitation" table if it doesn't already exist.
2) Dates are stored as SQL dates rather than the american-style MM/DD/YYYY style suggested in the spec. Hopefully storing as a true date object is more desirable! I thought about storing the date as a string in the format given but that felt more like a perverse adherence to the exact letter of the spec.
