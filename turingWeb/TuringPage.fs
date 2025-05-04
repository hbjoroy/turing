module TuringPage
open System.IO

let turingPage (context: Microsoft.AspNetCore.Http.HttpContext) =
    let template = File.ReadAllText "TuringPageTemplate.html"
    context.Response.ContentType <- "text/html"
    template