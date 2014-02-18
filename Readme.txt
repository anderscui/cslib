

"Lifeasier"

1. Smart Copier
	1) Files can be multiselected;
	2) When copying files, the relative path can be reserved;
	
2. VSS Helper
	1) Check checkedout items;
	2) Batch checkin;


References:
1. Data Structures and Algorithms with Object-Oriented Design Patterns in C# (Bruno R. Preiss etc.)
2. Data Structures and Problem Solving Using Java (Mark Allen Weiss)
3. The Design and Analysis of Algorithms (Levitin .A)
4. Beauty of Code


"Andersc.CodeLib.GenGen"
This app is expected to become a general-purpose generator-GenGen.

first step:
    db
        diff db driver;
        diff db objects(table, trigger, data records and others)
        support template;
        custom gen rules;


design:
    data source
        select objects

result of GetSchema() method
MetaDataCollections
DataSourceInformation
DataTypes
Restrictions
ReservedWords
Users
Databases
Tables
Columns
AllColumns
ColumnSetColumns
StructuredTypeMembers
Views
ViewColumns
ProcedureParameters
Procedures
ForeignKeys
IndexColumns
Indexes
UserDefinedTypes

Table structure:
TABLE_CATALOG: Db Name
TABLE_SCHEMA: dbo
TABLE_NAME: SiteVersion
TABLE_TYPE: BASE TABLE/VIEW

