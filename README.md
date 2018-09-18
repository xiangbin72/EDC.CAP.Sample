# EDC.CAP.Demo

# About CAP
[![N|CAP](https://camo.githubusercontent.com/452505edb71d41f2c1bd18907275b76291621e46/687474703a2f2f696d61676573323031352e636e626c6f67732e636f6d2f626c6f672f3235303431372f3230313730372f3235303431372d32303137303730353137353832373132382d313230333239313436392e706e67)](https://github.com/dotnetcore/CAP/)

CAP is a library based on .Net standard, which is a solution to deal with distributed transactions, also has the function of EventBus, it is lightweight, easy to use, and efficiently.<br/>
The author of CAP is savorboard, his blog is https://www.cnblogs.com/savorboard.<br/>
If you want to know more about CAP, please view this wiki: https://github.com/dotnetcore/CAP/wiki

# About this Demo
This is a simple demo on the data consistency based on the open source project CAP. It is a classic scenario on micro-service, which is also a distributed-transaction demo. The technologies this demo used is as below :

  - ASP.NET Core 2.0
  - RabbitMQ
  - MS SQL
  - Dapper
  - CAP

> The CAP supports both RabbitMQ and Kafka, we only need choose one of them for message queue.<br/>
> The CAP supports MSQL, MySQL, MongoDB and so on. I choose MSSQL to show in this demo.<br/>
> ASP.NET Core is one excellent technology which is better to implement micro-service in your project.

The struecture of this demo is as below, there are 3 micro-services build on ASP.Net Core.
[![N|CAP](https://github.com/EdisonChou/EDC.CAP.Demo/blob/master/doc/CAP%20Demo.png)](http://edisonchou.cnblogs.com)

A simple business logic introduction:

* [OrderService] - Receive Post request from service client (eg.MVC, SPA) and do some business logic such as add recrod to Order database, and send message to CAP (Here it will act as an Event-Bus).
* [CAP] - CAP will help us to send message to RabbitMQ/Kafka in order to tell subscriber (Here the Delivery Service and Storage Service are subscriber)
* [StorageService] - Once receive the message from CAP, will do some business logic such as reduce the storage number in Storage database and output some log info.
* [DeliveryService] - Once receive the message from CAP, will do some business logic such as add one record in Delivery database and output some log info.

All above steps are finished without error, it is a final result for the eventual consistency of data. Of course, if any error happend in one step, CAP will help us to control the transaction to keep data consistency.

### Installation
To use CAP, you need to install some packages as below.
CAP base component is necessary.
```sh
PM> Install-Package DotNetCore.CAP
```
RabbitMQ
```sh
PM> Install-Package DotNetCore.CAP.RabbitMQ
```
MSSQL
```sh
PM> Install-Package DotNetCore.CAP.SqlServer
```

### References
Find more : https://www.cnblogs.com/edisonchou/p/9363244.html



