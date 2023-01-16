_This repo is now archived. Subscribers to the [HAMINION membership](https://hamy.xyz/labs/haminions) have access to updates as well as full source code access for dozens of tutorials and examples._

# Overview

This repo aims to provide a simple example of a web API built with F# + [Giraffe](https://github.com/giraffe-fsharp/Giraffe) - a micro web framework on ASP.NET.

Full code walkthrough: [Build a simple F# web API with Giraffe](https://hamy.xyz/labs/2022-12-simple-fsharp-web-api-giraffe)

# API Overview

This is a simple blog api with two endpoints:

* `GET /posts` - Returns all blog posts
* `POST /posts/create` - Creates a new blog post with the json payload:
    * title : string
    * content : string

# Running the code

Requirements:

* .NET SDK

To Run:

`dotnet run`
