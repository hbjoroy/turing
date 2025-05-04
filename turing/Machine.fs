module Turing.Machine
open Turing.Tape
open Turing.Instructions

type Machine = {
    program: Program
    tape: Tape
    currentState: string
}

let doStep machine  =
    let symbol = read machine.tape
    
    machine.program.states
    |> List.tryFind (fun state -> state.name = machine.currentState)
    |> Option.bind (fun state ->
        state.transitions
        |> List.tryFind (fun t -> t.current = symbol))
    |> function
    | Some t ->
        let newTape = write machine.tape t.write
        let newTape =
            match t.movement with
            | Left -> moveLeft newTape
            | Right -> moveRight newTape
            | Stay -> newTape
        
        let newMachine = {
                program = machine.program
                tape = newTape
                currentState = t.newStateName
            }
        if List.contains newMachine.currentState newMachine.program.finalStateNames then            
            Finished newMachine
        else
            Running newMachine
    | None ->
        // No transition found, return halted machine
        Halted machine
let rec runMachine display steps machine =
    match machine with
    | Running m ->
        display steps m
        doStep m
        |> runMachine display (steps + 1)
    | _ -> machine

let newMachine program input =
    {
        program = program
        tape = intializeTape input
        currentState = program.initialStateName
    } |> Running