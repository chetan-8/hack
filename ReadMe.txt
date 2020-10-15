layer Communication

Request <-> API <-> Business <-> Service <-> Repository 

--------------------------Get Call-----------------
http://localhost:55442/api/Demo1/getcustomerbyid?id=1
---------------Put Call-------------
http://localhost:55442/api/Demo1/UpdateCustomer

Request

{
CustomerId = 2,
CustomerName = "Chetan_1"
}
-----------------Put Call----------------

http://localhost:55442/api/Demo1/CreateCustomer

Request

{
CustomerName = "Chetan"
}

-----------------Delete Call-------------

http://localhost:55442/api/Demo1/DeleteCustomer

Request

{
"CustomerId" : 1
}
--------------------------Please Run SQL.txt quries to create database and table--------------