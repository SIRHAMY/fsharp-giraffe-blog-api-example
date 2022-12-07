open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection

open Blog
open Giraffe

(* Web App Configuration *)

let webApp = 
    let blogDb = new BlogDb()

    let serviceTree = {
        getBlogDb = fun() -> blogDb
    }

    choose[
        route "/" >=> text "iamanapi"
        subRoute "/posts" 
            (choose [
                route "" >=> GET >=> warbler (fun _ -> 
                    (getPostsHttpHandler serviceTree))
                route "/create" 
                    >=> POST 
                    >=> warbler (fun _ -> 
                        (createPostHttpHandler serviceTree))
            ])
    ]

(* Infrastructure Configuration *)

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe (webApp)

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0