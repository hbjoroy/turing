open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open TuringService
open Microsoft.AspNetCore.Http
open System.Threading.Tasks

type TuringInput = {
    instructions: string
}

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddDistributedMemoryCache() |> ignore
    builder.Services.AddSession() |> ignore
    
    let app = builder.Build()
    app.UseSession() |> ignore
    app.UseStaticFiles() |> ignore    
    app.MapPost("/service/turing", Func<TuringInput,Task<IResult>>
        (fun message -> createTuringMachine message.instructions)) |> ignore
    app.Run()

    0 // Exit code
