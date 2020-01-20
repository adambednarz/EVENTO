# Evento
Building the second serious application following the next course of Piotr Gankiewicz (link to the course at the end)

## Getting Started
The repository contains application created in Visual Studio Code 2019 with C# (.Net Core) technology.

## What is Evento?
Backend application created in ASP.Net Core whose main purpose is to enable users to buy tickets for the events

## What is the purpose of coding Evento application?
The main goal is to expand and consolidate the knowledge that I learned in a previous course by the same author
(Passenger, which can also be found in my repository)

Learning to build an application using the HTTP protocol.
Learning clean architecture structure and design patterns, what is MVC and how to design RESTfull API and many more. 

## Application construction

The entire application is build from 5 different projects (using onion architecture):

* Api - contains MVC, controllers, appsettings (saparated from Core)
* Core - center of onion with Domain classes
* Infrastructure - contains all business logic e.g. services, handlers, mappers, repositories, extensions
* Tests - project for unit tests
* Tests end to end - project for integration tests
 
## What's interesting here?

* DTO
* Mapper
* Moq
* Autofac
* JWT
* xUnit

## Thanks

I would like to thank Piotr from this place for sharing such a great course. He opened my eyes and showed me how to program a professional.
 
Links:
http://piotrgankiewicz.com/courses/becoming-a-software-developer/
