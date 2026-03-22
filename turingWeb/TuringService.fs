module TuringService
open Microsoft.AspNetCore.Http

let createTuringMachine input = task {
    return Results.Ok $"Hello from Turing Machine {input}"
}