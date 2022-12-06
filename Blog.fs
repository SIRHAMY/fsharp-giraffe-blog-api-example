module Blog 

open System
open System.Threading.Tasks
open Microsoft.AspNetCore.Http
open Giraffe

[<CLIMutable>]
type BlogPost = {
    title: string
    content: string
}

type BlogDb() =

    let mutable allBlogPosts : BlogPost list = []

    member this.GetAllPosts = fun() -> allBlogPosts 

    member this.AddPost (newPost : BlogPost) =
        allBlogPosts <- (newPost::allBlogPosts)
        allBlogPosts

type BlogServiceTree = {
    getBlogDb : unit -> BlogDb
}

let getPostsHttpHandler (serviceTree: BlogServiceTree) =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        json (serviceTree.getBlogDb().GetAllPosts()) next ctx

let createPostHttpHandler (serviceTree: BlogServiceTree) =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! newPostJson = ctx.BindJsonAsync<BlogPost>()
            serviceTree.getBlogDb().AddPost(newPostJson) |> ignore
            return! json (newPostJson) next ctx
        }