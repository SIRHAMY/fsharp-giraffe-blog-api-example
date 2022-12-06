module Blog 

open Microsoft.AspNetCore.Http
open Giraffe

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

let createPostHttpHandler (serviceTree: BlogServiceTree) (newPost : BlogPost) =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        serviceTree.getBlogDb().AddPost(newPost) |> ignore
        json (newPost) next ctx